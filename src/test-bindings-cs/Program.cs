using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.IO.Compression;

namespace test_bindings_cs
{
    class Program
    {
        private const string _jsonExamples = "C:\\git\\fhir\\publish\\examples-json.zip";

        private static CamelCasePropertyNamesContractResolver _contractResolver = new CamelCasePropertyNamesContractResolver();

        static int Main(string[] args)
        {
            // **** check for zip file ****

            if (!File.Exists(_jsonExamples))
            {
                Console.WriteLine($"JSON Examples file {_jsonExamples} not found!");
                return -1;
            }

            try
            {

                // **** open the examples file ****

                ZipArchive zip = ZipFile.OpenRead(_jsonExamples);

                // **** traverse each file in this archive ****

                foreach (ZipArchiveEntry entry in zip.Entries)
                {
                    // **** first test, just look for patient ****

                    if (entry.Name != "patient-example.json")
                    {
                        continue;

                    }
                    // **** read the contents of this file ****

                    string exampleContents = ReadFileContents(entry);

                    // **** deserialize into an object ****

                    object parsed = JsonConvert.DeserializeObject(exampleContents);

                    // **** deserialize into a patient resource ****

                    fhir.Patient typed = JsonConvert.DeserializeObject<fhir.Patient>(exampleContents);

                    // **** re-serialize ****

                    string serializedParsed = JsonConvert.SerializeObject(
                        parsed,
                        Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            ContractResolver = _contractResolver,
                            
                        });

                    string serializedTyped = JsonConvert.SerializeObject(
                        typed,
                        Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            ContractResolver = _contractResolver,
                        });

                    // **** split on lines ****

                    string[] parsedLines = serializedParsed.Split('\n');
                    string[] typedLines = serializedTyped.Split('\n');

                    // **** check line counts ****

                    if (parsedLines.Length != typedLines.Length)
                    {
                        Console.WriteLine($"Mismatched line counts! parsed: {parsedLines.Length} typed: {typedLines.Length}");
                    }

                    // **** trim and remove trailing commas ****

                    for (int parsedIndex = 0; (parsedIndex < parsedLines.Length); parsedIndex++)
                    {
                        parsedLines[parsedIndex] = parsedLines[parsedIndex].Trim();

                        if (parsedLines[parsedIndex].EndsWith(','))
                        {
                            parsedLines[parsedIndex] = parsedLines[parsedIndex].Substring(0, parsedLines[parsedIndex].Length - 1);
                        }
                    }

                    for (int typedIndex = 0; (typedIndex < typedLines.Length); typedIndex++)
                    {
                        typedLines[typedIndex] = typedLines[typedIndex].Trim();

                        if (typedLines[typedIndex].EndsWith(','))
                        {
                            typedLines[typedIndex] = typedLines[typedIndex].Substring(0, typedLines[typedIndex].Length - 1);
                        }
                    }

                    // **** sort ****

                    Array.Sort(parsedLines);
                    Array.Sort(typedLines);

                    // **** compare ****

                    for (int parsedIndex = 0, typedIndex = 0; (parsedIndex < parsedLines.Length) && (typedIndex < typedLines.Length); parsedIndex++, typedIndex++)
                    {
                        // **** remove trailing comma (,) from either line, as sorting can result in different orders ****

                        if (parsedLines[parsedIndex].EndsWith(','))
                        {
                            parsedLines[parsedIndex] = parsedLines[parsedIndex].Substring(0, parsedLines[parsedIndex].Length - 1);
                        }

                        if (typedLines[typedIndex].EndsWith(','))
                        {
                            typedLines[typedIndex] = typedLines[typedIndex].Substring(0, typedLines[typedIndex].Length - 1);
                        }

                        // **** compare ****

                        if (!parsedLines[parsedIndex].Equals(typedLines[typedIndex], StringComparison.Ordinal))
                        {
                            Console.WriteLine($"Comparison failed, line: {parsedIndex}:");
                            Console.WriteLine($"\t>>>{parsedLines[parsedIndex]}");
                            Console.WriteLine($"\t>>>{typedLines[typedIndex]}");

                            File.WriteAllLines("c:/temp/parsed.txt", parsedLines);
                            File.WriteAllLines("c:/temp/typed.txt", typedLines);

                            return -3;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failure: exception: {ex.Message}");
                return -2;
            }
            


            // **** success ****

            return 0;
        }

        private static string ReadFileContents(ZipArchiveEntry entry)
        {
            // **** read this file ****

            using (Stream inputStream = entry.Open())
            using (StreamReader reader = new StreamReader(inputStream))
            {
                // **** read the stream contents ****

                return reader.ReadToEnd();
            }
        }
    }
}
