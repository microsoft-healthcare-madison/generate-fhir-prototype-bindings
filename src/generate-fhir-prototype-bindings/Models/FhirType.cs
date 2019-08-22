using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using generate_fhir_prototype_bindings.Managers;

namespace generate_fhir_prototype_bindings.Models
{ 
    public class FhirType : FhirBasicNode
    {
        #region Class Enums . . .

        public enum StructureDefinitionKind
        {
            PrimitiveType = 0,
            ComplexType,
            Resource,
            Logical
        }

        public string[] StructureDefinitionKindCodes = {
            "primitive-type",
            "complex-type",
            "resource",
            "logical"
        };

        #endregion Class Enums . . .

        #region Class Constants . . . 

        #endregion Class Constants . . .

        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets a value indicating whether this object is a circular reference.</summary>
        ///
        /// <value>True if this object is circular, false if not.</value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsCircular { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the properties for this type.</summary>
        ///
        /// <value>The properties.</value>
        ///-------------------------------------------------------------------------------------------------

        public Dictionary<string, FhirProperty> Properties { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets a value indicating whether this object is fhir primitive.</summary>
        ///
        /// <value>True if this object is fhir primitive, false if not.</value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsFhirPrimitive { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the language primitive.</summary>
        ///
        /// <value>The language primitive.</value>
        ///-------------------------------------------------------------------------------------------------

        public LanguagePrimitiveType LanguagePrimitive { get; set; }

        #endregion Instance Variables . . .

        #region Constructors . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Creates fhir type.</summary>
        ///
        /// <remarks>Gino Canessa, 7/12/2019.</remarks>
        ///
        /// <param name="name">           The name.</param>
        /// <param name="elementPath">    The element path to the current field.</param>
        /// <param name="typeName">       Name of the type.</param>
        /// <param name="comment">        The comment.</param>
        /// <param name="sourceFilename"> Filename of the source file.</param>
        /// <param name="isFhirPrimitive">True if this object is fhir primitive, false if not.</param>
        ///
        /// <returns>The new fhir type.</returns>
        ///-------------------------------------------------------------------------------------------------

        public static FhirType CreateFhirType(
                                                string name,
                                                string elementPath,
                                                string typeName,
                                                string comment,
                                                string sourceFilename,
                                                bool isFhirPrimitive
                                                )
        {
            // **** cannot have empty names or types here, must be resolved prior to creating node ****

            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            //if (typeName.Equals("Type"))
            if (typeName.Equals("=TypeRef", StringComparison.Ordinal) || typeName.Equals("Type"))
            {
                //typeName = "Element";
                typeName = elementPath;
            }

            // **** create this node ****

            FhirType node = new FhirType()
            {
                Name = name,
                NameCapitalized = string.Concat(name.Substring(0, 1).ToUpper(), name.Substring(1)),
                Comment = comment,
                TypeName = typeName,
                IsCircular = (name.Equals(typeName, StringComparison.OrdinalIgnoreCase)) ? true : false,
                SourceFilename = sourceFilename,
                Properties = new Dictionary<string, FhirProperty>(),
                IsFhirPrimitive = isFhirPrimitive,
                LanguagePrimitive = GetLanguagePrimitive(typeName)
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
            // **** determine if this is a basic type or one with properties ****

            if ((Properties == null) || (Properties.Count == 0))
            {
                return GetTypeScriptStringBasic();
            }

            return GetTypeScriptStringComplex(excludeCodes);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets C# string.</summary>
        ///
        /// <remarks>Gino Canessa, 8/21/2019.</remarks>
        ///
        /// <param name="languagePrimitiveDict">Dictionary of language primitives.</param>
        /// <param name="useLowerCaseName">     (Optional) True to use lower case name.</param>
        /// <param name="excludeCodes">             (Optional) True to exclude, false to include the
        ///                                         codes.</param>
        ///
        /// <returns>The C# string.</returns>
        ///-------------------------------------------------------------------------------------------------

        public override string GetCSharpString(
                                                Dictionary<string, LanguagePrimitiveType> languagePrimitiveDict, 
                                                bool useLowerCaseName = false,
                                                bool excludeCodes = false
                                                )
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder enumSB = new StringBuilder();
            StringBuilder valueSetSB = new StringBuilder();

            string comment = Comment.Replace("\n", "\n/// ").Replace("\r", "");

            // **** start with the interface open ****

            if (string.IsNullOrEmpty(TypeName) || Name.Equals("Element"))
            {
                sb.Append($"\t///<summary>\n\t///{comment}\n\t///</summary>\n\t///<source-file>{SourceFilename}</source-file>\n\tpublic class {NameCapitalized}\n\t{{\n");
            }
            else if (Name.Equals(TypeName, StringComparison.Ordinal))
            {
                sb.Append($"\t///<summary>\n\t///{comment}\n\t///</summary>\n\t///<source-file>{SourceFilename}</source-file>\n\tpublic class {NameCapitalized} : Element\n\t{{\n");
            }
            else
            {
                sb.Append($"\t///<summary>\n\t///{comment}\n\t///</summary>\n\t///<source-file>{SourceFilename}</source-file>\n\tpublic class {NameCapitalized} : {TypeName}\n\t{{\n");
            }

            // **** output resource type first (if necessary) ****

            if (FhirTypeManager.DoesTypeRequireResourceTag(Name))
            {
                sb.Append($"\t\t///<summary>Resource Type Name (for serialization)</summary>\n\t\tpublic string ResourceType => \"{Name}\";\n");
            }

            // **** grab our name in lower case for fast comparison ****

            string nameLower = Name.ToLower();

            // **** output the properties of this type (alphebetically) ****

            List<string> nodeNames = Properties.Keys.ToList<string>();
            nodeNames.Sort();

            foreach (string name in nodeNames)
            {
                // **** append this field's string ****

                sb.Append(Properties[name].GetCSharpString(languagePrimitiveDict, name.Equals(nameLower, StringComparison.Ordinal)));
                
                // **** check for having a code value list ****

                if ((!excludeCodes) && (Properties[name].CodeValues != null))
                {
                    enumSB.Append(Properties[name].GetCSharpCodesString(NameCapitalized));
                }

                // **** check for having a Value Set ****

                if (!string.IsNullOrEmpty(Properties[name].ValueSetUrl))
                {
                    valueSetSB.Append(Properties[name].GetCSharpValueSetString(NameCapitalized));
                }
            }

            // **** close our interface ****

            sb.Append("\t}\n");

            // **** return our string ****

            return valueSetSB.ToString() + enumSB.ToString() + sb.ToString();
        }


        #endregion Instance Interface . . .

        #region Internal Functions . . .
        
        private string GetTypeScriptStringBasic()
        {

            string comment = Comment.Replace("\n", "\n * ").Replace("\r", "");
            return $"/**\n * {comment}\n * From: {SourceFilename}\n */\nexport type {Name} = {TypeName};\n";
        }

        private string GetTypeScriptStringComplex(bool excludeCodes)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder enumSB = new StringBuilder();
            StringBuilder valueSetSB = new StringBuilder();

            string comment = Comment.Replace("\n", "\n * ").Replace("\r", "");

            // **** start with the interface open ****

            if (string.IsNullOrEmpty(TypeName) || Name.Equals("Element"))
            {
                sb.Append($"/**\n * {comment}\n * From: {SourceFilename}\n */\nexport {_typeScriptImplementationType} {Name} {{\n");
            }
            else if (Name.Equals(TypeName, StringComparison.Ordinal))
            {
                sb.Append($"/**\n * {comment}\n * From: {SourceFilename}\n */\nexport {_typeScriptImplementationType} {Name} extends Element {{\n");
            }
            else
            {
                sb.Append($"/**\n * {comment}\n * From: {SourceFilename}\n */\nexport {_typeScriptImplementationType} {Name} extends {TypeName} {{\n");
            }

            // **** output resource type first (if necessary) ****

            if (FhirTypeManager.DoesTypeRequireResourceTag(Name))
            {
                sb.Append($"\t/** Resource Type Name (for serialization) */\n\tresourceType: '{Name}';\n");
            }

            // **** output the properties of this type (alphebetically) ****

            List<string> nodeNames = Properties.Keys.ToList<string>();
            nodeNames.Sort();

            foreach (string name in nodeNames)
            {
                // **** append this field's string ****

                sb.Append(Properties[name].GetTypeScriptString());

                // **** check for having a code value list ****

                if ((!excludeCodes) && (Properties[name].CodeValues != null))
                {
                    enumSB.Append(Properties[name].GetTypeScriptCodesString(NameCapitalized));
                }

                // **** check for having a Value Set ****

                if (!string.IsNullOrEmpty(Properties[name].ValueSetUrl))
                {
                    valueSetSB.Append(Properties[name].GetTypeScriptValueSetString(NameCapitalized));
                }
            }

            // **** close our interface ****

            sb.Append("}\n");

            // **** return our string ****

            return valueSetSB.ToString() + enumSB.ToString() + sb.ToString();
        }


        #endregion Internal Functions . . .

    }

}
