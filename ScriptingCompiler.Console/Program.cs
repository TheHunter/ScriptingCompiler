using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheHunter.Scripting;

namespace ScriptingCompiler.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptHosting scripHost = new ScriptHosting();
            
            Console.WriteLine(@"Welcome to C# REPL. write quit to exit at any time.");
            while (true)
            {
                string code = Console.ReadLine();
                if (code != null && code.ToLower() != "quit")
                {
                    try
                    {
                        object result = scripHost.Execute(code);
                        Console.WriteLine(@"	{0}", result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
