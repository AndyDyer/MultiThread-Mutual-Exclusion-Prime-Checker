using System;
using System.Collections.Generic;
using System.Threading;

namespace Homework3 {
    internal class IsNumberPrimeCalculator {
        private readonly ICollection<long> _primeNumbers;
        private readonly Queue<long> _numbersToCheck;

        public IsNumberPrimeCalculator(ICollection<long> primeNumbers, Queue<long> numbersToCheck) {
            _primeNumbers = primeNumbers;
            _numbersToCheck = numbersToCheck;
        }

        public void CheckIfNumbersArePrime() {
            int i = 0;
            int j = 0;
            while (true) {
                j++;
                var numberToCheck = _numbersToCheck.Dequeue();
                if (IsNumberPrime(numberToCheck)) {
                    i++;
                    _primeNumbers.Add(numberToCheck);
                }
                if (_numbersToCheck.Count == 0)
                {
                    Console.WriteLine("This is how many hits: " + i + "    " + j);
                    break;
                }
            }
        }

        private bool IsNumberPrime(long numberWeAreChecking) {
            int i, flag = 0;
            for (i = 2; i <= numberWeAreChecking / 2; ++i)
            {
                if (numberWeAreChecking % i == 0)
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
                return true;
            else
                return false;
        }
    }
}
