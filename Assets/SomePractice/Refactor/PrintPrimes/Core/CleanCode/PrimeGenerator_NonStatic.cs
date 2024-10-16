using System;
using System.Collections.Generic;

namespace Refactoring.PrintPrimes.CleanCode
{
    public class PrimeGenerator_NonStatic
    {
        private int[] primes;
        private List<int> multiplesOfPrimeFactors;

        public int[] Generate(int numberOfPrimes){
            primes = new int[numberOfPrimes];
            multiplesOfPrimeFactors = new List<int>();
            Set2AsFirstPrime();
            CheckOddNumbersForSubsequentPrimes();
            return primes;
        }

        private void Set2AsFirstPrime(){
            primes[0] = 2;
            multiplesOfPrimeFactors.Add(2);
        }

        private void CheckOddNumbersForSubsequentPrimes(){
            int primeIndex = 1;
            for(int candidate = 3;
                primeIndex < primes.Length;
                candidate += 2){
                if(IsPrime(candidate)){
                    primes[primeIndex++] = candidate;
                }
            }
        }

        private bool IsPrime(int candidate){
            if(IsLeastRelevantMultipleOfNextLargerPrimeFactor(candidate)){
                multiplesOfPrimeFactors.Add(candidate);
                return false;
            }
            return IsNotMultipleOfAnyPreviousPrimeFactor(candidate);
        }

        private bool IsLeastRelevantMultipleOfNextLargerPrimeFactor(int candidate){
            int nextLargerPrimeFactor = primes[multiplesOfPrimeFactors.Count];
            int leastRelevantMultiple = nextLargerPrimeFactor * nextLargerPrimeFactor;
            return candidate == leastRelevantMultiple;
        }

        private bool IsNotMultipleOfAnyPreviousPrimeFactor(int candidate){
            for(int n = 1; n < multiplesOfPrimeFactors.Count; n++){
                if(IsMultipleOfNthPrimeFactor(candidate, n)){
                    return false;
                }
            }
            return true;
        }

        private bool IsMultipleOfNthPrimeFactor(int candidate, int n){
            return candidate == SmallestOddNthMultipleNotLessThanCandidate(candidate, n);
        }

        private int SmallestOddNthMultipleNotLessThanCandidate(int candidate, int n){
            int multiple = multiplesOfPrimeFactors[n];
            while(multiple < candidate){
                multiple += 2 * primes[n];
            }
            multiplesOfPrimeFactors[n] = multiple;
            return multiple;
        }
    }
}