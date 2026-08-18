' ==============================================================================
' មេរៀនទី ១.១៖ កម្មវិធីដំបូង (Hello World) និង រចនាសម្ព័ន្ធកម្មវិធីក្នុង VB.NET
' កាលបរិច្ឆេទ៖ 2026
' រៀបចំដោយ៖ Antigravity / PCCFP Institute
' ==============================================================================

Option Explicit On  ' បង្ខំឱ្យប្រកាសអថេរមុនពេលប្រើ (ជៀសវាងកំហុសអក្ខរាវិរុទ្ធឈ្មោះអថេរ)
Option Strict On    ' បង្ខំឱ្យពិនិត្យប្រភេទ Type ឱ្យបានច្បាស់លាស់ (ការពារការបាត់បង់ទិន្នន័យដោយចៃដន្យ)

Imports System      ' ទាញយក Namespace ស្តង់ដាររបស់ .NET មកប្រើ

Module HelloWorldProgram

    ''' <summary>
    ''' Sub Main គឺជាចំណុចចាប់ផ្តើម (Entry Point) នៃកម្មវិធី Console ទាំងអស់ក្នុង VB.NET
    ''' នៅពេលដែលយើង Run កម្មវិធី កូដក្នុង Sub Main នឹងត្រូវដំណើរការមុនគេបង្អស់។
    ''' </summary>
    Sub Main()
        ' ប្តូរពណ៌អក្សរ និងពណ៌ផ្ទៃខាងក្រោយលើផ្ទាំង Console
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.BackgroundColor = ConsoleColor.Black
        Console.Clear() ' សម្អាតអេក្រង់

        ' បង្ហាញចំណងជើង
        Console.WriteLine("==================================================")
        Console.WriteLine("        សូមស្វាគមន៍មកកាន់ការរៀន VB.NET 2026!      ")
        Console.WriteLine("==================================================")

        ' Console.WriteLine()៖ បង្ហាញអត្ថបទ រួចចុះបន្ទាត់ថ្មី
        Console.WriteLine("សួស្តីពិភពលោក! (Hello, World!)")
        Console.WriteLine("ភាសា VB.NET គឺជាភាសាដ៏មានឥទ្ធិពល និងងាយស្រួលរៀន។")

        ' Console.Write()៖ បង្ហាញអត្ថបទដោយមិនចុះបន្ទាត់
        Console.Write("សូមបញ្ចូលឈ្មោះរបស់អ្នក (Enter your name): ")

        ' Console.ReadLine()៖ ចាំទទួលយកអត្ថបទដែលអ្នកប្រើប្រាស់វាយបញ្ចូលតាមក្តារចុច
        Dim userName As String = Console.ReadLine()

        ' បង្ហាញសារស្វាគមន៍ដោយភ្ជាប់ឈ្មោះ
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine($"សួស្តី {userName}! សូមជូនពរឱ្យអ្នករៀន VB.NET ទទួលបានជោគជ័យ!")

        ' កំណត់ពណ៌ធម្មតាវិញ
        Console.ResetColor()

        Console.WriteLine()
        Console.WriteLine("ចុចគ្រាប់ចុចណាមួយដើម្បីបញ្ចប់កម្មវិធី...")
        Console.ReadKey() ' ទប់ផ្ទាំង Console កុំឱ្យបិទភ្លាមៗ រហូតទាល់តែចុច Key ណាមួយ
    End Sub

End Module
