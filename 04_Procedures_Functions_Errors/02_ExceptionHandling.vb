' ==============================================================================
' មេរៀនទី ៤.២៖ ការគ្រប់គ្រង និងចាប់កំហុស (Exception Handling with Try...Catch...Finally)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System

Module ExceptionHandlingDemo

    ' អនុគមន៍ដកប្រាក់ ដែលនឹងបោះ Exception បើសិនជាសមតុល្យមិនគ្រប់គ្រាន់
    Function WithdrawMoney(currentBalance As Decimal, amount As Decimal) As Decimal
        If amount <= 0 Then
            Throw New ArgumentException("ចំនួនទឹកប្រាក់ដកត្រូវតែធំជាង 0!")
        End If

        If amount > currentBalance Then
            ' បោះ Exception ផ្ទាល់ខ្លួន
            Throw New InvalidOperationException($"សមតុល្យមិនគ្រប់គ្រាន់! លុយក្នុងគណនីមានតែ {currentBalance:C2} តែចង់ដក {amount:C2}")
        End If

        Return currentBalance - amount
    End Function

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. ការចាប់កំហុសទូទៅ និងកំហុសជាក់លាក់ (Catching Specific Exceptions) ===")
        Dim userInput As String = "abc"

        Try
            Console.WriteLine($"កំពុងព្យាយាមបម្លែង '{userInput}' ទៅជាចំនួនគត់...")
            Dim num As Integer = Convert.ToInt32(userInput) ' នឹងបង្កឱ្យមាន FormatException
            Console.WriteLine($"តម្លៃបម្លែងបាន: {num}")

        Catch ex As FormatException
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine($"[កំហុសទម្រង់ FormatException]: តម្លៃបញ្ចូលមិនមែនជាលេខទេ! ({ex.Message})")
            Console.ResetColor()

        Catch ex As OverflowException
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine($"[កំហុស OverflowException]: លេខធំពេកលើសទំហំកំណត់!")
            Console.ResetColor()

        Catch ex As Exception
            ' ចាប់កំហុសទូទៅដែលមិនបានរៀបរាប់ខាងលើ
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine($"[កំហុសទូទៅ]: {ex.Message}")
            Console.ResetColor()

        Finally
            ' ប្លុក Finally នឹងដំណើរការជានិច្ច ទោះមាន Error ឬអត់ក៏ដោយ (ល្អសម្រាប់បិទ Connection ឬ File)
            Console.ForegroundColor = ConsoleColor.DarkGray
            Console.WriteLine("[Finally Block]: ដំណាក់កាលត្រួតពិនិត្យត្រូវបានបញ្ចប់។")
            Console.ResetColor()
        End Try

        Console.WriteLine()
        Console.WriteLine("=== ២. ការចាប់កំហុសចែកនឹងសូន្យ (DivideByZeroException) ===")
        Dim a As Integer = 100
        Dim b As Integer = 0

        Try
            Dim result As Integer = a \ b ' ចែកចំនួនគត់នឹង 0
            Console.WriteLine($"លទ្ធផល: {result}")
        Catch ex As DivideByZeroException
            Console.ForegroundColor = ConsoleColor.Yellow
            Console.WriteLine($"[ប្រយ័ត្ន]: មិនអាចចែកនឹងលេខសូន្យ (0) បានឡើយ!")
            Console.ResetColor()
        End Try

        Console.WriteLine()
        Console.WriteLine("=== ៣. ការបង្កើត និងបោះកំហុសផ្ទាល់ខ្លួន (Throwing Custom Exceptions) ===")
        Dim myBalance As Decimal = 500.0D

        Try
            Console.WriteLine($"សមតុល្យបច្ចុប្បន្ន: {myBalance:C2}")
            Console.WriteLine("សាកល្បងដកប្រាក់ $700.00...")
            myBalance = WithdrawMoney(myBalance, 700.0D)
            Console.WriteLine($"ដកប្រាក់ជោគជ័យ! សមតុល្យនៅសល់: {myBalance:C2}")

        Catch ex As InvalidOperationException
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine($"[កំហុសប្រតិបត្តិការ]: {ex.Message}")
            Console.ResetColor()

        Catch ex As ArgumentException
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine($"[កំហុស Parameter]: {ex.Message}")
            Console.ResetColor()

        Finally
            Console.WriteLine("[ធនាគារ]: សូមអរគុណដែលបានប្រើប្រាស់សេវាកម្ម ATM របស់យើង។")
        End Try

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
