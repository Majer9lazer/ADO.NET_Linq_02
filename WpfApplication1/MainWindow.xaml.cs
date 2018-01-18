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
using WpfApplication1.Model;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EquipmentDB db = new EquipmentDB();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void GetAreaData()
        {
            GridViewList.Columns.Clear();
            var data_A = db.Areas.Where(w => w.WorkingPeople > 2);
            List<Area> data_AL = data_A.ToList();
            GridViewList.Columns.Add(new GridViewColumn()
            {
                Header = "AreId",
                DisplayMemberBinding = new Binding()
                {
                    Path = new PropertyPath("AreaId")
                }
            });
            GridViewList.Columns.Add(new GridViewColumn()
            {
                Header = "FullName",
                DisplayMemberBinding = new Binding()
                {
                    Path = new PropertyPath("FullName")
                }
            });
            DataList.ItemsSource = data_AL;
            var data_B = db.Areas.Where(w => w.AssemblyArea == true).ToList();
            var data_C = db.Areas.Skip(10);
            var data_D = db.Areas.Skip(5).Take(3);
            var data_E = db.Areas.ToList().TakeWhile(tw => tw.OrderExecution != null);
            var data_F = db.Areas.ToList().TakeWhile(tw => tw.OrderExecution == null);
            var data_G = db.Areas.GroupBy(gb => gb.IP);
            int[] arr = new int[] { 22, 23, 24, 25, 26, 27, 28 };
            var data_H = db.Areas.Where(w => w.AreaId >= 22 && w.AreaId <= 28);
            var data_H2 = db.Timers.Where(w => w.AreaId >= 22 && w.AreaId <= 28);
            // Второй пример
            var data_H3 = db.Timers.Where(w => arr.Contains(w.AreaId.Value));
            var data_H3_2 = db.Areas.Where(w => arr.Contains(w.AreaId));
            int[] arr_2 = new[] { 38, 39, 102 };
            var data_I = db.Timers.Where(w => arr_2.Contains(w.AreaId.Value) && w.DateFinish != null).
                Where(w => w.DateStart >= DateTime.Parse("01.06.2017 ") &&
                w.DateFinish <= DateTime.Parse("30.08.2017"));
            var data_J = db.Timers.
                Where(w => w.DateFinish != null)
                .Count();
            var data_K = db.Timers.
                Join(db.Areas, tm => tm.AreaId, ar => ar.AreaId, (tm, ar) => new
                {
                    tm,
                    ar
                });
            var data_M = db.Timers.
                GroupBy(gr => gr.DateStart).OrderByDescending(ob => ob.Key.Value);
        }
        private void Get_A()
        {
            var data_A = db.Areas.Where(w => w.WorkingPeople > 2);
            List<Area> data_AL = data_A.ToList();
            GridViewList.Columns.Add(new GridViewColumn()
            {
                Header = "AreId",
                DisplayMemberBinding = new Binding()
                {
                    Path = new PropertyPath("AreaId")
                }
            });
            GridViewList.Columns.Add(new GridViewColumn()
            {
                Header = "FullName",
                DisplayMemberBinding = new Binding()
                {
                    Path = new PropertyPath("FullName")
                }
            });
            DataList.ItemsSource = data_AL;
        }
        private void Get_B()
        {
           if (DataList.ItemsSource != null)
            {
                DataList.ItemsSource = null;
                var data_B = db.Areas.Where(w => w.AssemblyArea == true).ToList();
                DataList.ItemsSource = data_B;
            }
            else
            {
                ErrorOrSuccesLabel.Content += "Ресурс пришел пустым))\n";
            }
           
        }
        private void Get_C()
        {
            var data_C = db.Areas.Skip(10).ToList();
        }

        public void GetAllMethods(string name, int sizeof_arr)
        {
            if (sizeof_arr != 0)
            {
                Mydell[] dells = new Mydell[sizeof_arr];
                dells[0] = Get_A;
                dells[1] = Get_B;
                dells[2] = Get_C;
            
                foreach (Mydell item in dells)
                {
                    if (item.Method.Name == name)
                    {
                        item.Invoke();
                        break;
                    }
                   
                }
            }
            else
            {

            }
        }
        private delegate void Mydell();

        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            GetAreaData();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
      
            ComboBox c = (ComboBox)sender;
            ComboBoxItem selItem = null;
            foreach (ComboBoxItem item in c.Items)
            {
                if (item.IsSelected == true)
                {
                    selItem = item;
                    break;
                }
                else
                {

                }
            }
            if (selItem != null)
            {
                GetAllMethods(selItem.Content.ToString(), c.Items.Count);
            }
        }
    }
}

