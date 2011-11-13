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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.CSharp;
using System.Reflection;

namespace Berserker
{
	/// <summary>
	/// This class allows compiling code from text and using it in
	/// the application. 
	/// </summary>
	public class Compiler
	{
		private const string AssemblyEnding = ".asm";
		private const string AssemblyDirectory = "asm";
		
		private delegate Object CompileDelegate(Object args);
		
		public Compiler()
		{
        }
		
		private string GetHeader()
		{
			StringBuilder header = new StringBuilder();
			
			header.Append("using System; ");
			header.Append("using System.Collections.Generic; ");
			header.Append("using System.Text; ");
			header.Append("using Berserker; ");
			header.Append("namespace Compiled { ");
			header.Append("	class Code { ");
			header.Append("		static public object Get(Object arg) { ");
			
			return header.ToString();
		}
		
		private string GetFooter()
		{
			return "} } }";
		}
		
		public Object CompileCode(string rawCode, Object args, string path)
		{
			if (path.EndsWith(AssemblyEnding))
				return null;
			
			path = Path.GetFullPath(path);
			
			string name = Path.GetFileName(path);
			string directory = Path.GetDirectoryName(path);
			string asmDirectory = directory + Path.DirectorySeparatorChar + AssemblyDirectory;
			string asmEquivalent = asmDirectory + Path.DirectorySeparatorChar + name + AssemblyEnding;
			
			if (!Directory.Exists(asmDirectory))
				Directory.CreateDirectory(asmDirectory);
			
			Module module = null;
			
			if (File.Exists(asmEquivalent))
			{
				module = Assembly.LoadFile(asmEquivalent).GetModules()[0];
			}
			else
			{
				CompilerParameters compilerParams = new CompilerParameters();
				
				string outputDirectory = Directory.GetCurrentDirectory();
				string code = GetHeader();
				code += rawCode;
				code += GetFooter();
				
				compilerParams.GenerateInMemory = true;
				compilerParams.TreatWarningsAsErrors = false;
				compilerParams.GenerateExecutable = false;
				compilerParams.IncludeDebugInformation = false;
				compilerParams.CompilerOptions = "/optimize";
				compilerParams.OutputAssembly = asmEquivalent;
				
				// Add references
				compilerParams.ReferencedAssemblies.Add("System.dll");
				compilerParams.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
				
				CSharpCodeProvider codeProvider = new CSharpCodeProvider();
				CompilerResults compile = codeProvider.CompileAssemblyFromSource(compilerParams, new string[] { code });
				
				if (compile.Errors.HasErrors)
				{
					string errorText = "Compile error: ";
					
					foreach (CompilerError ce in compile.Errors)
						errorText += "Error: " + ce.ToString() + "\n";
					
					Log.WriteLine(errorText);
					
					throw new Exception(errorText);
				}
				
				module = compile.CompiledAssembly.GetModules()[0];
			}
			
			Type methodType = null;
			MethodInfo methodInfo = null;
			
			if (module != null)
				methodType = module.GetType("Compiled.Code");
			
			if (methodType != null)
				methodInfo = methodType.GetMethod("Get");
			
			if (methodInfo != null)
				return methodInfo.Invoke(null, new object[] { args });
			
			return null;
		}
		
		/// <summary>
		/// Given a path to a file, this method returns an Object compiled from that file.
		/// </summary>
		/// <param name='path'>
		/// The path and name of the file.
		/// </param>
		/// <param name='args'>
		/// Any arguments supplied to the file.
		/// </param>
		public Object Compile(string path, Object args)
		{
			TextReader reader = new StreamReader(path);
			
			return CompileCode(reader.ReadToEnd(), args, path);
		}
	}
}