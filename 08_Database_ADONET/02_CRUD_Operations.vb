' ==============================================================================
' មេរៀនទី ៨.២៖ ប្រតិបត្តិការ CRUD និងសុវត្ថិភាព Parameterized Queries
' (Create, Read, Update, Delete & SQL Injection Prevention)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Data Access Layer (DAL) សម្រាប់គ្រប់គ្រងទិន្នន័យនិស្សិត
''' បង្ហាញពីការប្រើប្រាស់ Parameterized Queries ដើម្បីការពារ SQL Injection 100%
''' </summary>
Public Class StudentDataAccessLayer

    Private _connectionString As String

    Public Sub New(connString As String)
        _connectionString = connString
    End Sub

    ' ១. CREATE (INSERT): បន្ថែមទិន្នន័យសិស្សថ្មី
    Public Function InsertStudent(fullName As String, gender As String, age As Integer, email As String) As Integer
        ' ការពារ SQL Injection៖ ប្រើប្រាស់ @Parameter ជានិច្ច ហាមភ្ជាប់ String ដោយសញ្ញា & ក្នុង SQL!
        Dim sql As String = "INSERT INTO Students (FullName, Gender, Age, Email) " &
                            "VALUES (@FullName, @Gender, @Age, @Email); " &
                            "SELECT SCOPE_IDENTITY();" ' យក Auto-Generated ID ត្រឡប់មកវិញ

        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(sql, conn)
                ' បញ្ចូល Parameters
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = fullName
                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = gender
                cmd.Parameters.Add("@Age", SqlDbType.Int).Value = age
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email

                conn.Open()
                ' ExecuteScalar យកតម្លៃបន្ទាត់ដំបូង ជួរឈរដំបូង (SCOPE_IDENTITY)
                Dim newId As Object = cmd.ExecuteScalar()
                Return Convert.ToInt32(newId)
            End Using
        End Using
    End Function

    ' ២. READ (SELECT): ទាញយកទិន្នន័យសិស្សទាំងអស់
    Public Function GetAllStudents() As DataTable
        Dim dt As New DataTable()
        Dim sql As String = "SELECT StudentID, FullName, Gender, Age, Email FROM Students ORDER BY StudentID DESC"

        Using conn As New SqlConnection(_connectionString)
            Using adapter As New SqlDataAdapter(sql, conn)
                ' DataAdapter បើក និងបិទ Connection ដោយស្វ័យប្រវត្តិតាមរយៈ Fill()
                adapter.Fill(dt)
            End Using
        End Using

        Return dt
    End Function

    ' ៣. UPDATE: កែប្រែព័ត៌មានសិស្សតាម StudentID
    Public Function UpdateStudent(studentId As Integer, newName As String, newAge As Integer) As Boolean
        Dim sql As String = "UPDATE Students SET FullName = @FullName, Age = @Age WHERE StudentID = @StudentID"

        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = newName
                cmd.Parameters.Add("@Age", SqlDbType.Int).Value = newAge
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId

                conn.Open()
                ' ExecuteNonQuery ត្រឡប់ចំនួនជួរដែលរងផលប៉ះពាល់ (Rows Affected)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected > 0
            End Using
        End Using
    End Function

    ' ៤. DELETE: លុបទិន្នន័យសិស្សតាម StudentID
    Public Function DeleteStudent(studentId As Integer) As Boolean
        Dim sql As String = "DELETE FROM Students WHERE StudentID = @StudentID"

        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId

                conn.Open()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected > 0
            End Using
        End Using
    End Function

    ' ៥. TRANSACTION (ការការពារទិន្នន័យពេលប្រតិបត្តិការច្រើនជំហាន): Commit & Rollback
    Public Sub TransferCredit(fromStudentId As Integer, toStudentId As Integer, credits As Integer)
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            ' ចាប់ផ្តើម Transaction
            Dim transaction As SqlTransaction = conn.BeginTransaction()

            Try
                ' ជំហានទី ១: ដកក្រេឌីតពីសិស្ស A
                Dim sqlDeduct As String = "UPDATE Students SET Credits = Credits - @Credits WHERE StudentID = @FromID"
                Using cmd1 As New SqlCommand(sqlDeduct, conn, transaction)
                    cmd1.Parameters.AddWithValue("@Credits", credits)
                    cmd1.Parameters.AddWithValue("@FromID", fromStudentId)
                    cmd1.ExecuteNonQuery()
                End Using

                ' ជំហានទី ២: បន្ថែមក្រេឌីតឱ្យសិស្ស B
                Dim sqlAdd As String = "UPDATE Students SET Credits = Credits + @Credits WHERE StudentID = @ToID"
                Using cmd2 As New SqlCommand(sqlAdd, conn, transaction)
                    cmd2.Parameters.AddWithValue("@Credits", credits)
                    cmd2.Parameters.AddWithValue("@ToID", toStudentId)
                    cmd2.ExecuteNonQuery()
                End Using

                ' បើជោគជ័យទាំងពីរជំហាន ធ្វើការ Commit រក្សាទុកជាផ្លូវការ
                transaction.Commit()
                Console.WriteLine("[Transaction ជោគជ័យ]: ក្រេឌីតត្រូវបានផ្ទេររួចរាល់!")

            Catch ex As Exception
                ' បើមានកំហុសជំហានណាមួយ ធ្វើការ Rollback ត្រឡប់ទៅសភាពដើមវិញភ្លាម
                transaction.Rollback()
                Console.WriteLine($"[Transaction បរាជ័យ]: បាន Rollback ត្រឡប់សភាពដើម! មូលហេតុ: {ex.Message}")
            End Try
        End Using
    End Sub

End Class

Module CrudOperationsDemo

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        Console.WriteLine("================================================================")
        Console.WriteLine("  មេរៀនប្រតិបត្តិការ CRUD (Create, Read, Update, Delete) ក្នុង ADO.NET")
        Console.WriteLine("================================================================")

        Console.WriteLine("
        ចំណុចសំខាន់ៗនៃមេរៀននេះ៖
        ១. Parameterized Queries:
           - ជៀសវាងការភ្ជាប់ខ្សែអក្សរ (String Concatenation) ផ្ទាល់ទៅក្នុង SQL
           - ឧទាហរណ៍អាក្រក់៖ ""SELECT * FROM Users WHERE User='"" & input & ""'"" -> ងាយរងគ្រោះ SQL Injection!
           - ឧទាហរណ៍ល្អ៖ ប្រើ cmd.Parameters.Add(""@User"", SqlDbType.NVarChar).Value = input

        ២. Execute Methods ទាំង ៣៖
           - ExecuteNonQuery(): ប្រើសម្រាប់ INSERT, UPDATE, DELETE (ត្រឡប់ចំនួន Row ដែលប៉ះពាល់)
           - ExecuteScalar(): ប្រើសម្រាប់ទាញយកតម្លៃតែមួយ (COUNT, MAX, SUM, SCOPE_IDENTITY)
           - ExecuteReader(): ប្រើសម្រាប់អានទិន្នន័យជួរដេកតាមលំដាប់ (Forward-only, Read-only Fast Stream)

        ៣. Database Transactions:
           - ប្រើប្រាស់ SqlTransaction ដើម្បីធានាគោលការណ៍ ACID (Atomicity, Consistency, Isolation, Durability)
           - បើមាន Error ក្នុងដំណាក់កាលណាមួយ ត្រូវហៅ transaction.Rollback()
           - បើជោគជ័យគ្រប់ដំណាក់កាល ហៅ transaction.Commit()
        ")

        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
