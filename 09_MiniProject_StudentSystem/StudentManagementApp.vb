' ==============================================================================
' គម្រោងខ្នាតតូចជាក់ស្តែង (Mini Project): ប្រព័ន្ធគ្រប់គ្រងព័ត៌មានសិស្ស (Student Management System)
' រៀបចំ និងរចនាដោយ៖ PCCFP Institute
' បច្ចេកវិទ្យាបញ្ចូលគ្នា៖ OOP, Properties, Generic Lists, LINQ, File I/O, Exception Handling
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text

Namespace PCCFP.StudentSystem

    ''' <summary>
    ''' Entity Model តំណាងឱ្យសិស្សម្នាក់
    ''' </summary>
    Public Class StudentModel
        Public Property Id As Integer
        Public Property FullName As String
        Public Property Gender As String
        Public Property Major As String
        Public Property Score As Double

        Public ReadOnly Property Grade As String
            Get
                Select Case Score
                    Case Is >= 90 : Return "A (ល្អឥតខ្ចោះ)"
                    Case Is >= 80 : Return "B (ល្អណាស់)"
                    Case Is >= 70 : Return "C (ល្អ)"
                    Case Is >= 60 : Return "D (មធ្យម)"
                    Case Is >= 50 : Return "E (ខ្សោយ)"
                    Case Else : Return "F (ធ្លាក់)"
                End Select
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Sub New(id As Integer, name As String, gender As String, major As String, score As Double)
            Me.Id = id
            Me.FullName = name
            Me.Gender = gender
            Me.Major = major
            Me.Score = score
        End Sub

        Public Function ToCsv() As String
            Return $"{Id},{FullName},{Gender},{Major},{Score}"
        End Function
    End Class

    ''' <summary>
    ''' Service សម្រាប់គ្រប់គ្រងទិន្នន័យសិស្ស (Business Logic & LINQ)
    ''' </summary>
    Public Class StudentService
        Private _students As New List(Of StudentModel)()
        Private ReadOnly _dataFilePath As String

        Public Sub New()
            _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students_data.csv")
            LoadInitialData()
        End Sub

        Private Sub LoadInitialData()
            If File.Exists(_dataFilePath) Then
                LoadFromFile()
            Else
                ' ទិន្នន័យគំរូដំបូង ប្រសិនបើមិនទាន់មាន File
                _students.Add(New StudentModel(1001, "សុខ ចិន្តា", "ស្រី", "វិទ្យាសាស្ត្រកុំព្យូទ័រ", 92.5))
                _students.Add(New StudentModel(1002, "កែវ ពិសិដ្ឋ", "ប្រុស", "ព័ត៌មានវិទ្យា", 78.0))
                _students.Add(New StudentModel(1003, "ម៉ៅ ដារ៉ា", "ប្រុស", "វិទ្យាសាស្ត្រទិន្នន័យ", 85.0))
                _students.Add(New StudentModel(1004, "ចាន់ ធារ៉ា", "ស្រី", "វិទ្យាសាស្ត្រកុំព្យូទ័រ", 45.0))
                SaveToFile()
            End If
        End Sub

        Public Function GetAll() As List(Of StudentModel)
            Return _students
        End Function

        Public Function AddStudent(s As StudentModel) As Boolean
            If _students.Any(Function(x) x.Id = s.Id) Then
                Return False ' ID ស្ទួន
            End If
            _students.Add(s)
            SaveToFile()
            Return True
        End Function

        Public Function FindById(id As Integer) As StudentModel
            Return _students.FirstOrDefault(Function(x) x.Id = id)
        End Function

        Public Function SearchByName(keyword As String) As List(Of StudentModel)
            Return _students.Where(Function(x) x.FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList()
        End Function

        Public Function DeleteStudent(id As Integer) As Boolean
            Dim student = FindById(id)
            If student IsNot Nothing Then
                _students.Remove(student)
                SaveToFile()
                Return True
            End If
            Return False
        End Function

        Public Function UpdateStudent(id As Integer, newName As String, newMajor As String, newScore As Double) As Boolean
            Dim student = FindById(id)
            If student IsNot Nothing Then
                student.FullName = newName
                student.Major = newMajor
                student.Score = newScore
                SaveToFile()
                Return True
            End If
            Return False
        End Function

        Public Sub SaveToFile()
            Using writer As New StreamWriter(_dataFilePath, False, Encoding.UTF8)
                writer.WriteLine("ID,FullName,Gender,Major,Score")
                For Each s As StudentModel In _students
                    writer.WriteLine(s.ToCsv())
                Next
            End Using
        End Sub

        Public Sub LoadFromFile()
            If Not File.Exists(_dataFilePath) Then Return
            _students.Clear()
            Dim lines = File.ReadAllLines(_dataFilePath, Encoding.UTF8)
            For i As Integer = 1 To lines.Length - 1
                Dim line = lines(i).Trim()
                If Not String.IsNullOrEmpty(line) Then
                    Dim parts = line.Split(","c)
                    If parts.Length >= 5 Then
                        _students.Add(New StudentModel(Convert.ToInt32(parts(0)), parts(1), parts(2), parts(3), Convert.ToDouble(parts(4))))
                    End If
                End If
            Next
        End Sub
    End Class

    ''' <summary>
    ''' Main Program / Console UI Layer
    ''' </summary>
    Module Program

        Private service As New StudentService()

        Sub Main()
            Console.OutputEncoding = Encoding.UTF8
            Dim isRunning As Boolean = True

            While isRunning
                Console.Clear()
                Console.ForegroundColor = ConsoleColor.Cyan
                Console.WriteLine("==================================================================")
                Console.WriteLine("          ប្រព័ន្ធគ្រប់គ្រងព័ត៌មានសិស្ស (PCCFP STUDENT SYSTEM)    ")
                Console.WriteLine("==================================================================")
                Console.ResetColor()

                Console.WriteLine("  [1]. បង្ហាញបញ្ជីសិស្សទាំងអស់ (View All Students)")
                Console.WriteLine("  [2]. បន្ថែមសិស្សថ្មី (Add New Student)")
                Console.WriteLine("  [3]. ស្វែងរកសិស្ស (Search by ID or Name)")
                Console.WriteLine("  [4]. កែប្រែព័ត៌មានសិស្ស (Update Student Info)")
                Console.WriteLine("  [5]. លុបទិន្នន័យសិស្ស (Delete Student)")
                Console.WriteLine("  [6]. របាយការណ៍ស្ថិតិ និងចំណាត់ថ្នាក់ (Statistics & Ranking)")
                Console.WriteLine("  [0]. ចាកចេញពីកម្មវិធី (Exit)")
                Console.WriteLine("==================================================================")
                Console.Write("សូមជ្រើសរើសជម្រើសរបស់អ្នក [0-6]: ")

                Dim choice As String = Console.ReadLine()

                Select Case choice
                    Case "1" : DisplayAllStudents()
                    Case "2" : AddNewStudentUI()
                    Case "3" : SearchStudentUI()
                    Case "4" : UpdateStudentUI()
                    Case "5" : DeleteStudentUI()
                    Case "6" : ShowStatisticsReport()
                    Case "0"
                        isRunning = False
                        Console.WriteLine("\nសូមអរគុណដែលបានប្រើប្រាស់ប្រព័ន្ធ! សូមជម្រាបលា។")
                    Case Else
                        ShowMessage("ជម្រើសមិនត្រឹមត្រូវទេ! សូមព្យាយាមម្តងទៀត។", ConsoleColor.Red)
                End Select

                If isRunning Then
                    Console.WriteLine("\nចុច Key ណាមួយដើម្បីត្រឡប់ទៅ Menu ដើម...")
                    Console.ReadKey()
                End If
            End While
        End Sub

        Private Sub DisplayAllStudents()
            Dim list = service.GetAll()
            Console.Clear()
            Console.WriteLine($"=== បញ្ជីសិស្សទាំងអស់ ({list.Count} នាក់) ===")
            PrintTable(list)
        End Sub

        Private Sub AddNewStudentUI()
            Console.Clear()
            Console.WriteLine("=== បញ្ចូលសិស្សថ្មី ===")
            Try
                Console.Write("បញ្ចូលលេខសម្គាល់ ID (ឧទាហរណ៍: 1005): ")
                Dim id As Integer = Convert.ToInt32(Console.ReadLine())

                Console.Write("បញ្ចូលឈ្មោះសិស្ស: ")
                Dim name As String = Console.ReadLine().Trim()

                Console.Write("បញ្ចូលភេទ (ប្រុស/ស្រី): ")
                Dim gender As String = Console.ReadLine().Trim()

                Console.Write("បញ្ចូលជំនាញ (Major): ")
                Dim major As String = Console.ReadLine().Trim()

                Console.Write("បញ្ចូលពិន្ទុសរុប (0-100): ")
                Dim score As Double = Convert.ToDouble(Console.ReadLine())

                Dim newStudent As New StudentModel(id, name, gender, major, score)
                If service.AddStudent(newStudent) Then
                    ShowMessage("បានបន្ថែមសិស្សដោយជោគជ័យ!", ConsoleColor.Green)
                Else
                    ShowMessage("លេខសម្គាល់ ID នេះមានរួចហើយក្នុងប្រព័ន្ធ!", ConsoleColor.Red)
                End If
            Catch ex As Exception
                ShowMessage($"កំហុសបញ្ចូលទិន្នន័យ: {ex.Message}", ConsoleColor.Red)
            End Try
        End Sub

        Private Sub SearchStudentUI()
            Console.Clear()
            Console.WriteLine("=== ស្វែងរកព័ត៌មានសិស្ស ===")
            Console.Write("បញ្ចូលឈ្មោះ ឬពាក្យគន្លឹះដើម្បីស្វែងរក: ")
            Dim keyword As String = Console.ReadLine().Trim()

            Dim results = service.SearchByName(keyword)
            If results.Count > 0 Then
                Console.WriteLine($"\nរកឃើញសិស្សចំនួន {results.Count} នាក់:")
                PrintTable(results)
            Else
                ShowMessage($"រកមិនឃើញសិស្សដែលមានឈ្មោះ '{keyword}' ឡើយ!", ConsoleColor.Yellow)
            End If
        End Sub

        Private Sub UpdateStudentUI()
            Console.Clear()
            Console.WriteLine("=== កែប្រែព័ត៌មានសិស្ស ===")
            Try
                Console.Write("បញ្ចូល ID សិស្សដែលត្រូវកែប្រែ: ")
                Dim id As Integer = Convert.ToInt32(Console.ReadLine())

                Dim student = service.FindById(id)
                If student Is Nothing Then
                    ShowMessage($"រកមិនឃើញសិស្ស ID {id} ឡើយ!", ConsoleColor.Red)
                    Return
                End If

                Console.WriteLine($"សិស្សបច្ចុប្បន្ន៖ {student.FullName} | ជំនាញ៖ {student.Major} | ពិន្ទុ៖ {student.Score}")
                Console.Write("បញ្ចូលឈ្មោះថ្មី (ទុកទទេបើមិនប្តូរ): ")
                Dim newName As String = Console.ReadLine().Trim()
                If String.IsNullOrEmpty(newName) Then newName = student.FullName

                Console.Write("បញ្ចូលជំនាញថ្មី (ទុកទទេបើមិនប្តូរ): ")
                Dim newMajor As String = Console.ReadLine().Trim()
                If String.IsNullOrEmpty(newMajor) Then newMajor = student.Major

                Console.Write("បញ្ចូលពិន្ទុថ្មី (បញ្ចូល -1 បើមិនប្តូរ): ")
                Dim scoreInput As Double = Convert.ToDouble(Console.ReadLine())
                Dim newScore As Double = If(scoreInput < 0, student.Score, scoreInput)

                If service.UpdateStudent(id, newName, newMajor, newScore) Then
                    ShowMessage("បានកែប្រែទិន្នន័យសិស្សដោយជោគជ័យ!", ConsoleColor.Green)
                End If
            Catch ex As Exception
                ShowMessage($"កំហុស: {ex.Message}", ConsoleColor.Red)
            End Try
        End Sub

        Private Sub DeleteStudentUI()
            Console.Clear()
            Console.WriteLine("=== លុបទិន្នន័យសិស្ស ===")
            Try
                Console.Write("បញ្ចូល ID សិស្សដែលចង់លុប: ")
                Dim id As Integer = Convert.ToInt32(Console.ReadLine())

                Console.Write($"តើអ្នកពិតជាចង់លុបសិស្ស ID {id} មែនទេ? (Y/N): ")
                Dim confirm As String = Console.ReadLine().Trim().ToUpper()

                If confirm = "Y" Then
                    If service.DeleteStudent(id) Then
                        ShowMessage("បានលុបទិន្នន័យសិស្សដោយជោគជ័យ!", ConsoleColor.Green)
                    Else
                        ShowMessage($"រកមិនឃើញសិស្ស ID {id} ឡើយ!", ConsoleColor.Red)
                    End If
                Else
                    ShowMessage("បានបោះបង់ការលុប។", ConsoleColor.Yellow)
                End If
            Catch ex As Exception
                ShowMessage($"កំហុស: {ex.Message}", ConsoleColor.Red)
            End Try
        End Sub

        Private Sub ShowStatisticsReport()
            Dim list = service.GetAll()
            Console.Clear()
            Console.WriteLine("==================================================================")
            Console.WriteLine("                    របាយការណ៍ស្ថិតិ និងការវិភាគ (LINQ)            ")
            Console.WriteLine("==================================================================")

            If list.Count = 0 Then
                ShowMessage("មិនទាន់មានទិន្នន័យសិស្សសម្រាប់ធ្វើស្ថិតិនៅឡើយទេ!", ConsoleColor.Yellow)
                Return
            End If

            Dim total = list.Count
            Dim passedCount = list.Count(Function(s) s.Score >= 50.0)
            Dim failedCount = total - passedCount
            Dim avgScore = list.Average(Function(s) s.Score)
            Dim topStudent = list.OrderByDescending(Function(s) s.Score).First()
            Dim lowestStudent = list.OrderBy(Function(s) s.Score).First()

            Console.WriteLine($" * ចំនួនសិស្សសរុប     : {total} នាក់")
            Console.WriteLine($" * ចំនួនសិស្សជាប់ (>=50): {passedCount} នាក់ ({(passedCount / total) * 100:F1}%)")
            Console.WriteLine($" * ចំនួនសិស្សធ្លាក់ (<50) : {failedCount} នាក់ ({(failedCount / total) * 100:F1}%)")
            Console.WriteLine($" * មធ្យមភាគពិន្ទុទូទៅ    : {avgScore:F2} ពិន្ទុ")
            Console.WriteLine($" * សិស្សពិន្ទុខ្ពស់បំផុត : {topStudent.FullName} ({topStudent.Score} ពិន្ទុ - និទ្ទេស {topStudent.Grade})")
            Console.WriteLine($" * សិស្សពិន្ទុទាបបំផុត   : {lowestStudent.FullName} ({lowestStudent.Score} ពិន្ទុ)")

            Console.WriteLine("\n--- ចំណាត់ថ្នាក់តាមជំនាញឯកទេស (Group by Major) ---")
            Dim majorGroups = list.GroupBy(Function(s) s.Major)
            For Each grp In majorGroups
                Console.WriteLine($" [ជំនាញ៖ {grp.Key}] : {grp.Count()} នាក់ | មធ្យមភាគ: {grp.Average(Function(x) x.Score):F2}")
            Next
        End Sub

        Private Sub PrintTable(list As List(Of StudentModel))
            Console.WriteLine("--------------------------------------------------------------------------------")
            Console.WriteLine(String.Format("{0,-8} | {1,-18} | {2,-6} | {3,-22} | {4,-6} | {5}", "ID", "ឈ្មោះសិស្ស", "ភេទ", "ជំនាញ", "ពិន្ទុ", "និទ្ទេស"))
            Console.WriteLine("--------------------------------------------------------------------------------")
            For Each s In list
                Console.WriteLine(String.Format("{0,-8} | {1,-18} | {2,-6} | {3,-22} | {4,-6:F1} | {5}",
                                                s.Id, s.FullName, s.Gender, s.Major, s.Score, s.Grade))
            Next
            Console.WriteLine("--------------------------------------------------------------------------------")
        End Sub

        Private Sub ShowMessage(msg As String, color As ConsoleColor)
            Console.ForegroundColor = color
            Console.WriteLine($"\n[សារ]: {msg}")
            Console.ResetColor()
        End Sub

    End Module

End Namespace
