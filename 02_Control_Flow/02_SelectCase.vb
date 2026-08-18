' ==============================================================================
' មេរៀនទី ២.២៖ ការប្រើប្រាស់ Select Case (Decision Making with Select Case)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System

Module SelectCaseDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. Select Case សាមញ្ញ (ជ្រើសរើសតាមថ្ងៃនៃសប្តាហ៍) ===")
        Dim dayNumber As Integer = 3
        Dim dayName As String

        Select Case dayNumber
            Case 1
                dayName = "ថ្ងៃច័ន្ទ (Monday)"
            Case 2
                dayName = "ថ្ងៃអង្គារ (Tuesday)"
            Case 3
                dayName = "ថ្ងៃពុធ (Wednesday)"
            Case 4
                dayName = "ថ្ងៃព្រហស្បតិ៍ (Thursday)"
            Case 5
                dayName = "ថ្ងៃសុក្រ (Friday)"
            Case 6
                dayName = "ថ្ងៃសៅរ៍ (Saturday)"
            Case 7
                dayName = "ថ្ងៃអាទិត្យ (Sunday)"
            Case Else
                dayName = "លេខថ្ងៃមិនត្រឹមត្រូវ (Invalid Day)"
        End Select

        Console.WriteLine($"លេខថ្ងៃ {dayNumber} គឺ៖ {dayName}")

        Console.WriteLine()
        Console.WriteLine("=== ២. Select Case ជាមួយតម្លៃច្រើនក្នុងករណីតែមួយ (Multiple Values) ===")
        Dim dayOfWeek As Integer = 6

        Select Case dayOfWeek
            Case 1, 2, 3, 4, 5
                Console.WriteLine("ថ្ងៃនេះជាថ្ងៃធ្វើការ/រៀនសូត្រ (Weekday)")
            Case 6, 7
                Console.ForegroundColor = ConsoleColor.Cyan
                Console.WriteLine("ថ្ងៃនេះជាថ្ងៃចុងសប្តាហ៍សម្រាក (Weekend)")
                Console.ResetColor()
            Case Else
                Console.WriteLine("លេខថ្ងៃមិនត្រឹមត្រូវ!")
        End Select

        Console.WriteLine()
        Console.WriteLine("=== ៣. Select Case ជាមួយចន្លោះតម្លៃ (Range with 'To') ===")
        Dim age As Integer = 16
        Dim ageCategory As String

        Select Case age
            Case 0 To 2
                ageCategory = "ទារក (Baby / Infant)"
            Case 3 To 12
                ageCategory = "កុមារ (Child)"
            Case 13 To 19
                ageCategory = "យុវវ័យ (Teenager)"
            Case 20 To 59
                ageCategory = "មនុស្សពេញវ័យ (Adult)"
            Case Is >= 60
                ageCategory = "មនុស្សចាស់ (Senior Citizen)"
            Case Else
                ageCategory = "អាយុមិនត្រឹមត្រូវ"
        End Select

        Console.WriteLine($"អាយុ {age} ឆ្នាំ ស្ថិតក្នុងក្រុម៖ {ageCategory}")

        Console.WriteLine()
        Console.WriteLine("=== ៤. Select Case ជាមួយការប្រៀបធៀប (Comparison with 'Is') ===")
        Dim examScore As Integer = 88

        Select Case examScore
            Case Is >= 90
                Console.WriteLine("និទ្ទេស A - ឆ្នើម!")
            Case Is >= 80
                Console.WriteLine("និទ្ទេស B - ល្អណាស់!")
            Case Is >= 70
                Console.WriteLine("និទ្ទេស C - ល្អ!")
            Case Is >= 60
                Console.WriteLine("និទ្ទេស D - មធ្យម!")
            Case Is >= 50
                Console.WriteLine("និទ្ទេស E - ខ្សោយ!")
            Case Else
                Console.WriteLine("និទ្ទេស F - ធ្លាក់!")
        End Select

        Console.WriteLine()
        Console.WriteLine("=== ៥. Select Case ជាមួយខ្សែអក្សរ (String Matching) ===")
        Dim role As String = "ADMIN"

        ' ប្រើ ToUpper() ឬ ToLower() ដើម្បីកុំឱ្យខុសអក្សរតូចធំ (Case-insensitive)
        Select Case role.Trim().ToUpper()
            Case "ADMIN", "SUPERADMIN"
                Console.WriteLine("សិទ្ធិ៖ អាចគ្រប់គ្រងប្រព័ន្ធទាំងមូលបាន (Full Access)")
            Case "MANAGER"
                Console.WriteLine("សិទ្ធិ៖ អាចមើល និងកែប្រែទិន្នន័យសាខា (Branch Access)")
            Case "USER", "GUEST"
                Console.WriteLine("សិទ្ធិ៖ អាចមើលទិន្នន័យតែប៉ុណ្ណោះ (Read-only Access)")
            Case Else
                Console.WriteLine("សិទ្ធិមិនស្គាល់ (Unknown Role)")
        End Select

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
