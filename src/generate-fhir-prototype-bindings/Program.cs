using CommandLine;
using generate_fhir_prototype_bindings.Managers;
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

            Console.WriteLine("Processing basic primitives");

            // **** process primitive types ****

            if (!ProcessBasePrimitives(options.FhirDirectory))
            {
                Console.WriteLine("Failed to process base primitive types");
                return;
            }

            Console.WriteLine("...Done!");

            Console.WriteLine("Processing DataTypes");

            // **** process extended primitive types ****

            if (!ProcessExtendedPrimitives(options.FhirDirectory))
            {
                Console.WriteLine("Failed to process extended primitive types");
                return;
            }

            Console.WriteLine("...Done!");

            // **** add missing/undefined types ****

            AddMissingTypes();

            // **** process all other resource types ****

            if (!ProcessResources(options.FhirDirectory))
            {
                Console.WriteLine("Failed to process resources");
                return;
            }


            // **** ****

            Console.WriteLine($"Writing export file: {options.OutputFile}");

            // **** start our file ****

            using (StreamWriter writer = new StreamWriter(options.OutputFile))
            {

                // **** output our data ****

                FhirTypeManager.OutputTypeScript(writer, options.OutputNamespace);

                //if (!ProcessExtendedPrimitives(fhirDirectory, writer))
                //{
                //    Console.WriteLine("Failed to generate base primitive types");
                //    return;
                //}
            }

            Console.WriteLine("...Done!");
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

                string filenamePart = Path.GetFileNameWithoutExtension(filename).ToLower();

                // **** check for files that must be processed first or last ****

                if (filenamePart.Equals("primitives", StringComparison.Ordinal) ||
                    filenamePart.Equals("extension", StringComparison.Ordinal) ||
                    filenamePart.Equals("element", StringComparison.Ordinal))
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
                    Path.GetFileNameWithoutExtension(filename),
                    out firstElementName
                    );
            }

            //// **** check to see if the file has a restrictions sheet ****

            //if ((!string.IsNullOrEmpty(firstElementName)) && (ds.Tables.Contains("Restrictions")))
            //{
            //    // **** process restricted types ****

            //    ProcessRestrictionsTable(
            //        ds.Tables["Restrictions"], 
            //        firstElementName, 
            //        isPrimitive, 
            //        Path.GetFileNameWithoutExtension(filename)
            //        );
            //}

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

            ProcessPrimitiveTable(ds.Tables["Imports"], Path.GetFileNameWithoutExtension(primitivesFilename));

            // **** process base types from "Patterns" sheet ****

            ProcessPrimitiveTable(ds.Tables["Patterns"], Path.GetFileNameWithoutExtension(primitivesFilename));

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
