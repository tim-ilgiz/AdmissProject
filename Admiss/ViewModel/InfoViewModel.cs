using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Admiss.Model;

namespace Admiss.ViewModel
{
    public class InfoViewModel:INotify
    {
        public InfoViewModel()
        {
            AddPage1 = new Command(obj =>
           {
               if (IsTrue())
               {
                   MainWindowViewModel.mainViewModel.Content_Registration = new PageCameraViewModel();
               }
               else
               {
                   If_CodeIsNot = "Вы ввели не правильный код";
               }
           });
        }

        public bool IsTrue () => InfoModel.IsTrueCode(_Number_Code);

        private int _Number_Code; 
        public int Number_Code
        {
            get => _Number_Code;
            set
            {
                _Number_Code = value;
                OnPropertyChanged(nameof(Number_Code));
            }
        }
        

        private string _If_CodeIsNot;
        public string If_CodeIsNot
        {
            get=> _If_CodeIsNot;

            set
            {
                _If_CodeIsNot = value;
                OnPropertyChanged(nameof(If_CodeIsNot));
            }
        }
        
        public ICommand AddPage1 { get; set; }

    }
}
