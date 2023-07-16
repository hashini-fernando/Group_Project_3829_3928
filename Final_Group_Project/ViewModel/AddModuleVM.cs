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
using System.Windows.Controls;
using System.Windows.Documents;


namespace Final_Group_Project.ViewModel
{
    public partial class AddModuleVM : ObservableObject
    {
        [ObservableProperty]
        public Student selectedStudent1;

        [ObservableProperty]
        public Module selectedModule;

        [ObservableProperty]
        public Module selectedModule1;

        [ObservableProperty]
        public ObservableCollection<Module> listAllModule;

        [ObservableProperty]
        public ObservableCollection<Module> listRegModules;

        [ObservableProperty]
        public ObservableCollection<StudentModule> studentModules;


        [ObservableProperty]
        public int mark;

        DataBaseContest moduledb;

        public AddModuleVM()
        {

        }

        public AddModuleVM(Student student)
        {
            selectedStudent1 = student;
            moduledb = new DataBaseContest();
            ListAllModule = new ObservableCollection<Module>(moduledb.Modules.ToList());
            StudentModules = new ObservableCollection<StudentModule>(moduledb.studentModules.ToList());
            LoadRegModules();
        }

        public void LoadRegModules()
        {
            ListRegModules = new ObservableCollection<Module>();
            foreach (var M in studentModules)
            {
                if (M.Student_Reg == SelectedStudent1.Reg_No)
                {
                    if (M.Module != null)
                    {
                        ListRegModules.Add(M.Module);
                    }
                    else
                        MessageBox.Show("Error1000");
                }
            }
        }

        

        [RelayCommand]
        public void Register()
        {

            if (selectedStudent1 != null && SelectedModule != null)
            {
                bool isregisted = moduledb.studentModules.Any(sm => sm.Module_Code == SelectedModule.Code && sm.Student_Reg == SelectedStudent1.Reg_No);
                if (isregisted == false)
                {
                    var studentModule = new StudentModule
                    {
                        Module_Code = SelectedModule.Code,
                        Student_Reg = SelectedStudent1.Reg_No,
                        Grade = "None"
                    };
                    moduledb.studentModules.Add(studentModule);
                    moduledb.SaveChanges();
                    ListRegModules.Add(studentModule.Module);
                    
                }
                else
                    MessageBox.Show("All ready Registed");
            }
            else
                MessageBox.Show("Error");

  

        }

        [RelayCommand]
        public void Remove()
        {
            if (SelectedModule != null && SelectedStudent1 != null)
            {
                bool isregisted = moduledb.studentModules.Any(sm => sm.Module_Code == SelectedModule.Code && sm.Student_Reg == SelectedStudent1.Reg_No);
                if (isregisted)
                {
                    var smToDelete = moduledb.studentModules.FirstOrDefault(sm => sm.Module_Code == SelectedModule.Code && sm.Student_Reg == SelectedStudent1.Reg_No);
                    moduledb.studentModules.Remove(smToDelete);
                    moduledb.SaveChanges();
                    MessageBox.Show("All Ready Removed..!");
                    ListRegModules.Remove(SelectedModule);
                }
            }
            else
            { MessageBox.Show("First Select the Module..!"); }
        }
    }
}
