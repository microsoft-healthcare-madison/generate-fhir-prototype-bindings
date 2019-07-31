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
            TypeString,
            TypeNumber,
            TypeBoolean,
            TypeDateTime
        }

        /// <summary>The language primitive type names for C#.</summary>
        public static string[] LanguagePrimitiveTypeNamesCS = {
            "",
            "string",
            "decimal",
            "bool",
            "DateTime"
        };

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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets type script string.</summary>
        ///
        /// <remarks>Gino Canessa, 7/12/2019.</remarks>
        ///
        /// <returns>The type script string.</returns>
        ///-------------------------------------------------------------------------------------------------

        public virtual string GetTypeScriptString()
        {
            return string.Empty;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets C# string.</summary>
        ///
        /// <remarks>Gino Canessa, 7/31/2019.</remarks>
        ///
        /// <returns>The C# string.</returns>
        ///-------------------------------------------------------------------------------------------------

        public virtual string GetCSharpString(Dictionary<string, LanguagePrimitiveType> languagePrimitiveDict, bool useLowerCaseName = false)
        {
            return string.Empty;
        }

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

        internal static LanguagePrimitiveType GetLanguagePrimitive(string typeName)
        {
            typeName = typeName.Trim().ToLower();

            if (typeName.Equals("string", StringComparison.Ordinal))
            {
                return LanguagePrimitiveType.TypeString;
            }

            if (typeName.Equals("number", StringComparison.Ordinal))
            {
                return LanguagePrimitiveType.TypeNumber;
            }

            if (typeName.Equals("boolean", StringComparison.Ordinal))
            {
                return LanguagePrimitiveType.TypeBoolean;
            }

            if (typeName.Equals("datetime", StringComparison.Ordinal))
            {
                return LanguagePrimitiveType.TypeDateTime;
            }

            return LanguagePrimitiveType.None;
        }


        #endregion Internal Functions . . .



    }

}
