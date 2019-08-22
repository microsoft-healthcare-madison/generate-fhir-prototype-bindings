using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class FhirCodeConcept
    {
        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        public string Code { get; set; }
        public string Display { get; set; }
        public string Definition { get; set; }

        #endregion Instance Variables . . .

        #region Constructors . . .

        #endregion Constructors . . .

        #region Class Interface . . .

        #endregion Class Interface . . .

        #region Instance Interface . . .


        public string GetTypeScriptString(string system)
        {
            StringBuilder sb = new StringBuilder();

            string comment;

            if (!string.IsNullOrEmpty(Definition))
            {
                comment = Definition.Replace("\n", "\n\t * ").Replace("\r", "");
            }
            else if (!string.IsNullOrEmpty(Display))
            {
                comment = Display.Replace("\n", "\n\t * ").Replace("\r", "");
            }
            else
            {
                comment = $"Coding for '{Code}'";
            }

            // **** start with a comment ****

            sb.Append($"\t/*\n\t * {comment}\n\t */\n");

            // **** start our coding ****

            sb.Append($"\t{Code.Replace("-", "_")} = () => {{\n");
            sb.Append($"\t\tcode = \"{Code}\",\n");
            if (!string.IsNullOrEmpty(Display))
            {
                sb.Append($"\t\tdisplay = \"{Display}\",\n");
            }
            sb.Append($"\t\tsystem = \"{system}\"\n");
            sb.Append($"\t}};\n");

            // **** return our string ****

            return sb.ToString();
        }


        public string GetCSharpString(string system)
        {
            StringBuilder sb = new StringBuilder();

            string comment;

            if (!string.IsNullOrEmpty(Definition))
            {
                comment = Definition.Replace("\n", "\n\t\t/// ").Replace("\r", "");
            }
            else if (!string.IsNullOrEmpty(Display))
            {
                comment = Display.Replace("\n", "\n\t\t/// ").Replace("\r", "");
            }
            else
            {
                comment = $"Coding for '{Code}'";
            }

            // **** start with a comment ****

            sb.Append($"\t\t///<summary>{comment}</summary>\n");

            // **** start our coding ****

            sb.Append($"\t\tpublic readonly Coding {Code.Replace("-", "_")} = new Coding\n\t\t{{\n");
            sb.Append($"\t\t\tCode = \"{Code}\",\n");
            if (!string.IsNullOrEmpty(Display))
            {
                sb.Append($"\t\t\tDisplay = \"{Display}\",\n");
            }
            sb.Append($"\t\t\tSystem = \"{system}\"\n");
            sb.Append($"\t\t}};\n");

            // **** return our string ****

            return sb.ToString();
        }

        #endregion Instance Interface . . .

        #region Internal Functions . . .

        #endregion Internal Functions . . .

    }
}
