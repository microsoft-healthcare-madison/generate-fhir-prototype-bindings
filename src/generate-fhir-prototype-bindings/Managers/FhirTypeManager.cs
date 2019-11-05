using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using generate_fhir_prototype_bindings.Models;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using fhir;
using System.Runtime.InteropServices.ComTypes;
using generate_fhir_prototype_bindings.Languages;

namespace generate_fhir_prototype_bindings.Managers
{
    public class FhirTypeManager
    {
        #region Class Constants . . .

        /// <summary>The RegEx remove parentheses content.</summary>
        private const string _regexRemoveParenthesesContentDefinition = "\\(.*?\\)";

        private const string _regexSanitizeForPropertyDefinition = "[\r\n\\.\\|\\- \\/]";

        #endregion Class Constants . . .

        #region Class Variables . . .

        /// <summary>The instance for singleton pattern.</summary>
        private static FhirTypeManager _instance;

        private static Regex _regexSanitizeForProperty;

        #endregion Class Variables . . .

        #region Instance Variables . . .

        private Dictionary<string, FhirType> _fhirTypeDict;

        private List<string> _fhirPrimitives;

        private HashSet<string> _fhirTypesNeedingResourceType;

        private Regex _regexRemoveParenthesesContent;

        private List<fhir.CodeSystem> _fhirCodeSystems;
        private Dictionary<string, fhir.CodeSystem> _idCodeSystemDict;
        private Dictionary<string, fhir.CodeSystem> _urlCodeSystemDict;
        private Dictionary<string, fhir.CodeSystemConcept> _embeddedCodeSystemDict;

        private List<fhir.ValueSet> _fhirValueSets;
        private Dictionary<string, fhir.ValueSet> _idValueSetDict;
        private Dictionary<string, fhir.ValueSet> _urlValueSetDict;

        private Dictionary<string, fhir.CodeSystemConcept[]> _urlValueSetConceptsDict;
        private Dictionary<string, string> _urlValueSetSystemDict;

        private HashSet<string> _writtenValueSetAliases;
        private HashSet<string> _writtenValueSetNames;
        private HashSet<string> _writtenValueSetUrls;
        private HashSet<string> _writtenValueCodes;

        #endregion Instance Variables . . .

        #region Constructors . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Constructor that prevents a default instance of this class from being created.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///-------------------------------------------------------------------------------------------------

        private FhirTypeManager()
        {
            _fhirTypeDict = new Dictionary<string, FhirType>();
            _fhirPrimitives = new List<string>();
            _fhirTypesNeedingResourceType = new HashSet<string>();

            _fhirCodeSystems = new List<fhir.CodeSystem>();
            _idCodeSystemDict = new Dictionary<string, fhir.CodeSystem>();
            _urlCodeSystemDict = new Dictionary<string, fhir.CodeSystem>();
            _embeddedCodeSystemDict = new Dictionary<string, CodeSystemConcept>();

            _fhirValueSets = new List<fhir.ValueSet>();
            _idValueSetDict = new Dictionary<string, fhir.ValueSet>();
            _urlValueSetDict = new Dictionary<string, fhir.ValueSet>();

            _urlValueSetConceptsDict = new Dictionary<string, fhir.CodeSystemConcept[]>();
            _urlValueSetSystemDict = new Dictionary<string, string>();

            _writtenValueSetAliases = new HashSet<string>();
            _writtenValueSetNames = new HashSet<string>();
            _writtenValueSetUrls = new HashSet<string>();
            _writtenValueCodes = new HashSet<string>();

            _regexRemoveParenthesesContent = new Regex(_regexRemoveParenthesesContentDefinition);
            _regexSanitizeForProperty = new Regex(_regexSanitizeForPropertyDefinition);
        }

        #endregion Constructors . . .

        #region Class Interface . . .

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Initializes this object.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///-------------------------------------------------------------------------------------------------

