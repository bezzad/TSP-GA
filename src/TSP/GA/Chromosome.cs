using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TSP.GA
{
    public class Chromosome
    {
        /// <summary>
        /// Save any city position One time for all times usage
        /// </summary>
        public static List<System.Drawing.Point> citiesPosition = new List<Point>();

        /// <summary>
        /// Integer array for save a Loop way ، between all city
        /// </summary>
        public int[] Tour;
        
        /// <summary>
        /// double variable for save Loop distance 
        /// </summary>
        private double fitness;
        /// <summary>
        /// Read-Only Fitness of Chromosome
        /// </summary>
        public double Fitness 
        {
            get { return fitness; }
        } 
        
        /// <summary>
        /// chromosome for save All City Rout Way
        /// </summary>
        /// <param name="RangeOfArray">Number of All City</param>
        public Chromosome(int RangeOfArray) // for define array length
        {
            Tour = new int[RangeOfArray];
            fitness = -1; // default Sum of distance = -1 or null
            clean();
        }

        /// <summary>
        /// clear offspring chromosome array by num '-1'
        /// </summary>
        private void clean()
        {
            for (int c = 0; c < Tour.Length; c++)
                Tour[c] = -1;
        }

        /// <summary>
        /// Calculate Tour Distance
        /// </summary>
        /// <returns></returns>
        public void Calculate_Fitness()
        {
            double cast = 0;
            double X1; // = ovalShape_City[i - 1].Location.X;
            double X2; // = ovalShape_City[i].Location.X;
            double Y1; // = ovalShape_City[i - 1].Location.Y;
            double Y2; // = ovalShape_City[i].Location.Y;
            for (int i = 1; i < Tour.Length; i++)
            {
                X1 = citiesPosition[Tour[i - 1]].X;
                X2 = citiesPosition[Tour[i]].X;
                Y1 = citiesPosition[Tour[i - 1]].Y;
                Y2 = citiesPosition[Tour[i]].Y;
                cast += Math.Sqrt(((X2 - X1) * (X2 - X1)) + ((Y2 - Y1) * (Y2 - Y1)));
            }
            //
            // calculate last loop way (the way is between 0 & last city)
            X1 = citiesPosition[Tour[0]].X;
            X2 = citiesPosition[Tour[Tour.Length - 1]].X;
            Y1 = citiesPosition[Tour[0]].Y;
            Y2 = citiesPosition[Tour[Tour.Length - 1]].Y;
            cast += Math.Sqrt(((X2 - X1) * (X2 - X1)) + ((Y2 - Y1) * (Y2 - Y1)));
            //
            // return  loop city distance 
            this.fitness = cast;
        }
    }
}
