using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Final_Group_Project.DataBase;
using Final_Group_Project.View;
using Final_Group_Project.ViewModel;

namespace Final_Group_Project.ViewModel
{
    public partial class LoginWindowVM : ObservableObject
    {
        [ObservableProperty]
        public string username;

        [ObservableProperty]
        public string password;

        private const string aUserName = "admin";

        private const string aPassword = "123456";

        public Action CloseAction { get; set; }

        DataBaseContest db;

        public LoginWindowVM() {

            db = new DataBaseContest();
        }

        [RelayCommand]

        public void UserLogin()
        {
                bool user1 = db.Users.Any(user=>user.Name== username && user.Password==password);

                if (user1) 
                {
                    StudentWindow studentWindow = new StudentWindow();
                    studentWindow.Show();
                    CloseAction();                   
                
                }
                else 
                {
                    MessageBox.Show("Incorrect User Name or Password");
                }
            

        }

        [RelayCommand]
        public void AdminLoging()
        {
            if(Username==aUserName &&  Password==aPassword) 
            {
                Admin admin=new Admin();
                admin.Show();
                CloseAction();
            }
            else 
            {
                MessageBox.Show("Incorrect User Name or Password");
            }

        }

        public void Close()
        {
            CloseAction();
        }

    }
}
