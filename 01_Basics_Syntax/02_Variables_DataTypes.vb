' ==============================================================================
' មេរៀនទី ១.២៖ ប្រភេទអថេរ និងទិន្នន័យ (Variables, Constants & Data Types)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System

Module VariablesAndDataTypes

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8 ' គាំទ្រអក្សរខ្មែរលើ Console

        Console.WriteLine("=== ១. ការប្រកាសអថេរ និងប្រភេទតម្លៃ (Data Types) ===")

        ' ១. ប្រភេទចំនួនគត់ (Integer Types)
        Dim age As Integer = 25                   ' ចំនួនគត់ទូទៅ 32-bit (-2.1 ពាន់លាន ទៅ 2.1 ពាន់លាន)
        Dim smallNumber As Byte = 255             ' ចំនួនគត់វិជ្ជមានតូច 8-bit (0 ទៅ 255)
        Dim shortNum As Short = 32000             ' ចំនួនគត់ 16-bit (-32,768 ទៅ 32,767)
        Dim bigNumber As Long = 987654321012345L  ' ចំនួនគត់ធំ 64-bit (ប្រើ L នៅខាងចុង)

        ' ២. ប្រភេទចំនួនទសភាគ (Floating Point & Decimal)
        Dim price As Single = 19.99F              ' ទសភាគកម្រិត 32-bit (ប្រើ F នៅខាងចុង)
        Dim distance As Double = 125.756          ' ទសភាគកម្រិត 64-bit (ច្បាស់ និងពេញនិយមបំផុត)
        Dim salary As Decimal = 1500.5D           ' ទសភាគកម្រិតខ្ពស់ 128-bit សម្រាប់រូបិយវត្ថុ/លុយកាក់ (ប្រើ D)

        ' ៣. ប្រភេទអក្សរ និងខ្សែអក្សរ (Character & String)
        Dim grade As Char = "A"c                  ' តួអក្សរតែមួយគត់ 16-bit Unicode (ប្រើ "..."c)
        Dim studentName As String = "សុខ សាន"     ' ខ្សែអក្សរ (String)

        ' ៤. ប្រភេទតក្កវិជ្ជា (Boolean)
        Dim isStudentActive As Boolean = True     ' តម្លៃពិត (True) ឬ មិនពិត (False)

        ' ៥. ប្រភេទកាលបរិច្ឆេទ និងពេលវេលា (DateTime)
        Dim birthDate As Date = #2000-12-25#      ' កាលបរិច្ឆេទ (ប្រើសញ្ញា # កៀបសងខាង)
        Dim currentDateTime As DateTime = DateTime.Now

        ' ៦. អថេរថេរ (Constants - តម្លៃមិនអាចកែប្រែបានក្រោយប្រកាស)
        Const PI As Double = 3.14159265358979
        Const SCHOOL_NAME As String = "PCCFP Institute"

        ' បង្ហាញលទ្ធផល
        Console.WriteLine($"ឈ្មោះសិស្ស: {studentName}")
        Console.WriteLine($"អាយុ: {age} ឆ្នាំ")
        Console.WriteLine($"ពិន្ទុ: {grade}")
        Console.WriteLine($"ប្រាក់ខែ: {salary:C2}")
        Console.WriteLine($"ស្ថានភាពសកម្ម: {isStudentActive}")
        Console.WriteLine($"ថ្ងៃខែឆ្នាំកំណើត: {birthDate:dd/MM/yyyy}")
        Console.WriteLine($"ពេលវេលាបច្ចុប្បន្ន: {currentDateTime:yyyy-MM-dd HH:mm:ss}")
        Console.WriteLine($"សាលា: {SCHOOL_NAME}, តម្លៃ PI: {PI}")

        Console.WriteLine()
        Console.WriteLine("=== ២. ការបម្លែងប្រភេទតម្លៃ (Type Conversion / Casting) ===")

        ' ក. ការបម្លែងតាមរយៈ Helper Functions (CInt, CDbl, CStr, CBool, etc.)
        Dim strNumber As String = "150"
        Dim convertedInt As Integer = CInt(strNumber)      ' បម្លែង String ទៅ Integer
        Dim convertedDbl As Double = CDbl("45.67")         ' បម្លែង String ទៅ Double
        Dim numToString As String = CStr(12345)            ' បម្លែង Integer ទៅ String

        Console.WriteLine($"CInt(""150"") + 50 = {convertedInt + 50}")
        Console.WriteLine($"CDbl(""45.67"") = {convertedDbl}")
        Console.WriteLine($"CStr(12345) = {numToString}")

        ' ខ. ការប្រើប្រាស់ Class Convert (មានសុវត្ថិភាព និងទូលំទូលាយ)
        Dim textAmount As String = "250.75"
        Dim decimalAmount As Decimal = Convert.ToDecimal(textAmount)
        Console.WriteLine($"Convert.ToDecimal: {decimalAmount}")

        ' គ. ការប្រើប្រាស់ TryParse (វិធីសាស្រ្តល្អបំផុតដើម្បីការពារ Runtime Error ពេល Input ខុស)
        Dim userInput As String = "450a" ' តម្លៃមានអក្សរលាយ ដែលមិនអាចបម្លែងបាន
        Dim parsedResult As Integer

        ' TryParse នឹងប្រគល់តម្លៃ True បើបម្លែងបានជោគជ័យ និង False បើមានកំហុស
        If Integer.TryParse(userInput, parsedResult) Then
            Console.WriteLine($"ការបម្លែងជោគជ័យ! តម្លៃគឺ: {parsedResult}")
        Else
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine($"កំហុស៖ មិនអាចបម្លែង '{userInput}' ទៅជាចំនួនគត់បានឡើយ!")
            Console.ResetColor()
        End If

        Console.WriteLine()
        Console.WriteLine("=== ៣. អថេរដែលអាចផ្ទុកតម្លៃ Null (Nullable Types) ===")
        ' ធម្មតា Integer មិនអាចស្មើ Nothing/Null បានទេ ប៉ុន្តែបើប្រើ ? គឺអាចផ្ទុក Nothing បាន
        Dim optionalScore As Integer? = Nothing

        If optionalScore.HasValue Then
            Console.WriteLine($"ពិន្ទុមានតម្លៃ: {optionalScore.Value}")
        Else
            Console.WriteLine("ពិន្ទុមិនទាន់បានបញ្ចូលនៅឡើយទេ (Null/Nothing)!")
        End If

        optionalScore = 85
        If optionalScore.HasValue Then
            Console.WriteLine($"ក្រោយបញ្ចូលពិន្ទុ: {optionalScore.Value}")
        End If

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
