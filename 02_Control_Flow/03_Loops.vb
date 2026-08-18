' ==============================================================================
' មេរៀនទី ២.៣៖ រង្វិលជុំ (Loops & Iterations in VB.NET)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System

Module LoopsDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. រង្វិលជុំ For...Next (រាប់ចំនួនដងកំណត់ទុកជាមុន) ===")
        ' រាប់ពី 1 ទៅ 5
        Console.Write("រាប់ពី 1 ដល់ 5: ")
        For i As Integer = 1 To 5
            Console.Write($"{i} ")
        Next
        Console.WriteLine()

        ' រាប់រំលងជំហាន (Step 2)
        Console.Write("រាប់លេខសេស 1 ដល់ 10 (Step 2): ")
        For i As Integer = 1 To 10 Step 2
            Console.Write($"{i} ")
        Next
        Console.WriteLine()

        ' រាប់ថយក្រោយ (Step -1)
        Console.Write("រាប់ថយក្រោយ 5 ដល់ 1 (Step -1): ")
        For i As Integer = 5 To 1 Step -1
            Console.Write($"{i} ")
        Next
        Console.WriteLine()

        Console.WriteLine()
        Console.WriteLine("=== ២. រង្វិលជុំ For Each...Next (រត់កាត់តាមធាតុក្នុងបណ្តុំ ឬ Array) ===")
        Dim subjects As String() = {"VB.NET", "C#", "SQL Server", "Web Development"}

        Console.WriteLine("មុខវិជ្ជាកំពុងសិក្សា៖")
        For Each subject As String In subjects
            Console.WriteLine($" -> {subject}")
        Next

        Console.WriteLine()
        Console.WriteLine("=== ៣. រង្វិលជុំ While...End While (រត់ដរាបណាត្រូវលក្ខខណ្ឌ) ===")
        Dim count As Integer = 1
        Console.Write("While Loop (1 ដល់ 4): ")
        While count <= 4
            Console.Write($"{count} ")
            count += 1 ' បង្កើនតម្លៃ count ដើម្បីកុំឱ្យជាប់ Infinite Loop
        End While
        Console.WriteLine()

        Console.WriteLine()
        Console.WriteLine("=== ៤. រង្វិលជុំ Do While...Loop និង Do...Loop While ===")
        ' ក. Do While...Loop (ពិនិត្យលក្ខខណ្ឌមុនដំណើរការ)
        Dim num1 As Integer = 1
        Console.Write("Do While Loop: ")
        Do While num1 <= 3
            Console.Write($"{num1} ")
            num1 += 1
        Loop
        Console.WriteLine()

        ' ខ. Do...Loop While (ដំណើរការយ៉ាងហោចណាស់ម្តង សឹមពិនិត្យលក្ខខណ្ឌ)
        Dim num2 As Integer = 10
        Console.Write("Do...Loop While (ទោះលក្ខខណ្ឌខុស ក៏ដំណើរការបាន ១ ដងដែរ): ")
        Do
            Console.Write($"តម្លៃគឺ {num2} ")
            num2 += 1
        Loop While num2 < 5
        Console.WriteLine()

        Console.WriteLine()
        Console.WriteLine("=== ៥. រង្វិលជុំ Do Until...Loop (រត់រហូតដល់លក្ខខណ្ឌក្លាយជា True) ===")
        Dim batteryLevel As Integer = 85
        Console.Write("កំពុងសាកថ្ម (Do Until battery = 100): ")
        Do Until batteryLevel >= 100
            batteryLevel += 5
            Console.Write($"{batteryLevel}% ")
        Loop
        Console.WriteLine("-> ថ្មពេញហើយ!")

        Console.WriteLine()
        Console.WriteLine("=== ៦. ការប្រើប្រាស់ Exit For និង Continue For ===")
        Console.Write("រាប់ 1 ដល់ 10 (រំលងលេខ 3 ដោយ Continue, ឈប់ត្រឹមលេខ 7 ដោយ Exit): ")
        For i As Integer = 1 To 10
            If i = 3 Then
                Continue For ' រំលងជុំនេះ មិនធ្វើកូដខាងក្រោមទេ
            End If

            If i = 7 Then
                Exit For     ' បញ្ឈប់រង្វិលជុំភ្លាមៗ
            End If

            Console.Write($"{i} ")
        Next
        Console.WriteLine()

        Console.WriteLine()
        Console.WriteLine("=== ៧. រង្វិលជុំជាន់គ្នា (Nested Loops) - តារាងមេគុណ ២ ដល់ ៤ ===")
        For row As Integer = 2 To 4
            Console.WriteLine($"--- មេគុណ {row} ---")
            For col As Integer = 1 To 5
                Console.WriteLine($"{row} x {col} = {row * col}")
            Next
        Next

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
