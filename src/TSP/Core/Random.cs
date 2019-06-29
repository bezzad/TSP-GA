using System;
using System.Collections.Generic;
using System.Linq;

namespace TSP.Core
{
    public class Random : IDisposable
    {
        private List<int> List { get; }
        private int Min { get; }
        private int Max { get; }
        private int Preview { get; set; }

        public Random(int min, int max)
        {
            Min = min;
            Max = max;
            Preview = -1;
            List = Enumerable.Range(min, max - min).ToList();
        }

        public int Next()
        {
            var num = List.OrderBy(e => Guid.NewGuid()).First();
            Preview = (num == Preview && Min != Max) ? Next() : num;
            return Preview;
        }

        public void Dispose()
        {
            if (List != null)
                GC.SuppressFinalize(List);
        }

        ~Random()
        {
            Dispose();
        }
    }
}
