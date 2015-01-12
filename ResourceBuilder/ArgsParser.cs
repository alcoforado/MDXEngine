using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Options;
using System.IO;
namespace ResourceBuilder
{
    public class ArgsParser
    {
        public bool HasError { get { return !String.IsNullOrEmpty(ErrorMessage); } }
        public string ClassName { get; set; }
        public string DirSource { get; set; }
        public string Namespace { get; set; }
        public bool DisplayHelp { get; set; }
        public string ErrorMessage { get; private set; }
        private OptionSet _optionSet { get; set; }
        public string HelpMessage { get; set; }

        public ArgsParser(string[] args)
        {
            
            _optionSet = new OptionSet()
            .Add("h", "Display Help Message", option => DisplayHelp = true)
            .Add("namespace=", "Namespace of the resource class to be created", option => Namespace = option)
            .Add("classname=", "Name of the resource class", option => ClassName = option)
            .Add("sourceDir=", "Name of the directory containing the files to be stringified and added to the resource class, only files in that directory will be added, it is non recursive", option => DirSource = option);

            var writer = new StringWriter();
            _optionSet.WriteOptionDescriptions(writer);
            HelpMessage = @"
Usage is
ResourceBuilder --namespace=[NameSpace] --classname=[ClassName] --sourceDir=[dir]

" + writer.ToString();

            try
            {
                _optionSet.Parse(args);
                
                if (ClassName == null)
                {
                    ErrorMessage = "Error: ClassName parameter was not supplied" + "\n" + HelpMessage;
                }

                if (DirSource == null)
                {
                    ErrorMessage = "Error: DirSource parameter was  not supplied" + "\n" + HelpMessage;
                }
                else if (!(new DirectoryInfo(DirSource).Exists))
                {
                    ErrorMessage = "Directory does not exist" + "\n" + HelpMessage;
                }


                if (Namespace == null)
                    Namespace = "";

               

            }
            catch (OptionException e)
            {
                ErrorMessage = "Error: " + e.Message + "\n" + HelpMessage;
            }

        }
    }
}