using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using generate_fhir_prototype_bindings.Managers;
using generate_fhir_prototype_bindings.Models;
using fhir;
using System.Security.Cryptography.X509Certificates;

namespace generate_fhir_prototype_bindings.Languages
{
    public class LanguageTypeScript : ILanguangeExporter
    {
        #region Class Constants . . .

        private const string _lineComment = " *";
        private const char _indentChar = '\t';
        private const bool _flagOptionals = true;

        #endregion Class Constants . . .

        #region Class Variables . . .

        private static Dictionary<string, string> _fhirLanguageTypeMap = new Dictionary<string, string>()
        {
            { "instant", "string" },
            { "time", "string" },
            { "date", "string" },
            { "dateTime", "string" },
            { "boolean", "boolean" },
            { "decimal", "number" },
            { "integer", "number" },
            { "unsignedInt", "number" },
            { "positiveInt", "number" },
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
            { "uuid", "string" },
        };

        /// <summary>Set of all language reserved words</summary>
        private static HashSet<string> _reservedWordsSet;

        #endregion Class Variables . . .

        #region Instance Variables . . .

        private StringBuilder _valueSetExportSB;
        //private StringBuilder _valueSetInterfaceSB;
        
        #endregion Instance Variables . . .

        #region Constructors . . .

        public LanguageTypeScript()
        {
            _reservedWordsSet = new HashSet<string>()
            {
                "class",
                "const",
                "enum",
                "export",
                "interface",
                "type"
            };
            _valueSetExportSB = new StringBuilder();
            //_valueSetInterfaceSB = new StringBuilder();
        }

        #endregion Constructors . . .

        #region Interface:ILanguageExporter . . .

        Dictionary<string, string> ILanguangeExporter.FhirLanguageTypeMap => _fhirLanguageTypeMap;

        HashSet<string> ILanguangeExporter.ReservedWords => _reservedWordsSet;

        bool ILanguangeExporter.OutputBasicPrimitives => true;

        string ILanguangeExporter.SourceFileExtension => "ts";

        bool ILanguangeExporter.FlagOptionals => _flagOptionals;

        bool ILanguangeExporter.AppendCodes(
                        ref StringBuilder sb,
                        FhirProperty property,
                        string parentName
                        )
        {
            // **** put a comment ****

            sb.Append($"/**\n * Code Values for the {parentName}.{property.Name} field\n */\n");

            // **** open our enum ***

            sb.Append($"export enum {parentName}{property.NameCapitalized}Codes {{\n");

            // **** start adding values ****

            foreach (string code in property.CodeValues)
            {
                FhirTypeManager.SanitizeForCode(code, _reservedWordsSet, out string name, out string value);

                sb.Append($"\t{name.ToUpper()} = \"{value}\",\n");
            }

            // **** close our enum ***

            sb.Append("}\n");

            return true;
        }

        bool ILanguangeExporter.AppendFhirProperty(
                                ref StringBuilder sb,
                                FhirProperty property,
                                string typeName,
                                bool useLowerCaseName
                                )
        {
            // **** always use lower-case start ****

            string name = FhirTypeManager.SanitizeForProperty(property.Name, _reservedWordsSet);

            string comment = FhirTypeManager.SanitizeComment(property.Comment, _lineComment, _indentChar, 2);

            string optionalFlagString = (_flagOptionals && property.IsOptional) ? "?" : "";

            // **** ****

            if (property.IsArray)
            {
                sb.Append(
                    $"\t/**\n" +
                    $"\t * {comment}\n" +
                    //$"\t * Cardinality: {property.Cardinality}\n" +
                    $"\t */\n" +
                    $"\t{name}{optionalFlagString}: {typeName}[];\n" +
                    $"\t/**\n" +
                    $"\t * May contain extended information for property: '{name}'\n" +
                    $"\t */\n" +
                    $"\t_{name}?: Element[];\n"
                    );

                return true;
            }

            sb.Append(
                $"\t/**\n" +
                $"\t * {comment}\n" +
                //$"\t * Cardinality: {property.Cardinality}\n" +
                $"\t */\n" +
                $"\t{name}{optionalFlagString}: {typeName};\n" +
                $"\t/**\n" +
                $"\t * May contain extended information for property: '{name}'\n" +
                $"\t */\n" +
                $"\t_{name}?: Element;\n"
                );

            return true;
        }

        bool ILanguangeExporter.AppendFhirTypeOpen(ref StringBuilder sb, FhirType fhirType)
        {
            string comment = FhirTypeManager.SanitizeComment(fhirType.Comment, _lineComment, _indentChar, 1);

            if ((fhirType.Properties == null) || (fhirType.Properties.Count == 0))
            {
                sb.Append(
                    $"/**\n" +
                    $" * {comment}\n" +
                    $" * From: {fhirType.SourceFilename}\n" +
                    $" */\n" +
                    $"export type {fhirType.Name} = {fhirType.TypeName};\n"
                    );
                return true;
            }

            // **** start with the interface open ****

            if (string.IsNullOrEmpty(fhirType.TypeName) || fhirType.Name.Equals("Element"))
            {
                sb.Append(
                    $"/**\n" +
                    $" * {comment}\n" +
                    $" * From: {fhirType.SourceFilename}\n" +
                    $" */\n" +
                    $"export interface {fhirType.Name} {{\n"
                    );
            }
            else if (fhirType.Name.Equals(fhirType.TypeName, StringComparison.Ordinal))
            {
                sb.Append(
                    $"/**\n" +
                    $" * {comment}\n" +
                    $" * From: {fhirType.SourceFilename}\n" +
                    $" */\n" +
                    $"export interface {fhirType.Name} extends Element {{\n"
                    );
            }
            else
            {
                sb.Append(
                    $"/**\n" +
                    $" * {comment}\n" +
                    $" * From: {fhirType.SourceFilename}\n" +
                    $" */\n" +
                    $"export interface {fhirType.Name} extends {fhirType.TypeName} {{\n"
                    );
            }

            // **** output resource type first (if necessary) ****

            if (FhirTypeManager.DoesTypeRequireResourceTag(fhirType.Name))
            {
                sb.Append(
                    $"\t/** Resource Type Name (for serialization) */\n" +
                    $"\tresourceType: '{fhirType.Name}';\n"
                    );
            }

            return true;
        }

