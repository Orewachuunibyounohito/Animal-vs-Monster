using UnityEngine;

namespace Refactoring.PrintPrimes.CleanCode
{
    public class PrimesPrinter : MonoBehaviour
    {
        const int NUMBER_OF_PRIMES = 1000;
        int[] primes;

        const int ROWS_PER_PAGE = 50;
        const int COLUMNS_PER_PAGE = 4;

        RowColumnPagePrinter tablePrinter;


        private void Awake(){
            primes = PrimeGenerator.Generate(NUMBER_OF_PRIMES);
            // primes = new PrimeGenerator_NonStatic().Generate(NUMBER_OF_PRIMES);
            tablePrinter = 
                new RowColumnPagePrinter(ROWS_PER_PAGE,
                                         COLUMNS_PER_PAGE,
                                         $"The First {NUMBER_OF_PRIMES} Prime Numbers");
        }
        private void Start(){
            tablePrinter.Print(primes);
        }
    }
}