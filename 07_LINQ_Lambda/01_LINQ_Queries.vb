' ==============================================================================
' មេរៀនទី ៧.១៖ ការសរសេរ LINQ Queries (Language Integrated Query)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.Linq

Public Class Trainee
    Public Property Id As Integer
    Public Property Name As String
    Public Property Gender As String
    Public Property Course As String
    Public Property Score As Double

    Public Sub New(id As Integer, name As String, gender As String, course As String, score As Double)
        Me.Id = id
        Me.Name = name
        Me.Gender = gender
        Me.Course = course
        Me.Score = score
    End Sub
End Class

Module LINQQueriesDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        ' បង្កើតបញ្ជីទិន្នន័យគំរូ
        Dim trainees As New List(Of Trainee)() From {
            New Trainee(1, "សុខ ចិន្តា", "ស្រី", "VB.NET", 92.5),
            New Trainee(2, "ជា ពិសិដ្ឋ", "ប្រុស", "C#", 48.0),
            New Trainee(3, "កែវ រដ្ឋា", "ប្រុស", "VB.NET", 76.0),
            New Trainee(4, "ម៉ី លីណា", "ស្រី", "SQL Server", 88.0),
            New Trainee(5, "អេង សុភាព", "ប្រុស", "C#", 65.5),
            New Trainee(6, "ចាន់ ធារ៉ា", "ស្រី", "VB.NET", 42.0),
            New Trainee(7, "ហុង ម៉េង", "ប្រុស", "SQL Server", 95.0)
        }

        Console.WriteLine("=== ១. LINQ Query Syntax ទល់នឹង Method Syntax (ការចម្រាញ់ Where) ===")

        ' ក. Query Syntax (ទម្រង់ស្រដៀង SQL)
        Dim passedQuery = From t In trainees
                          Where t.Score >= 50.0
                          Select t

        ' ខ. Method Syntax (ប្រើ Lambda Expression - ពេញនិយមជាង)
        Dim passedMethod = trainees.Where(Function(t) t.Score >= 50.0).ToList()

        Console.WriteLine("បញ្ជីសិក្ខាកាមដែលប្រឡងជាប់ (Score >= 50):")
        For Each t As Trainee In passedMethod
            Console.WriteLine($" -> {t.Name} ({t.Course}) : {t.Score} ពិន្ទុ")
        Next

        Console.WriteLine()
        Console.WriteLine("=== ២. ការតម្រៀបទិន្នន័យ (OrderBy & OrderByDescending) ===")
        ' តម្រៀបពិន្ទុពីខ្ពស់មកទាប
        Dim topScorers = trainees.OrderByDescending(Function(t) t.Score).ToList()

        Console.WriteLine("តារាងចំណាត់ថ្នាក់ពិន្ទុពីខ្ពស់ទៅទាប:")
        For rank As Integer = 0 To topScorers.Count - 1
            Dim t = topScorers(rank)
            Console.WriteLine($"  ចំណាត់ថ្នាក់ #{rank + 1}: {t.Name,-12} | {t.Course,-10} | {t.Score:F1} ពិន្ទុ")
        Next

        Console.WriteLine()
        Console.WriteLine("=== ៣. អនុគមន៍គណនាសរុប (Aggregates: Count, Sum, Average, Min, Max) ===")
        Dim totalCount As Integer = trainees.Count()
        Dim vbNetCount As Integer = trainees.Count(Function(t) t.Course = "VB.NET")
        Dim avgScore As Double = trainees.Average(Function(t) t.Score)
        Dim maxScore As Double = trainees.Max(Function(t) t.Score)
        Dim minScore As Double = trainees.Min(Function(t) t.Score)

        Console.WriteLine($"ចំនួនសិក្ខាកាមសរុប     : {totalCount} នាក់")
        Console.WriteLine($"ចំនួនអ្នករៀន VB.NET   : {vbNetCount} នាក់")
        Console.WriteLine($"មធ្យមភាគពិន្ទុទូទៅ      : {avgScore:F2}")
        Console.WriteLine($"ពិន្ទុខ្ពស់បំផុត (Max) : {maxScore:F2}")
        Console.WriteLine($"ពិន្ទុទាបបំផុត (Min)   : {minScore:F2}")

        Console.WriteLine()
        Console.WriteLine("=== ៤. ការទាញយកធាតុជាក់លាក់ (First, FirstOrDefault, Any) ===")
        ' រកសិស្សដែលមានពិន្ទុខ្ពស់ជាងគេ
        Dim topStudent = trainees.OrderByDescending(Function(t) t.Score).FirstOrDefault()
        If topStudent IsNot Nothing Then
            Console.WriteLine($"សិស្សពូកែជាងគេបង្អស់: {topStudent.Name} ({topStudent.Score} ពិន្ទុ)")
        End If

        ' ពិនិត្យមើលថាតើមានសិស្សធ្លាក់ (Score < 50) ដែរឬទេ?
        Dim hasFailedStudents As Boolean = trainees.Any(Function(t) t.Score < 50.0)
        Console.WriteLine($"តើមានសិស្សធ្លាក់ដែរឬទេ? {hasFailedStudents}")

        Console.WriteLine()
        Console.WriteLine("=== ៥. ការចងក្រងជាក្រុម (GroupBy) តាមមុខវិជ្ជា ===")
        Dim groupedByCourse = trainees.GroupBy(Function(t) t.Course)

        For Each group In groupedByCourse
            Console.WriteLine($"\n[មុខវិជ្ជា៖ {group.Key}] - ចំនួនសិស្ស: {group.Count()} នាក់ | មធ្យមភាគ: {group.Average(Function(x) x.Score):F2}")
            For Each member In group
                Console.WriteLine($"   * {member.Name} (ភេទ: {member.Gender}, ពិន្ទុ: {member.Score})")
            Next
        Next

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
