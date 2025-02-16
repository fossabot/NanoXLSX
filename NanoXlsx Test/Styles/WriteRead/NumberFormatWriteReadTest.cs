﻿using NanoXLSX;
using NanoXLSX.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NanoXLSX_Test.Styles.WriteRead
{
    public class NumberFormatWriteReadTest
    {

        [Theory(DisplayName = "Test of the 'customFormatID' value when writing and reading a NumberFormat style")]
        [InlineData(164, "test")]
        [InlineData(165, 0.5f)]
        [InlineData(200, 22)]
        [InlineData(2000, true)]
        public void CustomFormatIDFormatTest(int styleValue, object value)
        {
            Style style = new Style();
            style.CurrentNumberFormat.CustomFormatID = styleValue;
            style.CurrentNumberFormat.Number = NumberFormat.FormatNumber.custom; // Mandatory
            Cell cell = TestUtils.SaveAndReadStyledCell(value, style, "A1");
            Assert.Equal(styleValue, cell.CellStyle.CurrentNumberFormat.CustomFormatID);
        }

        [Theory(DisplayName = "Test of the 'customFormatCode' value when writing and reading a NumberFormat style")]
        [InlineData("#", "test")]
        [InlineData("", 0.5f)]
        [InlineData(" ", 22)]
        [InlineData("ABCDE", true)]
        public void CustomFormatCodeNumberFormatTest(string styleValue, object value)
        {
            Style style = new Style();
            style.CurrentNumberFormat.CustomFormatCode = styleValue;
            style.CurrentNumberFormat.Number = NumberFormat.FormatNumber.custom; // Mandatory
            Cell cell = TestUtils.SaveAndReadStyledCell(value, style, "A1");
            Assert.Equal(styleValue, cell.CellStyle.CurrentNumberFormat.CustomFormatCode);
            Assert.Equal(NumberFormat.FormatNumber.custom, cell.CellStyle.CurrentNumberFormat.Number);
            Assert.True(cell.CellStyle.CurrentNumberFormat.IsCustomFormat);
        }

        [Theory(DisplayName = "Test of the 'formatNumber' value when writing and reading a NumberFormat style")]
        [InlineData(NumberFormat.FormatNumber.format_1, "test")]
        [InlineData(NumberFormat.FormatNumber.format_2, 0.5f)]
        [InlineData(NumberFormat.FormatNumber.format_3, 22)]
        [InlineData(NumberFormat.FormatNumber.format_4, true)]
        [InlineData(NumberFormat.FormatNumber.format_5, "")]
        [InlineData(NumberFormat.FormatNumber.format_6, -1)]
        [InlineData(NumberFormat.FormatNumber.format_7, -22.222f)]
        [InlineData(NumberFormat.FormatNumber.format_8, false)]
        [InlineData(NumberFormat.FormatNumber.format_9, 0)]
        [InlineData(NumberFormat.FormatNumber.format_10, "Æ")]
        [InlineData(NumberFormat.FormatNumber.format_11, "test")]
        [InlineData(NumberFormat.FormatNumber.format_12, 0.5f)]
        [InlineData(NumberFormat.FormatNumber.format_13, 22)]
        [InlineData(NumberFormat.FormatNumber.format_14, true)]
        [InlineData(NumberFormat.FormatNumber.format_15, "")]
        [InlineData(NumberFormat.FormatNumber.format_16, -1)]
        [InlineData(NumberFormat.FormatNumber.format_17, -22.222f)]
        [InlineData(NumberFormat.FormatNumber.format_18, false)]
        [InlineData(NumberFormat.FormatNumber.format_19, "noDate")]
        [InlineData(NumberFormat.FormatNumber.format_20, "Æ")]
        [InlineData(NumberFormat.FormatNumber.format_21, "test")]
        [InlineData(NumberFormat.FormatNumber.format_22, "noDate")]
        [InlineData(NumberFormat.FormatNumber.format_37, 22)]
        [InlineData(NumberFormat.FormatNumber.format_38, true)]
        [InlineData(NumberFormat.FormatNumber.format_39, "")]
        [InlineData(NumberFormat.FormatNumber.format_40, -1)]
        [InlineData(NumberFormat.FormatNumber.format_45, -22.222f)]
        [InlineData(NumberFormat.FormatNumber.format_46, false)]
        [InlineData(NumberFormat.FormatNumber.format_47, "noDate")]
        [InlineData(NumberFormat.FormatNumber.format_48, "Æ")]
        [InlineData(NumberFormat.FormatNumber.format_49, "test")]
        [InlineData(NumberFormat.FormatNumber.custom, 0.5f)]
        public void NumberNumberFormatTest(NumberFormat.FormatNumber styleValue, object value)
        {
            Style style = new Style();
            style.CurrentNumberFormat.Number = styleValue;
            Cell cell = TestUtils.SaveAndReadStyledCell(value, style, "A1");
            Assert.Equal(styleValue, cell.CellStyle.CurrentNumberFormat.Number);
        }

        [Theory(DisplayName = "Test of the 'formatNumber' value with date formats when writing and reading a NumberFormat style")]
        [InlineData(NumberFormat.FormatNumber.format_14, 1000, "26.09.1902")]
        [InlineData(NumberFormat.FormatNumber.format_15, 1000, "26.09.1902")]
        [InlineData(NumberFormat.FormatNumber.format_16, 1000, "26.09.1902")]
        [InlineData(NumberFormat.FormatNumber.format_17, 1000, "26.09.1902")]
        [InlineData(NumberFormat.FormatNumber.format_22, 1000, "26.09.1902")]
        public void NumberNumberFormatTest2(NumberFormat.FormatNumber styleValue, int value, string expected)
        {
            DateTime expectedValue = DateTime.ParseExact(expected, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            Style style = new Style();
            style.CurrentNumberFormat.Number = styleValue;
            Cell cell = TestUtils.SaveAndReadStyledCell(value, expectedValue, style, "A1");
            Assert.Equal(styleValue, cell.CellStyle.CurrentNumberFormat.Number);
            Assert.Equal(expectedValue, cell.Value);
        }

        [Theory(DisplayName = "Test of the 'formatNumber' value with time formats when writing and reading a NumberFormat style")]
        [InlineData(NumberFormat.FormatNumber.format_19, 0.5, "12:00:00")]
        [InlineData(NumberFormat.FormatNumber.format_20, 0.5, "12:00:00")]
        [InlineData(NumberFormat.FormatNumber.format_21, 0.5, "12:00:00")]
        [InlineData(NumberFormat.FormatNumber.format_45, 0.5, "12:00:00")]
        [InlineData(NumberFormat.FormatNumber.format_46, 0.5, "12:00:00")]
        [InlineData(NumberFormat.FormatNumber.format_47, 0.5, "12:00:00")]
        public void NumberNumberFormatTest3(NumberFormat.FormatNumber styleValue, float value, string expected)
        {
            TimeSpan expectedValue = TimeSpan.ParseExact(expected, "hh\\:mm\\:ss", System.Globalization.CultureInfo.InvariantCulture);
            Style style = new Style();
            style.CurrentNumberFormat.Number = styleValue;
            Cell cell = TestUtils.SaveAndReadStyledCell(value, expectedValue, style, "A1");
            Assert.Equal(styleValue, cell.CellStyle.CurrentNumberFormat.Number);
            Assert.Equal(expectedValue, cell.Value);
        }

    }
}
