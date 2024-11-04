using dnlib.DotNet.Emit;
using dnlib.DotNet;
using System.Linq;
using System;
using System.IO;

namespace TgBuilder.Core
{
    internal class Obfuscator
    {
        public static string PerformObfuscation(string outputFile)
        {
            string directory = Path.GetDirectoryName(outputFile);
            string originalFileName = Path.GetFileName(outputFile);
            string moduleNew = Path.Combine(directory, $"tmp_{originalFileName}");
            try
            {
                File.Copy(outputFile, moduleNew, overwrite: true);
                using (ModuleDef module = ModuleDefMD.Load(moduleNew))
                {
                    RenameProtector.Execute(module);
                    module.Write(outputFile);
                }

                return "Successfull";
            }
            catch (Exception ex)
            {
                return $"Obfuscation failed: {ex.Message}\nFailed method: {ex.TargetSite}";
            }
            finally
            {
                File.Delete(moduleNew);
            }
        }

        public static class RandomUtils
        {
            private static Random random = new Random();

            public static string RandomString(int length)
            {
                const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                return new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }

        public class RenameProtector
        {
            public static int count_xxx = 0;

            public static void Execute(ModuleDef module)
            {
                try
                {
                    module.Name = RandomUtils.RandomString(7);

                    foreach (var type in module.Types)
                    {
                        if (type.IsGlobalModuleType || type.IsRuntimeSpecialName || type.IsSpecialName || type.IsWindowsRuntime || type.IsInterface)
                            continue;

                        count_xxx++;
                        type.Name = RandomUtils.RandomString(40);
                        type.Namespace = "";

                        foreach (var property in type.Properties)
                        {
                            count_xxx++;
                            property.Name = RandomUtils.RandomString(40);
                        }

                        foreach (var field in type.Fields)
                        {
                            count_xxx++;
                            field.Name = RandomUtils.RandomString(40);
                        }

                        foreach (var eventDef in type.Events)
                        {
                            count_xxx++;
                            eventDef.Name = RandomUtils.RandomString(40);
                        }

                        foreach (var method in type.Methods)
                        {
                            if (method.IsConstructor) continue;
                            count_xxx++;
                            method.Name = RandomUtils.RandomString(40);

                            foreach (var param in method.ParamDefs)
                            {
                                count_xxx++;
                                param.Name = RandomUtils.RandomString(40);
                            }

                            if (method.HasBody)
                            {
                                foreach (var local in method.Body.Variables)
                                {
                                    count_xxx++;
                                    local.Name = RandomUtils.RandomString(40);
                                }

                                foreach (var instr in method.Body.Instructions)
                                {
                                    if (instr.OpCode == OpCodes.Ldloc || instr.OpCode == OpCodes.Stloc)
                                    {
                                        var localVar = instr.Operand as Local;
                                        if (localVar != null && localVar.Name != null)
                                        {
                                            count_xxx++;
                                            localVar.Name = RandomUtils.RandomString(40);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during renaming: {ex.Message}");
                }
            }
        }

    }
}
