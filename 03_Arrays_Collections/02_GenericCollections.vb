' ==============================================================================
' មេរៀនទី ៣.២៖ បណ្តុំទិន្នន័យ Generic Collections (List, Dictionary, Queue, Stack)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic

Module GenericCollectionsDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("=== ១. បណ្តុំ List(Of T) - បញ្ជីទិន្នន័យបត់បែនតាមតម្រូវការ ===")
        ' List(Of T) ងាយស្រួលជាង Array ព្រោះអាចបន្ថែម ឬលុបធាតុបានដោយសេរី (Auto-resize)
        Dim studentList As New List(Of String)()

        ' បន្ថែមធាតុ (Add)
        studentList.Add("ដារ៉ា")
        studentList.Add("រដ្ឋា")
        studentList.Add("ចិន្តា")
        studentList.Add("សុភាព")

        ' បញ្ចូលធាតុចំទីតាំងជាក់លាក់ (Insert)
        studentList.Insert(1, "មករា")

        ' លុបធាតុ (Remove / RemoveAt)
        studentList.Remove("សុភាព")
        studentList.RemoveAt(0) ' លុប "ដារ៉ា" ចេញពី Index 0

        Console.WriteLine($"ចំនួនសិស្សក្នុងបញ្ជី: {studentList.Count}")
        Console.WriteLine("បញ្ជីឈ្មោះសិស្សបច្ចុប្បន្ន:")
        For Each name As String In studentList
            Console.WriteLine($" -> {name}")
        Next

        ' ពិនិត្យវត្តមានធាតុ (Contains)
        Console.WriteLine($"មានឈ្មោះ 'រដ្ឋា' ក្នុងបញ្ជីទេ? {studentList.Contains("រដ្ឋា")}")

        Console.WriteLine()
        Console.WriteLine("=== ២. បណ្តុំ Dictionary(Of TKey, TValue) - គូ Key-Value ===")
        ' Dictionary ផ្ទុកទិន្នន័យជាគូ Key (មិនអាចស្ទួន) និង Value
        Dim countryCodes As New Dictionary(Of String, String)()

        countryCodes.Add("KH", "កម្ពុជា (Cambodia)")
        countryCodes.Add("US", "សហរដ្ឋអាមេរិក (United States)")
        countryCodes.Add("JP", "ជប៉ុន (Japan)")
        countryCodes.Add("FR", "បារាំង (France)")

        ' ទាញយកតម្លៃតាម Key
        Dim selectedCountry As String = countryCodes("KH")
        Console.WriteLine($"កូដ 'KH' ត្រូវនឹងប្រទេស៖ {selectedCountry}")

        ' ស្វែងរកតាម Key ប្រកបដោយសុវត្ថិភាព (TryGetValue)
        Dim searchKey As String = "JP"
        Dim countryName As String = ""
        If countryCodes.TryGetValue(searchKey, countryName) Then
            Console.WriteLine($"ស្វែងរកឃើញកូដ '{searchKey}' គឺ៖ {countryName}")
        End If

        ' បង្ហាញធាតុទាំងអស់ក្នុង Dictionary
        Console.WriteLine("បញ្ជីប្រទេសទាំងអស់ក្នុង Dictionary:")
        For Each kvp As KeyValuePair(Of String, String) In countryCodes
            Console.WriteLine($"  [{kvp.Key}] => {kvp.Value}")
        Next

        Console.WriteLine()
        Console.WriteLine("=== ៣. បណ្តុំ Queue(Of T) - ជួរតម្រង់ទិស FIFO (First-In, First-Out) ===")
        ' ចូលមុន ចេញមុន (ដូចជាការតម្រង់ជួរទិញសំបុត្រ)
        Dim customerQueue As New Queue(Of String)()

        ' Enqueue: បញ្ចូលទៅចុងជួរ
        customerQueue.Enqueue("អតិថិជន A (មកមុនគេ)")
        customerQueue.Enqueue("អតិថិជន B")
        customerQueue.Enqueue("អតិថិជន C (មកក្រោយគេ)")

        Console.WriteLine($"អ្នកដែលត្រូវចូលបន្ទាប់ (Peek): {customerQueue.Peek()}")

        ' Dequeue: យកចេញពីមុខជួរ
        While customerQueue.Count > 0
            Dim servedCustomer As String = customerQueue.Dequeue()
            Console.WriteLine($"កំពុងបម្រើសេវាជូន: {servedCustomer}")
        End While

        Console.WriteLine()
        Console.WriteLine("=== ៤. បណ្តុំ Stack(Of T) - ជួរ LIFO (Last-In, First-Out) ===")
        ' ចូលក្រោយ ចេញមុន (ដូចជាការដាក់ចានលើគ្នា ឬប៊ូតុង Undo)
        Dim actionHistory As New Stack(Of String)()

        ' Push: ដាក់បន្ថែមលើគេ
        actionHistory.Push("សកម្មភាព ១: បើកឯកសារ")
        actionHistory.Push("សកម្មភាព ២: វាយអត្ថបទ")
        actionHistory.Push("សកម្មភាព ៣: ប្តូរពណ៌អក្សរ")

        Console.WriteLine($"សកម្មភាពកំពូល (Peek): {actionHistory.Peek()}")

        ' Pop: យកចេញពីលើគេ (Undo)
        While actionHistory.Count > 0
            Dim undoneAction As String = actionHistory.Pop()
            Console.WriteLine($"កំពុងត្រឡប់ថយក្រោយ (Undo): {undoneAction}")
        End While

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
