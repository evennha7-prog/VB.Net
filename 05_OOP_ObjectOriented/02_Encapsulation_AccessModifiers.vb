' ==============================================================================
' មេរៀនទី ៥.២៖ ការខ្ចប់ទិន្នន័យ និងកម្រិតសិទ្ធិ (Encapsulation & Access Modifiers)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic

''' <summary>
''' ថ្នាក់គណនីធនាគារ (BankAccount) បង្ហាញពីគោលការណ៍ Encapsulation
''' ទិន្នន័យសមតុល្យ (_balance) មិនអាចកែប្រែដោយសេរីពីខាងក្រៅបានទេ 
''' គឺត្រូវឆ្លងកាត់ Method ដាក់ប្រាក់ (Deposit) ឬ ដកប្រាក់ (Withdraw) តែប៉ុណ្ណោះ។
''' </summary>
Public Class BankAccount

    ' ១. Private: មើលឃើញ និងប្រើប្រាស់បានតែក្នុង Class នេះប៉ុណ្ណោះ
    Private _accountNumber As String
    Private _balance As Decimal
    Private _transactionHistory As List(Of String)

    ' ២. Protected: ប្រើប្រាស់បានក្នុង Class នេះ និង Class កូនចៅដែល Inherits ពីវា
    Protected _accountHolder As String

    ' ៣. Friend: ប្រើប្រាស់បានគ្រប់ទីកន្លែងក្នុងគម្រោង Assembly តែមួយ (ដូច Internal ក្នុង C#)
    Friend BranchCode As String

    ' ៤. Public: អាចចូលប្រើប្រាស់បានពីគ្រប់ទីកន្លែង
    Public Property AccountNumber As String
        Get
            Return _accountNumber
        End Get
        Private Set(value As String) ' Private Set: អាចកំណត់បានតែខាងក្នុង Class ប៉ុណ្ណោះ
            _accountNumber = value
        End Set
    End Property

    Public ReadOnly Property Balance As Decimal
        Get
            Return _balance
        End Get
    End Property

    Public ReadOnly Property AccountHolder As String
        Get
            Return _accountHolder
        End Get
    End Property

    ' Constructor
    Public Sub New(accNumber As String, holderName As String, initialDeposit As Decimal)
        If initialDeposit < 10.0D Then
            Throw New ArgumentException("ប្រាក់កក់ដំបូងត្រូវតែយ៉ាងតិច $10.00!")
        End If

        _accountNumber = accNumber
        _accountHolder = holderName
        _balance = initialDeposit
        _transactionHistory = New List(Of String)()
        _transactionHistory.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] បើកគណនីដំបូង: +{initialDeposit:C2}")
    End Sub

    ' មុខងារដាក់ប្រាក់ (Deposit)
    Public Sub Deposit(amount As Decimal)
        If amount <= 0 Then
            Console.WriteLine("[បរាជ័យ]: ចំនួនទឹកប្រាក់ដាក់ត្រូវតែធំជាង 0!")
            Return
        End If

        _balance += amount
        _transactionHistory.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] ដាក់ប្រាក់: +{amount:C2} (សមតុល្យ: {_balance:C2})")
        Console.WriteLine($"[ជោគជ័យ]: បានដាក់ប្រាក់ {amount:C2} ចូលគណនី {_accountNumber}")
    End Sub

    ' មុខងារដកប្រាក់ (Withdraw)
    Public Function Withdraw(amount As Decimal) As Boolean
        If amount <= 0 Then
            Console.WriteLine("[បរាជ័យ]: ចំនួនទឹកប្រាក់ដកត្រូវតែធំជាង 0!")
            Return False
        End If

        If amount > _balance Then
            Console.WriteLine($"[បរាជ័យ]: សមតុល្យមិនគ្រប់គ្រាន់! (មានត្រឹម {_balance:C2})")
            Return False
        End If

        _balance -= amount
        _transactionHistory.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm}] ដកប្រាក់: -{amount:C2} (សមតុល្យ: {_balance:C2})")
        Console.WriteLine($"[ជោគជ័យ]: បានដកប្រាក់ {amount:C2} ពីគណនី {_accountNumber}")
        Return True
    End Function

    ' បង្ហាញប្រវត្តិប្រតិបត្តិការ
    Public Sub PrintStatement()
        Console.WriteLine("==================================================")
        Console.WriteLine($"របាយការណ៍គណនី: {_accountNumber} | ម្ចាស់: {_accountHolder}")
        Console.WriteLine($"សមតុល្យបច្ចុប្បន្ន: {_balance:C2}")
        Console.WriteLine("--- ប្រវត្តិប្រតិបត្តិការ ---")
        For Each log As String In _transactionHistory
            Console.WriteLine($"  {log}")
        Next
        Console.WriteLine("==================================================")
    End Sub

End Class

Module EncapsulationDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== តេស្តគោលការណ៍ Encapsulation លើគណនីធនាគារ ===")
        Dim acc As New BankAccount("001-234-567", "សុខ សាន", 100.0D)

        ' ប្រតិបត្តិការដាក់ និងដកប្រាក់
        acc.Deposit(50.0D)
        acc.Withdraw(30.0D)
        acc.Withdraw(200.0D) ' នឹងបរាជ័យដោយសារលើសសមតុល្យ

        ' បង្ហាញរបាយការណ៍
        Console.WriteLine()
        acc.PrintStatement()

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
