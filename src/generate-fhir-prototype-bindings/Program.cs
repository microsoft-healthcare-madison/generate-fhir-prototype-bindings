using CommandLine;
using fhir;
using generate_fhir_prototype_bindings.Languages;
using generate_fhir_prototype_bindings.Managers;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace generate_fhir_prototype_bindings
{
    class Program
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>Main entry-point for this application.</summary>
        ///
        /// <remarks>Gino Canessa, 7/12/2019.</remarks>
        ///
        /// <param name="args">An array of command-line argument strings.</param>
        ///-------------------------------------------------------------------------------------------------

        static void Main(string[] args)
        {
            // **** start timing ****

            Stopwatch timingWatch = Stopwatch.StartNew();
            
            // **** initialize our manager ****

            FhirTypeManager.Init();

            // **** process based on command line arguments ****

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(options => {
                    ProcessFhirDirectory(
                      options
                      );
                })
                .WithNotParsed(errors => { Console.WriteLine("Invalid arguments"); });

            // **** done ****

            long elapsedMs = timingWatch.ElapsedMilliseconds;

            Console.WriteLine($"Finished in: {elapsedMs / 1000.0} s");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the fhir directory described by options.</summary>
        ///
        /// <remarks>Gino Canessa, 7/15/2019.</remarks>
        ///
        /// <param name="options">Options for controlling the operation.</param>
        ///-------------------------------------------------------------------------------------------------

        static void ProcessFhirDirectory(Options options)
        {
            // **** make sure the directory exists ****

            if (!Directory.Exists(options.FhirDirectory))
            {
                Console.WriteLine($"Specified FHIR directory: {options.FhirDirectory} not found!");
                return;
            }

            // **** delete the output file if it exists ****

            if (File.Exists(options.OutputFile))
            {
                File.Delete(options.OutputFile);
            }

            // **** process all of the json files (only once) ****

            ProcessPublishedJson(options);

            // **** process XML spreadsheets (if necessary) ****

            if (options.UseOnlyXmlSpreadsheets)
            {
                ProcessXmlSpreadsheets(options);
            }

            // **** check for manual spreadsheet overrides ****

            if ((!options.UseOnlyXmlSpreadsheets) && 
                (!string.IsNullOrEmpty(options.TypesForXmlSpreadsheets)))
            {
                ProcessXmlSpreadsheetsFor(options);
            }

            // **** expand value sets ****

            FhirTypeManager.ExpandValueSets();

            // **** trim our output to match requested types ****

            FhirTypeManager.TrimForMatchingNames(options.TypesToOutput);

            // **** do ResourceType checks ****

            while (FhirTypeManager.PerformResourceTypeChecks() != 0)
            { }

            // **** check for writing typescript ****

            if (options.LanguageTypeScript)
            {
                WriteTypeScript(options);
            }

            if (options.LanguageCSharp)
            {
                WriteCSharp(options);
            }

            if (options.LanguageCSharpRaw)
            {
                WriteCSharpNoNewtonsoft(options);
            }

            // **** done ****

            Console.WriteLine("...Done!");
        }

        static bool ProcessPublishedJson(Options options)
        {
            string dir = Path.Combine(options.FhirDirectory, "publish");

            // **** check for this directory existing ****

            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Publish directory not found!");
                return false;
            }

            // **** get all canonical files in the publish directory ****

            string[] files = Directory.GetFiles(dir, "*.canonical.json", SearchOption.AllDirectories);

            // **** traverse the files ****

            foreach (string filename in files)
            {
                // **** read the contents ****

                string contents = File.ReadAllText(filename);

                // **** act depending on contents ****

                if (contents.Contains("\"resourceType\":\"StructureDefinition\""))
                {
                    // **** check for ignoring these ****

                    if (options.UseOnlyXmlSpreadsheets)
                    {
                        continue;
                    }

                    // **** parse into an object we can work with ****

                    fhir.StructureDefinition sd = JsonConvert.DeserializeObject<fhir.StructureDefinition>(contents);

                    // **** process this structure definition ****

                    ProcessStructureDefinition(sd, filename);

                    // **** done with this file ****

                    continue;
                }

                if (contents.Contains("\"resourceType\":\"CodeSystem\""))
                {
                    if (options.ExcludeCodeSystems)
                    {
                        continue;
                    }

                    // **** parse into an object we can work with ****

                    fhir.CodeSystem cs = JsonConvert.DeserializeObject<fhir.CodeSystem>(contents);

                    // **** process this code system ****

                    FhirTypeManager.LoadCodeSystem(cs);

                    // **** done with this file ****

                    continue;
                }

                if (contents.Contains("\"resourceType\":\"ValueSet\""))
                {
                    if (options.ExcludeValueSets)
                    {
                        continue;
                    }

                    // **** parse into an object we can work with ****

                    fhir.ValueSet vs = JsonConvert.DeserializeObject<fhir.ValueSet>(contents);

                    // **** process this value set ****

                    FhirTypeManager.LoadValueSet(vs, filename);

                    // **** done with this file ****

                    continue;
                }

            }


            // **** success *****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the structure definition JSON file described by filename.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="sd">The SD.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        static bool ProcessStructureDefinition(fhir.StructureDefinition sd, string filename)
        {
            // **** skip random extensions for now ****

            if ((sd.Type == "Extension") && 
                (sd.Id != "Extension"))
            {
                
                return true;
            }

            // **** check for elements ****

            if ((sd.Snapshot == null) || (sd.Snapshot.Element == null) || (sd.Snapshot.Element.Length == 0))
            {
                // **** nothing to do ****

                return false;
            }

            // **** process each kind of definition type ****

            switch (sd.Kind)
            {
                case "primitive-type":
                    ProcessStructurePrimitiveType(sd, filename);
                    break;

                case "complex-type":

                    //Console.WriteLine($"Complex type: {sd.Name} - {filename}");
                    ProcessStructureType(sd, filename);
                    break;

                case "resource":
                    ProcessStructureType(sd, filename);

                    break;

                case "logical":
                    Console.WriteLine($"Skipping logical type: {sd.Name} ({filename})");
                    break;

                default:
                    Console.WriteLine($"Unknown structureDefinition.kind: {sd.Kind} for {sd.Name} ({filename})");
                    break;
            }

            // **** success ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the structure type.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="sd">      The SD.</param>
        /// <param name="filename">Filename of the file.</param>
        ///-------------------------------------------------------------------------------------------------

        static void ProcessStructureType(fhir.StructureDefinition sd, string filename)
        {
            // **** figure out if this is a derived type ****

            string baseType = GetJsonTypeFromStructure(sd);

            if (string.IsNullOrEmpty(baseType))
            {
                // **** use the base type ****

                baseType = sd.Type;
            }

            // **** reformat circular references ****

            if (baseType.Equals(sd.Id, StringComparison.Ordinal))
            {
                baseType = "";
            }

            // **** traverse the elements ****

            for (int elementIndex = 0; elementIndex < sd.Snapshot.Element.Length; elementIndex++)
            {
                fhir.ElementDefinition element = sd.Snapshot.Element[elementIndex];

                // **** check for initial element (need to change type) ****

                if (elementIndex == 0)
                {
                    // **** use the base type ****

                    FhirTypeManager.ProcessSpreadsheetDataElement(
                        sd.Name,
                        baseType,
                        sd.Description,
                        $"{element.Min}..{element.Max}",
                        false,
                        filename
                        );

                    // **** check for defining a base type ****

                    if (!element.Id.Equals(sd.Name, StringComparison.Ordinal))
                    {
                        // **** make sure this node exists too ****

                        FhirTypeManager.ProcessSpreadsheetDataElement(
                            element.Id,
                            element.Base.Path,
                            element.Definition,
                            $"{element.Min}..{element.Max}",
                            false,
                            filename
                            );
                    }

                    // **** no more processing for this entry ****

                    continue;
                }

                // **** check for inherited property ****

                if ((element.Base != null) && 
                    (!string.IsNullOrEmpty(element.Base.Path)) &&
                    (!element.Base.Path.Equals(element.Id, StringComparison.Ordinal)))
                {
                    // **** skip this ****

                    continue;
                }

                // **** check for type information ****

                if ((element.Type != null) && (element.Type.Length > 0) && (element.Type[0].Code != null))
                {
                    // **** grab the valueSet if present ****

                    string valueSet = "";
                    if ((element.Binding != null) && (!string.IsNullOrEmpty(element.Binding.ValueSet)))
                    {
                        valueSet = element.Binding.ValueSet;
                    }

                    string cardinality = element.Type.Length > 1 ? "0..1" : $"{element.Min}..{element.Max}";

                    // **** traverse the types for this element ****

                    foreach (ElementDefinitionType defType in element.Type)
                    {
                        // **** remove array info from name (if necessary) ****

                        string elementName = element.Path.Replace(
                            "[x]",
                            string.Concat(
                                defType.Code.Substring(0, 1).ToUpper(),
                                defType.Code.Substring(1)
                                )
                            );

                        // **** check for a code type ****

                        if (defType.Code == "code")
                        {
                            string[] codeValues = element.Short.Split('|');

                            // **** process this field ****

                            FhirTypeManager.ProcessSpreadsheetDataElement(
                                elementName,
                                defType.Code,
                                (element.Comment == null) ? element.Definition : element.Comment,
                                $"{element.Min}..{element.Max}",
                                false,
                                filename,
                                codeValues
                                );

                            // **** done with this field ****

                            continue;
                        }

                        // **** process this field ****

                        FhirTypeManager.ProcessSpreadsheetDataElement(
                            elementName,
                            defType.Code,
                            element.Definition,
                            cardinality,
                            false,
                            filename,
                            valueSet: valueSet
                            );
                    }
                }

                // **** check for extension type information ****

                else if ((element.Type != null) && (element.Type.Length > 0) && (element.Type[0]._Code != null))
                {
                    // **** find the json type ****

                    string propertyType = "";

                    foreach (Extension ext in element.Type[0]._Code.Extension)
                    {
                        if (ext.Url.EndsWith("json-type"))
                        {
                            propertyType = ext.ValueString;
                            break;
                        }
                    }

                    // **** process this field ****

                    FhirTypeManager.ProcessSpreadsheetDataElement(
                        element.Path,
                        propertyType,
                        element.Definition,
                        $"{element.Min}..{element.Max}",
                        false,
                        filename
                        );
                }

                // **** use base path ****

                else
                {
                    // **** grab the assumed type ****

                    string propertyType = "";

                    // **** check for override ****

                    if (!string.IsNullOrEmpty(element.ContentReference))
                    {
                        propertyType = FhirPathToCamelCase(element.ContentReference);
                    }

                    // **** assume base path if nothing else filled out ****

                    if (string.IsNullOrEmpty(propertyType))
                    {
                        propertyType = FhirPathToCamelCase(element.Base.Path);
                    }

                    // **** process this field ****

                    FhirTypeManager.ProcessSpreadsheetDataElement(
                        element.Path,
                        propertyType,
                        element.Definition,
                        $"{element.Min}..{element.Max}",
                        false,
                        filename
                        );
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Fhir path to camel case.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="path">Full pathname of the file.</param>
        ///
        /// <returns>A string.</returns>
        ///-------------------------------------------------------------------------------------------------

        private static string FhirPathToCamelCase(string path)
        {
            string value = "";
            string[] parts = path.Split('.');

            foreach (string part in parts)
            {
                value += string.Concat(part.Substring(0, 1).ToUpper(), part.Substring(1));
            }

            return value;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the structure primitive type.</summary>
        ///
        /// <remarks>Gino Canessa, 8/12/2019.</remarks>
        ///
        /// <param name="sd">      The SD.</param>
        /// <param name="filename">Filename of the file.</param>
        ///-------------------------------------------------------------------------------------------------

        static void ProcessStructurePrimitiveType(fhir.StructureDefinition sd, string filename)
        {
            //Console.WriteLine($"Primitive: {sd.Name}:{GetJsonTypeFromStructure(sd)} - {filename}");

            // **** add our type ****

            FhirTypeManager.ProcessSpreadsheetType(sd.Name, GetPrimitiveJsonTypeFromStructure(sd), sd.Description, true, filename);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets JSON type from structure.</summary>
        ///
        /// <remarks>Gino Canessa, 8/12/2019.</remarks>
        ///
        /// <param name="sd">The SD.</param>
        ///
        /// <returns>The JSON type from structure.</returns>
        ///-------------------------------------------------------------------------------------------------

        private static string GetJsonTypeFromStructure(fhir.StructureDefinition sd)
        {
            // **** check for a base ****

            if (!string.IsNullOrEmpty(sd.BaseDefinition))
            {
                // **** remove most of the URL ****

                return sd.BaseDefinition.Substring(sd.BaseDefinition.LastIndexOf('/') + 1);
            }

            // **** cannot find ****

            return null;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets JSON type from structure.</summary>
        ///
        /// <remarks>Gino Canessa, 8/12/2019.</remarks>
        ///
        /// <param name="sd">The SD.</param>
        ///
        /// <returns>The JSON type from structure.</returns>
        ///-------------------------------------------------------------------------------------------------

        private static string GetPrimitiveJsonTypeFromStructure(fhir.StructureDefinition sd)
        {
            // **** build our value element name ****

            string name = $"{sd.Id}.value";

            // **** loop until we find the type ****

            foreach (fhir.ElementDefinition element in sd.Snapshot.Element)
            {
                if (element.Id.Equals(name, StringComparison.Ordinal))
                {
                    // **** use this element ***

                    return GetJsonTypeFromElement(element);
                }
            }

            // **** cannot find ****

            return "";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets JSON type from element.</summary>
        ///
        /// <remarks>Gino Canessa, 8/12/2019.</remarks>
        ///
        /// <param name="def">The definition.</param>
        ///
        /// <returns>The JSON type from element.</returns>
        ///-------------------------------------------------------------------------------------------------

        private static string GetJsonTypeFromElement(fhir.ElementDefinition def)
        {
            // **** ****

            foreach (fhir.ElementDefinitionType type in def.Type)
            {
                // **** check for the json type ****

                if ((type._Code != null) && (type._Code.Extension != null) && (type._Code.Extension.Length > 0))
                {
                    // **** find the correct one ****

                    foreach (fhir.Extension ext in type._Code.Extension)
                    {
                        if (ext.Url.EndsWith("json-type"))
                        {
                            // **** return this type ****

                            return ext.ValueString;
                        }
                    }
                }
            }

            // **** all else fails, default to string ****

            return "string";
        }
        
        static bool ProcessXmlSpreadsheetsFor(Options options)
        {
            string dir = Path.Combine(options.FhirDirectory, "source");

            // **** check for this directory existing ****

            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Source directory not found! Skipping XML Spreadsheets");
                return false;
            }

            // **** build our list of types to include ****

            string[] types = options.TypesForXmlSpreadsheets.Split('|');

            // **** traverse the types we want ****

            foreach (string typeName in types)
            {
                // **** build the filename we expect ****

                string filename = Path.Combine(options.FhirDirectory, "source", typeName, $"{typeName}-spreadsheet.xml");

                // **** check for this file ****

                if (!File.Exists(filename))
                {
                    Console.WriteLine($"Could not find {filename}, will not process XML spreadsheet!");
                    continue;
                }

                // **** remove this type (and subtypes) from the current list ****

                FhirTypeManager.RemoveType(typeName);

                // **** process this file ****

                ProcessResourceXmlFile(filename, false);
            }

            // **** ok ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the XML spreadsheets described by options.</summary>
        ///
        /// <remarks>Gino Canessa, 8/12/2019.</remarks>
        ///
        /// <param name="options">Options for controlling the operation.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        static bool ProcessXmlSpreadsheets(Options options)
        {
            string dir = Path.Combine(options.FhirDirectory, "source");

            // **** check for this directory existing ****

            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Source directory not found! Skipping XML Spreadsheets");
                return false;
            }

            Console.WriteLine("Processing basic primitives");

            // **** process primitive types ****

            if (!ProcessBasePrimitives(options.FhirDirectory))
            {
                Console.WriteLine("Failed to process base primitive types");
                return false;
            }

            Console.WriteLine("Processing DataTypes");

            // **** process extended primitive types ****

            if (!ProcessExtendedPrimitives(options.FhirDirectory))
            {
                Console.WriteLine("Failed to process extended primitive types");
                return false;
            }

            // **** add missing/undefined types ****

            AddMissingTypes();

            // **** process all other resource types ****

            if (!ProcessResources(options.FhirDirectory))
            {
                Console.WriteLine("Failed to process resources");
                return false;
            }

            // **** ok ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Writes a C#.</summary>
        ///
        /// <remarks>Gino Canessa, 8/12/2019.</remarks>
        ///
        /// <param name="options">Options for controlling the operation.</param>
        ///-------------------------------------------------------------------------------------------------

        static void WriteCSharp(Options options)
        {
            LanguageCSharp lang = new LanguageCSharp();

            string filename;

            // **** check for having an extension ****

            if (Path.HasExtension(options.OutputFile))
            {
                filename = options.OutputFile;
            }
            else
            {
                filename = $"{options.OutputFile}.{((ILanguangeExporter)lang).SourceFileExtension}";
            }

            // **** ****

            Console.WriteLine($"Writing C# file: {filename}");

            // **** start our file ****

            using (StreamWriter writer = new StreamWriter(filename))
            {
                // **** output our data ****

                FhirTypeManager.OutputForLang(
                    writer,
                    lang,
                    options.TypesToOutput,
                    options.OutputNamespace,
                    options.ExcludeCodes
                    );
                writer.Flush();
            }
        }


        static void WriteCSharpNoNewtonsoft(Options options)
        {
            LanguageCSharp lang = new LanguageCSharp();
            lang.IncludeNewtonsoftAnnotations = false;

            string filename;

            // **** check for having an extension ****

            if (Path.HasExtension(options.OutputFile))
            {
                filename = options.OutputFile;
            }
            else
            {
                filename = $"{options.OutputFile}.{((ILanguangeExporter)lang).SourceFileExtension}";
            }

            // **** ****

            Console.WriteLine($"Writing C# file WITHOUT Newtonsoft Json support: {filename}");

            // **** start our file ****

            using (StreamWriter writer = new StreamWriter(filename))
            {
                // **** output our data ****

                FhirTypeManager.OutputForLang(
                    writer,
                    lang,
                    options.TypesToOutput,
                    options.OutputNamespace,
                    options.ExcludeCodes
                    );
                writer.Flush();
            }
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>Writes the TypeScript file</summary>
        ///
        /// <remarks>Gino Canessa, 7/31/2019.</remarks>
        ///
        /// <param name="options">Options for controlling the operation.</param>
        ///-------------------------------------------------------------------------------------------------

        static void WriteTypeScript(Options options)
        {
            LanguageTypeScript lang = new LanguageTypeScript();

            string filename;

            // **** check for having an extension ****

            if (Path.HasExtension(options.OutputFile))
            {
                filename = options.OutputFile;
            }
            else
            {
                filename = $"{options.OutputFile}.{((ILanguangeExporter)lang).SourceFileExtension}";
            }

            // **** ****

            Console.WriteLine($"Writing TypeScript file: {filename}");

            // **** start our file ****

            using (StreamWriter writer = new StreamWriter(filename))
            {
                // **** output our data ****

                FhirTypeManager.OutputForLang(
                    writer,
                    lang,
                    options.TypesToOutput,
                    options.OutputNamespace,
                    options.ExcludeCodes
                    );
                writer.Flush();
            }
        }

        static void AddMissingTypes()
        {
            // TODO(ginoc): Remove once we find the actual definition of xhtml

            if (!FhirTypeManager.Exists("xhtml"))
            {
                FhirTypeManager.ProcessSpreadsheetType(
                    "xhtml",
                    "string",
                    "WARN: xhtml is defined as string as a definition cannot be found!",
                    true,
                    "definition-file-not-found"
                    );
            }

            // TODO(ginoc): Remove once we find the actual definition of SimpleQuantity

            if (!FhirTypeManager.Exists("SimpleQuantity"))
            {
                FhirTypeManager.ProcessSpreadsheetType(
                "SimpleQuantity",
                "Quantity",
                "WARN: SimpleQuantity definition cannot be found!",
                false,
                "definition-file-not-found"
                );
            }

            // TODO(ginoc): Remove once we find the actual definition of MoneyQuantity

            if (!FhirTypeManager.Exists("MoneyQuantity"))
            {
                FhirTypeManager.ProcessSpreadsheetType(
                "MoneyQuantity",
                "Quantity",
                "WARN: MoneyQuantity definition cannot be found!",
                false,
                "definition-file-not-found"
                );
            }

            // TODO(ginoc): Remove once we find the actual definition of Logical

            if (!FhirTypeManager.Exists("Logical"))
            {
                FhirTypeManager.ProcessSpreadsheetType(
                "Logical",
                "Element",
                "WARN: Logical definition cannot be found!",
                false,
                "definition-file-not-found"
                );
            }

            // TODO(ginoc): Remove once we find the actual definition of Structure (only seen in datatypes/actiondefinition.xml)

            if (!FhirTypeManager.Exists("Structure"))
            {
                FhirTypeManager.ProcessSpreadsheetType(
                "Structure",
                "Element",
                "WARN: Structure definition cannot be found!",
                false,
                "definition-file-not-found"
                );
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the resources described by fhirDirectory.</summary>
        ///
        /// <remarks>Gino Canessa, 7/10/2019.</remarks>
        ///
        /// <param name="fhirDirectory">Pathname of the fhir directory.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        static bool ProcessResources(string fhirDirectory)
        {
            // **** build our exclusion for datatypes ****

            string exclusionDataTypes = Path.Combine(fhirDirectory, "source", "datatypes");
            string exclusionTemplates = Path.Combine(fhirDirectory, "source", "templates");

            // **** traverse the XML files in the datatypes directory (exclude primitives) ****

            string[] files = Directory.GetFiles(Path.Combine(fhirDirectory, "source"), "*-spreadsheet.xml", SearchOption.AllDirectories);

            foreach (string filename in files)
            {
                // **** check for files with required orders ****

                if (filename.StartsWith(exclusionDataTypes) ||
                    filename.StartsWith(exclusionTemplates))
                {
                    // **** skip ****

                    continue;
                }

                //Console.WriteLine($"Checking Resource: {filename}...");

                // **** process this file ****

                ProcessResourceXmlFile(filename, false);
            }


            // **** success ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the extended primitives.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="fhirDirectory">Pathname of the fhir directory.</param>
        /// <param name="writer">       The writer.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        static bool ProcessExtendedPrimitives(string fhirDirectory)
        {

            //Console.WriteLine($"Checking Extended Primitives: element.xml ...");

            // **** process the element.xml file FIRST ****

            ProcessResourceXmlFile(Path.Combine(fhirDirectory, "source", "datatypes", "element.xml"), true);

            // **** traverse the XML files in the datatypes directory (exclude primitives) ****

            string[] files = Directory.GetFiles(Path.Combine(fhirDirectory, "source", "datatypes"), "*.xml");

            foreach (string filename in files)
            {
                // **** get the filename part ****

                //string filenamePart = Path.GetFileNameWithoutExtension(filename).ToLower();
                string filenamePart = Path.GetFileName(filename).ToLower();

                // **** check for files that must be processed first or last ****

                if (filenamePart.Equals("primitives.xml", StringComparison.Ordinal) ||
                    filenamePart.Equals("extension.xml", StringComparison.Ordinal) ||
                    filenamePart.Equals("element.xml", StringComparison.Ordinal))
                {
                    // **** skip ****

                    continue;
                }

                //Console.WriteLine($"Checking Extended Primitives: {filename}...");

                // **** process this file ****

                ProcessResourceXmlFile(filename, true);
            }

            //Console.WriteLine($"Checking Extended Primitives: extension.xml ...");

            // **** process the extension.xml file ****

            ProcessResourceXmlFile(Path.Combine(fhirDirectory, "source", "datatypes", "extension.xml"), true);

            // **** success ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the extended primitive file.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="filename">Filename of the file.</param>
        /// <param name="writer">  The writer.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        static bool ProcessResourceXmlFile(string filename, bool isPrimitive)
        {
            // **** make sure the file exists ****

            if (!File.Exists(filename))
            {
                Console.WriteLine($"File: {filename} not found!");
                return false;
            }

            if (!ExcelXmlParser.ParseFile(filename, out DataSet ds, true, true))
            {
                Console.WriteLine($"Failed to parse the file: {filename}.");

                // **** nothing else to do ****

                return false;
            }

            string firstElementName = "";

            // **** check to see if this is a type file (may be examples, etc.) ****

            if (ds.Tables.Contains("Data Elements"))
            {
                // **** process the data elements ****

                ProcessDataElementsTable(
                    ds.Tables["Data Elements"],
                    isPrimitive,
                    Path.GetFileName(filename), // Path.GetFileNameWithoutExtension(filename),
                    out firstElementName
                    );
            }

            // **** success ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the restrictions table.</summary>
        ///
        /// <remarks>Gino Canessa, 7/10/2019.</remarks>
        ///
        /// <param name="dt">             The dt.</param>
        /// <param name="elementBaseName">Name of the element base.</param>
        /// <param name="isPrimitive">    True if is primitive, false if not.</param>
        /// <param name="sourceFilename"> Filename of the source file.</param>
        ///-------------------------------------------------------------------------------------------------

        static void ProcessRestrictionsTable(
                                                DataTable dt,
                                                string elementBaseName,
                                                bool isPrimitive,
                                                string sourceFilename
                                                )
        {
            // **** grab the indexes of the columns we care about ****

            int ordinalName = dt.Columns["Name"].Ordinal;
            int ordinalComment = dt.Columns["Definition"].Ordinal;

            // **** iterate over the items in this table ****

            foreach (DataRow row in dt.Rows)
            {
                // **** grab our data ****

                string name = row[ordinalName].ToString();
                string comment = row[ordinalComment].ToString();

                // **** skip empty rows ****

                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }

                // **** skip standard restrictions (not interested right now) ****

                if (name.Equals(elementBaseName, StringComparison.Ordinal))
                {
                    continue;
                }

                // **** make this a type ****

                FhirTypeManager.ProcessSpreadsheetType(name, elementBaseName, comment, isPrimitive, sourceFilename);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the data elements table.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="dt">              The dt.</param>
        /// <param name="isPrimitive">     True if is primitive, false if not.</param>
        /// <param name="sourceFilename">  Filename of the source file.</param>
        /// <param name="firstElementName">[out] Name of the first element.</param>
        ///-------------------------------------------------------------------------------------------------

        static void ProcessDataElementsTable(
                                            DataTable dt,
                                            bool isPrimitive,
                                            string sourceFilename,
                                            out string firstElementName
                                            )
        {
            // **** grab the indexes of the columns we care about ****

            int ordinalElement = dt.Columns["Element"].Ordinal;
            int ordinalCardinality = dt.Columns["Card."].Ordinal;
            int ordinalType = dt.Columns["Type"].Ordinal;
            int ordinalComment = dt.Columns["Definition"].Ordinal;
            int ordinalShortName = dt.Columns["Short Name"].Ordinal;

            // **** check for no rows ****

            if ((dt.Rows == null) || (dt.Rows.Count == 0))
            {
                firstElementName = "";
                return;
            }

            // **** grab the element name from the first row ****

            firstElementName = dt.Rows[0][ordinalElement].ToString();

            // **** iterate over the items in this table ****

            foreach (DataRow row in dt.Rows)
            {
                // **** grab our data ****

                string element = row[ordinalElement].ToString();
                string cardinality = row[ordinalCardinality].ToString();
                string baseType = row[ordinalType].ToString();
                string comment = row[ordinalComment].ToString();

                // **** check for no comment (use short name) ****

                if (string.IsNullOrEmpty(comment))
                {
                    comment = row[ordinalShortName].ToString();
                }

                // **** skip empty rows ****

                if (string.IsNullOrEmpty(element))
                {
                    continue;
                }

                // **** process this field ****

                FhirTypeManager.ProcessSpreadsheetDataElement(element, baseType, comment, cardinality, isPrimitive, sourceFilename);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the primitive types.</summary>
        ///
        /// <remarks>Gino Canessa, 7/5/2019.</remarks>
        ///
        /// <param name="fhirDirectory">Pathname of the fhir directory.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        static bool ProcessBasePrimitives(string fhirDirectory)
        {
            // **** build the expected path to the primitives.xml file ****

            string primitivesFilename = Path.Combine(fhirDirectory, "source", "datatypes", "primitives.xml");

            Console.WriteLine($"Checking Base Primitives: {primitivesFilename}...");

            // **** make sure the file exists ****

            if (!File.Exists(primitivesFilename))
            {
                Console.WriteLine($"Primitives file: {primitivesFilename} not found!");
                return false;
            }

            if (!ExcelXmlParser.ParseFile(primitivesFilename, out DataSet ds, true, true))
            {
                Console.WriteLine($"Failed to parse the primitives file: {primitivesFilename}.");

                // **** nothing else to do ****

                return false;
            }

            // **** process base types from "Imports" sheet ****

            //ProcessPrimitiveTable(ds.Tables["Imports"], Path.GetFileNameWithoutExtension(primitivesFilename));
            ProcessPrimitiveTable(ds.Tables["Imports"], Path.GetFileName(primitivesFilename));

            // **** process base types from "Patterns" sheet ****

            //ProcessPrimitiveTable(ds.Tables["Patterns"], Path.GetFileNameWithoutExtension(primitivesFilename));
            ProcessPrimitiveTable(ds.Tables["Patterns"], Path.GetFileName(primitivesFilename));

            // **** success ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the primitive table.</summary>
        ///
        /// <remarks>Gino Canessa, 7/5/2019.</remarks>
        ///
        /// <param name="dt">The dt.</param>
        ///-------------------------------------------------------------------------------------------------

        static void ProcessPrimitiveTable(DataTable dt, string sourceFilename)
        {
            // **** grab the indexes of the columns we care about ****

            int ordinalName = dt.Columns["Data Type"].Ordinal;
            int ordinalType = dt.Columns["Json"].Ordinal;
            int ordinalComment = dt.Columns["Definition"].Ordinal;

            // **** iterate over the items in this table ****

            foreach (DataRow row in dt.Rows)
            {
                // **** grab our data ****

                string name = row[ordinalName].ToString();
                string baseType = row[ordinalType].ToString();
                string comment = row[ordinalComment].ToString();

                FhirTypeManager.ProcessSpreadsheetType(name, baseType, comment, true, sourceFilename);
            }
        }
    }
}
