using System;
using UnityEngine;

namespace Refactoring.PrintPrimes.CleanCode
{
    public class RowColumnPagePrinter
    {
        private int rowsPerPage;
        private int columnsPerPage;
        private int numbersPerPage;
        private string pageHeader;

        public RowColumnPagePrinter(int rowsPerPage,
                                    int columnsPerPage,
                                    string pageHeader){
            this.rowsPerPage = rowsPerPage;
            this.columnsPerPage = columnsPerPage;
            this.pageHeader = pageHeader;
            numbersPerPage = this.rowsPerPage * this.columnsPerPage;
        }

        public void Print(int[] data){
            string log = "";
            int pageNumber = 1;
            for(int firstIndexOnPage = 0, lastIndexOnPage;
                firstIndexOnPage < data.Length;
                firstIndexOnPage += numbersPerPage){
                lastIndexOnPage = Math.Min(firstIndexOnPage + numbersPerPage - 1, data.Length - 1);
                log += PrintPageHeader(pageHeader, pageNumber);
                log += PrintPage(firstIndexOnPage, lastIndexOnPage, data);
                log += "\f";
                pageNumber++;
            }
            Debug.Log(log);
        }

        private string PrintPage(int firstIndexOnPage,
                                 int lastIndexOnPage,
                                 int[] data){
            string page = "";
            int firstIndexOfLastRowOnPage = firstIndexOnPage + rowsPerPage - 1;
            for(int firstIndexInRow = firstIndexOnPage; firstIndexInRow <= firstIndexOfLastRowOnPage; firstIndexInRow++){
                page += PrintRow(firstIndexInRow, lastIndexOnPage, data);
                page += "\n";
            }
            return page;
        }

        private string PrintRow(int firstIndexInRow, int lastIndexOnPage, int[] data){
            string row = "";
            for(int column = 0, index; column < columnsPerPage; column++){
                index = firstIndexInRow + column * rowsPerPage;
                if(index <= lastIndexOnPage){
                    row += $"{data[index], 10}";
                }
            }
            return row;
        }

        private string PrintPageHeader(string pageHeader, int pageNumber){
            return $"{pageHeader} --- Page {pageNumber}\n";
        }
    }
}