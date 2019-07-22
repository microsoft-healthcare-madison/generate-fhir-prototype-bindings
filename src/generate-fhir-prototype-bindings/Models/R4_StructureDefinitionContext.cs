using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class R4_StructureDefinitionContext
    {
        #region Class Enums . . .

        public enum ExtensionContextType {
            FhirPath,
            Element,
            Extension
        }

        public string[] ExtensionContextTypeCodes = {
            "fhirpath",
            "element",
            "extension"
        };

        #endregion Class Enums . . .

        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        public string Type { get; set; }

        public string Expression { get; set; }
        
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
