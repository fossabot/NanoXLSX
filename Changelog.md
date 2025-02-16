# Change Log

## v2.0.0

---
Release Date: **03.09.2022 - Major Release**

### Workbook and Shortener

- Added a list of MRU colors that can be defined in the Workbook class (methods AddMruColor, ClearMruColors)
- Added an exposed property for the workbook protection password hash (will be filled when loading a workbook)
- Added the method SetSelectedWorksheet by name in the Workbook class
- Added two methods GetWorksheet by name or index in the Workbook class
- Added the methods CopyWorksheetIntoThis and CopyWorksheetTo with several overloads in the Workbook class
- Added the function RemoveWorksheet by index with the option of resetting the current worksheet, in the Workbook class
- Added the function SetCurrentWorksheet by index in the Workbook class
- Added the function SetSelectedWorksheet by name in the Workbook class
- Added a Shortener-Class constructor with a workbook reference
- The shortener functions Down and Right have now an option to keep row and column positions
- Added two shortener functions Up and Left
- Made several style assigning methods deprecated in the Workbook class (will be removed in future versions)

### Worksheet

- Added an exposed property for the worksheet protection password hash (will be filled when loading a workbook)
- Added the methods GetFirstDataColumnNumber, GetFirstDataColumnNumber, GetFirstDataRowNumber, GetFirstRowNumber, GetLastDataColumnNumber, GetFirstCellAddress, GetFirstDataCellAddress, GetLastDataColumnNumber, GetLastDataRowNumber, GetLastRowNumber, GetLastCellAddress,  GetLastCellAddress and GetLastDataCellAddress
- Added the methods GetRow and GetColumns by address string or index
- Added the method Copy to copy a worksheet (deep copy)
- Added a constructor with only the worksheet name as parameter
- Added and option in GoToNextColumn and GoToNextRow to either keep the current row or column
- Added the methods RemoveRowHeight and RemoveAllowedActionOnSheetProtection
- Renamed columnAddress and rowAddress to columnNumber and rowNumber in the AddNextCell, AddCellFormula and RemoveCell methods
- Added several validations for worksheet data

### Cells, Rows and Columns

- In Cell, the address can now have reference modifiers ($)
- The worksheet reference in the Cell constructor was removed. Assigning to a worksheet is now managed automatically by the worksheet when adding a cell
- Added a property CellAddressType in Cell
- Cells can now have null as value, interpreted as empty
- Added a new overloaded function ResolveCellCoordinate to resolve the address type as well
- Added ValidateColumnNumber and ValidateRowNumber in Cell
- In Address, the constructor with string and address type now only needs a string, since reference modifiers ($) are resolved automatically
- Address objects are now comparable
- Implemented better address validation
- Range start and end addresses are swapped automatically, if reversed

### Styles

- Font has now an enum of possible underline values (e.g. instead of a bool)
- CellXf supports now indentation
- A new, internal style repository was introduced to streamline the style management
- Color (RGB) values are now validated (Fill class has a function ValidateColor)
- Style components have now more appropriate default values
- MRU colors are now not collected from defined style colors but from the MRU list in the workbook object
- The ToString function of Styles and all sub parts will now give a complete outline of all elements
- Fixed several issues with style comparison
- Several style default values were introduced as constants

### Formulas

- Added uint as possible formula value. Valid types are int, uint, long, ulong, float, double, byte, sbyte, decimal, short and ushort
- Added several validity checks

### Reader

- Added default values for dates, times and culture info in the import options
- Added global casting import options: AllNumbersToDouble, AllNumbersToDecimal, AllNumbersToInt, EverythingToString
- Added column casting import options: Double, Decimal
- Added global import options: EnforcePhoneticCharacterImport, EnforceEmptyValuesAsString, DateTimeFormat, TemporalCultureInfo
- Added a meta data reader
- All style elements that can be written can also be read
- All workbook elements that can be written can also be read (exception: passwords cannot be recovered)
- All worksheet elements that can be written can also be read (exception: passwords cannot be recovered)
- Better handling of dates and times, especially with invalid (too low and too high numbers) values

### Misc
- Added a unit test project with several thousand, partially parametrized test cases
- Added several constants for boundary dates in the Utils class
- Added several functions for pane splitting in the Utils class
- Exposed the (legacy) password generation method in the Utils class
- Updated documentation among the whole project
- Exceptions have no sub-tiles anymore
- Overhauled the whole writer
- Removed lot of dead code for better maintenance


## v1.8.7

---
Release Date: **06.08.2022**

- Fixed a bug when setting a workbook protection password

## v1.8.6

---
Release Date: **02.04.2022**

- Added an import option to display phonetic characters (like Ruby Characters / Furigana / Zhuyin Fuhao are now discarded) in strings

Note: Phonetic characters are discarded by default. If the import option "EnforcePhoneticCharacterImport" is set to true, the phonetic transcription will be displayed in brackets, right after the characters to be transcribed

## v1.8.5

---
Release Date: **27.03.2022**

- Fixed a follow-up issue on finding first/last cell addresses on explicitly defined, empty cells
- Code maintenance

## v1.8.4

---
Release Date: **20.03.2022**

- Fixed a regression bug, caused by changes of v1.8.3


## v1.8.3

---
Release Date: **10.03.2022**

