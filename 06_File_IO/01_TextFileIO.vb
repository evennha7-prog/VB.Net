' ==============================================================================
' មេរៀនទី ៦.១៖ ការអាន និងសរសេរឯកសារអត្ថបទ (Text File I/O & Stream Processing)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.IO
Imports System.Text

Module TextFileIODemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        ' កំណត់ទីតាំងថត និងឯកសារ
        Dim dataFolder As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataStorage")
        Dim filePath As String = Path.Combine(dataFolder, "students_log.txt")

        ' បង្កើត Folder ប្រសិនបើមិនទាន់មាន
        If Not Directory.Exists(dataFolder) Then
            Directory.CreateDirectory(dataFolder)
            Console.WriteLine($"[ជោគជ័យ]: បានបង្កើត Folder '{dataFolder}'")
        End If

        Console.WriteLine("=== ១. ការសរសេរ និងអានឯកសារតាមរយៈ Class 'File' សាមញ្ញ ===")

        Dim initialContent As String = "សួស្តី! នេះជាកំណត់ត្រាដំបូងនៃមេរៀន VB.NET 2026." & Environment.NewLine &
                                      "កាលបរិច្ឆេទបង្កើត៖ " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        ' File.WriteAllText៖ បង្កើត និងសរសេរជាន់លើឯកសារចាស់
        File.WriteAllText(filePath, initialContent, Encoding.UTF8)
        Console.WriteLine($"[ជោគជ័យ]: បានសរសេរឯកសារទៅកាន់ '{Path.GetFileName(filePath)}'")

        ' File.AppendAllText៖ សរសេរបន្ថែមពីក្រោម (មិនលុបរបស់ចាស់ទេ)
        File.AppendAllText(filePath, Environment.NewLine & "បន្ថែមបន្ទាត់ថ្មី៖ និស្សិតទាំងអស់បានបញ្ចប់ Module 1 រួចរាល់។", Encoding.UTF8)

        ' File.ReadAllText៖ អានឯកសារទាំងមូលយកមកបង្ហាញ
        If File.Exists(filePath) Then
            Dim readText As String = File.ReadAllText(filePath, Encoding.UTF8)
            Console.WriteLine("\n--- ខ្លឹមសារក្នុងឯកសារ (ReadAllText) ---")
            Console.WriteLine(readText)
        End If

        Console.WriteLine()
        Console.WriteLine("=== ២. ការប្រើប្រាស់ StreamWriter & StreamReader (ជាមួយ Using Statement) ===")
        Dim logFilePath As String = Path.Combine(dataFolder, "system_events.log")

        ' Using Statement ធានាថាឯកសារនឹងត្រូវ Flush & Close ដោយស្វ័យប្រវត្តិ ទោះមាន Error ក៏ដោយ
        Using writer As New StreamWriter(logFilePath, append:=True, encoding:=Encoding.UTF8)
            writer.WriteLine($"[{DateTime.Now:HH:mm:ss}] - កម្មវិធីបានចាប់ផ្តើមដំណើរការ")
            writer.WriteLine($"[{DateTime.Now:HH:mm:ss}] - អ្នកប្រើប្រាស់បាន Login ជោគជ័យ")
            writer.WriteLine($"[{DateTime.Now:HH:mm:ss}] - បានទាញយកទិន្នន័យពី Database រួចរាល់")
        End Using
        Console.WriteLine("[ជោគជ័យ]: បានកត់ត្រា Log ដោយប្រើ StreamWriter")

        ' អានឯកសារបន្ទាត់ម្តងៗតាម StreamReader
        Console.WriteLine("\n--- អានឯកសារតាម StreamReader (Line-by-Line) ---")
        Using reader As New StreamReader(logFilePath, Encoding.UTF8)
            Dim lineNumber As Integer = 1
            While Not reader.EndOfStream
                Dim lineContent As String = reader.ReadLine()
                Console.WriteLine($"បន្ទាត់ទី {lineNumber}: {lineContent}")
                lineNumber += 1
            End While
        End Using

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
