using System;
using System.Collections.Generic;
using System.Collections;
using LCSExercise.Model;

namespace LCSExercise.LCSLogic
{
    /// <summary>
    /// Class to find LCS form the given set of strings
    /// </summary>
    class LCS
    {
        private string[] values;
        private int shortestStringIndex;
        
        public string[] Values
        {
            get { return values; }
            set
            {
                values = value;
                AssignShortestStringIndex();
            }
        }
        /// <summary>
        /// This algorithm works at O(n*m2) time complexity
        /// Logic - One by one consider all substrings of shortest string and for every substring check if it is a substring/whole string on the list of strings other than the shortest string
        /// </summary>
        /// <returns></returns>
        public LCSResponse FindLCS()
        {
            bool isFound = false;
            var lcsResponse = new LCSResponse();
            lcsResponse.lcs = new List<Value>();
            var notMatchList = new ArrayList();
            var lcsList = new ArrayList();
            //Take the shortest string from the list.
            string shortestString = values[shortestStringIndex];
            //Take the length from the shortest string and check the string list whether if the every substring is found or not. If not found, decrease the length by 1 and check again to the string list. 
            for (int targetStringLength = shortestString.Length; targetStringLength >= 0; targetStringLength--)
            {
                //Check all the combinations for the shortest string length.
                for (int startIndex = 0; (startIndex + targetStringLength) <= shortestString.Length; startIndex++)
                {
                    string currentSubString = shortestString.Substring(startIndex, targetStringLength);
                    if ((!lcsList.Contains(currentSubString)) && (!notMatchList.Contains(currentSubString) && !string.IsNullOrWhiteSpace(currentSubString)))
                    {
                        if (IsSubstringFoundInTheList(currentSubString))
                        {
                            isFound = true;
                            if(currentSubString.Length > 1)
                                lcsList.Add(currentSubString);
                        }
                        else
                        {
                            notMatchList.Add(currentSubString);
                        }
                    }
                }
                //If string is found in the string list then we no need to check again by decrease its length, because we found maximum length of the string.
                if (isFound)
                {
                    break;
                }
            }

            if (lcsList.Count == 0)
                lcsList.Add("No LCS Found");

            foreach (var lcsItem in lcsList)
            {
                var value = new Value();
                value.value = lcsItem.ToString();
                lcsResponse.lcs.Add(value);
            }
            return lcsResponse;
        }

        /// <summary>
        /// Check if  given substring found in the string list(either as whole string or substring)
        /// </summary>
        /// <param name="subString"></param>
        /// <returns></returns>
        private bool IsSubstringFoundInTheList(string subString)
        {
            bool isFound = true;
            for (int index = 0; index < values.Length; index++)
            {
                if (index != shortestStringIndex)
                {
                    if (values[index].IndexOf(subString) < 0)
                    {
                        isFound = false;
                        break;
                    }
                }
            }
            return isFound;
        }
        /// <summary>
        /// Find shortest string index from the string list.
        /// </summary>
        private void AssignShortestStringIndex()
        {
            shortestStringIndex = 0;
            for (int index = 1; index < values.Length; index++)
            {
                if (values[index].Length < values[shortestStringIndex].Length)
                {
                    shortestStringIndex = index;
                }
            }
        }
        /// <summary>
        /// This alogorithm works at O(n2) time complexity
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        public static string FindLongestCommonSubstring(char[] str1, char[] str2)
        {
            int[,] l = new int[str1.Length, str2.Length];
            int lcs = -1;
            string lcsStr = string.Empty;
            int end = -1;

            for (int i = 0; i < str1.Length; i++)
            {
                for (int j = 0; j < str2.Length; j++)
                {
                    if (str1[i] == str2[j])
                    {
                        if (i == 0 || j == 0)
                        {
                            l[i, j] = 1; // this should be always 1 as there's no previous value to look from 0th position
                        }
                        else
                            l[i, j] = l[i - 1, j - 1] + 1; //sum up the values diagonlly to know the longest substring 
                        if (l[i, j] > lcs)
                        {
                            lcs = l[i, j]; //to get the length of the LCS and will help to identify the start position of the substring
                            end = i; //to know the end position of the substring from the first given string, Note -  the substring can't go beyond this position
                        }
                    }
                    else
                        l[i, j] = 0;
                }
            }
            //end-lcs+1 will give the start position of the substring
            for (int i = end - lcs + 1; i <= end; i++)
            {
                lcsStr += str1[i];
            }

            return "LCS String is  "+ lcsStr.ToUpper();
        }
    }
}
