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

        #endregion Instance Variables . . .

        #region Constructors . . .

        public static FhirProperty CreateProperty(
                                                string name,
                                                string typeName,
                                                string comment,
                                                string cardinality
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

                // **** rejoin with camel case ****

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
                //// **** this is a hint which should be removed ****

                //typeName = "";
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
            // **** skip fields with no type (are defined in base object) ****

            if (string.IsNullOrEmpty(TypeName))
            {
                return "";
            }

            string OptionalFlagString = IsOptional ? "?" : "";

            // **** ****

            if (IsArray)
            {
                return $"\t/**\n\t * {Comment}\n\t */\n\t{Name}{OptionalFlagString}: {TypeName}[];\n" +
                       $"\t/**\n\t * May contain extended information for property: '{Name}'\n\t */\n\t_{Name}?: Element[];\n";
            }

            return $"\t/**\n\t * {Comment}\n\t */\n\t{Name}{OptionalFlagString}: {TypeName};\n" +
                   $"\t/**\n\t * May contain extended information for property: '{Name}'\n\t */\n\t_{Name}?: Element;\n";
        }


        #endregion Instance Interface . . .

        #region Internal Functions . . .


        #endregion Internal Functions . . .



    }

}
