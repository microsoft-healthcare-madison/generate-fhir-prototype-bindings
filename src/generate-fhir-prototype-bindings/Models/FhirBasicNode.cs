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


        #endregion Instance Interface . . .

        #region Internal Functions . . .

        #endregion Internal Functions . . .



    }

}
