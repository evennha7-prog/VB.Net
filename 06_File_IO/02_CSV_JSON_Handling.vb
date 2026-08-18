' ==============================================================================
' មេរៀនទី ៦.២៖ ការគ្រប់គ្រងឯកសារ CSV និង JSON (CSV & JSON Handling)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Text

' គំរូទិន្នន័យផលិតផល (Product Model)
Public Class Product
    Public Property Id As Integer
    Public Property Name As String
    Public Property Category As String
    Public Property UnitPrice As Decimal
    Public Property QuantityInStock As Integer

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, name As String, category As String, price As Decimal, qty As Integer)
        Me.Id = id
        Me.Name = name
        Me.Category = category
        Me.UnitPrice = price
        Me.QuantityInStock = qty
    End Sub

    ' បម្លែងទៅជាទម្រង់ CSV Row: "Id,Name,Category,UnitPrice,QuantityInStock"
    Public Function ToCsvRow() As String
        Return $"{Id},{EscapeCsv(Name)},{EscapeCsv(Category)},{UnitPrice},{QuantityInStock}"
    End Function

    ' បម្លែងទៅជាទម្រង់ JSON Object String
    Public Function ToJsonString() As String
        Return $"{{""id"": {Id}, ""name"": ""{Name}"", ""category"": ""{Category}"", ""unitPrice"": {UnitPrice}, ""qty"": {QuantityInStock}}}"
    End Function

    Private Shared Function EscapeCsv(field As String) As String
        If field.Contains(",") OrElse field.Contains("""") Then
            Return $"""{field.Replace("""", """""")}"""
        End If
        Return field
    End Function
End Class

Module CsvJsonHandlingDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Dim baseDir As String = AppDomain.CurrentDomain.BaseDirectory
        Dim csvFilePath As String = Path.Combine(baseDir, "Products.csv")
        Dim jsonFilePath As String = Path.Combine(baseDir, "Products.json")

        ' បង្កើតទិន្នន័យគំរូ
        Dim products As New List(Of Product)() From {
            New Product(1, "កុំព្យូទ័រយួរដៃ Dell XPS 15", "Electronics", 1299.99D, 10),
            New Product(2, "ក្តារចុច Mechanical Keyboard", "Accessories", 79.5D, 25),
            New Product(3, "ម៉ូនីទ័រ LG 27 Inch 4K", "Electronics", 349.0D, 15),
            New Product(4, "កៅអី Ergonomic Chair", "Furniture", 199.0D, 8)
        }

        Console.WriteLine("=== ១. ការសរសេរ និងអានឯកសារ CSV (Comma Separated Values) ===")

        ' ក. សរសេរចេញជា CSV File
        Using writer As New StreamWriter(csvFilePath, False, Encoding.UTF8)
            ' Header Row
            writer.WriteLine("ID,ProductName,Category,UnitPrice,QuantityInStock")
            ' Data Rows
            For Each p As Product In products
                writer.WriteLine(p.ToCsvRow())
            Next
        End Using
        Console.WriteLine($"[ជោគជ័យ]: បាននាំចេញទិន្នន័យទៅកាន់ '{Path.GetFileName(csvFilePath)}'")

        ' ខ. អានទិន្នន័យពី CSV File មកវិញ
        Dim importedProducts As New List(Of Product)()
        Dim lines As String() = File.ReadAllLines(csvFilePath, Encoding.UTF8)

        ' ចាប់ផ្តើមពីបន្ទាត់ទី 1 ដើម្បីរំលង Header (Index 0)
        For i As Integer = 1 To lines.Length - 1
            Dim line As String = lines(i).Trim()
            If Not String.IsNullOrEmpty(line) Then
                Dim parts As String() = line.Split(","c)
                If parts.Length >= 5 Then
                    Dim prod As New Product()
                    prod.Id = Convert.ToInt32(parts(0))
                    prod.Name = parts(1).Replace("""", "")
                    prod.Category = parts(2).Replace("""", "")
                    prod.UnitPrice = Convert.ToDecimal(parts(3))
                    prod.QuantityInStock = Convert.ToInt32(parts(4))
                    importedProducts.Add(prod)
                End If
            End If
        Next

        Console.WriteLine($"\n--- បង្ហាញទិន្នន័យអានបានពី CSV ({importedProducts.Count} មុខទំនិញ) ---")
        For Each p As Product In importedProducts
            Console.WriteLine($"  #{p.Id,-2} | {p.Name,-30} | {p.Category,-12} | {p.UnitPrice,8:C2} | ស្តុក: {p.QuantityInStock}")
        Next

        Console.WriteLine()
        Console.WriteLine("=== ២. ការបង្កើត និងរក្សាទុកទម្រង់ JSON ===")

        ' បង្កើត JSON Array String
        Dim sbJson As New StringBuilder()
        sbJson.AppendLine("[")
        For i As Integer = 0 To products.Count - 1
            sbJson.Append("  " & products(i).ToJsonString())
            If i < products.Count - 1 Then sbJson.Append(",")
            sbJson.AppendLine()
        Next
        sbJson.AppendLine("]")

        File.WriteAllText(jsonFilePath, sbJson.ToString(), Encoding.UTF8)
        Console.WriteLine($"[ជោគជ័យ]: បានរក្សាទុកទិន្នន័យ JSON ទៅកាន់ '{Path.GetFileName(jsonFilePath)}'")

        ' បង្ហាញខ្លឹមសារ JSON
        Console.WriteLine("\n--- ខ្លឹមសារ JSON File ---")
        Console.WriteLine(File.ReadAllText(jsonFilePath, Encoding.UTF8))

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
