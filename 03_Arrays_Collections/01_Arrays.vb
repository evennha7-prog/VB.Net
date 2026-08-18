' ==============================================================================
' មេរៀនទី ៣.១៖ ការប្រើប្រាស់អារ៉េ (Arrays in VB.NET)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System

Module ArraysDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. ការប្រកាស និងកំណត់តម្លៃ Array មួយវិមាត្រ (1D Array) ===")
        ' ចំណាំសំខាន់ក្នុង VB.NET៖ Dim arr(4) មានន័យថា Index ចាប់ពី 0 ដល់ 4 (មាន 5 ធាតុ!)
        Dim fruits(3) As String ' មាន 4 ធាតុ (Index: 0, 1, 2, 3)
        fruits(0) = "ផ្លែប៉ោម (Apple)"
        fruits(1) = "ផ្លែចេក (Banana)"
        fruits(2) = "ផ្លែក្រូច (Orange)"
        fruits(3) = "ផ្លែស្វាយ (Mango)"

        ' ការប្រកាស និងដាក់តម្លៃភ្លាមៗ (Inline Initialization)
        Dim numbers() As Integer = {45, 12, 89, 34, 7}

        Console.WriteLine($"ចំនួនធាតុក្នុង fruits: {fruits.Length}")
        Console.WriteLine($"ធាតុទី 1 (Index 0): {fruits(0)}")

        Console.WriteLine()
        Console.WriteLine("=== ២. ការរត់កាត់តាមធាតុក្នុង Array ===")
        ' ក. ប្រើប្រាស់ For Loop ជាមួយ Length ឬ GetUpperBound(0)
        Console.WriteLine("បង្ហាញផ្លែឈើតាមរយៈ For Loop:")
        For i As Integer = 0 To fruits.GetUpperBound(0)
            Console.WriteLine($"  [Index {i}] = {fruits(i)}")
        Next

        ' ខ. ប្រើប្រាស់ For Each Loop
        Console.WriteLine("បង្ហាញលេខតាមរយៈ For Each Loop:")
        For Each num As Integer In numbers
            Console.Write($"{num} ")
        Next
        Console.WriteLine()

        Console.WriteLine()
        Console.WriteLine("=== ៣. Function និង Method សំខាន់ៗលើ Array ===")
        ' តម្រៀបលេខពីតូចទៅធំ (Sorting)
        Array.Sort(numbers)
        Console.Write("ក្រោយពេល Sort (ពីតូចទៅធំ): ")
        For Each num As Integer In numbers
            Console.Write($"{num} ")
        Next
        Console.WriteLine()

        ' បញ្ច្រាសលំដាប់ធាតុ (Reverse)
        Array.Reverse(numbers)
        Console.Write("ក្រោយពេល Reverse (បញ្ច្រាស): ")
        For Each num As Integer In numbers
            Console.Write($"{num} ")
        Next
        Console.WriteLine()

        ' ស្វែងរកទីតាំង Index នៃធាតុណាមួយ (IndexOf)
        Dim searchVal As Integer = 34
        Dim foundIndex As Integer = Array.IndexOf(numbers, searchVal)
        Console.WriteLine($"លេខ {searchVal} ស្ថិតនៅ Index: {foundIndex}")

        Console.WriteLine()
        Console.WriteLine("=== ៤. អារ៉េដែលអាចប្តូរទំហំបាន (Dynamic Array - ReDim & ReDim Preserve) ===")
        Dim dynamicList() As String = {"កម្ពុជា", "ថៃ", "ឡាវ"}
        Console.WriteLine($"ទំហំដើម: {dynamicList.Length} ប្រទេស")

        ' ReDim Preserve៖ ប្តូរទំហំ Array តែរក្សាទុកទិន្នន័យចាស់ៗដដែល
        ReDim Preserve dynamicList(4) ' ពង្រីកពី 3 ធាតុទៅ 5 ធាតុ (Index 0 ដល់ 4)
        dynamicList(3) = "វៀតណាម"
        dynamicList(4) = "មីយ៉ាន់ម៉ា"

        Console.WriteLine("បញ្ជីប្រទេសក្រោយពង្រីក (ReDim Preserve):")
        For Each country As String In dynamicList
            Console.WriteLine($" - {country}")
        Next

        Console.WriteLine()
        Console.WriteLine("=== ៥. អារ៉េពីរវិមាត្រ (2D Array - Matrix) ===")
        ' បង្កើតតារាង ២ ជួរដេក (Rows: 0..1) និង ៣ ជួរឈរ (Cols: 0..2)
        Dim matrix(1, 2) As Integer
        matrix(0, 0) = 10 : matrix(0, 1) = 20 : matrix(0, 2) = 30
        matrix(1, 0) = 40 : matrix(1, 1) = 50 : matrix(1, 2) = 60

        Console.WriteLine("ទិន្នន័យក្នុង Matrix 2x3:")
        For row As Integer = 0 To matrix.GetUpperBound(0)
            For col As Integer = 0 To matrix.GetUpperBound(1)
                Console.Write($"{matrix(row, col),5}\t")
            Next
            Console.WriteLine()
        Next

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
