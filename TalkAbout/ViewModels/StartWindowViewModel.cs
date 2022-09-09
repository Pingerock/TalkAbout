using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using TalkAbout.Models;
using TalkAbout.Utils;

namespace TalkAbout.ViewModels
{
    class StartWindowViewModel
    {
        private DbOperations dbo;
        private DialogService ds;

        public ObservableCollection<User> AllUsers { get; set; }

        public StartWindowViewModel() {
            using (TalkAboutModel tam = new TalkAboutModel())
            {
                tam.Users.Add(new User() { Name = "Логин", Password = "Пароль" });
                tam.SaveChanges();
            }

            dbo = new DbOperations();
            ds = new DialogService();

            AllUsers = new ObservableCollection<User>();
        }

        private RelayCommand logIn;
        public RelayCommand LogIn
        {
            get
            {
                return logIn ??
                    (logIn = new RelayCommand(obj =>
                    {
                        if (ds.LogInDialog())
                        {
                            if (dbo.AuthentificationUser(ds.Password, ds.Username))
                            {
                                ds.LogInComplete(dbo.GetUserByUsername(ds.Username));
                            } else {
                                ds.ShowMessage("Неверный логин или пароль.");
                            }
                        }
                    }));
            }
        }

        private RelayCommand signUp;
        public RelayCommand SignUp
        {
            get
            {
                return signUp ??
                    (signUp = new RelayCommand(obj =>
                    {
                        if (ds.SignUpDialog())
                        {
                            if (!dbo.CheckFreeUsername(ds.Username))
                            {
                                User u = new User()
                                {
                                    Name = ds.Username,
                                    Password = ds.Password
                                };
                                dbo.AddUser(u);
                                AllUsers.Add(u);
                                ds.ShowMessage("Аккаунт успешно зарегистрирован!");
                            }
                            else {
                                ds.ShowMessage("Аккаунт с данным именем уже существует.");
                            }
                            
                        }
                    }));
            }
        }
    }
}
