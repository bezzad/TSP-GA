using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TSP.Core
{
    public class GeneticAlgorithm
    {
        public int ChromosomeLenght { get; set; }
        public int PopulationLenght { get; set; }
        public int SelectionPercent { get; set; }
        public int MutationProbability { get; set; }
        public int RegenerationLimit { get; set; }
        public int RegenerationCounter { get; set; }
        public int ConvergenceRate { get; set; }
        public Chromosome[] Population { get; set; }


        public GeneticAlgorithm(int len, int? popLenght = null,
            int? selectionPercent = null, int? mutationRate = null,
            int? maxRegenerationCount = null, int? convergenceRate = null)
        {
            ChromosomeLenght = len;
            PopulationLenght = popLenght ?? 100;
            SelectionPercent = selectionPercent ?? 50;
            MutationProbability = mutationRate ?? 20;
            RegenerationLimit = maxRegenerationCount ?? int.MaxValue;
            RegenerationCounter = 0;
            ConvergenceRate = convergenceRate ?? 60;
            Population = Enumerable.Range(0, PopulationLenght).Select(r => new Chromosome(ChromosomeLenght).Randomize()).ToArray();
        }

        public Chromosome Start()
        {
            Debug.WriteLine("Starting GA ...");

            while (Evaluation())
            {
                Selection(SelectionPercent);
            }

            return Population.First(); // Elitest chromosome
        }
        
        public bool Evaluation()
        {
            Population = Population.OrderBy(ch => ch.Fitness).ToArray(); // sort 
            var elit = Population.First();
            // if GA end condition occured then return false to stop generation;
            if (Math.Abs(elit.Fitness) < 2)
            {
                Debug.WriteLine("GA ended due to the best chromosome found :)");
                return false; // stop GA
            }
            if (RegenerationCounter >= RegenerationLimit)
            {
                Debug.WriteLine("GA ended due to the limitation of regeneration!!!");
                return false; // stop GA
            }
            if (Population.Count(c => Math.Abs(c.Fitness - elit.Fitness) < 1) >= Math.Min((double)ConvergenceRate / 100, 0.9) * PopulationLenght)
            {
                // calculate histogram to seen chromosomes convergence            
                Debug.WriteLine("GA ended due to the convergence of chromosomes :(");
                return false;
            }

            return true; // continue GA
        }

        public void Regeneration()
        {
            RegenerationCounter++;
            if (RegenerationCounter % 100 == 0)
                Debug.WriteLine("generation {0}, elite fitness is: {1}", RegenerationCounter, Population[0].Fitness);

            var newPopulation = new List<Chromosome>();

            // create new chromosomes 
            for (var index = Population.Length; index < PopulationLenght; index++)
            {
                var parent = GetRandomParent();
                var child = Crossover(parent.mom, parent.dad);
                Mutation(child, MutationProbability);
                child.Evaluate();
                newPopulation.Add(child);
            }

            Population = Population.Concat(newPopulation).ToArray(); // append newPopulation to end of this.Popunation
        }

        public void Selection(int percent)
        {
            var keepCount = percent * PopulationLenght / 100;
            Population = Population.Take(keepCount).ToArray(); // remove week chromosomes from keepCount index to end of array  
            Regeneration(); // start new generation   
        }

        public void Mutation(Chromosome chromosome, int rate)
        {
            //  _______________
            // |1|2|6|4|5|3|7|8|
            //      ^     ^
            //        SWAP
            //
            if (new Random(0, 100).Next() <= rate)
            { // if random number occured within mutation rate
                var rand = new Random(0, chromosome.Length - 1);
                var gen1 = rand.Next();
                var gen2 = rand.Next();
                if (gen1 == gen2)
                    throw new Exception("Mutation gens are duplicate!");
                // swape two gene from genome
                var genBuffer = chromosome.Genome[gen1];
                chromosome.Genome[gen1] = chromosome.Genome[gen2];
                chromosome.Genome[gen2] = genBuffer;
            }
        }

        public Chromosome Crossover(Chromosome mom, Chromosome dad)
        {
            if (mom == dad)
                Debug.WriteLine("Oh shet! are the mom and dad same!?");

            var child = new Chromosome(ChromosomeLenght)
            {
                Genome = Pmx(mom.Genome, dad.Genome)
            };

            return child;
        }

        /// <summary>
        /// PMX - Partially Mapped Crossover
        /// </summary>
        protected T[] Pmx<T>(T[] mom, T[] dad, int? cut1 = null, int? cut2 = null) where T : struct
        {
            cut1 = cut1 ?? new Random(1, mom.Length / 2).Next();   // left side of crossover section
            cut2 = cut2 ?? new Random(cut1.Value + 1, mom.Length - 2).Next();   // right side of crossover section
            var child = new T[mom.Length];
            var usedGenes = new HashSet<T>();
            var childEmptyIndexes = new Stack<int>();

            // copy:  [   |-----|   ]   from mom
            for (var i = cut1.Value; i <= cut2.Value; i++)
            {
                child[i] = mom[i];
                usedGenes.Add(mom[i]); // gene used
            }

            // copy:  [---|     |   ]   from dad
            for (var i = 0; i < cut1.Value; i++)
            {
                if (usedGenes.Contains(dad[i]))
                {
                    childEmptyIndexes.Push(i);
                }
                else
                {
                    child[i] = dad[i];
                    usedGenes.Add(dad[i]); // gene used
                }
            }

            // copy:  [   |     |---]   from dad
            for (var i = cut2.Value + 1; i < dad.Length; i++)
            {
                if (usedGenes.Contains(dad[i]))
                {
                    childEmptyIndexes.Push(i);
                }
                else
                {
                    child[i] = dad[i];
                    usedGenes.Add(dad[i]); // gene used
                }
            }

            // set child remain genes
            foreach (var gene in dad.Concat(mom))
            {
                if (childEmptyIndexes.Count == 0)
                    break;

                if (!usedGenes.Contains(gene))
                {
                    child[childEmptyIndexes.Pop()] = gene;
                }
            }

            return child;
        }

        protected (Chromosome mom, Chromosome dad) GetRandomParent()
        {
            var rand = new Random(0, Population.Length - 1);
            return (Population[rand.Next()], Population[rand.Next()]);
        }

    }
}