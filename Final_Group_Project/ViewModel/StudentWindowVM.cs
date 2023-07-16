using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_Group_Project.DataBase;
using Final_Group_Project.Models;
using Final_Group_Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace Final_Group_Project.ViewModel
{
    public partial class StudentWindowVM : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Student> listofStudent;

        [ObservableProperty]
        public Student selectedStudent;

        DataBaseContest studentData;

        public StudentWindowVM()
        {
            studentData = new DataBaseContest();
            LoadStudent();
        }

        private void LoadStudent()
        {
            ListofStudent = new ObservableCollection<Student>(studentData.Students.ToList());
        }

        [RelayCommand]
        public void AddStudent()
        {
            var addstudentvm = new AddStudentVM
            {
                Title = "ADD STUDENT"
            };

            AddStudent addStudentwindow = new AddStudent(addstudentvm);
            addStudentwindow.ShowDialog();

            if (addstudentvm.CurrentStudent != null)
            {
                studentData.Students.Add(addstudentvm.CurrentStudent);
                studentData.SaveChanges();
                ListofStudent.Add(addstudentvm.CurrentStudent);
            }
            else
                return;

        }

        [RelayCommand]
        public void EditStudent()
        {
            if (SelectedStudent != null)
            {
                var vm = new AddStudentVM(SelectedStudent);
                vm.Title = "EDIT STUDENT";
                var window = new AddStudent(vm);

                window.ShowDialog();

                if (vm.Issaved)
                {
                    var studentToUpdate = studentData.Students.FirstOrDefault(u => u.Reg_No == SelectedStudent.Reg_No);
                    if (studentToUpdate != null)
                    {
                        studentToUpdate.Reg_No = vm.CurrentStudent.Reg_No;
                        studentToUpdate.First_Name = vm.CurrentStudent.First_Name;
                        studentToUpdate.Last_Name = vm.CurrentStudent.Last_Name;
                        studentToUpdate.Address = vm.CurrentStudent.Address;
                        studentToUpdate.Tel_No = vm.CurrentStudent.Tel_No;

                        studentData.SaveChanges();
                        int index = ListofStudent.IndexOf(SelectedStudent);
                        ListofStudent.RemoveAt(index);
                        ListofStudent.Insert(index, studentToUpdate);
                    }
                }
            }
            else
                MessageBox.Show("Please Select Student");
        }

        [RelayCommand]
        public void Delete()
        {
            if (SelectedStudent != null)
            {
                studentData.Remove(SelectedStudent);
                studentData.SaveChanges();
                MessageBox.Show("Student Sucessfuly Delete");
                ListofStudent.Remove(SelectedStudent);
            }

            else
                MessageBox.Show("Please Select Student");
        }

        [RelayCommand]
        public void AddModule()
        {
            if (SelectedStudent != null)
            {
                var vm = new AddModuleVM(SelectedStudent);
                var window = new AddModule(vm);
                window.ShowDialog();
            }
            else
                MessageBox.Show("Please Select Student");
        }

        [RelayCommand]
        public void AddGrade()
        {
            if (SelectedStudent != null)
            {
                var vm = new AddGradeVM(SelectedStudent);
                var window = new AddGrade(vm);
                window.ShowDialog();
            }
            else
                MessageBox.Show("Please Select Student");
        }

        [RelayCommand]
        public void ShowResult()
        {
            if (SelectedStudent != null)
            {
                var vm = new ResultWindowVM(SelectedStudent);
                var window = new ResultWindow(vm);
                window.ShowDialog();
            }
            else
                MessageBox.Show("Please Select Student");
        }
    }
}
