/** GENERATED FILE on: 8/12/2019 9:08:04 AM **/

using System;
using System.Collections.Generic;

namespace fhir
{
	///<summary>
	///An address expressed using postal conventions (as opposed to GPS or other location definition formats).  This data type may be used to convey addresses for use in delivering mail as well as for visiting locations which might not be valid for mail delivery.  There are a variety of postal address formats defined around the world.
	///</summary>
	///<source-file>address.xml</source-file>
	public class Address : Element
	{
		///<summary>The name of the city, town, suburb, village or other community or delivery center</summary>
		public string City { get; set; }
		///<summary>May contain extended information for property: 'City'</summary>
		public Element _City { get; set; }
		///<summary>Country - a nation as commonly understood or generally accepted</summary>
		public string Country { get; set; }
		///<summary>May contain extended information for property: 'Country'</summary>
		public Element _Country { get; set; }
		///<summary>The name of the administrative area (county)</summary>
		public string District { get; set; }
		///<summary>May contain extended information for property: 'District'</summary>
		public Element _District { get; set; }
		///<summary>This component contains the house number, apartment number, street name, street direction,  P.O. Box number, delivery hints, and similar address information</summary>
		public string[] Line { get; set; }
		///<summary>May contain extended information for property: 'Line'</summary>
		public Element[] _Line { get; set; }
		///<summary>Time period when address was/is in use</summary>
		public Period Period { get; set; }
		///<summary>May contain extended information for property: 'Period'</summary>
		public Element _Period { get; set; }
		///<summary>A postal code designating a region defined by the postal service</summary>
		public string PostalCode { get; set; }
		///<summary>May contain extended information for property: 'PostalCode'</summary>
		public Element _PostalCode { get; set; }
		///<summary>Sub-unit of a country with limited sovereignty in a federally organized country. A code may be used if codes are in common use (e.g. US 2 letter state codes)</summary>
		public string State { get; set; }
		///<summary>May contain extended information for property: 'State'</summary>
		public Element _State { get; set; }
		///<summary>Specifies the entire address as it should be displayed e.g. on a postal label. This may be provided instead of or as well as the specific parts</summary>
		public string Text { get; set; }
		///<summary>May contain extended information for property: 'Text'</summary>
		public Element _Text { get; set; }
		///<summary>Distinguishes between physical addresses (those you can visit) and mailing addresses (e.g. PO Boxes and care-of addresses). Most addresses are both</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
		///<summary>The purpose of this address</summary>
		public string Use { get; set; }
		///<summary>May contain extended information for property: 'Use'</summary>
		public Element _Use { get; set; }
	}
	///<summary>
	///A  text note which also  contains information about who made the statement and when
	///</summary>
	///<source-file>annotation.xml</source-file>
	public class Annotation : Element
	{
		///<summary>The individual responsible for making the annotation.</summary>
		public Reference AuthorReference { get; set; }
		///<summary>May contain extended information for property: 'AuthorReference'</summary>
		public Element _AuthorReference { get; set; }
		///<summary>The individual responsible for making the annotation.</summary>
		public string AuthorString { get; set; }
		///<summary>May contain extended information for property: 'AuthorString'</summary>
		public Element _AuthorString { get; set; }
		///<summary>The text of the annotation in markdown format</summary>
		public string Text { get; set; }
		///<summary>May contain extended information for property: 'Text'</summary>
		public Element _Text { get; set; }
		///<summary>Indicates when this particular annotation was made</summary>
		public string Time { get; set; }
		///<summary>May contain extended information for property: 'Time'</summary>
		public Element _Time { get; set; }
	}
	///<summary>
	///For referring to data content defined in other formats.
	///</summary>
	///<source-file>attachment.xml</source-file>
	public class Attachment : Element
	{
		///<summary>Identifies the type of the data in the attachment and allows a method to be chosen to interpret or render the data. Includes mime type parameters such as charset where appropriate</summary>
		public string ContentType { get; set; }
		///<summary>May contain extended information for property: 'ContentType'</summary>
		public Element _ContentType { get; set; }
		///<summary>The date that the attachment was first created</summary>
		public string Creation { get; set; }
		///<summary>May contain extended information for property: 'Creation'</summary>
		public Element _Creation { get; set; }
		///<summary>The actual data of the attachment - a sequence of bytes, base64 encoded</summary>
		public string Data { get; set; }
		///<summary>May contain extended information for property: 'Data'</summary>
		public Element _Data { get; set; }
		///<summary>The calculated hash of the data using SHA-1. Represented using base64.</summary>
		public string Hash { get; set; }
		///<summary>May contain extended information for property: 'Hash'</summary>
		public Element _Hash { get; set; }
		///<summary>The human language of the content. The value can be any valid value according to BCP 47</summary>
		public string Language { get; set; }
		///<summary>May contain extended information for property: 'Language'</summary>
		public Element _Language { get; set; }
		///<summary>The number of bytes of data that make up this attachment (before base64 encoding, if that is done).</summary>
		public decimal Size { get; set; }
		///<summary>May contain extended information for property: 'Size'</summary>
		public Element _Size { get; set; }
		///<summary>A label or set of text to display in place of the data</summary>
		public string Title { get; set; }
		///<summary>May contain extended information for property: 'Title'</summary>
		public Element _Title { get; set; }
		///<summary>A location where the data can be accessed</summary>
		public string Url { get; set; }
		///<summary>May contain extended information for property: 'Url'</summary>
		public Element _Url { get; set; }
	}
	///<summary>
	///Base definition for all elements that are defined inside a resource - but not those in a data type
	///</summary>
	///<source-file>backboneelement.xml</source-file>
	public class BackboneElement : Element
	{
		///<summary>May be used to represent additional information that is not part of the basic definition of the element and that modifies the understanding of the element in which it is contained and/or the understanding of the containing element's descendants. Usually modifier elements provide negation or qualification. To make the use of extensions safe and manageable, there is a strict set of governance applied to the definition and use of extensions. Though any implementer can define an extension, there is a set of requirements that SHALL be met as part of the definition of the extension. Applications processing a resource are required to check for modifier extensions.
		/// 
		/// Modifier extensions SHALL NOT change the meaning of any elements on Resource or DomainResource (including cannot change the meaning of modifierExtension itself)</summary>
		public Extension[] ModifierExtension { get; set; }
		///<summary>May contain extended information for property: 'ModifierExtension'</summary>
		public Element[] _ModifierExtension { get; set; }
	}
	///<summary>
	///A concept that may be defined by a formal reference to a terminology or ontology or may be provided by text
	///</summary>
	///<source-file>codeableconcept.xml</source-file>
	public class CodeableConcept : Element
	{
		///<summary>A reference to a code defined by a terminology system </summary>
		public Coding[] Coding { get; set; }
		///<summary>May contain extended information for property: 'Coding'</summary>
		public Element[] _Coding { get; set; }
		///<summary>A human language representation of the concept as seen/selected/uttered by the user who entered the data and/or which represents the intended meaning of the user</summary>
		public string Text { get; set; }
		///<summary>May contain extended information for property: 'Text'</summary>
		public Element _Text { get; set; }
	}
	///<summary>
	///A reference to a code defined by a terminology system 
	///</summary>
	///<source-file>coding.xml</source-file>
	public class Coding : Element
	{
		///<summary>A symbol in syntax defined by the system. The symbol may be a predefined code or an expression in a syntax defined by the coding system (e.g. post-coordination)</summary>
		public string Code { get; set; }
		///<summary>May contain extended information for property: 'Code'</summary>
		public Element _Code { get; set; }
		///<summary>A representation of the meaning of the code in the system, following the rules of the system</summary>
		public string Display { get; set; }
		///<summary>May contain extended information for property: 'Display'</summary>
		public Element _Display { get; set; }
		///<summary>The identification of the code system that defines the meaning of the symbol in the code. </summary>
		public string System { get; set; }
		///<summary>May contain extended information for property: 'System'</summary>
		public Element _System { get; set; }
		///<summary>Indicates that this coding was chosen by a user directly - e.g. off a pick list of available items (codes or displays)</summary>
		public bool UserSelected { get; set; }
		///<summary>May contain extended information for property: 'UserSelected'</summary>
		public Element _UserSelected { get; set; }
		///<summary>The version of the code system which was used when choosing this code. Note that a well-maintained code system does not need the version reported, because the meaning of codes is consistent across versions. However this cannot consistently be assured, and when the meaning is not guaranteed to be consistent, the version SHOULD be exchanged</summary>
		public string Version { get; set; }
		///<summary>May contain extended information for property: 'Version'</summary>
		public Element _Version { get; set; }
	}
	///<summary>
	///Specifies contact information for a person or organization
	///</summary>
	///<source-file>contactdetail.xml</source-file>
	public class ContactDetail : Element
	{
		///<summary>The name of an individual to contact</summary>
		public string Name { get; set; }
		///<summary>May contain extended information for property: 'Name'</summary>
		public Element _Name { get; set; }
		///<summary>The contact details for the individual (if a name was provided) or the organization</summary>
		public ContactPoint[] Telecom { get; set; }
		///<summary>May contain extended information for property: 'Telecom'</summary>
		public Element[] _Telecom { get; set; }
	}
	///<summary>
	///Details for all kinds of technology mediated contact points for a person or organization, including telephone, email, etc.
	///</summary>
	///<source-file>contactpoint.xml</source-file>
	public class ContactPoint : Element
	{
		///<summary>Time period when the contact point was/is in use</summary>
		public Period Period { get; set; }
		///<summary>May contain extended information for property: 'Period'</summary>
		public Element _Period { get; set; }
		///<summary>Specifies a preferred order in which to use a set of contacts. ContactPoints with lower rank values are more preferred than those with higher rank values</summary>
		public decimal Rank { get; set; }
		///<summary>May contain extended information for property: 'Rank'</summary>
		public Element _Rank { get; set; }
		///<summary>Telecommunications form for contact point - what communications system is required to make use of the contact</summary>
		public string System { get; set; }
		///<summary>May contain extended information for property: 'System'</summary>
		public Element _System { get; set; }
		///<summary>Identifies the purpose for the contact point</summary>
		public string Use { get; set; }
		///<summary>May contain extended information for property: 'Use'</summary>
		public Element _Use { get; set; }
		///<summary>The actual contact point details, in a form that is meaningful to the designated communication system (i.e. phone number or email address).</summary>
		public string Value { get; set; }
		///<summary>May contain extended information for property: 'Value'</summary>
		public Element _Value { get; set; }
	}
	///<summary>
	///A contributor to the content of a knowledge asset, including authors, editors, reviewers, and endorsers
	///</summary>
	///<source-file>contributor.xml</source-file>
	public class Contributor : Element
	{
		///<summary>Contact details to assist a user in finding and communicating with the contributor</summary>
		public ContactDetail[] Contact { get; set; }
		///<summary>May contain extended information for property: 'Contact'</summary>
		public Element[] _Contact { get; set; }
		///<summary>The name of the individual or organization responsible for the contribution</summary>
		public string Name { get; set; }
		///<summary>May contain extended information for property: 'Name'</summary>
		public Element _Name { get; set; }
		///<summary>The type of contributor</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
	}
	///<summary>
	///Describes a required data item for evaluation in terms of the type of data, and optional code or date-based filters of the data
	///</summary>
	///<source-file>datarequirement.xml</source-file>
	public class DataRequirement : Element
	{
		///<summary>Code filters specify additional constraints on the data, specifying the value set of interest for a particular element of the data. Each code filter defines an additional constraint on the data, i.e. code filters are AND'ed, not OR'ed</summary>
		public DataRequirementCodeFilter[] CodeFilter { get; set; }
		///<summary>May contain extended information for property: 'CodeFilter'</summary>
		public Element[] _CodeFilter { get; set; }
		///<summary>Date filters specify additional constraints on the data in terms of the applicable date range for specific elements. Each date filter specifies an additional constraint on the data, i.e. date filters are AND'ed, not OR'ed</summary>
		public DataRequirementDateFilter[] DateFilter { get; set; }
		///<summary>May contain extended information for property: 'DateFilter'</summary>
		public Element[] _DateFilter { get; set; }
		///<summary>Specifies a maximum number of results that are required (uses the _count search parameter)</summary>
		public decimal Limit { get; set; }
		///<summary>May contain extended information for property: 'Limit'</summary>
		public Element _Limit { get; set; }
		///<summary>Indicates that specific elements of the type are referenced by the knowledge module and must be supported by the consumer in order to obtain an effective evaluation. This does not mean that a value is required for this element, only that the consuming system must understand the element and be able to provide values for it if they are available. 
		/// 
		/// The value of mustSupport SHALL be a FHIRPath resolveable on the type of the DataRequirement. The path SHALL consist only of identifiers, constant indexers, and .resolve() (see the [Simple FHIRPath Profile](fhirpath.html#simple) for full details)</summary>
		public string[] MustSupport { get; set; }
		///<summary>May contain extended information for property: 'MustSupport'</summary>
		public Element[] _MustSupport { get; set; }
		///<summary>The profile of the required data, specified as the uri of the profile definition</summary>
		public string[] Profile { get; set; }
		///<summary>May contain extended information for property: 'Profile'</summary>
		public Element[] _Profile { get; set; }
		///<summary>Specifies the order of the results to be returned</summary>
		public DataRequirementSort[] Sort { get; set; }
		///<summary>May contain extended information for property: 'Sort'</summary>
		public Element[] _Sort { get; set; }
		///<summary>The intended subjects of the data requirement. If this element is not provided, a Patient subject is assumed</summary>
		public CodeableConcept SubjectCodeableConcept { get; set; }
		///<summary>May contain extended information for property: 'SubjectCodeableConcept'</summary>
		public Element _SubjectCodeableConcept { get; set; }
		///<summary>The intended subjects of the data requirement. If this element is not provided, a Patient subject is assumed</summary>
		public Reference SubjectReference { get; set; }
		///<summary>May contain extended information for property: 'SubjectReference'</summary>
		public Element _SubjectReference { get; set; }
		///<summary>The type of the required data, specified as the type name of a resource. For profiles, this value is set to the type of the base resource of the profile</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
	}
	///<summary>
	///Code filters specify additional constraints on the data, specifying the value set of interest for a particular element of the data. Each code filter defines an additional constraint on the data, i.e. code filters are AND'ed, not OR'ed
	///</summary>
	///<source-file>datarequirement.xml</source-file>
	public class DataRequirementCodeFilter : Element
	{
		///<summary>The codes for the code filter. If values are given, the filter will return only those data items for which the code-valued attribute specified by the path has a value that is one of the specified codes. If codes are specified in addition to a value set, the filter returns items matching a code in the value set or one of the specified codes</summary>
		public Coding[] Code { get; set; }
		///<summary>May contain extended information for property: 'Code'</summary>
		public Element[] _Code { get; set; }
		///<summary>The code-valued attribute of the filter. The specified path SHALL be a FHIRPath resolveable on the specified type of the DataRequirement, and SHALL consist only of identifiers, constant indexers, and .resolve(). The path is allowed to contain qualifiers (.) to traverse sub-elements, as well as indexers ([x]) to traverse multiple-cardinality sub-elements (see the [Simple FHIRPath Profile](fhirpath.html#simple) for full details). Note that the index must be an integer constant. The path must resolve to an element of type code, Coding, or CodeableConcept</summary>
		public string Path { get; set; }
		///<summary>May contain extended information for property: 'Path'</summary>
		public Element _Path { get; set; }
		///<summary>A token parameter that refers to a search parameter defined on the specified type of the DataRequirement, and which searches on elements of type code, Coding, or CodeableConcept</summary>
		public string SearchParam { get; set; }
		///<summary>May contain extended information for property: 'SearchParam'</summary>
		public Element _SearchParam { get; set; }
		///<summary>The valueset for the code filter. The valueSet and code elements are additive. If valueSet is specified, the filter will return only those data items for which the value of the code-valued element specified in the path is a member of the specified valueset</summary>
		public string ValueSet { get; set; }
		///<summary>May contain extended information for property: 'ValueSet'</summary>
		public Element _ValueSet { get; set; }
	}
	///<summary>
	///Date filters specify additional constraints on the data in terms of the applicable date range for specific elements. Each date filter specifies an additional constraint on the data, i.e. date filters are AND'ed, not OR'ed
	///</summary>
	///<source-file>datarequirement.xml</source-file>
	public class DataRequirementDateFilter : Element
	{
		///<summary>The date-valued attribute of the filter. The specified path SHALL be a FHIRPath resolveable on the specified type of the DataRequirement, and SHALL consist only of identifiers, constant indexers, and .resolve(). The path is allowed to contain qualifiers (.) to traverse sub-elements, as well as indexers ([x]) to traverse multiple-cardinality sub-elements (see the [Simple FHIRPath Profile](fhirpath.html#simple) for full details). Note that the index must be an integer constant. The path must resolve to an element of type date, dateTime, Period, Schedule, or Timing</summary>
		public string Path { get; set; }
		///<summary>May contain extended information for property: 'Path'</summary>
		public Element _Path { get; set; }
		///<summary>A date parameter that refers to a search parameter defined on the specified type of the DataRequirement, and which searches on elements of type date, dateTime, Period, Schedule, or Timing</summary>
		public string SearchParam { get; set; }
		///<summary>May contain extended information for property: 'SearchParam'</summary>
		public Element _SearchParam { get; set; }
		///<summary>The value of the filter. If period is specified, the filter will return only those data items that fall within the bounds determined by the Period, inclusive of the period boundaries. If dateTime is specified, the filter will return only those data items that are equal to the specified dateTime. If a Duration is specified, the filter will return only those data items that fall within Duration before now</summary>
		public string ValueDateTime { get; set; }
		///<summary>May contain extended information for property: 'ValueDateTime'</summary>
		public Element _ValueDateTime { get; set; }
		///<summary>The value of the filter. If period is specified, the filter will return only those data items that fall within the bounds determined by the Period, inclusive of the period boundaries. If dateTime is specified, the filter will return only those data items that are equal to the specified dateTime. If a Duration is specified, the filter will return only those data items that fall within Duration before now</summary>
		public Duration ValueDuration { get; set; }
		///<summary>May contain extended information for property: 'ValueDuration'</summary>
		public Element _ValueDuration { get; set; }
		///<summary>The value of the filter. If period is specified, the filter will return only those data items that fall within the bounds determined by the Period, inclusive of the period boundaries. If dateTime is specified, the filter will return only those data items that are equal to the specified dateTime. If a Duration is specified, the filter will return only those data items that fall within Duration before now</summary>
		public Period ValuePeriod { get; set; }
		///<summary>May contain extended information for property: 'ValuePeriod'</summary>
		public Element _ValuePeriod { get; set; }
	}
	///<summary>
	///Specifies the order of the results to be returned
	///</summary>
	///<source-file>datarequirement.xml</source-file>
	public class DataRequirementSort : Element
	{
		///<summary>The direction of the sort, ascending or descending</summary>
		public string Direction { get; set; }
		///<summary>May contain extended information for property: 'Direction'</summary>
		public Element _Direction { get; set; }
		///<summary>The attribute of the sort. The specified path must be resolvable from the type of the required data. The path is allowed to contain qualifiers (.) to traverse sub-elements, as well as indexers ([x]) to traverse multiple-cardinality sub-elements. Note that the index must be an integer constant</summary>
		public string Path { get; set; }
		///<summary>May contain extended information for property: 'Path'</summary>
		public Element _Path { get; set; }
	}
	///<summary>
	///A resource that includes narrative, extensions, and contained resources.
	///</summary>
	///<source-file>domainresource-spreadsheet.xml</source-file>
	public class DomainResource : Resource
	{
		///<summary>These resources do not have an independent existence apart from the resource that contains them - they cannot be identified independently, and nor can they have their own independent transaction scope</summary>
		public Resource[] Contained { get; set; }
		///<summary>May contain extended information for property: 'Contained'</summary>
		public Element[] _Contained { get; set; }
		///<summary>May be used to represent additional information that is not part of the basic definition of the resource. To make the use of extensions safe and manageable, there is a strict set of governance  applied to the definition and use of extensions. Though any implementer can define an extension, there is a set of requirements that SHALL be met as part of the definition of the extension</summary>
		public Extension[] Extension { get; set; }
		///<summary>May contain extended information for property: 'Extension'</summary>
		public Element[] _Extension { get; set; }
		///<summary>May be used to represent additional information that is not part of the basic definition of the resource and that modifies the understanding of the element that contains it and/or the understanding of the containing element's descendants. Usually modifier elements provide negation or qualification. To make the use of extensions safe and manageable, there is a strict set of governance applied to the definition and use of extensions. Though any implementer is allowed to define an extension, there is a set of requirements that SHALL be met as part of the definition of the extension. Applications processing a resource are required to check for modifier extensions.
		/// 
		/// Modifier extensions SHALL NOT change the meaning of any elements on Resource or DomainResource (including cannot change the meaning of modifierExtension itself)</summary>
		public Extension[] ModifierExtension { get; set; }
		///<summary>May contain extended information for property: 'ModifierExtension'</summary>
		public Element[] _ModifierExtension { get; set; }
		///<summary>A human-readable narrative that contains a summary of the resource and can be used to represent the content of the resource to a human. The narrative need not encode all the structured data, but is required to contain sufficient detail to make it "clinically safe" for a human to just read the narrative. Resource definitions may define what content should be represented in the narrative to ensure clinical safety</summary>
		public Narrative Text { get; set; }
		///<summary>May contain extended information for property: 'Text'</summary>
		public Element _Text { get; set; }
	}
	///<summary>
	///A length of time
	///</summary>
	///<source-file>duration.xml</source-file>
	public class Duration : Quantity
	{
	}
	///<summary>
	///Base definition for all elements in a resource
	///</summary>
	///<source-file>element.xml</source-file>
	public class Element
	{
		///<summary>May be used to represent additional information that is not part of the basic definition of the element. To make the use of extensions safe and manageable, there is a strict set of governance  applied to the definition and use of extensions. Though any implementer can define an extension, there is a set of requirements that SHALL be met as part of the definition of the extension</summary>
		public Extension[] Extension { get; set; }
		///<summary>May contain extended information for property: 'Extension'</summary>
		public Element[] _Extension { get; set; }
		///<summary>Unique id for the element within a resource (for internal references). This may be any string value that does not contain spaces</summary>
		public string Id { get; set; }
		///<summary>May contain extended information for property: 'Id'</summary>
		public Element _Id { get; set; }
	}
	///<summary>
	///Captures constraints on each element within the resource, profile, or extension
	///</summary>
	///<source-file>elementdefinition.xml</source-file>
	public class ElementDefinition : BackboneElement
	{
		///<summary>Identifies additional names by which this element might also be known</summary>
		public string[] Alias { get; set; }
		///<summary>May contain extended information for property: 'Alias'</summary>
		public Element[] _Alias { get; set; }
		///<summary>Information about the base definition of the element, provided to make it unnecessary for tools to trace the deviation of the element through the derived and related profiles. When the element definition is not the original definition of an element - i.g. either in a constraint on another type, or for elements from a super type in a snap shot - then the information in provided in the element definition may be different to the base definition. On the original definition of the element, it will be same</summary>
		public ElementDefinitionBase Base { get; set; }
		///<summary>May contain extended information for property: 'Base'</summary>
		public Element _Base { get; set; }
		///<summary>Binds to a value set if this element is coded (code, Coding, CodeableConcept, Quantity), or the data types (string, uri)</summary>
		public ElementDefinitionBinding Binding { get; set; }
		///<summary>May contain extended information for property: 'Binding'</summary>
		public Element _Binding { get; set; }
		///<summary>A code that has the same meaning as the element in a particular terminology.</summary>
		public Coding[] Code { get; set; }
		///<summary>May contain extended information for property: 'Code'</summary>
		public Element[] _Code { get; set; }
		///<summary>Explanatory notes and implementation guidance about the data element, including notes about how to use the data properly, exceptions to proper use, etc. (Note: The text you are reading is specified in ElementDefinition.comment)</summary>
		public string Comment { get; set; }
		///<summary>May contain extended information for property: 'Comment'</summary>
		public Element _Comment { get; set; }
		///<summary>A reference to an invariant that may make additional statements about the cardinality or value in the instance</summary>
		public string[] Condition { get; set; }
		///<summary>May contain extended information for property: 'Condition'</summary>
		public Element[] _Condition { get; set; }
		///<summary>Formal constraints such as co-occurrence and other constraints that can be computationally evaluated within the context of the instance</summary>
		public ElementDefinitionConstraint[] Constraint { get; set; }
		///<summary>May contain extended information for property: 'Constraint'</summary>
		public Element[] _Constraint { get; set; }
		///<summary>Identifies an element defined elsewhere in the definition whose content rules should be applied to the current element. ContentReferences bring across all the rules that are in the ElementDefinition for the element, including definitions, cardinality constraints, bindings, invariants etc.</summary>
		public string ContentReference { get; set; }
		///<summary>May contain extended information for property: 'ContentReference'</summary>
		public Element _ContentReference { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Structure DefaultValueActionDefinition { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueActionDefinition'</summary>
		public Element _DefaultValueActionDefinition { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueActionDefinitionBehavior { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueActionDefinitionBehavior'</summary>
		public Element _DefaultValueActionDefinitionBehavior { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueActionDefinitionCustomization { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueActionDefinitionCustomization'</summary>
		public Element _DefaultValueActionDefinitionCustomization { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueActionDefinitionRelatedAction { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueActionDefinitionRelatedAction'</summary>
		public Element _DefaultValueActionDefinitionRelatedAction { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Address DefaultValueAddress { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueAddress'</summary>
		public Element _DefaultValueAddress { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Quantity DefaultValueAge { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueAge'</summary>
		public Element _DefaultValueAge { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Annotation DefaultValueAnnotation { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueAnnotation'</summary>
		public Element _DefaultValueAnnotation { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Attachment DefaultValueAttachment { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueAttachment'</summary>
		public Element _DefaultValueAttachment { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueBackboneElement { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueBackboneElement'</summary>
		public Element _DefaultValueBackboneElement { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueBase64Binary { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueBase64Binary'</summary>
		public Element _DefaultValueBase64Binary { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public bool DefaultValueBoolean { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueBoolean'</summary>
		public Element _DefaultValueBoolean { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueCanonical { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueCanonical'</summary>
		public Element _DefaultValueCanonical { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueCode { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueCode'</summary>
		public Element _DefaultValueCode { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public CodeableConcept DefaultValueCodeableConcept { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueCodeableConcept'</summary>
		public Element _DefaultValueCodeableConcept { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Coding DefaultValueCoding { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueCoding'</summary>
		public Element _DefaultValueCoding { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public ContactDetail DefaultValueContactDetail { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueContactDetail'</summary>
		public Element _DefaultValueContactDetail { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public ContactPoint DefaultValueContactPoint { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueContactPoint'</summary>
		public Element _DefaultValueContactPoint { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Contributor DefaultValueContributor { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueContributor'</summary>
		public Element _DefaultValueContributor { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Quantity DefaultValueCount { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueCount'</summary>
		public Element _DefaultValueCount { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public DataRequirement DefaultValueDataRequirement { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDataRequirement'</summary>
		public Element _DefaultValueDataRequirement { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueDataRequirementCodeFilter { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDataRequirementCodeFilter'</summary>
		public Element _DefaultValueDataRequirementCodeFilter { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueDataRequirementDateFilter { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDataRequirementDateFilter'</summary>
		public Element _DefaultValueDataRequirementDateFilter { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueDataRequirementSort { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDataRequirementSort'</summary>
		public Element _DefaultValueDataRequirementSort { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueDate { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDate'</summary>
		public Element _DefaultValueDate { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueDateTime { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDateTime'</summary>
		public Element _DefaultValueDateTime { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public decimal DefaultValueDecimal { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDecimal'</summary>
		public Element _DefaultValueDecimal { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Quantity DefaultValueDistance { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDistance'</summary>
		public Element _DefaultValueDistance { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public BackboneElement DefaultValueDosage { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDosage'</summary>
		public Element _DefaultValueDosage { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueDosageDoseAndRate { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDosageDoseAndRate'</summary>
		public Element _DefaultValueDosageDoseAndRate { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Quantity DefaultValueDuration { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueDuration'</summary>
		public Element _DefaultValueDuration { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public BackboneElement DefaultValueElementDefinition { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueElementDefinition'</summary>
		public Element _DefaultValueElementDefinition { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueElementDefinitionBase { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueElementDefinitionBase'</summary>
		public Element _DefaultValueElementDefinitionBase { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueElementDefinitionSlicing { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueElementDefinitionSlicing'</summary>
		public Element _DefaultValueElementDefinitionSlicing { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueElementDefinitionSlicingDiscriminator'</summary>
		public Element _DefaultValueElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public Element DefaultValueElementDefinitionType { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueElementDefinitionType'</summary>
		public Element _DefaultValueElementDefinitionType { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueId { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueId'</summary>
		public Element _DefaultValueId { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueInstant { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueInstant'</summary>
		public Element _DefaultValueInstant { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public decimal DefaultValueInteger { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueInteger'</summary>
		public Element _DefaultValueInteger { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueMarkdown { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueMarkdown'</summary>
		public Element _DefaultValueMarkdown { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueOid { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueOid'</summary>
		public Element _DefaultValueOid { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public decimal DefaultValuePositiveInt { get; set; }
		///<summary>May contain extended information for property: 'DefaultValuePositiveInt'</summary>
		public Element _DefaultValuePositiveInt { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueString { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueString'</summary>
		public Element _DefaultValueString { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueTime { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueTime'</summary>
		public Element _DefaultValueTime { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public decimal DefaultValueUnsignedInt { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueUnsignedInt'</summary>
		public Element _DefaultValueUnsignedInt { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueUri { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueUri'</summary>
		public Element _DefaultValueUri { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueUrl { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueUrl'</summary>
		public Element _DefaultValueUrl { get; set; }
		///<summary>The value that should be used if there is no value stated in the instance (e.g. 'if not otherwise specified, the abstract is false')</summary>
		public string DefaultValueUuid { get; set; }
		///<summary>May contain extended information for property: 'DefaultValueUuid'</summary>
		public Element _DefaultValueUuid { get; set; }
		///<summary>Provides a complete explanation of the meaning of the data element for human readability.  For the case of elements derived from existing elements (e.g. constraints), the definition SHALL be consistent with the base definition, but convey the meaning of the element in the particular context of use of the resource. (Note: The text you are reading is specified in ElementDefinition.definition)</summary>
		public string Definition { get; set; }
		///<summary>May contain extended information for property: 'Definition'</summary>
		public Element _Definition { get; set; }
		///<summary>A sample value for this element demonstrating the type of information that would typically be found in the element</summary>
		public ElementDefinitionExample[] Example { get; set; }
		///<summary>May contain extended information for property: 'Example'</summary>
		public Element[] _Example { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Structure FixedActionDefinition { get; set; }
		///<summary>May contain extended information for property: 'FixedActionDefinition'</summary>
		public Element _FixedActionDefinition { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedActionDefinitionBehavior { get; set; }
		///<summary>May contain extended information for property: 'FixedActionDefinitionBehavior'</summary>
		public Element _FixedActionDefinitionBehavior { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedActionDefinitionCustomization { get; set; }
		///<summary>May contain extended information for property: 'FixedActionDefinitionCustomization'</summary>
		public Element _FixedActionDefinitionCustomization { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedActionDefinitionRelatedAction { get; set; }
		///<summary>May contain extended information for property: 'FixedActionDefinitionRelatedAction'</summary>
		public Element _FixedActionDefinitionRelatedAction { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Address FixedAddress { get; set; }
		///<summary>May contain extended information for property: 'FixedAddress'</summary>
		public Element _FixedAddress { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Quantity FixedAge { get; set; }
		///<summary>May contain extended information for property: 'FixedAge'</summary>
		public Element _FixedAge { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Annotation FixedAnnotation { get; set; }
		///<summary>May contain extended information for property: 'FixedAnnotation'</summary>
		public Element _FixedAnnotation { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Attachment FixedAttachment { get; set; }
		///<summary>May contain extended information for property: 'FixedAttachment'</summary>
		public Element _FixedAttachment { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedBackboneElement { get; set; }
		///<summary>May contain extended information for property: 'FixedBackboneElement'</summary>
		public Element _FixedBackboneElement { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedBase64Binary { get; set; }
		///<summary>May contain extended information for property: 'FixedBase64Binary'</summary>
		public Element _FixedBase64Binary { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public bool FixedBoolean { get; set; }
		///<summary>May contain extended information for property: 'FixedBoolean'</summary>
		public Element _FixedBoolean { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedCanonical { get; set; }
		///<summary>May contain extended information for property: 'FixedCanonical'</summary>
		public Element _FixedCanonical { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedCode { get; set; }
		///<summary>May contain extended information for property: 'FixedCode'</summary>
		public Element _FixedCode { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public CodeableConcept FixedCodeableConcept { get; set; }
		///<summary>May contain extended information for property: 'FixedCodeableConcept'</summary>
		public Element _FixedCodeableConcept { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Coding FixedCoding { get; set; }
		///<summary>May contain extended information for property: 'FixedCoding'</summary>
		public Element _FixedCoding { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public ContactDetail FixedContactDetail { get; set; }
		///<summary>May contain extended information for property: 'FixedContactDetail'</summary>
		public Element _FixedContactDetail { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public ContactPoint FixedContactPoint { get; set; }
		///<summary>May contain extended information for property: 'FixedContactPoint'</summary>
		public Element _FixedContactPoint { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Contributor FixedContributor { get; set; }
		///<summary>May contain extended information for property: 'FixedContributor'</summary>
		public Element _FixedContributor { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Quantity FixedCount { get; set; }
		///<summary>May contain extended information for property: 'FixedCount'</summary>
		public Element _FixedCount { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public DataRequirement FixedDataRequirement { get; set; }
		///<summary>May contain extended information for property: 'FixedDataRequirement'</summary>
		public Element _FixedDataRequirement { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedDataRequirementCodeFilter { get; set; }
		///<summary>May contain extended information for property: 'FixedDataRequirementCodeFilter'</summary>
		public Element _FixedDataRequirementCodeFilter { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedDataRequirementDateFilter { get; set; }
		///<summary>May contain extended information for property: 'FixedDataRequirementDateFilter'</summary>
		public Element _FixedDataRequirementDateFilter { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedDataRequirementSort { get; set; }
		///<summary>May contain extended information for property: 'FixedDataRequirementSort'</summary>
		public Element _FixedDataRequirementSort { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedDate { get; set; }
		///<summary>May contain extended information for property: 'FixedDate'</summary>
		public Element _FixedDate { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedDateTime { get; set; }
		///<summary>May contain extended information for property: 'FixedDateTime'</summary>
		public Element _FixedDateTime { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public decimal FixedDecimal { get; set; }
		///<summary>May contain extended information for property: 'FixedDecimal'</summary>
		public Element _FixedDecimal { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Quantity FixedDistance { get; set; }
		///<summary>May contain extended information for property: 'FixedDistance'</summary>
		public Element _FixedDistance { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public BackboneElement FixedDosage { get; set; }
		///<summary>May contain extended information for property: 'FixedDosage'</summary>
		public Element _FixedDosage { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedDosageDoseAndRate { get; set; }
		///<summary>May contain extended information for property: 'FixedDosageDoseAndRate'</summary>
		public Element _FixedDosageDoseAndRate { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Quantity FixedDuration { get; set; }
		///<summary>May contain extended information for property: 'FixedDuration'</summary>
		public Element _FixedDuration { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public BackboneElement FixedElementDefinition { get; set; }
		///<summary>May contain extended information for property: 'FixedElementDefinition'</summary>
		public Element _FixedElementDefinition { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedElementDefinitionBase { get; set; }
		///<summary>May contain extended information for property: 'FixedElementDefinitionBase'</summary>
		public Element _FixedElementDefinitionBase { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedElementDefinitionSlicing { get; set; }
		///<summary>May contain extended information for property: 'FixedElementDefinitionSlicing'</summary>
		public Element _FixedElementDefinitionSlicing { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>May contain extended information for property: 'FixedElementDefinitionSlicingDiscriminator'</summary>
		public Element _FixedElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public Element FixedElementDefinitionType { get; set; }
		///<summary>May contain extended information for property: 'FixedElementDefinitionType'</summary>
		public Element _FixedElementDefinitionType { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedId { get; set; }
		///<summary>May contain extended information for property: 'FixedId'</summary>
		public Element _FixedId { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedInstant { get; set; }
		///<summary>May contain extended information for property: 'FixedInstant'</summary>
		public Element _FixedInstant { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public decimal FixedInteger { get; set; }
		///<summary>May contain extended information for property: 'FixedInteger'</summary>
		public Element _FixedInteger { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedMarkdown { get; set; }
		///<summary>May contain extended information for property: 'FixedMarkdown'</summary>
		public Element _FixedMarkdown { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedOid { get; set; }
		///<summary>May contain extended information for property: 'FixedOid'</summary>
		public Element _FixedOid { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public decimal FixedPositiveInt { get; set; }
		///<summary>May contain extended information for property: 'FixedPositiveInt'</summary>
		public Element _FixedPositiveInt { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedString { get; set; }
		///<summary>May contain extended information for property: 'FixedString'</summary>
		public Element _FixedString { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedTime { get; set; }
		///<summary>May contain extended information for property: 'FixedTime'</summary>
		public Element _FixedTime { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public decimal FixedUnsignedInt { get; set; }
		///<summary>May contain extended information for property: 'FixedUnsignedInt'</summary>
		public Element _FixedUnsignedInt { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedUri { get; set; }
		///<summary>May contain extended information for property: 'FixedUri'</summary>
		public Element _FixedUri { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedUrl { get; set; }
		///<summary>May contain extended information for property: 'FixedUrl'</summary>
		public Element _FixedUrl { get; set; }
		///<summary>Specifies a value that SHALL be exactly the value  for this element in the instance. For purposes of comparison, non-significant whitespace is ignored, and all values must be an exact match (case and accent sensitive). Missing elements/attributes must also be missing.</summary>
		public string FixedUuid { get; set; }
		///<summary>May contain extended information for property: 'FixedUuid'</summary>
		public Element _FixedUuid { get; set; }
		///<summary>If true, the value of this element affects the interpretation of the element or resource that contains it, and the value of the element cannot be ignored. Typically, this is used for status, negation and qualification codes. The effect of this is that the element cannot be ignored by systems: they SHALL either recognize the element and process it, and/or a pre-determination has been made that it is not relevant to their particular system.</summary>
		public bool IsModifier { get; set; }
		///<summary>May contain extended information for property: 'IsModifier'</summary>
		public Element _IsModifier { get; set; }
		///<summary>Explains how that element affects the interpretation of the resource or element that contains it</summary>
		public string IsModifierReason { get; set; }
		///<summary>May contain extended information for property: 'IsModifierReason'</summary>
		public Element _IsModifierReason { get; set; }
		///<summary>Whether the element should be included if a client requests a search with the parameter _summary=true</summary>
		public bool IsSummary { get; set; }
		///<summary>May contain extended information for property: 'IsSummary'</summary>
		public Element _IsSummary { get; set; }
		///<summary>A single preferred label which is the text to display beside the element indicating its meaning or to use to prompt for the element in a user display or form.</summary>
		public string Label { get; set; }
		///<summary>May contain extended information for property: 'Label'</summary>
		public Element _Label { get; set; }
		///<summary>Identifies a concept from an external specification that roughly corresponds to this element</summary>
		public ElementDefinitionMapping[] Mapping { get; set; }
		///<summary>May contain extended information for property: 'Mapping'</summary>
		public Element[] _Mapping { get; set; }
		///<summary>The maximum number of times this element is permitted to appear in the instance</summary>
		public string Max { get; set; }
		///<summary>May contain extended information for property: 'Max'</summary>
		public Element _Max { get; set; }
		///<summary>Indicates the maximum length in characters that is permitted to be present in conformant instances and which is expected to be supported by conformant consumers that support the element</summary>
		public decimal MaxLength { get; set; }
		///<summary>May contain extended information for property: 'MaxLength'</summary>
		public Element _MaxLength { get; set; }
		///<summary>The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public string MaxValueDate { get; set; }
		///<summary>May contain extended information for property: 'MaxValueDate'</summary>
		public Element _MaxValueDate { get; set; }
		///<summary>The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public string MaxValueDateTime { get; set; }
		///<summary>May contain extended information for property: 'MaxValueDateTime'</summary>
		public Element _MaxValueDateTime { get; set; }
		///<summary>The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public decimal MaxValueDecimal { get; set; }
		///<summary>May contain extended information for property: 'MaxValueDecimal'</summary>
		public Element _MaxValueDecimal { get; set; }
		///<summary>The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public string MaxValueInstant { get; set; }
		///<summary>May contain extended information for property: 'MaxValueInstant'</summary>
		public Element _MaxValueInstant { get; set; }
		///<summary>The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public decimal MaxValueInteger { get; set; }
		///<summary>May contain extended information for property: 'MaxValueInteger'</summary>
		public Element _MaxValueInteger { get; set; }
		///<summary>The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public decimal MaxValuePositiveInt { get; set; }
		///<summary>May contain extended information for property: 'MaxValuePositiveInt'</summary>
		public Element _MaxValuePositiveInt { get; set; }
		///<summary>The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public Quantity MaxValueQuantity { get; set; }
		///<summary>May contain extended information for property: 'MaxValueQuantity'</summary>
		public Element _MaxValueQuantity { get; set; }
		///<summary>The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public string MaxValueTime { get; set; }
		///<summary>May contain extended information for property: 'MaxValueTime'</summary>
		public Element _MaxValueTime { get; set; }
		///<summary>The maximum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public decimal MaxValueUnsignedInt { get; set; }
		///<summary>May contain extended information for property: 'MaxValueUnsignedInt'</summary>
		public Element _MaxValueUnsignedInt { get; set; }
		///<summary>The Implicit meaning that is to be understood when this element is missing (e.g. 'when this element is missing, the period is ongoing')</summary>
		public string MeaningWhenMissing { get; set; }
		///<summary>May contain extended information for property: 'MeaningWhenMissing'</summary>
		public Element _MeaningWhenMissing { get; set; }
		///<summary>The minimum number of times this element SHALL appear in the instance</summary>
		public decimal Min { get; set; }
		///<summary>May contain extended information for property: 'Min'</summary>
		public Element _Min { get; set; }
		///<summary>The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public string MinValueDate { get; set; }
		///<summary>May contain extended information for property: 'MinValueDate'</summary>
		public Element _MinValueDate { get; set; }
		///<summary>The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public string MinValueDateTime { get; set; }
		///<summary>May contain extended information for property: 'MinValueDateTime'</summary>
		public Element _MinValueDateTime { get; set; }
		///<summary>The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public decimal MinValueDecimal { get; set; }
		///<summary>May contain extended information for property: 'MinValueDecimal'</summary>
		public Element _MinValueDecimal { get; set; }
		///<summary>The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public string MinValueInstant { get; set; }
		///<summary>May contain extended information for property: 'MinValueInstant'</summary>
		public Element _MinValueInstant { get; set; }
		///<summary>The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public decimal MinValueInteger { get; set; }
		///<summary>May contain extended information for property: 'MinValueInteger'</summary>
		public Element _MinValueInteger { get; set; }
		///<summary>The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public decimal MinValuePositiveInt { get; set; }
		///<summary>May contain extended information for property: 'MinValuePositiveInt'</summary>
		public Element _MinValuePositiveInt { get; set; }
		///<summary>The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public Quantity MinValueQuantity { get; set; }
		///<summary>May contain extended information for property: 'MinValueQuantity'</summary>
		public Element _MinValueQuantity { get; set; }
		///<summary>The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public string MinValueTime { get; set; }
		///<summary>May contain extended information for property: 'MinValueTime'</summary>
		public Element _MinValueTime { get; set; }
		///<summary>The minimum allowed value for the element. The value is inclusive. This is allowed for the types date, dateTime, instant, time, decimal, integer, and Quantity</summary>
		public decimal MinValueUnsignedInt { get; set; }
		///<summary>May contain extended information for property: 'MinValueUnsignedInt'</summary>
		public Element _MinValueUnsignedInt { get; set; }
		///<summary>If true, implementations that produce or consume resources SHALL provide "support" for the element in some meaningful way.  If false, the element may be ignored and not supported. If false, whether to populate or use the data element in any way is at the discretion of the implementation</summary>
		public bool MustSupport { get; set; }
		///<summary>May contain extended information for property: 'MustSupport'</summary>
		public Element _MustSupport { get; set; }
		///<summary>If present, indicates that the order of the repeating element has meaning and describes what that meaning is.  If absent, it means that the order of the element has no meaning</summary>
		public string OrderMeaning { get; set; }
		///<summary>May contain extended information for property: 'OrderMeaning'</summary>
		public Element _OrderMeaning { get; set; }
		///<summary>The path identifies the element and is expressed as a "."-separated list of ancestor elements, beginning with the name of the resource or extension</summary>
		public string Path { get; set; }
		///<summary>May contain extended information for property: 'Path'</summary>
		public Element _Path { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Structure PatternActionDefinition { get; set; }
		///<summary>May contain extended information for property: 'PatternActionDefinition'</summary>
		public Element _PatternActionDefinition { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternActionDefinitionBehavior { get; set; }
		///<summary>May contain extended information for property: 'PatternActionDefinitionBehavior'</summary>
		public Element _PatternActionDefinitionBehavior { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternActionDefinitionCustomization { get; set; }
		///<summary>May contain extended information for property: 'PatternActionDefinitionCustomization'</summary>
		public Element _PatternActionDefinitionCustomization { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternActionDefinitionRelatedAction { get; set; }
		///<summary>May contain extended information for property: 'PatternActionDefinitionRelatedAction'</summary>
		public Element _PatternActionDefinitionRelatedAction { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Address PatternAddress { get; set; }
		///<summary>May contain extended information for property: 'PatternAddress'</summary>
		public Element _PatternAddress { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Quantity PatternAge { get; set; }
		///<summary>May contain extended information for property: 'PatternAge'</summary>
		public Element _PatternAge { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Annotation PatternAnnotation { get; set; }
		///<summary>May contain extended information for property: 'PatternAnnotation'</summary>
		public Element _PatternAnnotation { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Attachment PatternAttachment { get; set; }
		///<summary>May contain extended information for property: 'PatternAttachment'</summary>
		public Element _PatternAttachment { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternBackboneElement { get; set; }
		///<summary>May contain extended information for property: 'PatternBackboneElement'</summary>
		public Element _PatternBackboneElement { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternBase64Binary { get; set; }
		///<summary>May contain extended information for property: 'PatternBase64Binary'</summary>
		public Element _PatternBase64Binary { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public bool PatternBoolean { get; set; }
		///<summary>May contain extended information for property: 'PatternBoolean'</summary>
		public Element _PatternBoolean { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternCanonical { get; set; }
		///<summary>May contain extended information for property: 'PatternCanonical'</summary>
		public Element _PatternCanonical { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternCode { get; set; }
		///<summary>May contain extended information for property: 'PatternCode'</summary>
		public Element _PatternCode { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public CodeableConcept PatternCodeableConcept { get; set; }
		///<summary>May contain extended information for property: 'PatternCodeableConcept'</summary>
		public Element _PatternCodeableConcept { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Coding PatternCoding { get; set; }
		///<summary>May contain extended information for property: 'PatternCoding'</summary>
		public Element _PatternCoding { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public ContactDetail PatternContactDetail { get; set; }
		///<summary>May contain extended information for property: 'PatternContactDetail'</summary>
		public Element _PatternContactDetail { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public ContactPoint PatternContactPoint { get; set; }
		///<summary>May contain extended information for property: 'PatternContactPoint'</summary>
		public Element _PatternContactPoint { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Contributor PatternContributor { get; set; }
		///<summary>May contain extended information for property: 'PatternContributor'</summary>
		public Element _PatternContributor { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Quantity PatternCount { get; set; }
		///<summary>May contain extended information for property: 'PatternCount'</summary>
		public Element _PatternCount { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public DataRequirement PatternDataRequirement { get; set; }
		///<summary>May contain extended information for property: 'PatternDataRequirement'</summary>
		public Element _PatternDataRequirement { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternDataRequirementCodeFilter { get; set; }
		///<summary>May contain extended information for property: 'PatternDataRequirementCodeFilter'</summary>
		public Element _PatternDataRequirementCodeFilter { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternDataRequirementDateFilter { get; set; }
		///<summary>May contain extended information for property: 'PatternDataRequirementDateFilter'</summary>
		public Element _PatternDataRequirementDateFilter { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternDataRequirementSort { get; set; }
		///<summary>May contain extended information for property: 'PatternDataRequirementSort'</summary>
		public Element _PatternDataRequirementSort { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternDate { get; set; }
		///<summary>May contain extended information for property: 'PatternDate'</summary>
		public Element _PatternDate { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternDateTime { get; set; }
		///<summary>May contain extended information for property: 'PatternDateTime'</summary>
		public Element _PatternDateTime { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public decimal PatternDecimal { get; set; }
		///<summary>May contain extended information for property: 'PatternDecimal'</summary>
		public Element _PatternDecimal { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Quantity PatternDistance { get; set; }
		///<summary>May contain extended information for property: 'PatternDistance'</summary>
		public Element _PatternDistance { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public BackboneElement PatternDosage { get; set; }
		///<summary>May contain extended information for property: 'PatternDosage'</summary>
		public Element _PatternDosage { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternDosageDoseAndRate { get; set; }
		///<summary>May contain extended information for property: 'PatternDosageDoseAndRate'</summary>
		public Element _PatternDosageDoseAndRate { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Quantity PatternDuration { get; set; }
		///<summary>May contain extended information for property: 'PatternDuration'</summary>
		public Element _PatternDuration { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public BackboneElement PatternElementDefinition { get; set; }
		///<summary>May contain extended information for property: 'PatternElementDefinition'</summary>
		public Element _PatternElementDefinition { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternElementDefinitionBase { get; set; }
		///<summary>May contain extended information for property: 'PatternElementDefinitionBase'</summary>
		public Element _PatternElementDefinitionBase { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternElementDefinitionSlicing { get; set; }
		///<summary>May contain extended information for property: 'PatternElementDefinitionSlicing'</summary>
		public Element _PatternElementDefinitionSlicing { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>May contain extended information for property: 'PatternElementDefinitionSlicingDiscriminator'</summary>
		public Element _PatternElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public Element PatternElementDefinitionType { get; set; }
		///<summary>May contain extended information for property: 'PatternElementDefinitionType'</summary>
		public Element _PatternElementDefinitionType { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternId { get; set; }
		///<summary>May contain extended information for property: 'PatternId'</summary>
		public Element _PatternId { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternInstant { get; set; }
		///<summary>May contain extended information for property: 'PatternInstant'</summary>
		public Element _PatternInstant { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public decimal PatternInteger { get; set; }
		///<summary>May contain extended information for property: 'PatternInteger'</summary>
		public Element _PatternInteger { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternMarkdown { get; set; }
		///<summary>May contain extended information for property: 'PatternMarkdown'</summary>
		public Element _PatternMarkdown { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternOid { get; set; }
		///<summary>May contain extended information for property: 'PatternOid'</summary>
		public Element _PatternOid { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public decimal PatternPositiveInt { get; set; }
		///<summary>May contain extended information for property: 'PatternPositiveInt'</summary>
		public Element _PatternPositiveInt { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternString { get; set; }
		///<summary>May contain extended information for property: 'PatternString'</summary>
		public Element _PatternString { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternTime { get; set; }
		///<summary>May contain extended information for property: 'PatternTime'</summary>
		public Element _PatternTime { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public decimal PatternUnsignedInt { get; set; }
		///<summary>May contain extended information for property: 'PatternUnsignedInt'</summary>
		public Element _PatternUnsignedInt { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternUri { get; set; }
		///<summary>May contain extended information for property: 'PatternUri'</summary>
		public Element _PatternUri { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternUrl { get; set; }
		///<summary>May contain extended information for property: 'PatternUrl'</summary>
		public Element _PatternUrl { get; set; }
		///<summary>Specifies a value that the value in the instance SHALL follow - that is, any value in the pattern must be found in the instance. Other additional values may be found too. This is effectively constraint by example.  
		/// 
		/// When pattern[x] is used to constrain a primitive, it means that the value provided in the pattern[x] must match the instance value exactly.
		/// 
		/// When pattern[x] is used to constrain an array, it means that each element provided in the pattern[x] array must (recursively) match at least one element from the instance array.
		/// 
		/// When pattern[x] is used to constrain a complex object, it means that each property in the pattern must be present in the complex object, and its value must recursively match -- i.e.,
		/// 
		/// 1. If primitive: it must match exactly the pattern value
		/// 2. If a complex object: it must match (recursively) the pattern value
		/// 3. If an array: it must match (recursively) the pattern value</summary>
		public string PatternUuid { get; set; }
		///<summary>May contain extended information for property: 'PatternUuid'</summary>
		public Element _PatternUuid { get; set; }
		///<summary>Codes that define how this element is represented in instances, when the deviation varies from the normal case</summary>
		public string[] Representation { get; set; }
		///<summary>May contain extended information for property: 'Representation'</summary>
		public Element[] _Representation { get; set; }
		///<summary>This element is for traceability of why the element was created and why the constraints exist as they do. This may be used to point to source materials or specifications that drove the structure of this element.</summary>
		public string Requirements { get; set; }
		///<summary>May contain extended information for property: 'Requirements'</summary>
		public Element _Requirements { get; set; }
		///<summary>A concise description of what this element means (e.g. for use in autogenerated summaries)</summary>
		public string Short { get; set; }
		///<summary>May contain extended information for property: 'Short'</summary>
		public Element _Short { get; set; }
		///<summary>If true, indicates that this slice definition is constraining a slice definition with the same name in an inherited profile. If false, the slice is not overriding any slice in an inherited profile. If missing, the slice might or might not be overriding a slice in an inherited profile, depending on the sliceName</summary>
		public bool SliceIsConstraining { get; set; }
		///<summary>May contain extended information for property: 'SliceIsConstraining'</summary>
		public Element _SliceIsConstraining { get; set; }
		///<summary>The name of this element definition slice, when slicing is working. The name must be a token with no dots or spaces. This is a unique name referring to a specific set of constraints applied to this element, used to provide a name to different slices of the same element</summary>
		public string SliceName { get; set; }
		///<summary>May contain extended information for property: 'SliceName'</summary>
		public Element _SliceName { get; set; }
		///<summary>Indicates that the element is sliced into a set of alternative definitions (i.e. in a structure definition, there are multiple different constraints on a single element in the base resource). Slicing can be used in any resource that has cardinality ..* on the base resource, or any resource with a choice of types. The set of slices is any elements that come after this in the element sequence that have the same path, until a shorter path occurs (the shorter path terminates the set)</summary>
		public ElementDefinitionSlicing Slicing { get; set; }
		///<summary>May contain extended information for property: 'Slicing'</summary>
		public Element _Slicing { get; set; }
		///<summary>The data type or resource that the value of this element is permitted to be</summary>
		public ElementDefinitionType[] Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element[] _Type { get; set; }
	}
	///<summary>
	///Information about the base definition of the element, provided to make it unnecessary for tools to trace the deviation of the element through the derived and related profiles. When the element definition is not the original definition of an element - i.g. either in a constraint on another type, or for elements from a super type in a snap shot - then the information in provided in the element definition may be different to the base definition. On the original definition of the element, it will be same
	///</summary>
	///<source-file>elementdefinition.xml</source-file>
	public class ElementDefinitionBase : Element
	{
		///<summary>Maximum cardinality of the base element identified by the path</summary>
		public string Max { get; set; }
		///<summary>May contain extended information for property: 'Max'</summary>
		public Element _Max { get; set; }
		///<summary>Minimum cardinality of the base element identified by the path</summary>
		public decimal Min { get; set; }
		///<summary>May contain extended information for property: 'Min'</summary>
		public Element _Min { get; set; }
		///<summary>The Path that identifies the base element - this matches the ElementDefinition.path for that element. Across FHIR, there is only one base definition of any element - that is, an element definition on a [[[StructureDefinition]]] without a StructureDefinition.base</summary>
		public string Path { get; set; }
		///<summary>May contain extended information for property: 'Path'</summary>
		public Element _Path { get; set; }
	}
	///<summary>
	///Binds to a value set if this element is coded (code, Coding, CodeableConcept, Quantity), or the data types (string, uri)
	///</summary>
	///<source-file>elementdefinition.xml</source-file>
	public class ElementDefinitionBinding : Element
	{
		///<summary>Describes the intended use of this particular set of codes</summary>
		public string Description { get; set; }
		///<summary>May contain extended information for property: 'Description'</summary>
		public Element _Description { get; set; }
		///<summary>Indicates the degree of conformance expectations associated with this binding - that is, the degree to which the provided value set must be adhered to in the instances</summary>
		public string Strength { get; set; }
		///<summary>May contain extended information for property: 'Strength'</summary>
		public Element _Strength { get; set; }
		///<summary>Refers to the value set that identifies the set of codes the binding refers to</summary>
		public string ValueSet { get; set; }
		///<summary>May contain extended information for property: 'ValueSet'</summary>
		public Element _ValueSet { get; set; }
	}
	///<summary>
	///Formal constraints such as co-occurrence and other constraints that can be computationally evaluated within the context of the instance
	///</summary>
	///<source-file>elementdefinition.xml</source-file>
	public class ElementDefinitionConstraint : Element
	{
		///<summary>A [FHIRPath](fhirpath.html) expression of constraint that can be executed to see if this constraint is met</summary>
		public string Expression { get; set; }
		///<summary>May contain extended information for property: 'Expression'</summary>
		public Element _Expression { get; set; }
		///<summary>Text that can be used to describe the constraint in messages identifying that the constraint has been violated </summary>
		public string Human { get; set; }
		///<summary>May contain extended information for property: 'Human'</summary>
		public Element _Human { get; set; }
		///<summary>Allows identification of which elements have their cardinalities impacted by the constraint.  Will not be referenced for constraints that do not affect cardinality</summary>
		public string Key { get; set; }
		///<summary>May contain extended information for property: 'Key'</summary>
		public Element _Key { get; set; }
		///<summary>Description of why this constraint is necessary or appropriate</summary>
		public string Requirements { get; set; }
		///<summary>May contain extended information for property: 'Requirements'</summary>
		public Element _Requirements { get; set; }
		///<summary>Identifies the impact constraint violation has on the conformance of the instance</summary>
		public string Severity { get; set; }
		///<summary>May contain extended information for property: 'Severity'</summary>
		public Element _Severity { get; set; }
		///<summary>A reference to the original source of the constraint, for traceability purposes</summary>
		public string Source { get; set; }
		///<summary>May contain extended information for property: 'Source'</summary>
		public Element _Source { get; set; }
		///<summary>An XPath expression of constraint that can be executed to see if this constraint is met</summary>
		public string Xpath { get; set; }
		///<summary>May contain extended information for property: 'Xpath'</summary>
		public Element _Xpath { get; set; }
	}
	///<summary>
	///A sample value for this element demonstrating the type of information that would typically be found in the element
	///</summary>
	///<source-file>elementdefinition.xml</source-file>
	public class ElementDefinitionExample : Element
	{
		///<summary>Describes the purpose of this example amoung the set of examples</summary>
		public string Label { get; set; }
		///<summary>May contain extended information for property: 'Label'</summary>
		public Element _Label { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Structure ValueActionDefinition { get; set; }
		///<summary>May contain extended information for property: 'ValueActionDefinition'</summary>
		public Element _ValueActionDefinition { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueActionDefinitionBehavior { get; set; }
		///<summary>May contain extended information for property: 'ValueActionDefinitionBehavior'</summary>
		public Element _ValueActionDefinitionBehavior { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueActionDefinitionCustomization { get; set; }
		///<summary>May contain extended information for property: 'ValueActionDefinitionCustomization'</summary>
		public Element _ValueActionDefinitionCustomization { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueActionDefinitionRelatedAction { get; set; }
		///<summary>May contain extended information for property: 'ValueActionDefinitionRelatedAction'</summary>
		public Element _ValueActionDefinitionRelatedAction { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Address ValueAddress { get; set; }
		///<summary>May contain extended information for property: 'ValueAddress'</summary>
		public Element _ValueAddress { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Quantity ValueAge { get; set; }
		///<summary>May contain extended information for property: 'ValueAge'</summary>
		public Element _ValueAge { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Annotation ValueAnnotation { get; set; }
		///<summary>May contain extended information for property: 'ValueAnnotation'</summary>
		public Element _ValueAnnotation { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Attachment ValueAttachment { get; set; }
		///<summary>May contain extended information for property: 'ValueAttachment'</summary>
		public Element _ValueAttachment { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueBackboneElement { get; set; }
		///<summary>May contain extended information for property: 'ValueBackboneElement'</summary>
		public Element _ValueBackboneElement { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueBase64Binary { get; set; }
		///<summary>May contain extended information for property: 'ValueBase64Binary'</summary>
		public Element _ValueBase64Binary { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public bool ValueBoolean { get; set; }
		///<summary>May contain extended information for property: 'ValueBoolean'</summary>
		public Element _ValueBoolean { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueCanonical { get; set; }
		///<summary>May contain extended information for property: 'ValueCanonical'</summary>
		public Element _ValueCanonical { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueCode { get; set; }
		///<summary>May contain extended information for property: 'ValueCode'</summary>
		public Element _ValueCode { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public CodeableConcept ValueCodeableConcept { get; set; }
		///<summary>May contain extended information for property: 'ValueCodeableConcept'</summary>
		public Element _ValueCodeableConcept { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Coding ValueCoding { get; set; }
		///<summary>May contain extended information for property: 'ValueCoding'</summary>
		public Element _ValueCoding { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public ContactDetail ValueContactDetail { get; set; }
		///<summary>May contain extended information for property: 'ValueContactDetail'</summary>
		public Element _ValueContactDetail { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public ContactPoint ValueContactPoint { get; set; }
		///<summary>May contain extended information for property: 'ValueContactPoint'</summary>
		public Element _ValueContactPoint { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Contributor ValueContributor { get; set; }
		///<summary>May contain extended information for property: 'ValueContributor'</summary>
		public Element _ValueContributor { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Quantity ValueCount { get; set; }
		///<summary>May contain extended information for property: 'ValueCount'</summary>
		public Element _ValueCount { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public DataRequirement ValueDataRequirement { get; set; }
		///<summary>May contain extended information for property: 'ValueDataRequirement'</summary>
		public Element _ValueDataRequirement { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueDataRequirementCodeFilter { get; set; }
		///<summary>May contain extended information for property: 'ValueDataRequirementCodeFilter'</summary>
		public Element _ValueDataRequirementCodeFilter { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueDataRequirementDateFilter { get; set; }
		///<summary>May contain extended information for property: 'ValueDataRequirementDateFilter'</summary>
		public Element _ValueDataRequirementDateFilter { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueDataRequirementSort { get; set; }
		///<summary>May contain extended information for property: 'ValueDataRequirementSort'</summary>
		public Element _ValueDataRequirementSort { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueDate { get; set; }
		///<summary>May contain extended information for property: 'ValueDate'</summary>
		public Element _ValueDate { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueDateTime { get; set; }
		///<summary>May contain extended information for property: 'ValueDateTime'</summary>
		public Element _ValueDateTime { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public decimal ValueDecimal { get; set; }
		///<summary>May contain extended information for property: 'ValueDecimal'</summary>
		public Element _ValueDecimal { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Quantity ValueDistance { get; set; }
		///<summary>May contain extended information for property: 'ValueDistance'</summary>
		public Element _ValueDistance { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public BackboneElement ValueDosage { get; set; }
		///<summary>May contain extended information for property: 'ValueDosage'</summary>
		public Element _ValueDosage { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueDosageDoseAndRate { get; set; }
		///<summary>May contain extended information for property: 'ValueDosageDoseAndRate'</summary>
		public Element _ValueDosageDoseAndRate { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Quantity ValueDuration { get; set; }
		///<summary>May contain extended information for property: 'ValueDuration'</summary>
		public Element _ValueDuration { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public BackboneElement ValueElementDefinition { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinition'</summary>
		public Element _ValueElementDefinition { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueElementDefinitionBase { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionBase'</summary>
		public Element _ValueElementDefinitionBase { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueElementDefinitionExample { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionExample'</summary>
		public Element _ValueElementDefinitionExample { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueElementDefinitionSlicing { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionSlicing'</summary>
		public Element _ValueElementDefinitionSlicing { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionSlicingDiscriminator'</summary>
		public Element _ValueElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public Element ValueElementDefinitionType { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionType'</summary>
		public Element _ValueElementDefinitionType { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueId { get; set; }
		///<summary>May contain extended information for property: 'ValueId'</summary>
		public Element _ValueId { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueInstant { get; set; }
		///<summary>May contain extended information for property: 'ValueInstant'</summary>
		public Element _ValueInstant { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public decimal ValueInteger { get; set; }
		///<summary>May contain extended information for property: 'ValueInteger'</summary>
		public Element _ValueInteger { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueMarkdown { get; set; }
		///<summary>May contain extended information for property: 'ValueMarkdown'</summary>
		public Element _ValueMarkdown { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueOid { get; set; }
		///<summary>May contain extended information for property: 'ValueOid'</summary>
		public Element _ValueOid { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public decimal ValuePositiveInt { get; set; }
		///<summary>May contain extended information for property: 'ValuePositiveInt'</summary>
		public Element _ValuePositiveInt { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueString { get; set; }
		///<summary>May contain extended information for property: 'ValueString'</summary>
		public Element _ValueString { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueTime { get; set; }
		///<summary>May contain extended information for property: 'ValueTime'</summary>
		public Element _ValueTime { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public decimal ValueUnsignedInt { get; set; }
		///<summary>May contain extended information for property: 'ValueUnsignedInt'</summary>
		public Element _ValueUnsignedInt { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueUri { get; set; }
		///<summary>May contain extended information for property: 'ValueUri'</summary>
		public Element _ValueUri { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueUrl { get; set; }
		///<summary>May contain extended information for property: 'ValueUrl'</summary>
		public Element _ValueUrl { get; set; }
		///<summary>The actual value for the element, which must be one of the types allowed for this element</summary>
		public string ValueUuid { get; set; }
		///<summary>May contain extended information for property: 'ValueUuid'</summary>
		public Element _ValueUuid { get; set; }
	}
	///<summary>
	///Identifies a concept from an external specification that roughly corresponds to this element
	///</summary>
	///<source-file>elementdefinition.xml</source-file>
	public class ElementDefinitionMapping : Element
	{
		///<summary>Comments that provide information about the mapping or its use</summary>
		public string Comment { get; set; }
		///<summary>May contain extended information for property: 'Comment'</summary>
		public Element _Comment { get; set; }
		///<summary>An internal reference to the definition of a mapping</summary>
		public string Identity { get; set; }
		///<summary>May contain extended information for property: 'Identity'</summary>
		public Element _Identity { get; set; }
		///<summary>Identifies the computable language in which mapping.map is expressed.</summary>
		public string Language { get; set; }
		///<summary>May contain extended information for property: 'Language'</summary>
		public Element _Language { get; set; }
		///<summary>Expresses what part of the target specification corresponds to this element</summary>
		public string Map { get; set; }
		///<summary>May contain extended information for property: 'Map'</summary>
		public Element _Map { get; set; }
	}
	///<summary>
	///Indicates that the element is sliced into a set of alternative definitions (i.e. in a structure definition, there are multiple different constraints on a single element in the base resource). Slicing can be used in any resource that has cardinality ..* on the base resource, or any resource with a choice of types. The set of slices is any elements that come after this in the element sequence that have the same path, until a shorter path occurs (the shorter path terminates the set)
	///</summary>
	///<source-file>elementdefinition.xml</source-file>
	public class ElementDefinitionSlicing : Element
	{
		///<summary>A human-readable text description of how the slicing works. If there is no discriminator, this is required to be present to provide whatever information is possible about how the slices can be differentiated</summary>
		public string Description { get; set; }
		///<summary>May contain extended information for property: 'Description'</summary>
		public Element _Description { get; set; }
		///<summary>Designates which child elements are used to discriminate between the slices when processing an instance. If one or more discriminators are provided, the value of the child elements in the instance data SHALL completely distinguish which slice the element in the resource matches based on the allowed values for those elements in each of the slices</summary>
		public ElementDefinitionSlicingDiscriminator[] Discriminator { get; set; }
		///<summary>May contain extended information for property: 'Discriminator'</summary>
		public Element[] _Discriminator { get; set; }
		///<summary>If the matching elements have to occur in the same order as defined in the profile</summary>
		public bool Ordered { get; set; }
		///<summary>May contain extended information for property: 'Ordered'</summary>
		public Element _Ordered { get; set; }
		///<summary>Whether additional slices are allowed or not. When the slices are ordered, profile authors can also say that additional slices are only allowed at the end</summary>
		public string Rules { get; set; }
		///<summary>May contain extended information for property: 'Rules'</summary>
		public Element _Rules { get; set; }
	}
	///<summary>
	///Designates which child elements are used to discriminate between the slices when processing an instance. If one or more discriminators are provided, the value of the child elements in the instance data SHALL completely distinguish which slice the element in the resource matches based on the allowed values for those elements in each of the slices
	///</summary>
	///<source-file>elementdefinition.xml</source-file>
	public class ElementDefinitionSlicingDiscriminator : Element
	{
		///<summary>A FHIRPath expression, using [the simple subset of FHIRPath](fhirpath.html#simple), that is used to identify the element on which discrimination is based</summary>
		public string Path { get; set; }
		///<summary>May contain extended information for property: 'Path'</summary>
		public Element _Path { get; set; }
		///<summary>How the element value is interpreted when discrimination is evaluated</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
	}
	///<summary>
	///The data type or resource that the value of this element is permitted to be
	///</summary>
	///<source-file>elementdefinition.xml</source-file>
	public class ElementDefinitionType : Element
	{
		///<summary>If the type is a reference to another resource, how the resource is or can be aggregated - is it a contained resource, or a reference, and if the context is a bundle, is it included in the bundle</summary>
		public string[] Aggregation { get; set; }
		///<summary>May contain extended information for property: 'Aggregation'</summary>
		public Element[] _Aggregation { get; set; }
		///<summary>URL of Data type or Resource that is a(or the) type used for this element. References are URLs that are relative to http://hl7.org/fhir/StructureDefinition e.g. "string" is a reference to http://hl7.org/fhir/StructureDefinition/string. Absolute URLs are only allowed in logical models</summary>
		public string Code { get; set; }
		///<summary>May contain extended information for property: 'Code'</summary>
		public Element _Code { get; set; }
		///<summary>Identifies a profile structure or implementation Guide that applies to the datatype this element refers to. If any profiles are specified, then the content must conform to at least one of them. The URL can be a local reference - to a contained StructureDefinition, or a reference to another StructureDefinition or Implementation Guide by a canonical URL. When an implementation guide is specified, the type SHALL conform to at least one profile defined in the implementation guide</summary>
		public string[] Profile { get; set; }
		///<summary>May contain extended information for property: 'Profile'</summary>
		public Element[] _Profile { get; set; }
		///<summary>Used when the type is "Reference" or "canonical", and identifies a profile structure or implementation Guide that applies to the target of the reference this element refers to. If any profiles are specified, then the content must conform to at least one of them. The URL can be a local reference - to a contained StructureDefinition, or a reference to another StructureDefinition or Implementation Guide by a canonical URL. When an implementation guide is specified, the target resource SHALL conform to at least one profile defined in the implementation guide</summary>
		public string[] TargetProfile { get; set; }
		///<summary>May contain extended information for property: 'TargetProfile'</summary>
		public Element[] _TargetProfile { get; set; }
		///<summary>Whether this reference needs to be version specific or version independent, or whether either can be used</summary>
		public string Versioning { get; set; }
		///<summary>May contain extended information for property: 'Versioning'</summary>
		public Element _Versioning { get; set; }
	}
	///<summary>
	///A expression that is evaluated in a specified context and returns a value. The context of use of the expression must specify the context in which the expression is evaluated, and how the result of the expression is used
	///</summary>
	///<source-file>expression.xml</source-file>
	public class Expression : Element
	{
		///<summary>A brief, natural language description of the condition that effectively communicates the intended semantics </summary>
		public string Description { get; set; }
		///<summary>May contain extended information for property: 'Description'</summary>
		public Element _Description { get; set; }
		///<summary>An expression in the specified language that returns a value</summary>
		public string expression { get; set; }
		///<summary>May contain extended information for property: 'expression'</summary>
		public Element _expression { get; set; }
		///<summary>The media type of the language for the expression</summary>
		public string Language { get; set; }
		///<summary>May contain extended information for property: 'Language'</summary>
		public Element _Language { get; set; }
		///<summary>A short name assigned to the expression to allow for multiple reuse of the expression in the context where it is defined</summary>
		public string Name { get; set; }
		///<summary>May contain extended information for property: 'Name'</summary>
		public Element _Name { get; set; }
		///<summary>A URI that defines where the expression is found</summary>
		public string Reference { get; set; }
		///<summary>May contain extended information for property: 'Reference'</summary>
		public Element _Reference { get; set; }
	}
	///<summary>
	///Optional Extension Element - found in all resources
	///</summary>
	///<source-file>extension.xml</source-file>
	public class Extension : Element
	{
		///<summary>Source of the definition for the extension code - a logical name or a URL</summary>
		public string Url { get; set; }
		///<summary>May contain extended information for property: 'Url'</summary>
		public Element _Url { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Structure ValueActionDefinition { get; set; }
		///<summary>May contain extended information for property: 'ValueActionDefinition'</summary>
		public Element _ValueActionDefinition { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueActionDefinitionBehavior { get; set; }
		///<summary>May contain extended information for property: 'ValueActionDefinitionBehavior'</summary>
		public Element _ValueActionDefinitionBehavior { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueActionDefinitionCustomization { get; set; }
		///<summary>May contain extended information for property: 'ValueActionDefinitionCustomization'</summary>
		public Element _ValueActionDefinitionCustomization { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueActionDefinitionRelatedAction { get; set; }
		///<summary>May contain extended information for property: 'ValueActionDefinitionRelatedAction'</summary>
		public Element _ValueActionDefinitionRelatedAction { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Address ValueAddress { get; set; }
		///<summary>May contain extended information for property: 'ValueAddress'</summary>
		public Element _ValueAddress { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Quantity ValueAge { get; set; }
		///<summary>May contain extended information for property: 'ValueAge'</summary>
		public Element _ValueAge { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Annotation ValueAnnotation { get; set; }
		///<summary>May contain extended information for property: 'ValueAnnotation'</summary>
		public Element _ValueAnnotation { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Attachment ValueAttachment { get; set; }
		///<summary>May contain extended information for property: 'ValueAttachment'</summary>
		public Element _ValueAttachment { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueBackboneElement { get; set; }
		///<summary>May contain extended information for property: 'ValueBackboneElement'</summary>
		public Element _ValueBackboneElement { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueBase64Binary { get; set; }
		///<summary>May contain extended information for property: 'ValueBase64Binary'</summary>
		public Element _ValueBase64Binary { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public bool ValueBoolean { get; set; }
		///<summary>May contain extended information for property: 'ValueBoolean'</summary>
		public Element _ValueBoolean { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueCanonical { get; set; }
		///<summary>May contain extended information for property: 'ValueCanonical'</summary>
		public Element _ValueCanonical { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueCode { get; set; }
		///<summary>May contain extended information for property: 'ValueCode'</summary>
		public Element _ValueCode { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public CodeableConcept ValueCodeableConcept { get; set; }
		///<summary>May contain extended information for property: 'ValueCodeableConcept'</summary>
		public Element _ValueCodeableConcept { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Coding ValueCoding { get; set; }
		///<summary>May contain extended information for property: 'ValueCoding'</summary>
		public Element _ValueCoding { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public ContactDetail ValueContactDetail { get; set; }
		///<summary>May contain extended information for property: 'ValueContactDetail'</summary>
		public Element _ValueContactDetail { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public ContactPoint ValueContactPoint { get; set; }
		///<summary>May contain extended information for property: 'ValueContactPoint'</summary>
		public Element _ValueContactPoint { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Contributor ValueContributor { get; set; }
		///<summary>May contain extended information for property: 'ValueContributor'</summary>
		public Element _ValueContributor { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Quantity ValueCount { get; set; }
		///<summary>May contain extended information for property: 'ValueCount'</summary>
		public Element _ValueCount { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public DataRequirement ValueDataRequirement { get; set; }
		///<summary>May contain extended information for property: 'ValueDataRequirement'</summary>
		public Element _ValueDataRequirement { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueDataRequirementCodeFilter { get; set; }
		///<summary>May contain extended information for property: 'ValueDataRequirementCodeFilter'</summary>
		public Element _ValueDataRequirementCodeFilter { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueDataRequirementDateFilter { get; set; }
		///<summary>May contain extended information for property: 'ValueDataRequirementDateFilter'</summary>
		public Element _ValueDataRequirementDateFilter { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueDataRequirementSort { get; set; }
		///<summary>May contain extended information for property: 'ValueDataRequirementSort'</summary>
		public Element _ValueDataRequirementSort { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueDate { get; set; }
		///<summary>May contain extended information for property: 'ValueDate'</summary>
		public Element _ValueDate { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueDateTime { get; set; }
		///<summary>May contain extended information for property: 'ValueDateTime'</summary>
		public Element _ValueDateTime { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public decimal ValueDecimal { get; set; }
		///<summary>May contain extended information for property: 'ValueDecimal'</summary>
		public Element _ValueDecimal { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Quantity ValueDistance { get; set; }
		///<summary>May contain extended information for property: 'ValueDistance'</summary>
		public Element _ValueDistance { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public BackboneElement ValueDosage { get; set; }
		///<summary>May contain extended information for property: 'ValueDosage'</summary>
		public Element _ValueDosage { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueDosageDoseAndRate { get; set; }
		///<summary>May contain extended information for property: 'ValueDosageDoseAndRate'</summary>
		public Element _ValueDosageDoseAndRate { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Quantity ValueDuration { get; set; }
		///<summary>May contain extended information for property: 'ValueDuration'</summary>
		public Element _ValueDuration { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public BackboneElement ValueElementDefinition { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinition'</summary>
		public Element _ValueElementDefinition { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueElementDefinitionBase { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionBase'</summary>
		public Element _ValueElementDefinitionBase { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueElementDefinitionBinding { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionBinding'</summary>
		public Element _ValueElementDefinitionBinding { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueElementDefinitionConstraint { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionConstraint'</summary>
		public Element _ValueElementDefinitionConstraint { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueElementDefinitionExample { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionExample'</summary>
		public Element _ValueElementDefinitionExample { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueElementDefinitionMapping { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionMapping'</summary>
		public Element _ValueElementDefinitionMapping { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueElementDefinitionSlicing { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionSlicing'</summary>
		public Element _ValueElementDefinitionSlicing { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionSlicingDiscriminator'</summary>
		public Element _ValueElementDefinitionSlicingDiscriminator { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueElementDefinitionType { get; set; }
		///<summary>May contain extended information for property: 'ValueElementDefinitionType'</summary>
		public Element _ValueElementDefinitionType { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueExpression { get; set; }
		///<summary>May contain extended information for property: 'ValueExpression'</summary>
		public Element _ValueExpression { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Extension ValueExtension { get; set; }
		///<summary>May contain extended information for property: 'ValueExtension'</summary>
		public Element _ValueExtension { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public HumanName ValueHumanName { get; set; }
		///<summary>May contain extended information for property: 'ValueHumanName'</summary>
		public Element _ValueHumanName { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueId { get; set; }
		///<summary>May contain extended information for property: 'ValueId'</summary>
		public Element _ValueId { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Identifier ValueIdentifier { get; set; }
		///<summary>May contain extended information for property: 'ValueIdentifier'</summary>
		public Element _ValueIdentifier { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueInstant { get; set; }
		///<summary>May contain extended information for property: 'ValueInstant'</summary>
		public Element _ValueInstant { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public decimal ValueInteger { get; set; }
		///<summary>May contain extended information for property: 'ValueInteger'</summary>
		public Element _ValueInteger { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueMarkdown { get; set; }
		///<summary>May contain extended information for property: 'ValueMarkdown'</summary>
		public Element _ValueMarkdown { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public BackboneElement ValueMarketingStatus { get; set; }
		///<summary>May contain extended information for property: 'ValueMarketingStatus'</summary>
		public Element _ValueMarketingStatus { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueMeta { get; set; }
		///<summary>May contain extended information for property: 'ValueMeta'</summary>
		public Element _ValueMeta { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public ModuleMetadata ValueModuleMetadata { get; set; }
		///<summary>May contain extended information for property: 'ValueModuleMetadata'</summary>
		public Element _ValueModuleMetadata { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Money ValueMoney { get; set; }
		///<summary>May contain extended information for property: 'ValueMoney'</summary>
		public Element _ValueMoney { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Narrative ValueNarrative { get; set; }
		///<summary>May contain extended information for property: 'ValueNarrative'</summary>
		public Element _ValueNarrative { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueOid { get; set; }
		///<summary>May contain extended information for property: 'ValueOid'</summary>
		public Element _ValueOid { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public ParameterDefinition ValueParameterDefinition { get; set; }
		///<summary>May contain extended information for property: 'ValueParameterDefinition'</summary>
		public Element _ValueParameterDefinition { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Period ValuePeriod { get; set; }
		///<summary>May contain extended information for property: 'ValuePeriod'</summary>
		public Element _ValuePeriod { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public BackboneElement ValuePopulation { get; set; }
		///<summary>May contain extended information for property: 'ValuePopulation'</summary>
		public Element _ValuePopulation { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public decimal ValuePositiveInt { get; set; }
		///<summary>May contain extended information for property: 'ValuePositiveInt'</summary>
		public Element _ValuePositiveInt { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public BackboneElement ValueProdCharacteristic { get; set; }
		///<summary>May contain extended information for property: 'ValueProdCharacteristic'</summary>
		public Element _ValueProdCharacteristic { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public BackboneElement ValueProductShelfLife { get; set; }
		///<summary>May contain extended information for property: 'ValueProductShelfLife'</summary>
		public Element _ValueProductShelfLife { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Quantity ValueQuantity { get; set; }
		///<summary>May contain extended information for property: 'ValueQuantity'</summary>
		public Element _ValueQuantity { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Range ValueRange { get; set; }
		///<summary>May contain extended information for property: 'ValueRange'</summary>
		public Element _ValueRange { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Ratio ValueRatio { get; set; }
		///<summary>May contain extended information for property: 'ValueRatio'</summary>
		public Element _ValueRatio { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Reference ValueReference { get; set; }
		///<summary>May contain extended information for property: 'ValueReference'</summary>
		public Element _ValueReference { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public RelatedArtifact ValueRelatedArtifact { get; set; }
		///<summary>May contain extended information for property: 'ValueRelatedArtifact'</summary>
		public Element _ValueRelatedArtifact { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public SampledData ValueSampledData { get; set; }
		///<summary>May contain extended information for property: 'ValueSampledData'</summary>
		public Element _ValueSampledData { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Signature ValueSignature { get; set; }
		///<summary>May contain extended information for property: 'ValueSignature'</summary>
		public Element _ValueSignature { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueString { get; set; }
		///<summary>May contain extended information for property: 'ValueString'</summary>
		public Element _ValueString { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public BackboneElement ValueSubstanceAmount { get; set; }
		///<summary>May contain extended information for property: 'ValueSubstanceAmount'</summary>
		public Element _ValueSubstanceAmount { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueSubstanceAmountReferenceRange { get; set; }
		///<summary>May contain extended information for property: 'ValueSubstanceAmountReferenceRange'</summary>
		public Element _ValueSubstanceAmountReferenceRange { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public BackboneElement ValueSubstanceMoiety { get; set; }
		///<summary>May contain extended information for property: 'ValueSubstanceMoiety'</summary>
		public Element _ValueSubstanceMoiety { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueTime { get; set; }
		///<summary>May contain extended information for property: 'ValueTime'</summary>
		public Element _ValueTime { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public BackboneElement ValueTiming { get; set; }
		///<summary>May contain extended information for property: 'ValueTiming'</summary>
		public Element _ValueTiming { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public Element ValueTimingRepeat { get; set; }
		///<summary>May contain extended information for property: 'ValueTimingRepeat'</summary>
		public Element _ValueTimingRepeat { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public TriggerDefinition ValueTriggerDefinition { get; set; }
		///<summary>May contain extended information for property: 'ValueTriggerDefinition'</summary>
		public Element _ValueTriggerDefinition { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public decimal ValueUnsignedInt { get; set; }
		///<summary>May contain extended information for property: 'ValueUnsignedInt'</summary>
		public Element _ValueUnsignedInt { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueUri { get; set; }
		///<summary>May contain extended information for property: 'ValueUri'</summary>
		public Element _ValueUri { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueUrl { get; set; }
		///<summary>May contain extended information for property: 'ValueUrl'</summary>
		public Element _ValueUrl { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public UsageContext ValueUsageContext { get; set; }
		///<summary>May contain extended information for property: 'ValueUsageContext'</summary>
		public Element _ValueUsageContext { get; set; }
		///<summary>Value of extension - must be one of a constrained set of the data types (see [Extensibility](extensibility.html) for a list)</summary>
		public string ValueUuid { get; set; }
		///<summary>May contain extended information for property: 'ValueUuid'</summary>
		public Element _ValueUuid { get; set; }
	}
	///<summary>
	///A human's name with the ability to identify parts and usage
	///</summary>
	///<source-file>humanname.xml</source-file>
	public class HumanName : Element
	{
		///<summary>The part of a name that links to the genealogy. In some cultures (e.g. Eritrea) the family name of a son is the first name of his father.</summary>
		public string Family { get; set; }
		///<summary>May contain extended information for property: 'Family'</summary>
		public Element _Family { get; set; }
		///<summary>Given name</summary>
		public string[] Given { get; set; }
		///<summary>May contain extended information for property: 'Given'</summary>
		public Element[] _Given { get; set; }
		///<summary>Indicates the period of time when this name was valid for the named person.</summary>
		public Period Period { get; set; }
		///<summary>May contain extended information for property: 'Period'</summary>
		public Element _Period { get; set; }
		///<summary>Part of the name that is acquired as a title due to academic, legal, employment or nobility status, etc. and that appears at the start of the name</summary>
		public string[] Prefix { get; set; }
		///<summary>May contain extended information for property: 'Prefix'</summary>
		public Element[] _Prefix { get; set; }
		///<summary>Part of the name that is acquired as a title due to academic, legal, employment or nobility status, etc. and that appears at the end of the name</summary>
		public string[] Suffix { get; set; }
		///<summary>May contain extended information for property: 'Suffix'</summary>
		public Element[] _Suffix { get; set; }
		///<summary>Specifies the entire name as it should be displayed e.g. on an application UI. This may be provided instead of or as well as the specific parts</summary>
		public string Text { get; set; }
		///<summary>May contain extended information for property: 'Text'</summary>
		public Element _Text { get; set; }
		///<summary>Identifies the purpose for this name</summary>
		public string Use { get; set; }
		///<summary>May contain extended information for property: 'Use'</summary>
		public Element _Use { get; set; }
	}
	///<summary>
	///An identifier - identifies some entity uniquely and unambiguously. Typically this is used for business identifiers
	///</summary>
	///<source-file>identifier.xml</source-file>
	public class Identifier : Element
	{
		///<summary>Organization that issued/manages the identifier</summary>
		public Reference Assigner { get; set; }
		///<summary>May contain extended information for property: 'Assigner'</summary>
		public Element _Assigner { get; set; }
		///<summary>Time period during which identifier is/was valid for use</summary>
		public Period Period { get; set; }
		///<summary>May contain extended information for property: 'Period'</summary>
		public Element _Period { get; set; }
		///<summary>Establishes the namespace for the value - that is, a URL that describes a set values that are unique.</summary>
		public string System { get; set; }
		///<summary>May contain extended information for property: 'System'</summary>
		public Element _System { get; set; }
		///<summary>A coded type for the identifier that can be used to determine which identifier to use for a specific purpose</summary>
		public CodeableConcept Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
		///<summary>The purpose of this identifier</summary>
		public string Use { get; set; }
		///<summary>May contain extended information for property: 'Use'</summary>
		public Element _Use { get; set; }
		///<summary>The portion of the identifier typically relevant to the user and which is unique within the context of the system</summary>
		public string Value { get; set; }
		///<summary>May contain extended information for property: 'Value'</summary>
		public Element _Value { get; set; }
	}
	///<summary>
	///The metadata about a resource. This is content in the resource that is maintained by the infrastructure. Changes to the content might not always be associated with version changes to the resource
	///</summary>
	///<source-file>meta.xml</source-file>
	public class Meta : Element
	{
		///<summary>When the resource last changed - e.g. when the version changed</summary>
		public string LastUpdated { get; set; }
		///<summary>May contain extended information for property: 'LastUpdated'</summary>
		public Element _LastUpdated { get; set; }
		///<summary>A list of profiles (references to [[[StructureDefinition]]] resources) that this resource claims to conform to. The URL is a reference to [[[StructureDefinition.url]]]</summary>
		public string[] Profile { get; set; }
		///<summary>May contain extended information for property: 'Profile'</summary>
		public Element[] _Profile { get; set; }
		///<summary>Security labels applied to this resource. These tags connect specific resources to the overall security policy and infrastructure</summary>
		public Coding[] Security { get; set; }
		///<summary>May contain extended information for property: 'Security'</summary>
		public Element[] _Security { get; set; }
		///<summary>A uri that identifies the source system of the resource. This provides a minimal amount of [[[Provenance]]] information that can be used to track or differentiate the source of information in the resource. The source may identify another FHIR server, document, message, database, etc.</summary>
		public string Source { get; set; }
		///<summary>May contain extended information for property: 'Source'</summary>
		public Element _Source { get; set; }
		///<summary>Tags applied to this resource. Tags are intended to be used to identify and relate resources to process and workflow, and applications are not required to consider the tags when interpreting the meaning of a resource</summary>
		public Coding[] Tag { get; set; }
		///<summary>May contain extended information for property: 'Tag'</summary>
		public Element[] _Tag { get; set; }
		///<summary>The version specific identifier, as it appears in the version portion of the URL. This value changes when the resource is created, updated, or deleted</summary>
		public string VersionId { get; set; }
		///<summary>May contain extended information for property: 'VersionId'</summary>
		public Element _VersionId { get; set; }
	}
	///<summary>
	///Common Ancestor declaration for conformance and knowledge artifact resources
	///</summary>
	///<source-file>metadataresource-spreadsheet.xml</source-file>
	public class MetadataResource : DomainResource
	{
		///<summary>The date on which the resource content was approved by the publisher. Approval happens once when the content is officially approved for usage.</summary>
		public string ApprovalDate { get; set; }
		///<summary>May contain extended information for property: 'ApprovalDate'</summary>
		public Element _ApprovalDate { get; set; }
		///<summary>Contact details to assist a user in finding and communicating with the publisher</summary>
		public ContactDetail[] Contact { get; set; }
		///<summary>May contain extended information for property: 'Contact'</summary>
		public Element[] _Contact { get; set; }
		///<summary>A copyright statement relating to the {{title}} and/or its contents. Copyright statements are generally legal restrictions on the use and publishing of the {{title}}.</summary>
		public string Copyright { get; set; }
		///<summary>May contain extended information for property: 'Copyright'</summary>
		public Element _Copyright { get; set; }
		///<summary>The date  (and optionally time) when the {{title}} was published. The date must change when the business version changes and it must change if the status code changes. In addition, it should change when the substantive content of the {{title}} changes.</summary>
		public string Date { get; set; }
		///<summary>May contain extended information for property: 'Date'</summary>
		public Element _Date { get; set; }
		///<summary>A free text natural language description of the {{title}} from a consumer's perspective.</summary>
		public string Description { get; set; }
		///<summary>May contain extended information for property: 'Description'</summary>
		public Element _Description { get; set; }
		///<summary>The period during which the {{title}} content was or is planned to be in active use.</summary>
		public Period EffectivePeriod { get; set; }
		///<summary>May contain extended information for property: 'EffectivePeriod'</summary>
		public Element _EffectivePeriod { get; set; }
		///<summary>A Boolean value to indicate that this {{title}} is authored for testing purposes (or education/evaluation/marketing) and is not intended to be used for genuine usage.</summary>
		public bool Experimental { get; set; }
		///<summary>May contain extended information for property: 'Experimental'</summary>
		public Element _Experimental { get; set; }
		///<summary>A formal identifier that is used to identify this {{title}} when it is represented in other formats, or referenced in a specification, model, design or an instance.</summary>
		public Identifier[] Identifier { get; set; }
		///<summary>May contain extended information for property: 'Identifier'</summary>
		public Element[] _Identifier { get; set; }
		///<summary>A legal or geographic region in which the {{title}} is intended to be used.</summary>
		public CodeableConcept[] Jurisdiction { get; set; }
		///<summary>May contain extended information for property: 'Jurisdiction'</summary>
		public Element[] _Jurisdiction { get; set; }
		///<summary>The date on which the resource content was last reviewed. Review happens periodically after approval but does not change the original approval date.</summary>
		public string LastReviewDate { get; set; }
		///<summary>May contain extended information for property: 'LastReviewDate'</summary>
		public Element _LastReviewDate { get; set; }
		///<summary>A natural language name identifying the {{title}}. This name should be usable as an identifier for the module by machine processing applications such as code generation.</summary>
		public string Name { get; set; }
		///<summary>May contain extended information for property: 'Name'</summary>
		public Element _Name { get; set; }
		///<summary>The name of the organization or individual that published the {{title}}.</summary>
		public string Publisher { get; set; }
		///<summary>May contain extended information for property: 'Publisher'</summary>
		public Element _Publisher { get; set; }
		///<summary>Explanation of why this {{title}} is needed and why it has been designed as it has.</summary>
		public string Purpose { get; set; }
		///<summary>May contain extended information for property: 'Purpose'</summary>
		public Element _Purpose { get; set; }
		///<summary>The status of this {{title}}. Enables tracking the life-cycle of the content.</summary>
		public string Status { get; set; }
		///<summary>May contain extended information for property: 'Status'</summary>
		public Element _Status { get; set; }
		///<summary>A short, descriptive, user-friendly title for the {{title}}.</summary>
		public string Title { get; set; }
		///<summary>May contain extended information for property: 'Title'</summary>
		public Element _Title { get; set; }
		///<summary>An absolute URI that is used to identify this {{title}} when it is referenced in a specification, model, design or an instance; also called its canonical identifier. This SHOULD be globally unique and SHOULD be a literal address at which at which an authoritative instance of this {{title}} is (or will be) published. This URL can be the target of a canonical reference. It SHALL remain the same when the {{title}} is stored on different servers</summary>
		public string Url { get; set; }
		///<summary>May contain extended information for property: 'Url'</summary>
		public Element _Url { get; set; }
		///<summary>The content was developed with a focus and intent of supporting the contexts that are listed. These contexts may be general categories (gender, age, ...) or may be references to specific programs (insurance plans, studies, ...) and may be used to assist with indexing and searching for appropriate {{title}} instances</summary>
		public UsageContext[] UseContext { get; set; }
		///<summary>May contain extended information for property: 'UseContext'</summary>
		public Element[] _UseContext { get; set; }
		///<summary>The identifier that is used to identify this version of the {{title}} when it is referenced in a specification, model, design or instance. This is an arbitrary value managed by the {{title}} author and is not expected to be globally unique. For example, it might be a timestamp (e.g. yyyymmdd) if a managed version is not available. There is also no expectation that versions can be placed in a lexicographical sequence</summary>
		public string Version { get; set; }
		///<summary>May contain extended information for property: 'Version'</summary>
		public Element _Version { get; set; }
	}
	///<summary>
	///The ModuleMetadata structure defines the common metadata elements used by quality improvement artifacts. This information includes descriptive and topical metadata to enable repository searches, as well as governance and evidentiary support information
	///</summary>
	///<source-file>modulemetadata.xml</source-file>
	public class ModuleMetadata : Element
	{
		///<summary>Contact details to assist a user in finding and communicating with the publisher</summary>
		public ContactDetail[] Contact { get; set; }
		///<summary>May contain extended information for property: 'Contact'</summary>
		public Element[] _Contact { get; set; }
		///<summary>A contributor to the content of the module, including authors, editors, reviewers, and endorsers</summary>
		public Contributor[] Contributor { get; set; }
		///<summary>May contain extended information for property: 'Contributor'</summary>
		public Element[] _Contributor { get; set; }
		///<summary>A copyright statement relating to the module and/or its contents. Copyright statements are generally legal restrictions on the use and publishing of the module</summary>
		public string Copyright { get; set; }
		///<summary>May contain extended information for property: 'Copyright'</summary>
		public Element _Copyright { get; set; }
		///<summary>Specifies various attributes of the patient population for whom and/or environment of care in which the knowledge module is applicable</summary>
		public UsageContext[] Coverage { get; set; }
		///<summary>May contain extended information for property: 'Coverage'</summary>
		public Element[] _Coverage { get; set; }
		///<summary>A free text natural language description of the module from the consumer's perspective</summary>
		public string Description { get; set; }
		///<summary>May contain extended information for property: 'Description'</summary>
		public Element _Description { get; set; }
		///<summary>The period during which the module content is effective</summary>
		public Period EffectivePeriod { get; set; }
		///<summary>May contain extended information for property: 'EffectivePeriod'</summary>
		public Element _EffectivePeriod { get; set; }
		///<summary>Determines whether the module was developed for testing purposes (or education/evaluation/marketing), and is not intended to be used in production environments</summary>
		public bool Experimental { get; set; }
		///<summary>May contain extended information for property: 'Experimental'</summary>
		public Element _Experimental { get; set; }
		///<summary>A logical identifier for the module such as the CMS or NQF identifiers for a measure artifact. Note that at least one identifier is required for non-experimental active artifacts</summary>
		public Identifier[] Identifier { get; set; }
		///<summary>May contain extended information for property: 'Identifier'</summary>
		public Element[] _Identifier { get; set; }
		///<summary>The date on which the module content was last reviewed</summary>
		public string LastReviewDate { get; set; }
		///<summary>May contain extended information for property: 'LastReviewDate'</summary>
		public Element _LastReviewDate { get; set; }
		///<summary>A machine-friendly name for the module. This name should be usable as an identifier for the module by machine processing applications such as code generation</summary>
		public string Name { get; set; }
		///<summary>May contain extended information for property: 'Name'</summary>
		public Element _Name { get; set; }
		///<summary>The date on which the module was published</summary>
		public string PublicationDate { get; set; }
		///<summary>May contain extended information for property: 'PublicationDate'</summary>
		public Element _PublicationDate { get; set; }
		///<summary>The name of the individual or organization that published the module (also known as the steward for the module). This information is required for non-experimental published artifacts</summary>
		public string Publisher { get; set; }
		///<summary>May contain extended information for property: 'Publisher'</summary>
		public Element _Publisher { get; set; }
		///<summary>A brief description of the purpose of the module</summary>
		public string Purpose { get; set; }
		///<summary>May contain extended information for property: 'Purpose'</summary>
		public Element _Purpose { get; set; }
		///<summary>Related resources such as additional documentation, justification, or bibliographic references</summary>
		public RelatedArtifact[] RelatedArtifact { get; set; }
		///<summary>May contain extended information for property: 'RelatedArtifact'</summary>
		public Element[] _RelatedArtifact { get; set; }
		///<summary>The status of the module</summary>
		public string Status { get; set; }
		///<summary>May contain extended information for property: 'Status'</summary>
		public Element _Status { get; set; }
		///<summary>A short, descriptive, user-friendly title for the module</summary>
		public string Title { get; set; }
		///<summary>May contain extended information for property: 'Title'</summary>
		public Element _Title { get; set; }
		///<summary>Clinical topics related to the content of the module</summary>
		public CodeableConcept[] Topic { get; set; }
		///<summary>May contain extended information for property: 'Topic'</summary>
		public Element[] _Topic { get; set; }
		///<summary>Identifies the type of knowledge module, such as a rule, library, documentation template, or measure</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
		///<summary>An absolute URL that is used to identify this module when it is referenced. This SHALL be a URL, SHOULD be globally unique, and SHOULD be an address at which this module definition is (or will be) published</summary>
		public string Url { get; set; }
		///<summary>May contain extended information for property: 'Url'</summary>
		public Element _Url { get; set; }
		///<summary>A detailed description of how the module is used from a clinical perspective</summary>
		public string Usage { get; set; }
		///<summary>May contain extended information for property: 'Usage'</summary>
		public Element _Usage { get; set; }
		///<summary>The version of the module, if any. To provide a version consistent with the Decision Support Service specification, use the format Major.Minor.Revision (e.g. 1.0.0). For more information on versioning knowledge modules, refer to the Decision Support Service specification. Note that a version is required for non-experimental active artifacts</summary>
		public string Version { get; set; }
		///<summary>May contain extended information for property: 'Version'</summary>
		public Element _Version { get; set; }
	}
	///<summary>
	///An amount of economic utility in some recognized currency
	///</summary>
	///<source-file>money.xml</source-file>
	public class Money : Element
	{
		///<summary>ISO 4217 Currency Code</summary>
		public string Currency { get; set; }
		///<summary>May contain extended information for property: 'Currency'</summary>
		public Element _Currency { get; set; }
		///<summary>Numerical value (with implicit precision)</summary>
		public decimal Value { get; set; }
		///<summary>May contain extended information for property: 'Value'</summary>
		public Element _Value { get; set; }
	}
	///<summary>
	///A human-readable summary of the resource conveying the essential clinical and business information for the resource
	///</summary>
	///<source-file>narrative.xml</source-file>
	public class Narrative : Element
	{
		///<summary>The actual narrative content, a stripped down version of XHTML</summary>
		public string Div { get; set; }
		///<summary>May contain extended information for property: 'Div'</summary>
		public Element _Div { get; set; }
		///<summary>The status of the narrative - whether it's entirely generated (from just the defined data or the extensions too), or whether a human authored it and it may contain additional data</summary>
		public string Status { get; set; }
		///<summary>May contain extended information for property: 'Status'</summary>
		public Element _Status { get; set; }
	}
	///<summary>
	///The parameters to the module. This collection specifies both the input and output parameters. Input parameters are provided by the caller as part of the $evaluate operation. Output parameters are included in the GuidanceResponse
	///</summary>
	///<source-file>parameterdefinition.xml</source-file>
	public class ParameterDefinition : Element
	{
		///<summary>A brief discussion of what the parameter is for and how it is used by the module</summary>
		public string Documentation { get; set; }
		///<summary>May contain extended information for property: 'Documentation'</summary>
		public Element _Documentation { get; set; }
		///<summary>The maximum number of times this element is permitted to appear in the request or response</summary>
		public string Max { get; set; }
		///<summary>May contain extended information for property: 'Max'</summary>
		public Element _Max { get; set; }
		///<summary>The minimum number of times this parameter SHALL appear in the request or response</summary>
		public decimal Min { get; set; }
		///<summary>May contain extended information for property: 'Min'</summary>
		public Element _Min { get; set; }
		///<summary>The name of the parameter used to allow access to the value of the parameter in evaluation contexts</summary>
		public string Name { get; set; }
		///<summary>May contain extended information for property: 'Name'</summary>
		public Element _Name { get; set; }
		///<summary>If specified, this indicates a profile that the input data must conform to, or that the output data will conform to</summary>
		public string Profile { get; set; }
		///<summary>May contain extended information for property: 'Profile'</summary>
		public Element _Profile { get; set; }
		///<summary>The type of the parameter</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
		///<summary>Whether the parameter is input or output for the module</summary>
		public string Use { get; set; }
		///<summary>May contain extended information for property: 'Use'</summary>
		public Element _Use { get; set; }
	}
	///<summary>
	///A time period defined by a start and end date and optionally time.
	///</summary>
	///<source-file>period.xml</source-file>
	public class Period : Element
	{
		///<summary>The end of the period. If the end of the period is missing, it means no end was known or planned at the time the instance was created. The start may be in the past, and the end date in the future, which means that period is expected/planned to end at that time</summary>
		public string End { get; set; }
		///<summary>May contain extended information for property: 'End'</summary>
		public Element _End { get; set; }
		///<summary>The start of the period. The boundary is inclusive.</summary>
		public string Start { get; set; }
		///<summary>May contain extended information for property: 'Start'</summary>
		public Element _Start { get; set; }
	}
	///<summary>
	///A measured amount (or an amount that can potentially be measured). Note that measured amounts include amounts that are not precisely quantified, including amounts involving arbitrary units and floating currencies
	///</summary>
	///<source-file>quantity.xml</source-file>
	public class Quantity : Element
	{
		///<summary>A computer processable form of the unit in some unit representation system</summary>
		public string Code { get; set; }
		///<summary>May contain extended information for property: 'Code'</summary>
		public Element _Code { get; set; }
		///<summary>How the value should be understood and represented - whether the actual value is greater or less than the stated value due to measurement issues; e.g. if the comparator is "<" , then the real value is < stated value</summary>
		public string Comparator { get; set; }
		///<summary>May contain extended information for property: 'Comparator'</summary>
		public Element _Comparator { get; set; }
		///<summary>The identification of the system that provides the coded form of the unit</summary>
		public string System { get; set; }
		///<summary>May contain extended information for property: 'System'</summary>
		public Element _System { get; set; }
		///<summary>A human-readable form of the unit</summary>
		public string Unit { get; set; }
		///<summary>May contain extended information for property: 'Unit'</summary>
		public Element _Unit { get; set; }
		///<summary>The value of the measured amount. The value includes an implicit precision in the presentation of the value</summary>
		public decimal Value { get; set; }
		///<summary>May contain extended information for property: 'Value'</summary>
		public Element _Value { get; set; }
	}
	///<summary>
	///A set of ordered Quantities defined by a low and high limit. 
	///</summary>
	///<source-file>range.xml</source-file>
	public class Range : Element
	{
		///<summary>The high limit. The boundary is inclusive. </summary>
		public SimpleQuantity High { get; set; }
		///<summary>May contain extended information for property: 'High'</summary>
		public Element _High { get; set; }
		///<summary>The low limit. The boundary is inclusive.</summary>
		public SimpleQuantity Low { get; set; }
		///<summary>May contain extended information for property: 'Low'</summary>
		public Element _Low { get; set; }
	}
	///<summary>
	///A relationship of two Quantity values - expressed as a numerator and a denominator. 
	///</summary>
	///<source-file>ratio.xml</source-file>
	public class Ratio : Element
	{
		///<summary>The value of the denominator</summary>
		public Quantity Denominator { get; set; }
		///<summary>May contain extended information for property: 'Denominator'</summary>
		public Element _Denominator { get; set; }
		///<summary>The value of the numerator</summary>
		public Quantity Numerator { get; set; }
		///<summary>May contain extended information for property: 'Numerator'</summary>
		public Element _Numerator { get; set; }
	}
	///<summary>
	///A reference from one resource to another
	///</summary>
	///<source-file>reference.xml</source-file>
	public class Reference : Element
	{
		///<summary>Plain text narrative that identifies the resource in addition to the resource reference </summary>
		public string Display { get; set; }
		///<summary>May contain extended information for property: 'Display'</summary>
		public Element _Display { get; set; }
		///<summary>An identifier for the target resource. This is used when there is no way to reference the other resource directly, either because the entity it represents is not available through a FHIR server, or because there is no way for the author of the resource to convert a known identifier to an actual location. There is no requirement that a Reference.identifier point to something that is actually exposed as a FHIR instance, but it SHALL point to a business concept that would be expected to be exposed as a FHIR instance, and that instance would need to be of a FHIR resource type allowed by the reference</summary>
		public Identifier Identifier { get; set; }
		///<summary>May contain extended information for property: 'Identifier'</summary>
		public Element _Identifier { get; set; }
		///<summary>A reference to a location at which the other resource is found. The reference may be a relative reference, in which case it is relative to the service base URL, or an absolute URL that resolves to the location where the resource is found. The reference may be version specific or not. If the reference is not to a FHIR RESTful server, then it should be assumed to be version specific. Internal fragment references (start with '#') refer to contained resources</summary>
		public string reference { get; set; }
		///<summary>May contain extended information for property: 'reference'</summary>
		public Element _reference { get; set; }
		///<summary>The expected type of the target of the reference. If both Reference.type and Reference.reference are populated and Reference.reference is a FHIR URL, both SHALL be consistent.
		/// 
		/// The type is the Canonical URL of Resource Definition that is the type this reference refers to. References are URLs that are relative to http://hl7.org/fhir/StructureDefinition/ e.g. "Patient" is a reference to http://hl7.org/fhir/StructureDefinition/Patient. Absolute URLs are only allowed for logical models (and can only be used in references in logical models, not resources)</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
	}
	///<summary>
	///Related artifacts such as additional documentation, justification, or bibliographic references
	///</summary>
	///<source-file>relatedartifact.xml</source-file>
	public class RelatedArtifact : Element
	{
		///<summary>A bibliographic citation for the related artifact. This text SHOULD be formatted according to an accepted citation format</summary>
		public string Citation { get; set; }
		///<summary>May contain extended information for property: 'Citation'</summary>
		public Element _Citation { get; set; }
		///<summary>A brief description of the document or knowledge resource being referenced, suitable for display to a consumer</summary>
		public string Display { get; set; }
		///<summary>May contain extended information for property: 'Display'</summary>
		public Element _Display { get; set; }
		///<summary>The document being referenced, represented as an attachment. This is exclusive with the resource element</summary>
		public Attachment Document { get; set; }
		///<summary>May contain extended information for property: 'Document'</summary>
		public Element _Document { get; set; }
		///<summary>A short label that can be used to reference the citation from elsewhere in the containing artifact, such as a footnote index</summary>
		public string Label { get; set; }
		///<summary>May contain extended information for property: 'Label'</summary>
		public Element _Label { get; set; }
		///<summary>The related resource, such as a library, value set, profile, or other knowledge resource</summary>
		public string Resource { get; set; }
		///<summary>May contain extended information for property: 'Resource'</summary>
		public Element _Resource { get; set; }
		///<summary>The type of relationship to the related artifact</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
		///<summary>A url for the artifact that can be followed to access the actual content</summary>
		public string Url { get; set; }
		///<summary>May contain extended information for property: 'Url'</summary>
		public Element _Url { get; set; }
	}
	///<summary>
	///This is the base resource type for everything.
	///</summary>
	///<source-file>resource-spreadsheet.xml</source-file>
	public class Resource
	{
        /// <summary>The type name of the resource (e.g., Patient, Encounter).</summary>
        public string ResourceType { get; set; }
		///<summary>The logical id of the resource, as used in the URL for the resource. Once assigned, this value never changes</summary>
		public string Id { get; set; }
		///<summary>May contain extended information for property: 'Id'</summary>
		public Element _Id { get; set; }
		///<summary>A reference to a set of rules that were followed when the resource was constructed, and which must be understood when processing the content. Often, this is a reference to an implementation guide that defines the special rules along with other profiles etc.</summary>
		public string ImplicitRules { get; set; }
		///<summary>May contain extended information for property: 'ImplicitRules'</summary>
		public Element _ImplicitRules { get; set; }
		///<summary>The base language in which the resource is written</summary>
		public string Language { get; set; }
		///<summary>May contain extended information for property: 'Language'</summary>
		public Element _Language { get; set; }
		///<summary>The metadata about the resource. This is content that is maintained by the infrastructure. Changes to the content might not always be associated with version changes to the resource</summary>
		public Meta Meta { get; set; }
		///<summary>May contain extended information for property: 'Meta'</summary>
		public Element _Meta { get; set; }
	}
	///<summary>
	///A series of measurements taken by a device, with upper and lower limits. There may be more than one dimension in the data
	///</summary>
	///<source-file>sampleddata.xml</source-file>
	public class SampledData : Element
	{
		///<summary>A series of data points which are decimal values separated by a single space (character u20). The special values "E" (error), "L" (below detection limit) and "U" (above detection limit) can also be used in place of a decimal value</summary>
		public string Data { get; set; }
		///<summary>May contain extended information for property: 'Data'</summary>
		public Element _Data { get; set; }
		///<summary>The number of sample points at each time point. If this value is greater than one, then the dimensions will be interlaced - all the sample points for a point in time will be recorded at once</summary>
		public decimal Dimensions { get; set; }
		///<summary>May contain extended information for property: 'Dimensions'</summary>
		public Element _Dimensions { get; set; }
		///<summary>A correction factor that is applied to the sampled data points before they are added to the origin</summary>
		public decimal Factor { get; set; }
		///<summary>May contain extended information for property: 'Factor'</summary>
		public Element _Factor { get; set; }
		///<summary>The lower limit of detection of the measured points. This is needed if any of the data points have the value "L" (lower than detection limit)</summary>
		public decimal LowerLimit { get; set; }
		///<summary>May contain extended information for property: 'LowerLimit'</summary>
		public Element _LowerLimit { get; set; }
		///<summary>The base quantity that a measured value of zero represents. In addition, this provides the units of the entire measurement series</summary>
		public SimpleQuantity Origin { get; set; }
		///<summary>May contain extended information for property: 'Origin'</summary>
		public Element _Origin { get; set; }
		///<summary>The length of time between sampling times, measured in milliseconds</summary>
		public decimal Period { get; set; }
		///<summary>May contain extended information for property: 'Period'</summary>
		public Element _Period { get; set; }
		///<summary>The upper limit of detection of the measured points. This is needed if any of the data points have the value "U" (higher than detection limit)</summary>
		public decimal UpperLimit { get; set; }
		///<summary>May contain extended information for property: 'UpperLimit'</summary>
		public Element _UpperLimit { get; set; }
	}
	///<summary>
	///A signature along with supporting context. The signature may be a digital signature that is cryptographic in nature, or some other signature acceptable to the domain. This other signature may be as simple as a graphical image representing a hand-written signature, or a signature ceremony Different signature approaches have different utilities.
	///</summary>
	///<source-file>signature.xml</source-file>
	public class Signature : Element
	{
		///<summary>The base64 encoding of the Signature content. When signature is not recorded electronically this element would be empty.</summary>
		public string Data { get; set; }
		///<summary>May contain extended information for property: 'Data'</summary>
		public Element _Data { get; set; }
		///<summary>A reference to an application-usable description of the identity that is represented by the signature.</summary>
		public Reference OnBehalfOf { get; set; }
		///<summary>May contain extended information for property: 'OnBehalfOf'</summary>
		public Element _OnBehalfOf { get; set; }
		///<summary>A mime type that indicates the technical format of the signature. Important mime types are application/signature+xml for X ML DigSig, application/jose for JWS, and image/* for a graphical image of a signature, etc.</summary>
		public string SigFormat { get; set; }
		///<summary>May contain extended information for property: 'SigFormat'</summary>
		public Element _SigFormat { get; set; }
		///<summary>A mime type that indicates the technical format of the target resources signed by the signature.</summary>
		public string TargetFormat { get; set; }
		///<summary>May contain extended information for property: 'TargetFormat'</summary>
		public Element _TargetFormat { get; set; }
		///<summary>An indication of the reason that the entity signed this document. This may be explicitly included as part of the signature information and can be used when determining accountability for various actions concerning the document. </summary>
		public Coding[] Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element[] _Type { get; set; }
		///<summary>When the digital signature was signed. </summary>
		public string When { get; set; }
		///<summary>May contain extended information for property: 'When'</summary>
		public Element _When { get; set; }
		///<summary>A reference to an application-usable description of the identity that signed  (e.g. the signature used their private key)</summary>
		public Reference Who { get; set; }
		///<summary>May contain extended information for property: 'Who'</summary>
		public Element _Who { get; set; }
	}
	///<summary>
	///WARN: SimpleQuantity definition cannot be found!
	///</summary>
	///<source-file>definition-file-not-found.xml</source-file>
	public class SimpleQuantity : Quantity
	{
	}
	///<summary>
	///WARN: Structure definition cannot be found!
	///</summary>
	///<source-file>definition-file-not-found.xml</source-file>
	public class Structure : Element
	{
	}
	///<summary>
	///A definition of a FHIR structure. This resource is used to describe the underlying resources, data types defined in FHIR, and also for describing extensions and constraints on resources and data types.
	///</summary>
	///<source-file>structuredefinition-spreadsheet.xml</source-file>
	public class StructureDefinition : MetadataResource
	{
		///<summary>Whether structure this definition describes is abstract or not  - that is, whether the structure is not intended to be instantiated. For Resources and Data types, abstract types will never be exchanged  between systems</summary>
		public bool Abstract { get; set; }
		///<summary>May contain extended information for property: 'Abstract'</summary>
		public Element _Abstract { get; set; }
		///<summary>An absolute URI that is the base structure from which this type is derived, either by specialization or constraint</summary>
		public string BaseDefinition { get; set; }
		///<summary>May contain extended information for property: 'BaseDefinition'</summary>
		public Element _BaseDefinition { get; set; }
		///<summary>Identifies the types of resource or data type elements to which the extension can be applied</summary>
		public StructureDefinitionContext[] Context { get; set; }
		///<summary>May contain extended information for property: 'Context'</summary>
		public Element[] _Context { get; set; }
		///<summary>A set of rules as FHIRPath Invariants about when the extension can be used (e.g. co-occurrence variants for the extension). All the rules must be true. </summary>
		public string[] ContextInvariant { get; set; }
		///<summary>May contain extended information for property: 'ContextInvariant'</summary>
		public Element[] _ContextInvariant { get; set; }
		///<summary>How the type relates to the baseDefinition.</summary>
		public string Derivation { get; set; }
		///<summary>May contain extended information for property: 'Derivation'</summary>
		public Element _Derivation { get; set; }
		///<summary>A differential view is expressed relative to the base StructureDefinition - a statement of differences that it applies</summary>
		public StructureDefinitionDifferential Differential { get; set; }
		///<summary>May contain extended information for property: 'Differential'</summary>
		public Element _Differential { get; set; }
		///<summary>The version of the FHIR specification on which this StructureDefinition is based - this is the formal version of the specification, without the revision number, e.g. [publication].[major].[minor], which is $version$ for this version</summary>
		public string FhirVersion { get; set; }
		///<summary>May contain extended information for property: 'FhirVersion'</summary>
		public Element _FhirVersion { get; set; }
		///<summary>A set of key words or terms from external terminologies that may be used to assist with indexing and searching of templates nby describing the use of this structure definition, or the content it describes </summary>
		public Coding[] Keyword { get; set; }
		///<summary>May contain extended information for property: 'Keyword'</summary>
		public Element[] _Keyword { get; set; }
		///<summary>Defines the kind of structure that this definition is describing</summary>
		public string Kind { get; set; }
		///<summary>May contain extended information for property: 'Kind'</summary>
		public Element _Kind { get; set; }
		///<summary>An external specification that the content is mapped to</summary>
		public StructureDefinitionMapping[] Mapping { get; set; }
		///<summary>May contain extended information for property: 'Mapping'</summary>
		public Element[] _Mapping { get; set; }
		///<summary>A snapshot view is expressed in a standalone form that can be used and interpreted without considering the base StructureDefinition</summary>
		public StructureDefinitionSnapshot Snapshot { get; set; }
		///<summary>May contain extended information for property: 'Snapshot'</summary>
		public Element _Snapshot { get; set; }
		///<summary>The type this structure describes. If the derivation kind is 'specialization' then this is the master definition for a type, and there is always one of these (a data type, an extension, a resource, including abstract ones). Otherwise the structure definition is a constraint on the stated type (and in this case, the type cannot be an abstract type).  References are URLs that are relative to http://hl7.org/fhir/StructureDefinition e.g. "string" is a reference to http://hl7.org/fhir/StructureDefinition/string. Absolute URLs are only allowed in logical models</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
	}
	///<summary>
	///Identifies the types of resource or data type elements to which the extension can be applied
	///</summary>
	///<source-file>structuredefinition-spreadsheet.xml</source-file>
	public class StructureDefinitionContext : Element
	{
		///<summary>An expression that defines where an extension can be used in resources</summary>
		public string Expression { get; set; }
		///<summary>May contain extended information for property: 'Expression'</summary>
		public Element _Expression { get; set; }
		///<summary>Defines how to interpret the expression that defines what the context of the extension is</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
	}
	///<summary>
	///A differential view is expressed relative to the base StructureDefinition - a statement of differences that it applies
	///</summary>
	///<source-file>structuredefinition-spreadsheet.xml</source-file>
	public class StructureDefinitionDifferential : Element
	{
		///<summary>Captures constraints on each element within the resource</summary>
		public ElementDefinition[] Element { get; set; }
		///<summary>May contain extended information for property: 'Element'</summary>
		public Element[] _Element { get; set; }
	}
	///<summary>
	///An external specification that the content is mapped to
	///</summary>
	///<source-file>structuredefinition-spreadsheet.xml</source-file>
	public class StructureDefinitionMapping : Element
	{
		///<summary>Comments about this mapping, including version notes, issues, scope limitations, and other important notes for usage</summary>
		public string Comment { get; set; }
		///<summary>May contain extended information for property: 'Comment'</summary>
		public Element _Comment { get; set; }
		///<summary>An Internal id that is used to identify this mapping set when specific mappings are made</summary>
		public string Identity { get; set; }
		///<summary>May contain extended information for property: 'Identity'</summary>
		public Element _Identity { get; set; }
		///<summary>A name for the specification that is being mapped to</summary>
		public string Name { get; set; }
		///<summary>May contain extended information for property: 'Name'</summary>
		public Element _Name { get; set; }
		///<summary>An absolute URI that identifies the specification that this mapping is expressed to</summary>
		public string Uri { get; set; }
		///<summary>May contain extended information for property: 'Uri'</summary>
		public Element _Uri { get; set; }
	}
	///<summary>
	///A snapshot view is expressed in a standalone form that can be used and interpreted without considering the base StructureDefinition
	///</summary>
	///<source-file>structuredefinition-spreadsheet.xml</source-file>
	public class StructureDefinitionSnapshot : Element
	{
		///<summary>Captures constraints on each element within the resource</summary>
		public ElementDefinition[] Element { get; set; }
		///<summary>May contain extended information for property: 'Element'</summary>
		public Element[] _Element { get; set; }
	}
	///<summary>
	///Specifies an event that may occur multiple times. Timing schedules are used to record when things are planned, expected or requested to occur. The most common usage is in dosage instructions for medications. They are also used when planning care of various kinds, and may be used for reporting the schedule to which past regular activities were carried out
	///</summary>
	///<source-file>timing.xml</source-file>
	public class Timing : BackboneElement
	{
		///<summary>A code for the timing schedule (or just text in code.text). Some codes such as BID are ubiquitous, but many institutions define their own additional codes. If a code is provided, the code is understood to be a complete statement of whatever is specified in the structured timing data, and either the code or the data may be used to interpret the Timing, with the exception that .repeat.bounds still applies over the code (and is not contained in the code)</summary>
		public CodeableConcept Code { get; set; }
		///<summary>May contain extended information for property: 'Code'</summary>
		public Element _Code { get; set; }
		///<summary>Identifies specific times when the event occurs</summary>
		public string[] Event { get; set; }
		///<summary>May contain extended information for property: 'Event'</summary>
		public Element[] _Event { get; set; }
		///<summary>A set of rules that describe when the event is scheduled</summary>
		public TimingRepeat Repeat { get; set; }
		///<summary>May contain extended information for property: 'Repeat'</summary>
		public Element _Repeat { get; set; }
	}
	///<summary>
	///A set of rules that describe when the event is scheduled
	///</summary>
	///<source-file>timing.xml</source-file>
	public class TimingRepeat : Element
	{
		///<summary>Either a duration for the length of the timing schedule, a range of possible length, or outer bounds for start and/or end limits of the timing schedule</summary>
		public Duration BoundsDuration { get; set; }
		///<summary>May contain extended information for property: 'BoundsDuration'</summary>
		public Element _BoundsDuration { get; set; }
		///<summary>Either a duration for the length of the timing schedule, a range of possible length, or outer bounds for start and/or end limits of the timing schedule</summary>
		public Period BoundsPeriod { get; set; }
		///<summary>May contain extended information for property: 'BoundsPeriod'</summary>
		public Element _BoundsPeriod { get; set; }
		///<summary>Either a duration for the length of the timing schedule, a range of possible length, or outer bounds for start and/or end limits of the timing schedule</summary>
		public Range BoundsRange { get; set; }
		///<summary>May contain extended information for property: 'BoundsRange'</summary>
		public Element _BoundsRange { get; set; }
		///<summary>A total count of the desired number of repetitions across the duration of the entire timing specification. If countMax is present, this element indicates the lower bound of the allowed range of count values</summary>
		public decimal Count { get; set; }
		///<summary>May contain extended information for property: 'Count'</summary>
		public Element _Count { get; set; }
		///<summary>If present, indicates that the count is a range - so to perform the action between [count] and [countMax] times</summary>
		public decimal CountMax { get; set; }
		///<summary>May contain extended information for property: 'CountMax'</summary>
		public Element _CountMax { get; set; }
		///<summary>If one or more days of week is provided, then the action happens only on the specified day(s)</summary>
		public string[] DayOfWeek { get; set; }
		///<summary>May contain extended information for property: 'DayOfWeek'</summary>
		public Element[] _DayOfWeek { get; set; }
		///<summary>How long this thing happens for when it happens. If durationMax is present, this element indicates the lower bound of the allowed range of the duration</summary>
		public decimal Duration { get; set; }
		///<summary>May contain extended information for property: 'Duration'</summary>
		public Element _Duration { get; set; }
		///<summary>If present, indicates that the duration is a range - so to perform the action between [duration] and [durationMax] time length</summary>
		public decimal DurationMax { get; set; }
		///<summary>May contain extended information for property: 'DurationMax'</summary>
		public Element _DurationMax { get; set; }
		///<summary>The units of time for the duration, in UCUM units</summary>
		public string DurationUnit { get; set; }
		///<summary>May contain extended information for property: 'DurationUnit'</summary>
		public Element _DurationUnit { get; set; }
		///<summary>The number of times to repeat the action within the specified period. If frequencyMax is present, this element indicates the lower bound of the allowed range of the frequency</summary>
		public decimal Frequency { get; set; }
		///<summary>May contain extended information for property: 'Frequency'</summary>
		public Element _Frequency { get; set; }
		///<summary>If present, indicates that the frequency is a range - so to repeat between [frequency] and [frequencyMax] times within the period or period range</summary>
		public decimal FrequencyMax { get; set; }
		///<summary>May contain extended information for property: 'FrequencyMax'</summary>
		public Element _FrequencyMax { get; set; }
		///<summary>The number of minutes from the event. If the event code does not indicate whether the minutes is before or after the event, then the offset is assumed to be after the event</summary>
		public decimal Offset { get; set; }
		///<summary>May contain extended information for property: 'Offset'</summary>
		public Element _Offset { get; set; }
		///<summary>Indicates the duration of time over which repetitions are to occur; e.g. to express "3 times per day", 3 would be the frequency and "1 day" would be the period. If periodMax is present, this element indicates the lower bound of the allowed range of the period length</summary>
		public decimal Period { get; set; }
		///<summary>May contain extended information for property: 'Period'</summary>
		public Element _Period { get; set; }
		///<summary>If present, indicates that the period is a range from [period] to [periodMax], allowing expressing concepts such as "do this once every 3-5 days</summary>
		public decimal PeriodMax { get; set; }
		///<summary>May contain extended information for property: 'PeriodMax'</summary>
		public Element _PeriodMax { get; set; }
		///<summary>The units of time for the period in UCUM units</summary>
		public string PeriodUnit { get; set; }
		///<summary>May contain extended information for property: 'PeriodUnit'</summary>
		public Element _PeriodUnit { get; set; }
		///<summary>Specified time of day for action to take place</summary>
		public string[] TimeOfDay { get; set; }
		///<summary>May contain extended information for property: 'TimeOfDay'</summary>
		public Element[] _TimeOfDay { get; set; }
		///<summary>An approximate time period during the day, potentially linked to an event of daily living that indicates when the action should occur</summary>
		public string[] When { get; set; }
		///<summary>May contain extended information for property: 'When'</summary>
		public Element[] _When { get; set; }
	}
	///<summary>
	///A description of a triggering event. Triggering events can be named events, data events, or periodic, as determined by the type element
	///</summary>
	///<source-file>triggerdefinition.xml</source-file>
	public class TriggerDefinition : Element
	{
		///<summary>A boolean-valued expression that is evaluated in the context of the container of the trigger definition and returns whether or not the trigger fires</summary>
		public Expression Condition { get; set; }
		///<summary>May contain extended information for property: 'Condition'</summary>
		public Element _Condition { get; set; }
		///<summary>The triggering data of the event (if this is a data trigger). If more than one data is requirement is specified, then all the data requirements must be true</summary>
		public DataRequirement[] Data { get; set; }
		///<summary>May contain extended information for property: 'Data'</summary>
		public Element[] _Data { get; set; }
		///<summary>A formal name for the event. This may be an absolute URI that identifies the event formally (e.g. from a trigger registry), or a simple relative URI that identifies the event in a local context</summary>
		public string Name { get; set; }
		///<summary>May contain extended information for property: 'Name'</summary>
		public Element _Name { get; set; }
		///<summary>The timing of the event (if this is a periodic trigger)</summary>
		public string TimingDate { get; set; }
		///<summary>May contain extended information for property: 'TimingDate'</summary>
		public Element _TimingDate { get; set; }
		///<summary>The timing of the event (if this is a periodic trigger)</summary>
		public string TimingDateTime { get; set; }
		///<summary>May contain extended information for property: 'TimingDateTime'</summary>
		public Element _TimingDateTime { get; set; }
		///<summary>The timing of the event (if this is a periodic trigger)</summary>
		public Reference TimingReference { get; set; }
		///<summary>May contain extended information for property: 'TimingReference'</summary>
		public Element _TimingReference { get; set; }
		///<summary>The timing of the event (if this is a periodic trigger)</summary>
		public Timing TimingTiming { get; set; }
		///<summary>May contain extended information for property: 'TimingTiming'</summary>
		public Element _TimingTiming { get; set; }
		///<summary>The type of triggering event</summary>
		public string Type { get; set; }
		///<summary>May contain extended information for property: 'Type'</summary>
		public Element _Type { get; set; }
	}
	///<summary>
	///Specifies clinical/business/etc. metadata that can be used to retrieve, index and/or categorize an artifact. This metadata can either be specific to the applicable population (e.g., age category, DRG) or the specific context of care (e.g., venue, care setting, provider of care)
	///</summary>
	///<source-file>usagecontext.xml</source-file>
	public class UsageContext : Element
	{
		///<summary>A code that identifies the type of context being specified by this usage context</summary>
		public Coding Code { get; set; }
		///<summary>May contain extended information for property: 'Code'</summary>
		public Element _Code { get; set; }
		///<summary>A value that defines the context specified in this context of use. The interpretation of the value is defined by the code</summary>
		public CodeableConcept ValueCodeableConcept { get; set; }
		///<summary>May contain extended information for property: 'ValueCodeableConcept'</summary>
		public Element _ValueCodeableConcept { get; set; }
		///<summary>A value that defines the context specified in this context of use. The interpretation of the value is defined by the code</summary>
		public Quantity ValueQuantity { get; set; }
		///<summary>May contain extended information for property: 'ValueQuantity'</summary>
		public Element _ValueQuantity { get; set; }
		///<summary>A value that defines the context specified in this context of use. The interpretation of the value is defined by the code</summary>
		public Range ValueRange { get; set; }
		///<summary>May contain extended information for property: 'ValueRange'</summary>
		public Element _ValueRange { get; set; }
		///<summary>A value that defines the context specified in this context of use. The interpretation of the value is defined by the code</summary>
		public Reference ValueReference { get; set; }
		///<summary>May contain extended information for property: 'ValueReference'</summary>
		public Element _ValueReference { get; set; }
	}
} // close namespace: fhir
