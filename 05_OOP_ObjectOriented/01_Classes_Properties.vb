' ==============================================================================
' មេរៀនទី ៥.១៖ មូលដ្ឋានគ្រឹះ OOP (Classes, Objects, Properties & Constructors)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System

' កំណត់ Class សិស្ស (Student)
Public Class Student

    ' ១. Private Fields (ទិន្នន័យផ្ទៃក្នុងដែលត្រូវលាក់)
    Private _id As Integer
    Private _name As String
    Private _score As Double

    ' ២. Auto-Implemented Property (Property សង្ខេបសម្រាប់ទិន្នន័យធម្មតា)
    Public Property Major As String
    Public Property Email As String

    ' ៣. Full Property ជាមួយ Get និង Set (មាន Logic ផ្ទៀងផ្ទាត់ Validation មុនអនុញ្ញាតឱ្យបញ្ចូល)
    Public Property Id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            If value <= 0 Then
                Throw New ArgumentException("ID សិស្សត្រូវតែជាលេខវិជ្ជមានធំជាង 0!")
            End If
            _id = value
        End Set
    End Property

    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            If String.IsNullOrWhiteSpace(value) Then
                Throw New ArgumentException("ឈ្មោះសិស្សមិនអាចទទេបានឡើយ!")
            End If
            _name = value.Trim()
        End Set
    End Property

    Public Property Score As Double
        Get
            Return _score
        End Get
        Set(value As Double)
            If value < 0.0 OrElse value > 100.0 Then
                Throw New ArgumentOutOfRangeException("value", "ពិន្ទុត្រូវតែនៅចន្លោះ 0 ដល់ 100!")
            End If
            _score = value
        End Set
    End Property

    ' Read-Only Property (អាចមើលបានតែមិនអាចកែប្រែផ្ទាល់ពីក្រៅបានទេ)
    Public ReadOnly Property Grade As String
        Get
            Select Case _score
                Case Is >= 90 : Return "A"
                Case Is >= 80 : Return "B"
                Case Is >= 70 : Return "C"
                Case Is >= 60 : Return "D"
                Case Is >= 50 : Return "E"
                Case Else : Return "F"
            End Select
        End Get
    End Property

    ' ៤. Constructor (Sub New): ដំណើរការភ្លាមពេលបង្កើត Object ថ្មី
    ' ក. Default Constructor (គ្មាន Parameter)
    Public Sub New()
        _id = 1
        _name = "គ្មានឈ្មោះ"
        _score = 0.0
        Major = "ទូទៅ"
    End Sub

    ' ខ. Parameterized Constructor (Constructor ជាមួយ Parameter)
    Public Sub New(id As Integer, name As String, score As Double, major As String)
        ' ប្រើប្រាស់ Property ដើម្បីឆ្លងកាត់ការផ្ទៀងផ្ទាត់ (Validation)
        Me.Id = id
        Me.Name = name
        Me.Score = score
        Me.Major = major
    End Sub

    ' ៥. Methods (សកម្មភាព ឬមុខងាររបស់ Class)
    Public Sub DisplayStudentCard()
        Console.WriteLine("---------------------------------------------")
        Console.WriteLine($"កាតសម្គាល់សិស្ស ID  : {Id}")
        Console.WriteLine($"ឈ្មោះសិស្ស          : {Name}")
        Console.WriteLine($"ជំនាញឯកទេស         : {Major}")
        Console.WriteLine($"ពិន្ទុសរុប           : {Score:F2}")
        Console.WriteLine($"និទ្ទេស             : {Grade}")
        Console.WriteLine("---------------------------------------------")
    End Sub

    Public Function IsPassed() As Boolean
        Return Score >= 50.0
    End Function

End Class

Module ClassesAndPropertiesDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. បង្កើត Object តាមរយៈ Parameterized Constructor ===")
        Try
            ' បង្កើត Instance របស់ Class Student
            Dim student1 As New Student(101, "ចាន់ សុខា", 88.5, "វិទ្យាសាស្ត្រកុំព្យូទ័រ")
            student1.DisplayStudentCard()

            Console.WriteLine($"ស្ថានភាពសិស្សទី ១: {If(student1.IsPassed(), "ជាប់ (Passed)", "ធ្លាក់ (Failed)")}")

            Console.WriteLine()
            Console.WriteLine("=== ២. បង្កើត Object ទីពីរ និងផ្លាស់ប្តូរតម្លៃ Property ===")
            Dim student2 As New Student()
            student2.Id = 102
            student2.Name = "កែវ ពិសិដ្ឋ"
            student2.Major = "ព័ត៌មានវិទ្យា (IT)"
            student2.Score = 45.0
            student2.DisplayStudentCard()

            Console.WriteLine($"ស្ថានភាពសិស្សទី ២: {If(student2.IsPassed(), "ជាប់ (Passed)", "ធ្លាក់ (Failed)")}")

            Console.WriteLine()
            Console.WriteLine("=== ៣. តេស្តការផ្ទៀងផ្ទាត់ទិន្នន័យ (Validation Exception) ===")
            Console.WriteLine("សាកល្បងបញ្ចូលពិន្ទុ 150 (ខុសច្បាប់)...")
            student2.Score = 150.0 ' នឹងបោះ ArgumentOutOfRangeException

        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine($"[កំហុសដែលបានចាប់]: {ex.Message}")
            Console.ResetColor()
        End Try

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
