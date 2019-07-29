using Admiss.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal;
using APIClient;
using DB = APIClient.EntityDb; 

namespace Admiss.ViewModel
{
    public class MainWindowViewModel : INotify
    {
        public static MainWindowViewModel mainViewModel;

        private DateTime time;
        private DispatcherTimer timer;
        private DispatcherTimer timerUpdateDb;

        public static string Name { get; set; }
        public static string FName { get; set; }
        public static string LastName { get; set; }
        public static int Number { get; set; }
        public static int Seria { get; set; }
        public static bool ResultFace { get; set; }

        #region Commands 
        public ICommand Registr_Command { get; set; }
        public ICommand RegistrReload_Command { get; set; }
        public ICommand Registr3_Command { get; set; }

        #endregion

        public MainWindowViewModel()
        {
            mainViewModel = this;
            Visible_ContentControl = Visibility.Collapsed;
            time = new DateTime();

            timer = new DispatcherTimer();
            timerUpdateDb = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();

            timerUpdateDb.Interval = new TimeSpan(0, 0, 0, 3);
            timerUpdateDb.Tick += MainWindowModel.windowModel.TimerUpdateDb_Tick; 
            timerUpdateDb.Start();

            UriSourse = @"https://bm-technology.ru/";
            time = DateTime.Now;
            Times = time.ToShortTimeString();
            Data = time.ToShortDateString();

            //Commands
            Registr_Command = new Command(obj => MainWindowModel.windowModel.IsVisibleWindow1());

            RegistrReload_Command = new Command(obj => MainWindowModel.windowModel.IsVisibleWindow2());

            Registr3_Command = new Command(obj => MainWindowModel.windowModel.IsVisibleWindow3());

            //using (DB.AdmissEntityDb db = new DB.AdmissEntityDb())
            //{
            //    db.AdmissObjects.Add(
            //        new DB.AdmissObject
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //            FirstName = "Admin",
            //            LastName = "Admin",
            //            EndData = DateTime.Now,
            //            StartData = DateTime.Now,
            //            SecretNumberCode = 1111
            //        });
            //    db.SaveChanges(); 
            //}
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time = DateTime.Now;
            Times = time.ToShortTimeString();
            Data = time.ToShortDateString();
            if (time.ToShortTimeString() == "22:00")
            {
                MainWindowModel.windowModel.TimeOff();
            }
        }

        #region Property
        DateTime? _UpdateDbDate = null;
        public DateTime UpdateDbDate
        {
            get
            {
                _UpdateDbDate = _UpdateDbDate ?? Properties.Settings.Default.UpdateDbDate;
                return (DateTime)_UpdateDbDate;
            }
            set
            {
                Properties.Settings.Default.UpdateDbDate = value;
                Properties.Settings.Default.Save();
                _UpdateDbDate = value;
            }
        }

        private InfoViewModel _infomode;
        private InfoViewModel model
        {
            get => _infomode;
            set
            {
                _infomode = value;
                OnPropertyChanged(nameof(model));
            }
        }

        private Visibility _IsWebBrowser;
        public Visibility IsWebBrowser
        {
            get => _IsWebBrowser;
            set
            {
                _IsWebBrowser = value;
                OnPropertyChanged(nameof(IsWebBrowser));
            }
        }

        private string _data;

        public string Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        private string _times;
        public string Times
        {
            get => _times;
            set
            {
                _times = value;
                OnPropertyChanged(nameof(Times));
            }
        }

        public string UriSourse { get; set; }

        private static object _contentregistr;
        public object Content_Registration
        {
            get => _contentregistr;
            set
            {
                _contentregistr = value;
                OnPropertyChanged(nameof(Content_Registration));
            }
        }

        private Visibility _visibleContentControl;
        public Visibility Visible_ContentControl
        {
            get => _visibleContentControl;
            set
            {
                _visibleContentControl = value;
                OnPropertyChanged(nameof(Visible_ContentControl));
            }
        }

        HttpConnection _DbModel;
        public HttpConnection DbModel
        {
            get => _DbModel;
            set
            {
                _DbModel = value;
            }
        }
    }
    #endregion
}
