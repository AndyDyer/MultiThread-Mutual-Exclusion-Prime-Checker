using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Homework3 {
    internal class Calculator {

        public void Run(NumberReader reader) {
            var results = new List<long>();
            var numbersToCheck = new Queue<long>();
            SpinLock myLock = new SpinLock();
            bool gotLock = false;

            foreach (var value in reader.ReadIntegers())
            {
                numbersToCheck.Enqueue(value);
            }

            Console.WriteLine(numbersToCheck.Count);

            StartComputationThreads(results, numbersToCheck, myLock, gotLock);

            var progressMonitor = new ProgressMonitor(results);

            new Thread(progressMonitor.Run) {IsBackground = true}.Start();

            while (numbersToCheck.Count > 0) {
                Thread.Sleep(100); // wait for the computation to complete.
            }
         Console.WriteLine("{0} of the numbers were prime", progressMonitor.TotalCount);
        }

        private static void StartComputationThreads(List<long> results, Queue<long> numbersToCheck, SpinLock myLock, bool gotLock) {
            var threads = CreateThreads(results, numbersToCheck, myLock, gotLock);
            threads.ForEach(thread => thread.Start());
        }
        
        private static List<Thread> CreateThreads(List<long> results, Queue<long> numbersToCheck, SpinLock myLock, bool gotLock) {
            var threadCount = 4;

            Console.WriteLine("Using {0} compute threads and 1 I/O thread", threadCount);

            var threads =
                (from threadNumber in Sequence.Create(0, threadCount)
                    let calculator = new IsNumberPrimeCalculator(results, numbersToCheck, myLock, gotLock)
                    let newThread =
                        new Thread(calculator.CheckIfNumbersArePrime) {
                            IsBackground = true,
                            Priority = ThreadPriority.BelowNormal
                        }
                    select newThread).ToList();
            return threads;
        }
    }
}