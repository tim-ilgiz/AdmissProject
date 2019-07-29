using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Admiss.Model.FaceVerificaton;
using Admiss.ViewModel;
using APIClient;
using DB = APIClient.EntityDb; 


namespace Admiss.Model
{
    public class ResultViewModel : INotify
    {
        public static Face2FaceVerification face2FaceVerification;
        public DispatcherTimer watingTimer = new DispatcherTimer();
        public ResultViewModel()
        {
            Face2FaceVerification.resultViewModel = this;
            GifLoadImage = @"..\Resources\loadinfo.gif";
            IsVisibleButton_Ok = Visibility.Collapsed;

        }

        public static bool IsTrueResult(string name, 
                                        string fname, 
                                        string pname, 
                                        int number, 
                                        int seria )
        {
            using (DB.AdmissEntityDb db = new DB.AdmissEntityDb () )
            {
                return db.AdmissObjects.Any( x=> x.FirstName==name&& x.LastName==fname );
            }
        }

        public void FinishText()
        {
            if (ResultViewModel.IsTrueResult(
                MainWindowViewModel.Name,
                MainWindowViewModel.FName,
                MainWindowViewModel.LastName,
                MainWindowViewModel.Number,
                MainWindowViewModel.Seria) && Face2FaceVerification.FaceResult)
            {
                TextResult = "Успешно!";
                GifLoadImage = @"..\Resources\checked.png";
                IsVisibleButton_Ok = Visibility.Visible; 
            }
            else
            {
                TextResult = "Данные не прошли проверку, повторите процедуру";
                GifLoadImage = @"..\Resources\cancel.png";
                IsVisibleButton_Ok = Visibility.Collapsed; 
            }
            
        }

        public void WaitingText()
        {
            //ПРОВЕРКА ДАННЫХ... 
            TextResult = "Подождите, идет проверка полученных данных...";     
        }

        Visibility _isVisibleButton_Ok; 
        public Visibility IsVisibleButton_Ok
        {
            get => _isVisibleButton_Ok;
            set
            {
                _isVisibleButton_Ok = value;
                OnPropertyChanged(nameof(IsVisibleButton_Ok));
            }
        }

        private string _TextResult;
        public string TextResult
        {
            get => _TextResult;
            set
            {
                _TextResult = value;
                OnPropertyChanged(nameof(TextResult));
            }
        }

        private string _GifLoadImage;
        public string GifLoadImage
        {
            get => _GifLoadImage;
            set
            {
                _GifLoadImage = value;
                OnPropertyChanged(nameof(GifLoadImage));
            }
        }
    }
}
