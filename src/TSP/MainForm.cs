using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using TSP.GA;
using TSP.TimerGraphs;
using ZedGraph;
using ThreadState = System.Threading.ThreadState;

namespace TSP
{
    public partial class MainForm : Form
    {
        // Properties
        [STAThread]
        static void Main()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }

        #region Define Varibale
        PointPairList[] _pPlistTfg;
        PointPairList[] _pPlistTgg;
        PointPairList[] _pPlistGfg;

        TimeFitnessGraph TFG;
        TimeGenerationGraph TGG;
        GenerationFitnessGraph GFG;

        CancellationTokenSource tokenSource;

        int countCPUCore = 1;

        int StartedTick = 0;

        // Number Population
        int Npop = 500;

        // Number Keep Chromosome Size 
        int N_keep = 0;

        // Double Array Pn for save Rank
        double[] Pn;

        private ShapeContainer shapeContainer_allCityShape;
        public List<OvalShape> ovalShape_City = new List<OvalShape>();
        private List<LineShape> lineShape_Way = new List<LineShape>();

        // save number of all city
        private int counter_City = 0;
        public int Counter_City { get { return counter_City; } } // ReadOnly

        // Save DNA information in Chromosome Array
        Chromosome[] pop;

        // create new process or for end process
        Thread RunTime;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            shapeContainer_allCityShape = new ShapeContainer();
            // 
            // shapeContainer_allCityShape
            // 
            shapeContainer_allCityShape.Location = new Point(0, 0);
            shapeContainer_allCityShape.Margin = new Padding(0);
            shapeContainer_allCityShape.Size = new Size(this.Width, this.Height); //800, 600);
            shapeContainer_allCityShape.TabIndex = 0;
            shapeContainer_allCityShape.TabStop = false;

            this.Controls.Add(shapeContainer_allCityShape);
            //
            // Make up some data points from the Sine function
            _pPlistTfg = new PointPairList[2]; // [0] for Series GA and [1] for PGA (Time-Fitness) data
            _pPlistTfg[0] = new PointPairList(); // For Series GA (Time-Fitness) Data's
            _pPlistTfg[1] = new PointPairList(); // For Parallel GA (Time-Fitness) Data's

            _pPlistTgg = new PointPairList[2]; // [0] for Series GA and [1] for PGA (Time-Fitness) data
            _pPlistTgg[0] = new PointPairList(); // For Series GA (Time-Fitness) Data's
            _pPlistTgg[1] = new PointPairList(); // For Parallel GA (Time-Fitness) Data's

