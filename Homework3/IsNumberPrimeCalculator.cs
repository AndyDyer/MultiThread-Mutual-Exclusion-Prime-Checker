
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
            long numberToCheck = 0;
            while (true) {
                lock (_numbersToCheck)
                {
                    if (_numbersToCheck.Count > 0)
                    {
                        numberToCheck = _numbersToCheck.Dequeue();
                        if (IsNumberPrime(numberToCheck))
                        {

                            lock (_primeNumbers)
                            {
                                _primeNumbers.Add(numberToCheck);
                            }
                           
                        }
                    }
                    else
                    {
                        break;
                    }
                }
               
                if (_numbersToCheck.Count == 0)
                {
                    break;
                }
            }
        }

        private bool IsNumberPrime(long numberWeAreChecking) {
            const int firstNumberToCheck = 3;

            if (numberWeAreChecking % 2 == 0)
            {
                return false;
            }
            else
            {
                int lastNumberToCheck = ((int)Math.Sqrt(numberWeAreChecking));
                for (int currentDivisor = firstNumberToCheck; currentDivisor <= lastNumberToCheck; currentDivisor += 2)
                {
                    if (numberWeAreChecking % currentDivisor == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
