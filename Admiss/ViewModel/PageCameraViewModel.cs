using Admiss.Model;
using Admiss.Model.FaceVerificaton;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Admiss.ViewModel
{
    public class PageCameraViewModel : INotify
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private bool Flag { get; set; } = true;
        public bool FaceVarificationFalg { get; set; } = true;
        public PageCameraViewModel()
        {
            CameraCapture = new CameraCapture();
            ResultViewModel = new ResultViewModel();
            Face2FaceVerification = new Face2FaceVerification();
            Reload_Photo = new Command(obj => ReloadPhoto());
            ResultLoadViewModel = new ResultLoadViewModel();
            Complete = new Command(obj =>
            {
                Face2FaceVerificationStart();
                NextPage();
            });
            
            dispatcherTimer.Tick += Start_Detect_Face_Event;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0);
            dispatcherTimer.Start();
        }

        public void NextPage () => MainWindowViewModel.mainViewModel.Content_Registration = ResultLoadViewModel; 

        public static ICommand Complete { get; set; }
        public static ICommand Face2FaceVerificationCommand { get; set; }
        private CameraCapture _CameraCapture;
        public CameraCapture CameraCapture
        {
            get => _CameraCapture;
            set
            {
                _CameraCapture = value;
                OnPropertyChanged(nameof(CameraCapture));
            }
        }

        private Face2FaceVerification _Face2FaceVerification;
        public Face2FaceVerification Face2FaceVerification
        {
            get => _Face2FaceVerification;
            set
            {
                _Face2FaceVerification = value;
                OnPropertyChanged(nameof(Face2FaceVerification));
            }
        }

        private ResultLoadViewModel _ResultLoadViewModel;
        public ResultLoadViewModel ResultLoadViewModel
        {
            get => _ResultLoadViewModel;
            set
            {
                _ResultLoadViewModel = value;
                OnPropertyChanged(nameof(ResultLoadViewModel));
            }
        }

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

        private void ReloadPhoto()
        {
            CameraCapture.Init();
            CameraCapture.DetectFeatures();
            CameraCapture.DispatcherTimer_Tick();
            Face2FaceVerification.LeftImagePicker_Metod();
            Face2FaceVerification.RightImagePicker_Metod();
            FaceVarificationFalg = true;
        }

        public void Face2FaceVerificationStart()
        {
            if (FaceVarificationFalg)
            {
                Face2FaceVerification.Face2FaceVerification_Metod();
                FaceVarificationFalg = false;
            }
        }
      
        private void Start_Detect_Face_Event(object sender, EventArgs e)
        {
            if (Flag)
            {
                ReloadPhoto();
            }
            Flag = false;
        }

        public ICommand Reload_Photo { get; set; }
        public ICommand Add_Photo { get; set; }
        public static ICommand Back { get; set; }
    }
}
