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

        [Option("only-structure-defs", Default = true, HelpText = "(Default) Set to use only Structure Definitions (.../fhir/publish)")]
        public bool UseOnlyStructureDefinitions { get; set; }

        [Option("only-xml-sheets", Default = false, HelpText = "Set to use only XML Spreadsheets (.../fhir/source)")]
        public bool UseOnlyXmlSpreadsheets { get; set; }

        [Option("types-for-xml-sheets", Default ="", HelpText = "'|' Separated list of resources to read from XML files. E.g., Topic|Subscription")]
        public string TypesForXmlSpreadsheets { get; set; }

        [Option("exclude-code-systems", Default =false, HelpText = "Set to Exclude processing of Code Systems")]
        public bool ExcludeCodeSystems { get; set; }

        [Option("exclude-value-sets", Default =false, HelpText ="Set to Exclude processing of Value Sets")]
        public bool ExcludeValueSets { get; set; }
        #endregion Input Options

        #region Ouput Options . . .

        [Option('o', "output-path", Required = true, HelpText = "Path or filename for output (will append appropriate extension if not provided)")]
        public string OutputFile { get; set; }

        [Option("output-single", Default = true, HelpText = "Set to output a single file")]
        public bool OutputSingleFile { get; set; }

        [Option("types-to-output", Default = "", HelpText = "'|' Separated list of resources/profiles/types to export (will include all necessary types below). E.g., Topic|Subscription")]
        public string TypesToOutput { get; set; }

        [Option("exclude-codes", Default =false, HelpText ="Set to Exclude output of Code values as enums/lists")]
        public bool ExcludeCodes { get; set; }

        #endregion Output Options . . .

        #region Language Options . . .

        [Option("ts", Default = false, Hidden = false, HelpText = "Generate TypeScript bindings")]
        public bool LanguageTypeScript { get; set; }

        [Option("cs", Default = false, Hidden = false, HelpText = "Generate Default C# bindings (currently NewtonSoft JSON)")]
        public bool LanguageCSharpDefault { get; set; }

        [Option("cs-plain", Default = false, Hidden = false, HelpText = "Generate Plain C# bindings")]
        public bool LanguageCSharpPlain { get; set; }

        [Option("cs-core", Default = false, Hidden = false, HelpText = "Generate C# bindings for System.Text.Json (.Net Core)")]
        public bool LanguageCSharpCore { get; set; }

        [Option("cs-newtonsoft", Default = false, Hidden = false, HelpText = "Generate C# bindings for Newtonsoft.Json")]
        public bool LanguageCSharpNewtonsoft { get; set; }

        [Option("cs-polymorphic", Default = true, Hidden = false, HelpText = "Generate Polymorphic Deserialization Helpers if possible")]
        public bool LanguageCSharpPolymorphic { get; set; }

        #endregion Language Options . . .

        #region Generator Options . . .

        [Option("namespace", Default = "fhir", HelpText = "The Exported namespace or module name")]
        public string OutputNamespace { get; set; }


        #endregion Generator Options . . .
    }
}
