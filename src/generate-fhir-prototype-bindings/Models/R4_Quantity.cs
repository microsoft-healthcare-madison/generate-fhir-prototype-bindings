using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class R4_Quantity
    {
        #region Class Enums . . .

        public enum QuantityComparator {
            LessThan,
            LessThanOrEquals,
            GreaterThanOrEquals,
            GreaterThan,
            Equals,
            NotEqual,
        }

        public string[] QuantityComparatorCodes = {
            "<",
            "<=",
            ">=",
            ">",
            "=",
            "!="
        };

        #endregion Class Enums . . .

        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        public decimal? Value { get; set; }

        public string Comparator { get; set; }
        
        public string Unit { get; set; }

        public string System { get; set; }

        public string Code { get; set; }

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
