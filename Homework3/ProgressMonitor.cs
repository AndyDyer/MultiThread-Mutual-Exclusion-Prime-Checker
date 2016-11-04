using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework3 {
    internal class ProgressMonitor {
        private readonly List<long> _results;
        public long TotalCount = 0;

        public ProgressMonitor(List<long> results) {
            _results = results;
        }

        public void Run() {
            while (true) {
                Thread.Sleep(100); // wait for 1/10th of a second

                lock (_results)
                {
                    if (_results.Count == 0) break;
                    TotalCount += _results.Count;
                    _results.Clear(); // clear out the current primes to save some memory
                    Console.WriteLine("The amount of primes:" + TotalCount);
                }


            }
        }
    }
}