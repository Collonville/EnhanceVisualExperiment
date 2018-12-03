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

        private List<string> GetImageName()
        {
            List<string> fileNames = new List<string>();
            
            foreach(var fileName in System.IO.Directory.GetFiles(@"A:\ドキュメント\Labo\LocalRipository\img", "*", System.IO.SearchOption.AllDirectories))
            {
                //Only Filename
                fileNames.Add(System.IO.Path.GetFileName(fileName));

                //Filename & path
                //fileNames.Add(fileName);
            } 

            return fileNames;
        }

        private void BindList()
        {
            List<Image> list = new List<Image>();
            List<string> imagePath = GetImageName();

            foreach(var fileName in imagePath)
            {

                for (int i = 0; i < 1; i++)
                {
                    list.Add(new Image
                    {
                        Index = (i + 1).ToString(),
                        Path = @"A:\ドキュメント\Labo\LocalRipository\outimg\continuity_hue\" + fileName.Replace(".jpg", "") + "_" + i.ToString() + ".jpg"
                    });
                }
            }

            



            ImgList.ItemsSource = list;
        }
    }
}
