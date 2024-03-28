using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public static class Extensions
    {
        public static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;
            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;
            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];
                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }
    
        public static T[][][] ToJaggedArray<T>(this T[][,] threeDimensionalArray)
        {
            int rowsFirstIndex = threeDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = threeDimensionalArray.GetUpperBound(0);
            int arraySize = rowsLastIndex - rowsFirstIndex + 1;
            T[][][] result = new T[arraySize][][];
            for (int i = 0; i < arraySize; i++)
            {
                result[i] = ToJaggedArray(threeDimensionalArray[i]);
            }
            return result;
        }
    
        public static T[,] ToMultidimensionalArray<T>(this T[][] jaggedArray)
        {
            T[,] multidimensionalArray = new T[jaggedArray.Length, jaggedArray[0].Length];
            try
            {
                for (int i = 0; i < jaggedArray.Length; i++)
                {
                    for (int j = 0; j < jaggedArray[0].Length; j++)
                    {
                        multidimensionalArray[i,j] = jaggedArray[i][j];
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception(ex.Message);
            }
            return multidimensionalArray;
        }

        public static T[][,] ToMultidimensionalArray<T>(this T[][][] jaggedArray)
        {
            T[][,] multidimensionalArray = new T[jaggedArray.Length][,];
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                multidimensionalArray[i] = ToMultidimensionalArray(jaggedArray[i]);
            }
            return multidimensionalArray;
        }
    }
}
