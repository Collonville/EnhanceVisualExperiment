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
        private List<string> imagePath;
        private int watchingIndex = 0;

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
            imagePath = GetImageName();

            for (int i = 0; i < 100; i++)
            {
                list.Add(new Image
                {
                    Index = (i + 1).ToString(),
                    Path = @"A:\ドキュメント\Labo\LocalRipository\outimg\continuity_hue\" + imagePath[watchingIndex].Replace(".jpg", "") + "_" + i.ToString() + ".jpg"
                });
            }

            ImgList.ItemsSource = list;

            IndexLabel.Content = watchingIndex.ToString() + "/50";
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            watchingIndex++;

            if (watchingIndex > 50)
                watchingIndex = 50;

            BindList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            watchingIndex--;

            if (watchingIndex < 0)
                watchingIndex = 0;

            BindList();
        }
    }
}
