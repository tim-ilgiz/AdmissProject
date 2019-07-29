using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Admiss.ViewModel;
using APIClient;
using DB = APIClient.EntityDb;
using MW = Admiss.ViewModel.MainWindowViewModel;

namespace Admiss.Model
{
    public class MainWindowModel
    {
        public static MainWindowModel windowModel = new MainWindowModel();

        private bool numberWindow1 = default;
        private bool numberWindow2 = default;
        private bool numberWindow3 = default;

        public MainWindowModel()
        {
            windowModel = this;
        }

        public void TimeOff()
        {
            using (DB.AdmissEntityDb db = new DB.AdmissEntityDb())
            {
                foreach (var item in db.AdmissObjects)
                {
                    //item.DataTimeOnn--; 
                    //if (item.DataTimeOnn<=0)
                    //{
                    //    db.MapObjects.Remove(item);
                    //}
                }
                db.SaveChanges();
            }
        }

        //Синхронизация с БД... 
        public void TimerUpdateDb_Tick(object sender, EventArgs e)
        {
            DateTime lastServerDate =  Program.GetTime();
            if (MW.mainViewModel.UpdateDbDate!= lastServerDate)
            {
                var itemsserverDb =  Program.GetHttpDb();
                using (DB.AdmissEntityDb db = new DB.AdmissEntityDb())
                {
                    foreach (var item in db.AdmissObjects)
                    {
                        var elementServer = itemsserverDb.FirstOrDefault(x => x.Id == item.Id);

                        if(elementServer!=null)
                        {
                            if (item.Id == elementServer.Id &&
                                item.FirstName == elementServer.FirstName&&
                                item.LastName == elementServer.LastName&&
                                item.Patronymic == elementServer.Patronymic&&
                                item.SecretNumberCode == elementServer.SecretNumberCode&&
                                item.EndData == elementServer.EndData&&
                                item.StartData == elementServer.StartData) continue; 

                            if (item.Id == elementServer.Id)
                            {
                                item.FirstName = elementServer.FirstName;
                                item.LastName = elementServer.LastName;
                                item.Patronymic = elementServer.Patronymic;
                                item.SecretNumberCode = elementServer.SecretNumberCode;
                                item.EndData = elementServer.EndData;
                                item.StartData = elementServer.StartData; 
                            }
                        }
                        else
                        {
                            db.AdmissObjects.Remove(item);
                        }
                    }

                    foreach (var item in itemsserverDb)
                    {
                        var obj = db.AdmissObjects.FirstOrDefault(x => x.Id == item.Id); 
                        if (obj == null)
                        {
                            db.AdmissObjects.Add(new DB.AdmissObject
                            {
                                Id = item.Id, 
                                FirstName = item.FirstName, 
                                LastName = item.LastName, 
                                Patronymic = item.Patronymic, 
                                SecretNumberCode = item.SecretNumberCode, 
                                StartData = item.StartData, 
                                EndData = item.EndData
                            });
                        }
                    }
                    //TODO: База не хочет работать без исключений
                    db.SaveChanges();
                    MW.mainViewModel.UpdateDbDate = lastServerDate;
                }
            }
        }

        public void AllFalse()
        {
            numberWindow1 = default;
            numberWindow2 = default;
            numberWindow3 = default;
        }

        public void IsVisibleWindow1()
        {
            if (!numberWindow1)
            {
                numberWindow1 = true;
                numberWindow2 = false;
                numberWindow3 = false;
            }
            else numberWindow1 = false;

            if (numberWindow1)
            {
                MW.mainViewModel.Content_Registration = new Page1ViewModel();
                MetodVisible();
            }
            else
            {
                MetodCollapsed();
            }
        }
        public void IsVisibleWindow2()
        {
            if (!numberWindow2)
            {
                numberWindow2 = true;
                numberWindow1 = false;
                numberWindow3 = false;
            }
            else numberWindow2 = false;

            if (numberWindow2)
            {
                MW.mainViewModel.Content_Registration = new InfoViewModel();
                MetodVisible();
            }
            else
            {
                MetodCollapsed();
            }
        }

        public void IsVisibleWindow3()
        {
            if (!numberWindow3)
            {
                numberWindow3 = true;
                numberWindow2 = false;
                numberWindow1 = false;
            }
            else numberWindow3 = false;

            if (numberWindow3)
            {
                MW.mainViewModel.Content_Registration = new InfoViewModel();
                MetodVisible();
            }
            else
            {
                MetodCollapsed();
            }
        }

        private static void MetodCollapsed()
        {
            MW.mainViewModel.Visible_ContentControl = Visibility.Collapsed;
        }

        private static void MetodVisible()
        {
            MW.mainViewModel.Visible_ContentControl = Visibility.Visible;
        }


    }
}
