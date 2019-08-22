using generate_fhir_prototype_bindings.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class FhirProperty : FhirBasicNode
    {
        #region Class Constants . . . 

        #endregion Class Constants . . .

        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets a value indicating whether this object is array.</summary>
        ///
        /// <value>True if this object is array, false if not.</value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsArray { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets a value indicating whether this object is optional.</summary>
        ///
        /// <value>True if this object is optional, false if not.</value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsOptional { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the code values.</summary>
        ///
        /// <value>The code values.</value>
        ///-------------------------------------------------------------------------------------------------

        public string[] CodeValues { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the value set key.</summary>
        ///
        /// <value>The value set key.</value>
        ///-------------------------------------------------------------------------------------------------

        public string ValueSetUrl { get; set; }

        #endregion Instance Variables . . .

        #region Constructors . . .

        public static FhirProperty CreateProperty(
                                                string name,
                                                string typeName,
                                                string comment,
                                                string cardinality,
                                                string[] codeValues = null,
                                                string valueSetUrl = null
                                                )
        {
            // **** do not allow properties without names ****

            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            // **** handle special case of TypeRef (not a type, but used as one) ****

            if (typeName.Equals("=TypeRef", StringComparison.Ordinal) || typeName.Equals("Type"))
            {
                typeName = "Element";
            }

            // **** handle special case of having an @ (references internal type definition) ****

            if (typeName.StartsWith('@'))
            {
                // **** remove the '@' ****

                typeName = typeName.Substring(1);

                // **** split based on path components ****

                string[] typeParts = typeName.Split('.');

                // **** rejoin with pascal case ****

                typeName = typeParts[0];

                // **** already added first part, since it will have been capitalized ****

                for (int partIndex = 1; partIndex < typeParts.Length; partIndex++)
                {
                    if (string.IsNullOrEmpty(typeParts[partIndex]))
                    {
                        continue;
                    }

                    typeName += typeParts[partIndex].Substring(0, 1).ToUpper() + typeParts[partIndex].Substring(1);
                }
            }
            else if ((typeName.StartsWith('=')) || (typeName.StartsWith('#')))
            {
                // **** this should be formatted correctly already ****

                typeName = typeName.Substring(1);
            }

            // **** remove any spaces ****

            typeName = typeName.Replace(" ", "");

            // **** create our field ****

            FhirProperty node = new FhirProperty()
            {
                Name = name,
                NameCapitalized = string.Concat(name.Substring(0, 1).ToUpper(), name.Substring(1)),
                Comment = comment,
                TypeName = typeName,
                IsArray = (!string.IsNullOrEmpty(cardinality) && !cardinality.EndsWith('1')) ? true : false,
                IsOptional = (cardinality.StartsWith("0")) ? true : false,
                CodeValues = codeValues,
                ValueSetUrl = valueSetUrl,
            };

            return node;
        }

        public static FhirProperty CreateProperty(
                                                string name,
                                                string typeName,
                                                string comment,
                                                bool isArray,
                                                bool isOptional
                                                )
        {
            // **** do not allow properties without names ****

            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            // **** handle special case of TypeRef (not a type, but used as one) ****

            if (typeName.Equals("=TypeRef", StringComparison.Ordinal) || typeName.Equals("Type"))
            {
                typeName = "Element";
            }

            // **** handle special case of having an @ (references internal type definition) ****

            if (typeName.StartsWith('@'))
            {
                // **** remove the '@' ****

                typeName = typeName.Substring(1);

                // **** split based on path components ****

                string[] typeParts = typeName.Split('.');

                // **** rejoin with pascal case ****

                typeName = typeParts[0];

                // **** already added first part, since it will have been capitalized ****

                for (int partIndex = 1; partIndex < typeParts.Length; partIndex++)
                {
                    if (string.IsNullOrEmpty(typeParts[partIndex]))
                    {
                        continue;
                    }

                    typeName += typeParts[partIndex].Substring(0, 1).ToUpper() + typeParts[partIndex].Substring(1);
                }
            }
            else if (typeName.StartsWith('='))
            {
                // **** this should be formatted correctly already ****

                typeName = typeName.Substring(1);
            }

            // **** remove any spaces ****

            typeName = typeName.Replace(" ", "");

            // **** create our field ****

            FhirProperty node = new FhirProperty()
            {
                Name = name,
                NameCapitalized = string.Concat(name.Substring(0, 1).ToUpper(), name.Substring(1)),
                Comment = comment,
                TypeName = typeName,
                IsArray = isArray,
                IsOptional = isOptional,
            };

            return node;
        }

        #endregion Constructors . . .

        #region Class Interface . . .

        #endregion Class Interface . . .

        #region Instance Interface . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets a typescript string representing this node.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <returns>The TypeScript definition string.</returns>
        ///-------------------------------------------------------------------------------------------------

        public override string GetTypeScriptString(bool excludeCodes = false)
        {
            // **** skip fields with no type (are defined in base object) ****

            if (string.IsNullOrEmpty(TypeName))
            {
                return "";
            }

            string comment = Comment.Replace("\n", "\n\t * ").Replace("\r", "");

            string OptionalFlagString = IsOptional ? "?" : "";

            // **** ****

            if (IsArray)
            {
                return $"\t/**\n\t * {comment}\n\t */\n\t{Name}{OptionalFlagString}: {TypeName}[];\n" +
                       $"\t/**\n\t * May contain extended information for property: '{Name}'\n\t */\n\t_{Name}?: Element[];\n";
            }

            return $"\t/**\n\t * {comment}\n\t */\n\t{Name}{OptionalFlagString}: {TypeName};\n" +
                   $"\t/**\n\t * May contain extended information for property: '{Name}'\n\t */\n\t_{Name}?: Element;\n";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets a TypeScript enum string for code values</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="parentName">Name of the parent.</param>
        ///
        /// <returns>The type script code enum.</returns>
        ///-------------------------------------------------------------------------------------------------

        public string GetTypeScriptCodesString(string parentName)
        {
            if ((CodeValues == null) || (CodeValues.Length == 0))
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            // **** put a comment ****

            sb.Append($"/**\n * Code Values for the {parentName}.{Name} field\n */\n");

            // **** open our enum ***

            sb.Append($"export enum {parentName}{NameCapitalized}Codes {{\n");

            // **** start adding values ****

            foreach (string code in CodeValues)
            {
                MangleCodeForOutput(code, out string name, out string value);

                sb.Append($"\t{name} = \"{value}\",\n");
            }

            // **** close our enum ***

            sb.Append("}\n");

            // **** return our string ****

            return sb.ToString();
        }

        public string GetTypeScriptValueSetString(string parentName)
        {
            // **** return our string ****

            return FhirTypeManager.GetTypeScriptValueSetString($"{parentName}{NameCapitalized}", ValueSetUrl);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Mangle code for output.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="input">The input.</param>
        /// <param name="name"> [out] The name.</param>
        /// <param name="value">[out] The value.</param>
        ///-------------------------------------------------------------------------------------------------

        private void MangleCodeForOutput(string input, out string name, out string value)
        {
            name = input.Trim();

            if (name.Contains(" "))
            {
                name = name.Substring(0, name.IndexOf(" "));
            }

            value = name;

            switch (name)
            {
                case "<":
                    name = "LESS_THAN";
                    break;
                case "<=":
                    name = "LESS_THAN_OR_EQUAL";
                    break;
                case ">=":
                    name = "GREATER_THAN_OR_EQUAL";
                    break;
                case ">":
                    name = "GREATER_THAN";
                    break;
                case "=":
                    name = "EQUALS";
                    break;
                default:
                    name = name.ToUpper().Replace("-", "_").Replace("/", "_").Replace(".", "");
                    break;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets C# string.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="languagePrimitiveDict">Dictionary of language primitives.</param>
        /// <param name="useLowerCaseName">     (Optional) True to use lower case name.</param>
        ///
        /// <returns>The C# string.</returns>
        ///-------------------------------------------------------------------------------------------------

        public override string GetCSharpString(
                                                Dictionary<string, LanguagePrimitiveType> languagePrimitiveDict, 
                                                bool useLowerCaseName = false,
                                                bool excludeCodes = false
                                                )
        {
            // **** skip fields with no type (are defined in base object) ****

            if (string.IsNullOrEmpty(TypeName))
            {
                return "";
            }

            string name = useLowerCaseName ? Name : NameCapitalized;
            string comment = Comment.Replace("\n", "\n\t\t/// ");
            comment = comment.Replace("\r", "");

            string typeName = TypeName.Trim().ToLower();

            // **** check for overriding the type name ****

            typeName = languagePrimitiveDict.ContainsKey(typeName) ? LanguagePrimitiveTypeNamesCS[(int)languagePrimitiveDict[typeName]] : TypeName;

            // **** ****

            if (IsArray)
            {
                return $"\t\t///<summary>{comment}</summary>\n\t\tpublic {typeName}[] {name} {{ get; set; }}\n" +
                       $"\t\t///<summary>May contain extended information for property: '{name}'</summary>\n\t\tpublic Element[] _{name} {{ get; set; }}\n";
            }

            return $"\t\t///<summary>{comment}</summary>\n\t\tpublic {typeName} {name} {{ get; set; }}\n" +
                $"\t\t///<summary>May contain extended information for property: '{name}'</summary>\n\t\tpublic Element _{name} {{ get; set; }}\n";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets C# code enum.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="parentName">Name of the parent.</param>
        ///
        /// <returns>The C# code enum.</returns>
        ///-------------------------------------------------------------------------------------------------

        public string GetCSharpCodesString(string parentName)
        {
            if ((CodeValues == null) || (CodeValues.Length == 0))
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            // **** put a comment ****

            sb.Append($"\t///<summary>Code Values for the {parentName}.{Name} field</summary>\n");

            // **** open our enum ***

            sb.Append($"\tpublic sealed class {parentName}{NameCapitalized}Codes {{\n");

            // **** start adding values ****

            foreach (string code in CodeValues)
            {
                MangleCodeForOutput(code, out string name, out string value);

                sb.Append($"\t\tpublic const string {name} = \"{value}\";\n");
            }

            // **** close our enum ***

            sb.Append("\t}\n");

            // **** return our string ****

            return sb.ToString();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets C# code system string.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="parentName">Name of the parent.</param>
        ///
        /// <returns>The C# code system string.</returns>
        ///-------------------------------------------------------------------------------------------------

        public string GetCSharpValueSetString(string parentName)
        {
            // **** return our string ****

            return FhirTypeManager.GetCSharpValueSetString($"{parentName}{NameCapitalized}", ValueSetUrl);
        }


        #endregion Instance Interface . . .

        #region Internal Functions . . .


        #endregion Internal Functions . . .



    }

}
