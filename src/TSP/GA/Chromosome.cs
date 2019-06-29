using System;
using System.Collections.Generic;
using System.Drawing;

namespace TSP.GA
{
    public class Chromosome
    {
        /// <summary>
        /// Save any city position One time for all times usage
        /// </summary>
        public static List<Point> CitiesPosition = new List<Point>();

        /// <summary>
        /// Integer array for save a Loop way ، between all city
        /// </summary>
        public int[] Tour;
       
        /// <summary>
        /// Read-Only Fitness of Chromosome
        /// </summary>
        public double Fitness { get; private set; }
        
        /// <summary>
        /// chromosome for save All City Rout Way
        /// </summary>
        /// <param name="rangeOfArray">Number of All City</param>
        public Chromosome(int rangeOfArray) // for define array length
        {
            Tour = new int[rangeOfArray];
            Fitness = -1; // default Sum of distance = -1 or null
            Clean();
        }

        /// <summary>
        /// clear offspring chromosome array by num '-1'
        /// </summary>
        private void Clean()
        {
            for (var c = 0; c < Tour.Length; c++)
                Tour[c] = -1;
        }

        /// <summary>
        /// Calculate Tour Distance
        /// </summary>
        /// <returns></returns>
        public void Calculate_Fitness()
        {
            double cast = 0;
            double x1; // = ovalShape_City[i - 1].Location.X;
            double x2; // = ovalShape_City[i].Location.X;
            double y1; // = ovalShape_City[i - 1].Location.Y;
            double y2; // = ovalShape_City[i].Location.Y;
            for (int i = 1; i < Tour.Length; i++)
            {
                x1 = CitiesPosition[Tour[i - 1]].X;
                x2 = CitiesPosition[Tour[i]].X;
                y1 = CitiesPosition[Tour[i - 1]].Y;
                y2 = CitiesPosition[Tour[i]].Y;
                cast += Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
            }
            //
            // calculate last loop way (the way is between 0 & last city)
            x1 = CitiesPosition[Tour[0]].X;
            x2 = CitiesPosition[Tour[Tour.Length - 1]].X;
            y1 = CitiesPosition[Tour[0]].Y;
            y2 = CitiesPosition[Tour[Tour.Length - 1]].Y;
            cast += Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
            //
            // return  loop city distance 
            Fitness = cast;
        }
    }
}
