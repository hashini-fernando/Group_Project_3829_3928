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

namespace Final_Group_Project.ViewModel
{
    public partial class AdminVM : ObservableObject
    {
        [ObservableProperty]
        public string username;

        [ObservableProperty]
        public string password;

       


        DataBaseContest userData;

        public AdminVM()
        {
            userData = new DataBaseContest();
            LoadUser();
        }


        [ObservableProperty]

        public ObservableCollection<User> listofuser;
        [ObservableProperty]
        public User selectedUser;

        private void LoadUser()
        {
            Listofuser = new ObservableCollection<User>(userData.Users.ToList());
        }

        [RelayCommand]

        public void Update(Object obj)
        {
            SelectedUser = obj as User;
        }

        [RelayCommand]
        public void Update_User()
        {
            userData.SaveChanges();
            SelectedUser = new User();
        }

        [RelayCommand]
        public void AddUser()
        {
            var user = new User();
            user.Name = Username;
            user.Password = Password;
            int id = 1 + userData.Users.Count();
            user.Id = id;
            userData.Users.Add(user);
            userData.SaveChanges();

            Username = null;
            Password = null;

            Listofuser.Add(user);
        }
    }
}
