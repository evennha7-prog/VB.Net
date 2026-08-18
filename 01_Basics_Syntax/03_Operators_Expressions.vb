' ==============================================================================
' មេរៀនទី ១.៣៖ ប្រមាណវិធី និងកន្សោមប្រមាណវិធី (Operators & Expressions)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System

Module OperatorsAndExpressions

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. ប្រមាណវិធីនព្វន្ធ (Arithmetic Operators) ===")
        Dim a As Integer = 20
        Dim b As Integer = 6

        Dim addition As Integer = a + b            ' បូក (26)
        Dim subtraction As Integer = a - b         ' ដក (14)
        Dim multiplication As Integer = a * b      ' គុណ (120)
        Dim floatDivision As Double = a / b        ' ចែកចេញទសភាគ (3.3333333333333335)
        Dim integerDivision As Integer = a \ b     ' ចែកយកតែចំនួនគត់ (3)
        Dim remainder As Integer = a Mod b         ' សំណល់ពីការចែក (2)
        Dim power As Double = 2 ^ 3                ' ស្វ័យគុណ 2 លើ 3 = 8

        Console.WriteLine($"{a} + {b} = {addition}")
        Console.WriteLine($"{a} - {b} = {subtraction}")
        Console.WriteLine($"{a} * {b} = {multiplication}")
        Console.WriteLine($"{a} / {b} = {floatDivision:F2} (ចែកធម្មតា)")
        Console.WriteLine($"{a} \\ {b} = {integerDivision} (ចែកយកតែចំនួនគត់ \\)")
        Console.WriteLine($"{a} Mod {b} = {remainder} (សំណល់ពីការចែក)")
        Console.WriteLine($"2 ^ 3 = {power} (ស្វ័យគុណ)")

        Console.WriteLine()
        Console.WriteLine("=== ២. ប្រមាណវិធីប្រៀបធៀប (Comparison Operators) ===")
        Dim x As Integer = 15
        Dim y As Integer = 20

        Console.WriteLine($"{x} = {y}  : {x = y}  (ស្មើគ្នា)")
        Console.WriteLine($"{x} <> {y} : {x <> y} (មិនស្មើគ្នា)")
        Console.WriteLine($"{x} < {y}  : {x < y}  (តូចជាង)")
        Console.WriteLine($"{x} > {y}  : {x > y}  (ធំជាង)")
        Console.WriteLine($"{x} <= {y} : {x <= y} (តូចជាង ឬស្មើ)")
        Console.WriteLine($"{x} >= {y} : {x >= y} (ធំជាង ឬស្មើ)")

        ' ការប្រៀបធៀប Object ឬ Null តាមរយៈ Is និង IsNot
        Dim obj1 As Object = Nothing
        Dim obj2 As New Object()
        Console.WriteLine($"obj1 Is Nothing: {obj1 Is Nothing}")
        Console.WriteLine($"obj2 IsNot Nothing: {obj2 IsNot Nothing}")

        Console.WriteLine()
        Console.WriteLine("=== ៣. ប្រមាណវិធីតក្កវិជ្ជា (Logical Operators) ===")
        Dim isAdult As Boolean = True
        Dim hasIDCard As Boolean = False

        ' ចំណាំ៖ ក្នុង VB.NET គួរប្រើ AndAlso និង OrElse ព្រោះវាជា Short-circuit evaluation
        ' (ប្រសិនបើលក្ខខណ្ឌដំបូងគ្រប់គ្រាន់ វានឹងមិនគណនាលក្ខខណ្ឌបន្ទាប់ទេ ដែលជួយបង្កើនល្បឿន និងសុវត្ថិភាព)
        Dim canEnterAndAlso As Boolean = isAdult AndAlso hasIDCard
        Dim canEnterOrElse As Boolean = isAdult OrElse hasIDCard
        Dim notAdult As Boolean = Not isAdult

        Console.WriteLine($"isAdult AndAlso hasIDCard: {canEnterAndAlso} (ត្រូវតែពិតទាំងពីរ)")
        Console.WriteLine($"isAdult OrElse hasIDCard : {canEnterOrElse} (ពិតតែមួយក៏បាន)")
        Console.WriteLine($"Not isAdult              : {notAdult}")

        Console.WriteLine()
        Console.WriteLine("=== ៤. ប្រមាណវិធីភ្ជាប់ខ្សែអក្សរ (String Concatenation) ===")
        Dim firstName As String = "វិចិត្រ"
        Dim lastName As String = "សាន"

        ' ប្រើសញ្ញា & ដើម្បីភ្ជាប់ខ្សែអក្សរ (ជា Best Practice ជាងសញ្ញា +)
        Dim fullName As String = firstName & " " & lastName
        ' ឬប្រើ String Interpolation ($"...")
        Dim modernFullName As String = $"{firstName} {lastName}"

        Console.WriteLine($"ភ្ជាប់ដោយ &  : {fullName}")
        Console.WriteLine($"ភ្ជាប់ដោយ $"": {modernFullName}")

        Console.WriteLine()
        Console.WriteLine("=== ៥. ប្រមាណវិធីលក្ខខណ្ឌសង្ខេប (Inline If / Ternary Operator) ===")
        Dim studentScore As Double = 78.5
        ' Syntax: If(លក្ខខណ្ឌ, តម្លៃបើពិត, តម្លៃបើមិនពិត)
        Dim resultStatus As String = If(studentScore >= 50, "ជាប់ (Passed)", "ធ្លាក់ (Failed)")
        Console.WriteLine($"ពិន្ទុ {studentScore} => លទ្ធផល៖ {resultStatus}")

        ' Null-Coalescing Operator: If(variable, fallbackValue)
        Dim nickname As String = Nothing
        Dim displayName As String = If(nickname, "អនាមិក (Anonymous)")
        Console.WriteLine($"ឈ្មោះហៅក្រៅ: {displayName}")

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
