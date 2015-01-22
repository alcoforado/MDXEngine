using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ResourceBuilder
{
    public class ResouceCodeGen
    {

        public void GenerateCodeFile(string Namespace, string ClassName, string DirSource, TextWriter writer)
        {
            var dir = new DirectoryInfo(DirSource);
            if (!dir.Exists)
                throw new Exception("Directory does not exist");


            //Write the header
            writer.WriteLine(String.Format(@"
namespace {0}
    {{
        static internal class {1}
    {{
", Namespace, ClassName));


            //for all files in DirSource directory
            foreach (var file in dir.GetFiles())
            {
                string memberName = file.Name.Replace(".", "_");

                var fs = file.OpenText();
                string text = fs.ReadToEnd();
                text=text.Replace("\"", "\"\"");
                text = "\t\t static public readonly string " + memberName + "= @\"" + text + "\";\n";
                writer.WriteLine(text);
            }

            //Close braces
            writer.WriteLine(@"
    }
}
");



        }
    }
}
    
