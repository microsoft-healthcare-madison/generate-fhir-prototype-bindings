using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class FhirBasicNode
    {
        #region Class Constants . . . 

        internal const string _typeScriptImplementationType = "interface";   // class | interface

        #endregion Class Constants . . .

        #region Class Enums . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Values that represent language primitive types.</summary>
        ///
        /// <remarks>Gino Canessa, 7/31/2019.</remarks>
        ///-------------------------------------------------------------------------------------------------

        public enum LanguagePrimitiveType : int
        {
            None,
            TypeDateTime,
            TypeBoolean,
            TypeNumber,
            TypeString,
        }

        public const int FhirPrimitiveCount = 20;

        public static string[] FhirPrimitives = {

            "instant",
            "time",
            "date",
            "dateTime",

            "boolean",
            
            "decimal",
            "integer",
            "unsignedInt",
            "positiveInt",
            
            "base64Binary",
            "url",
            "code",
            "string",
            "uri",
            "canonical",
            "markdown",
            "id",
            "oid",
            "xhtml",

            "uuid",
        };

        //public static string[] JsonLanguagePrimitives = {
        //    "",
        //    "string",
        //    "number",
        //    "boolean",
        //    "datetime",
        //    "number",
        //    "number",
        //    "number",
        //};

        #endregion Class Enums . . .

        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the name.</summary>
        ///
        /// <value>The name.</value>
        ///-------------------------------------------------------------------------------------------------

        public string Name { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the name capitalized (for joining into pascal case).</summary>
        ///
        /// <value>The name capitalized.</value>
        ///-------------------------------------------------------------------------------------------------

        public string NameCapitalized { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the comment.</summary>
        ///
        /// <value>The comment.</value>
        ///-------------------------------------------------------------------------------------------------

        public string Comment { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the type name.</summary>
        ///
        /// <value>The type of the base.</value>
        ///-------------------------------------------------------------------------------------------------

        public string TypeName { get; set; }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the name of the file which this node was derived from.</summary>
        ///
        /// <value>The file source.</value>
        ///-------------------------------------------------------------------------------------------------

        public string SourceFilename { get; set; }

        #endregion Instance Variables . . .

        #region Constructors . . .

        #endregion Constructors . . .

        #region Class Interface . . .

        #endregion Class Interface . . .

        #region Instance Interface . . .

        #endregion Instance Interface . . .

        #region Internal Functions . . .


        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets language primitive.</summary>
        ///
        /// <remarks>Gino Canessa, 7/31/2019.</remarks>
        ///
        /// <param name="typeName">Name of the type.</param>
        ///
        /// <returns>The language primitive.</returns>
        ///-------------------------------------------------------------------------------------------------

        internal static int GetLanguagePrimitiveIndex(string name, string typeName)
        {
            name = name.Trim().ToLower();
            typeName = typeName.Trim().ToLower();

            for (int index = 0; index < FhirPrimitiveCount; index++)
            {
                if ((name.Equals(FhirPrimitives[index], StringComparison.Ordinal)) ||
                    (typeName.Equals(FhirPrimitives[index], StringComparison.Ordinal)))
                {
                    return index;
                }
            }

            return -1;
        }


        #endregion Internal Functions . . .



    }

}
