using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Options;
using System.IO;
namespace ResourceBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new ArgsParser(args);
            if (parser.HasError)
                System.Console.WriteLine(parser.ErrorMessage);
            else
            {
                var codeGen = new ResouceCodeGen();

                StreamWriter writer = null;
                if (parser.Language == "c#")
                {
                    writer = new StreamWriter(String.Format(".\\{0}.cs", parser.ClassName));
                    codeGen.GenerateCodeFile(parser.Namespace, parser.ClassName, parser.DirSource, writer);
                }
                else if (parser.Language == "typescript")
                {
                    writer = new StreamWriter(String.Format(".\\{0}.ts", parser.ClassName));
                    codeGen.GenerateTypescript(parser.ClassName, parser.DirSource, writer);
                }
            


                writer.Flush();
                writer.Dispose();
            }
            return;
        }
    }
}
