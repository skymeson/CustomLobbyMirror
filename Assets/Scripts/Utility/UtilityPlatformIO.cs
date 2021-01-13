using UnityEngine;
using System.Collections;

namespace MyUtility
{
	public static class UtilityPlatformIO
	{
		public static void SaveToFile(string p_filePath, byte[] p_fileContents)
		{
#if UNITY_METRO && !UNITY_EDITOR
			UnityEngine.Windows.File.WriteAllBytes(p_filePath, p_fileContents);
#else
			using (System.IO.FileStream stream = System.IO.File.Open(p_filePath, System.IO.FileMode.Create))
			{
				stream.Write(p_fileContents, 0, p_fileContents.Length);
			}
#endif
		}

		public static void SaveToFile(string p_filePath, string p_fileContentAsString)
		{
#if UNITY_METRO && !UNITY_EDITOR
			byte[] fileContentAsBytes = System.Text.Encoding.UTF8.GetBytes(p_fileContentAsString);
			UnityEngine.Windows.File.WriteAllBytes(p_filePath, fileContentAsBytes);
#else
			using (System.IO.FileStream stream = System.IO.File.Open(p_filePath, System.IO.FileMode.Create))
			{
				using (System.IO.StreamWriter writer = new System.IO.StreamWriter(stream))
				{
					writer.Write(p_fileContentAsString);
				}
			}
#endif
		}

		public static string FixFilePath(string p_path)
		{
#if UNITY_STANDALONE_WIN || UNITY_METRO
			return "file:///" + p_path;
#else
			return "file://" + p_path;
#endif
		}
	}
}

#if IS_MY_MOBILE
// please ignore this, or delete it if you have a IS_MY_MOBILE directive in your built
// A MESSAGE FOR MYSELF:
... copy paste too much huh?
#endif
