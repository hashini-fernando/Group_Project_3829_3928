using CommunityToolkit.Mvvm.ComponentModel;
using Final_Group_Project.DataBase;
using Final_Group_Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Group_Project.ViewModel
{
    public partial class ResultWindowVM : ObservableObject
    {
        [ObservableProperty]
        public Student selectedStudent3;

        [ObservableProperty]
        public ObservableCollection<Module> listRMod;

        [ObservableProperty]
        public ObservableCollection<Module> listMod;

        [ObservableProperty]
        public ObservableCollection<StudentModule> listSM;

        [ObservableProperty]
        public ObservableCollection<StudentModule> listGrade;

        DataBaseContest dbcontext;

        public ResultWindowVM() { }

        public ResultWindowVM(Student student)
        {
            SelectedStudent3 = student;
            dbcontext = new DataBaseContest();
            ListSM = new ObservableCollection<StudentModule>(dbcontext.studentModules);
            ListMod = new ObservableCollection<Module>(dbcontext.Modules.ToList());
            LoadGradeList();
            LoadRegMod();
        }

        public void LoadGradeList()
        {
            ListGrade = new ObservableCollection<StudentModule>();
            foreach (var sm in ListSM)
            {
                if (sm.Student_Reg == SelectedStudent3.Reg_No)
                {
                    ListGrade.Add(sm);
                }
            }
        }

        public void LoadRegMod()
        {
            ListRMod = new ObservableCollection<Module>();
            foreach (var sm in ListSM)
            {
                if (sm.Student_Reg == SelectedStudent3.Reg_No)
                {
                    if (sm.Module_Code != null)
                    {
                        foreach (var m in ListMod)
                        {
                            if (m.Code == sm.Module_Code)
                            {
                                ListRMod.Add(m);
                            }
                        }
                    }
                }
            }
        }
    }
}
