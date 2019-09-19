using generate_fhir_prototype_bindings.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace generate_fhir_prototype_bindings.Languages
{
    public interface ILanguangeExporter
    {
        string[] LanguageJsonTypes
        {
            get;
        }

        HashSet<string> ReservedWords
        {
            get;
        }

        bool OutputBasicPrimitives
        {
            get;
        }

        string SourceFileExtension
        {
            get;
        }

        bool FlagOptionals
        {
            get;
        }

        bool AppendFileHeader(ref StringBuilder sb, string typesToOutput);

        bool AppendFileFooter(ref StringBuilder sb);

        bool AppendModuleOpen(ref StringBuilder sb, string outputNamespace);
        
        bool AppendModuleClose(ref StringBuilder sb, string outputNamespace);

        bool AppendFhirTypeOpen(
                                ref StringBuilder sb,
                                FhirType fhirType
                                );

        bool AppendFhirTypeClose(
                                ref StringBuilder sb,
                                FhirType fhirType
                                );

        bool AppendFhirProperty(
                                ref StringBuilder sb,
                                FhirProperty property,
                                string typeName,
                                bool useLowerCaseName = false
                                );

        bool AppendCodes(
                        ref StringBuilder sb,
                        FhirProperty property, 
                        string parentName
                        );

        bool AppendValueSetOpen(ref StringBuilder sb, string sanitizedAlias, string sanitizedName, fhir.ValueSet valueSet);

        bool AppendValueSetClose(ref StringBuilder sb, string sanitizedAlias, string sanitizedName, fhir.ValueSet valueSet);

        bool AppendValueSetAlias(ref StringBuilder sb, string sanitizedAlias, string sanitizedName, string valueSetUrl);


        bool AppendValueSetCodeConcept(
                                        ref StringBuilder sb, 
                                        string sanitizedValueSetName,
                                        string sanitizedCodeName, 
                                        fhir.CodeSystemConcept concept, 
                                        string systemUrl
                                        );

    }
}
