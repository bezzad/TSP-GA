using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TSP.GA
{
    public class Chromosome
    {
        /// <summary>
        /// Save any city position One time for all times usage
        /// </summary>
        public static List<Point> CitiesPosition { get; set; } = new List<Point>();

        /// <summary>
        /// A Tour or Integer array for save a Loop way ، between all city
        /// </summary>
        public int[] Genome { get; set; }

        /// <summary>
        /// Number of All City
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Read-Only Fitness of Chromosome
        /// </summary>
        public double Fitness { get; private set; }

        /// <summary>
        /// chromosome for save All City Rout Way
        /// </summary>
        /// <param name="len">Number of All City</param>
        public Chromosome(int len) // for define array length
        {
            Length = len;
            Genome = Enumerable.Repeat(-1, Length).ToArray();
            Fitness = double.MaxValue;
        }

        /// <summary>
        /// Calculate Genome Distance
        /// </summary>
        /// <returns></returns>
        public void Evaluate()
        {
            double fit = 0;
            for (var i = 0; i < Length - 2; i++)
                fit += CalcDistance(CitiesPosition[Genome[i]], CitiesPosition[Genome[i + 1]]);

            // calc first node distance in from last node
            //fit += CalcDistance(CitiesPosition[Genome[0]], CitiesPosition[Genome[Length - 1]]);

            //
            // return  loop city distance 
            Fitness = fit;
        }

        public Chromosome Randomize()
        {
            var nums = Enumerable.Range(0, Length).ToList(); // Length:8 => [1,2,3,4,5,6,7,8]
            for (var g = 0; g < Length; g++)
            {
                var rand = new Random().Next(0, nums.Count - 1);
                Genome[g] = nums[rand];
                nums.RemoveAt(rand);
            }

            Evaluate();
            return this;
        }

        private double CalcDistance(Point p1, Point p2)
        {
            var xDiff = p2.X - p1.X;
            var yDiff = p2.Y - p1.Y;

            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }
    }
}
