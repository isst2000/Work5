using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace LevenshteinLibrary
{
    public static class LevenshteinDistance
    {
        /// <summary>
        /// Damerau–Levenshtein calculating class
        /// </summary>
        public static int Distance(string P1, string P2)
        {
            if ((P1 == null) || (P2 == null)) 
                return -1; //in this case we will return -1
            int L1 = P1.Length;
            int L2 = P2.Length;
            if ((L1 == 0) && (L2 == 0)) //if both zero-lenghted, distance is 0
                return 0;
            if (L1 == 0) //if first lenght equals 0 then distance equals L2
                return L2;
            if (L2 == 0) //if second lenght equals 0 then distance equals L1
                return L1; 
            string UP1 = P1.ToUpper(); //upping cases of string 1
            string UP2 = P2.ToUpper(); //upping cases of string 1

            int[,] matrix = new int[L1 + 1, L2 + 1];
            for (int i = 0; i <= L1; i++) //zero-row init
                matrix[i, 0] = i;
            for (int j = 0; j <= L2; j++) //zero-col init
                matrix[0, j] = j;

            for (int i = 1; i <= L1; i++) //encalculating the distance
                for (int j = 1; j <= L2; j++)
                    {
                        int CharEqual = ((UP1.Substring(i - 1, 1) == UP2.Substring(j - 1, 1)) ? 0 : 1);
                        int InsertValue = matrix[i, j - 1] + 1; //adding
                        int DeleteValue = matrix[i - 1, j] + 1; //deleting
                        int subst = matrix[i - 1, j - 1] + CharEqual; //replacing
                        matrix[i, j] = Math.Min(Math.Min(InsertValue, DeleteValue), subst); //encalculating current item og the matrix
                        if ((i > 1) && (j > 1) && (UP1.Substring(i - 1, 1) == UP2.Substring(j - 2, 1)) &&
                            (UP1.Substring(i - 2, 1) == UP2.Substring(j - 1, 1)))
                            matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + CharEqual); //Damerau addition
                    }

            return matrix[L1, L2]; //result equals down-right item of the matrix
        }
    }
}
