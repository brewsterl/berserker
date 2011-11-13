// Berserker: Multi-platform open source game server.
// Copyright (C) 2011 Berserker Group
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace Berserker
{
	public static class Log
	{
        static int startTextLength = 0;
		static int startTime = 0;
		
		public static void WriteBegin(string text)
		{
			string s = String.Format("{0}: {1}", DateTime.Now, text);
			
			Console.Write(s);
			
			startTextLength = s.Length;
			startTime = System.Environment.TickCount;
		}
		
		public static void WriteEnd()
		{
			int elapsed = System.Environment.TickCount - startTime;
			string done = "[done]";
			string doneTime = "";
			ConsoleColor doneColor = ConsoleColor.Green;
			
			if (elapsed < 1000)
			{
				doneTime = String.Format("({0} ms)", elapsed);
			}
			else
			{
				doneTime = String.Format("({0:0.00} s)", elapsed / 1000.0);
			}
			
			Console.Write(Log.RepeatString(" ", Console.WindowWidth - startTextLength - done.Length - 12));
			Console.ForegroundColor = doneColor;
			Console.Write(done);
			Console.ResetColor();
			Console.Write(Log.RepeatString(" ", 11 - doneTime.Length));
			Console.Write(doneTime);
			Console.WriteLine();
		}
		
		public static void WriteError(string errorText)
		{
			string error = "[error]";
			ConsoleColor errorColor = ConsoleColor.Red;
			
			Console.Write(Log.RepeatString(" ", Console.WindowWidth - startTextLength - error.Length - 12));
			Console.ForegroundColor = errorColor;
			Console.WriteLine(error);
			Console.ResetColor();
			Console.WriteLine(errorText);
		}
		
		public static void WriteDebug(string debugText)
		{
			ConsoleColor debugColor = ConsoleColor.Cyan;
			
			Console.Write(DateTime.Now + ": ");
			Console.ForegroundColor = debugColor;
			Console.WriteLine("[Debug] " + debugText);
			Console.ResetColor();
			
			/*string s = String.Format("{0}: {1}", DateTime.Now, debugText);
			ConsoleColor debugColor = ConsoleColor.Cyan;
			
			Console.ForegroundColor = debugColor;
			Console.WriteLine(s);
			Console.ResetColor();*/
			
			/*string s = String.Format("{0}: {1}", DateTime.Now, debugText);
			string debug = "[debug]";
			ConsoleColor debugColor = ConsoleColor.Cyan;
			
			Console.Write(s);
			Console.Write(Log.RepeatString(" ", Console.WindowWidth - s.Length - debug.Length - 12));
			Console.ForegroundColor = debugColor;
			Console.WriteLine(debug);
			Console.ResetColor();*/
		}
		
		public static void Write(string text, params object[] args)
		{
			Console.Write("{0}: {1}", DateTime.Now, String.Format(text, args));
		}
		
		public static void WriteLine(string text, params object[] args)
		{
			Console.WriteLine("{0}: {1}", DateTime.Now, String.Format(text, args));
		}
		
		private static string RepeatString(string toRepeat, int count)
		{
			try
			{
				return string.Join(toRepeat, new string[count + 1]);
			}
			catch
			{
				return toRepeat;
			}
		}
	}
}
