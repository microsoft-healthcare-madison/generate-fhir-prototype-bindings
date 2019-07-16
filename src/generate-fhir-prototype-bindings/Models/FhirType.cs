using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace generate_fhir_prototype_bindings.Models
{
    public class FhirType : FhirBasicNode
    {
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

        public override string GetTypeScriptString()
        {
            // **** determine if this is a basic type or one with properties ****

            if ((Properties == null) || (Properties.Count == 0))
            {
                return GetTypeScriptStringBasic();
            }

            return GetTypeScriptStringComplex();
        }


        #endregion Instance Interface . . .

        #region Internal Functions . . .


        private string GetTypeScriptStringBasic()
        {
            return $"/**\n * {Comment}\n * From: {SourceFilename}.xml\n */\ntype {Name} = {TypeName};\n";
        }

        private string GetTypeScriptStringComplex()
        {
            StringBuilder sb = new StringBuilder();

            // **** start with the interface open ****

            if (string.IsNullOrEmpty(TypeName) || Name.Equals("Element"))
            {
                sb.Append($"/**\n * {Comment}\n * From: {SourceFilename}.xml\n */\n{_typeScriptImplementationType} {Name} {{\n");
            }
            else if (Name.Equals(TypeName, StringComparison.Ordinal))
            {
                sb.Append($"/**\n * {Comment}\n * From: {SourceFilename}.xml\n */\n{_typeScriptImplementationType} {Name} extends Element {{\n");
            }
            else
            {
                sb.Append($"/**\n * {Comment}\n * From: {SourceFilename}.xml\n */\n{_typeScriptImplementationType} {Name} extends {TypeName} {{\n");
            }

            // **** output the properties of this type (alphebetically) ****

            List<string> nodeNames = Properties.Keys.ToList<string>();
            nodeNames.Sort();

            foreach (string name in nodeNames)
            {
                // **** append this field's string ****

                sb.Append(Properties[name].GetTypeScriptString());
            }

            // **** close our interface ****

            sb.Append("}\n");

            // **** return our string ****

            return sb.ToString();
        }


        #endregion Internal Functions . . .



    }

}