- Added functions to determine the first cell address, column number or row number of a worksheet
- Adapted internal style handling
- Adapted the internal building of XML documents
- Fixed a bug in the handling of border colors


## v1.8.2

---
Release Date: **20.12.2021**

- Added hidden property for worksheets when loading a workbook

Note: The reader functionality on worksheets is not feature complete yet. Additional information like panes, splitting, column and row sizes are currently in development


## v1.8.1

---
Release Date: **12.09.2021**

- Fixed a bug when hiding worksheets

Note: It is not possible anymore to remove all worksheets from a workbook, or to set a hidden one as active. This would lead to an invalid Excel file

## v1.8.0

---
Release Date: **10.07.2021**

- Added functions to split (and freeze) a worksheet horizontally and vertically into panes
- Added a property to set the visibility of a workbook
- Added a property to set the visibility of worksheets
- Added two examples in the demo for the introduced split, freeze and visibility functionalities
- Added the possibility to define column widths and row height even if there are no cells defined
- Fixed the internal representation of column widths and row heights
- Minor code maintenance

Note: The column widths and row heights may change slightly with this release, since now the actual (internal) width and height is applied when setting a non-standard column width or row height

## v1.7.0

---
Release Date: **05.06.2021**

- Added functions to determine the last row, column or cell with data
- Fixed documentation formatting issues
- Updated readme and documentation

## v1.6.0

---
Release Date: **18.04.2021**

- Introduced library version for .NET Standard 2.0 (and assigned demos)
- Updated project structure (two projects for .NET >=4.5 and two for .NET Standard 2.0)
- Added function SetStyle in the Worksheet class
- Added demo for the new SetStyle function
- Changed behavior of empty cells. They are now not string but implicit numeric cells
- Added new function ResolveEnclosedAddresses in Range class
- Added new function GetAddressScope in Cell class
- Fixed the validation of cell addresses (single cell)
- Defined several immutable lists as return values to IReadOnlyList
- Minor code maintenance

Thanks to the following people (in the order of contribution date):

- Shobb for the introduction of IReadOnlyList
- John Lenz for the port to .NET Standard
- Ned Marinov for the proposal of the new SetStyle function

## v1.5.0

---
Release Date: **10.12.2020**

- Added indentation property of horizontal text alignment (CellXF) as style 
- Added example in demo for text indentation
- Code Cleanup

## v1.4.1

---
Release Date: **13.09.2020**

- Fixed a bug regarding numeric cells in the worksheet reader. Bug fix provided by John Lenz
- Minor code maintenance
- Updated readme and documentation

## v1.4.0

---
Release Date: **30.08.2020**

- Added style reader to resolve dates and times properly
- Added new data type TIME, represented by TimeSpan objects in reader and writer
- Changed namespace from 'Styles' to 'NanoXLSX.Styles'
- Added time (TimeSpan) examples to the demos
- Added a check to ensure dates are not set beyond 9999-12-31 (limitation of OAdate)
- Updated documentation
- Fixed some code formatting issues

### Notes

- To be consistent, the namespace of 'Styles' was changed to 'NanoXLSX.Styles'. Minor changes may be necessary in existing code if styles were used
- Currently, the style reader resolves only number formats to determine dates and times, as well as custom formats. Other components like fonts, borders or fills are neglected at the moment

## v1.3.6

---
Release Date: **19.07.2020**

- Fixed a bug in the reader regarding dates, times and booleans
- Fixed a bug in the method AddNextCellFormula

Note: Fixes provided by Silvio Burger and Thiago Souza. The fix for the reader bug is currently a work-around

## v1.3.5

---
Release Date: **10.01.2020**

- Fixed a bug in the reader regarding decimal numbers (for locales where the decimal pointer is not a dot)
- Formal changes

## v1.3.4

---
Release Date: **01.12.2019**

- Fixed a bug of reorganized worksheets (when deleted in Excel)
- Fixed a bug in the handling of shared strings

## v1.3.3

---
Release Date: **20.05.2019**

- Fixed a bug in the handling of streams (streams can be left open now)
- Updated stream demo
- Code Cleanup
- Removed executable folder, since executables are available through releases, compilation or NuGet

## v1.3.2

---
Release Date: **08.12.2018**

- Improved the performance of adding stylized cells by factor 10 to 100

## v1.3.1

---
Release Date: **04.11.2018**

- Fixed a bug in the style handling of merged cells. Bug fix provided by David Courtel for PicoXLSX

## v1.3.0

---
Release Date: **06.10.2018**

- Added missing features of PicoXLSX (synced with PicoXLSX version 2.6.1)
- Added asynchronous methods SaveAsync, SaveAsAsync, SaveAsStreamAsync and LoadAsync
- Added a new example for the introduced async methods
- Renamed namespace Exception to Exceptions
- Renamed namespace Style to Styles
- Fixed a bug regarding formulas in the reader
- Added support for dates in the reader
- Documentation Update
- Removed redundant code

## v1.2.4

---
Release Date: **24.08.2018**

- Fixed a bug regarding formulas in the reader
- Added support for dates in the reader
- Documentation Update

## v1.2.3

---
Release Date: **24.08.2018**

- Initial Release (synced to v 1.2.3 of NanoXLSX4j for Java)
