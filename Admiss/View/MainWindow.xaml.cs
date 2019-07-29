using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Admiss.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region Первая инициализация с БД 
            //using (AdmissConnection db = new AdmissConnection())
            //{
            //    db.MapObjects.Add(new MapObject
            //    {
            //        Id = "00008",
            //        Name = "Ilgizz",
            //        FName = "Timrukovzzz",
            //        Series = 323223,
            //        Number = 3213231,
            //        ParentId = 221
            //    });
            //    db.SaveChanges();

            //}
            #endregion
        }

        private void Load_Uri(object sender, NavigatingCancelEventArgs e)
        {

        }
    }
}

