using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder
{
    public static class StringExtensions
    {
        /// <summary>
        ///  Detects whether two words are just one mutation away from 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsAdjacent(this string first, string second)
        {
            if (first.Length > second.Length) {
                (first, second) = (second, first);
            }

            if (second.Length - first.Length > 1) return false;
            if (first == second) return false;
            if (first == "") return second.Length == 1;

            bool hasDifferences = false;

            if (second.Length == first.Length) {
                for (int i = 0; i < first.Length; i++) {
                    if (first[i] != second[i]) {
                        if (hasDifferences) return false;
                        hasDifferences = true;
                    }
                }

                return hasDifferences;
            }

            for (int i = 0, j = 0; i < first.Length && j < second.Length; i++, j++) {
                if (first[i] != second[j]) {
                    if (hasDifferences) return false;
                    i--;
                    hasDifferences = true;
                }
            }

            return true;
        }


        public static int Levenshtein(this string a, string b)
        {
            int[,] d = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++) d[i, 0] = i;
            for (int i = 0; i <= b.Length; i++) d[0, i] = i;

            for (int i = 1; i <= a.Length; i++)
            for (int j = 1; j <= b.Length; j++) {
                var substitutionCost = a[i - 1] == b[j - 1] ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + substitutionCost);
            }

            return d[a.Length, b.Length];
        }
    }
}
