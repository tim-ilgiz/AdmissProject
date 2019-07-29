using Admiss.Model;
using System.Windows.Input;

namespace Admiss.ViewModel
{
    public class ResultLoadViewModel : INotify
    {
        public ResultLoadViewModel()
        {
            ResultViewModel = new ResultViewModel();
            ResultViewModel.TextResult = "Идет проверка. Ожидайте";
            resultLoadModel = new ResultLoadModel();
            EndOperation = new Command(obj =>
           {
               resultLoadModel.Result();
               //Вернуться на начальный экран 
               MainWindowViewModel.mainViewModel.Visible_ContentControl = System.Windows.Visibility.Collapsed;
               MainWindowModel.windowModel.AllFalse(); 
           });

            Click_Cancel = new Command(obj =>
           {
               MainWindowViewModel.mainViewModel.Content_Registration = new InfoViewModel();
           });
        }

        #region Propertys
        
        private ResultViewModel _ResultViewModel;
        public ResultViewModel ResultViewModel
        {
            get => _ResultViewModel;
            set
            {
                _ResultViewModel = value;
                OnPropertyChanged(nameof(ResultViewModel));
            }
        }

        ResultLoadModel _resultLoadModel;
        public ResultLoadModel resultLoadModel
        {
            get => _resultLoadModel;
            set
            {
                _resultLoadModel = value;
                OnPropertyChanged(nameof(resultLoadModel));
            }
        }

        #endregion

        public ICommand Click_Cancel { get; }
        public ICommand EndOperation { get; }
    }
}
