using System;
using System.Collections;

namespace TSP.GA
{
    public static class Crossover
    {
        /// <summary>
        /// Do Crossover between 2 Chromosome's
        /// </summary>
        /// <param name="Dad">Father chromosome for product Children Chromosome</param>
        /// <param name="Mum">Mather chromosome for product Children Chromosome</param>
        /// <param name="rand">random reproducer</param>
        /// <returns></returns>
        public static Chromosome crossover(this Chromosome Dad, Chromosome Mum, Random rand)
        {
            // for check written or duplicated
            bool write = false;

            ArrayList duplicate = new ArrayList();

            //
            // define offspring chromosome length
            Chromosome offspring = new Chromosome(Dad.Tour.Length);
            
            //
            //          Greedy Crossover Algorithm
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
            int index_dad = rand.Next(0, Dad.Tour.Length - 1);
            int index_mum = Mum.Tour.IndexOf(Dad.Tour[index_dad]);
            //
            // push selected info in offspring array
            push_info(offspring.Tour, Dad.Tour[index_dad], "Center", duplicate, out write);
            //
            // read Left of Dad chromosome & Right of Mum chromosome
            index_dad--; // left loop   <----
            index_mum++; // right loop  ---->
            //
            // -1 because selected point info was saved
            int child_lenght = offspring.Tour.Length - 1; // number of free space or '-1'
            while (child_lenght > 0) 
            {
                // check range of index number 
                if (index_dad < 0) index_dad = Dad.Tour.Length - 1;
                if (index_mum >= Mum.Tour.Length) index_mum = 0;

                write = false;
                offspring.Tour.push_info(Dad.Tour[index_dad], "Left", duplicate, out write);
                if (write) child_lenght--;
                write = false;
                offspring.Tour.push_info(Mum.Tour[index_mum], "Right", duplicate, out write);
                if (write) child_lenght--;
                //
                // REDUCTION 
                index_dad--;
                index_mum++;
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
        /// <param name="place_array"></param>
        /// <param name="info"></param>
        /// <param name="loc_course"></param>
        /// <param name="duplicate"></param>
        /// <param name="write"></param>
        private static void push_info(this int[] place_array, int info, string loc_course, ArrayList duplicate, out bool write)
        {
            write = false;
            // check duplicate info in array
            if (duplicate.Contains(info)) return; // exit on function
            else duplicate.Add(info);

            // else "info wasn't duplicate" then "write = true"
            write = true;
            //
            // place info in special locate
            int index;
            switch (loc_course)
            {
                case "Center":
                    {
                        index = Convert.ToInt32(Math.Floor(Convert.ToDouble(place_array.Length / 2)));
                        place_array[index] = info;
                        write = true;
                    }
                    return;
                case "Left":
                    {
                        for (int l = 0; l < place_array.Length; l++) // '-1' is free home        |
                            if (place_array[l] != -1) // find free space home -1 ( |-1|-1|-1|-1|5|... )
                            {                     //                        |
                                if ((l - 1) < 0)  // not found free home ( |9|_|_|... )
                                {
                                    // Shift Right all array home
                                    // before Shift Right = |*|*|*|_|_|_|_|_|
                                    // After Shift Right  = |_|*|*|*|_|_|_|_|
                                    for (int s = place_array.Length - 1; s > 0; s--)
                                        place_array[s] = place_array[s - 1];
                                    //
                                    // save info at first home or place_array[0]
                                    place_array[0] = info;
                                    return;
                                }
                                else
                                {
                                    place_array[l - 1] = info;
                                    return;
                                }
                            }
                        // all array home is free by '-1' or "place_array has none home but never happen this mood"
                        // save info in left of home = place_array[0]
                        place_array[0] = info;
                    }
                    return;
                case "Right":
                    {
                        for (int l = place_array.Length - 1; l >= 0; l--)  // '-1' is free home       
                            if (place_array[l] != -1) // find free space home + 1 ( ...|5|-1|-1|-1|-1| )
                            {                     //                        |
                                if ((l + 1) >= place_array.Length)  // not found free home ( ...|_|_|_|_|9| )
                                {
                                    // Shift Left all array home
                                    // before Shift Left = |_|_|_|_|_|*|*|*|
                                    // After Shift Left  = |_|_|_|_|*|*|*|_|
                                    for (int s = 0; s < place_array.Length - 1; s++) 
                                        place_array[s] = place_array[s + 1];
                                    //
                                    // save info at last home or place_array[place_array.Lenght-1]
                                    place_array[place_array.Length - 1] = info;
                                    return;
                                }
                                else
                                {
                                    place_array[l + 1] = info;
                                    return;
                                }
                            }
                        // all array home is free by '-1' or "place_array has none home but never happen this mood"
                        // save info in Right of home = place_array[place_array.Lenght-1]
                        place_array[place_array.Length - 1] = info;
                    }
                    return;
                default: return;
            }
        }
    }
}