using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_Group_Project.Models;
using Final_Group_Project.View;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Group_Project.ViewModel
{
    public partial class AddStudentVM : ObservableObject
    {
        [ObservableProperty]
        public string title;
        [ObservableProperty]
        public string firstName;
        [ObservableProperty]
        public string lastName;
        [ObservableProperty]
        public string regNo;
        [ObservableProperty]
        public string address;
        [ObservableProperty]
        public int telephoneNo;


        public Action CloseAction { get; internal set; }
        public Student CurrentStudent { get; private set; }
        public bool Issaved;


        public AddStudentVM(Student student)
        {
            CurrentStudent = student;
            firstName = student.First_Name;
            lastName = student.Last_Name;
            regNo = student.Reg_No;
            address = student.Address;
            telephoneNo = student.Tel_No;
            
            
        }

        public AddStudentVM() { }

        [RelayCommand]

        public void Save()
        {
            if(CurrentStudent == null) 
            {
                CurrentStudent= new Student()
                {
                    First_Name=FirstName,
                    Last_Name=LastName,
                    Address=Address,
                    Reg_No=RegNo,
                    Tel_No=TelephoneNo
                    
                };

                CloseAction();
                MessageBox.Show("Sucessfully added Student");
            }

            else 
            {
                CurrentStudent.First_Name = FirstName;
                CurrentStudent.Last_Name = LastName;
                CurrentStudent.Reg_No = RegNo;
                CurrentStudent.Address = Address;
                CurrentStudent.Tel_No = TelephoneNo;

                Issaved = true;

                
                MessageBox.Show("Sucessfully Edit Student");
                CloseAction();
            }
        }
    }
}
