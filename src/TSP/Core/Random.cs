using System;
using System.Collections.Generic;
using System.Linq;

namespace TSP.Core
{
    public class Random : IDisposable
    {
        private List<int> List { get; }

        public Random(int min, int max)
        {
            List = Enumerable.Range(min, max - min).ToList();
        }

        public int Next()
        {
            return List.OrderBy(e => Guid.NewGuid()).First();
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
