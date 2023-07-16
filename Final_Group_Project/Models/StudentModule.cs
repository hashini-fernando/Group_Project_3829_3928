using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Group_Project.Models
{
    public class StudentModule
    {
        public string Student_Reg { get; set; }
        public Student Student {  get; set; }
        public string Module_Code { get; set; }
        public Module Module { get; set; }
        public double Marks { get; set; }
        public string Grade { get; set; }

        public StudentModule() { }


    }
}
