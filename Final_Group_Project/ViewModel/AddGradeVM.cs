using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_Group_Project.DataBase;
using Final_Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Group_Project.ViewModel
{
    public partial class AddGradeVM : ObservableObject
    {
        [ObservableProperty]
        public Student selectedStudent2;

        [ObservableProperty]
        public ObservableCollection<Module> modules;

        [ObservableProperty]
        public ObservableCollection<Module> listRModule;

        [ObservableProperty]
        public ObservableCollection<StudentModule> listSModule;

        [ObservableProperty]
        public ObservableCollection<StudentModule> gradeList;

        [ObservableProperty]
        public Module selectedModule1;

        [ObservableProperty]
        public double marks;
        DataBaseContest db;

        public AddGradeVM() { }

        public AddGradeVM(Student student)
        {
            SelectedStudent2 = student;
            db = new DataBaseContest();
            ListSModule = new ObservableCollection<StudentModule>(db.studentModules.ToList());
            Modules = new ObservableCollection<Module>(db.Modules.ToList());
            LoadRModules();
            LoadGradeList();

        }

        public void LoadGradeList()
        {
            GradeList = new ObservableCollection<StudentModule>();
            foreach (var sm in ListSModule)
            {
                if (sm.Student_Reg == SelectedStudent2.Reg_No)
                {
                    GradeList.Add(sm);
                }
            }
        }

        public void LoadRModules()
        {
            ListRModule = new ObservableCollection<Module>();
            foreach (var M in ListSModule)
            {
                if (M.Student_Reg == SelectedStudent2.Reg_No)
                {
                    if (M.Module_Code != null)
                    {
                        foreach (var m in Modules)
                        {
                            if (m.Code.Equals(M.Module_Code))
                            {
                                ListRModule.Add(m);
                            }
                        }
                    }
                    else
                        MessageBox.Show("Error10");
                }
            }
        }

        [RelayCommand]
        public void GetGrade()
        {
            foreach (var sm in GradeList)
            {
                if (sm.Module_Code == SelectedModule1.Code)
                {
                    if (Marks >= 85)
                        sm.Grade = "A+";
                    else if (Marks >= 75)
                        sm.Grade = "A";
                    else if (Marks >= 70)
                        sm.Grade = "A-";
                    else if (Marks >= 65)
                        sm.Grade = "B+";
                    else if (Marks >= 60)
                        sm.Grade = "B";
                    else if (Marks >= 55)
                        sm.Grade = "B-";
                    else if (Marks >= 50)
                        sm.Grade = "C+";
                    else if (Marks >= 45)
                        sm.Grade = "C";
                    else if (Marks >= 40)
                        sm.Grade = "C-";
                    else
                        sm.Grade = "E";

                    var studentmoduleToUp = db.studentModules.FirstOrDefault(sm => sm.Student_Reg == SelectedStudent2.Reg_No && sm.Module_Code == SelectedModule1.Code);

                    if (studentmoduleToUp != null)
                    {
                        studentmoduleToUp.Grade = sm.Grade;
                        studentmoduleToUp.Marks = Marks;
                        db.SaveChanges();
                        LoadGradeList();
                    }
                    else
                        MessageBox.Show("Error 200");
                }
                LoadGradeList();
            }
            Marks = 0;
        }

        [RelayCommand]
        public void CalGPA()
        {
            if (SelectedStudent2 != null)
            {
                double GPCredit = 0.0;
                double Totalcredit = 0.0;

                foreach (var m in ListRModule)
                {
                    foreach (var sm in GradeList)
                    {
                        if (m.Code == sm.Module_Code)
                        {
                            int Credit = m.Credit;
                            double GradePoint = 0.0;
                            string Grade = sm.Grade;

                            if (Grade == "A+")
                                GradePoint = 4.0;
                            else if (Grade == "A")
                                GradePoint = 4.0;
                            else if (Grade == "A-")
                                GradePoint = 3.7;
                            else if (Grade == "B+")
                                GradePoint = 3.3;
                            else if (Grade == "B")
                                GradePoint = 3.0;
                            else if (Grade == "B-")
                                GradePoint = 2.7;
                            else if (Grade == "C+")
                                GradePoint = 2.3;
                            else if (Grade == "C")
                                GradePoint = 2.0;
                            else if (Grade == "C-")
                                GradePoint = 1.7;
                            else
                                GradePoint = 0.0;

                            GPCredit += GradePoint * Credit;
                            Totalcredit += Credit;

                        }
                    }
                }

                double GPA = Math.Round((GPCredit / Totalcredit), 4);

                var ToUpStudent = db.Students.FirstOrDefault(s => s.Reg_No == SelectedStudent2.Reg_No);

                if (ToUpStudent != null)
                {
                    ToUpStudent.Gpa = GPA;
                    db.SaveChanges();
                    MessageBox.Show("GPA Calculated..!");
                }
                else
                    MessageBox.Show("Error 100");
            }
        }
    }
}
