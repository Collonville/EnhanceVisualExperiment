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

namespace EnhanceVisualExper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BindList();
        }

        private void BindList()
        {
            List<Image> list = new List<Image>();

            for(int i = 0; i < 100; i++)
            {
                list.Add(new Image { Path = @"D:\Documents\Visual Studio 2017\Projects\EnhanceVisualExper\EnhanceVisualExper\Img\strawberry_"+ i.ToString() + ".jpg" });

            }



            ImgList.ItemsSource = list;
        }
    }
}
