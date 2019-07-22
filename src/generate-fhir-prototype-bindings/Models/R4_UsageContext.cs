using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class R4_UsageContext
    {
        #region Class Enums . . .

        #endregion Class Enums . . .

        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        public string Code { get; set; }

        public R4_CodeableConcept ValueCodeableConcept { get; set; }

        public R4_Quantity ValueQuantity { get; set; }

        public R4_Range ValueRange { get; set; }

        public string ValueReference { get; set; }
        
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
