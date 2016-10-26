
using System;
using System.Collections.Generic;
using System.Threading;


namespace Homework3 {

    internal class IsNumberPrimeCalculator {
        private readonly ICollection<long> _primeNumbers;
        private readonly Queue<long> _numbersToCheck;
        SpinLock _myLock;
        bool _gotLock;
        private static Mutex mut = new Mutex();


        public IsNumberPrimeCalculator(ICollection<long> primeNumbers, Queue<long> numbersToCheck, SpinLock myLock, bool gotLock) {
            _primeNumbers = primeNumbers;
            _numbersToCheck = numbersToCheck;
            _myLock = myLock;
            _gotLock = gotLock;
        }
        public void CheckIfNumbersArePrime() {
            long numberToCheck = 0;
            while (true) {
                _gotLock = false;
                try
                {
                    _myLock.Enter(ref _gotLock);
                    if (_numbersToCheck.Count > 0)
                    {
                        numberToCheck = _numbersToCheck.Dequeue();
                    } else
                    {
                        break;
                    }
                }
                finally
                {
                    if (_gotLock) {
                        _myLock.Exit();
                    }
                }
               

                if (IsNumberPrime(numberToCheck)) {

                    _gotLock = false;
                    try
                    {
                        _myLock.Enter(ref _gotLock);
                        _primeNumbers.Add(numberToCheck);
                    }
                    finally
                    {
                        if (_gotLock)
                        {
                            _myLock.Exit();
                        }
                    }
                }
                if (_numbersToCheck.Count == 0)
                {
                    break;
                }
            }
        }

        private bool IsNumberPrime(long numberWeAreChecking) {
            const int firstNumberToCheck = 5;

            if (numberWeAreChecking % 2 == 0 || numberWeAreChecking % 3 == 0)
            {
                return false;
            }
            else
            {
                int lastNumberToCheck = (int) Math.Sqrt(numberWeAreChecking);
                for (int currentDivisor = firstNumberToCheck; currentDivisor < lastNumberToCheck; currentDivisor += 2)
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
