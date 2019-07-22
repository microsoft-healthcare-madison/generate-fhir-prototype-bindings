using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace generate_fhir_prototype_bindings
{
    
    public class Options
    {
        #region Input Options . . .

        [Option('i', "fhir-directory", Required = true, HelpText = "FHIR Base Directory")]
        public string FhirDirectory { get; set; }

        #endregion Input Options

        #region Ouput Options . . .

        [Option('o', "output-path", Required = true, HelpText = "Path or filename for output")]
        public string OutputFile { get; set; }

        [Option("output-single", Default = true, HelpText = "Set to output a single file")]
        public bool OutputSingleFile { get; set; }

        [Option("types-to-output", Default = "", HelpText = "'|' Separated list of resources/profiles/types to export (will include all necessary types below). E.g., Topic|Subscription")]
        public string TypesToOutput { get; set; }

        #endregion Output Options . . .

        #region Language Options . . .

        [Option("ts", Default = false, Hidden = false, HelpText = "Generate TypeScript bindings")]
        public bool LanguageTypeScript { get; set; }

        #endregion Language Options . . .

        #region Generator Options . . .

        [Option("namespace", Default = "fhir", HelpText = "The Exported namespace or module name")]
        public string OutputNamespace { get; set; }


        #endregion Generator Options . . .
    }
}
