using System;

namespace TSP.Core
{
    /// <summary>
    /// Get Chromosome by MutationHelper Operand
    /// </summary>
    [Obsolete("MutationHelper is deprecated, please use GeneticAlgorithm.Mutation instead.")]
    public static class MutationHelper
    {
        // Function Uniform of class MutationHelper
        // change a bit of offspring chromosome for mutation
        public static void Mutation (this Chromosome child, System.Random rand)
        {
            // Random Number for choose 2 bit between 0 ~ (offspring.Length - 1)
            // if(offspring.Length == 8)
            //                           (0)×-------------×(offspring.Length-1)
            //                             |_|_|_|_|_|_|_|_|
            //                              0 1 2 3 4 5 6 7
            //
            // change 2 bit locate (Greedy MutationHelper)
            // before Greedy Mutate:
            // chromosome Child =      |_|_|_|_|_|_|_|_| ...
            //                          0 1 2 3 4 5 6 7 
            // Greedy Mutating:
            // Select 2 bit (1 & 4)       *     *
            // chromosome Child =      |_|_|_|_|_|_|_|_| ...            (Step 1)
            //                          0 1 2 3 4 5 6 7 
            // After Greedy MutationHelper:    
            // Changed 2 bit (1 & 4)      *     *
            // chromosome Child =      |_|_|_|_|_|_|_|_| ...            (Step 2)
            //                          0 4 2 3 1 5 6 7 
            //
            // Step 1: -------------- Select 2 bit by Random Number -----------------------
            int bit0 = rand.Next(0, child.Genome.Length - 1);
            int bit1;
            do
            {
                bit1 = rand.Next(0, child.Genome.Length - 1);
            }
            // if bit0 == bit1 then no mutate because selected bit change by self
            while (bit1 == bit0); 
            // -------------------------------------------------------------------------------
            // Step 2: +++++++++++++++++++ Change selected bit's +++++++++++++++++++++++++++++
            //
            //         buffer <---- bit0
            int buffer = child.Genome[bit0];
            //
            //         bit0   <---- bit1
            child.Genome[bit0] = child.Genome[bit1];
            //
            //         bit1   <---- buffer
            child.Genome[bit1] = buffer;
            // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        }
    }
}
