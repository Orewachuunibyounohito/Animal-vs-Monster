using UnityEngine;

namespace Refactoring.PrintPrimes
{
    public class PrintPrimes_After01 : MonoBehaviour
    {
        const int COUNT_OF_FIRST = 1000;
        const int LINES_IN_ONE_PAGE = 50;
        const int ITEMS_IN_ONE_LINE = 4;
        // const int WW = 10;
        const int COUNT_OF_THRESHOLD = 30;

        const int SMALLEST_PRIME = 2;

        [ContextMenu("Print")]
        public void print()
        {
            int[] primes = new int[COUNT_OF_FIRST + 1];
            int pageNumber;
            int currentOriginForPage;
            int currentOriginForRow;
            int IndexOfItem;
            int currentNumber;
            int currentIndex;
            bool foundPrime;
            int currentTraversalIndex;
            int currentThreshold;
            int currentThresholdIndex;
            int[] thresholds = new int[COUNT_OF_THRESHOLD + 1];

            currentNumber = 1;
            currentIndex = 1;
            primes[1] = 2;
            currentTraversalIndex = 2;
            currentThreshold = 9;
            while (currentIndex < COUNT_OF_FIRST)
            {
                do
                {
                    currentNumber = currentNumber + SMALLEST_PRIME;
                    if (currentNumber == currentThreshold)
                    {
                        currentTraversalIndex = currentTraversalIndex + 1;
                        currentThreshold = primes[currentTraversalIndex] * primes[currentTraversalIndex];
                        thresholds[currentTraversalIndex - 1] = currentNumber;
                    }
                    currentThresholdIndex = 2;
                    foundPrime = true;
                    bool hasNext = currentThresholdIndex < currentTraversalIndex && foundPrime;
                    while (hasNext)
                    {
                        while (thresholds[currentThresholdIndex] < currentNumber)
                        {
                            thresholds[currentThresholdIndex] = thresholds[currentThresholdIndex] + primes[currentThresholdIndex] + primes[currentThresholdIndex];
                        }
                        if (thresholds[currentThresholdIndex] == currentNumber)
                        {
                            foundPrime = false;
                        }
                        currentThresholdIndex = currentThresholdIndex + 1;
                        hasNext = currentThresholdIndex < currentTraversalIndex && foundPrime;
                    }
                } while (!foundPrime);
                currentIndex = currentIndex + 1;
                primes[currentIndex] = currentNumber;
            }
            {
                pageNumber = 1;
                currentOriginForPage = 1;
                while (currentOriginForPage <= COUNT_OF_FIRST)
                {
                    string log = "";
                    log += $"The First {COUNT_OF_FIRST} Prime Numbers --- {pageNumber}\n";
                    log += "\n";
                    for (currentOriginForRow = currentOriginForPage; currentOriginForRow < currentOriginForPage + LINES_IN_ONE_PAGE; currentOriginForRow++)
                    {
                        for (IndexOfItem = 0; IndexOfItem < ITEMS_IN_ONE_LINE; IndexOfItem++)
                        {
                            if (currentOriginForRow + IndexOfItem * LINES_IN_ONE_PAGE <= COUNT_OF_FIRST)
                            {
                                log += $"{primes[currentOriginForRow + IndexOfItem * LINES_IN_ONE_PAGE],10}";
                            }
                        }
                        log += "\n";
                    }
                    log += "\f";
                    Debug.Log(log);
                    pageNumber = pageNumber + 1;
                    currentOriginForPage = currentOriginForPage + LINES_IN_ONE_PAGE * ITEMS_IN_ONE_LINE;
                }
            }
        }
    }

    public class Primes
    {
        const int SMALLEST_PRIME = 2;

        private int[] primes;
        private int countOfFirst;
        private int linesInOnePage;
        private int itemsInOneLine;
        private int countOfThreshold;

        private Primes(int countOfFirst, int linesInOnePage, int itemsInOneLine, int countOfThreshold)
        {
            primes = new int[countOfFirst + 1];
            this.countOfFirst = countOfFirst;
            this.linesInOnePage = linesInOnePage;
            this.itemsInOneLine = itemsInOneLine;
            this.countOfThreshold = countOfThreshold;

            primes[1] = 2;
            int currentIndex = 1;
            while(currentIndex <= countOfFirst){
                currentIndex++;
                primes[currentIndex] = FindNextPrime(currentIndex);
            }
        }

        private int FindNextPrime(int index){
            int currentNumber;
            int currentTraversalIndex;
            int currentThreshold;
            int currentThresholdIndex;
            int[] thresholds = new int[countOfThreshold + 1];
            bool foundPrime;
            bool hasNext;

            currentNumber = primes[index];
            currentTraversalIndex = 2;
            currentThreshold = 9;
            do
            {
                currentNumber = currentNumber + SMALLEST_PRIME;
                if (currentNumber == currentThreshold)
                {
                    currentTraversalIndex = currentTraversalIndex + 1;
                    currentThreshold = primes[currentTraversalIndex] * primes[currentTraversalIndex];
                    thresholds[currentTraversalIndex - 1] = currentNumber;
                }
                currentThresholdIndex = 2;
                foundPrime = true;
                hasNext = currentThresholdIndex < currentTraversalIndex && foundPrime;
                while (hasNext)
                {
                    while (thresholds[currentThresholdIndex] < currentNumber)
                    {
                        thresholds[currentThresholdIndex] = thresholds[currentThresholdIndex] + primes[currentThresholdIndex] + primes[currentThresholdIndex];
                    }
                    if (thresholds[currentThresholdIndex] == currentNumber)
                    {
                        foundPrime = false;
                    }
                    currentThresholdIndex = currentThresholdIndex + 1;
                    hasNext = currentThresholdIndex < currentTraversalIndex && foundPrime;
                }
            } while (!foundPrime);
            return currentNumber;
        }

        public static Primes GeneratePrimesWithDefaultFormat(int countOfFirst){
            return new Primes(countOfFirst, 50, 4, 30);
        }

        public void Print()
        {
            int currentPageNumber = 1;
            int currentOriginForPage = 1;
            
            while (currentOriginForPage <= countOfFirst)
            {
                string log = "";
                log += $"The First {countOfFirst} Prime Numbers --- {currentPageNumber}\n";
                log += "\n";
                for (int currentOriginForRow = currentOriginForPage; currentOriginForRow < currentOriginForPage + linesInOnePage; currentOriginForRow++)
                {
                    for (int IndexOfItem = 0; IndexOfItem < itemsInOneLine; IndexOfItem++)
                    {
                        if (currentOriginForRow + IndexOfItem * linesInOnePage <= countOfFirst)
                        {
                            log += $"{primes[currentOriginForRow + IndexOfItem * linesInOnePage], 10}";
                        }
                    }
                    log += "\n";
                }
                log += "\f";
                Debug.Log(log);
                currentPageNumber = currentPageNumber + 1;
                currentOriginForPage = currentOriginForPage + linesInOnePage * itemsInOneLine;
            }
        }
    }
}