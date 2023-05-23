using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace StudentManagement
{
    public class StudentManagement
    {
        private string connectionString;
        public StudentManagement(string dbpath)
        {
            connectionString = $"Data Source={dbpath};Version=3;";
        }

        public void InsertSchool(int schoolID,string schoolName)
        {
            string checkIdQuery = "SELECT COUNT(*) FROM School WHERE SchoolID = @schoolID";
            int count;
            using (SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command=new SQLiteCommand(checkIdQuery,connection))
                {
                    command.Parameters.AddWithValue("@schoolID", schoolID);
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            if(count>0)
            {
                Console.WriteLine("School ID already exists.");
            }
            else
            {
                string query = "INSERT INTO School (SchoolID,SchoolName) VALUES (@schoolID,@schoolNmae)";
                using (SQLiteConnection connection=new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command=new SQLiteCommand(query,connection))
                    {
                        command.Parameters.AddWithValue("@schoolID", schoolID);
                        command.Parameters.AddWithValue("@schoolName", schoolName);
                        command.ExecuteNonQuery();
                    }
                }
                //记录操作到日志
                LogOperation("InsertSchool", DateTime.Now.ToString());
            }
        }

        public void InsertClass(int classID,string className,int schoolID)
        {
            string checkIdQuery = "SELECT COUNT (*) FROM Class WHERE ClassID=@classID";
            int count;
            using (SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command=new SQLiteCommand(checkIdQuery,connection))
                {
                    command.Parameters.AddWithValue("@classID", classID);
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            if(count>0)
            {
                Console.WriteLine("ClassID already exists.");
            }
            else
            {
                string query = "INSERT INTO Class (ClassID, ClassName, SchoolID) VALUES(@classID, @className, @schoolID)";
                using (SQLiteConnection connection=new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command=new SQLiteCommand(query,connection))
                    {
                        command.Parameters.AddWithValue("@classID", classID);
                        command.Parameters.AddWithValue("@className", className);
                        command.Parameters.AddWithValue("@schoolID", schoolID);
                        command.ExecuteNonQuery();
                    }
                }
                //记录操作到日志
                LogOperation("InsertClass", DateTime.Now.ToString());
            }
        }

        public void InsertStudent(int studentID,string studentName,int age,int classID)
        {
            string checkIdQuery = "SELECT COUNT(*) FROM Student WHERE StudentID=@studentID";
            int count;
            using (SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command=new SQLiteCommand(checkIdQuery,connection))
                {
                    command.Parameters.AddWithValue("@studentID", studentID);
                    count= Convert.ToInt32(command.ExecuteScalar());
                }
            }
            if(count>0)
            {
                Console.WriteLine("StudentID already exists.");
            }
            else
            {
                string query = "INSERT INTO Student (StudentID, StudentName, Age, ClassID) VALUES (@studentID, @studentName, @age, @classID)";
                using (SQLiteConnection connection=new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command=new SQLiteCommand(query,connection))
                    {
                        command.Parameters.AddWithValue("@studentID", studentID);
                        command.Parameters.AddWithValue("@studentName", studentName);
                        command.Parameters.AddWithValue("@age", age);
                        command.Parameters.AddWithValue("classID", classID);
                        command.ExecuteNonQuery();
                    }
                }
                //记录操作到日志
                LogOperation("InsertStudent", DateTime.Now.ToString());
            }
        }

        public void UpdateStudentAge(int studentID,int newAge)
        {
            string query = "UPDATE Student SET Age=@newAge WHERE StudentID=@studentID";
            using (SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command=new SQLiteCommand(query,connection))
                {
                    command.Parameters.AddWithValue("newAge", newAge);
                    command.Parameters.AddWithValue("studentID", studentID);
                    command.ExecuteNonQuery();          
                }
            }
            //记录操作到日志
            LogOperation("UpdateStudentAge", DateTime.Now.ToString());
        }

        public void DeleteStudent(int studentID)
        {
            string checkIdQuery = "SELECT COUNT(*) FROM Student WHERE StudentID=@studentID";
            int count;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command=new SQLiteCommand(checkIdQuery,connection))
                {
                    command.Parameters.AddWithValue("@studentID", studentID);
                    count=Convert.ToInt32(command.ExecuteScalar());
                }
            }
            if(count==0)
            {
                Console.WriteLine("StudentID is not exites.");
            }
            else
            {
                string query = "DELETE FROM Student WHERE StudentID=@studentID";
                using (SQLiteConnection connection=new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command=new SQLiteCommand(query,connection))
                    {
                        command.Parameters.AddWithValue("@studentID", studentID);
                        command.ExecuteNonQuery();
                    }
                }
                //记录操作到日志
                LogOperation("DeleteStudent", DateTime.Now.ToString());
            }
        }

        public void RetrieveStudentByClass(int classID)
        {
            string query = "SELECT * FROM Student WHERE ClassID = @classID";
            using (SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command=new SQLiteCommand(query,connection))
                {
                    command.Parameters.AddWithValue("@classID", classID);
                    using (SQLiteDataReader reader=command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            int studentID = reader.GetInt32(0);
                            string studentName=reader.GetString(1);
                            int age=reader.GetInt32(2);

                            Console.WriteLine($"Student ID:{ studentID}");
                            Console.WriteLine($"Name: {studentName}");
                            Console.WriteLine($"Age: {age}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        public void RetrieveAllSchools()
        {
            string query = "SELECT * FROM School";
            using (SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command=new SQLiteCommand(query,connection))
                {
                    using (SQLiteDataReader reader=command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            int schoolID=reader.GetInt32(0);
                            string schoolName=reader.GetString(1);
                            Console.WriteLine($"School ID: {schoolID}");
                            Console.WriteLine($"Name: {schoolName}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        public void RetrieveAllClasses()
        {
            string query = "SELECT * FROM Class";
            using (SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command=new SQLiteCommand(query,connection))
                {
                    using (SQLiteDataReader reader=command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            int classID=reader.GetInt32(0);
                            string className=reader.GetString(1);
                            int schoolID = reader.GetInt32(2);

                            Console.WriteLine($"Class ID: {classID}");
                            Console.WriteLine($"Name: {className}");
                            Console.WriteLine($"School ID: {schoolID}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        public void RetrieveAllStudents()
        {
            string query = "SELECT * FROM Student";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int studentID = reader.GetInt32(0);
                            string studentName = reader.GetString(1);
                            int age = reader.GetInt32(2);
                            int classID=reader.GetInt32(3);

                            Console.WriteLine($"Student ID: {studentID}");
                            Console.WriteLine($"Name: {studentName}");
                            Console.WriteLine($"Age {age}");
                            Console.WriteLine($"Class ID: {classID}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
        private void LogOperation(string operation,string timestamp)
        {
            string logQuery = "INSERT INTO Log (Operation, TimeStamp) VALUES (@operation, @timestamp)";
            using (SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command=new SQLiteCommand(logQuery,connection))
                {
                    command.Parameters.AddWithValue("@opreation", operation);
                    command.Parameters.AddWithValue("@timestamp", timestamp);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DisplayLogs()
        {
            string logQuery = "SELECT * FROM Log";
            using(SQLiteConnection connection=new SQLiteConnection(connectionString))
            {
                connection.Open();
                using(SQLiteCommand command=new SQLiteCommand(logQuery,connection))
                {
                    using (SQLiteDataReader reader=command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            int logID=reader.GetInt32(0);
                            string operation =reader.GetString(1);
                            string timestamp =reader.GetString(2);
                            Console.WriteLine($"Log ID: {logID}");
                            Console.WriteLine($"Operation: {operation}");
                            Console.WriteLine($"Timestamp: {timestamp}");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }


    }
    public class Program
    {
        public static void Main()
        {
            string dbPath = @"D:\SQLiteEX-Databases\StudentManagement.db";
            StudentManagement studentManagement = new StudentManagement(dbPath);
            while(true)
            {
                // 显示操作菜单
                Console.WriteLine("请选择要进行的操作：");
                Console.WriteLine("1. 查询所有学校信息");
                Console.WriteLine("2. 查询所有班级信息");
                Console.WriteLine("3. 查询所有学生信息");
                Console.WriteLine("4. 添加学校");
                Console.WriteLine("5. 添加班级");
                Console.WriteLine("6. 添加学生");
                Console.WriteLine("7. 修改学生年龄");
                Console.WriteLine("8. 删除学生");
                Console.WriteLine("9. 显示操作记录");
                Console.WriteLine("10. 退出程序");
                Console.Write("请选择：");
                //读取用户输入的操作编号
                int choice=int.Parse(Console.ReadLine());
                Console.WriteLine(choice);
                //根据用户输入的操作编号执行相应的操作
                switch(choice)
                {
                    case 1://查询所有学校信息
                        studentManagement.RetrieveAllSchools();
                        break;
                    case 2://查询所有班级信息
                        studentManagement.RetrieveAllClasses();
                        break;
                    case 3://查询所有学生信息
                        studentManagement.RetrieveAllStudents();
                        break;
                    case 4://添加学校
                        Console.WriteLine("请输入SchoolID");
                        int schoolID1=int.Parse(Console.ReadLine());
                        Console.WriteLine("请输入SchoolName");
                        string schoolName1=Convert.ToString(Console.ReadLine());
                        studentManagement.InsertSchool(schoolID1, schoolName1);
                        break;
                    case 5://添加班级
                        Console.WriteLine("请输入ClassID");
                        int classID1=int.Parse(Console.ReadLine());
                        Console.WriteLine("请输入ClassName");
                        string className1=Convert.ToString(Console.ReadLine());
                        Console.WriteLine("请输入SchoolID");
                        int schoolID = int.Parse(Console.ReadLine());
                        studentManagement.InsertClass(classID1, className1, schoolID);
                        break;
                    case 6://添加学生
                        Console.WriteLine("请输入StudentID");
                        int studentID1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("请输入StudentName");
                        string studentName1 = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("请输入Age");
                        int age1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("请输入ClassID");
                        int classID = int.Parse(Console.ReadLine());
                        studentManagement.InsertStudent(studentID1, studentName1, age1, classID);
                        break;
                    case 7://修改学生年龄
                        Console.WriteLine("请输入要修改年龄的StudentID");
                        int studentID_updata_age = int.Parse(Console.ReadLine());
                        Console.WriteLine("请输入要修改后的年龄为");
                        int update_age = int.Parse(Console.ReadLine());
                        studentManagement.UpdateStudentAge(studentID_updata_age, update_age);
                        break;
                    case 8://删除学生
                        Console.WriteLine("请输入要删除的StudentID");
                        int studentID_delete = int.Parse(Console.ReadLine());
                        studentManagement.DeleteStudent(studentID_delete);
                        break;
                    case 9://显示操作记录
                        studentManagement.DisplayLogs();
                        break;
                    case 10://退出程序
                        Environment.Exit(0);
                        break;
                    default://输入错误，重新显示菜单
                        Console.WriteLine("输入错误，请重新输入！");
                        break;
                }
            }
        }
    }

}
