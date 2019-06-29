using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TSP.Core
{
    public class GeneticAlgorithm
    {
        public int ChromosomeLenght { get; set; }
        public int PopunationLenght { get; set; }
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
            PopunationLenght = popLenght ?? 100;
            SelectionPercent = selectionPercent ?? 50;
            MutationProbability = mutationRate ?? 20;
            RegenerationLimit = maxRegenerationCount ?? int.MaxValue;
            RegenerationCounter = 0;
            ConvergenceRate = convergenceRate ?? 60;
            Population = Enumerable.Range(0, PopunationLenght).Select(r => new Chromosome(ChromosomeLenght).Randomize()).ToArray();
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

    }
}