using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using generate_fhir_prototype_bindings.Models;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;

namespace generate_fhir_prototype_bindings.Managers
{
    public class FhirTypeManager
    {
        #region Class Constants . . .

        /// <summary>The RegEx remove parentheses content.</summary>
        private const string _regexRemoveParenthesesContentDefinition = "\\(.*?\\)";

        #endregion Class Constants . . .

        #region Class Variables . . .

        /// <summary>The instance for singleton pattern.</summary>
        private static FhirTypeManager _instance;

        #endregion Class Variables . . .

        #region Instance Variables . . .

        private Dictionary<string, FhirType> _fhirTypeDict;

        private List<string> _fhirPrimitives;

        private Regex _regexRemoveParenthesesContent;

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

            _regexRemoveParenthesesContent = new Regex(_regexRemoveParenthesesContentDefinition);
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
        /// <summary>Process the primitive.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="name">          The name.</param>
        /// <param name="baseType">      Type of the base.</param>
        /// <param name="comment">       The comment.</param>
        /// <param name="isPrimitive">   True if is primitive, false if not.</param>
        /// <param name="sourceFilename">Filename of the source file.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void ProcessType(
                                        string name,
                                        string baseType,
                                        string comment,
                                        bool isPrimitive,
                                        string sourceFilename
                                        )
        {
            _instance._ProcessType(name, baseType, comment, isPrimitive, sourceFilename);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the primitive.</summary>
        ///
        /// <remarks>Gino Canessa, 7/9/2019.</remarks>
        ///
        /// <param name="name">          The name.</param>
        /// <param name="baseType">      Type of the base.</param>
        /// <param name="comment">       The comment.</param>
        /// <param name="sourceFilename">Filename of the source file.</param>
        ///-------------------------------------------------------------------------------------------------

        private void _ProcessType(
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
        /// <summary>Process the data element.</summary>
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

        public static void ProcessDataElement(
                                                string element,
                                                string baseType,
                                                string comment,
                                                string cardinality,
                                                bool isPrimitive,
                                                string sourceFilename
                                                )
        {
            _instance._ProcessDataElement(element, baseType, comment, cardinality, isPrimitive, sourceFilename);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the data element.</summary>
        ///
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="element">       The element.</param>
        /// <param name="baseType">      Type of the base.</param>
        /// <param name="comment">       The comment.</param>
        /// <param name="cardinality">   The cardinality.</param>
        /// <param name="isFhirPrimitive">   True if is primitive, false if not.</param>
        /// <param name="sourceFilename">Filename of the source file.</param>
        ///-------------------------------------------------------------------------------------------------

        private void _ProcessDataElement(
                                        string element,
                                        string baseType,
                                        string comment,
                                        string cardinality,
                                        bool isFhirPrimitive,
                                        string sourceFilename
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
            string pathCamelCase = "";

            // **** for debugging ****

            if (element.StartsWith("MeasureReport", StringComparison.Ordinal))
            {
                currentTypeNode = null;
            }

            //if (element.Contains("ActionDefinition.relatedAction", StringComparison.Ordinal))
            //{
            //    currentTypeNode = null;
            //}

            // **** traverse name parts and resolve each one ****

            for (int nameIndex = 0; nameIndex < namePartCount; nameIndex++)
            {
                //if ((currentTypeNode != null) && (currentTypeNode.Name == "Element"))
                //{
                //    Console.WriteLine("Here");
                //}

                // **** grab this name part ****

                string name = names[nameIndex];

                // **** remove markup prefixes in names ****

                while (!char.IsLetterOrDigit(name[0]))
                {
                    name = name.Substring(1);
                }

                // **** remove all spaces from names ****

                name = name.Replace(" ", "");

                // **** append the next part of our Camel Case path ****

                pathCamelCase += string.Concat(name.Substring(0, 1).ToUpper(), name.Substring(1));

                // **** top level always needs to be a type ****

                if (nameIndex == 0)
                {
                    // **** create a type for this within the manager ****

                    FindOrCreateType(
                        name,
                        pathCamelCase,
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
                        cardinality
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

                    currentTypeNode.Properties[name].TypeName = pathCamelCase;

                    // **** find or create a type for this ****

                    FindOrCreateType(
                        pathCamelCase,
                        pathCamelCase,
                        "Element",
                        currentTypeNode.Properties[name].Comment,
                        currentTypeNode.SourceFilename,
                        currentTypeNode.IsFhirPrimitive,
                        ref currentTypeNode
                        );
                }

                //// **** determine if this field needs to be promoted ****

                //if ((nameIndex < (namePartCount - 1)) &&
                //    (   string.IsNullOrEmpty(baseType) ||
                //        baseType[0].Equals('=') ||
                //        baseType[0].Equals('@') ||
                //        baseType.Equals("BackboneElement", StringComparison.Ordinal) ||
                //        baseType.Equals("Element", StringComparison.Ordinal) ||
                //        (_fhirTypeDict.ContainsKey(baseType) && _fhirTypeDict[baseType].IsFhirPrimitive)
                //    ))
                //{
                //    // **** change the type on the existing node ****

                //    currentTypeNode.TypeName = pathCamelCase;

                //    // **** create a type for this within the manager ****

                //    FindOrCreateType(
                //        pathCamelCase,
                //        pathCamelCase,
                //        pathCamelCase,
                //        comment,
                //        sourceFilename,
                //        isFhirPrimitive,
                //        ref currentTypeNode
                //        );

                //    // **** nothing else to do for top level ****

                //    continue;
                //}

                // ----------------------------------


                //// **** check to see if this child already exists within this type ****

                //if (currentTypeNode.Properties.ContainsKey(name))
                //{
                //    // **** grab our type name ****

                //    string nextTypeName = currentTypeNode.Properties[name].TypeName;

                //    // **** check for no type (implicit typing of element groups) ****

                //    if (string.IsNullOrEmpty(nextTypeName))
                //    {
                //        // **** use the path for this type ****

                //        nextTypeName = pathCamelCase;

                //        // **** update our node ****

                //        currentTypeNode.Properties[name].TypeName = nextTypeName;

                //        // **** create our new type node ****

                //        FhirType nextType = FhirType.CreateFhirType(
                //            nextTypeName,
                //            pathCamelCase,
                //            "Element",
                //            currentTypeNode.Properties[name].Comment,
                //            currentTypeNode.SourceFilename,
                //            false
                //            );

                //        // **** add to our dictionary ****

                //        _fhirTypeDict.Add(nextTypeName, nextType);

                //    }

                //    // **** move to the node indicated ****

                //    if (_fhirTypeDict.ContainsKey(currentTypeNode.Properties[name].TypeName))
                //    {
                //        // **** move to the desired type node ****

                //        currentTypeNode = _fhirTypeDict[currentTypeNode.Properties[name].TypeName];
                //    }

                //    // **** nothing else to process yet ****

                //    continue;
                //}




                //// **** look for Backbone elements ****

                //if (string.Equals(property.TypeName, "BackboneElement", StringComparison.Ordinal) ||
                //    string.Equals(property.TypeName, "Element"))
                //{
                //    if (!_fhirTypeDict.ContainsKey(pathCamelCase))
                //    {
                //        // **** create our new type node ****

                //        FhirType nextType = FhirType.CreateFhirType(
                //            pathCamelCase,
                //            pathCamelCase,
                //            property.TypeName,
                //            currentTypeNode.Properties[name].Comment,
                //            currentTypeNode.SourceFilename,
                //            false
                //            );

                //        // **** add to our dictionary ****

                //        _fhirTypeDict.Add(pathCamelCase, nextType);
                //    }

                //    // **** move our pointer ****

                //    currentTypeNode = _fhirTypeDict[pathCamelCase];
                //}

                //// **** check for new named types ****

                //if (baseType.StartsWith('=') || baseType.StartsWith('@'))
                //{
                //    // **** need to create a type for this if it doesn't exist ****

                //    if (!_fhirTypeDict.ContainsKey(property.TypeName))
                //    {
                //        // **** create our new type node ****

                //        FhirType nextType = FhirType.CreateFhirType(
                //            property.TypeName,
                //            pathCamelCase,
                //            "Element",
                //            currentTypeNode.Properties[name].Comment,
                //            currentTypeNode.SourceFilename,
                //            false
                //            );

                //        // **** add to our dictionary ****

                //        _fhirTypeDict.Add(property.TypeName, nextType);
                //    }

                //    // **** move our pointer ****

                //    currentTypeNode = _fhirTypeDict[property.TypeName];
                //}

            }


        }


        private void FindOrCreateType(
                                        string name,
                                        string pathCamelCase,
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

                if (!_fhirTypeDict.ContainsKey(typeName))
                {
                    // TODO(ginoc): Need to trigger load of base types to inherit fields and type information

                    _fhirTypeDict.Add(typeName, null);
                }

                // **** create this type ****

                currentTypeNode = FhirType.CreateFhirType(
                    name,
                    pathCamelCase,
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
        /// <remarks>Gino Canessa, 7/8/2019.</remarks>
        ///
        /// <param name="writer">         The writer.</param>
        /// <param name="outputNamespace">The output namespace.</param>
        ///-------------------------------------------------------------------------------------------------

        public static void OutputTypeScript(StreamWriter writer, string outputNamespace)
        {
            _instance._OutputTypeScript(writer, outputNamespace);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Output type script.</summary>
        ///
        /// <remarks>Gino Canessa, 7/9/2019.</remarks>
        ///
        /// <param name="writer">         The writer.</param>
        /// <param name="outputNamespace">The output namespace.</param>
        ///-------------------------------------------------------------------------------------------------

        private void _OutputTypeScript(StreamWriter writer, string outputNamespace)
        {
            // **** write our header ****

            writer.Write($"/** GENERATED FILE on: {DateTime.Now} **/\n\n");

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

                //if (node.Name.StartsWith("MeasureReport", StringComparison.Ordinal))
                //{
                //    Console.WriteLine("");
                //}

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

        #endregion Class Interface . . .

        #region Instance Interface . . .

        #endregion Instance Interface . . .

        #region Internal Functions . . .

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
