using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class R4_Identifier
    {
        #region Class Enums . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Values that represent identifier uses.</summary>
        ///
        /// <remarks>Gino Canessa, 7/17/2019.</remarks>
        ///-------------------------------------------------------------------------------------------------

        public enum IdentifierUse {
            usual = 0,
            official,
            temp,
            secondary,
            old
        }

        /// <summary>The identifier use codes.</summary>
        public string[] IdentifierUseCodes = {
            "usual",
            "official",
            "temp",
            "secondary",
            "old"
        };
        
        #endregion Class Enums . . .

        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        public string Id { get; set; }

        public string Use { get; set; }

        public R4_CodeableConcept Type { get; set; }

        public string System { get; set; }

        public string Value { get; set; }

        public R4_Period Period { get; set; }

        public string Assigner { get; set; }
        
        #endregion Instance Variables . . .

        #region Constructors . . .

        #endregion Constructors . . .

        #region Class Interface . . .

        #endregion Class Interface . . .

        #region Instance Interface . . .

        #endregion Instance Interface . . .

        #region Internal Functions . . .

        #endregion Internal Functions . . .

    }
}
