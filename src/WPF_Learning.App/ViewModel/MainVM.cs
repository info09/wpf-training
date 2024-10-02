using System.Windows;
using System.Windows.Input;
using WPF_Learning.App.View;

namespace WPF_Learning.App.ViewModel
{
    public class MainVM
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand ProjectCommand { get; set; }
        public ICommand DepartmentCommand { get; set; }
        public ICommand EmployeeCommand { get; set; }
        public ICommand TaskCommand { get; set; }

        public MainVM()
        {
            LoadedWindowCommand = new RelayCommand<Window>(p => true, p =>
            {
                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;

                var loginVM = loginWindow.DataContext as LoginVM;
                if (loginVM.IsLogin)
                    p.Show();
                else
                    p.Close();
            });
            ProjectCommand = new RelayCommand<Window>(p => true, p => { ProjectWindow wd = new ProjectWindow(); wd.ShowDialog(); });
            DepartmentCommand = new RelayCommand<Window>(p => true, p => { DepartmentWindow wd = new DepartmentWindow(); wd.ShowDialog(); });
            EmployeeCommand = new RelayCommand<Window>(p => true, p => { EmployeeWindow wd = new EmployeeWindow(); wd.ShowDialog(); });
            TaskCommand = new RelayCommand<Window>(p => true, p => { TaskWindow wd = new TaskWindow(); wd.ShowDialog(); });
        }
    }
}
