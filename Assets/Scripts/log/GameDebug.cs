using System;

namespace Base.Utils
{
    public class GameDebug
    {
    	private static object _fileLock = new object();
    	
		public static void PrintToFile(string filePath, string fmt, params object[] args)
		{
			lock(_fileLock)
			{
				string msg = string.Format(fmt, args);
				using (System.IO.StreamWriter w = new System.IO.StreamWriter(filePath, true))
				{
					w.WriteLine(msg);
				}
			}
		}
		
		private static string _logFilePath = "./Game.log";
#if UNITY_EDITOR
		public static void Init()
		{
			_logFilePath = UnityEngine.Application.dataPath + "/../Game.log";
		}
#endif
		
		private static void WriteLine(string msg)
    	{
#if UNITY_EDITOR
    		UnityEngine.Debug.Log(msg);
#else
			Console.WriteLine(msg);
#endif
    		PrintToFile(_logFilePath, msg);
    		
    	}
    	
    	
		public static void Log(string msg)
		{
		    WriteLine(msg);
		}
		
		public static void LogFormat(string fmt, params object[] args)
		{
		    WriteLine(string.Format(fmt, args));
		}
    }
}
