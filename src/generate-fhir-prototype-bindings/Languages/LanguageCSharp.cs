using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using generate_fhir_prototype_bindings.Managers;
using generate_fhir_prototype_bindings.Models;
using fhir;

namespace generate_fhir_prototype_bindings.Languages
{
    public class LanguageCSharp : ILanguangeExporter
    {
        #region Class Constants . . .

        private const string _lineComment = "///";
        private const char _indentChar = '\t';
        private const bool _flagOptionals = true;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Values that represent C# styles.</summary>
        ///
        /// <remarks>Gino Canessa, 12/5/2019.</remarks>
        ///-------------------------------------------------------------------------------------------------

        public enum CSharpStyle: int {
            Plain = 0,
            SystemTextJson,
            Newtonsoft
        }

        #endregion Class Constants . . .

        #region Class Variables . . .

        private static Dictionary<string, string> _fhirLanguageTypeMap = new Dictionary<string, string>()
        {
            { "instant", "string" },
            { "time", "string" },
            { "date", "string" },
            { "dateTime", "string" }, // Cannot use "DateTime" because of Partial Dates... may want to consider defining a new type, but not today
            { "boolean", "bool" },
            { "decimal", "decimal" },
            { "integer", "int" },
            { "integer64", "string" },  // int64 serializes as string, need to implement custom converter to change this
            { "unsignedInt", "uint" },
            { "positiveInt", "uint" },
            { "base64Binary", "string" },
            { "url", "string" },
            { "uri", "string" },
            { "code", "string" },
            { "string", "string" },
            { "canonical", "string" },
            { "markdown", "string" },
            { "id", "string" },
            { "oid", "string" },
            { "xhtml", "string" },
            { "uuid", "Guid" },
        };

        /// <summary>Set of all language reserved words</summary>
        private static HashSet<string> _reservedWordsSet;

        #endregion Class Variables . . .

        #region Instance Variables . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the language style.</summary>
        ///
        /// <value>The language style.</value>
        ///-------------------------------------------------------------------------------------------------

        public int LanguageStyle { get; set; } = (int)CSharpStyle.SystemTextJson;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets a value indicating whether the polymorphic deserialization.</summary>
        ///
        /// <value>True if polymorphic deserialization, false if not.</value>
        ///-------------------------------------------------------------------------------------------------

        public bool PolymorphicDeserialization { get; set; } = true;

        /// <summary>The exported resources.</summary>
        private Dictionary<string, string> _exportedResourceNamesAndTypes;

        #endregion Instance Variables . . .


        #region Constructors . . .

        public LanguageCSharp()
        {
            _reservedWordsSet = new HashSet<string>()
            {
                "abstract",
                "as",
                "base",
                "bool",
                "break",
                "byte",
                "case",
                "catch",
                "char",
                "checked",
                "class",
                "const",
                "continue",
                "decimal",
                "default",
                "delegate",
                "do",
                "double",
                "else",
                "enum",
                "event",
                "explicit",
                "extern",
                "false",
                "finally",
                "fixed",
                "float",
                "for",
                "foreach",
                "goto",
                "if",
                "implicit",
                "in",
                "int",
                "interface",
                "internal",
                "is",
                "lock",
                "long",
                "namespace",
                "new",
                "null",
                "object",
                "operator",
                "out",
                "override",
                "params",
                "private",
                "protected",
                "public",
                "readonly",
                "ref",
                "return",
                "sbyte",
                "sealed",
                "short",
                "sizeof",
                "stackalloc",
                "static",
                "string",
                "struct",
                "switch",
                "this",
                "throw",
                "true",
                "try",
                "typeof",
                "uint",
                "ulong",
                "unchecked",
                "unsafe",
                "ushort",
                "using",
                "static",
                "virtual",
                "void",
                "volatile",
                "while"
            };
            _exportedResourceNamesAndTypes = new Dictionary<string, string>();
        }

        #endregion Constructors . . .

        #region Instance Interface . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets filename part for style.</summary>
        ///
        /// <remarks>Gino Canessa, 12/5/2019.</remarks>
        ///
        /// <value>The filename part for style.</value>
        ///-------------------------------------------------------------------------------------------------

