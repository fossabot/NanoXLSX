﻿using NanoXLSX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NanoXLSX_Test.Reader
{
    public class ImportOptionTest
    {

        [Fact( DisplayName = "Test of the reader functionality with the global import option to cast everything to string")]
        public void CastAllToStringTest()
        {
            Dictionary<string, Object> cells = new Dictionary<string, object>();
            cells.Add("A1", "test");
            cells.Add("A2", true);
            cells.Add("A3", false);
            cells.Add("A4", 42);
            cells.Add("A5", 0.55f);
            cells.Add("A6", -0.111d);
            cells.Add("A7", new DateTime(2020, 11, 10, 9, 8, 7, 0));
            cells.Add("A8", new TimeSpan(18, 15, 12));
            cells.Add("A9", null);
            Dictionary<string, string> expectedCells = new Dictionary<string, string>();
            expectedCells.Add("A1", "test");
            expectedCells.Add("A2", "True");
            expectedCells.Add("A3", "False");
            expectedCells.Add("A4", "42");
            expectedCells.Add("A5", "0.55");
            expectedCells.Add("A6", "-0.111");
            expectedCells.Add("A7", "2020-11-10 09:08:07");
            expectedCells.Add("A8", "18:15:12");
            expectedCells.Add("A9", null);

            ImportOptions options = new ImportOptions();
            options.GlobalEnforcingType = ImportOptions.GlobalType.EverythingToString;
            AssertValues<object, string>(cells, options, AssertEquals, expectedCells);
        }

        [Fact(DisplayName = "Test of the reader functionality with the global import option to cast all number to double")]
        public void CastToDoubleTest()
        {
            Dictionary<string, Object> cells = new Dictionary<string, object>();
            cells.Add("A1", "test");
            cells.Add("A2", true);
            cells.Add("A3", false);
            cells.Add("A4", 42);
            cells.Add("A5", 0.55f);
            cells.Add("A6", -0.111d);
            cells.Add("A7", new DateTime(2020, 11, 10, 9, 8, 7, 0));
            cells.Add("A8", new TimeSpan(18, 15, 12));
            cells.Add("A9", null);
            Dictionary<string, object> expectedCells = new Dictionary<string, object>();
            expectedCells.Add("A1", "test");
            expectedCells.Add("A2", true);
            expectedCells.Add("A3", false);
            expectedCells.Add("A4", 42d);
            expectedCells.Add("A5", 0.55d);
            expectedCells.Add("A6", -0.111d);
            expectedCells.Add("A7", double.Parse(Utils.GetOADateTimeString(new DateTime(2020,11,10,9,8,7,0))));
            expectedCells.Add("A8", double.Parse(Utils.GetOATimeString(new TimeSpan(18,15,12))));
            expectedCells.Add("A9", null);
            ImportOptions options = new ImportOptions();
            options.GlobalEnforcingType = ImportOptions.GlobalType.AllNumbersToDouble;
            AssertValues<object, object>(cells, options, AssertApproximate, expectedCells);
        }

        [Fact(DisplayName = "Test of the reader functionality with the global import option to cast all number to int")]
        public void CastToIntTest()
        {
            Dictionary<string, Object> cells = new Dictionary<string, object>();
            cells.Add("A1", "test");
            cells.Add("A2", true);
            cells.Add("A3", false);
            cells.Add("A4", 42);
            cells.Add("A5", 0.55f);
            cells.Add("A6", -3.111d);
            cells.Add("A7", new DateTime(2020, 11, 10, 9, 8, 7, 0));
            cells.Add("A8", new TimeSpan(18,15,12));
            cells.Add("A9", -4.9f);
            cells.Add("A10", 0.49d);
            cells.Add("A11", null);
            Dictionary<string, object> expectedCells = new Dictionary<string, object>();
            expectedCells.Add("A1", "test");
            expectedCells.Add("A2", true);
            expectedCells.Add("A3", false);
            expectedCells.Add("A4", 42);
            expectedCells.Add("A5", 1);
            expectedCells.Add("A6", -3);
            expectedCells.Add("A7", (int)Math.Round(double.Parse(Utils.GetOADateTimeString(new DateTime(2020, 11, 10, 9, 8, 7, 0))),0));
            expectedCells.Add("A8", (int)Math.Round(double.Parse(Utils.GetOATimeString(new TimeSpan(18,15,12))), 0));
            expectedCells.Add("A9", -5);
            expectedCells.Add("A10", 0);
            expectedCells.Add("A11", null);
            ImportOptions options = new ImportOptions();
            options.GlobalEnforcingType = ImportOptions.GlobalType.AllNumbersToInt;
            AssertValues<object, object>(cells, options, AssertApproximate, expectedCells);
        }

        [Fact(DisplayName = "Test of the reader functionality with the import option EnforceEmptyValuesAsString")]
        public void EnforceEmptyValuesAsStringTest()
        {
            Dictionary<string, Object> cells = new Dictionary<string, object>();
            cells.Add("A1", "test");
            cells.Add("A2", true);
            cells.Add("A3", 22.2d);
            cells.Add("A4", null);
            cells.Add("A5", "");
            Dictionary<string, object> expectedCells = new Dictionary<string, object>();
            expectedCells.Add("A1", "test");
            expectedCells.Add("A2", true);
            expectedCells.Add("A3", 22.2f); // Import will go to smallest float unit (float 32 / single)
            expectedCells.Add("A4", "");
            expectedCells.Add("A5", "");
            ImportOptions options = new ImportOptions();
            options.EnforceEmptyValuesAsString = true;
            AssertValues<object, object>(cells, options, AssertApproximate, expectedCells);
        }

        [Fact(DisplayName = "Test of the EnforcingStartRowNumber functionality on global enforcing rules")]
        public void EnforcingStartRowNumberTest()
        {
            Dictionary<string, Object> cells = new Dictionary<string, object>();
            cells.Add("A1", 22);
            cells.Add("A2", true);
            cells.Add("A3", 22);
            cells.Add("A4", true);
            cells.Add("A5", 22.5d);
            Dictionary<string, object> expectedCells = new Dictionary<string, object>();
            expectedCells.Add("A1", 22);
            expectedCells.Add("A2", true);
            expectedCells.Add("A3", "22"); // Import will go to the smallest float unit (float 32 / single)
            expectedCells.Add("A4", "True");
            expectedCells.Add("A5", "22.5");
            ImportOptions options = new ImportOptions();
            options.EnforcingStartRowNumber = 2;
            options.GlobalEnforcingType = ImportOptions.GlobalType.EverythingToString;
            AssertValues<object, object>(cells, options, AssertApproximate, expectedCells);
        }

        private static void AssertValues<T,D>(Dictionary<string, T> givenCells, ImportOptions importOptions, Action<object, object> assertionAction, Dictionary<string, D> expectedCells = null)
        {
            Workbook workbook = new Workbook("worksheet1");
            foreach (KeyValuePair<string, T> cell in givenCells)
            {
                workbook.CurrentWorksheet.AddCell(cell.Value, cell.Key);
            }
            MemoryStream stream = new MemoryStream();
            workbook.SaveAsStream(stream, true);
            stream.Position = 0;
            Workbook givenWorkbook = Workbook.Load(stream, importOptions);

            Assert.NotNull(givenWorkbook);
            Worksheet givenWorksheet = givenWorkbook.SetCurrentWorksheet(0);
            Assert.Equal("worksheet1", givenWorksheet.SheetName);
            foreach (string address in givenCells.Keys)
            {
                Cell givenCell = givenWorksheet.GetCell(new Address(address));
                D expectedValue = expectedCells[address];
                if (expectedValue == null)
                {
                    Assert.Equal(Cell.CellType.EMPTY, givenCell.DataType);
                }
                else
                {
                    assertionAction.Invoke(expectedValue, (D)givenCell.Value);
                }
            }
        }
        private static void AssertEquals<T>(T expected, T given)
        {
            Assert.Equal(expected, given);
        }

        private static void AssertApproximate(object expected, object given)
        {
            double threshold = 0.0000001; // The precision may vary 
            if (given is double)
            {
                Assert.True(Math.Abs((double)given - (double)expected) < threshold);
            }
            else if (given is float)
            {
                Assert.True(Math.Abs((float)given - (float)expected) < threshold);
            }
            else if (given is DateTime)
            {
                AssertApproximate((double)expected, double.Parse(Utils.GetOADateTimeString((DateTime)given)));
            }
            else
            {
                AssertEquals<object>(expected,given);
            }
            
        }

    }
}