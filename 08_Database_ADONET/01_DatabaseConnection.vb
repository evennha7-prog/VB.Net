' ==============================================================================
' មេរៀនទី ៨.១៖ មូលដ្ឋានគ្រឹះ ADO.NET និងការភ្ជាប់ Database (Database Connectivity)
' ==============================================================================

Option Explicit On
Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlClient ' ឬ Microsoft.Data.SqlClient សម្រាប់ .NET Core/.NET 8

Module DatabaseConnectionDemo

    ''' <summary>
    ''' កំណត់ទម្រង់ Connection Strings សម្រាប់ Database ពេញនិយមផ្សេងៗ
    ''' </summary>
    Sub ExplainConnectionStrings()
        Console.WriteLine("=== ១. ទម្រង់ Connection Strings ពេញនិយម ===")

        ' ១. SQL Server (Windows Authentication / Trusted Connection)
        Dim sqlServerTrusted As String = "Server=localhost;Database=PCCFP_SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;"
        Console.WriteLine($"1. SQL Server (Windows Auth):" & vbCrLf & $"   {sqlServerTrusted}")

        ' ២. SQL Server (SQL Authentication ជាមួយ Username/Password)
        Dim sqlServerAuth As String = "Server=192.168.1.100;Database=PCCFP_SchoolDB;User Id=sa;Password=YourStrongPassword123!;"
        Console.WriteLine($"2. SQL Server (SQL Auth):" & vbCrLf & $"   {sqlServerAuth}")

        ' ៣. SQL Server LocalDB (សម្រាប់រៀនសូត្រក្នុង Visual Studio)
        Dim sqlLocalDb As String = "Server=(localdb)\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=D:\Data\SchoolDB.mdf;"
        Console.WriteLine($"3. SQL Server LocalDB:" & vbCrLf & $"   {sqlLocalDb}")

        ' ៤. Microsoft Access DB (.accdb)
        Dim accessDb As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Data\SchoolDB.accdb;"
        Console.WriteLine($"4. MS Access Connection:" & vbCrLf & $"   {accessDb}")
    End Sub

    ''' <summary>
    ''' ស្ថាបត្យកម្ម Disconnected (DataTable & DataAdapter)
    ''' ដំណើរការទាញយកទិន្នន័យមកផ្ទុកក្នុង Memory របស់ Client ហើយផ្តាច់ Connection ភ្លាមៗ (សន្សំធនធាន Server)
    ''' </summary>
    Sub DemonstrateDataTableInMemory()
        Console.WriteLine("\n=== ២. ការប្រើប្រាស់ DataTable ក្នុង Memory (Disconnected Model) ===")

        ' បង្កើត DataTable ដោយផ្ទាល់
        Dim dtStudents As New DataTable("Students")

        ' បង្កើត Columns
        dtStudents.Columns.Add("StudentID", GetType(Integer))
        dtStudents.Columns.Add("FullName", GetType(String))
        dtStudents.Columns.Add("Major", GetType(String))
        dtStudents.Columns.Add("GPA", GetType(Double))

        ' កំណត់ Primary Key
        dtStudents.PrimaryKey = New DataColumn() {dtStudents.Columns("StudentID")}

        ' បញ្ចូលទិន្នន័យ (Rows)
        dtStudents.Rows.Add(101, "កែវ ពិសិដ្ឋ", "Computer Science", 3.85)
        dtStudents.Rows.Add(102, "សុខ ចិន្តា", "Information Technology", 3.92)
        dtStudents.Rows.Add(103, "ម៉ៅ ដារ៉ា", "Data Analytics", 3.45)

        Console.WriteLine($"បានបង្កើត DataTable '{dtStudents.TableName}' មាន {dtStudents.Rows.Count} ជួរ (Rows):")
        Console.WriteLine("---------------------------------------------------------------")
        Console.WriteLine(String.Format("{0,-10} | {1,-20} | {2,-22} | {3,-5}", "StudentID", "FullName", "Major", "GPA"))
        Console.WriteLine("---------------------------------------------------------------")

        For Each row As DataRow In dtStudents.Rows
            Console.WriteLine(String.Format("{0,-10} | {1,-20} | {2,-22} | {3,-5:F2}",
                                            row("StudentID"), row("FullName"), row("Major"), row("GPA")))
        Next
        Console.WriteLine("---------------------------------------------------------------")

        ' ការចម្រាញ់ Select លើ DataTable
        Dim honorsRows As DataRow() = dtStudents.Select("GPA >= 3.80")
        Console.WriteLine($"\nចំនួននិស្សិតឆ្នើម (GPA >= 3.80): {honorsRows.Length} នាក់")
        For Each r In honorsRows
            Console.WriteLine($" -> {r("FullName")} (GPA: {r("GPA")})")
        Next
    End Sub

    ''' <summary>
    ''' កូដគំរូស្តង់ដារសម្រាប់តភ្ជាប់ទៅ SQL Server ពិតប្រាកដ (ជាមួយ Try...Catch & Using)
    ''' </summary>
    Sub SampleSqlServerConnectionCode()
        Console.WriteLine("\n=== ៣. រចនាសម្ព័ន្ធកូដភ្ជាប់ទៅ SQL Server ពិតប្រាកដ ===")

        Dim connectionString As String = "Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;"

        Console.WriteLine("រចនាសម្ព័ន្ធ Best Practice ដោយប្រើ Using Statement:")
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("
        Using conn As New SqlConnection(connectionString)
            Try
                conn.Open()
                Console.WriteLine(""ការភ្ជាប់ Database ជោគជ័យ! ស្ថានភាព: "" & conn.State.ToString())

                Dim query As String = ""SELECT COUNT(*) FROM sys.databases""
                Using cmd As New SqlCommand(query, conn)
                    Dim dbCount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
                    Console.WriteLine(""ចំនួន Database ក្នុង Server: "" & dbCount)
                End Using

            Catch ex As SqlException
                Console.WriteLine(""កំហុស SQL Server: "" & ex.Message)
            Catch ex As Exception
                Console.WriteLine(""កំហុសទូទៅ: "" & ex.Message)
            Finally
                ' Using នឹងបិទ conn ដោយស្វ័យប្រវត្តិ
            End Try
        End Using
        ")
        Console.ResetColor()
    End Sub

    Sub Main()
        Console.OutputEncoding = System.Text.Encoding.UTF8

        ExplainConnectionStrings()
        DemonstrateDataTableInMemory()
        SampleSqlServerConnectionCode()

        Console.WriteLine()
        Console.WriteLine("ចុច Key ណាមួយដើម្បីបញ្ចប់...");
        Console.ReadKey()
    End Sub

End Module