        bool ILanguangeExporter.AppendFhirTypeClose(ref StringBuilder sb, FhirType fhirType)
        {
            if ((fhirType.Properties == null) || (fhirType.Properties.Count == 0))
            {
                return true;
            }

            sb.Append("}\n");
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

            return true;
        }

        bool ILanguangeExporter.AppendFileFooter(ref StringBuilder sb)
        {
            sb.Append($"/** END OF GENERATED FILE **/\n");
            return true;
        }

        bool ILanguangeExporter.AppendModuleOpen(ref StringBuilder sb, string outputNamespace)
        {
            return true;
        }

        bool ILanguangeExporter.AppendModuleClose(ref StringBuilder sb, string outputNamespace)
        {
            return true;
        }

        bool ILanguangeExporter.AppendValueSetOpen(ref StringBuilder sb, string sanitizedAlias, string sanitizedName, ValueSet valueSet)
        {
            // **** clear our related objects for the value set ****

            //_valueSetInterfaceSB.Clear();
            _valueSetExportSB.Clear();

            string comment;

            // **** start with a comment ****

            if (!string.IsNullOrEmpty(valueSet.Description))
            {
                comment = 
                    $"/**\n" +
                    $" * {FhirTypeManager.SanitizeComment(valueSet.Description, _lineComment, _indentChar, 1)}\n" +
                    $" */\n";
            }
            else
            {
                comment = 
                    $"/**\n" +
                    $" * Expanded ValueSet from {valueSet.Url}\n" +
                    $" */\n";
            }

            // **** add our comment ****

            //_valueSetInterfaceSB.Append(comment);
            _valueSetExportSB.Append(comment);

            // **** open our value set (interface has to be before export) ****

            //_valueSetInterfaceSB.Append($"interface {sanitizedName}_Interface {{\n");
            //_valueSetExportSB.Append($"export const {sanitizedName}: {sanitizedName}_Interface = {{\n");
            _valueSetExportSB.Append($"export const {sanitizedName} = {{\n");

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
            string comment;

            // **** start with a comment ****

            if (!string.IsNullOrEmpty(concept.Definition))
            {
                comment = FhirTypeManager.SanitizeComment(concept.Definition, _lineComment, _indentChar, 2);
            }
            else if (!string.IsNullOrEmpty(concept.Display))
            {
                comment = FhirTypeManager.SanitizeComment(concept.Display, _lineComment, _indentChar, 2);
            }
            else
            {
                comment = $"Value for '{concept.Code}'</summary>\n";
            }

            // **** coding is exported inline (before internal and external interfaces) ****

            sb.Append($"const {sanitizedValueSetName}_{sanitizedCodeName}: Coding = {{\n");
            sb.Append($"\t\tcode: \"{concept.Code}\",\n");
            if (!string.IsNullOrEmpty(concept.Display))
            {
                sb.Append($"\t\tdisplay: \"{concept.Display.Replace("\"", "\\\"")}\",\n");
            }
            sb.Append($"\t\tsystem: \"{systemUrl}\"\n");
            sb.Append($"\t}};\n");

            // **** add this code to our interface ****

            //_valueSetInterfaceSB.Append($"\t/**\n\t * {comment}\n\t */\n");
            //_valueSetInterfaceSB.Append($"\t{sanitizedCodeName}: Coding,\n");

            // **** add this code to our export ****

            _valueSetExportSB.Append($"\t/**\n\t * {comment}\n\t */\n");
            _valueSetExportSB.Append($"\t{sanitizedCodeName}: {sanitizedValueSetName}_{sanitizedCodeName},\n");

            return true;
        }

        bool ILanguangeExporter.AppendValueSetClose(ref StringBuilder sb, string sanitizedAlias, string sanitizedName, ValueSet valueSet)
        {
            //_valueSetInterfaceSB.Append("};\n");
            _valueSetExportSB.Append("}\n");

            // **** add our related objects to the normal string now ****

            //sb.Append(_valueSetInterfaceSB);
            //_valueSetInterfaceSB.Clear();

            sb.Append(_valueSetExportSB);
            _valueSetExportSB.Clear();

            return true;
        }

        bool ILanguangeExporter.AppendValueSetAlias(ref StringBuilder sb, string sanitizedAlias, string sanitizedName, string valueSetUrl)
        {
            sb.Append(
                $"/*\n * ValueSet alias for {valueSetUrl}\n */\n" +
                $"export const {sanitizedAlias}Values = {sanitizedName};\n"
                );
            return true;
        }

        #endregion Interface:ILanguageExporter . . .
    }
}