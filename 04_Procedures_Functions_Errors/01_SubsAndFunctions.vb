' ==============================================================================
' មេរៀនទី ៤.១៖ អនុគមន៍ និងនីតិវិធី (Subs, Functions, ByVal, ByRef, Overloading)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System

Module SubsAndFunctionsDemo

    ' ១. Sub Procedure: ដំណើរការកូដតែមិនប្រគល់តម្លៃត្រឡប់មកវិញទេ (No Return Value)
    Sub ShowWelcomeBanner(schoolName As String)
        Console.WriteLine("==================================================")
        Console.WriteLine($"    ស្វាគមន៍មកកាន់ការសិក្សា {schoolName}   ")
        Console.WriteLine("==================================================")
    End Sub

    ' ២. Function: ដំណើរការកូដ និងប្រគល់តម្លៃត្រឡប់មកវិញ (Returns a Value)
    Function CalculateAverage(score1 As Double, score2 As Double, score3 As Double) As Double
        Dim total As Double = score1 + score2 + score3
        Dim average As Double = total / 3.0
        Return average ' ឬ CalculateAverage = average
    End Function

    ' ៣. ByVal (Pass by Value): បញ្ជូនតម្លៃចម្លង (តម្លៃដើមក្រៅ Sub មិនប្រែប្រួលទេ)
    Sub IncreaseByVal(ByVal x As Integer)
        x += 10
        Console.WriteLine($"  [ក្នុង IncreaseByVal]: x = {x}")
    End Sub

    ' ៤. ByRef (Pass by Reference): បញ្ជូនអាសយដ្ឋានអង្គចងចាំ (តម្លៃដើមក្រៅ Sub នឹងប្រែប្រួលតាម)
    Sub IncreaseByRef(ByRef x As Integer)
        x += 10
        Console.WriteLine($"  [ក្នុង IncreaseByRef]: x = {x}")
    End Sub

    ' ៥. Optional Parameters: ប៉ារ៉ាម៉ែត្រជម្រើស (បើមិនបញ្ចូល នឹងយក Default Value)
    Sub GreetPerson(name As String, Optional title As String = "លោក/អ្នកនាង")
        Console.WriteLine($"សួស្តី {title} {name}!")
    End Sub

    ' ៦. ParamArray: អាចទទួលតម្លៃ Parameter ចំនួនប៉ុន្មានក៏បានជា Array
    Function SumAll(ParamArray numbers() As Double) As Double
        Dim total As Double = 0.0
        For Each num As Double In numbers
            total += num
        Next
        Return total
    End Function

    ' ៧. Method Overloading: អនុគមន៍ឈ្មោះដូចគ្នា តែ Parameter ខុសគ្នា (Overloads)
    Overloads Sub DisplayInfo(text As String)
        Console.WriteLine($"[ព័ត៌មានជាអក្សរ]: {text}")
    End Sub

    Overloads Sub DisplayInfo(number As Integer)
        Console.WriteLine($"[ព័ត៌មានជាលេខ]: {number}")
    End Sub

    Overloads Sub DisplayInfo(name As String, age As Integer)
        Console.WriteLine($"[ព័ត៌មានសិស្ស]: ឈ្មោះ {name}, អាយុ {age} ឆ្នាំ")
    End Sub

    ' ៨. Recursive Function: អនុគមន៍ដែលហៅខ្លួនឯង (ឧទាហរណ៍៖ គណនា Factorial n!)
    Function Factorial(n As Integer) As Long
        If n <= 1 Then
            Return 1
        Else
            Return n * Factorial(n - 1)
        End If
    End Function

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        ' តេស្ត Sub និង Function
        ShowWelcomeBanner("វិទ្យាស្ថាន PCCFP")

        Dim avg As Double = CalculateAverage(85.5, 90.0, 78.0)
        Console.WriteLine($"មធ្យមភាគពិន្ទុ: {avg:F2}")

        Console.WriteLine()
        Console.WriteLine("=== ការប្រៀបធៀប ByVal និង ByRef ===")
        Dim myNumber As Integer = 50

        Console.WriteLine($"តម្លៃដើមមុនហៅ ByVal: {myNumber}")
        IncreaseByVal(myNumber)
        Console.WriteLine($"តម្លៃក្រោយហៅ ByVal: {myNumber} (នៅរក្សាតម្លៃ 50 ដដែល)")

        Console.WriteLine()
        Console.WriteLine($"តម្លៃដើមមុនហៅ ByRef: {myNumber}")
        IncreaseByRef(myNumber)
        Console.WriteLine($"តម្លៃក្រោយហៅ ByRef: {myNumber} (តម្លៃកើនឡើងដល់ 60!)")

        Console.WriteLine()
        Console.WriteLine("=== តេស្ត Optional Parameter ===")
        GreetPerson("សុខា")                  ' ប្រើ Default title
        GreetPerson("ចិន្តា", "កញ្ញា")          ' កំណត់ title ផ្ទាល់

        Console.WriteLine()
        Console.WriteLine("=== តេស្ត ParamArray (បូកលេខប៉ុន្មានក៏បាន) ===")
        Dim total1 As Double = SumAll(10.5, 20.5)
        Dim total2 As Double = SumAll(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)
        Console.WriteLine($"SumAll(10.5, 20.5) = {total1}")
        Console.WriteLine($"SumAll(1..10) = {total2}")

        Console.WriteLine()
        Console.WriteLine("=== តេស្ត Function Overloading ===")
        DisplayInfo("ភាសា VB.NET")
        DisplayInfo(2026)
        DisplayInfo("ដារ៉ា", 22)

        Console.WriteLine()
        Console.WriteLine("=== តេស្ត Recursion (Factorial) ===")
        Dim fact5 As Long = Factorial(5) ' 5! = 5 * 4 * 3 * 2 * 1 = 120
        Console.WriteLine($"Factorial នៃ 5! = {fact5}")

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
