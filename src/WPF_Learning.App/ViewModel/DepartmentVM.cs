using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Learning.Core.Domains.Systems;
using WPF_Learning.Core.Repositories;
using WPF_Learning.Core.SeedWorks;
using WPF_Learning.Model;

namespace WPF_Learning.App.ViewModel
{
    public class DepartmentVM : BaseVM
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _context;
        private ObservableCollection<Department> _list;
        public ObservableCollection<Department> List { get { return _list; } set { _list = value; OnPropertyChanged(); } }

        private string _Name;
        public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }

        private Department _SeletedItem;
        public Department SelectedItem
        {
            get => _SeletedItem; set
            {
                _SeletedItem = value; OnPropertyChanged(); 
                if (SelectedItem != null)
                {
                    Name = SelectedItem.Name;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public DepartmentVM()
        {
            _Name = "";
            _SeletedItem = new Department();
            _unitOfWork = App.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _context = App.ServiceProvider.GetRequiredService<ApplicationContext>();
            _list = new ObservableCollection<Department>(_context.Departments);
            AddCommand = new RelayCommand<object>(p =>
            {
                if (string.IsNullOrEmpty(Name))
                    return false;
                var isExisted = _unitOfWork.DepartmentRepository.Find(i => i.Name == Name);
                if (isExisted == null || isExisted.Count() != 0)
                    return false;

                return true;
            }, p =>
            {
                var department = new Department() { Id = Guid.NewGuid(), Name = Name };

                _unitOfWork.DepartmentRepository.Add(department);
                _unitOfWork.CompleteAsync();

                List.Add(department);
            });

            EditCommand = new RelayCommand<object>(p =>
            {
                if (SelectedItem == null) return false;
                var displayList = _unitOfWork.DepartmentRepository.Find(i => i.Id == SelectedItem.Id);
                if (displayList != null && displayList.Count() != 0)
                    return true;

                return false;
            }, p =>
            {
                var department = _unitOfWork.DepartmentRepository.Find(x => x.Id == SelectedItem.Id).SingleOrDefault();
                department!.Name = Name;

                _context.SaveChanges();

                SelectedItem.Name = Name;
            });

        }
    }
}
