' ==============================================================================
' មេរៀនទី ៧.២៖ ការប្រើប្រាស់ Lambda Expressions និង Delegates (Func, Action, Predicate)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic

Module LambdaExpressionsDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. Single-Line Lambda Functions ===")
        ' Func(Of TInput, TResult)៖ Delegate ដែលទទួល Input ហើយប្រគល់ Output ត្រឡប់មកវិញ
        Dim square As Func(Of Integer, Integer) = Function(x) x * x
        Dim add As Func(Of Double, Double, Double) = Function(a, b) a + b
        Dim isEven As Func(Of Integer, Boolean) = Function(n) n Mod 2 = 0

        Console.WriteLine($"Square(6) = {square(6)}")
        Console.WriteLine($"Add(12.5, 7.5) = {add(12.5, 7.5)}")
        Console.WriteLine($"IsEven(10) = {isEven(10)}")
        Console.WriteLine($"IsEven(7)  = {isEven(7)}")

        Console.WriteLine()
        Console.WriteLine("=== ២. Multi-Line Lambda Functions ===")
        ' Multi-Line Lambda ប្រើនៅពេលដែលការគណនាមានច្រើនបន្ទាត់
        Dim calculateTax As Func(Of Decimal, Decimal) =
            Function(income As Decimal)
                If income <= 500.0D Then
                    Return 0.0D
                ElseIf income <= 2000.0D Then
                    Return income * 0.05D ' ពន្ធ 5%
                Else
                    Return income * 0.1D  ' ពន្ធ 10%
                End If
            End Function

        Console.WriteLine($"ពន្ធលើប្រាក់ចំណូល $400  : {calculateTax(400.0D):C2}")
        Console.WriteLine($"ពន្ធលើប្រាក់ចំណូល $1500 : {calculateTax(1500.0D):C2}")
        Console.WriteLine($"ពន្ធលើប្រាក់ចំណូល $3000 : {calculateTax(3000.0D):C2}")

        Console.WriteLine()
        Console.WriteLine("=== ៣. Lambda Sub (Action Delegate - គ្មាន Return Value) ===")
        ' Action(Of T) ប្រើសម្រាប់ប្រតិបត្តិការដែលមិនប្រគល់តម្លៃ (ដូចជា Sub)
        Dim printColoredMessage As Action(Of String, ConsoleColor) =
            Sub(msg, color)
                Console.ForegroundColor = color
                Console.WriteLine($"[*] {msg}")
                Console.ResetColor()
            End Sub

        printColoredMessage("សារជូនដំណឹងព័ត៌មានធម្មតា", ConsoleColor.Cyan)
        printColoredMessage("សារព្រមានប្រយ័ត្ន!", ConsoleColor.Yellow)
        printColoredMessage("សារកំហុសធ្ងន់ធ្ងរ!", ConsoleColor.Red)

        Console.WriteLine()
        Console.WriteLine("=== ៤. ការប្រើប្រាស់ Predicate(Of T) ជាមួយ List.FindAll & List.Exists ===")
        ' Predicate(Of T) គឺជា Function ដែលត្រឡប់តម្លៃ Boolean (True/False)
        Dim numbers As New List(Of Integer) From {12, 45, 8, 99, 24, 7, 60}

        ' ស្វែងរកលេខគូទាំងអស់តាម Lambda Predicate
        Dim evenNumbers As List(Of Integer) = numbers.FindAll(Function(n) n Mod 2 = 0)
        Console.WriteLine("លេខគូទាំងអស់ក្នុងបញ្ជី: " & String.Join(", ", evenNumbers))

        ' ស្វែងរកថាតើមានលេខធំជាង 50 ដែរឬទេ
        Dim hasBigNumber As Boolean = numbers.Exists(Function(n) n > 50)
        Console.WriteLine($"មានលេខធំជាង 50 ដែរឬទេ? {hasBigNumber}")

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
