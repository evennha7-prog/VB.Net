' ==============================================================================
' មេរៀនទី ៣.៣៖ ការរៀបចំ និងកែច្នៃអត្ថបទ (String Manipulation & StringBuilder)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Text

Module StringManipulationDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. អនុគមន៍មូលដ្ឋានលើខ្សែអក្សរ (String Functions) ===")
        Dim message As String = "  សួស្តីនិស្សិត VB.NET ទាំងអស់គ្នា!  "

        ' ប្រវែងអក្សរ (Length)
        Console.WriteLine($"ប្រវែងអត្ថបទដើម: {message.Length} តួអក្សរ")

        ' កាត់ចន្លោះទំនេរឆ្វេងស្តាំ (Trim, TrimStart, TrimEnd)
        Dim cleanMessage As String = message.Trim()
        Console.WriteLine($"ក្រោយ Trim: '{cleanMessage}' (ប្រវែង: {cleanMessage.Length})")

        ' អក្សរធំ និង អក្សរតូច (ToUpper, ToLower)
        Dim engText As String = "Hello World From Cambodia"
        Console.WriteLine($"ToUpper: {engText.ToUpper()}")
        Console.WriteLine($"ToLower: {engText.ToLower()}")

        ' ស្វែងរកទីតាំងអក្សរ (IndexOf, Contains, StartsWith, EndsWith)
        Console.WriteLine($"Contains 'VB.NET': {cleanMessage.Contains("VB.NET")}")
        Console.WriteLine($"StartsWith 'សួស្តី': {cleanMessage.StartsWith("សួស្តី")}")
        Console.WriteLine($"IndexOf 'VB.NET': {cleanMessage.IndexOf("VB.NET")}")

        ' កាត់យកផ្នែកខ្លះនៃអត្ថបទ (Substring)
        ' Syntax: Substring(startIndex, length)
        Dim sampleText As String = "Programming in VB.NET"
        Dim subStr As String = sampleText.Substring(15, 6) ' កាត់យកពាក្យ "VB.NET"
        Console.WriteLine($"Substring(15, 6) នៃ '{sampleText}' គឺ៖ '{subStr}'")

        ' ជំនួសអក្សរ (Replace)
        Dim replacedText As String = sampleText.Replace("VB.NET", "VB 2026")
        Console.WriteLine($"ក្រោយពេល Replace: '{replacedText}'")

        Console.WriteLine()
        Console.WriteLine("=== ២. ការបំបែក និងផ្គុំអត្ថបទ (Split & String.Join) ===")
        Dim csvData As String = "ផ្លែប៉ោម,ផ្លែចេក,ផ្លែស្វាយ,ផ្លែទុរេន,ផ្លែដូង"

        ' Split៖ បំបែក String ទៅជា Array តាមសញ្ញាក្បៀស (,)
        Dim fruitArray As String() = csvData.Split(","c)

        Console.WriteLine("លទ្ធផលក្រោយ Split:")
        For i As Integer = 0 To fruitArray.Length - 1
            Console.WriteLine($"  [{i}] {fruitArray(i)}")
        Next

        ' String.Join៖ ផ្គុំ Array មកជា String វិញដោយភ្ជាប់ដោយសញ្ញា "-"
        Dim joinedString As String = String.Join(" - ", fruitArray)
        Console.WriteLine($"ក្រោយ String.Join: {joinedString}")

        Console.WriteLine()
        Console.WriteLine("=== ៣. ការផ្ទៀងផ្ទាត់ String ទទេ ឬ Null (Validation) ===")
        Dim emptyStr As String = ""
        Dim spaceStr As String = "   "
        Dim nullStr As String = Nothing

        Console.WriteLine($"IsNullOrEmpty(emptyStr): {String.IsNullOrEmpty(emptyStr)}")
        Console.WriteLine($"IsNullOrWhiteSpace(spaceStr): {String.IsNullOrWhiteSpace(spaceStr)}")
        Console.WriteLine($"IsNullOrEmpty(nullStr): {String.IsNullOrEmpty(nullStr)}")

        Console.WriteLine()
        Console.WriteLine("=== ៤. ការប្រើប្រាស់ StringBuilder (សម្រាប់ភ្ជាប់អក្សរច្រើនក្នុង Loop) ===")
        ' ការប្រើសញ្ញា & ក្នុង Loop រាប់ពាន់ដងនាំឱ្យស៊ី Memory ច្រើន និងដំណើរការយឺត។
        ' StringBuilder ជួយសន្សំ Memory និងដំណើរការលឿនជាងរាប់សិបដង។
        Dim sb As New StringBuilder()

        sb.Append("របាយការណ៍សង្ខេប៖")
        sb.AppendLine() ' ចុះបន្ទាត់ថ្មី
        For i As Integer = 1 To 5
            sb.AppendLine($"  + ចំណុចទី {i}: មេរៀនកម្រិតទី {i}")
        Next

        Console.WriteLine(sb.ToString())

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
