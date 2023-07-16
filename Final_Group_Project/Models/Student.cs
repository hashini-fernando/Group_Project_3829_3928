using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Group_Project.Models
{
    public class Student
    {
        [Key]
        public string Reg_No { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set;}

        public int Tel_No { get; set; }

        public string Address { get; set; }

        

        public double Gpa { get; set; }

        public ObservableCollection<StudentModule> StudentModules { get; set; }

        public Student()
        {

        }
    }
}
