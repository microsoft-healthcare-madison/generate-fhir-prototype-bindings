using fhir;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
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
                int status = 0;

                // **** open the examples file ****

                ZipArchive zip = ZipFile.OpenRead(_jsonExamples);

                // **** traverse each file in this archive ****

                foreach (ZipArchiveEntry entry in zip.Entries)
                {

                    status = TestSourceParsing(entry);

                    //// **** act on entries we want to test ****

                    //switch (entry.Name)
                    //{
                    //    case "patient-example.json":
                    //    case "patient-example-a.json":
                    //    case "patient-example-animal.json":
                    //    case "patient-example-b.json":
                    //    case "patient-example-c.json":
                    //    case "patient-example-d.json":
                    //        status = TestSourceParsing(entry);
                    //        break;
                    //    default:
                    //        continue;
                    //        //break;
                    //}
                    if (status != 0)
                    {
                        return status;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failure: exception: {ex.Message}");
                return -2;
            }

            // **** success ****

            Console.WriteLine("\nPassed!\n");
            return 0;
        }

        public class ResourceConverter : JsonCreationConverter<fhir.Resource>
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            protected override fhir.Resource Create(Type objectType, JObject jObject)
            {
                if (jObject.ContainsKey("resourceType"))
                {
                    Type resourceType = Type.GetType($"fhir.{jObject.Value<string>("resourceType")}", false);

                    if (resourceType != null)
                    {
                        return (fhir.Resource)Activator.CreateInstance(resourceType);
                    }
                }

                return new fhir.Resource();
            }

        }

        public abstract class JsonCreationConverter<T> : JsonConverter
        {
            protected abstract T Create(Type objectType, JObject jObject);

            public override bool CanConvert(Type objectType)
            {
                return typeof(T).IsAssignableFrom(objectType);
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override object ReadJson(JsonReader reader,
                                            Type objectType,
                                             object existingValue,
                                             JsonSerializer serializer)
            {
                // **** load a JObject from the read stream ****

                JObject jObject = JObject.Load(reader);

                // **** create target object based on JObject ****

                T target = Create(objectType, jObject);

                // **** populate the object properties ****

                serializer.Populate(jObject.CreateReader(), target);

                // **** return our object ****

                return target;
            }
        }


        private static int TestSourceParsing(ZipArchiveEntry entry)
        {
            string source = ReadFileContents(entry);

            // **** deserialize into an object ****

            JObject parsed = JsonConvert.DeserializeObject<JObject>(source);

            // **** check for a resource type ****

            if (!parsed.ContainsKey("resourceType"))
            {

                Console.WriteLine($"WARN: Cannot Test: {entry.Name}, does not contain 'resourceType'!");

                // **** cannot test file ****

                return 0;
            }

            string resourceTypeName = parsed.Value<string>("resourceType");

            // **** check for a matching type ****

            Type resourceType = Type.GetType($"fhir.{resourceTypeName}", false);

            if (resourceType == null)
            {
                Console.WriteLine($"WARN: Cannot Test: {entry.Name}, could not match type to parse: {resourceTypeName}");
                return 0;
            }

            Console.WriteLine($"INFO: Testing {entry.Name}, parsing {resourceTypeName} as {resourceType.Name}");

            // **** deserialize into a patient resource ****

            //T typed = JsonConvert.DeserializeObject<T>(source);
            //dynamic typed = JsonConvert.DeserializeObject(source, resourceType);
            dynamic typed = JsonConvert.DeserializeObject(source, resourceType, new ResourceConverter());

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
                    // **** check for having an additional .0 on the line ****

                    if (typedLines[typedIndex].EndsWith(".0"))
                    {
                        Console.WriteLine($"WARN: {entry.Name}: Serialized precision mismatch: from {parsedLines[parsedIndex]} to {typedLines[typedIndex]}");
                        continue;
                    }

                    // **** check for a different Date/Time format (e.g., time zone) ****

                    if ((TryGetDateTime(parsedLines[parsedIndex], out DateTime parsedTime)) &&
                        (TryGetDateTime(typedLines[typedIndex], out DateTime typedTime)) &&
                        (parsedTime.ToUniversalTime().Equals(typedTime.ToUniversalTime())))
                    {
                        Console.WriteLine($"INFO: {entry.Name}: TimeZone conversion occured: from {parsedLines[parsedIndex]} to {typedLines[typedIndex]}");
                        continue;
                    }

                    Console.WriteLine($"Comparison failed: {entry.Name} line: {parsedIndex}:");
                    Console.WriteLine($"\t>>>{parsedLines[parsedIndex]}");
                    Console.WriteLine($"\t>>>{typedLines[typedIndex]}");
                    Console.WriteLine("Files written to c:\\temp for analysis. . .");

                    File.WriteAllLines("c:/temp/parsed.txt", parsedLines);
                    File.WriteAllLines("c:/temp/typed.txt", typedLines);

                    File.WriteAllText("c:/temp/parsed.json", serializedParsed);
                    File.WriteAllText("c:/temp/typed.json", serializedTyped);

                    return -10;
                }
            }

            return 0;
        }

        private static bool TryGetDateTime(string jsonLine, out DateTime value)
        {
            // **** should end in a " mark ****

            if (!jsonLine.EndsWith("\""))
            {
                value = DateTime.MinValue;
                return false;
            }

            // **** remove the trailing quote ****

            jsonLine = jsonLine.Substring(0, jsonLine.Length - 1);

            if (!jsonLine.Contains('"'))
            {
                value = DateTime.MinValue;
                return false;
            }

            // **** grab from the now-last-quote forward ****

            jsonLine = jsonLine.Substring(jsonLine.LastIndexOf('"') + 1);

            // **** try to get a date-time out of this ****

            if (DateTime.TryParse(jsonLine, out value))
            {
                return true;
            }

            return false;
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
