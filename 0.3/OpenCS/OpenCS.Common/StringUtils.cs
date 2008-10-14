using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace OpenCS.Common
{
    /// <summary>
    /// 문자열 관련 유틸 클래스.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Tags for excluing
        /// </summary>
        public static string ExcludeTagsPattern;

        /// <summary>
        /// HTML 태그를 없앤다. 참고 주소: http://www.developerfusion.co.uk/show/3901/
        /// 
        /// </summary>
        /// <param name="src">원본 문자열</param>
        /// <returns>변환된 문자열</returns>
        public static string StripTags(string src)
        {
            Regex re = new Regex("<[^>]*>");
            return re.Replace(src, "");
        }

        /// <summary>
        /// Evaluator for <c>StripTags</c>.
        /// </summary>
        /// <param name="match"><c>Match</c></param>
        /// <returns>Stripped tag string</returns>
        public static string OnMatch(Match match)
        {
            string output = match.Groups[0].Value;

            if (string.IsNullOrEmpty(ExcludeTagsPattern) == false)
            {
                Regex re = new Regex(ExcludeTagsPattern, RegexOptions.IgnoreCase);
                if (re.Matches(output).Count > 0)
                {
                    return output;
                }
            }

            return "";
        }

        /// <summary>
        /// Remove HTML tags with options. See also http://www.developerfusion.co.uk/show/3901/, 
        /// http://forums.asp.net/t/1323604.aspx
        /// </summary>
        /// <param name="src">Source text string</param>
        /// <param name="removeScripts">Remove scripts at first</param>
        /// <param name="excludeTagsPattern">Pattern of exclude tags</param>
        /// <returns>Stripped text strign</returns>
        public static string StripTags(string src, bool removeScripts, string excludeTagsPattern)
        {
            string output = src;

            // remove scripts
            // http://forums.asp.net/t/1323604.aspx
            if (removeScripts == true)
            {
                output = Regex.Replace(output, "<script.*?</script>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                output = Regex.Replace(output, "<style.*?</style>", "", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }

            // Assign the replace method to the MatchEvaluator delegate.
            ExcludeTagsPattern = excludeTagsPattern;
            MatchEvaluator evaluator = new MatchEvaluator(OnMatch);

            Regex re = new Regex("<[^>]*>");
            output = re.Replace(output, evaluator);

            return output;
        }

        /// <summary>
        /// 태그와 태그사이의 문자열을 구한다. 태그를 찾지 못하면 원본 문자열과 동일한 문자열을 반환한다.
        /// </summary>
        /// <param name="src">원본 문자열</param>
        /// <param name="startTag">시작 태그</param>
        /// <param name="endTag">종료 태그</param>
        /// <returns>사이의 문자열</returns>
        public static string GrabString(string src, string startTag, string endTag)
        {
            string result = src;

            int startIdx = src.IndexOf(startTag);
            if (startIdx > -1)
            {
                int startLen = startTag.Length;
                int endIdx = src.IndexOf(endTag, startIdx);
                if (endIdx > -1)
                {
                    int len = src.Length;
                    result = src.Substring(startIdx + startLen, endIdx - startIdx - startLen);
                }
            }

            return result;
        }

        /// <summary>
        /// 현재 시간에 대한 타임스탬프 문자열을 반환한다. 형식은 "[yyyy/MM/dd HH:mm:ss]".
        /// ref: http://en.csharp-online.net/CSharp_Format_Specifiers%E2%80%94DateTime_Format_Specifiers
        /// </summary>
        /// <returns>타임스탬프 문자열</returns>
        public static string GetTimeStamp()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 현재 시간에 대한 틱값과 함께 타임스탬프 문자열을 반환한다. 형식은 "[yyyy/MM/dd HH:mm:ss 633471568757530000]".
        /// ref: http://en.csharp-online.net/CSharp_Format_Specifiers%E2%80%94DateTime_Format_Specifiers
        /// </summary>
        /// <returns>타임스탬프 문자열</returns>
        public static string GetTimeStampWithTicks()
        {
            DateTime now = DateTime.Now;

            return string.Format(CultureInfo.CurrentCulture, "[{0} {1}]", now.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.CurrentCulture), now.Ticks);
        }

    }
}
