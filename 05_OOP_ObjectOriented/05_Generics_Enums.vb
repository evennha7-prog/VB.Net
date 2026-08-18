' ==============================================================================
' មេរៀនទី ៥.៥៖ ការប្រើប្រាស់ Enums និង Generics (Enums, Generic Classes & Methods)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic

' ១. Enumerations (Enum): កំណត់បណ្តុំឈ្មោះថេរដើម្បីងាយស្រួលអាន និងការពារការវាយតម្លៃខុស
Public Enum OrderStatus
    Pending = 1       ' កំពុងរង់ចាំ
    Processing = 2    ' កំពុងរៀបចំ
    Shipped = 3       ' កំពុងដឹកជញ្ជូន
    Delivered = 4     ' ដឹកដល់ហើយ
    Cancelled = 5     ' បានលុបចោល
End Enum

Public Enum UserRole
    Administrator
    Instructor
    Student
    Guest
End Enum

' ២. Generic Class: Class ដែលអាចដំណើរការជាមួយប្រភេទ Data Type ណាក៏បាន (Type Safety)
Public Class ApiResponse(Of T)

    Public Property IsSuccess As Boolean
    Public Property Message As String
    Public Property Data As T
    Public Property Timestamp As DateTime

    Public Sub New(success As Boolean, msg As String, payload As T)
        Me.IsSuccess = success
        Me.Message = msg
        Me.Data = payload
        Me.Timestamp = DateTime.Now
    End Sub

    Public Sub PrintResponse()
        Console.WriteLine($"[API ឆ្លើយតប] ស្ថានភាព: {If(IsSuccess, "ជោគជ័យ", "បរាជ័យ")} | សារ: {Message}")
        Console.WriteLine($"  ទិន្នន័យ (Data): {Data} | ពេលវេលា: {Timestamp:yyyy-MM-dd HH:mm:ss}")
    End Sub

End Class

' ៣. Generic Repository / Helper
Public Class UtilityHelper

    ' Generic Method សម្រាប់ផ្លាស់ប្តូរតម្លៃរវាងអថេរពីរ (Swap Values)
    Public Shared Sub Swap(Of T)(ByRef first As T, ByRef second As T)
        Dim temp As T = first
        first = second
        second = temp
    End Sub

    ' Generic Method សម្រាប់បង្ហាញធាតុទាំងអស់ក្នុង List
    Public Shared Sub PrintList(Of T)(title As String, items As IEnumerable(Of T))
        Console.WriteLine($"--- {title} ---")
        For Each item As T In items
            Console.WriteLine($" * {item}")
        Next
    End Sub

End Class

Module GenericsAndEnumsDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. ការប្រើប្រាស់ Enum ===")
        Dim currentOrder As OrderStatus = OrderStatus.Processing
        Console.WriteLine($"ស្ថានភាព Order បច្ចុប្បន្ន: {currentOrder} (តម្លៃលេខ: {CInt(currentOrder)})")

        Select Case currentOrder
            Case OrderStatus.Pending
                Console.WriteLine("ការកុម្ម៉ង់កំពុងរង់ចាំការទូទាត់។")
            Case OrderStatus.Processing
                Console.WriteLine("ហាងកំពុងរៀបចំវេចខ្ចប់ទំនិញរបស់អ្នក...")
            Case OrderStatus.Delivered
                Console.WriteLine("ទំនិញត្រូវបានប្រគល់ជូនរួចរាល់។")
        End Select

        Console.WriteLine()
        Console.WriteLine("=== ២. ការប្រើប្រាស់ Generic Method (Swap) ===")
        Dim x As Integer = 100, y As Integer = 999
        Console.WriteLine($"មុន Swap: x = {x}, y = {y}")
        UtilityHelper.Swap(Of Integer)(x, y)
        Console.WriteLine($"ក្រោយ Swap: x = {x}, y = {y}")

        Dim strA As String = "ភាសាខ្មែរ", strB As String = "English"
        Console.WriteLine($"មុន Swap: A = {strA}, B = {strB}")
        UtilityHelper.Swap(Of String)(strA, strB)
        Console.WriteLine($"ក្រោយ Swap: A = {strA}, B = {strB}")

        Console.WriteLine()
        Console.WriteLine("=== ៣. ការប្រើប្រាស់ Generic Class (ApiResponse) ===")
        ' ApiResponse ផ្ទុក Integer
        Dim countResponse As New ApiResponse(Of Integer)(True, "ទាញយកចំនួនសិស្សជោគជ័យ", 450)
        countResponse.PrintResponse()

        ' ApiResponse ផ្ទុក String
        Dim authResponse As New ApiResponse(Of String)(True, "Login ជោគជ័យ", "TOKEN_ABC_123456")
        authResponse.PrintResponse()

        ' ApiResponse ផ្ទុក List(Of String)
        Dim courses As New List(Of String) From {"VB.NET", "C#", "SQL Server"}
        UtilityHelper.PrintList(Of String)("បញ្ជីវគ្គសិក្សា", courses)

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
