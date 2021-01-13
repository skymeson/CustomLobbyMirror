using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyUtility
{
	public static class UtilityStrings
	{
		private const int MAX_LENGTH = 75;
		public static string CleanUpString(string str)
		{
			return CleanUpString(str, MAX_LENGTH);
		}
		public static string CleanUpString(string str, int max_length)
		{
			string result = Regex.Replace(str, @"[^\w\.@_*+$<>\- ]", "");
			return result.Substring(0, Mathf.Min(max_length, result.Length));
		}

		public static string InsertSpacesIntoCamelCase(string p_camelCaseString)
		{
			return Regex.Replace(
				p_camelCaseString,
				@"(?=[A-Z][a-z][0-9])|(?<=[a-z])(?=[A-Z])|(?<=[a-z])(?=[0-9])",
				" ");
		}
	}
}
