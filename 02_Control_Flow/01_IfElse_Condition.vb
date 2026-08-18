' ==============================================================================
' មេរៀនទី ២.១៖ ការប្រើប្រាស់លក្ខខណ្ឌ If...Then...Else (Conditional Statements)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System

Module IfElseConditionDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. លក្ខខណ្ឌ If...Then សាមញ្ញ ===")
        Dim score As Double = 85.0

        If score >= 50.0 Then
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine($"ពិន្ទុ {score}: អ្នកបានប្រឡងជាប់!")
            Console.ResetColor()
        End If

        Console.WriteLine()
        Console.WriteLine("=== ២. លក្ខខណ្ឌ If...Then...Else ===")
        Dim age As Integer = 16

        If age >= 18 Then
            Console.WriteLine("អ្នកមានសិទ្ធិបោះឆ្នោតបាន (Eligible to vote)។")
        Else
            Console.WriteLine($"អ្នកមានអាយុត្រឹមតែ {age} ឆ្នាំ មិនទាន់គ្រប់អាយុបោះឆ្នោតទេ។")
        End If

        Console.WriteLine()
        Console.WriteLine("=== ៣. លក្ខខណ្ឌពហុជម្រើស If...ElseIf...Else (ការគណនានិទ្ទេស Grading) ===")
        Dim studentScore As Double = 87.5
        Dim grade As String

        If studentScore >= 90 Then
            grade = "A (ល្អប្រសើរណាស់ - Excellent)"
        ElseIf studentScore >= 80 Then
            grade = "B (ល្អណាស់ - Very Good)"
        ElseIf studentScore >= 70 Then
            grade = "C (ល្អ - Good)"
        ElseIf studentScore >= 60 Then
            grade = "D (មធ្យម - Fair)"
        ElseIf studentScore >= 50 Then
            grade = "E (ខ្សោយ - Poor)"
        Else
            grade = "F (ធ្លាក់ - Fail)"
        End If

        Console.WriteLine($"ពិន្ទុ: {studentScore} => និទ្ទេស: {grade}")

        Console.WriteLine()
        Console.WriteLine("=== ៤. លក្ខខណ្ឌជាន់គ្នា (Nested If...Else) ===")
        Dim isMember As Boolean = True
        Dim purchaseAmount As Decimal = 120.0D
        Dim discount As Decimal = 0.0D

        If isMember Then
            If purchaseAmount >= 100.0D Then
                discount = 0.2D ' បញ្ចុះតម្លៃ 20%
                Console.WriteLine("អតិថិជនសមាជិក ទិញចាប់ពី $100 ឡើងទៅ => បញ្ចុះតម្លៃ 20%")
            Else
                discount = 0.1D ' បញ្ចុះតម្លៃ 10%
                Console.WriteLine("អតិថិជនសមាជិក ទិញក្រោម $100 => បញ្ចុះតម្លៃ 10%")
            End If
        Else
            If purchaseAmount >= 100.0D Then
                discount = 0.05D ' បញ្ចុះតម្លៃ 5%
                Console.WriteLine("អតិថិជនទូទៅ ទិញចាប់ពី $100 ឡើងទៅ => បញ្ចុះតម្លៃ 5%")
            Else
                discount = 0.0D
                Console.WriteLine("អតិថិជនទូទៅ => មិនមានការបញ្ចុះតម្លៃទេ")
            End If
        End If

        Dim finalPrice As Decimal = purchaseAmount - (purchaseAmount * discount)
        Console.WriteLine($"តម្លៃដើម: {purchaseAmount:C2} | បញ្ចុះតម្លៃ: {discount:P0} | តម្លៃត្រូវទូទាត់: {finalPrice:C2}")

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
