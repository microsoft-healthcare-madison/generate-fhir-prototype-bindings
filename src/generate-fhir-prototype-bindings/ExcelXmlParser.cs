using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace generate_fhir_prototype_bindings
{
    class ExcelXmlParser
    {
        private enum DataColumnTypes
        {
            typeString,
            typeDecimal,
            typeBoolean,
            typeDateTime,
            typeUnknown
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Parse an Office 2003 XML file into a DataSet.</summary>
        ///
        /// <remarks>Gino Canessa, 7/5/2019.</remarks>
        ///
        /// <param name="xmlFilename">        Filename of the XML file.</param>
        /// <param name="ds">                 [out] The ds.</param>
        /// <param name="firstRowsAreHeaders">(Optional) True if first rows are headers.</param>
        /// <param name="detectColumnTypes">  (Optional) True to detect column types, false defaults to string</param>
        ///
        /// <returns>True if it succeeds, false if it fails.</returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool ParseFile(
                                    string xmlFilename,
                                    out DataSet ds,
                                    bool firstRowsAreHeaders = true,
                                    bool detectColumnTypes = false
                                    )
        {
            // **** default to returning null ****

            ds = null;

            // **** sanity check ****

            if (!File.Exists(xmlFilename))
            {
                Console.WriteLine($"Input file: {xmlFilename} not found!");

                // **** fail ****

                return false;
            }

            // **** create our XML document object ****

            XmlDocument xmlDoc = new XmlDocument();

            // **** load the specified file ****

            xmlDoc.Load(xmlFilename);

            // **** create a namespace manager for the document ****

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);

            // **** add the namespaces we see in FHIR spreadsheets ****

            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
            nsmgr.AddNamespace("x", "urn:schemas-microsoft-com:office:excel");
            nsmgr.AddNamespace("html", "http://www.w3.org/TR/REC-html40");
            nsmgr.AddNamespace("o", "urn:schemas-microsoft-com:office:office");

            // **** create the data set we will return ****

            ds = new DataSet();

            // **** traverse the worksheets in the document ****

            foreach (XmlNode worksheetNode in xmlDoc.DocumentElement.SelectNodes("//ss:Worksheet", nsmgr))
            {
                int columnCount = 0;

                // **** create a data table representing this worksheet ****

                DataTable worksheetDt = new DataTable(worksheetNode.Attributes["ss:Name"].Value);

                // **** select the table record ****

                XmlNode worksheetTableNode = worksheetNode.SelectSingleNode("ss:Table", nsmgr);

                // **** check for a column number attribute ****

                if (worksheetTableNode.Attributes["ss:ExpandedColumnCount"] != null)
                {
                    columnCount = int.Parse(worksheetTableNode.Attributes["ss:ExpandedColumnCount"].Value);
                }

                // **** add our table to our set ****

                ds.Tables.Add(worksheetDt);

                // **** grab the rows in this table ****

                XmlNodeList worksheetRows = worksheetTableNode.SelectNodes("ss:Row", nsmgr);

                // **** check for no rows ****

                if (worksheetRows.Count == 0)
                {
                    // **** move to the next worksheet ****

                    continue;
                }

                // **** determine location of first data row ****

                int firstDatRowIndex = (firstRowsAreHeaders) ? 1 : 0;

                // **** check for first row being header names ****

                if (firstRowsAreHeaders)
                {
                    // **** process the header ****

                    ProcessHeaderRow(ref worksheetDt, worksheetRows, nsmgr, columnCount);
                }
                else
                {
                    AddNonHeaderColumns(ref worksheetDt, worksheetRows, firstDatRowIndex, nsmgr, columnCount);
                }

                // **** check for a data row to detect data types ****

                if ((detectColumnTypes == true) && (worksheetRows.Count > firstDatRowIndex))
                {
                    DetectColumnDataTypes(ref worksheetDt, worksheetRows, firstDatRowIndex, nsmgr);
                }

                // **** create our column typing array ****

                DataColumnTypes[] columnTypes = new DataColumnTypes[worksheetDt.Columns.Count];

                for (int columnIndex = 0; columnIndex < worksheetDt.Columns.Count; columnIndex++)
                {
                    string currentColumnType = worksheetDt.Columns[columnIndex].DataType.Name;

                    switch (currentColumnType)
                    {
                        case "Boolean":
                            columnTypes[columnIndex] = DataColumnTypes.typeBoolean;
                            break;
                        case "DateTime":
                            columnTypes[columnIndex] = DataColumnTypes.typeDateTime;
                            break;
                        case "Decimal":
                            columnTypes[columnIndex] = DataColumnTypes.typeDecimal;
                            break;
                        case "String":
                            columnTypes[columnIndex] = DataColumnTypes.typeString;
                            break;

                        default:
                            columnTypes[columnIndex] = DataColumnTypes.typeUnknown;
                            break;
                    }
                }

                // **** process data rows ****

                for (int rowIndex = firstDatRowIndex; rowIndex < worksheetRows.Count; rowIndex++)
                {
                    ProcessRows(ref worksheetDt, worksheetRows, rowIndex, nsmgr, columnTypes);
                }
            }

            // **** success! ****

            return true;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the rows.</summary>
        ///
        /// <remarks>Gino Canessa, 7/5/2019.</remarks>
        ///
        /// <param name="worksheetDt">     [in,out] The worksheet dt.</param>
        /// <param name="worksheetRows">   The worksheet rows.</param>
        /// <param name="firstDatRowIndex">The first dat row index.</param>
        /// <param name="nsmgr">           The nsmgr.</param>
        ///-------------------------------------------------------------------------------------------------

        private static void ProcessRows(
                                        ref DataTable worksheetDt,
                                        XmlNodeList worksheetRows,
                                        int firstDatRowIndex,
                                        XmlNamespaceManager nsmgr,
                                        DataColumnTypes[] columnTypes
                                        )
        {
            // **** create our table row ****

            DataRow tableRow = worksheetDt.NewRow();

            // **** grab the first data row cells ****

            XmlNodeList rowCells = worksheetRows[firstDatRowIndex].SelectNodes("ss:Cell", nsmgr);

            // **** determine the number of cells ****

            int cellCount = rowCells.Count;

            // **** process each cell ****

            for (int cellIndex = 0, columnIndex = 0; cellIndex < cellCount; cellIndex++, columnIndex++)
            {
                // **** grab the cell ****

                XmlNode cell = rowCells[cellIndex];

                // **** check for a cell index (indicates skipped cells) ****

                if (cell.Attributes["ss:Index"] != null)
                {
                    // **** update our column index ****

                    columnIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;
                }

                // **** try to grab the data ****

                XmlNode cellData = cell.SelectSingleNode("ss:Data", nsmgr);

                // **** add our value ****

                if ((cellData != null) && (!string.IsNullOrEmpty(cellData.InnerText)))
                {
                    // **** act depending on type ****

                    switch (columnTypes[columnIndex])
                    {
                        case DataColumnTypes.typeString:

                            // **** leave as string ****

                            tableRow[columnIndex] = cellData.InnerText;
                            break;

                        case DataColumnTypes.typeDecimal:

                            // **** pull a decimal value ****

                            if (decimal.TryParse(cellData.InnerText, out decimal valueDecimal))
                            {
                                tableRow[columnIndex] = valueDecimal;
                            }

                            break;

                        case DataColumnTypes.typeBoolean:

                            // **** check value ****

                            if ((cellData.InnerText == "1") || (cellData.InnerText.ToLower().StartsWith("t")))
                            {
                                tableRow[columnIndex] = true;
                            }
                            else
                            {
                                tableRow[columnIndex] = false;
                            }

                            break;

                        case DataColumnTypes.typeDateTime:

                            // **** pull a datetime value ****

                            if (DateTime.TryParse(cellData.InnerText, out DateTime valueDateTime))
                            {
                                tableRow[columnIndex] = valueDateTime;
                            }

                            break;

                        default:

                            // **** just copy the value ****

                            tableRow[columnIndex] = cellData.InnerText;

                            break;
                    }

                }
            }

            // **** add our row ****

            worksheetDt.Rows.Add(tableRow);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Detect column data types.</summary>
        ///
        /// <remarks>Gino Canessa, 7/5/2019.</remarks>
        ///
        /// <param name="worksheetDt">     [in,out] The worksheet dt.</param>
        /// <param name="worksheetRows">   The worksheet rows.</param>
        /// <param name="firstDatRowIndex">The first dat row index.</param>
        /// <param name="nsmgr">           The nsmgr.</param>
        ///-------------------------------------------------------------------------------------------------

        private static void DetectColumnDataTypes(
                                                 ref DataTable worksheetDt,
                                                 XmlNodeList worksheetRows,
                                                 int firstDatRowIndex,
                                                 XmlNamespaceManager nsmgr
                                                 )
        {
            // **** grab the first data row cells ****

            XmlNodeList rowCells = worksheetRows[firstDatRowIndex].SelectNodes("ss:Cell", nsmgr);

            // **** determine the number of cells ****

            int cellCount = rowCells.Count;

            // **** process each cell ****

            for (int cellIndex = 0, columnIndex = 0; cellIndex < cellCount; cellIndex++, columnIndex++)
            {
                // **** grab the cell ****

                XmlNode cell = rowCells[cellIndex];

                // **** check for a cell index (indicates skipped cells) ****

                if (cell.Attributes["ss:Index"] != null)
                {
                    // **** update our column index ****

                    columnIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;
                }

                // **** try to grab the data ****

                XmlNode cellData = cell.SelectSingleNode("ss:Data", nsmgr);

                // **** check to see if this field has no data type ****

                if (cellData == null)
                {
                    // **** leave as string ****

                    continue;
                }

                // **** act depending on type ****

                switch (cellData.Attributes["ss:Type"].Value)
                {
                    case "String":

                        // **** leave as string ****

                        break;

                    case "Number":

                        // **** update column type ****

                        worksheetDt.Columns[columnIndex].DataType = typeof(decimal);

                        break;

                    case "Boolean":

                        // **** update column type ****

                        worksheetDt.Columns[columnIndex].DataType = typeof(bool);

                        break;

                    case "DateTime":

                        // **** update column type ****

                        worksheetDt.Columns[columnIndex].DataType = typeof(DateTime);

                        break;

                    default:

                        Console.WriteLine($"Unknown/Unhandled Data Type: {cellData.Attributes["ss:Type"].Value}!!!");

                        break;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Adds a non header columns.</summary>
        ///
        /// <remarks>Gino Canessa, 7/5/2019.</remarks>
        ///
        /// <param name="worksheetDt">     [in,out] The worksheet dt.</param>
        /// <param name="worksheetRows">   The worksheet rows.</param>
        /// <param name="firstDatRowIndex">The first dat row index.</param>
        /// <param name="nsmgr">           The nsmgr.</param>
        ///-------------------------------------------------------------------------------------------------

        private static void AddNonHeaderColumns(
                                                ref DataTable worksheetDt,
                                                XmlNodeList worksheetRows,
                                                int firstDatRowIndex,
                                                XmlNamespaceManager nsmgr,
                                                int estimatedColumnCount = 0
                                                )
        {
            // **** grab the first data row cells ****

            XmlNodeList rowCells = worksheetRows[firstDatRowIndex].SelectNodes("ss:Cell", nsmgr);

            // **** determine the number of cells ****

            int cellCount = rowCells.Count;
            int columnIndex = 0;

            // **** process each cell ****

            for (int cellIndex = 0; cellIndex < cellCount; cellIndex++, columnIndex++)
            {
                // **** check for a cell index (indicates skipped cells) ****

                if (rowCells[cellIndex].Attributes["ss:Index"] != null)
                {
                    // **** check the specified index ****

                    int specifiedIndex = int.Parse(rowCells[cellIndex].Attributes["ss:Index"].Value) - 1;

                    while (columnIndex < specifiedIndex)
                    {
                        // **** add this column as a string for now, cannot determine type yet ****

                        worksheetDt.Columns.Add($"Column {columnIndex++}", typeof(string));
                        columnIndex++;
                    }
                }

                // **** add this column as a string for now, cannot determine type yet ****

                worksheetDt.Columns.Add($"Column {columnIndex}", typeof(string));
            }

            // **** check for being below the estimate ****

            while (columnIndex < estimatedColumnCount)
            {
                // **** add a blank-titled column as a string for now, cannot determine type yet ****

                worksheetDt.Columns.Add($"Column {columnIndex}", typeof(string));

                columnIndex++;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Process the header row.</summary>
        ///
        /// <remarks>Gino Canessa, 7/5/2019.</remarks>
        ///
        /// <param name="worksheetDt">  [in,out] The worksheet dt.</param>
        /// <param name="worksheetRows">The worksheet rows.</param>
        /// <param name="nsmgr">        The nsmgr.</param>
        ///-------------------------------------------------------------------------------------------------

        private static void ProcessHeaderRow(
                                            ref DataTable worksheetDt,
                                            XmlNodeList worksheetRows,
                                            XmlNamespaceManager nsmgr,
                                            int estimatedColumnCount = 0
                                            )
        {
            // **** grab the cells of the header row ****

            XmlNodeList rowCells = worksheetRows[0].SelectNodes("ss:Cell", nsmgr);

            // **** determine the number of cells ****

            int cellCount = rowCells.Count;
            int columnIndex = 0;

            // **** process each cell ****

            for (int cellIndex = 0; cellIndex < cellCount; cellIndex++, columnIndex++)
            {
                // **** grab the cell ****

                XmlNode cell = rowCells[cellIndex];

                // **** check for a cell index (indicates skipped cells) ****

                if (cell.Attributes["ss:Index"] != null)
                {
                    // **** check the specified index ****

                    int specifiedIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                    while (columnIndex < specifiedIndex)
                    {
                        // **** add a blank-titled column as a string for now, cannot determine type yet ****

                        worksheetDt.Columns.Add($"", typeof(string));
                        columnIndex++;
                    }
                }

                // **** try to grab the data ****

                XmlNode cellData = cell.SelectSingleNode("ss:Data", nsmgr);

                // **** check to see if this field has no data type ****

                if (cellData == null)
                {
                    // **** add a blank-titled column as a string for now, cannot determine type yet ****

                    worksheetDt.Columns.Add($"Column {columnIndex}", typeof(string));

                    // **** nothing else for this column ****

                    continue;
                }

                // **** check for this being a duplicate column name ****

                if (worksheetDt.Columns.Contains(cellData.InnerText))
                {
                    // **** add a blank-titled column as a string for now, cannot determine type yet ****

                    worksheetDt.Columns.Add($"Column {columnIndex}", typeof(string));

                    // **** nothing else for this column ****

                    continue;
                }

                // **** add this column as a string for now, cannot determine type yet ****

                worksheetDt.Columns.Add(cellData.InnerText, typeof(string));
            }

            // **** check for being below the estimate ****

            while (columnIndex < estimatedColumnCount)
            {
                // **** add a blank-titled column as a string for now, cannot determine type yet ****

                worksheetDt.Columns.Add($"", typeof(string));

                columnIndex++;
            }
        }
    }
}
