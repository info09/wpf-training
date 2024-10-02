using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using WPF_Learning.Core.Domains.Identity;

namespace WPF_Learning.App.ViewModel
{
    public class LoginVM : BaseVM
    {
        private readonly UserManager<AppUser> _userManager;
        public bool IsLogin { get; set; }

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginVM()
        {
            _userManager = App.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            IsLogin = false;
            _UserName = "";
            _Password = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, async (p) => { await LoginAsync(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        private async Task LoginAsync(Window p)
        {
            if (p == null)
                return;

            var user = await _userManager.FindByNameAsync(UserName);

            if (user == null || user.IsActive == false || user.LockoutEnabled)
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                return;
            }

            var result = await PasswordSignInAsync(user.UserName, Password);

            if (result)
            {
                IsLogin = true;

                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }


        private async Task<bool> PasswordSignInAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);
                if (result)
                {
                    // Handle the sign-in logic, such as setting the current user context
                    return true;
                }
            }
            return false;
        }
    }
}
