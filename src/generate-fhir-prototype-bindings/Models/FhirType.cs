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

        #endregion Instance Interface . . .

        #region Internal Functions . . .
        


        #endregion Internal Functions . . .

    }

}
