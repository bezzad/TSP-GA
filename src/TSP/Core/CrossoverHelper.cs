using System;
using System.Collections;

namespace TSP.Core
{
    public static class CrossoverHelper
    {
        /// <summary>
        /// Do CrossoverHelper between 2 Chromosome's
        /// </summary>
        /// <param name="dad">Father chromosome for product Children Chromosome</param>
        /// <param name="mum">Mather chromosome for product Children Chromosome</param>
        /// <param name="rand">random reproducer</param>
        /// <returns></returns>
        public static Chromosome Crossover(this Chromosome dad, Chromosome mum, Random rand)
        {
            // for check written or duplicated
            bool write = false;

            ArrayList duplicate = new ArrayList();

            //
            // define offspring chromosome length
            Chromosome offspring = new Chromosome(dad.Genome.Length);
            
            //
            //          Greedy CrossoverHelper Algorithm
            //
            //   _                    _   ?
            // CDEFABG    Dad   <-----E  <--      Loop Vector to Left
            //
            //      _            ?   _
            // GFADCEB    Mum   -->  E----->      Loop Vector to Right
            //
            // Offspring (New Chromosome) = 
            //            Step '1':         E
            //            Step '2':    	   DEB	
            //            Step '3':    	  CDEBG
            //            Step '4':	      CDEBGF
            //            Step '5':	      CDEBGFA
            //                           _
            // select point, in example: E
            int indexDad = rand.Next(0, dad.Genome.Length - 1);
            int indexMum = mum.Genome.IndexOf(dad.Genome[indexDad]);
            //
            // push selected info in offspring array
            push_info(offspring.Genome, dad.Genome[indexDad], "Center", duplicate, out write);
            //
            // read Left of Dad chromosome & Right of Mum chromosome
            indexDad--; // left loop   <----
            indexMum++; // right loop  ---->
            //
            // -1 because selected point info was saved
            int childLenght = offspring.Genome.Length - 1; // number of free space or '-1'
            while (childLenght > 0) 
            {
                // check range of index number 
                if (indexDad < 0) indexDad = dad.Genome.Length - 1;
                if (indexMum >= mum.Genome.Length) indexMum = 0;

                write = false;
                offspring.Genome.push_info(dad.Genome[indexDad], "Left", duplicate, out write);
                if (write) childLenght--;
                write = false;
                offspring.Genome.push_info(mum.Genome[indexMum], "Right", duplicate, out write);
                if (write) childLenght--;
                //
                // REDUCTION 
                indexDad--;
                indexMum++;
            }

            return offspring;
        }

        /// <summary>
        /// Find index of info in this array
        /// </summary>
        /// <param name="array">this integer array</param>
        /// <param name="info">a integer for search in array</param>
        /// <returns>Index Of founded info in array</returns>
        private static int IndexOf(this int[] array, int info)
        {
            for (int f = 0; f < array.Length; f++)
                if (array[f] == info)
                    return f;
            return -1;// throw new Exception("your info not found in search_array");
        }

        /// <summary>
        /// Place info in child chromosome by special locate (by check duplicate info)
        /// </summary>
        /// <param name="placeArray"></param>
        /// <param name="info"></param>
        /// <param name="locCourse"></param>
        /// <param name="duplicate"></param>
        /// <param name="write"></param>
        private static void push_info(this int[] placeArray, int info, string locCourse, ArrayList duplicate, out bool write)
        {
            write = false;
            // check duplicate info in array
            if (duplicate.Contains(info)) return; // exit on function
            else duplicate.Add(info);

            // else "info wasn't duplicate" then "write = true"
            write = true;
            //
            // place info in special locate
            switch (locCourse)
            {
                case "Center":
                    {
                        var index = Convert.ToInt32(Math.Floor(Convert.ToDouble(placeArray.Length / 2)));
                        placeArray[index] = info;
                        write = true;
                    }
                    return;
                case "Left":
                    {
                        for (int l = 0; l < placeArray.Length; l++) // '-1' is free home        |
                            if (placeArray[l] != -1) // find free space home -1 ( |-1|-1|-1|-1|5|... )
                            {                     //                        |
                                if ((l - 1) < 0)  // not found free home ( |9|_|_|... )
                                {
                                    // Shift Right all array home
                                    // before Shift Right = |*|*|*|_|_|_|_|_|
                                    // After Shift Right  = |_|*|*|*|_|_|_|_|
                                    for (int s = placeArray.Length - 1; s > 0; s--)
                                        placeArray[s] = placeArray[s - 1];
                                    //
                                    // save info at first home or place_array[0]
                                    placeArray[0] = info;
                                    return;
                                }
                                else
                                {
                                    placeArray[l - 1] = info;
                                    return;
                                }
                            }
                        // all array home is free by '-1' or "place_array has none home but never happen this mood"
                        // save info in left of home = place_array[0]
                        placeArray[0] = info;
                    }
                    return;
                case "Right":
                    {
                        for (int l = placeArray.Length - 1; l >= 0; l--)  // '-1' is free home       
                            if (placeArray[l] != -1) // find free space home + 1 ( ...|5|-1|-1|-1|-1| )
                            {                     //                        |
                                if ((l + 1) >= placeArray.Length)  // not found free home ( ...|_|_|_|_|9| )
                                {
                                    // Shift Left all array home
                                    // before Shift Left = |_|_|_|_|_|*|*|*|
                                    // After Shift Left  = |_|_|_|_|*|*|*|_|
                                    for (int s = 0; s < placeArray.Length - 1; s++) 
                                        placeArray[s] = placeArray[s + 1];
                                    //
                                    // save info at last home or place_array[place_array.Lenght-1]
                                    placeArray[placeArray.Length - 1] = info;
                                    return;
                                }
                                else
                                {
                                    placeArray[l + 1] = info;
                                    return;
                                }
                            }
                        // all array home is free by '-1' or "place_array has none home but never happen this mood"
                        // save info in Right of home = place_array[place_array.Lenght-1]
                        placeArray[placeArray.Length - 1] = info;
                    }
                    return;
                default: return;
            }
        }
    }
}