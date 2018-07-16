using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DillenManagementStudio
{
    //this class and methods are enabled for every string (because it's static and because of the parameter "this string str")
    //to use it: "str.method(other parameters)" or "method(str, other parameters)"
    public static class StringAndCharExtensions
    {
        ///MY STRING'S METHODS
        //method returns the index of the first ministr in the str (case insensitive and multiple spaces)
        //iArrayNumber: which position of the ministr list
        //lastLetter: the last letter of the ministr
        //to show multiple spaces in a string put "%"
        public static int IndexOfFirstMinistrListMultSpacesCI(this string str, List<string> ministrs, int startIndex, ref int iArrayNumber, ref int lastLetter)
        {
            //function will return the index of the first letter of the string that was been search
                //example: " hi, how           are you? ".IndexOfSupreme("how%are") => 5
                //          012345678901234567
            //lastLetter: it will return the index of the last letter of the string
                //example: " hi, how           are you? ".IndexOfSupreme("how%are", lastLetter) => lastLetter = 21
                //          0123456789012345678901234567

            int ret = str.Length;
            iArrayNumber = -1;

            for (int i = 0; i < ministrs.Count; i++)
            {
                int currentIndex;
                int currentLastIndex = -1;

                int indexPerc = ministrs[i].IndexOf("%");
                if (indexPerc < 0)
                {
                    currentIndex = str.IndexOfEvenSingQuotMarksAndNothingBefore(ministrs[i], startIndex);
                    if (currentIndex >= 0)
                        currentLastIndex = currentIndex + ministrs[i].Length;
                }
                else
                    currentIndex = str.IndexOfWithNWhiteSpaces(ministrs[i].Substring(0, indexPerc), ministrs[i].Substring(indexPerc + 1), startIndex, ref currentLastIndex);

                if (currentIndex >= 0 && currentIndex < ret)
                {
                    ret = currentIndex;
                    lastLetter = currentLastIndex;
                    iArrayNumber = i;
                }
            }

            if (ret == str.Length)
                return -1;
            return ret;
        }

        //method returns the index of a subtring: "ministr1 + any spaces + ministr2" (case insensitive)
        //lastLetter: the last letter of the ministr2
        //str.indexOf(ministr1%ministr2);
        public static int IndexOfWithNWhiteSpaces(this string str, string ministr1, string ministr2, int startIndex, ref int lastLetter)
        {
            int indexOf1 = str.IndexOfEvenSingQuotMarksAndNothingBefore(ministr1, startIndex);

            if (indexOf1 < 0)
                return indexOf1;

            int indexOf2 = str.IndexOf(ministr2, indexOf1 + ministr1.Length + 1, StringComparison.CurrentCultureIgnoreCase);

            if (indexOf2 < 0)
                return indexOf2;

            for (int i = indexOf1 + ministr1.Length; i < indexOf2; i++)
                if (str[i] != ' ')
                    return -1;

            lastLetter = indexOf2 + ministr2.Length;
            return indexOf1;
        }

        //method returns the index of a substring from a startIndex if there are even number of single quotation marks before the substring (case insensitive)
        //obs: if there's an odd number of single quotation marks before the substring, it returns -1
        public static int IndexOfEvenSingQuotMarksAndNothingBefore(this string str, string ministr, int startIndex)
        {
            //indexOf MINISTR
            int ret = str.IndexOf(ministr, startIndex, StringComparison.CurrentCultureIgnoreCase);
            if (ret < 0)
            {
                //if the last caracter of the ministr is an space, search without it
                if (ministr[ministr.Length - 1] != ' ')
                    return -1;
                
                ret = str.IndexOf(ministr.Substring(0, ministr.Length-1), startIndex, StringComparison.CurrentCultureIgnoreCase);
                //if the ministr without the space is in the last part of the string, it continues the function, else it returns -1
                if (ret < 0 || ret + ministr.Length < str.Length)
                    return -1;
            }

            //indexOf @
            if (ret > 0 //if ret == 0, thre's nothing before
                && str[ret - 1] != ' ')
                //if there's a @ right before it, it's a variable 
                //(example: "@proc_sp", "sproc_sp")
                return -1;

            //idexOf '
            int currentIndex = startIndex - 1;
            int qtdSingQuotMarks = 0;
            while (true)
            {
                currentIndex = str.IndexOf("'", currentIndex + 1);
                if (currentIndex >= 0 && currentIndex < ret)
                    qtdSingQuotMarks++;
                else
                    break;
            }

            if (qtdSingQuotMarks % 2 != 0)
                return -1;
            
            //FINALLY
            return ret;
        }

        //method returns the index of the first ministr in the str (case insensitive)
        //iArrayNumber: which position of the ministr list
        //lastLetter: the last letter of the ministr
        public static int IndexOfFirstMinistrListCI(this string str, List<string> ministrs, int startIndex, ref int iArrayNumber, ref int lastLetter)
        {
            //function will return the index of the first letter of the string that was been search
                //example: " hi, how are you? ".IndexOfSupreme("how are") => 5
                //          012345678901234567
            //lastLetter: it will return the index of the last letter of the string
                //example: " hi, how are you? ".IndexOfSupreme("how are", lastLetter) => lastLetter = 12
                //          012345678901234567

            int ret = str.Length;
            iArrayNumber = -1;

            for (int i = 0; i < ministrs.Count; i++)
            {
                int currentIndex = str.IndexOfEvenSingQuotMarksAndNothingBefore(ministrs[i], startIndex);
                
                if (currentIndex >= 0 && currentIndex < ret)
                {
                    ret = currentIndex;
                    lastLetter = currentIndex + ministrs[i].Length; 
                    iArrayNumber = i;
                }
            }

            if (ret == str.Length)
                return -1;
            return ret;
        }

        // method returns the index of the first letter different in he strings
        //
        public static int IndexDiferent(this string str, string otherStr)
        {
            string str1; //smaller
            string str2; //larger
            if(otherStr.Length < str.Length)
            {
                str1 = otherStr;
                str2 = str;
            }else
            {
                str1 = str;
                str2 = otherStr;
            }

            for (int i = 0; i < str1.Length; i++)
                if (str1[i] != str2[i])
                    return i;

            if (str.Length == otherStr.Length)
                //nothing different
                return -1;
            else
                return str1.Length;
        }


        ///indexOf List<string> simple
        //method returns the index of the first ministr in the str (case insensitive)
        //iArrayNumber: which position of the ministr list
        public static int IndexOf(this string str, List<string> ministrs, int startIndex, ref int iArrayNumber)
        {
            int ret = str.Length;
            iArrayNumber = -1;

            for (int i = 0; i < ministrs.Count; i++)
            {
                int currentIndex = str.IndexOf(ministrs[i], startIndex);

                if (currentIndex >= 0 && currentIndex < ret)
                {
                    ret = currentIndex;
                    iArrayNumber = i;
                }
            }

            if (ret == str.Length)
                return -1;
            return ret;
        }

        ///indexOf and LastIndexOf List<char>
        // method returns de first indexOf of the list of char from a startIndex
        //
        public static int IndexOf(this string str, List<char> chars, int startIndex)
        {
            return str.IndexOf(chars, startIndex, str.Length);
        }

        // method returns de first indexOf of the list of char from a startIndex to an endIndex
        //
        public static int IndexOf(this string str, List<char> chars, int startIndex, int endIndex)
        {
            int ret = str.Length;

            for (int i = 0; i < chars.Count; i++)
            {
                int currentIndex = str.IndexOf(chars[i], startIndex, endIndex - startIndex);

                if (currentIndex >= 0 && currentIndex < ret)
                    ret = currentIndex;
            }

            if (ret == str.Length)
                return -1;
            return ret;
        }

        // method returns de lastIndexOf of the list of char
        //
        public static int LastIndexOf(this string str, List<char> chars, int endIndex)
        {
            int ret = -1;

            for (int i = 0; i < chars.Count; i++)
            {
                int currentIndex = str.LastIndexOf(chars[i], endIndex);

                if (currentIndex > ret)
                    ret = currentIndex;
            }
            
            return ret;
        }


        ///methods COUNT APPEARANCES
        //method returns the number of appearances of some char from an index to another
        //
        public static int CountAppearances(this string str, char c, int startIndex, int endIndex)
        {
            int index = startIndex;
            int ret = -1;
            while (index >= 0) //adds 1 each time so index can't be less than 0 (0 is when there's no more)
            {
                index = str.IndexOf(c, startIndex, endIndex - startIndex);
                startIndex = index + 1;
                ret++;
            }

            return ret;
        }

        //method returns the number of appearances of some char from an index
        //
        public static int CountAppearances(this string str, char c, int startIndex)
        {
            return str.CountAppearances(c, startIndex, str.Length);
        }

        //method returns the number of appearances of some char
        //
        public static int CountAppearances(this string str, char c)
        {
            return str.CountAppearances(c, 0);
        }


        ///First Word after a string
        // method returns the first word after an string
        //
        public static string FirstWord(this string str, string ministr)
        {
            int index = str.IndexOf(ministr);
            if (index < 0)
                throw new Exception("The searchen string doesn't exist in this string!");
            return str.FirstWord(index + ministr.Length);
        }

        // method returns the first word after an index
        //
        public static string FirstWord(this string str, int index)
        {
            if (index < 0 || index >= str.Length)
                throw new Exception("Invalid index!");

            try
            {
                while (str[index] == ' ')
                    index++;
            }catch(Exception e)
            {
                throw new Exception("There's no more words after this index!");
            }

            List<char> lis = new List<char>();
            lis.Add(' ');
            lis.Add('(');

            int final = str.IndexOf(lis, index);
            if (final < 0)
                final = str.Length;
                
            return str.Substring(index, final - index);
        }


        ///word-break
        // method returns the string with enters based on the numbers of characters allowed in each line
        //
        public static string BreakWords(this string str, int qtdMaxCharsPerLine)
        {
            str += " ";
            int iCurrSpace = 0;
            int iLastSpace = 0;

            for (; ; )
            {
                int iEspaco = -1;

                while (iEspaco - iCurrSpace <= qtdMaxCharsPerLine)
                {
                    iLastSpace = iEspaco;
                    iEspaco = str.IndexOf(" ", iLastSpace + 1);

                    if (iEspaco < 0)
                    {
                        iLastSpace = iEspaco;
                        break;
                    }
                }

                if (iLastSpace == str.Length - 1 || iLastSpace < 0)
                    break;

                str = str.Insert(iLastSpace + 1, "\n");
                iCurrSpace = iLastSpace + 2;
            }

            return str;
        }


        ///CHAR EXTENSIONS
        public static int EqualsList(this char c, List<char> list)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (c == list[i])
                    return i;
            }

            return -1;
        }
    }
}

/*
         * not using
        public static int IndexOfStringArray(this string str, string[] strArray, int startIndex)
        {
            int ret = -1;

            foreach (string searchStr in strArray)
            {
                int indexStr = str.IndexOf(searchStr, startIndex, StringComparison.CurrentCultureIgnoreCase);
                if (indexStr < ret)
                    ret = indexStr;
            }

            return ret;
        }

        private static int IndexOfStringArrayWithNWhiteSpaces(this string str, string[] ministr1, string[] ministr2, int startIndex)
        {
            int ret = -1;

            for (int i = 0; i < ministr1.Length; i++)
            {
                int indexOf1 = str.IndexOf(ministr1[i], startIndex, StringComparison.CurrentCultureIgnoreCase);

                if (indexOf1 >= 0)
                {
                    int indexOf2 = str.IndexOf(ministr2[i], indexOf1 + ministr1.Length + 2, StringComparison.CurrentCultureIgnoreCase);

                    for (int iAux = indexOf1; iAux < indexOf2; iAux++)
                        if (str[iAux] != ' ')
                        {
                            indexOf2 = -1;
                            break;
                        }

                    if (indexOf2 >= 0 && indexOf1 < ret)
                        ret = indexOf1;
                }
            }

            return ret;
        } */