        public string GetFilenamePartForStyle
        {
            get {
                switch (LanguageStyle)
                {
                    case (int)CSharpStyle.Plain:
                        return "";
                    //break;
                    case (int)CSharpStyle.SystemTextJson:
                        return "-core";
                    //break;
                    case (int)CSharpStyle.Newtonsoft:
                        return "-newtonsoft";
                    //break;
                    default:
                        return $"-{LanguageStyle}";
                        //break;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets style name.</summary>
        ///
        /// <remarks>Gino Canessa, 12/5/2019.</remarks>
        ///
        /// <value>The style name.</value>
        ///-------------------------------------------------------------------------------------------------

        public string GetStyleName
        {
            get
            {
                switch (LanguageStyle)
                {
                    case (int)CSharpStyle.Plain:
                        return "Plain";
                    //break;
                    case (int)CSharpStyle.SystemTextJson:
                        return "System.Text.Json";
                    //break;
                    case (int)CSharpStyle.Newtonsoft:
                        return "Newtonsoft Json";
                    //break;
                    default:
                        return $"Unknown style: {LanguageStyle}";
                        //break;
                }
            }
        }

        #endregion Instance Interface . . .

        #region Interface:ILanguageExporter . . .

        Dictionary<string, string> ILanguangeExporter.FhirLanguageTypeMap => _fhirLanguageTypeMap;

        HashSet<string> ILanguangeExporter.ReservedWords => _reservedWordsSet;

        bool ILanguangeExporter.OutputBasicPrimitives => false;

        string ILanguangeExporter.SourceFileExtension => "cs";

        bool ILanguangeExporter.FlagOptionals => _flagOptionals;

        bool ILanguangeExporter.AppendCodes(
                        ref StringBuilder sb,
                        FhirProperty property, 
                        string parentName
                        )
        {
            // **** put a comment ****

            sb.Append($"\t///<summary>Code Values for the {parentName}.{property.Name} field</summary>\n");

            // **** open our enum ***

            sb.Append($"\tpublic sealed class {parentName}{property.NameCapitalized}Codes {{\n");

            // **** start adding values ****

            foreach (string code in property.CodeValues)
            {
                FhirTypeManager.SanitizeForCode(code, _reservedWordsSet, out string name, out string value);

                sb.Append($"\t\tpublic const string {name.ToUpper()} = \"{value}\";\n");
            }

            // **** close our enum ***

            sb.Append("\t}\n");

            return true;
        }

        bool ILanguangeExporter.AppendFhirProperty(
                                ref StringBuilder sb,
                                FhirProperty property, 
                                string typeName,
                                bool useLowerCaseName
                                )
        {
            string nameCamel = FhirTypeManager.SanitizeForProperty(property.Name, _reservedWordsSet);
            string namePascal = FhirTypeManager.SanitizeForProperty(property.NameCapitalized, _reservedWordsSet);

            string propertyName;
            string extendedName;

            if (useLowerCaseName)
            {
                propertyName = nameCamel;
                extendedName = nameCamel;
            }
            else
            {
                switch (LanguageStyle)
                {
                    case (int)CSharpStyle.SystemTextJson:
                        propertyName = namePascal;
                        extendedName = namePascal;
                        break;
                    case (int)CSharpStyle.Newtonsoft:
                        propertyName = namePascal;
                        extendedName = namePascal;
                        break;
                    case (int)CSharpStyle.Plain:
                    default:
                        propertyName = namePascal;
                        extendedName = nameCamel;
                        break;
                }
            }

            string propertyAnnotation = "";
            string extendedAnnotation = "";

            switch (LanguageStyle)
            {
                case (int)CSharpStyle.SystemTextJson:
                    propertyAnnotation = $"\t\t[JsonPropertyName(\"{property.Name}\")]\n";
                    extendedAnnotation = $"\t\t[JsonPropertyName(\"_{property.Name}\")]\n";
                    break;
                case (int)CSharpStyle.Newtonsoft:
                    propertyAnnotation = $"\t\t[JsonProperty(PropertyName = \"{property.Name}\")]\n";
                    extendedAnnotation = $"\t\t[JsonProperty(PropertyName = \"_{property.Name}\")]\n";
                    break;
                case (int)CSharpStyle.Plain:
                default:

                    break;
            }

            string comment = FhirTypeManager.SanitizeComment(property.Comment, _lineComment, _indentChar, 2);

            string optionalFlagString = (_flagOptionals && property.IsOptional) ? "?" : "";

            // **** nullable reference types are not allowed in current C# ****

            switch (typeName)
            {
                case "bool":
                case "decimal":
                case "DateTime":
                case "int":
                case "uint":
                case "Guid":

                    // **** ignore - types can be optional ****
                break;
                default:
                    // **** do not allow reference types to be null (for now) ****
                    optionalFlagString = "";
                    break;
            }

            // **** ****

            if (property.IsArray)
            {
                sb.Append(
                    $"\t\t///<summary>{comment}</summary>\n" +
                    propertyAnnotation +
                    $"\t\tpublic {typeName}[] {propertyName} {{ get; set; }}\n" +
                    $"\t\t///<summary>May contain extended information for property: '{propertyName}'</summary>\n" +
                    extendedAnnotation +
                    $"\t\tpublic Element[] _{extendedName} {{ get; set; }}\n");

                return true;
            }

            sb.Append(
                $"\t\t///<summary>{comment}</summary>\n" +
                propertyAnnotation +
                $"\t\tpublic {typeName}{optionalFlagString} {propertyName} {{ get; set; }}\n" +
                $"\t\t///<summary>May contain extended information for property: '{propertyName}'</summary>\n" +
                extendedAnnotation +
                $"\t\tpublic Element _{extendedName} {{ get; set; }}\n");

            return true;
        }

        bool ILanguangeExporter.AppendFhirTypeOpen(ref StringBuilder sb, FhirType fhirType)
        {
            string comment = FhirTypeManager.SanitizeComment(fhirType.Comment, _lineComment, _indentChar, 1);

            // **** start with the interface open ****

            if (string.IsNullOrEmpty(fhirType.TypeName) || fhirType.Name.Equals("Element"))
            {
                sb.Append(
                    $"\t///<summary>\n" +
                    $"\t///{comment}\n" +
                    $"\t///</summary>\n" +
                    $"\t///<source-file>{fhirType.SourceFilename}</source-file>\n" +
                    $"\tpublic class {fhirType.NameCapitalized}\n" +
                    $"\t{{\n"
                    );
            }
            else if (fhirType.Name.Equals(fhirType.TypeName, StringComparison.Ordinal))
            {
                sb.Append(
                    $"\t///<summary>\n" +
                    $"\t///{comment}\n" +
                    $"\t///</summary>\n" +
                    $"\t///<source-file>{fhirType.SourceFilename}</source-file>\n" +
                    $"\tpublic class {fhirType.NameCapitalized} : Element\n" +
                    $"\t{{\n"
                    );
            }
            else
            {
                sb.Append(
                    $"\t///<summary>\n" +
                    $"\t///{comment}\n" +
                    $"\t///</summary>\n" +
                    $"\t///<source-file>{fhirType.SourceFilename}</source-file>\n" +
                    $"\tpublic class {fhirType.NameCapitalized} : {fhirType.TypeName}\n" +
                    $"\t{{\n");
            }

            // **** output resource type first (if necessary) ****

            if (FhirTypeManager.DoesTypeRequireResourceTag(fhirType.Name))
            {
                // **** add this resource to our list (for polymorphic deserialization) ****

                _exportedResourceNamesAndTypes.Add(fhirType.Name, fhirType.NameCapitalized);

                // **** output the correct ResourceType field based on style ****

                switch (LanguageStyle)
                {
                    case (int)CSharpStyle.SystemTextJson:
                        sb.Append(
                            $"\t\t///<summary>Resource Type Name (for serialization)</summary>\n" +
                            $"\t\t[JsonPropertyName(\"resourceType\")]\n" +
                            $"\t\tpublic string ResourceType => \"{fhirType.Name}\";\n"
                            );
                        break;
                    case (int)CSharpStyle.Newtonsoft:
                        sb.Append(
                            $"\t\t///<summary>Resource Type Name (for serialization)</summary>\n" +
                            $"\t\t[JsonProperty(PropertyName = \"resourceType\")]\n" +
                            $"\t\tpublic string ResourceType => \"{fhirType.Name}\";\n"
                            );
                        break;
                    case (int)CSharpStyle.Plain:
                    default:
                        sb.Append(
                            $"\t\t///<summary>Resource Type Name (for serialization)</summary>\n" +
                            $"\t\tpublic string ResourceType => \"{fhirType.Name}\";\n"
                            );
                        break;
                }
            }

            return true;
        }

        bool ILanguangeExporter.AppendFhirTypeClose(ref StringBuilder sb, FhirType fhirType)
        {
            sb.Append("\t}\n");
            return true;
        }

        bool ILanguangeExporter.AppendFileHeader(ref StringBuilder sb, string typesToOutput)
        {
            sb.Append($"/** GENERATED FILE **/\n");
            sb.Append($"/** Generated by: {Environment.UserName} at {DateTime.Now} **/\n");

            if (!string.IsNullOrEmpty(typesToOutput))
            {
                sb.Append($"/** Restricted to types: {typesToOutput} **/\n");
            }

            switch (LanguageStyle)
            {
                case (int)CSharpStyle.SystemTextJson:
                    sb.Append("using System;\nusing System.Collections.Generic;\nusing System.Text.Json\n\n");
                    break;
                case (int)CSharpStyle.Newtonsoft:
                    sb.Append("using System;\nusing System.Collections.Generic;\nusing Newtonsoft.Json;\nusing Newtonsoft.Json.Linq;\n\n");
                    break;
                case (int)CSharpStyle.Plain:
                default:
                    sb.Append("using System;\nusing System.Collections.Generic;\n\n");
                    break;
            }

            return true;
        }

        bool ILanguangeExporter.AppendFileFooter(ref StringBuilder sb)
        {
            sb.Append($"/** END OF GENERATED FILE **/\n");
            return true;
        }

        private bool AppendPolymorphicHelpersNewtonsoft(ref StringBuilder sb)
        {
            // **** open class ****

            sb.Append("\tpublic class ResourceConverter : JsonConverter\n\t{\n");
            
            // **** function CanConvert ****

            sb.Append("\t\tpublic override bool CanConvert(Type objectType)\n\t\t{\n");
            sb.Append("\t\t\treturn typeof(Resource).IsAssignableFrom(objectType);\n");
            sb.Append("\t\t}\n");

            // **** property CanWrite ****

            sb.Append("\t\tpublic override bool CanWrite\n\t\t{\n");
            sb.Append("\t\t\tget { return false; }\n");
            sb.Append("\t\t}\n");

            // **** function WriteJson ****

            sb.Append("\t\tpublic override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)\n\t\t{\n");
            sb.Append("\t\t\tthrow new NotImplementedException();\n");
            sb.Append("\t\t}\n");

            // **** property CanRead ****

            sb.Append("\t\tpublic override bool CanRead\n\t\t{\n");
            sb.Append("\t\t\tget { return true; }\n");
            sb.Append("\t\t}\n");

            // **** function ReadJson ****

            sb.Append("\t\tpublic override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)\n\t\t{\n");
            sb.Append("\t\t\tJObject jObject = JObject.Load(reader);\n");
            sb.Append("\t\t\tstring resourceType = jObject[\"resourceType\"].Value<string>();\n");
            sb.Append("\t\t\tobject target = null;\n");
            sb.Append("\t\t\tswitch (resourceType)\n");
            sb.Append("\t\t\t{\n");

            // **** loop through our types ****

            foreach (KeyValuePair<string, string>kvp in _exportedResourceNamesAndTypes)
            {
                sb.Append($"\t\t\t\tcase \"{kvp.Key}\":\n");
                sb.Append($"\t\t\t\t\ttarget = new {kvp.Value}();\n");
                sb.Append("\t\t\t\t\tbreak;\n");
            }

            // **** default case returns a Resource object ****

            sb.Append("\t\t\t\tdefault:\n");
            sb.Append("\t\t\t\t\ttarget = new Resource();\n");
            sb.Append("\t\t\t\t\tbreak;\n");

            // **** close switch ****

            sb.Append("\t\t\t}\n");
            
            // **** populate ****

            sb.Append("\t\t\tserializer.Populate(jObject.CreateReader(), target);\n");
            
            // **** return/close ReadJson ****

            sb.Append("\t\t\treturn target;\n");
            sb.Append("\t\t}\n");

            // **** close class ****

            sb.Append("\t}\n");
            
            // **** success ****

            return true;
        }

        private bool AppendPolymorphicHelpersCore(ref StringBuilder sb)
        {
            //// **** open class ****

            //sb.Append("\tpublic class ResourceConverter : JsonConverter<Resource>\n\t{\n");

            //// **** function CanConvert ****

            //sb.Append("\t\tpublic override bool CanConvert(Type typeToConvert)\n\t\t{\n");
            //sb.Append("\t\t\treturn typeof(Resource).IsAssignableFrom(typeToConvert);\n");
            //sb.Append("\t\t}\n");

            //// **** function Write ****

            //sb.Append("\t\tpublic override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)\n\t\t{\n");
            //sb.Append("\t\t\tbase.Write(writer, value, options);\n");
            //sb.Append("\t\t}\n");

            //// **** function Read ****

            //sb.Append("\t\tpublic override object ReadJson(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)\n\t\t{\n");
            //sb.Append("\t\t\treader.Read();\n");
            //sb.Append("\t\t\tif (reader.TokenType != JsonTokenType.PropertyName)\n");
            //sb.Append("\t\t\t{\n");
            //sb.Append("\t\t\t\tthrow new JsonException();\n");
            //sb.Append("\t\t\t}\n");
            //sb.Append("\t\t\tstring propertyName = reader.GetString();\n");
            //sb.Append("\t\t\tif (propertyName != \"resourceType\")\n");
            //sb.Append("\t\t\t{\n");
            //sb.Append("\t\t\t\tthrow new JsonException();\n");
            //sb.Append("\t\t\t}\n");
            //sb.Append("\t\t\treader.Read();\n");
            //sb.Append("\t\t\tif (reader.TokenType != JsonTokenType.String)\n");
            //sb.Append("\t\t\t{\n");
            //sb.Append("\t\t\t\tthrow new JsonException();\n");
            //sb.Append("\t\t\t}\n");
            //sb.Append("\t\t\tstring resourceType = reader.GetString();\n");
            //sb.Append("\t\t\tResource target;\n");
            //sb.Append("\t\t\tswitch (resourceType)\n");
            //sb.Append("\t\t\t{\n");

            //// **** loop through our types ****

            //foreach (KeyValuePair<string, string> kvp in _exportedResourceNamesAndTypes)
            //{
            //    sb.Append($"\t\t\t\tcase \"{kvp.Key}\":\n");
            //    sb.Append($"\t\t\t\t\ttarget = new {kvp.Value}();\n");
            //    sb.Append("\t\t\t\t\tbreak;\n");
            //}

            //// **** default case returns a Resource object ****

            //sb.Append("\t\t\t\tdefault:\n");
            //sb.Append("\t\t\t\t\ttarget = new Resource();\n");
            //sb.Append("\t\t\t\t\tbreak;\n");

            //// **** close switch ****

            //sb.Append("\t\t\t}\n");

            //// **** populate ****

            //sb.Append("\t\t\tserializer.Populate(jObject.CreateReader(), target);\n");

            //// **** return/close ReadJson ****

            //sb.Append("\t\t\treturn target;\n");
            //sb.Append("\t\t}\n");

            //// **** close class ****

            //sb.Append("\t}\n");

            // **** success ****

            return true;
        }

        bool ILanguangeExporter.AppendModuleClose(ref StringBuilder sb, string outputNamespace)
        {
            // **** check for including polymorphic deserialization helpers ****

            if (PolymorphicDeserialization)
            {
                switch (LanguageStyle)
                {
                    case (int)CSharpStyle.SystemTextJson:
                        if (!AppendPolymorphicHelpersCore(ref sb))
                        {
                            return false;
                        }
                        break;
                    case (int)CSharpStyle.Newtonsoft:
                        if (!AppendPolymorphicHelpersNewtonsoft(ref sb))
                        {
                            return false;
                        }
                        break;
                    case (int)CSharpStyle.Plain:
                    default:
                        // **** not supported ****
                        Console.WriteLine($"Cannot export Polymorphic Deserialization helpers for C#: {GetStyleName}!");
                        break;
                }
            }

            // **** close our namespace ****

            sb.Append($"}} // close namespace: {outputNamespace}\n");
            return true;
        }

        bool ILanguangeExporter.AppendModuleOpen(ref StringBuilder sb, string outputNamespace)
        {
            sb.Append($"namespace {outputNamespace}\n{{\n");
            return true;
        }

        bool ILanguangeExporter.AppendValueSetOpen(ref StringBuilder sb, string sanitizedAlias, string sanitizedName, fhir.ValueSet valueSet)
        {
            // **** start with a comment ****

            if (!string.IsNullOrEmpty(valueSet.Description))
            {
                sb.Append($"\t///<summary>{FhirTypeManager.SanitizeComment(valueSet.Description, _lineComment, _indentChar, 1)}</summary>\n");
            }
            else
            {
                sb.Append($"\t///<summary>Expanded ValueSet from {valueSet.Url}</summary>\n");
            }

            // **** open our value set ****

            sb.Append($"\tpublic abstract class {sanitizedName}\n\t{{\n");

            return true;
        }

        bool ILanguangeExporter.AppendValueSetCodeConcept(
                                        ref StringBuilder sb,
                                        string sanitizedValueSetName,
                                        string sanitizedCodeName,
                                        fhir.CodeSystemConcept concept,
                                        string systemUrl
                                        )
        {
            // **** start with a comment ****

            if (!string.IsNullOrEmpty(concept.Definition))
            {
                sb.Append($"\t\t///<summary>{FhirTypeManager.SanitizeComment(concept.Definition, _lineComment, _indentChar, 2)}</summary>\n");
            }
            else if (!string.IsNullOrEmpty(concept.Display))
            {
                sb.Append($"\t\t///<summary>{FhirTypeManager.SanitizeComment(concept.Display, _lineComment, _indentChar, 2)}</summary>\n");
            }
            else
            {
                sb.Append($"\t\t///<summary>Value for '{concept.Code}'</summary>\n");
            }

            // **** start our coding ****

            sb.Append($"\t\tpublic static readonly Coding {sanitizedCodeName} = new Coding\n\t\t{{\n");
            sb.Append($"\t\t\tCode = \"{concept.Code}\",\n");
            if (!string.IsNullOrEmpty(concept.Display))
            {
                sb.Append($"\t\t\tDisplay = \"{concept.Display.Replace("\"", "\\\"")}\",\n");
            }
            sb.Append($"\t\t\tSystem = \"{systemUrl}\"\n");
            sb.Append($"\t\t}};\n");

            return true;
        }
        bool ILanguangeExporter.AppendValueSetAlias(ref StringBuilder sb, string sanitizedAlias, string sanitizedName, string valueSetUrl)
        {
            sb.Append(
                $"\t///<summary>ValueSet alias for {valueSetUrl}</summary>\n" +
                $"\tpublic abstract class {sanitizedAlias}Values : {sanitizedName} {{ }}\n");
            return true;
        }

        bool ILanguangeExporter.AppendValueSetClose(ref StringBuilder sb, string sanitizedAlias, string sanitizedName, fhir.ValueSet valueSet)
        {
            // **** close our class **** 

            sb.Append($"\t}};\n");
            return true;
        }

        #endregion Interface:ILanguageExporter . . .

    }
}
