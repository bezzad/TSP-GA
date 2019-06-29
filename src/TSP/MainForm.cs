using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using TSP.Core;
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

        TimeFitnessGraph _tfg;
        TimeGenerationGraph _tgg;
        GenerationFitnessGraph _gfg;

        CancellationTokenSource _tokenSource;
        int _startedTick = 0;

        public int CountCpuCore { get; set; } = 1;

        // Number Population
        public int PopulationNumber { get; set; } = 500;

        // Number Keep Chromosome Size 
        int _nKeep = 0;

        // Double Array Pn for save Rank
        double[] _pn;

        private ShapeContainer ShapeContainerAllCityShape { get; set; }
        private List<LineShape> LineShapeWay { get; set; } = new List<LineShape>();
        public List<OvalShape> OvalShapeCity { get; set; } = new List<OvalShape>();

        // save number of all city
        public int CounterCity { get; private set; }
        public GeneticAlgorithm Genetic { get; private set; }

        // create new process or for end process
        Thread _runTime;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            // 
            // shapeContainer_allCityShape
            // 
            ShapeContainerAllCityShape = new ShapeContainer
            {
                Location = new Point(0, 0),
                Margin = new Padding(0),
                Size = new Size(Width, Height),
                TabIndex = 0,
                TabStop = false
            };

            Controls.Add(ShapeContainerAllCityShape);
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
        public static void UiInvoke(Control uiControl, Action action)
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
                if (statusStrip1.InvokeRequired)
                {
                    var d = new SetValueCallback(SetValue);
                    Invoke(d, new object[] { v });
                }
                else
                {
                    toolStripProgressBar1.Value = v;
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
                if (statusStrip1.InvokeRequired)
                {
                    var d = new SetMaxValueCallback(SetMaxValue);
                    Invoke(d, new object[] { v });
                }
                else
                {
                    toolStripProgressBar1.Maximum = v;
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
                UiInvoke(lblGeneration, delegate ()
                {
                    lblGeneration.Text = v;
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
                    UiInvoke(lblLenght, delegate ()
                    {
                        lblLenght.Text = v;
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
                if (ShapeContainerAllCityShape.InvokeRequired)
                {
                    var d = new AddShapeCallback(AddLineShape);
                    Invoke(d, new object[] { l });
                }
                else
                {
                    ShapeContainerAllCityShape.Shapes.Add(l);
                }
            }
            catch
            {
                // ignored
            }
        }
        // ------------------------------------------------------------
        delegate void RemoveShapeCallback(LineShape l);
        private void RemoveLineShape(LineShape l)
        {
            try
            {
                if (ShapeContainerAllCityShape.InvokeRequired)
                {
                    var d = new RemoveShapeCallback(RemoveLineShape);
                    Invoke(d, new object[] { l });
                }
                else
                {
                    ShapeContainerAllCityShape.Shapes.Remove(l);
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
                LineShapeWay[l].X1 = p0.X + 10;
                LineShapeWay[l].X2 = p1.X + 10;
                LineShapeWay[l].Y1 = p0.Y + 10;
                LineShapeWay[l].Y2 = p1.Y + 10;
            }
            catch { }
        }
        // ------------------------------------------------------------
        private void SetNumPopEnable(bool en)
        {
            try
            {
                try
                {
                    UiInvoke(numPopulation, delegate ()
                    {
                        numPopulation.Enabled = en;
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
        public void Ga()
        {
            var rand = new System.Random();
            var eliteFitness = double.MaxValue;
            //
            // set cities position
            SetCitiesPosition(OvalShapeCity);
            //
            // initialize Parallel Computing for GA
            CountCpuCore = CalcCountOfCpu(); // Calculate number of active core or CPU for this app
            _tokenSource = new CancellationTokenSource();
            //
            // set Start TickTime
            _startedTick = Environment.TickCount;

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
            Genetic = new GeneticAlgorithm(CounterCity, PopulationNumber);
            
            var count = 0;
            SetValue(0);
            //toolStripProgressBar1.Value = 0;
            //
            SetGenerationText("0000");
            //lblGeneration.Text = "0000";
            //
            if (CounterCity <= 5)
                SetMaxValue(100);
            //toolStripProgressBar1.Maximum = 100;
            //
            else if (CounterCity <= 15)
                SetMaxValue(1000);
            //toolStripProgressBar1.Maximum = 1000;
            //
            else if (CounterCity <= 30)
                SetMaxValue(10000);
            //toolStripProgressBar1.Maximum = 10000;
            //
            else if (CounterCity <= 40)
                SetMaxValue(51000);
            //toolStripProgressBar1.Maximum = 51000;
            //
            else if (CounterCity <= 60)
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
                for (var i = PopulationNumber - 1; i > 0; i--)
                    for (var j = 1; j <= i; j++)
                        if (Genetic.Population[j - 1].Fitness > Genetic.Population[j].Fitness)
                        {
                            var ch = Genetic.Population[j - 1];
                            Genetic.Population[j - 1] = Genetic.Population[j];
                            Genetic.Population[j] = ch;
                        }
                //
                #endregion

                #region Elitism
                if (eliteFitness > Genetic.Population[0].Fitness)
                {
                    eliteFitness = Genetic.Population[0].Fitness;
                    SetTimeGraph(eliteFitness, count, true);

                    if (dynamicalGraphicToolStripMenuItem.Checked) // Design if Graphically is ON
                    {
                        RefreshTour();
                    }
                    //
                    //-----------------------------------------------------------------------------
                    SetLenghtText(Genetic.Population[0].Fitness.ToString());
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
            SetNumPopEnable(true);
            //
            // The END
            Stop();
        }

        #region Generation Tools

        //find percent of All chromosome rate for delete Amiss(xRate) or Useful(Nkeep) chromosome
        //x_Rate According by chromosome fitness Average 
        private void x_Rate()
        {
            // calculate Addition of all fitness
            double sumFitness = 0;
            for (var i = 0; i < PopulationNumber; i++)
                sumFitness += Genetic.Population[i].Fitness;
            // calculate Average of All chromosome fitness 
            var aveFitness = sumFitness / PopulationNumber; //Average of all chromosome fitness
            _nKeep = 0; // N_keep start at 0 till Average fitness chromosome
            for (var i = 0; i < PopulationNumber; i++)
                if (aveFitness >= Genetic.Population[i].Fitness)
                {
                    _nKeep++; // counter as 0 ~ fitness Average + 1
                }
            if (_nKeep <= 0) _nKeep = 2;
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
            _pn = new double[_nKeep]; // Create chromosome possibility Array Cell as N_keep
            double sum = ((_nKeep * (_nKeep + 1)) / 2); // (∑ No.chromosome) == (n(n+1) / 2)
            _pn[0] = _nKeep / sum; // Father (Best - Elite) chromosome Possibility
            for (var i = 1; i < _nKeep; i++)
            {
                // Example: if ( Pn[Elite] = 0.4  &  Pn[Elite +1] = 0.2  &  Pn[Elite +2]  = 0.1 )
                // Then Own:          0 <= R <= 0.4 ===> Select chromosome[Elite]
                //                  0.4 <  R <= 0.6 ===> Select chromosome[Elite +1] 
                //                  0.6 <  R <= 0.7 ===> Select chromosome[Elite +2]
                // etc ... 
                _pn[i] = ((_nKeep - i) / sum) + _pn[i - 1];
            }
        }

        // Return Father and Mather chromosome with Probability of chromosome fitness
        private Chromosome Rank(System.Random rand)
        {
            var r = rand.NextDouble();
            for (var i = 0; i < _nKeep; i++)
            {
                // Example: if ( Pn[Elite] = 0.6  &  Pn[Elite+1] = 0.3  &  Pn[Elite+2]  = 0.1 )
                // Then Own:          0 <= R <= 0.6  ===> Select chromosome[Elite]
                //                  0.6 <  R <= 0.9  ===> Select chromosome[Elite +1] 
                //                  0.9 <  R <= 1    ===> Select chromosome[Elite +2]
                // 
                if (r <= _pn[i]) return Genetic.Population[i];
            }
            return Genetic.Population[0]; // if don't run Modality of 'for' then return Elite chromosome 
        }

        // Check the isotropy All REMNANT chromosome (N_keep)
        public bool Isotropy_Evaluatuon()
        {
            // Isotropy percent is 50% of All chromosome Fitness
            var perIso = Convert.ToInt32(Math.Truncate(Convert.ToDouble(50 * _nKeep / 100)));
            var counterIsotropy = 0;
            var bestFitness = Genetic.Population[0].Fitness;
            //
            // i start at 1 because DNA_Array[0] is self BestFitness
            for (var i = 1; i < _nKeep; i++)
                if (bestFitness >= Genetic.Population[i].Fitness) counterIsotropy++;

            // G.A Algorithm did isotropy and app Stopped
            if (counterIsotropy >= perIso) return false;
            else return true; // G.A Algorithm didn't isotropy and app will continued
        }

        private void ReproduceByParallelThreads()
        {
            #region Parallel Reproduct Code
            var th = new Thread[CountCpuCore];

            // Create a semaphore that can satisfy up to three
            // concurrent requests. Use an initial count of zero,
            // so that the entire semaphore count is initially
            // owned by the main program thread.
            //
            var sem = new Semaphore(CountCpuCore, CountCpuCore);
            var isAlive = new bool[CountCpuCore];
            var isCompleted = new bool[CountCpuCore];

            var length = (PopulationNumber - _nKeep) / CountCpuCore;
            var divideReminder = (PopulationNumber - _nKeep) % CountCpuCore;

            for (var proc = 0; proc < th.Length; proc++)
            {
                var tt = new ThreadToken(proc,
                    length + ((proc == CountCpuCore - 1) ? divideReminder : 0),
                    _nKeep + (proc * length));

                th[proc] = new Thread((x) =>
                {
                    // Entered
                    sem.WaitOne();
                    isAlive[((ThreadToken)x).No] = true;

                    // work ...
                    PReproduction(((ThreadToken)x).StartIndex, ((ThreadToken)x).Length, ((ThreadToken)x).Rand);

                    // We have finished our job, so release the semaphore
                    isCompleted[((ThreadToken)x).No] = true;
                    sem.Release();
                });
                SetThreadPriority(th[proc]);
                th[proc].Start(tt);
            }

        startloop:
            foreach (var alive in isAlive) // wait parent starter for start all children.
                if (!alive)
                    goto startloop;

                endLoop:
            sem.WaitOne();
            foreach (var complete in isCompleted) // wait parent to interrupt for finishes all of children jobs.
                if (!complete)
                    goto endLoop;

            // Continue Parent Work
            sem.Close();
            #endregion
        }
        private void ReproduceByParallelTask()
        {
            #region Parallel Reproduct Code
            var tasks = new Task[CountCpuCore];

            var length = (PopulationNumber - _nKeep) / CountCpuCore;
            var divideReminder = (PopulationNumber - _nKeep) % CountCpuCore;

            for (var proc = 0; proc < tasks.Length; proc++)
            {
                var tt = new ThreadToken(proc,
                    length + ((proc == CountCpuCore - 1) ? divideReminder : 0),
                    _nKeep + (proc * length));

                tasks[proc] = Task.Factory.StartNew(x =>
                {
                    // work ...
                    PReproduction(((ThreadToken)x).StartIndex, ((ThreadToken)x).Length, ((ThreadToken)x).Rand);

                }, tt, _tokenSource.Token);// TaskCreationOptions.AttachedToParent);
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
        public void Reproduction(System.Random rand) // Series 
        {
            for (var i = _nKeep; i < PopulationNumber; i++)
            {
                //
                // for send and check Father & Mather chromosome
                Chromosome rankFather, rankMather, child;

                // have a problem (maybe Rank_1() == Rank_2()) then Father == Mather
                // Solve Problem by Loop checker
                do
                {
                    rankFather = Rank(rand);
                    rankMather = Rank(rand);
                }
                while (rankFather == rankMather);
                //
                // CrossoverHelper
                child = rankFather.Crossover(rankMather, rand);
                //
                //  run MutationHelper
                //
                child.Mutation(rand);
                //
                // calculate children chromosome fitness
                //
                child.Evaluate();

                Interlocked.Exchange(ref Genetic.Population[i], child); // atomic operation between multiple Thread shared
            }
        }
        /// <summary>
        /// Parallel Create New chromosome with Father & Mather Chromosome Instead of deleted chromosomes
        /// </summary>
        public void PReproduction(int startIndex, int length, System.Random rand) // Parallel 
        {
            for (var i = startIndex; i < (startIndex + length) && i < PopulationNumber; i++)
            {
                //
                // for send and check Father & Mather chromosome
                Chromosome rankFather, rankMather;

                // have a problem (maybe Rank_1() == Rank_2()) then Father == Mather
                // Solve Problem by Loop checker
                do
                {
                    rankFather = Rank(rand);
                    rankMather = Rank(rand);
                }
                while (rankFather == rankMather);
                //
                // CrossoverHelper
                var child = rankFather.Crossover(rankMather, rand);
                //
                //  run MutationHelper
                //
                child.Mutation(rand);
                //
                // calculate children chromosome fitness
                //
                child.Evaluate();

                Interlocked.Exchange(ref Genetic.Population[i], child); // atomic operation between multiple Thread shared
            }
        }

        /// <summary>
        /// Parallel Create New chromosome with Father & Mather Chromosome Instead of deleted chromosomes
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        public void PReproduction(System.Random rand) // Parallel.For 
        {
            Parallel.For(_nKeep, PopulationNumber,
                        new ParallelOptions() { MaxDegreeOfParallelism = CountCpuCore, CancellationToken = _tokenSource.Token },
                        (i, loopState) =>
                        {
                            // have a problem (maybe Rank_1() == Rank_2()) then Father == Mather
                            // Solve Problem by Loop checker
                            Chromosome rankFather, rankMather, child;
                            do
                            {
                                Monitor.Enter(rand);
                                rankFather = Rank(rand);
                                rankMather = Rank(rand);
                                Monitor.Exit(rand);
                            }
                            while (rankFather == rankMather);
                            //
                            // CrossoverHelper
                            child = rankFather.Crossover(rankMather, rand);
                            //
                            //  run MutationHelper
                            //
                            child.Mutation(rand);
                            //
                            // calculate children chromosome fitness
                            //
                            child.Evaluate();

                            Interlocked.Exchange(ref Genetic.Population[i], child); // atomic operation between multiple Thread shared

                            if (_tokenSource.IsCancellationRequested || _tokenSource.Token.IsCancellationRequested)
                            {
                                loopState.Stop();
                                loopState.Break();
                                return;
                            }
                        });
        }

        #endregion

        private void SetCitiesPosition(List<OvalShape> ovalShapeCity)
        {
            Chromosome.CitiesPosition.Clear();
            foreach (var city in ovalShapeCity)
                Chromosome.CitiesPosition.Add(city.Location);
        }

        private void Stop()
        {
            if (_runTime != null)
            {
                if (_runTime.IsAlive)
                {
                    SetNumPopEnable(true); // Enable population numUpDown
                    UiInvoke(btnStartStop, delegate ()
                    {
                        btnStartStop.Checked = false;
                    });
                    UiInvoke(btnPauseResume, delegate ()
                    {
                        btnPauseResume.Checked = false;
                    });
                    try
                    {
                        if (pGAToolStripMenuItem.Checked)
                        {
                            _tokenSource.Cancel();
                        }
                        _runTime.Abort();
                    }
                    catch { }
                    RefreshTour();
                }
            }
        }

        private int CalcCountOfCpu()
        {
            var numCore = 0;

            #region Find number of Active CPU or CPU core's for this Programs

            var affinityDec = Process.GetCurrentProcess().ProcessorAffinity.ToInt64();
            var affinityBin = Convert.ToString(affinityDec, 2); // toBase 2
            foreach (var anyOne in affinityBin.ToCharArray())
                if (anyOne == '1') numCore++;

            #endregion

            //if (numCore > 2) return --numCore;
            return numCore;
        }

        private void SetThreadPriority(Thread th)
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
                        case ProcessPriorityClass.AboveNormal:
                            th.Priority = ThreadPriority.AboveNormal;
                            break;
                        case ProcessPriorityClass.BelowNormal:
                            th.Priority = ThreadPriority.BelowNormal;
                            break;
                        case ProcessPriorityClass.High:
                            th.Priority = ThreadPriority.Highest;
                            break;
                        case ProcessPriorityClass.Idle:
                            th.Priority = ThreadPriority.Lowest;
                            break;
                        case ProcessPriorityClass.Normal:
                            th.Priority = ThreadPriority.Normal;
                            break;
                        case ProcessPriorityClass.RealTime:
                            th.Priority = ThreadPriority.Highest;
                            break;
                    }
                    //
                    // Set Thread Affinity 
                    //
                    Thread.BeginThreadAffinity();
                }
            }
        }

        private void SetTimeGraph(double eliteFitness, long generation, bool fitnessRefreshed)
        {
            var timeLenght = (Environment.TickCount - _startedTick) / 10; // Convert to MiliSecond
            if (pGAToolStripMenuItem.Checked)
            {
                if (fitnessRefreshed)
                {
                    _pPlistTfg[1].Add(timeLenght, eliteFitness);
                    _pPlistGfg[1].Add(generation, eliteFitness);
                }
                _pPlistTgg[1].Add(timeLenght, generation);
            }
            else
            {
                if (fitnessRefreshed)
                {
                    _pPlistTfg[0].Add(timeLenght, eliteFitness);
                    _pPlistGfg[0].Add(generation, eliteFitness);
                }
                _pPlistTgg[0].Add(timeLenght, generation);
            }
        }

        private void refreshDGV_CityPositions()
        {
            dgvCity.Rows.Clear();

            for (var count = 0; count < OvalShapeCity.Count; count++)
            {
                dgvCity.Rows.Add(new object[] { count + 1, OvalShapeCity[count].Location.ToString() });
            }
        }

        private void create_City(Point e)
        {
            CounterCity++;
            toolStripStatuslblNumCity.Text = CounterCity.ToString();
            var newCity = new OvalShape();
            // 
            // newCity
            // 
            newCity.BackColor = Color.Red;
            newCity.BackStyle = BackStyle.Opaque;
            newCity.BorderColor = Color.Red;
            newCity.Cursor = Cursors.Hand;
            newCity.Location = new Point(e.X, e.Y);
            newCity.Size = new Size(20, 20);
            newCity.Click += ovalShape_Click;

            OvalShapeCity.Add(newCity);
            ShapeContainerAllCityShape.Shapes.Add(newCity);
        }

        private void RefreshTour()
        {
            try
            {
                Point point1, point0;
                for (var c = 1; c <= CounterCity; c++)
                    try
                    {
                        //this.shapeContainer_allCityShape.Shapes.Remove(lineShape_Way[c]);
                        RemoveLineShape(LineShapeWay[c]);
                        //
                    }
                    catch { break; }

                for (var c = 1; c < CounterCity; c++)
                {
                    // pop[0] is Elite chromosome or best less Distance -----------------------
                    point1 = OvalShapeCity[Genetic.Population[0].Genome[c]].Location;
                    point0 = OvalShapeCity[Genetic.Population[0].Genome[c - 1]].Location;

                    try
                    {
                        var d = new SetPointCallback(SetPoint);
                        BeginInvoke(d, new object[] { c, point0, point1 });
                    }
                    catch { }

                    //this.shapeContainer_allCityShape.Shapes.Add(lineShape_Way[c]);
                    AddLineShape(LineShapeWay[c]);
                    //
                }
                // design line between city 0 & last city
                // pop[0] is Elite chromosome or best less Distance
                point1 = OvalShapeCity[Genetic.Population[0].Genome[CounterCity - 1]].Location;
                point0 = OvalShapeCity[Genetic.Population[0].Genome[0]].Location;

                try
                {
                    var d2 = new SetPointCallback(SetPoint);
                    BeginInvoke(d2, new object[] { 0, point0, point1 });
                }
                catch { }

                //this.shapeContainer_allCityShape.Shapes.Add(lineShape_Way[0]);
                AddLineShape(LineShapeWay[0]);
            }
            catch { }
        }

        private void ovalShape_Click(object sender, EventArgs e)
        {
            CounterCity--;
            OvalShapeCity.Remove((OvalShape)sender);
            ShapeContainerAllCityShape.Shapes.Remove(((OvalShape)sender)); // Remove Selected Shape
            // Minus 1 as City Number's
            toolStripStatuslblNumCity.Text = CounterCity.ToString();
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
            var mPosition = new Point(e.X - 10, e.Y - 10);
            if (mPosition.X > 1 && mPosition.X < Width - 300 && mPosition.Y > 65 && mPosition.Y < Height - 85)
            {
                Stop();
                foreach (var anyLine in LineShapeWay)
                    ShapeContainerAllCityShape.Shapes.Remove(anyLine);
                LineShapeWay.Clear();
                create_City(mPosition);
                //
                // Refresh City Positions List
                refreshDGV_CityPositions();
            }
        }

        private void numPopulation_ValueChanged(object sender, EventArgs e)
        {
            PopulationNumber = (int)numPopulation.Value;
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
            foreach (var city in OvalShapeCity)
                ShapeContainerAllCityShape.Shapes.Remove(city);
            foreach (var anyLine in LineShapeWay)
                ShapeContainerAllCityShape.Shapes.Remove(anyLine);
            OvalShapeCity.Clear();
            CounterCity = 0;
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
            var ofd = new OpenFileDialog();
            ofd.Title = "Open City Positions";
            ofd.RestoreDirectory = true;
            ofd.Filter = "Text files|*.txt";
            ofd.DefaultExt = "CityPositions.txt";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _runTime.Abort();
                }
                catch { }

                //
                // Remove Old City and road
                //
                foreach (var city in OvalShapeCity)
                    ShapeContainerAllCityShape.Shapes.Remove(city);
                foreach (var anyLine in LineShapeWay)
                    ShapeContainerAllCityShape.Shapes.Remove(anyLine);
                OvalShapeCity.Clear();
                CounterCity = 0;
                //
                // Create New City
                //
                var cityPositions = File.ReadAllLines(ofd.FileName);
                foreach (var cityP in cityPositions)
                {
                    var startIndexX = cityP.IndexOf("{X=", StringComparison.CurrentCultureIgnoreCase) + 3;
                    var endIndexX = cityP.IndexOf(",", StringComparison.CurrentCultureIgnoreCase);
                    var x = int.Parse(cityP.Substring(startIndexX, endIndexX - startIndexX));

                    var startIndexY = cityP.IndexOf(",Y=", StringComparison.CurrentCultureIgnoreCase) + 3;
                    var endIndexY = cityP.IndexOf("}", StringComparison.CurrentCultureIgnoreCase);
                    var y = int.Parse(cityP.Substring(startIndexY, endIndexY - startIndexY));
                    create_City(new Point(x, y));
                }
                //
                // Refresh City Position List
                //
                refreshDGV_CityPositions();
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                RestoreDirectory = true,
                Title = @"Save City Positions",
                Filter = @"Text files|*.txt",
                DefaultExt = "CityPositions.txt"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var postions = new List<string>();
                foreach (var city in OvalShapeCity)
                {
                    postions.Add(city.Location.ToString());
                }
                File.WriteAllLines(sfd.FileName, postions.ToArray());
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
            for (var th = 0; th < Process.GetCurrentProcess().Threads.Count; th++)
                Process.GetCurrentProcess().Threads[th].Dispose();
            Application.Exit();
        }

        private void dynamicalGraphicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dynamicalGraphicToolStripMenuItem.Checked = !dynamicalGraphicToolStripMenuItem.Checked;

            if (dynamicalGraphicToolStripMenuItem.Checked) RefreshTour();

            toolsToolStripMenuItem.ShowDropDown();
        }

        private void timerFitnessGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timerFitnessGraphToolStripMenuItem.Checked)
            {
                _tfg.Dispose();
                timerFitnessGraphToolStripMenuItem.Checked = false;
            }
            else if (_pPlistTfg != null)
            {
                _tfg = new TimeFitnessGraph();
                _tfg.timerGraphToolStripMenuItem = timerFitnessGraphToolStripMenuItem;
                _tfg.PPlist = _pPlistTfg;
                _tfg.Show();
            }
            toolsToolStripMenuItem.ShowDropDown();
        }

        private void generationFitnessGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (generationFitnessGraphToolStripMenuItem.Checked)
            {
                _gfg.Dispose();
                generationFitnessGraphToolStripMenuItem.Checked = false;
            }
            else if (_pPlistGfg != null)
            {
                _gfg = new GenerationFitnessGraph();
                _gfg.timerGraphToolStripMenuItem = generationFitnessGraphToolStripMenuItem;
                _gfg.PPlist = _pPlistGfg;
                _gfg.Show();
            }
            toolsToolStripMenuItem.ShowDropDown();
        }

        private void timerGenerationGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timerGenerationGraphToolStripMenuItem.Checked)
            {
                _tgg.Dispose();
                timerGenerationGraphToolStripMenuItem.Checked = false;
            }
            else if (_pPlistTgg != null)
            {
                _tgg = new TimeGenerationGraph();
                _tgg.timerGraphToolStripMenuItem = timerGenerationGraphToolStripMenuItem;
                _tgg.PPlist = _pPlistTgg;
                _tgg.Show();
            }
            toolsToolStripMenuItem.ShowDropDown();
        }

        private void newRandomCitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var enrpForm = new EnterNumberRandomPointForm();
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
                var rand = new System.Random();
                for (var citiesNo = 0; citiesNo < enrpForm.NumberOfCities; citiesNo++)
                {
                    //
                    // find new safely points according by contact rate:
                    Point newPoint;
                    bool safely;
                    var maxSafelyPoint = new Point(); // save best safely point if do not found any safely Points
                    double bestFitness = 0; /// save distance between maxSafelyPoint and newPoint
                    var maxSafelyLoopsNo = enrpForm.NumberSafety; // Probability for find new Safely points
                    do
                    {
                        newPoint = new Point(rand.Next(1, Width - 300), rand.Next(65, Height - 85));
                        safely = true;
                        foreach (var otherCity in OvalShapeCity) // Check Safety!
                        {
                            if (Math.Abs(otherCity.Location.X - newPoint.X) < 24 &&
                                Math.Abs(otherCity.Location.Y - newPoint.Y) < 24)
                            {
                                safely = false;
                                var fitness = Math.Sqrt(Math.Pow((newPoint.X - otherCity.Location.X), 2) + Math.Pow((newPoint.Y - otherCity.Location.Y), 2));
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
            var about = new FormAbout();
            about.ShowDialog();
        }

        private void btnStartStop_CheckedChanged(object sender, EventArgs e)
        {
            if (btnStartStop.Checked)
            {
                btnPauseResume.Enabled = true;
                btnStartStop.Text = "Stop Process ×";
                #region Start
                if (OvalShapeCity.Count <= 1)
                {
                    btnStartStop.Checked = false;
                    MessageBox.Show("Please Create some cities for a tour!", "Empty Genome Exception",
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
                foreach (var anyLine in LineShapeWay)
                    ShapeContainerAllCityShape.Shapes.Remove(anyLine);
                LineShapeWay.Clear();

                foreach (var shape in ShapeContainerAllCityShape.Shapes)
                {
                    if (shape.GetType() != typeof(OvalShape) && shape is Shape s)
                    {
                        ShapeContainerAllCityShape.Shapes.Remove(s);
                    }
                }

                ShapeContainerAllCityShape.Refresh();

                for (var c = 0; c < OvalShapeCity.Count; c++)
                {
                    var newLine = new LineShape
                    {
                        BorderColor = Color.Blue,
                        Cursor = Cursors.Default,
                        Enabled = false
                    };
                    LineShapeWay.Add(newLine);
                }
                //
                //
                #endregion
                //
                // Solve();
                try
                {
                    if (!_runTime.IsAlive)
                    {
                        _runTime = new Thread(Ga);
                        SetThreadPriority(_runTime);
                        _runTime.Start();
                    }
                }
                catch
                {
                    _runTime = new Thread(Ga);
                    SetThreadPriority(_runTime);
                    _runTime.Start();
                }
                #endregion
            }
            else
            {
                if (btnPauseResume.Checked)
                {
                    btnPauseResume.Checked = false;
                }
                btnStartStop.Text = @"&Start Process";
                Stop();
            }
        }

        private void btnPauseResume_CheckedChanged(object sender, EventArgs e)
        {
            if (btnPauseResume.Checked)
            {
                btnPauseResume.Text = @"&Resume Process";
                try
                {
                    if (_runTime.IsAlive)
                        _runTime.Suspend();
                }
                catch { }
            }
            else
            {
                btnPauseResume.Text = @"&Pause Process";
                try { if (_runTime.ThreadState == ThreadState.Suspended) _runTime.Resume(); }
                catch { }

                foreach (var anyLine in LineShapeWay)
                    ShapeContainerAllCityShape.Shapes.Remove(anyLine);
                foreach (var anyLine in LineShapeWay)
                    ShapeContainerAllCityShape.Shapes.Add(anyLine);
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
            SetThreadPriority(_runTime);
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
            SetThreadPriority(_runTime);
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
            SetThreadPriority(_runTime);
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
            SetThreadPriority(_runTime);
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
            SetThreadPriority(_runTime);
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
            SetThreadPriority(_runTime);
        }

        private void SetAffinityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var paf = new ProcessorAffinityForm();
            paf.ShowDialog();
            CountCpuCore = CalcCountOfCpu();
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
        public ThreadToken(int threadNo, int length, int startIndex)
        {
            No = threadNo;
            Length = length;
            StartIndex = startIndex;
            Rand = new System.Random();
        }
        public int No;
        public int Length;
        public int StartIndex;
        public System.Random Rand;
    };
}