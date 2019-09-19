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


        #endregion Class Constants . . .

        #region Class Variables . . .

        /// <summary>The language primitive type names for C# (see FhirBasicNode.LanguagePrimitiveType for order).</summary>
        private static string[] _equivalentJsonTypes = {
            "",
            "string",
            "decimal",
            "bool",
            "DateTime"
        };

        /// <summary>Set of all language reserved words</summary>
        private static HashSet<string> _reservedWordsSet;

        #endregion Class Variables . . .

        #region Instance Variables . . .

        public bool IncludeNewtonsoftAnnotations { get; set; } = true;

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
        }

        #endregion Constructors . . .

        #region Interface:ILanguageExporter . . .

        string[] ILanguangeExporter.LanguageJsonTypes => _equivalentJsonTypes;

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
                if (IncludeNewtonsoftAnnotations)
                {
                    propertyName = namePascal;
                    extendedName = namePascal;
                }
                else
                {
                    propertyName = namePascal;
                    extendedName = nameCamel;
                }
            }

            string propertyAnnotation = "";
            string extendedAnnotation = "";

            if (IncludeNewtonsoftAnnotations)
            {
                propertyAnnotation = $"\t\t[JsonProperty(PropertyName = \"{nameCamel}\")]\n";
                extendedAnnotation = $"\t\t[JsonProperty(PropertyName = \"_{nameCamel}\")]\n";
            }

            string comment = FhirTypeManager.SanitizeComment(property.Comment, _lineComment, _indentChar, 2);

            string optionalFlagString = (_flagOptionals && property.IsOptional) ? "?" : "";

            // **** nullable reference types are not allowed in current C# ****

            switch (typeName)
            {
                case "bool":
                case "decimal":
                    // **** ignore - use current style ****
                break;
                default:
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
                sb.Append(
                    $"\t\t///<summary>Resource Type Name (for serialization)</summary>\n" +
                    $"\t\tpublic string ResourceType => \"{fhirType.Name}\";\n"
                    );
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
            
            if (IncludeNewtonsoftAnnotations)
            {
                sb.Append("using System;\nusing System.Collections.Generic;\nusing Newtonsoft.Json;\n\n");
            }
            else
            {
                sb.Append("using System;\nusing System.Collections.Generic;\n\n");
            }

            return true;
        }

        bool ILanguangeExporter.AppendFileFooter(ref StringBuilder sb)
        {
            sb.Append($"/** END OF GENERATED FILE **/\n");
            return true;
        }
        bool ILanguangeExporter.AppendModuleClose(ref StringBuilder sb, string outputNamespace)
        {
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
