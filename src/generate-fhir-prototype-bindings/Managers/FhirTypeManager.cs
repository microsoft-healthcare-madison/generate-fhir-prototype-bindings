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

        private HashSet<string> _writtenValueSets;

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

            _writtenValueSets = new HashSet<string>();

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
        /// <summary>Output type script.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="writer">         The writer.</param>
        /// <param name="outputNamespace">The output namespace.</param>
        /// <param name="matchNames">     List of names of the matches.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void OutputTypeScript(
                                            StreamWriter writer, 
                                            string outputNamespace, 
                                            string typesToOutput,
                                            bool excludeCodes
                                            )
        {
            _instance._OutputTypeScript(writer, outputNamespace, typesToOutput, excludeCodes);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Output C#.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="writer">         The writer.</param>
        /// <param name="outputNamespace">The output namespace.</param>
        /// <param name="typesToOutput">  The types to output.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void OutputCSharp(StreamWriter writer, string outputNamespace, string typesToOutput)
        {
            _instance._OutputCSharp(writer, outputNamespace, typesToOutput);
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

        public static string GetCSharpValueSetString(string alias, string key)
        {
            return _instance._GetCSharpValueSetString(alias, key);
        }

        public static string GetTypeScriptValueSetString(string alias, string key)
        {
            return _instance._GetTypeScriptValueSetString(alias, key);
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

        private static bool IsAllDigits(string value)
        {
            foreach (char c in value)
            {
                if ((c < '0') || (c > '9'))
                {
                    return false;
                }
            }
            return true;
        }
        public static string SanitizeForProperty(string value)
        {
            // **** need to check for all digits ****

            if (IsAllDigits(value))
            {
                return $"VAL_{value}";
            }

            // **** check for symbols we need to replace ****

            if (value.Contains("<="))
            {
                value = value.Replace("<=", "LESS_THAN_OR_EQUAL");
            }

            if (value.Contains("<"))
            {
                value = value.Replace("<", "LESS_THAN");
            }

            if (value.Contains(">="))
            {
                value = value.Replace(">=", "GREATER_THAN_OR_EQUAL");
            }

            if (value.Contains(">"))
            {
                value = value.Replace(">", "GREATER_THAN");
            }

            if (value.Contains("!="))
            {
                value = value.Replace("!=", "NOT_EQUAL");
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

            // **** ****

            return _regexSanitizeForProperty.Replace(value, "_");
        }

        private string _GetTypeScriptValueSetString(string alias, string valueSetUrl)
        {
            if ((string.IsNullOrEmpty(valueSetUrl)) ||
                (!_urlValueSetDict.ContainsKey(valueSetUrl)) ||
                (_writtenValueSets.Contains(alias)))
            {
                return "";
            }

            // **** make sure we have successfully expanded this value set ****

            if (!_urlValueSetConceptsDict.ContainsKey(valueSetUrl))
            {
                return "";
            }

            // **** grab our value set ****

            fhir.ValueSet valueSet = _urlValueSetDict[valueSetUrl];

            string sanitizedName = SanitizeForProperty(valueSet.Name);

            // **** ****

            StringBuilder codingSB = new StringBuilder();
            StringBuilder interfaceSB = new StringBuilder();
            StringBuilder exportSB = new StringBuilder();

            // **** check if we need to output the base class ****

            if (!_writtenValueSets.Contains(valueSetUrl))
            {
                string comment;
                
                if (!string.IsNullOrEmpty(valueSet.Description))
                {
                    comment = valueSet.Description.Replace("\n", "\n * ").Replace("\r", "");
                }
                else
                {
                    comment = $"Expanded ValueSet from {valueSetUrl}";
                }

                // **** start with a comment ****

                exportSB.Append($"/*\n * {comment}\n */\n");

                // **** start our high-order statements ****

                interfaceSB.Append($"interface {sanitizedName}_Interface {{\n");
                exportSB.Append($"export let {sanitizedName}: {sanitizedName}_Interface = {{\n");

                // **** traverse the expanded values ****

                foreach (fhir.CodeSystemConcept concept in _urlValueSetConceptsDict[valueSetUrl])
                {
                    string sanitizedCodeName = SanitizeForProperty(concept.Code);

                    // **** figure out the proper comment ****

                    if (!string.IsNullOrEmpty(concept.Definition))
                    {
                        comment = concept.Definition.Replace("\n", "\n\t * ").Replace("\r", "");
                    }
                    else if (!string.IsNullOrEmpty(concept.Display))
                    {
                        comment = concept.Display.Replace("\n", "\n\t * ").Replace("\r", "");
                    }
                    else
                    {
                        comment = $"Value for '{concept.Code}'";
                    }

                    // **** build the internal variable ****

                    codingSB.Append($"let {sanitizedName}_{sanitizedCodeName}: Coding = {{\n");
                    codingSB.Append($"\t\tcode: \"{concept.Code}\",\n");
                    if (!string.IsNullOrEmpty(concept.Display))
                    {
                        codingSB.Append($"\t\tdisplay: \"{concept.Display}\",\n");
                    }
                    codingSB.Append($"\t\tsystem: \"{_urlValueSetSystemDict[valueSetUrl]}\"\n");
                    codingSB.Append($"\t}};\n");

                    // **** add this code to our interface ****

                    interfaceSB.Append($"\t{sanitizedCodeName}: Coding,\n");

                    // **** add this code to our export ****

                    exportSB.Append($"\t/*\n\t * {comment}\n\t */\n");
                    exportSB.Append($"\t{sanitizedCodeName}: {sanitizedName}_{sanitizedCodeName},\n");
                }

                // **** close our interface and export **** 

                interfaceSB.Append("};\n");
                exportSB.Append("}\n");

                // **** flag written ****

                _writtenValueSets.Add(valueSetUrl);
            }

            // **** add our alias ****

            exportSB.Append($"/*\n * ValueSet alias for {valueSetUrl}\n */\n" +
                $"export let {SanitizeForProperty(alias)}Values = {sanitizedName};\n");

            // **** flag written ****

            _writtenValueSets.Add(alias);

            // **** return our string ****

            return codingSB.ToString() + interfaceSB.ToString() + exportSB.ToString();
        }

        private string _GetCSharpValueSetString(string alias, string valueSetUrl)
        {
            if ((string.IsNullOrEmpty(valueSetUrl)) ||
                (!_urlValueSetDict.ContainsKey(valueSetUrl)) ||
                (_writtenValueSets.Contains(alias)))
            {
                return "";
            }

            // **** make sure we have successfully expanded this value set ****

            if (!_urlValueSetConceptsDict.ContainsKey(valueSetUrl))
            {
                return "";
            }

            // **** grab our value set ****

            fhir.ValueSet valueSet = _urlValueSetDict[valueSetUrl];

            // **** ****

            StringBuilder sb = new StringBuilder();

            // **** check if we need to output the base class ****

            if (!_writtenValueSets.Contains(valueSetUrl))
            {
                string comment;

                if (!string.IsNullOrEmpty(valueSet.Description))
                {
                    comment = valueSet.Description.Replace("\n", "\n\t/// ").Replace("\r", "");
                }
                else
                {
                    comment = $"Expanded ValueSet from {valueSetUrl}";
                }

                // **** start with a comment ****

                sb.Append($"\t///<summary>{comment}</summary>\n");

                // **** start our value set ****

                sb.Append($"\tpublic abstract class {SanitizeForProperty(valueSet.Name)}\n\t{{\n");

                // **** traverse the expanded values ****

                foreach (fhir.CodeSystemConcept concept in _urlValueSetConceptsDict[valueSetUrl])
                {
                    if (!string.IsNullOrEmpty(concept.Definition))
                    {
                        comment = concept.Definition.Replace("\n", "\n\t\t/// ").Replace("\r", "");
                    }
                    else if (!string.IsNullOrEmpty(concept.Display))
                    {
                        comment = concept.Display.Replace("\n", "\n\t\t/// ").Replace("\r", "");
                    }
                    else
                    {
                        comment = $"Value for '{concept.Code}'";
                    }

                    // **** start with a comment ****

                    sb.Append($"\t\t///<summary>{comment}</summary>\n");

                    // **** start our coding ****

                    sb.Append($"\t\tpublic readonly Coding {SanitizeForProperty(concept.Code)} = new Coding\n\t\t{{\n");
                    sb.Append($"\t\t\tCode = \"{concept.Code}\",\n");
                    if (!string.IsNullOrEmpty(concept.Display))
                    {
                        sb.Append($"\t\t\tDisplay = \"{concept.Display}\",\n");
                    }
                    sb.Append($"\t\t\tSystem = \"{_urlValueSetSystemDict[valueSetUrl]}\"\n");
                    sb.Append($"\t\t}};\n");
                }

                // **** close our class **** 

                sb.Append($"\t}};\n");

                // **** flag written ****

                _writtenValueSets.Add(valueSetUrl);
            }

            // **** add our alias ****

            sb.Append($"\t\t///<summary>ValueSet alias for {valueSetUrl}</summary>\n" +
                $"\tpublic abstract class {SanitizeForProperty(alias)}Values : {SanitizeForProperty(valueSet.Name)} {{ }}\n");

            // **** flag written ****

            _writtenValueSets.Add(alias);

            // **** return our string ****

            return sb.ToString();
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
                        cardinality
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

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Output type script.</summary>
        ///
        /// <remarks>Gino Canessa, 7/9/2019.</remarks>
        ///
        /// <param name="writer">         The writer.</param>
        /// <param name="outputNamespace">The output namespace.</param>
        /// <param name="typesToOutput">  The types to output.</param>
        /// <param name="excludeCodes">   True to exclude, false to include the codes.</param>
        ///-------------------------------------------------------------------------------------------------

        private void _OutputTypeScript(
                                        StreamWriter writer, 
                                        string outputNamespace, 
                                        string typesToOutput,
                                        bool excludeCodes
                                        )
        {
            // **** have not written any code systems ****

            _writtenValueSets.Clear();

            // **** write our header ****

            writer.Write($"/** GENERATED FILE **/\n");
            writer.Write($"/** Generated by: {Environment.UserName} at {DateTime.Now} **/\n");

            if (!string.IsNullOrEmpty(typesToOutput))
            {
                writer.Write($"/** Restricted to types: {typesToOutput} **/\n");
            }
            
            // **** start with our module ****

            writer.Write($"export module {outputNamespace} {{\n");

            // **** sort by name ****

            _fhirPrimitives.Sort();

            // **** first loop is for basic types within primitives ****

            foreach (string primitiveName in _fhirPrimitives)
            {
                // **** grab our node ****

                FhirType node = _fhirTypeDict[primitiveName];

                // **** skip what we do not want ****

                if ((node.IsCircular) || (node.Properties.Count != 0))
                {
                    continue;
                }

                // **** output to our file ****

                writer.Write(node.GetTypeScriptString());
            }

            // **** second loop is for Resources types within primitives ****

            foreach (string primitiveName in _fhirPrimitives)
            {
                // **** grab our node ****

                FhirType node = _fhirTypeDict[primitiveName];

                // **** skip what we do not want ****

                if (node.Properties.Count == 0)
                {
                    continue;
                }

                // **** output to our file ****

                writer.Write(node.GetTypeScriptString());
            }

            // **** output the rest of the types ****

            List<string> nodeNames = _fhirTypeDict.Keys.ToList<string>();
            nodeNames.Sort();

            foreach (string nodeName in nodeNames)
            {
                // **** grab our node ****

                FhirType node = _fhirTypeDict[nodeName];

                if (node == null)
                {
                    continue;
                }

                // **** skip circular references and primitives ****

                if (node.IsFhirPrimitive)
                {
                    continue;
                }

                // **** output to our file ****

                writer.Write(node.GetTypeScriptString());
            }

            // **** close our module ****

            writer.Write($"}} // close module: {outputNamespace}\n");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Output C#.</summary>
        ///
        /// <remarks>Gino Canessa, 8/20/2019.</remarks>
        ///
        /// <param name="writer">         The writer.</param>
        /// <param name="outputNamespace">The output namespace.</param>
        /// <param name="typesToOutput">  The types to output.</param>
        ///-------------------------------------------------------------------------------------------------

        private void _OutputCSharp(StreamWriter writer, string outputNamespace, string typesToOutput)
        {
            // **** have not written any code systems ****

            _writtenValueSets.Clear();

            // **** write our header ****

            writer.Write($"/** GENERATED FILE **/\n");
            writer.Write($"/** Generated by: {Environment.UserName} at {DateTime.Now} **/\n");

            if (!string.IsNullOrEmpty(typesToOutput))
            {
                writer.Write($"/** Restricted to types: {typesToOutput} **/\n");
            }

            writer.Write("using System;\nusing System.Collections.Generic;\n\n");

            // **** start our namespace ****

            writer.Write($"namespace {outputNamespace}\n{{\n");

            Dictionary<string, FhirBasicNode.LanguagePrimitiveType> languagePrimitiveDict = new Dictionary<string, FhirBasicNode.LanguagePrimitiveType>();

            // **** first loop is to discover the primitive types ****

            foreach (string primitiveName in _fhirPrimitives)
            {
                // **** grab our node ****

                FhirType node = _fhirTypeDict[primitiveName];

                // **** skip what we do not want ****

                if (node.Properties.Count != 0)
                {
                    continue;
                }

                // **** check for a language primitive ****

                if (node.LanguagePrimitive != FhirBasicNode.LanguagePrimitiveType.None)
                {
                    // **** add to our lookup dictionary ****

                    languagePrimitiveDict.Add(node.Name.Trim().ToLower(), node.LanguagePrimitive);

                    // **** don't do anything else with this type ****

                    continue;
                }
            }

            // **** force add any missing primitives we need to change ****

            if (!languagePrimitiveDict.ContainsKey("number"))
            {
                languagePrimitiveDict.Add("number", FhirBasicNode.LanguagePrimitiveType.TypeNumber);
            }

            if (!languagePrimitiveDict.ContainsKey("datetime"))
            {
                languagePrimitiveDict.Add("datetime", FhirBasicNode.LanguagePrimitiveType.TypeDateTime);
            }

            // **** output the rest of our types ****

            List<string> nodeNames = _fhirTypeDict.Keys.ToList<string>();
            nodeNames.Sort();

            foreach (string nodeName in nodeNames)
            {
                // **** grab our node ****

                FhirType node = _fhirTypeDict[nodeName];

                if (node == null)
                {
                    continue;
                }

                // **** skip circular references and primitives ****

                if (node.LanguagePrimitive != FhirBasicNode.LanguagePrimitiveType.None)
                { 
                    continue;
                }

                // **** output to our file ****

                writer.Write(node.GetCSharpString(languagePrimitiveDict));
            }

            // **** close our module ****

            writer.Write($"}} // close namespace: {outputNamespace}\n");
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
