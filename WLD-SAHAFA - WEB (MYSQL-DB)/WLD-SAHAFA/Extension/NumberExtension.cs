using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WLD_SAHAFA.Extension
{
    public static class NumberExtension
    {
        private static readonly string[] digitWordsEnglish = { "zero.", "one.", "two.", "three.", "four.", "five.", "six.", "seven.", "eight.", "nine." };
        private static readonly string[] digitWordsArabic = { "صفر.", "وا حد.", "إثنان.", "ثلاثة.", "	أربعة.", "خمسة.", "	ستّة.", "سبعة.", "ثامنية.", "تعسة." };

        public static string ConvertNumberToWordsEnglish(this int number)
        {
            if (number == 0)
            {
                return "";
            }

            int digit = number % 10;
            string words = ConvertNumberToWordsEnglish(number / 10);
            return words + " " + digitWordsEnglish[digit];
        }
        public static string ConvertNumberToWordsArabic(this int number)
        {
            if (number == 0)
            {
                return "";
            }

            int digit = number % 10;
            string words = ConvertNumberToWordsArabic(number / 10);
            return words + " " + digitWordsArabic[digit];
        }
    }
}