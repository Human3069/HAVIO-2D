using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace _KMH_Framework
{
    public static class LogUtility 
    {
        #region LogFormat
        [HideInCallstack]
        public static void LogFormat<T>(this T caller, string log) where T : class
        {
            string formattedName = caller.GetType().GetFormattedTypeName(Color.white);
            Debug.LogFormat(formattedName, log);
        }

        [HideInCallstack]
        public static void LogFormat<T>(this T caller, string log, Color formatColor) where T : class
        {
            string formattedName = caller.GetType().GetFormattedTypeName(formatColor);
            Debug.LogFormat(formattedName, log);
        }

        [HideInCallstack]
        public static void LogFormat(this string log, [CallerFilePath] string callerFilePath = "")
        {
            string formattedName = callerFilePath.GetFormattedCallerFilePath(Color.white);
            Debug.LogFormat(formattedName, log);
        }

        [HideInCallstack]
        public static void LogFormat(this string log, Color formatColor, [CallerFilePath] string callerFilePath = "")
        {
            string formattedName = callerFilePath.GetFormattedCallerFilePath(formatColor);
            Debug.LogFormat(formattedName, log);
        }
        #endregion

        #region LogWarningFormat
        [HideInCallstack]
        public static void LogWarningFormat<T>(this T caller, string log) where T : class
        {
            string formattedName = caller.GetType().GetFormattedTypeName(Color.white);
            Debug.LogWarningFormat(formattedName, log);
        }

        [HideInCallstack]
        public static void LogWarningFormat<T>(this T caller, string log, Color formatColor) where T : class
        {
            string formattedName = caller.GetType().GetFormattedTypeName(formatColor);
            Debug.LogWarningFormat(formattedName, log);
        }

        [HideInCallstack]
        public static void LogWarningFormat(this string log, [CallerFilePath] string callerFilePath = "")
        {
            string formattedName = callerFilePath.GetFormattedCallerFilePath(Color.white);
            Debug.LogWarningFormat(formattedName, log);
        }

        [HideInCallstack]
        public static void LogWarningFormat(this string log, Color formatColor, [CallerFilePath] string callerFilePath = "")
        {
            string formattedName = callerFilePath.GetFormattedCallerFilePath(formatColor);
            Debug.LogWarningFormat(formattedName, log);
        }
        #endregion

        #region LogErrorFormat
        [HideInCallstack]
        public static void LogErrorFormat<T>(this T caller, string log) where T : class
        {
            string formattedName = caller.GetType().GetFormattedTypeName(Color.white);
            Debug.LogErrorFormat(formattedName, log);
        }

        [HideInCallstack]
        public static void LogErrorFormat<T>(this T caller, string log, Color formatColor) where T : class
        {
            string formattedName = caller.GetType().GetFormattedTypeName(formatColor);
            Debug.LogErrorFormat(formattedName, log);
        }

        [HideInCallstack]
        public static void LogErrorFormat(this string log, [CallerFilePath] string callerFilePath = "")
        {
            string formattedName = callerFilePath.GetFormattedCallerFilePath(Color.white);
            Debug.LogErrorFormat(formattedName, log);
        }

        [HideInCallstack]
        public static void LogErrorFormat(this string log, Color formatColor, [CallerFilePath] string callerFilePath = "")
        {
            string formattedName = callerFilePath.GetFormattedCallerFilePath(formatColor);
            Debug.LogErrorFormat(formattedName, log);
        }
        #endregion

        #region  LogWithTrack
        [HideInCallstack]
        public static void LogFormatWithTrack(this string log, UnityEngine.Object obj, [CallerFilePath] string callerFilePath = "")
        {
            string formattedName = callerFilePath.GetFormattedCallerFilePath(Color.white);
            Debug.LogFormat(obj, formattedName, log);
        }

        [HideInCallstack]
        public static void LogErrorFormatWithTrack(this string log, UnityEngine.Object obj, [CallerFilePath] string callerFilePath = "")
        {
            string formattedName = callerFilePath.GetFormattedCallerFilePath(Color.white);
            Debug.LogErrorFormat(obj, formattedName, log);
        }

        [HideInCallstack]
        public static void LogFormatWithTrack<T>(this T caller, string log, UnityEngine.Object obj) where T : class
        {
            string formattedName = caller.GetType().GetFormattedTypeName(Color.white);
            Debug.LogFormat(obj, formattedName, log);
        }

        [HideInCallstack]
        public static void LogErrorFormatWithTrack<T>(this T caller, string log, UnityEngine.Object obj) where T : class
        {
            string formattedName = caller.GetType().GetFormattedTypeName(Color.white);
            Debug.LogErrorFormat(obj, formattedName, log);
        }
        #endregion

        /// <summary>
        /// CallerFilePath를 가져와서 포맷팅된 문자열로 변환합니다. 예) "D/Works/Test.cs" -> "<color=#FF0000><b>[Test]</b></color> {0}"
        /// </summary>
        /// <param name="callerFilePath">확장자 포함된 디렉토리</param>
        /// <param name="formatColor">포맷을 감쌀 색상</param>
        /// <returns>포맷화된 CLR 이름</returns>
        [HideInCallstack]
        private static string GetFormattedCallerFilePath(this string callerFilePath, Color formatColor)
        {
            string callerCLRName = Path.GetFileNameWithoutExtension(callerFilePath);
            string hexColor = formatColor.ToHtmlString();
            string formattedName = "<color=#" + hexColor + "><b>[" + callerCLRName + "]</b></color> {0}";

            return formattedName;
        }

        /// <summary>
        /// Type을 가져와서 포맷팅된 문자열로 변환합니다. 예) "System.String" -> "<color=#FF0000><b>[String]</b></color> {0}"
        /// </summary>
        /// <param name="type">타입 변수</param>
        /// <param name="formatColor">포맷을 감쌀 색상</param>
        /// <returns>포맷화된 Type 이름</returns>
        [HideInCallstack]
        private static string GetFormattedTypeName(this Type type, Color formatColor)
        {
            string hexColor = formatColor.ToHtmlString();
            string formattedName = "<color=#" + hexColor + "><b>[" + type.Name + "]</b></color> {0}";

            return formattedName;
        }
    }
}
