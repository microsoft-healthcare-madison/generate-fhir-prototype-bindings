using System;
using System.Collections.Generic;
using System.Text;

namespace generate_fhir_prototype_bindings.Models
{
    public class R4_StructureDefinition
    {
        #region Class Enums . . . 

        //public enum StructureDefinitionKind
        //{
        //    PrimitiveType = 0,
        //    ComplexType,
        //    Resource,
        //    Logical
        //}

        //public string[] StructureDefinitionKindCodes = {
        //    "primitive-type",
        //    "complex-type",
        //    "resource",
        //    "logical"
        //};

        public enum PublicationStatus {
            Draft,
            Active,
            Retired,
            Unknown
        }

        public string[] PublicationStatusCodes = {
            "draft",
            "active",
            "retired",
            "unknown"
        };

        public enum StructureDefinitionUse
        {
            FhirStructure = 0,
            CustomResource,
            DomainAnalysisModel,
            WireFormat,
            Archetype,
            Template
        }

        public string[] StructureDefinitionUseCodes = {
            "fhir-structure",
            "custom-resource",
            "dam",
            "wire-format",
            "archetype",
            "template"
        };

        public enum TypeDerivationRule {
            Specialization = 0,
            Constraint
        }

        public string[] TypeDerivationRuleCodes = {
            "specialization",
            "constraint"
        };

        #endregion Class Enums . . .

        #region Class Variables . . .

        #endregion Class Variables . . .

        #region Instance Variables . . .

        public string Url { get; set; }

        public R4_Identifier[] Identifier { get; set; }

        public string Version { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public bool? Experimental { get; set; }

        public DateTime? Date { get; set; }

        public string Publisher { get; set; }

        public R4_ContactDetail[] Contact { get; set; }

        public string Description { get; set; }

        public R4_UsageContext[] UseContext { get; set; }

        public string[] Jurisdiction { get; set; }

        public string Purpose { get; set; }

        public string Copyright { get; set; }

        public string Keyword { get; set; }

        public string FhirVersion { get; set; }

        public R4_StructureDefinitionMapping[] Mapping { get; set; }

        public string Kind { get; set; }

        public bool Abstract { get; set; }

        public R4_StructureDefinitionContext[] Context { get; set; }

        public string[] ContextInvariant { get; set; }

        public string Type { get; set; }

        public string BaseDefinition { get; set; }

        public string Derivation { get; set; }


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