            _pPlistGfg = new PointPairList[2]; // [0] for Series GA and [1] for PGA (Time-Fitness) data
            _pPlistGfg[0] = new PointPairList(); // For Series GA (Time-Fitness) Data's
            _pPlistGfg[1] = new PointPairList(); // For Parallel GA (Time-Fitness) Data's
        }

        #region Thread Invoked
        public static void UIInvoke(Control uiControl, Action action)
        {
            if (!uiControl.IsDisposed)
            {
                if (uiControl.InvokeRequired)
                {
                    uiControl.BeginInvoke(action);
                }
                else
                {
                    action();
                }
            }
        }

        // ------------------------------------------------------------
        // This delegate enables asynchronous calls for setting
        // the Value property on a toolStripProgressBar control.
        delegate void SetValueCallback(int v);
        private void SetValue(int v)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            try
            {
                if (this.statusStrip1.InvokeRequired)
                {
                    SetValueCallback d = new SetValueCallback(SetValue);
                    this.Invoke(d, new object[] { v });
                }
                else
                {
                    this.toolStripProgressBar1.Value = v;
                }
            }
            catch { }
        }

        // ------------------------------------------------------------
        // This delegate enables asynchronous calls for setting
        // the Maximum.Value property on a toolStripProgressBar control.
        delegate void SetMaxValueCallback(int v);
        private void SetMaxValue(int v)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            try
            {
                if (this.statusStrip1.InvokeRequired)
                {
                    SetMaxValueCallback d = new SetMaxValueCallback(SetMaxValue);
                    this.Invoke(d, new object[] { v });
                }
                else
                {
                    this.toolStripProgressBar1.Maximum = v;
                }
            }
            catch { }
        }

        // ------------------------------------------------------------
        // This delegate enables asynchronous calls for setting
        // the Value property on a toolStripProgressBar control.
        private void SetGenerationText(string v)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            try
            {
                UIInvoke(this.lblGeneration, delegate()
                {
                    this.lblGeneration.Text = v;
                });
            }
            catch { }
        }
        // ------------------------------------------------------------
        private void SetLenghtText(string v)
        {
            try
            {
                try
                {
                    UIInvoke(this.lblLenght, delegate()
                    {
                        this.lblLenght.Text = v;
                    });
                }
                catch { }
            }
            catch { }
        }
        // ------------------------------------------------------------
        delegate void AddShapeCallback(LineShape l);
        private void AddLineShape(LineShape l)
        {
            try
            {
                if (shapeContainer_allCityShape.InvokeRequired)
                {
                    AddShapeCallback d = new AddShapeCallback(AddLineShape);
                    this.Invoke(d, new object[] { l });
                }
                else
                {
                    shapeContainer_allCityShape.Shapes.Add(l);
                }
            }
            catch { }
        }
        // ------------------------------------------------------------
        delegate void RemoveShapeCallback(LineShape l);
        private void RemoveLineShape(LineShape l)
        {
            try
            {
                if (shapeContainer_allCityShape.InvokeRequired)
                {
                    RemoveShapeCallback d = new RemoveShapeCallback(RemoveLineShape);
                    this.Invoke(d, new object[] { l });
                }
                else
                {
                    shapeContainer_allCityShape.Shapes.Remove(l);
                }
            }
            catch { }
        }
        // ------------------------------------------------------------
        delegate void SetPointCallback(int l, Point p0, Point p1);
        private void SetPoint(int l, Point p0, Point p1)
        {
            try
            {
                lineShape_Way[l].X1 = p0.X + 10;
                lineShape_Way[l].X2 = p1.X + 10;
                lineShape_Way[l].Y1 = p0.Y + 10;
                lineShape_Way[l].Y2 = p1.Y + 10;
            }
            catch { }
        }
        // ------------------------------------------------------------
        private void setNumPopEnable(bool En)
        {
            try
            {
                try
                {
                    UIInvoke(this.numPopulation, delegate()
                    {
                        this.numPopulation.Enabled = En;
                    });
                }
                catch { }
            }
            catch { }
        }
        #endregion

        /// <summary>
        /// Genetic Algorithm for TSP
        /// </summary>
        public void GA()
        {
            //
            // set cities position
            setCitiesPosition(ovalShape_City);

            //
            // initialize Parallel Computing for GA
            countCPUCore = calcCountOfCPU(); // Calculate number of active core or CPU for this app
            tokenSource = new CancellationTokenSource();

            //
            // set Start TickTime
            StartedTick = Environment.TickCount;

            if (pGAToolStripMenuItem.Checked)  // clear Parallel points
            {
                _pPlistTfg[1].Clear();
                _pPlistGfg[1].Clear();
                _pPlistTgg[1].Clear();
            }
            else // clear Series points
            {
                _pPlistTfg[0].Clear(); 
                _pPlistGfg[0].Clear();
                _pPlistTgg[0].Clear();
            }
            //
            Random rand = new Random();

            double EliteFitness = double.MaxValue;

            #region Population
            // create first population by Npop = 500;
            Population(rand); // initialize population
            #endregion

            #region Evaluate Fitness
            for (int i = 0; i < Npop; i++)
                pop[i].Calculate_Fitness();

            #endregion

            int count = 0;
            SetValue(0);
            //toolStripProgressBar1.Value = 0;
            //
            SetGenerationText("0000");
            //lblGeneration.Text = "0000";
            //
            if (counter_City <= 5)
                SetMaxValue(100);
            //toolStripProgressBar1.Maximum = 100;
            //
            else if (counter_City <= 15)
                SetMaxValue(1000);
            //toolStripProgressBar1.Maximum = 1000;
            //
            else if (counter_City <= 30)
                SetMaxValue(10000);
            //toolStripProgressBar1.Maximum = 10000;
            //
            else if (counter_City <= 40)
                SetMaxValue(51000);
            //toolStripProgressBar1.Maximum = 51000;
            //
            else if (counter_City <= 60)
                SetMaxValue(100000);
            //toolStripProgressBar1.Maximum = 100000;
            //
            else
                SetMaxValue(1000000);
            //toolStripProgressBar1.Maximum = 1000000;
            //
            //

            do
            {
                #region Selection
                #region Bubble Sort all chromosome by fitness
                // 
                for (int i = Npop - 1; i > 0; i--)
                    for (int j = 1; j <= i; j++)
                        if (pop[j - 1].Fitness > pop[j].Fitness)
                        {
                            Chromosome ch = pop[j - 1];
                            pop[j - 1] = pop[j];
                            pop[j] = ch;
                        }
                //
                #endregion

                #region Elitism
                if (EliteFitness > pop[0].Fitness)
                {
                    EliteFitness = pop[0].Fitness;
                    setTimeGraph(EliteFitness, count, true);

                    if (dynamicalGraphicToolStripMenuItem.Checked) // Design if Graphically is ON
                    {
                        refreshTour();
                    }
                    //
                    //-----------------------------------------------------------------------------
                    SetLenghtText(pop[0].Fitness.ToString());
                    //
                }
                //else setTimeGraph(EliteFitness, count, false); // just refresh Generation Graph's

                #endregion
                x_Rate(); // Selection any worst chromosome for clear and ...
                #endregion

                #region Reproduction
                // Definition Probability According by chromosome fitness 
                // create Pn[N_keep];
                Rank_Trim();

                if (pGAToolStripMenuItem.Checked) // Parallel Genetic Algorithm
                {
                    if (threadParallelismToolStripMenuItem.Checked) // PGA by MultiThreading
                    {
                        ReproduceByParallelThreads();
                    }
                    else if (taskParallelismToolStripMenuItem.Checked) // PGA by Task Parallelism
                    {
                        ReproduceByParallelTask();
                    }
                    else if (parallelForToolStripMenuItem.Checked) // PGA by Parallel.For ...
                    {
                        PReproduction(rand);
                    }
                }
                else // Series Genetic Algorithm
                {
                    #region Series Reproduct Code
                    Reproduction(rand);
                    #endregion
                }
                #endregion

                count++;
                SetGenerationText(count.ToString());
                //lblGeneration.Text = count.ToString();
                //
                SetValue(toolStripProgressBar1.Value + 1);
                //toolStripProgressBar1.Value++;
                //
            }
            while (count < toolStripProgressBar1.Maximum && Isotropy_Evaluatuon());

            //
            //toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
            SetValue(toolStripProgressBar1.Maximum);
            //
            // UnLock numUpDownPop
            setNumPopEnable(true);
            //
            // The END
            Stop();
        }

        #region Generation Tools
        private void Population(Random rand)
        {
            // create first population by Npop = 500;
            pop = new Chromosome[Npop];
            int[] RandNum = new int[counter_City];
            int[] RandNumber = new int[counter_City];
            int buffer = counter_City - 1;
            for (int l = 0; l < counter_City; l++)
                RandNumber[l] = l;
            for (int i = 0; i < Npop; i++)
            {
                RandNum = RandNumber;
                buffer = counter_City - 1;
                pop[i] = new Chromosome(counter_City);
                int b;
                int buffer2;
                for (int j = 0; j < counter_City; j++)
                {
                    b = rand.Next(0, buffer);
                    pop[i].Tour[j] = RandNum[b];
                    buffer2 = RandNum[buffer];
                    RandNum[buffer] = RandNum[b];
                    RandNum[b] = buffer2;
                    buffer--;
                }
            }
        }

        //find percent of All chromosome rate for delete Amiss(xRate) or Useful(Nkeep) chromosome
        //x_Rate According by chromosome fitness Average 
        private void x_Rate()
        {
            // calculate Addition of all fitness
            double sumFitness = 0;
            for (int i = 0; i < Npop; i++)
                sumFitness += pop[i].Fitness;
            // calculate Average of All chromosome fitness 
            double aveFitness = sumFitness / Npop; //Average of all chromosome fitness
            N_keep = 0; // N_keep start at 0 till Average fitness chromosome
            for (int i = 0; i < Npop; i++)
                if (aveFitness >= pop[i].Fitness)
                {
                    N_keep++; // counter as 0 ~ fitness Average + 1
                }
            if (N_keep <= 0) N_keep = 2;
        }

        // Definition Probability According by chromosome fitness 
        private void Rank_Trim()
        {
            // First Reserve Possibility Number for every Remnant chromosome 
            // chromosome Possibility Function is:
            // (1 + N_keep - No.chromosome) / ( ∑ No.chromosome) 
            // Where as at this program No.chromosome Of Array begin as Number 0
            // There for No.chromosome in Formula = No.chromosome + 1
            // then function is: if (n == N_keep)
            // Possibility[No.chromosome] = (n - No.chromosome) / (n(n+1) / 2)
            //
            Pn = new double[N_keep]; // Create chromosome possibility Array Cell as N_keep
            double Sum = ((N_keep * (N_keep + 1)) / 2); // (∑ No.chromosome) == (n(n+1) / 2)
            Pn[0] = N_keep / Sum; // Father (Best - Elite) chromosome Possibility
            for (int i = 1; i < N_keep; i++)
            {
                // Example: if ( Pn[Elite] = 0.4  &  Pn[Elite +1] = 0.2  &  Pn[Elite +2]  = 0.1 )
                // Then Own:          0 <= R <= 0.4 ===> Select chromosome[Elite]
                //                  0.4 <  R <= 0.6 ===> Select chromosome[Elite +1] 
                //                  0.6 <  R <= 0.7 ===> Select chromosome[Elite +2]
                // etc ... 
                Pn[i] = ((N_keep - i) / Sum) + Pn[i - 1];
            }
        }

        // Return Father and Mather chromosome with Probability of chromosome fitness
        private Chromosome Rank(Random rand)
        {
            double R = rand.NextDouble();
            for (int i = 0; i < N_keep; i++)
            {
                // Example: if ( Pn[Elite] = 0.6  &  Pn[Elite+1] = 0.3  &  Pn[Elite+2]  = 0.1 )
                // Then Own:          0 <= R <= 0.6  ===> Select chromosome[Elite]
                //                  0.6 <  R <= 0.9  ===> Select chromosome[Elite +1] 
                //                  0.9 <  R <= 1    ===> Select chromosome[Elite +2]
                // 
                if (R <= Pn[i]) return pop[i];
            }
            return pop[0]; // if don't run Modality of 'for' then return Elite chromosome 
        }

        // Check the isotropy All REMNANT chromosome (N_keep)
        public bool Isotropy_Evaluatuon()
        {
            // Isotropy percent is 50% of All chromosome Fitness
            int per_Iso = Convert.ToInt32(Math.Truncate(Convert.ToDouble(50 * N_keep / 100)));
            int counter_Isotropy = 0;
            double BestFitness = pop[0].Fitness;
            //
            // i start at 1 because DNA_Array[0] is self BestFitness
            for (int i = 1; i < N_keep; i++)
                if (BestFitness >= pop[i].Fitness) counter_Isotropy++;

            // G.A Algorithm did isotropy and app Stopped
            if (counter_Isotropy >= per_Iso) return false;
            else return true; // G.A Algorithm didn't isotropy and app will continued
        }

        private void ReproduceByParallelThreads()
        {
            #region Parallel Reproduct Code
            Thread[] th = new Thread[countCPUCore];

            // Create a semaphore that can satisfy up to three
            // concurrent requests. Use an initial count of zero,
            // so that the entire semaphore count is initially
            // owned by the main program thread.
            //
            Semaphore sem = new Semaphore(countCPUCore, countCPUCore);
            bool[] isAlive = new bool[countCPUCore];
            bool[] isCompleted = new bool[countCPUCore];

            int length = (Npop - N_keep) / countCPUCore;
            int divideReminder = (Npop - N_keep) % countCPUCore;

            for (int proc = 0; proc < th.Length; proc++)
            {
                ThreadToken tt = new ThreadToken(proc,
                    length + ((proc == countCPUCore - 1) ? divideReminder : 0),
                    N_keep + (proc * length));

                th[proc] = new Thread(new ParameterizedThreadStart((x) =>
                {
                    // Entered
                    sem.WaitOne();
                    isAlive[((ThreadToken)x).No] = true;

                    // work ...
                    PReproduction(((ThreadToken)x).startIndex, ((ThreadToken)x).length, ((ThreadToken)x).rand);

                    // We have finished our job, so release the semaphore
                    isCompleted[((ThreadToken)x).No] = true;
                    sem.Release();
                }));
                setThreadPriority(th[proc]);
                th[proc].Start(tt);
            }

        startloop:
            foreach (bool alive in isAlive) // wait parent starter for start all children.
                if (!alive)
                    goto startloop;

        endLoop:
            sem.WaitOne();
            foreach (bool complete in isCompleted) // wait parent to interrupt for finishes all of children jobs.
                if (!complete)
                    goto endLoop;

            // Continue Parent Work
            sem.Close();
            #endregion
        }
        private void ReproduceByParallelTask()
        {
            #region Parallel Reproduct Code
            Task[] tasks = new Task[countCPUCore];

            int length = (Npop - N_keep) / countCPUCore;
            int divideReminder = (Npop - N_keep) % countCPUCore;

            for (int proc = 0; proc < tasks.Length; proc++)
            {
                ThreadToken tt = new ThreadToken(proc,
                    length + ((proc == countCPUCore - 1) ? divideReminder : 0),
                    N_keep + (proc * length));

                tasks[proc] = Task.Factory.StartNew(x =>
                {
                    // work ...
                    PReproduction(((ThreadToken)x).startIndex, ((ThreadToken)x).length, ((ThreadToken)x).rand);

                }, tt, tokenSource.Token);// TaskCreationOptions.AttachedToParent);
            }

            // When user code that is running in a task creates a task with the AttachedToParent option, 
            // the new task is known as a child task of the originating task, 
            // which is known as the parent task. 
            // You can use the AttachedToParent option to express structured task parallelism,
            // because the parent task implicitly waits for all child tasks to complete. 
            // The following example shows a task that creates one child task:
            Task.WaitAll(tasks);

            // or

            //Block until all tasks complete.
            //Parent.Wait(); // when all task are into a parent task
            #endregion
        }
        /// <summary>
        /// Series Create New chromosome with Father & Mather Chromosome Instead of deleted chromosomes
        /// </summary>
        /// <param name="rand"></param>
        public void Reproduction(Random rand) // Series 
        {
            for (int i = N_keep; i < Npop; i++)
            {
                //
                // for send and check Father & Mather chromosome
                Chromosome Rank_Father, Rank_Mather, child;

                // have a problem (maybe Rank_1() == Rank_2()) then Father == Mather
                // Solve Problem by Loop checker
                do
                {
                    Rank_Father = Rank(rand);
                    Rank_Mather = Rank(rand);
                }
                while (Rank_Father == Rank_Mather);
                //
                // Crossover
                child = Rank_Father.crossover(Rank_Mather, rand);
                //
                //  run Mutation
                //
                child.mutation(rand);
                //
                // calculate children chromosome fitness
                //
                child.Calculate_Fitness();

                Interlocked.Exchange(ref pop[i], child); // atomic operation between multiple Thread shared
            }
        }
        /// <summary>
        /// Parallel Create New chromosome with Father & Mather Chromosome Instead of deleted chromosomes
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        public void PReproduction(int startIndex, int length, Random rand) // Parallel 
        {
            for (int i = startIndex; i < (startIndex + length) && i < Npop; i++)
            {
                //
                // for send and check Father & Mather chromosome
                Chromosome Rank_Father, Rank_Mather, child;

                // have a problem (maybe Rank_1() == Rank_2()) then Father == Mather
                // Solve Problem by Loop checker
                do
                {
                    Rank_Father = Rank(rand);
                    Rank_Mather = Rank(rand);
                }
                while (Rank_Father == Rank_Mather);
                //
                // Crossover
                child = Rank_Father.crossover(Rank_Mather, rand);
                //
                //  run Mutation
                //
                child.mutation(rand);
                //
                // calculate children chromosome fitness
                //
                child.Calculate_Fitness();

                Interlocked.Exchange(ref pop[i], child); // atomic operation between multiple Thread shared
            }
        }
        /// <summary>
        /// Parallel Create New chromosome with Father & Mather Chromosome Instead of deleted chromosomes
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        public void PReproduction(Random rand) // Parallel.For 
        {
            Parallel.For(N_keep, Npop,
                        new ParallelOptions() { MaxDegreeOfParallelism = countCPUCore, CancellationToken = tokenSource.Token },
                        (i, loopState) =>
                        {
                            // have a problem (maybe Rank_1() == Rank_2()) then Father == Mather
                            // Solve Problem by Loop checker
                            Chromosome Rank_Father, Rank_Mather, child;
                            do
                            {
                                Monitor.Enter(rand);
                                Rank_Father = Rank(rand);
                                Rank_Mather = Rank(rand);
                                Monitor.Exit(rand);
                            }
                            while (Rank_Father == Rank_Mather);
                            //
                            // Crossover
                            child = Rank_Father.crossover(Rank_Mather, rand);
                            //
                            //  run Mutation
                            //
                            child.mutation(rand);
                            //
                            // calculate children chromosome fitness
                            //
                            child.Calculate_Fitness();

                            Interlocked.Exchange(ref pop[i], child); // atomic operation between multiple Thread shared

                            if (tokenSource.IsCancellationRequested || tokenSource.Token.IsCancellationRequested)
                            {
                                loopState.Stop();
                                loopState.Break();
                                return;
                            }
                        });
        }

        #endregion

        private void setCitiesPosition(List<OvalShape> ovalShape_City)
        {
            Chromosome.citiesPosition.Clear();
            foreach (var city in ovalShape_City)
                Chromosome.citiesPosition.Add(city.Location);
        }

        private void Stop()
        {
            if (RunTime != null)
            {
                if (RunTime.IsAlive)
                {
                    setNumPopEnable(true); // Enable population numUpDown
                    UIInvoke(btnStartStop, delegate()
                    {
                        btnStartStop.Checked = false;
                    });
                    UIInvoke(btnPauseResume, delegate()
                    {
                        btnPauseResume.Checked = false;
                    });
                    try
                    {
                        if (pGAToolStripMenuItem.Checked)
                        {
                            tokenSource.Cancel();
                        }
                        RunTime.Abort();
                    }
                    catch { }
                    refreshTour();
                }
            }
        }

        private int calcCountOfCPU()
        {
            int numCore = 0;

            #region Find number of Active CPU or CPU core's for this Programs

            long Affinity_Dec = Process.GetCurrentProcess().ProcessorAffinity.ToInt64();
            string Affinity_Bin = Convert.ToString(Affinity_Dec, 2); // toBase 2
            foreach (char anyOne in Affinity_Bin.ToCharArray())
                if (anyOne == '1') numCore++;

            #endregion

            //if (numCore > 2) return --numCore;
            return numCore;
        }

        private void setThreadPriority(Thread th)
        {
            if (th != null)
            {
                if (th.ThreadState != ThreadState.Aborted &&
                   th.ThreadState != ThreadState.AbortRequested &&
                   th.ThreadState != ThreadState.Stopped &&
                   th.ThreadState != ThreadState.StopRequested)
                {
                    switch (Process.GetCurrentProcess().PriorityClass)
                    {
                        case ProcessPriorityClass.AboveNormal: th.Priority = ThreadPriority.AboveNormal;
                            break;
                        case ProcessPriorityClass.BelowNormal: th.Priority = ThreadPriority.BelowNormal;
                            break;
                        case ProcessPriorityClass.High: th.Priority = ThreadPriority.Highest;
                            break;
                        case ProcessPriorityClass.Idle: th.Priority = ThreadPriority.Lowest;
                            break;
                        case ProcessPriorityClass.Normal: th.Priority = ThreadPriority.Normal;
                            break;
                        case ProcessPriorityClass.RealTime: th.Priority = ThreadPriority.Highest;
                            break;
                    }
                    //
                    // Set Thread Affinity 
                    //
                    Thread.BeginThreadAffinity();
                }
            }
        }

        private void setTimeGraph(double EliteFitness, long Generation, bool FitnessRefreshed)
        {
            int timeLenght = (Environment.TickCount - StartedTick) / 10; // Convert to MiliSecond
            if (pGAToolStripMenuItem.Checked)
            {
                if (FitnessRefreshed)
                {
                    _pPlistTfg[1].Add(timeLenght, EliteFitness);
                    _pPlistGfg[1].Add(Generation, EliteFitness);
                }
                _pPlistTgg[1].Add(timeLenght, Generation);
            }
            else
            {
                if (FitnessRefreshed)
                {
                    _pPlistTfg[0].Add(timeLenght, EliteFitness);
                    _pPlistGfg[0].Add(Generation, EliteFitness);
                }
                _pPlistTgg[0].Add(timeLenght, Generation);
            }
        }

        private void refreshDGV_CityPositions()
        {
            dgvCity.Rows.Clear();

            for (int count = 0; count < ovalShape_City.Count; count++)
            {
                dgvCity.Rows.Add(new object[] { count + 1, ovalShape_City[count].Location.ToString() });
            }
        }

        private void create_City(Point e)
        {
            counter_City++;
            toolStripStatuslblNumCity.Text = counter_City.ToString();
            OvalShape newCity = new OvalShape();
            // 
            // newCity
            // 
            newCity.BackColor = Color.Red;
            newCity.BackStyle = BackStyle.Opaque;
            newCity.BorderColor = Color.Red;
            newCity.Cursor = Cursors.Hand;
            newCity.Location = new Point(e.X, e.Y);
            newCity.Size = new Size(20, 20);
            newCity.Click += new EventHandler(this.ovalShape_Click);

            ovalShape_City.Add(newCity);
            shapeContainer_allCityShape.Shapes.Add(newCity);
        }

        private void refreshTour()
        {
            try
            {
                Point Point1, Point0;
                for (int c = 1; c <= counter_City; c++)
                    try
                    {
                        //this.shapeContainer_allCityShape.Shapes.Remove(lineShape_Way[c]);
                        RemoveLineShape(lineShape_Way[c]);
                        //
                    }
                    catch { break; }

                for (int c = 1; c < counter_City; c++)
                {
                    // pop[0] is Elite chromosome or best less Distance -----------------------
                    Point1 = ovalShape_City[pop[0].Tour[c]].Location;
                    Point0 = ovalShape_City[pop[0].Tour[c - 1]].Location;

                    try
                    {
                        SetPointCallback d = new SetPointCallback(SetPoint);
                        this.BeginInvoke(d, new object[] { c, Point0, Point1 });
                    }
                    catch { }

                    //this.shapeContainer_allCityShape.Shapes.Add(lineShape_Way[c]);
                    AddLineShape(lineShape_Way[c]);
                    //
                }
                // design line between city 0 & last city
                // pop[0] is Elite chromosome or best less Distance
                Point1 = ovalShape_City[pop[0].Tour[counter_City - 1]].Location;
                Point0 = ovalShape_City[pop[0].Tour[0]].Location;

                try
                {
                    SetPointCallback d2 = new SetPointCallback(SetPoint);
                    this.BeginInvoke(d2, new object[] { 0, Point0, Point1 });
                }
                catch { }

                //this.shapeContainer_allCityShape.Shapes.Add(lineShape_Way[0]);
                AddLineShape(lineShape_Way[0]);
            }
            catch { }
        }

        private void ovalShape_Click(object sender, EventArgs e)
        {
            counter_City--;
            ovalShape_City.Remove((OvalShape)sender);
            shapeContainer_allCityShape.Shapes.Remove(((OvalShape)sender)); // Remove Selected Shape
            // Minus 1 as City Number's
            toolStripStatuslblNumCity.Text = counter_City.ToString();
            //
            // Refresh City Positions List
            refreshDGV_CityPositions();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatuslblLocate.Text = "X = " + e.X.ToString() + " ,  Y = " + e.Y.ToString();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            Point mPosition = new Point(e.X - 10, e.Y - 10);
            if (mPosition.X > 1 && mPosition.X < this.Width - 300 && mPosition.Y > 65 && mPosition.Y < this.Height - 85)
            {
                Stop();
                foreach (LineShape anyLine in lineShape_Way)
                    this.shapeContainer_allCityShape.Shapes.Remove(anyLine);
                lineShape_Way.Clear();
                create_City(mPosition);
                //
                // Refresh City Positions List
                refreshDGV_CityPositions();
            }
        }

        private void numPopulation_ValueChanged(object sender, EventArgs e)
        {
            Npop = (int)numPopulation.Value;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
            //
            // Remove Old City and road
            //
            foreach (var city in ovalShape_City)
                shapeContainer_allCityShape.Shapes.Remove(city);
            foreach (LineShape anyLine in lineShape_Way)
                this.shapeContainer_allCityShape.Shapes.Remove(anyLine);
            ovalShape_City.Clear();
            counter_City = 0;
            //
            // Refresh City Position List
            //
            refreshDGV_CityPositions();
            //
            // Clear All Label 
            //
            toolStripProgressBar1.ProgressBar.Value = 0;
            lblGeneration.Text = "0000";
            lblLenght.Text = "0000";
            toolStripStatuslblNumCity.Text = "0";
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open City Positions";
            ofd.RestoreDirectory = true;
            ofd.Filter = "Text files|*.txt";
            ofd.DefaultExt = "CityPositions.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    RunTime.Abort();
                }
                catch { }

                //
                // Remove Old City and road
                //
                foreach (var city in ovalShape_City)
                    shapeContainer_allCityShape.Shapes.Remove(city);
                foreach (LineShape anyLine in lineShape_Way)
                    this.shapeContainer_allCityShape.Shapes.Remove(anyLine);
                ovalShape_City.Clear();
                counter_City = 0;
                //
                // Create New City
                //
                string[] cityPositions = File.ReadAllLines(ofd.FileName);
                foreach (string cityP in cityPositions)
                {
                    int startIndexX = cityP.IndexOf("{X=", StringComparison.CurrentCultureIgnoreCase) + 3;
                    int endIndexX = cityP.IndexOf(",", StringComparison.CurrentCultureIgnoreCase);
                    int X = int.Parse(cityP.Substring(startIndexX, endIndexX - startIndexX));

                    int startIndexY = cityP.IndexOf(",Y=", StringComparison.CurrentCultureIgnoreCase) + 3;
                    int endIndexY = cityP.IndexOf("}", StringComparison.CurrentCultureIgnoreCase);
                    int Y = int.Parse(cityP.Substring(startIndexY, endIndexY - startIndexY));
                    create_City(new Point(X, Y));
                }
                //
                // Refresh City Position List
                //
                refreshDGV_CityPositions();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.Title = "Save City Positions";
            sfd.Filter = "Text files|*.txt";
            sfd.DefaultExt = "CityPositions.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                List<string> postions = new List<string>();
                foreach (var city in ovalShape_City)
                {
                    postions.Add(city.Location.ToString());
                }
                File.WriteAllLines(sfd.FileName, postions.ToArray());
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Stop();
            for (int th = 0; th < Process.GetCurrentProcess().Threads.Count; th++)
                Process.GetCurrentProcess().Threads[th].Dispose();
            Application.Exit();
        }

        private void dynamicalGraphicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dynamicalGraphicToolStripMenuItem.Checked = !dynamicalGraphicToolStripMenuItem.Checked;

            if (dynamicalGraphicToolStripMenuItem.Checked) refreshTour();

            toolsToolStripMenuItem.ShowDropDown();
        }

        private void timerFitnessGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.timerFitnessGraphToolStripMenuItem.Checked)
            {
                TFG.Dispose();
                timerFitnessGraphToolStripMenuItem.Checked = false;
            }
            else if (_pPlistTfg != null)
            {
                TFG = new TimeFitnessGraph();
                TFG.timerGraphToolStripMenuItem = this.timerFitnessGraphToolStripMenuItem;
                TFG.PPlist = this._pPlistTfg;
                TFG.Show();
            }
            toolsToolStripMenuItem.ShowDropDown();
        }

        private void generationFitnessGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.generationFitnessGraphToolStripMenuItem.Checked)
            {
                GFG.Dispose();
                generationFitnessGraphToolStripMenuItem.Checked = false;
            }
            else if (_pPlistGfg != null)
            {
                GFG = new GenerationFitnessGraph();
                GFG.timerGraphToolStripMenuItem = this.generationFitnessGraphToolStripMenuItem;
                GFG.PPlist = this._pPlistGfg;
                GFG.Show();
            }
            toolsToolStripMenuItem.ShowDropDown();
        }

        private void timerGenerationGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.timerGenerationGraphToolStripMenuItem.Checked)
            {
                TGG.Dispose();
                timerGenerationGraphToolStripMenuItem.Checked = false;
            }
            else if (_pPlistTgg != null)
            {
                TGG = new TimeGenerationGraph();
                TGG.timerGraphToolStripMenuItem = this.timerGenerationGraphToolStripMenuItem;
                TGG.PPlist = this._pPlistTgg;
                TGG.Show();
            }
            toolsToolStripMenuItem.ShowDropDown();
        }

        private void newRandomCitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnterNumberRandomPointForm enrpForm = new EnterNumberRandomPointForm();
            if (enrpForm.ShowDialog() == DialogResult.OK)
            {
                //
                // Clear old data
                //
                newToolStripMenuItem_Click(sender, e);
                //
                // Create Random Points between Parent Form Size
                // Min X=1 , Y=84
                // Max X=FormSize.X-300 , Y=FormSize.Y-85
                // ...
                Random rand = new Random();
                for (int citiesNo = 0; citiesNo < enrpForm.NumberOfCities; citiesNo++)
                {
                    //
                    // find new safely points according by contact rate:
                    Point newPoint;
                    bool safely;
                    Point maxSafelyPoint = new Point(); // save best safely point if do not found any safely Points
                    double bestFitness = 0; /// save distance between maxSafelyPoint and newPoint
                    int maxSafelyLoopsNo = enrpForm.NumberSafety; // Probability for find new Safely points
                    do
                    {
                        newPoint = new Point(rand.Next(1, this.Width - 300), rand.Next(65, this.Height - 85));
                        safely = true;
                        foreach (var otherCity in ovalShape_City) // Check Safety!
                        {
                            if (Math.Abs(otherCity.Location.X - newPoint.X) < 24 &&
                                Math.Abs(otherCity.Location.Y - newPoint.Y) < 24)
                            {
                                safely = false;
                                double fitness = Math.Sqrt(Math.Pow((newPoint.X - otherCity.Location.X), 2) + Math.Pow((newPoint.Y - otherCity.Location.Y), 2));
                                if (fitness > bestFitness)
                                {
                                    bestFitness = fitness;
                                    maxSafelyPoint = newPoint;
                                }
                                break;
                            }
                        }
                        maxSafelyLoopsNo--;
                    } while (!safely && maxSafelyLoopsNo > 0);
                    if (!safely) newPoint = maxSafelyPoint;
                    //
                    create_City(newPoint);
                }
                //
                // Refresh City Position List
                //
                refreshDGV_CityPositions();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout about = new FormAbout();
            about.ShowDialog();
        }

        private void btnStartStop_CheckedChanged(object sender, EventArgs e)
        {
            if (btnStartStop.Checked)
            {
                btnPauseResume.Enabled = true;
                btnStartStop.Text = "Stop Process ×";
                #region Start
                if (ovalShape_City.Count <= 1)
                {
                    btnStartStop.Checked = false;
                    MessageBox.Show("Please Create some cities for a tour!", "Empty Tour Exception",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                //
                #region Graphical Works
                //
                // Disable Population numUpDown
                //
                numPopulation.Enabled = false;
                // 
                // lineShape_Way
                // 
                foreach (LineShape anyLine in lineShape_Way)
                    shapeContainer_allCityShape.Shapes.Remove(anyLine);
                lineShape_Way.Clear();

                for (int ctrl = 0; ctrl < shapeContainer_allCityShape.Shapes.Count; ctrl++)
                {
                    if (shapeContainer_allCityShape.Shapes.get_Item(ctrl).GetType() != typeof(OvalShape))
                    {
                        shapeContainer_allCityShape.Shapes.RemoveAt(ctrl);
                    }
                }
                shapeContainer_allCityShape.Refresh();

                for (int c = 0; c < ovalShape_City.Count; c++)
                {
                    LineShape newLine = new LineShape();
                    newLine.BorderColor = Color.Blue;
                    newLine.Cursor = Cursors.Default;
                    newLine.Enabled = false;
                    lineShape_Way.Add(newLine);
                }
                //
                //
                #endregion
                //
                // Solve();
                try
                {
                    if (!RunTime.IsAlive)
                    {
                        RunTime = new Thread(new ThreadStart(GA));
                        setThreadPriority(RunTime);
                        RunTime.Start();
                    }
                }
                catch
                {
                    RunTime = new Thread(new ThreadStart(GA));
                    setThreadPriority(RunTime);
                    RunTime.Start();
                }
                #endregion
            }
            else
            {
                if (btnPauseResume.Checked)
                {
                    btnPauseResume.Checked = false;
                }
                btnStartStop.Text = "&Start Process";
                Stop();
            }
        }

        private void btnPauseResume_CheckedChanged(object sender, EventArgs e)
        {
            if (btnPauseResume.Checked)
            {
                btnPauseResume.Text = "&Resume Process";
                try
                {
                    if (RunTime.IsAlive)
                        RunTime.Suspend();
                }
                catch { }
            }
            else
            {
                btnPauseResume.Text = "&Pause Process";
                try { if (RunTime.ThreadState == ThreadState.Suspended) RunTime.Resume(); }
                catch { }

                foreach (LineShape anyLine in lineShape_Way)
                    this.shapeContainer_allCityShape.Shapes.Remove(anyLine);
                foreach (LineShape anyLine in lineShape_Way)
                    this.shapeContainer_allCityShape.Shapes.Add(anyLine);
            }
        }

        private void RealtimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
                HighToolStripMenuItem.Checked = false;
                AboveNormalToolStripMenuItem.Checked = false;
                NormalToolStripMenuItem.Checked = false;
                BelowNormalToolStripMenuItem.Checked = false;
                LowToolStripMenuItem.Checked = false;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            else
            {
                ((ToolStripMenuItem)sender).Checked = true;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            setThreadPriority(RunTime);
        }

        private void HighToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                RealtimeToolStripMenuItem.Checked = false;
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
                AboveNormalToolStripMenuItem.Checked = false;
                NormalToolStripMenuItem.Checked = false;
                BelowNormalToolStripMenuItem.Checked = false;
                LowToolStripMenuItem.Checked = false;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            else
            {
                ((ToolStripMenuItem)sender).Checked = true;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            setThreadPriority(RunTime);
        }

        private void AboveNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                RealtimeToolStripMenuItem.Checked = false;
                HighToolStripMenuItem.Checked = false;
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;
                NormalToolStripMenuItem.Checked = false;
                BelowNormalToolStripMenuItem.Checked = false;
                LowToolStripMenuItem.Checked = false;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            else
            {
                ((ToolStripMenuItem)sender).Checked = true;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            setThreadPriority(RunTime);
        }

        private void NormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                RealtimeToolStripMenuItem.Checked = false;
                HighToolStripMenuItem.Checked = false;
                AboveNormalToolStripMenuItem.Checked = false;
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;
                BelowNormalToolStripMenuItem.Checked = false;
                LowToolStripMenuItem.Checked = false;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            else
            {
                ((ToolStripMenuItem)sender).Checked = true;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            setThreadPriority(RunTime);
        }

        private void BelowNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                RealtimeToolStripMenuItem.Checked = false;
                HighToolStripMenuItem.Checked = false;
                AboveNormalToolStripMenuItem.Checked = false;
                NormalToolStripMenuItem.Checked = false;
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
                LowToolStripMenuItem.Checked = false;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            else
            {
                ((ToolStripMenuItem)sender).Checked = true;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            setThreadPriority(RunTime);
        }

        private void LowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                RealtimeToolStripMenuItem.Checked = false;
                HighToolStripMenuItem.Checked = false;
                AboveNormalToolStripMenuItem.Checked = false;
                NormalToolStripMenuItem.Checked = false;
                BelowNormalToolStripMenuItem.Checked = false;
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            else
            {
                ((ToolStripMenuItem)sender).Checked = true;
                ProcessPriorityToolStripMenuItem.ShowDropDown();
                SetPriorityToolStripMenuItem.ShowDropDown();
            }
            setThreadPriority(RunTime);
        }

        private void SetAffinityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessorAffinityForm paf = new ProcessorAffinityForm();
            paf.ShowDialog();
            countCPUCore = calcCountOfCPU();
        }

        private void pGAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if checked then  Parallel Genetic Algorithm Enable
            pGAToolStripMenuItem.Checked = !pGAToolStripMenuItem.Checked;
            if (pGAToolStripMenuItem.Checked) taskParallelismToolStripMenuItem.Checked = true;
            else
            {
                taskParallelismToolStripMenuItem.Checked = false;
                threadParallelismToolStripMenuItem.Checked = false;
                parallelForToolStripMenuItem.Checked = false;
            }
            // show Panel
            ProcessPriorityToolStripMenuItem.ShowDropDown();
            pGAToolStripMenuItem.ShowDropDown();
        }

        private void taskParallelismToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // First change self check to unOlder self check's
            taskParallelismToolStripMenuItem.Checked = !taskParallelismToolStripMenuItem.Checked;

            // Then check PGA by self
            pGAToolStripMenuItem.Checked = taskParallelismToolStripMenuItem.Checked;

            // and check other by !self
            threadParallelismToolStripMenuItem.Checked = false;
            parallelForToolStripMenuItem.Checked = false;

            // show Panel
            ProcessPriorityToolStripMenuItem.ShowDropDown();
            pGAToolStripMenuItem.ShowDropDown();
        }

        private void threadParallelismToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // First change self check to unOlder self check's
            threadParallelismToolStripMenuItem.Checked = !threadParallelismToolStripMenuItem.Checked;

            // Then check PGA by self
            pGAToolStripMenuItem.Checked = threadParallelismToolStripMenuItem.Checked;

            // and check other by !self
            taskParallelismToolStripMenuItem.Checked = false;
            parallelForToolStripMenuItem.Checked = false;

            // show Panel
            ProcessPriorityToolStripMenuItem.ShowDropDown();
            pGAToolStripMenuItem.ShowDropDown();
        }

        private void parallelForToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // First change self check to unOlder self check's
            parallelForToolStripMenuItem.Checked = !parallelForToolStripMenuItem.Checked;

            // Then check PGA by self
            pGAToolStripMenuItem.Checked = parallelForToolStripMenuItem.Checked;

            // and check other by !self
            taskParallelismToolStripMenuItem.Checked = false;
            threadParallelismToolStripMenuItem.Checked = false;

            // show Panel
            ProcessPriorityToolStripMenuItem.ShowDropDown();
            pGAToolStripMenuItem.ShowDropDown();
        }

    }
    public struct ThreadToken
    {
        public ThreadToken(int Thread_No, int _length, int _startIndex)
        {
            No = Thread_No;
            length = _length;
            startIndex = _startIndex;
            rand = new Random();
        }
        public int No;
        public int length;
        public int startIndex;
        public Random rand;
    };
}