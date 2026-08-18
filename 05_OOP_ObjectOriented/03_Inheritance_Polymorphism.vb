' ==============================================================================
' មេរៀនទី ៥.៣៖ ដំណរពូជ និងពហុរូប (Inheritance, Polymorphism, MustInherit & Overrides)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic

' ១. Base Class / Abstract Class (MustInherit): មិនអាចបង្កើត Object ផ្ទាល់បានទេ ទុកសម្រាប់តែឱ្យ Class ផ្សេងតពូជ
Public MustInherit Class Employee

    Public Property Id As Integer
    Public Property Name As String
    Public Property BaseSalary As Decimal

    Public Sub New(id As Integer, name As String, baseSalary As Decimal)
        Me.Id = id
        Me.Name = name
        Me.BaseSalary = baseSalary
    End Sub

    ' Abstract Method (MustOverride): Class កូនៗទាំងអស់ត្រូវតែសរសេរកូដអនុវត្តផ្ទាល់ខ្លួន
    Public MustOverride Function CalculateMonthlyIncome() As Decimal

    ' Virtual Method (Overridable): មានកូដលំនាំដើម តែអនុញ្ញាតឱ្យ Class កូនៗសរសេរកែច្នៃបន្ថែម (Override) បាន
    Public Overridable Sub DisplayDetails()
        Console.WriteLine($"[បុគ្គលិក ID: {Id}] ឈ្មោះ: {Name}, ប្រាក់ខែគោល: {BaseSalary:C2}")
    End Sub

End Class

' ២. Derived Class ទីមួយ៖ បុគ្គលិកពេញម៉ោង (FullTimeEmployee)
Public Class FullTimeEmployee
    Inherits Employee

    Public Property AnnualBonus As Decimal

    ' ហៅ Constructor របស់ Base Class ដោយប្រើ MyBase.New(...)
    Public Sub New(id As Integer, name As String, baseSalary As Decimal, bonus As Decimal)
        MyBase.New(id, name, baseSalary)
        Me.AnnualBonus = bonus
    End Sub

    ' Overrides អនុវត្តការគណនាប្រាក់ចំណូលជាក់ស្តែង
    Public Overrides Function CalculateMonthlyIncome() As Decimal
        Dim monthlyBonus As Decimal = AnnualBonus / 12.0D
        Return BaseSalary + monthlyBonus
    End Function

    Public Overrides Sub DisplayDetails()
        MyBase.DisplayDetails() ' ហៅ Method របស់ Base Class
        Console.WriteLine($"   -> ប្រភេទ: ពេញម៉ោង (Full-Time) | ប្រាក់លើកទឹកចិត្តប្រចាំឆ្នាំ: {AnnualBonus:C2} | ចំណូលសរុបប្រចាំខែ: {CalculateMonthlyIncome():C2}")
    End Sub

End Class

' ៣. Derived Class ទីពីរ៖ បុគ្គលិកក្រៅម៉ោង (PartTimeEmployee)
Public Class PartTimeEmployee
    Inherits Employee

    Public Property HourlyRate As Decimal
    Public Property HoursWorked As Integer

    Public Sub New(id As Integer, name As String, hourlyRate As Decimal, hoursWorked As Integer)
        MyBase.New(id, name, 0.0D) ' ប្រាក់ខែគោល = 0
        Me.HourlyRate = hourlyRate
        Me.HoursWorked = hoursWorked
    End Sub

    Public Overrides Function CalculateMonthlyIncome() As Decimal
        Return HourlyRate * CDec(HoursWorked)
    End Function

    Public Overrides Sub DisplayDetails()
        Console.WriteLine($"[បុគ្គលិក ID: {Id}] ឈ្មោះ: {Name} (ក្រៅម៉ោង Part-Time)")
        Console.WriteLine($"   -> ម៉ោងធ្វើការ: {HoursWorked} ម៉ោង x {HourlyRate:C2}/ម៉ោង | ចំណូលសរុបប្រចាំខែ: {CalculateMonthlyIncome():C2}")
    End Sub

End Class

Module InheritancePolymorphismDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ការអនុវត្ត Polymorphism (ពហុរូប) ===")
        ' បង្កើតបញ្ជីដែលផ្ទុកប្រភេទ Base Class (Employee) ប៉ុន្តែអាចដាក់ Object កូនចៅទាំងពីរប្រភេទចូលគ្នាបាន
        Dim employeeList As New List(Of Employee)()

        employeeList.Add(New FullTimeEmployee(1001, "សុខ ចិន្តា", 1200.0D, 2400.0D))
        employeeList.Add(New PartTimeEmployee(1002, "វ៉ាន់ ដារ៉ា", 15.0D, 80))
        employeeList.Add(New FullTimeEmployee(1003, "មាស សំណាង", 2000.0D, 5000.0D))

        Dim totalPayroll As Decimal = 0.0D

        ' រត់កាត់បញ្ជីទាំងអស់ - .NET នឹងហៅ DisplayDetails() និង CalculateMonthlyIncome() ត្រឹមត្រូវតាមប្រភេទ Object នីមួយៗដោយស្វ័យប្រវត្តិ
        For Each emp As Employee In employeeList
            emp.DisplayDetails()
            totalPayroll += emp.CalculateMonthlyIncome()
            Console.WriteLine()
        Next

        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine("==================================================")
        Console.WriteLine($"សរុបថវិកាត្រូវបើកប្រាក់ខែប្រចាំខែ: {totalPayroll:C2}")
        Console.WriteLine("==================================================")
        Console.ResetColor()

        Console.WriteLine()
        Console.WriteLine("=== ការពិនិត្យប្រភេទ Object (Type Checking & Casting) ===")
        For Each emp As Employee In employeeList
            If TypeOf emp Is FullTimeEmployee Then
                Dim ftEmp As FullTimeEmployee = CType(emp, FullTimeEmployee)
                Console.WriteLine($"រកឃើញបុគ្គលិកពេញម៉ោង: {ftEmp.Name} មាន Bonus: {ftEmp.AnnualBonus:C2}")
            End If
        Next

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
