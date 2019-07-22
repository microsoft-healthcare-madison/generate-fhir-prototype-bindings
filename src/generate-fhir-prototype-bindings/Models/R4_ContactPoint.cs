using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class R4_ContactPoint
    {
        #region Class Enums . . .

        public enum ContactPointSystem {
            Phone = 0,
            Fax,
            Email,
            Pager,
            Url,
            Sms,
            Other
        }

        public string[] ContactPointSystemCodes = {
            "phone",
            "fax",
            "email",
            "pager",
            "url",
            "sms",
            "other"
        };

        public enum ContactPointUse {
            Home = 0,
            Work,
            Temp,
            Old,
            Mobile
        }

        public string[] ContactPointUseCodes = {
            "home",
            "work",
            "temp",
            "old",
            "mobile"
        };

        #endregion Class Enums . . .

        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        public string System { get; set; }

        public string Value { get; set; }

        public string Use { get; set; }

        public int? Rank { get; set; }

        public R4_Period Period { get; set; }

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
