using MVVM_Basics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_Basics.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public static User LoginedUser { get; set; }

        public MainViewModel()
        {
            LoginedUser = DataProvider.Ins.DB.Users.First();
            //MessageBox.Show("Đã vào MainViewModel -> Data Context của MainWindow");
        }
    }
}
