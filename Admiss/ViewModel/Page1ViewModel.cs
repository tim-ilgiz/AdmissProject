using Admiss.Model;
using System.Windows.Input;

namespace Admiss.ViewModel
{
    public class Page1ViewModel : INotify
    {
        public Page1ViewModel()
        {
            //Временная синхронизация 
            //Синхронизироапть с базой данных    
            Name = "Admin";
            FName = "Admin";
            PName = "Admin";

            SeriaPassport = 9999;
            NumberPassport = 999999;
            PassportYear = "99.99.99";
            DriveNumber = "999999";
            GriveYear = "99.99.99";

            Open_PageCameraView = new Command(obj =>
            {
                MainWindowViewModel.Name = this.Name;
                MainWindowViewModel.FName = this.FName;
                MainWindowViewModel.Seria = this.SeriaPassport;
                MainWindowViewModel.Number = this.NumberPassport; 
                AddCommandPage2();
            });
        }
        public ICommand Open_PageCameraView { get; set; }
        private void AddCommandPage2() => MainWindowViewModel.mainViewModel.Content_Registration = new PageCameraViewModel();

        #region Property
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));

            }
        }
        private string fname;
        public string FName
        {
            get => fname;
            set
            {
                fname = value;
                OnPropertyChanged(nameof(FName));

            }
        }
        private string pname;
        public string PName
        {
            get => pname;
            set
            {
                pname = value;
                OnPropertyChanged(nameof(PName));

            }
        }

        private int _SeriaPassport;
        public int SeriaPassport
        {
            get=> _SeriaPassport;
            set
            {
                _SeriaPassport = value; 
                OnPropertyChanged(nameof (SeriaPassport));
            }
        }

        private int _NumberPassport; 
        public int NumberPassport
        {
            get => _NumberPassport;
            set
            {
                _NumberPassport = value; 
                OnPropertyChanged(nameof(NumberPassport));
            }
        }


        private string _PassportYear; 
        public string PassportYear
        {
            get => _PassportYear;
            set
            {
                _PassportYear = value;
                OnPropertyChanged(nameof(PassportYear)); 
            }
        }

        private string _DriveNumber;
        public string DriveNumber
        {
            get => _DriveNumber;
            set
            {
                _DriveNumber = value;
                OnPropertyChanged(nameof(DriveNumber));
            }
        }

        private string _GriveYear; 
        public string GriveYear
        {
            get => _GriveYear;
            set
            {
                _GriveYear = value;
                OnPropertyChanged(nameof(GriveYear));
            }
        }

        #endregion
    }
}
