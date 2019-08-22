using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class FhirCodeSystem
    {
        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        public string Name { get; set; }
        public string Display { get; set; }
        public string Definition { get; set; }

        public string System { get; set; }

        public List<FhirCodeConcept> Concepts { get; set; }

        public List<string> ReferencedSystems { get; set; }

        #endregion Instance Variables . . .

        #region Constructors . . .

        #endregion Constructors . . .

        #region Class Interface . . .

        #endregion Class Interface . . .

        #region Instance Interface . . .


        public string GetTypeScriptString()
        {
            StringBuilder sb = new StringBuilder();

            string comment = Definition.Replace("\n", "\n * ").Replace("\r", "");

            // **** start with a comment ****

            sb.Append($"/*\n * {comment}\n */\n");

            // **** start our coding ****

            sb.Append($"export enum {Name.Replace("-", "_")}\n{{\n");

            // **** traverse our concepts ****

            foreach (FhirCodeConcept concept in Concepts)
            {
                sb.Append(concept.GetTypeScriptString(System));
            }

            // **** close our class **** 

            sb.Append($"}};\n");

            // **** return our string ****

            return sb.ToString();
        }


        public string GetCSharpString()
        {
            StringBuilder sb = new StringBuilder();

            string comment = Definition.Replace("\n", "\n\t/// ").Replace("\r", "");

            // **** start with a comment ****

            sb.Append($"\t///<summary>{comment}</summary>\n");

            // **** start our coding ****

            sb.Append($"\tpublic abstract class {Name.Replace("-", "_")}\n\t{{\n");

            // **** traverse our concepts ****

            foreach (FhirCodeConcept concept in Concepts)
            {
                sb.Append(concept.GetCSharpString(System));
            }

            // **** close our class **** 

            sb.Append($"\t}};\n");

            // **** return our string ****

            return sb.ToString();
        }

        #endregion Instance Interface . . .

        #region Internal Functions . . .

        #endregion Internal Functions . . .

    }
}

