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

        #endregion Instance Interface . . .

        #region Internal Functions . . .


        #endregion Internal Functions . . .



    }

}
