' ==============================================================================
' មេរៀនទី ៥.៤៖ ការប្រើប្រាស់ Interface (Interfaces & Multiple Implementation)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic

' ១. កំណត់ Interface សម្រាប់មុខងារបង់ប្រាក់
Public Interface IPaymentMethod
    Property MethodName As String
    Function ProcessPayment(amount As Decimal) As Boolean
    Sub PrintReceipt(transactionId As String, amount As Decimal)
End Interface

' ២. កំណត់ Interface សម្រាប់ការបញ្ជូនដំណឹង (Notification)
Public Interface INotifiable
    Sub SendNotification(recipient As String, message As String)
End Interface

' ៣. Class ទីមួយ៖ អនុវត្តការទូទាត់តាម ABA KHQR (Implements Multiple Interfaces)
Public Class ABAKHQRPayment
    Implements IPaymentMethod, INotifiable

    Public Property MethodName As String Implements IPaymentMethod.MethodName
    Public Property MerchantId As String

    Public Sub New(merchantId As String)
        Me.MerchantId = merchantId
        Me.MethodName = "ABA KHQR Payment"
    End Sub

    Public Function ProcessPayment(amount As Decimal) As Boolean Implements IPaymentMethod.ProcessPayment
        Console.WriteLine($"[ABA Bank]: កំពុងដំណើរការស្កេន KHQR ចំនួន {amount:C2} ទៅកាន់ Merchant '{MerchantId}'...")
        ' សន្មតថាការទូទាត់ជោគជ័យ
        Return True
    End Function

    Public Sub PrintReceipt(transactionId As String, amount As Decimal) Implements IPaymentMethod.PrintReceipt
        Console.WriteLine($"[បង្កាន់ដៃ ABA]: លេខប្រតិបត្តិការ: {transactionId} | ចំនួនទឹកប្រាក់: {amount:C2} | ជោគជ័យ")
    End Sub

    Public Sub SendNotification(recipient As String, message As String) Implements INotifiable.SendNotification
        Console.WriteLine($"[ABA Mobile App Push Notification ទៅកាន់ {recipient}]: {message}")
    End Sub

End Class

' ៤. Class ទីពីរ៖ អនុវត្តការទូទាត់តាមកាតឥណទាន Credit Card
Public Class CreditCardPayment
    Implements IPaymentMethod

    Public Property MethodName As String Implements IPaymentMethod.MethodName
    Private _cardNumber As String

    Public Sub New(cardNumber As String)
        Me.MethodName = "Visa / MasterCard"
        Me._cardNumber = cardNumber
    End Sub

    Public Function ProcessPayment(amount As Decimal) As Boolean Implements IPaymentMethod.ProcessPayment
        Console.WriteLine($"[Card Gateway]: កំពុងកាត់ប្រាក់ពីកាត ****-****-****-{_cardNumber.Substring(_cardNumber.Length - 4)} ចំនួន {amount:C2}...")
        Return True
    End Function

    Public Sub PrintReceipt(transactionId As String, amount As Decimal) Implements IPaymentMethod.PrintReceipt
        Console.WriteLine($"[បង្កាន់ដៃ Credit Card]: Txn #{transactionId} | ទឹកប្រាក់: {amount:C2} | អនុម័ត")
    End Sub

End Class

' ៥. Checkout Service ដែលដំណើរការលើ Interface (Loose Coupling)
Public Class CheckoutManager

    Public Shared Sub ExecuteCheckout(paymentEngine As IPaymentMethod, amount As Decimal, customerContact As String)
        Console.WriteLine($"\n--- ចាប់ផ្តើមទូទាត់តាមរយៈ: {paymentEngine.MethodName} ---")
        Dim success As Boolean = paymentEngine.ProcessPayment(amount)

        If success Then
            Dim txnId As String = "TXN-" & Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
            paymentEngine.PrintReceipt(txnId, amount)

            ' បើ Payment Engine នោះ Implement INotifiable ផងដែរ នោះនឹងផ្ញើសារជូនដំណឹង
            If TypeOf paymentEngine Is INotifiable Then
                Dim notifier As INotifiable = CType(paymentEngine, INotifiable)
                notifier.SendNotification(customerContact, $"អ្នកបានទូទាត់ចំនួន {amount:C2} ដោយជោគជ័យ!")
            End If
        End If
    End Sub

End Class

Module InterfacesDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== តេស្តប្រព័ន្ធទូទាត់ប្រាក់ដោយប្រើ Interface ===")

        Dim abaPayment As New ABAKHQRPayment("PCCFP_MERCHANT_001")
        Dim cardPayment As New CreditCardPayment("4111222233334444")

        ' អតិថិជនទី ១ ជ្រើសរើសបង់តាម KHQR
        CheckoutManager.ExecuteCheckout(abaPayment, 45.5D, "012 345 678")

        ' អតិថិជនទី ២ ជ្រើសរើសបង់តាម Credit Card
        CheckoutManager.ExecuteCheckout(cardPayment, 120.0D, "098 765 432")

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
