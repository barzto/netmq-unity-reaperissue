using System;

namespace Base.Utils
{
    public class GameDebug
    {
		public static void PrintToFile(string filePath, string fmt, params object[] args)
		{
			string msg = string.Format(fmt, args);
			using (System.IO.StreamWriter w = new System.IO.StreamWriter(filePath, true))
			{
				w.WriteLine(msg);
			}
		}
		
		private static string _logFilePath;
		public static void Init()
		{
			_logFilePath = UnityEngine.Application.dataPath + "/../Game.log";
		}
		
		private static void WriteLine(string msg)
    	{
    		UnityEngine.Debug.Log(msg);
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
