using System;
using System.IO;
using System.Collections.Generic;

namespace EmployeeTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI uI = new UI();
            uI.UserInterface();
        }
    }

    public class UI         //UserInterface Class
    {
        EmployeeTable et = new EmployeeTable();
        public void UserInterface()
        {
            while (true)
            {
                UIGetFormFile();
                ShowOptions();
            }
        }
        public void ShowOptions()
        {
            bool loop = true;
            while (loop)
            {
                et.Display();
                Console.WriteLine("=============\n");
                Console.WriteLine("1. Add\t2. Delete\n3. Edit\t4. Quit");
                Console.WriteLine("=============\n");
                int ans = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (ans)
                {
                    case 1:
                        UIAdd();
                        break;
                    case 2:
                        UIDelete();
                        break;
                    case 3:
                        UIEdit();
                        break;
                    case 4:
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("please choose one of the following options");
                        break;
                }
                et.save();
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
        }
        public void UIGetFormFile()
        {
            Console.WriteLine("Please enter your file name with .txt (if it does not exist, it will be created:\nif you want to edit the default file (data.txt) press the number 1");
            string ans = Console.ReadLine();
            if (ans != null && ans != "" && ans != " " && ans != "1")
                et.file = ans+".txt";
            if (ans == "1")
                et.file = "data.txt";
            et.GetFormFile();
            et.save();
        }
        private void UIAdd()
        {
            Console.Write("ID:\t");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Firstname:\t");
            string fname = Console.ReadLine();
            Console.Write("Lastname:\t");
            string lname = Console.ReadLine();
            et.Add(id, fname, lname);
        }
        private void UIEdit()
        {
            Console.Write("Old ID:\t");
            int OldID = Convert.ToInt32(Console.ReadLine());
            Console.Write("New ID:\t");
            int NewID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Firstname:\t");
            string fname = Console.ReadLine();
            Console.Write("Lastname:\t");
            string lname = Console.ReadLine();
            et.Edit(OldID, NewID, fname, lname);
        }
        private void UIDelete()
        {
            Console.Write("ID:\t");
            int id = Convert.ToInt32(Console.ReadLine());
            et.Delete(id);
        }
    }

    class Employee
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }

        public new string ToString(string seperator)
        {
            return ID + seperator + FName + seperator + LName;
        }

    }
    class EmployeeTable
    {
        List<Employee> list = new List<Employee>();
        public string file;

        public void Display()       //displayes the list of Employees
        {
            foreach(Employee e in list)
                Console.WriteLine(e.ToString(" "));
        }

        public void GetFormFile()   //To get the data out of an txt file
        {
            using (StreamReader sr = new StreamReader(file))
            {
                while (sr.Peek() > 0)
                {
                    string[] teile = sr.ReadLine().Split(';');
                    Employee emp = new Employee();
                    emp.ID = Convert.ToInt32(teile[0]);
                    emp.FName = teile[1];
                    emp.LName = teile[2];
                    list.Add(emp);
                }
            }
        }
        public void Edit(int OldID, int NewID, string fname, string lname) //To change the data of an Employee
        {
            for (int i = 0; i < list.Count; i++)
            {
                Employee emp = list[i];
                if (emp.ID == OldID)
                {
                    emp.ID = NewID;
                    emp.FName = fname;
                    emp.LName = lname;
                }
            }
        }
        public void Add(int id, string fname, string lname)     //To add a new Employee
        {
            Employee emp = new Employee();
            emp.ID = id;
            emp.FName = fname;
            emp.LName = lname;
            list.Add(emp);
        }

        public void Delete(int id)      //To delete an Employee
        {
            for (int i = 0; i < list.Count; i++)
            {
                Employee emp = list[i];
                if (emp.ID == id)
                    list.Remove(emp);
            }

        }
        public void save()      //to save the changes to the txt file
        {
            using(StreamWriter sw = new StreamWriter(file))
            {
                foreach(Employee e in list)
                    sw.WriteLine(e.ToString(";"));
            }
        }
    }
}