        public static void Init()
        {
            // **** make an instance ****

            CheckOrCreateInstance();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Trim for matching names.</summary>
        ///
        /// <remarks>Gino Canessa, 7/22/2019.</remarks>
        ///
        /// <param name="matchingNames">List of names of the matchings.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void TrimForMatchingNames(string matchingNames)
        {
            _instance._TrimForMatchingNames(matchingNames);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Determine if 'name' exists.</summary>
        ///
        /// <remarks>Gino Canessa, 7/11/2019.</remarks>
        ///
        /// <param name="name">The name.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool Exists(string name)
        {
            return (_instance._fhirTypeDict.ContainsKey(name) && _instance._fhirTypeDict[name] != null);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process a primitive type from an XML Spreadsheet.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="name">          The name.</param>
        /// <param name="baseType">      Type of the base.</param>
        /// <param name="comment">       The comment.</param>
        /// <param name="isPrimitive">   True if is primitive, false if not.</param>
        /// <param name="sourceFilename">Filename of the source file.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void ProcessSpreadsheetType(
                                                string name,
                                                string baseType,
                                                string comment,
                                                bool isPrimitive,
                                                string sourceFilename
                                                )
        {
            _instance._ProcessSpreadsheetType(
                name, 
                baseType, 
                comment, 
                isPrimitive, 
                sourceFilename
                );
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process an XML Spreadsheet data element.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="element">       The element.</param>
        /// <param name="baseType">      Type of the base.</param>
        /// <param name="comment">       The comment.</param>
        /// <param name="cardinality">   The cardinality.</param>
        /// <param name="isPrimitive">   True if is primitive, false if not.</param>
        /// <param name="sourceFilename">Filename of the source file.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void ProcessSpreadsheetDataElement(
                                                string element,
                                                string baseType,
                                                string comment,
                                                string cardinality,
                                                bool isPrimitive,
                                                string sourceFilename,
                                                string[] codeValues = null,
                                                string valueSet = null
                                                )
        {
            _instance._ProcessSpreadsheetDataElement(
                element, 
                baseType, 
                comment, 
                cardinality, 
                isPrimitive, 
                sourceFilename,
                codeValues,
                valueSet
                );
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Output for language.</summary>
        ///
        /// <remarks>Gino Canessa, 9/19/2019.</remarks>
        ///
        /// <param name="writer">         The writer.</param>
        /// <param name="lang">           The language.</param>
        /// <param name="typesToOutput">  The types to output.</param>
        /// <param name="outputNamespace">The output namespace.</param>
        /// <param name="excludeCodes">   True to exclude, false to include the codes.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void OutputForLang(
                                        StreamWriter writer,
                                        ILanguangeExporter lang,
                                        string typesToOutput,
                                        string outputNamespace,
                                        bool excludeCodes
                                        )
        {
            _instance._OutputForLanguage(writer, lang, typesToOutput, outputNamespace, excludeCodes);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Performs the resource type checks action.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <returns>An int.</returns>
        ///-------------------------------------------------------------------------------------------------

        public static int PerformResourceTypeChecks()
        {
            return _instance._PerformResourceTypeChecks();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Query if 'name' does type require resource tag.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="name">The name.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool DoesTypeRequireResourceTag(string name)
        {
            return _instance._fhirTypesNeedingResourceType.Contains(name);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Removes the type described by name.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="name">The name.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool RemoveType(string name)
        {
            return _instance._RemoveType(name);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Load the code system described by codeSystem.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="codeSystem">The code system.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool LoadCodeSystem(fhir.CodeSystem codeSystem)
        {
            return _instance._LoadCodeSystem(codeSystem);
        }

        public static bool LoadValueSet(fhir.ValueSet valueSet, string filename)
        {
            return _instance._LoadValueSet(valueSet, filename);
        }

        public static bool ExpandValueSets()
        {
            return _instance._ExpandValueSets();
        }

        public static bool TryGetValueSet(string valueSetUrl, out fhir.ValueSet valueSet)
        {
            if (!_instance._urlValueSetDict.ContainsKey(valueSetUrl))
            {
                valueSet = null;
                return false;
            }
            valueSet = _instance._urlValueSetDict[valueSetUrl];
            return true;
        }

        #endregion Class Interface . . .

        #region Instance Interface . . .

        #endregion Instance Interface . . .

        #region Internal Functions . . .

        private bool _ExpandValueSets()
        {
            // **** traverse the value sets we have been exposed to ****

            foreach (fhir.ValueSet valueSet in _fhirValueSets)
            {
                // **** attempt to expand this value set ****

                ExpandValueSet(valueSet);
            }

            // **** success ****

            return true;
        }

        private bool ExpandValueSet(fhir.ValueSet valueSet)
        {
            // **** sanity checks ****

            if (valueSet == null)
            {
                return true;
            }

            if (_urlValueSetConceptsDict.ContainsKey(valueSet.Url))
            {
                return true;
            }

            // **** check for composition rules we know how to handle ****

            if ((valueSet.Compose == null) || 
                (valueSet.Compose.Include == null) ||
                (valueSet.Compose.Include.Length == 0))
            {
                return false;
            }

            List<fhir.CodeSystemConcept> concepts = new List<CodeSystemConcept>();

            // **** process includes ****

            foreach (fhir.ValueSetComposeInclude include in valueSet.Compose.Include)
            {
                // **** check for no or unknown system ****

                if ((string.IsNullOrEmpty(include.System)) ||
                    (!_urlCodeSystemDict.ContainsKey(include.System)) ||
                    (_urlCodeSystemDict[include.System].Concept == null))
                {
                    // **** cannot process this (partial is worse than missing) ****

                    return false;
                }

                // **** check to see if there is no filter ****

                if ((include.Filter == null) ||
                    (include.Filter.Length == 0))
                {
                    // **** add the entire code system ****

                    concepts.AddRange(_urlCodeSystemDict[include.System].Concept);

                    // **** set our code system for this value set ****

                    _urlValueSetSystemDict[valueSet.Url] = include.System;

                    // **** done with this include ****

                    continue;
                }

                // **** check for filters we know about ****

                if (_embeddedCodeSystemDict.ContainsKey($"{include.System}#{include.Filter[0].Value}"))
                {
                    // *** add this set ****

                    concepts.AddRange(_embeddedCodeSystemDict[$"{include.System}#{include.Filter[0].Value}"].Concept);

                    // **** set our code system for this value set ****

                    _urlValueSetSystemDict[valueSet.Url] = include.System;

                    // **** done with this include ****

                    continue;
                }

                // **** search for this value manually ****

                if (FindCodeSystemValue(
                        _urlCodeSystemDict[include.System].Concept,
                        include.Filter[0].Value,
                        out CodeSystemConcept foundConcept))
                {
                    // **** add this concept ****

                    concepts.Add(foundConcept);

                    // **** set our code system for this value set ****

                    _urlValueSetSystemDict[valueSet.Url] = include.System;
                }

                // **** don't know how to handle ****

                return false;
            }


            HashSet<string> addedConceptCodes = new HashSet<string>();

            // **** flatten our concepts ****

            concepts = FlattenCodeSystemConcepts(concepts, ref addedConceptCodes);

            // **** process excludes ****

            if (valueSet.Compose.Exclude != null)
            {
                foreach (fhir.ValueSetComposeInclude exclude in valueSet.Compose.Exclude)
                {
                    if ((exclude.Concept == null) || (exclude.Concept.Length == 0))
                    {
                        continue;
                    }

                    // **** process each concept ****

                    foreach (fhir.ValueSetComposeIncludeConcept excludeConcept in exclude.Concept)
                    {
                        // **** look for this item ****

                        concepts.RemoveAll(cpt => cpt.Code == excludeConcept.Code);
                    }
                }
            }

            // **** add this set ****

            _urlValueSetConceptsDict.Add(valueSet.Url, concepts.ToArray());

            // **** success ****

            return true;
        }

        private List<fhir.CodeSystemConcept> FlattenCodeSystemConcepts(
                                                            IEnumerable<fhir.CodeSystemConcept> conceptTree,
                                                            ref HashSet<string> addedConceptCodes
                                                            )
        {
            List<fhir.CodeSystemConcept> flat = new List<fhir.CodeSystemConcept>();

            // **** traverse our tree ****

            foreach (fhir.CodeSystemConcept concept in conceptTree)
            {
                // **** ignore duplicates ****

                if (addedConceptCodes.Contains(concept.Code))
                {
                    continue;
                }

                // **** add this node ****

                if (ConceptWithoutSubs(concept, out fhir.CodeSystemConcept cleanConcept))
                {
                    flat.Add(cleanConcept);

                    // **** add to our hash ****

                    addedConceptCodes.Add(cleanConcept.Code);
                }

                // **** add any subnodes ****

                if ((concept.Concept != null) && (concept.Concept.Length > 0))
                {
                    flat.AddRange(FlattenCodeSystemConcepts(concept.Concept, ref addedConceptCodes));
                }
            }

            // **** return our list ****

            return flat;
        }

        private bool ConceptWithoutSubs(fhir.CodeSystemConcept concept, out fhir.CodeSystemConcept cleanConcept)
        {
            if ((concept.Property != null) &&
                (concept.Property.Length > 0) &&
                (concept.Property.Any(prop => (prop.Code == "notSelectable") && (prop.ValueBoolean == true))))
            {
                cleanConcept = null;
                return false;
            }
               
            // **** create our concept ****

            cleanConcept = new CodeSystemConcept();

            // **** set values we have ****

            if (!string.IsNullOrEmpty(concept.Code))
            {
                cleanConcept.Code = concept.Code;
            }

            if (!string.IsNullOrEmpty(concept.Display))
            {
                cleanConcept.Display = concept.Display;
            }

            if (!string.IsNullOrEmpty(concept.Definition))
            {
                cleanConcept.Definition = concept.Definition;
            }

            // **** return our object ****

            return true;
        }

        private bool FindCodeSystemValue(
                                        fhir.CodeSystemConcept[] concepts,
                                        string requestedCode,
                                        out fhir.CodeSystemConcept concept
                                        )
        {
            // **** assume not found ****

            concept = null;

            // **** start traversing ****

            for (int searchIndex = 0; searchIndex < concepts.Length; searchIndex++)
            {
                // **** check for match ****

                if (concepts[searchIndex].Code == requestedCode)
                {
                    concept = concepts[searchIndex];
                    return true;
                }

                // **** check for needing to recurse ****

                if ((concepts[searchIndex].Concept != null) &&
                    (concepts[searchIndex].Concept.Length > 0))
                {
                    if (FindCodeSystemValue(
                            concepts[searchIndex].Concept, 
                            requestedCode, 
                            out concept))
                    {
                        return true;
                    }
                }
            }

            // **** did not find ****

            return false;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Requires alpha.</summary>
        ///
        /// <remarks>Gino Canessa, 9/18/2019.</remarks>
        ///
        /// <param name="value">The value.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        private static bool RequiresAlpha(string value)
        {
            foreach (char c in value)
            {
                if (((c < '0') || (c > '9')) && (c != '_'))
                {
                    return false;
                }
            }
            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Sanitize for property.</summary>
        ///
        /// <remarks>Gino Canessa, 9/18/2019.</remarks>
        ///
        /// <param name="value">                The value.</param>
        /// <param name="languageReservedWords">The language reserved words.</param>
        ///
        /// <returns>A string.</returns>
        ///-------------------------------------------------------------------------------------------------

        public static string SanitizeForProperty(string value, HashSet<string> languageReservedWords)
        {
            // **** check for symbols we need to replace ****

            if (value.Equals("...", StringComparison.Ordinal))
            {
                value = "NONE";
            }

            if (value.Contains("<="))
            {
                value = value.Replace("<=", "LESS_THAN_OR_EQUALS");
            }

            if (value.Contains("<"))
            {
                value = value.Replace("<", "LESS_THAN");
            }

            if (value.Contains(">="))
            {
                value = value.Replace(">=", "GREATER_THAN_OR_EQUALS");
            }

            if (value.Contains(">"))
            {
                value = value.Replace(">", "GREATER_THAN");
            }

            if (value.Contains("!="))
            {
                value = value.Replace("!=", "NOT_EQUALS");
            }

            if (value.Contains("=="))
            {
                value = value.Replace("==", "EQUALS");
            }

            if (value.Contains("="))
            {
                value = value.Replace("=", "EQUALS");
            }

            if (value.Contains("+"))
            {
                value = value.Replace("+", "PLUS");
            }

            if (char.IsDigit(value[0]))
            {
                value = "_" + value;
            }

            // **** make major substitutions ****

            value = _regexSanitizeForProperty.Replace(value, "_");

            // **** need to check for all digits or underscores, or reserved word ****

            if ((RequiresAlpha(value)) || (languageReservedWords.Contains(value)))
            {
                if (value[0] == '_')
                {
                    return $"VAL{value}";
                }

                return $"VAL_{value}";
            }

            // **** ****

            return value;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Sanitize for code.</summary>
        ///
        /// <remarks>Gino Canessa, 9/18/2019.</remarks>
        ///
        /// <param name="input">The input.</param>
        /// <param name="name"> [out] The name.</param>
        /// <param name="value">[out] The value.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void SanitizeForCode(
                                            string input, 
                                            HashSet<string>languageReservedWords, 
                                            out string name, 
                                            out string value
                                            )
        {
            name = input.Trim();

            if (name.Contains(" "))
            {
                name = name.Substring(0, name.IndexOf(" "));
            }

            value = name;

            name = FhirTypeManager.SanitizeForProperty(name, languageReservedWords);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Sanitize comment.</summary>
        ///
        /// <remarks>Gino Canessa, 9/18/2019.</remarks>
        ///
        /// <param name="comment">            The comment.</param>
        /// <param name="languageLineComment">The language line comment.</param>
        /// <param name="languageIndent">     The language indent.</param>
        /// <param name="indentation">        (Optional) The indentation.</param>
        ///
        /// <returns>A string.</returns>
        ///-------------------------------------------------------------------------------------------------

        public static string SanitizeComment(
                                            string comment, 
                                            string languageLineComment, 
                                            char languageIndent,
                                            int indentation = 0
                                            )
        {
            switch (indentation)
            {
                default:
                case 0:
                    return comment.Replace("\n", $"\n{languageLineComment} ").Replace("\r", "");
                case 1:
                    return comment.Replace("\n", $"\n{languageIndent}{languageLineComment} ").Replace("\r", "");
                case 2:
                    return comment.Replace("\n", $"\n{languageIndent}{languageIndent}{languageLineComment} ").Replace("\r", "");
                case 3:
                    return comment.Replace("\n", $"\n{languageIndent}{languageIndent}{languageIndent}{languageLineComment} ").Replace("\r", "");
            }
        }

        private bool _LoadValueSet(fhir.ValueSet valueSet, string filename)
        {
            // **** sanity check ****

            if ((valueSet == null) || (string.IsNullOrEmpty(valueSet.Url)))
            {
                return false;
            }

            // **** check to see if this is a known code system ****

            if (_urlValueSetDict.ContainsKey(valueSet.Url))
            {
                // **** assume already added ****

                return true;
            }

            // **** add this valueSet to our list ****

            _fhirValueSets.Add(valueSet);

            // **** add to the appropriate tracking sets ****

            _idValueSetDict.Add(valueSet.Id, valueSet);
            _urlValueSetDict.Add(valueSet.Url, valueSet);

            // **** ok ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the code system described by codeSystem.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="codeSystem">The code system.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        private bool _LoadCodeSystem(fhir.CodeSystem codeSystem)
        {
            // **** sanity check ****

            if (codeSystem == null)
            {
                return false;
            }

            // **** check to see if this is a known code system ****

            if (_urlCodeSystemDict.ContainsKey(codeSystem.Url))
            {
                // **** assume already added ****

                return true;
            }

            // **** add this codesystem to our list ****

            _fhirCodeSystems.Add(codeSystem);

            // **** add to the appropriate tracking sets ****

            _idCodeSystemDict.Add(codeSystem.Id, codeSystem);
            _urlCodeSystemDict.Add(codeSystem.Url, codeSystem);

            // **** check this code system for a hierarchy ****

            if ((codeSystem.Concept != null) && (codeSystem.Concept.Length > 0))
            {
                CheckForEmbeddedCodeSystems(codeSystem, codeSystem.Concept);
            }

            // **** ok ****

            return true;
        }

        private bool CheckForEmbeddedCodeSystems(
                                                fhir.CodeSystem codeSystem, 
                                                fhir.CodeSystemConcept[] concepts
                                                )
        {
            // **** traverse the concepts ****

            foreach (fhir.CodeSystemConcept concept in concepts)
            {
                if (concept == null)
                {
                    continue;
                }

                // **** test for recursion ****

                if ((concept.Concept != null) && (concept.Concept.Length > 0))
                {
                    CheckForEmbeddedCodeSystems(codeSystem, concept.Concept);
                }

                // **** test exclusions on this concept ****

                if ((concept.Property == null) || (concept.Property.Length == 0))
                {
                    continue;
                }

                // **** test for a not-selectable item ****

                foreach (fhir.CodeSystemConceptProperty prop in concept.Property)
                {
                    if ((prop.Code == "notSelectable") && (prop.ValueBoolean == true))
                    {
                        // **** add this to our dictionary ****

                        _embeddedCodeSystemDict.Add(
                            $"{codeSystem.Url}#{concept.Code}", 
                            concept
                            );

                        // **** done testing ****

                        break;
                    }
                }
            }

            // **** success ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Removes the type described by name.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="name">The name.</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        private bool _RemoveType(string name)
        {
            // **** check for this type existing ****

            if (!_fhirTypeDict.ContainsKey(name))
            {
                return false;
            }

            // **** grab this type ****

            FhirType fhirType = _fhirTypeDict[name];

            // **** traverse properties, removing any cascading types ****

            foreach (FhirProperty property in fhirType.Properties.Values)
            {
                // **** get this property's type info ****

                if (!_fhirTypeDict.ContainsKey(property.TypeName))
                {
                    continue;
                }

                FhirType subType = _fhirTypeDict[property.TypeName];

                // **** check to see if this type was defined in the same file ****

                if (subType.SourceFilename.Equals(fhirType.SourceFilename, StringComparison.Ordinal))
                {
                    // **** remove this type ****

                    _RemoveType(subType.Name);
                }
            }

            // **** remove this type ****

            _fhirTypeDict.Remove(name);

            // **** success ****

            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Performs the resource type checks action.</summary>
        ///
        /// <remarks>Gino Canessa, 8/12/2019.</remarks>
        ///
        /// <returns>An int.</returns>
        ///-------------------------------------------------------------------------------------------------

        private int _PerformResourceTypeChecks()
        {
            // **** make sure Resource and DomainResource are excluded ****

            if (_fhirTypesNeedingResourceType.Contains("Resource"))
            {
                _fhirTypesNeedingResourceType.Remove("Resource");
            }

            if (_fhirTypesNeedingResourceType.Contains("DomainResource"))
            {
                _fhirTypesNeedingResourceType.Remove("DomainResource");
            }

            // **** check the number of items in the hashmap ****

            int startCount = _fhirTypesNeedingResourceType.Count;

            // **** traverse our types checking for ones we need to flag ****

            foreach (FhirType fhirType in _fhirTypeDict.Values)
            {
                if (fhirType == null)
                {
                    continue;
                }

                // **** if the type this resource descends from is listed, but this resource isn't ****

                if ((!_fhirTypesNeedingResourceType.Contains(fhirType.Name)) &&
                    (_fhirTypesNeedingResourceType.Contains(fhirType.TypeName)))
                {
                    _fhirTypesNeedingResourceType.Add(fhirType.Name);
                }
            }

            // **** return the number of items added ****

            return (_fhirTypesNeedingResourceType.Count - startCount);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Trim for matching names.</summary>
        ///
        /// <remarks>Gino Canessa, 7/22/2019.</remarks>
        ///
        /// <param name="matchingNames">List of names of the matchings.</param>
        ///-------------------------------------------------------------------------------------------------

        private void _TrimForMatchingNames(string matchingNames)
        {
            if (string.IsNullOrEmpty(matchingNames))
            {
                return;
            }

            // **** grab an array for each name we need to match ***

            string[] names = matchingNames.Split('|');

            HashSet<string> typesToOutput = new HashSet<string>();
            
            // **** traverse our array and build the names we need ****

            foreach (string name in names)
            {
                // **** add this name and all related names ****

                AddNameForOutput(name, ref typesToOutput);
            }

            // **** grab all our type names (cannot remove from dictionary while traversing enumerator) ****

            string[] knownTypes = _fhirTypeDict.Keys.ToArray<string>();

            // **** remove everything from our dictionary that's not in our hash set ****

            foreach (string typeName in knownTypes)
            {
                // **** check for this type being in our output set ****

                if (typesToOutput.Contains(typeName))
                {
                    // **** leave this record ****

                    continue;
                }

                // **** remove this record ****

                _fhirTypeDict.Remove(typeName);

                // **** check for primitives list ****

                if (_fhirPrimitives.Contains(typeName))
                {
                    _fhirPrimitives.Remove(typeName);
                }
            }
        }

        private void AddNameForOutput(string name, ref HashSet<string> typesToOutput)
        {
            // **** check for no name (blank types) ****

            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            // **** check for already added ****

            if (typesToOutput.Contains(name))
            {
                // **** nothing else to do ****

                return;
            }

            // **** check for not containing this name ****

            if (!_fhirTypeDict.ContainsKey(name))
            {
                // **** nothing else to do ****

                return;
            }

            // **** add this name to the hash set ****

            typesToOutput.Add(name);

            // **** grab this node from our dictionary ****

            FhirType fhirType = _fhirTypeDict[name];

            // **** add this record's type to our output list ****

            AddNameForOutput(fhirType.TypeName, ref typesToOutput);

            // **** done if there are no properties ****

            if ((fhirType.Properties == null) || (fhirType.Properties.Count == 0))
            {
                // **** nothing else to do here ****

                return;
            }

            // **** traverse properties to add them ****

            foreach (FhirProperty property in fhirType.Properties.Values)
            {
                // **** add this property's type ****

                AddNameForOutput(property.TypeName, ref typesToOutput);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process a primitive type from an XML Spreadsheet.</summary>
        ///
        /// <remarks>Gino Canessa, 7/9/2019.</remarks>
        ///
        /// <param name="name">           The name.</param>
        /// <param name="baseType">       Type of the base.</param>
        /// <param name="comment">        The comment.</param>
        /// <param name="isFhirPrimitive">True if is primitive, false if not.</param>
        /// <param name="sourceFilename"> Filename of the source file.</param>
        ///-------------------------------------------------------------------------------------------------

        private void _ProcessSpreadsheetType(
                                            string name,
                                            string baseType,
                                            string comment,
                                            bool isFhirPrimitive,
                                            string sourceFilename
                                            )
        {
            // **** skip empty or excluded fields ****

            if ((string.IsNullOrEmpty(name)) || (name[0] == '!'))
            {
                return;
            }

            // **** create a type object for this primitive ****

            FhirType node = FhirType.CreateFhirType(
                name,
                name,
                baseType,
                comment,
                sourceFilename,
                isFhirPrimitive
                );

            // **** add this primitive to our dictionary and list ****

            if (node != null)
            {
                if (_fhirTypeDict.ContainsKey(name) && _fhirTypeDict[name] == null)
                {
                    _fhirTypeDict.Remove(name);
                }

                _fhirTypeDict.Add(name, node);

                if (isFhirPrimitive)
                {
                    _fhirPrimitives.Add(name);
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process an XML Spreadsheet data element.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="element">        The element.</param>
        /// <param name="baseType">       Type of the base.</param>
        /// <param name="comment">        The comment.</param>
        /// <param name="cardinality">    The cardinality.</param>
        /// <param name="isFhirPrimitive">True if is primitive, false if not.</param>
        /// <param name="sourceFilename"> Filename of the source file.</param>
        ///-------------------------------------------------------------------------------------------------

        private void _ProcessSpreadsheetDataElement(
                                        string element,
                                        string baseType,
                                        string comment,
                                        string cardinality,
                                        bool isFhirPrimitive,
                                        string sourceFilename,
                                        string[] codeValues = null,
                                        string valueSet = null
                                        )
        {
            // **** skip empty or excluded fields ****

            if ((string.IsNullOrEmpty(element)) || (element[0] == '!'))
            {
                return;
            }

            // **** we are not doing things like Reference type limiting ****

            baseType = _regexRemoveParenthesesContent.Replace(baseType, string.Empty);

            // **** split the element into names ****

            string[] names = element.Split('.');
            int namePartCount = names.Length;
            FhirType currentTypeNode = null;
            string pathPascalCase = "";

            // **** traverse name parts and resolve each one ****

            for (int nameIndex = 0; nameIndex < namePartCount; nameIndex++)
            {
                // **** grab this name part ****

                string name = names[nameIndex];

                // **** remove markup prefixes in names ****

                while (!char.IsLetterOrDigit(name[0]))
                {
                    name = name.Substring(1);
                }

                // **** remove all spaces from names ****

                name = name.Replace(" ", "");

                // **** append the next part of our Pascal Case path ****

                pathPascalCase += string.Concat(name.Substring(0, 1).ToUpper(), name.Substring(1));

                // **** top level always needs to be a type ****

                if (nameIndex == 0)
                {
                    // **** create a type for this within the manager ****

                    FindOrCreateType(
                        name,
                        pathPascalCase,
                        baseType,
                        comment,
                        sourceFilename,
                        isFhirPrimitive,
                        ref currentTypeNode
                        );

                    // **** nothing else to do for top level ****

                    continue;
                }

                // **** check for type expansion markup ****

                if (name.Contains("[x]"))
                {
                    // **** add these fields ****

                    AddNodesForFieldTypeArray(name, baseType, comment, cardinality, currentTypeNode);

                    // **** done with this field ****

                    continue;
                }

                // **** check for this field not existing ****

                if (!currentTypeNode.Properties.ContainsKey(name))
                {
                    // **** create this field ****

                    FhirProperty property = FhirProperty.CreateProperty(
                        name,
                        baseType,
                        comment,
                        cardinality,
                        codeValues,
                        valueSet
                        );

                    // **** add this property to our type ****

                    currentTypeNode.Properties.Add(name, property);
                }

                // **** check if we are not the last item in this path chain ****

                if (nameIndex != (namePartCount - 1))
                {
                    // **** get the type off of the property for this element ****

                    string fieldType = currentTypeNode.Properties[name].TypeName;

                    // **** since we are promoting this node, we need to change it's type ****

                    currentTypeNode.Properties[name].TypeName = pathPascalCase;

                    // **** find or create a type for this ****

                    FindOrCreateType(
                        pathPascalCase,
                        pathPascalCase,
                        "Element",
                        currentTypeNode.Properties[name].Comment,
                        currentTypeNode.SourceFilename,
                        currentTypeNode.IsFhirPrimitive,
                        ref currentTypeNode
                        );
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Either selects an existing matching type or creates the matching type record.</summary>
        ///
        /// <remarks>Gino Canessa, 7/16/2019.</remarks>
        ///
        /// <param name="name">           The name.</param>
        /// <param name="pathPascalCase">  The path pascal case.</param>
        /// <param name="typeName">       Name of the type.</param>
        /// <param name="comment">        The comment.</param>
        /// <param name="sourceFilename"> Filename of the source file.</param>
        /// <param name="isFhirPrimitive">True if is primitive, false if not.</param>
        /// <param name="currentTypeNode">[in,out] The current type node.</param>
        ///-------------------------------------------------------------------------------------------------

        private void FindOrCreateType(
                                        string name,
                                        string pathPascalCase,
                                        string typeName,
                                        string comment,
                                        string sourceFilename,
                                        bool isFhirPrimitive,
                                        ref FhirType currentTypeNode
                                        )
        {
            // **** check for filling in a forward-declared type ****

            if ((_fhirTypeDict.ContainsKey(name)) && (_fhirTypeDict[name] == null))
            {
                _fhirTypeDict.Remove(name);
            }

            // **** check to see if we need to create this resource ****

            if (!_fhirTypeDict.ContainsKey(name))
            {
                // **** make sure base type exists ****

                if ((!_fhirTypeDict.ContainsKey(typeName)) && (!name.Equals(typeName, StringComparison.Ordinal)))
                {
                    // TODO(ginoc): Need to trigger load of base types to inherit fields and type information

                    _fhirTypeDict.Add(typeName, null);
                }

                // **** create this type ****

                currentTypeNode = FhirType.CreateFhirType(
                    name,
                    pathPascalCase,
                    typeName,
                    comment,
                    sourceFilename,
                    isFhirPrimitive
                    );

                // **** add to our dictionary ****

                _fhirTypeDict.Add(name, currentTypeNode);

                // **** if this is a primitive resource, add to our list ****

                if (isFhirPrimitive)
                {
                    _fhirPrimitives.Add(name);
                }

                // **** check to see if this decends from anything in our 'resource' hashmap ****

                if ((typeName.Equals("Resource", StringComparison.Ordinal)) || 
                    (typeName.Equals("DomainResource", StringComparison.Ordinal)))
                {
                    _fhirTypesNeedingResourceType.Add(name);
                }
            }
            else
            {
                // **** grab a reference to our node ****

                currentTypeNode = _fhirTypeDict[name];
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Adds the nodes for field with a type array.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="name">       The name.</param>
        /// <param name="baseType">   Type of the base.</param>
        /// <param name="comment">    The comment.</param>
        /// <param name="cardinality">The cardinality.</param>
        /// <param name="fhirType">   The FHIR type we are adding fields to.</param>
        ///
        ///-------------------------------------------------------------------------------------------------

        private void AddNodesForFieldTypeArray(
                                                string name,
                                                string baseType,
                                                string comment,
                                                string cardinality,
                                                FhirType fhirType
                                                )
        {
            const string optionalCardinality = "0..1";

            // **** parse out the array indicator ****

            string baseName = name.Replace("[x]", "");

            // **** check for all types ****

            if (baseType == "*")
            {
                // **** traverse primitives ****

                foreach (string primitiveName in _fhirPrimitives)
                {
                    FhirType primitive = _fhirTypeDict[primitiveName];

                    // **** join the name and the type ****

                    string fullName = string.Concat(baseName, primitive.NameCapitalized);

                    // **** create this field ****

                    FhirProperty property = FhirProperty.CreateProperty(
                        fullName,
                        primitive.TypeName,
                        comment,
                        optionalCardinality
                        );

                    // **** add this property to our type ****

                    if (!fhirType.Properties.ContainsKey(fullName))
                    {
                        fhirType.Properties.Add(fullName, property);
                    }
                }
            }
            else
            {
                // **** parse the type list ****

                string[] subtypes = baseType.Split('|');

                if (subtypes.Length > 1)
                {
                    cardinality = optionalCardinality;
                }

                foreach (string subtype in subtypes)
                {
                    if (string.IsNullOrEmpty(subtype))
                    {
                        continue;
                    }

                    // **** trim whitespace ****

                    string trimmedSubtype = subtype.Replace(" ", "");

                    // **** make sure we have a capital for the join ****

                    string subtypeCapitalized = string.Concat(trimmedSubtype.Substring(0, 1).ToUpper(), trimmedSubtype.Substring(1));

                    // **** join the name and the type ****

                    string fullName = string.Concat(baseName, subtypeCapitalized);

                    // **** create this field ****

                    FhirProperty property = FhirProperty.CreateProperty(
                        fullName,
                        trimmedSubtype,
                        comment,
                        cardinality
                        );

                    // **** add this property to our type ****

                    if (!fhirType.Properties.ContainsKey(fullName))
                    {
                        fhirType.Properties.Add(fullName, property);
                    }
                }
            }
        }

        private void _OutputForLanguage(
                                        StreamWriter writer,
                                        ILanguangeExporter lang,
                                        string typesToOutput,
                                        string outputNamespace,
                                        bool excludeCodes
                                        )
        {
            StringBuilder fileSB = new StringBuilder();

            StringBuilder typeSB = new StringBuilder();
            StringBuilder enumSB = new StringBuilder();
            StringBuilder valueSetSB = new StringBuilder();

            // **** have not written any code systems ****

            _writtenValueSetAliases.Clear();
            _writtenValueSetNames.Clear();
            _writtenValueSetUrls.Clear();
            _writtenValueCodes.Clear();

            // **** get the header ****

            lang.AppendFileHeader(ref fileSB, typesToOutput);

            // **** open our module ****

            lang.AppendModuleOpen(ref fileSB, outputNamespace);

            // **** build a dictionary with types we need to replace out ****

            //Dictionary<string, FhirBasicNode.LanguagePrimitiveType> languagePrimitiveDict = new Dictionary<string, FhirBasicNode.LanguagePrimitiveType>();

            //// **** build a primitive replacement dictionary if the language requires it ****

            //if (!lang.OutputBasicPrimitives)
            //{
            //    // **** first loop is to discover the primitive types ****

            //    foreach (string primitiveName in _fhirPrimitives)
            //    {
            //        // **** grab our node ****

            //        FhirType node = _fhirTypeDict[primitiveName];

            //        // **** skip what we do not want ****

            //        if (node.Properties.Count != 0)
            //        {
            //            continue;
            //        }

            //        // **** check for a language primitive ****

            //        if (node.LanguagePrimitiveIndex != FhirBasicNode.LanguagePrimitiveType.None)
            //        {
            //            // **** add to our lookup dictionary ****

            //            languagePrimitiveDict.Add(node.Name.Trim().ToLower(), node.LanguagePrimitiveIndex);

            //            // **** don't do anything else with this type ****

            //            continue;
            //        }
            //    }

            //    // **** add any primitives we need to define (skip first (zero), number of types in FhirBasicNode.LanguagePrimitive) ****

            //    for (int i = 1; i < 5; i++)
            //    {
            //        // **** check for not being the same ****

            //        if ((FhirBasicNode.JsonLanguagePrimitives[i] != lang.LanguageJsonTypes[i]) &&
            //            (!languagePrimitiveDict.ContainsKey(FhirBasicNode.JsonLanguagePrimitives[i])))
            //        {
            //            languagePrimitiveDict.Add(FhirBasicNode.JsonLanguagePrimitives[i], (FhirBasicNode.LanguagePrimitiveType)i);
            //        }
            //    }
            //}

            // **** output the rest of our types ****

            List<string> fhirTypeNames = _fhirTypeDict.Keys.ToList<string>();
            fhirTypeNames.Sort();

            foreach (string fhirTypeName in fhirTypeNames)
            {
                // **** grab our node ****

                FhirType fhirType = _fhirTypeDict[fhirTypeName];

                if (fhirType == null)
                {
                    continue;
                }

                //// **** always skip cicular primitives ****

                //if ((fhirType.LanguagePrimitive != FhirBasicNode.LanguagePrimitiveType.None) &&
                //    (fhirType.IsCircular))
                //{
                //    continue;
                //}

                // **** skip other primitives if the language does not want them ****

                if ((!lang.OutputBasicPrimitives) &&
                    (lang.FhirLanguageTypeMap.ContainsKey(fhirType.TypeName)))
                {
                    continue;
                }

                // **** make sure we don't self-define primitives ****

                if ((lang.OutputBasicPrimitives) &&
                    (lang.FhirLanguageTypeMap.ContainsKey(fhirType.TypeName)) &&
                    (fhirType.IsCircular))
                {
                    continue;
                }

                // **** get our type open ****

                lang.AppendFhirTypeOpen(ref typeSB, fhirType);

                // **** get the name in all lower case for faster comparison ****

                string nameLower = fhirType.Name.ToLower();

                // **** output the properties of this type (alphebetically) ****

                List<string> propertyNames = fhirType.Properties.Keys.ToList<string>();
                propertyNames.Sort();

                foreach (string propertyName in propertyNames)
                {
                    // **** get this property ****

                    FhirProperty property = fhirType.Properties[propertyName];

                    // **** skip fields with no type (are defined in base object) ****

                    if (string.IsNullOrEmpty(property.TypeName))
                    {
                        continue;
                    }

                    // **** grab this type ****

                    string propertyType = property.TypeName;

                    // **** determine the type we are using in output ****

                    if (lang.FhirLanguageTypeMap.ContainsKey(propertyType))
                    {
                        propertyType = lang.FhirLanguageTypeMap[propertyType];
                    }

                    // **** append this field's string ****

                    lang.AppendFhirProperty(
                        ref typeSB,
                        property,
                        propertyType,
                        propertyName.Equals(nameLower, StringComparison.Ordinal)
                        );

                    // **** check for having a code value list ****

                    if ((!excludeCodes) && 
                        (property.CodeValues != null) &&
                        (property.CodeValues.Length > 0))
                    {
                        lang.AppendCodes(
                                    ref enumSB,
                                    property,
                                    fhirType.NameCapitalized
                                    );
                    }

                    // **** check for having a Value Set ****

                    if (!string.IsNullOrEmpty(fhirType.Properties[propertyName].ValueSetUrl))
                    {
                        BuildValueSet(
                            lang,
                            ref valueSetSB,
                            $"{fhirType.NameCapitalized}{property.NameCapitalized}",
                            property.ValueSetUrl
                            );
                    }
                }

                // **** close our type ****

                lang.AppendFhirTypeClose(ref typeSB, fhirType);
            }

            // **** add all our pieces together ****

            fileSB.Append(enumSB);
            enumSB.Clear();

            fileSB.Append(valueSetSB);
            valueSetSB.Clear();

            fileSB.Append(typeSB);
            typeSB.Clear();


            // **** close our module ****

            lang.AppendModuleClose(ref fileSB, outputNamespace);

            // **** get the footer ****

            lang.AppendFileFooter(ref fileSB);

            // **** write the file ****

            writer.Write(fileSB.ToString());
        }

        private bool BuildValueSet(
                                    ILanguangeExporter lang,
                                    ref StringBuilder sb, 
                                    string alias, 
                                    string valueSetUrl
                                    )
        {
            if ((string.IsNullOrEmpty(valueSetUrl)) ||
                (!TryGetValueSet(valueSetUrl, out fhir.ValueSet valueSet)) ||
                (!_urlValueSetConceptsDict.ContainsKey(valueSetUrl)) ||
                (_urlValueSetConceptsDict[valueSetUrl].Length == 0))
            {
                return false;
            }

            // **** check for already exporting it ****

            if (_writtenValueSetAliases.Contains(alias))
            {
                return true;
            }

            // **** grab our value set name and alias, sanitized ****

            string sanitizedAlias = SanitizeForProperty(alias, lang.ReservedWords);
            string sanitizedName = SanitizeForProperty(valueSet.Name, lang.ReservedWords);

            // **** check for only needing to write the alias ****

            if ((!_writtenValueSetAliases.Contains(alias)) &&
                (_writtenValueSetNames.Contains(valueSet.Name)))
                //(_writtenValueSetUrls.Contains(valueSetUrl)))
            {
                // **** add our alias ****

                lang.AppendValueSetAlias(ref sb, sanitizedAlias, sanitizedName, valueSetUrl);

                // **** add to written aliases ****

                _writtenValueSetAliases.Add(alias);

                // **** done ****

                return true;
            }

            // **** get our open value set ****

            lang.AppendValueSetOpen(ref sb, sanitizedAlias, sanitizedName, valueSet);

            // **** traverse the expanded values ****

            foreach (fhir.CodeSystemConcept concept in _urlValueSetConceptsDict[valueSetUrl])
            {
                string sanitizedCodeName = SanitizeForProperty(concept.Code, lang.ReservedWords);

                // **** skip already exported ****

                if (_writtenValueCodes.Contains(sanitizedCodeName))
                {
                    continue;
                }

                if (lang.AppendValueSetCodeConcept(
                        ref sb, 
                        sanitizedName,
                        sanitizedCodeName, 
                        concept, 
                        _urlValueSetSystemDict[valueSetUrl]
                        )
                    )
                {
                    _writtenValueCodes.Add(sanitizedCodeName);
                }
            }

            // **** close our value set **** 

            lang.AppendValueSetClose(ref sb, sanitizedAlias, sanitizedName, valueSet);

            // **** flag written ****

            _writtenValueSetUrls.Add(valueSetUrl);
            _writtenValueSetNames.Add(valueSet.Name);

            // **** add our alias ****

            lang.AppendValueSetAlias(ref sb, sanitizedAlias, sanitizedName, valueSetUrl);
            _writtenValueSetAliases.Add(sanitizedName);

            // **** return our string ****

            return true;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>Check or create instance.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///-------------------------------------------------------------------------------------------------

        private static void CheckOrCreateInstance()
        {
            if (_instance == null)
            {
                _instance = new FhirTypeManager();
            }
        }

        #endregion Internal Functions . . .

    }
}
