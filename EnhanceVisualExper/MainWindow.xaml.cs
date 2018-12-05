using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace EnhanceVisualExper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> imagePath;
        private int watchingIndex = 0;
        private int[,] selectedTable = new int[50, 100];
        private const int ImageCollectNUM = 4;

        private const string InputImagePath = @"C:\Users\ht235_000\Documents\Laboratory\EnhanceImage\img";
        private const string AllImagePath = @"C:\Users\ht235_000\Documents\Laboratory\EnhanceImage\outimg\continuity_hue\All\";

        public MainWindow()
        {
            InitializeComponent();

            imagePath = GetImageName();

            BindList();
        }

        private List<string> GetImageName()
        {
            List<string> fileNames = new List<string>();

            foreach (var fileName in System.IO.Directory.GetFiles(InputImagePath, "*", System.IO.SearchOption.AllDirectories))
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
            
            for (int i = 0; i < 100; i++)
            {
                list.Add(new Image
                {
                    Index = (i + 1).ToString(),
                    Path = AllImagePath + imagePath[watchingIndex].Replace(".jpg", "") + "_" + i.ToString() + ".jpg"
                });
            }

            ImgList.ItemsSource = list;

            //入力画像の更新
            BitmapImage btm = new BitmapImage();
            btm.BeginInit();
            btm.UriSource = new Uri(AllImagePath + imagePath[watchingIndex].Replace(".jpg", "") + "_0.jpg");
            btm.EndInit();

            InputImg.Source = btm;

            //インデックス表示の更新
            IndexLabel.Content = watchingIndex.ToString() + "/49";
        }

        private void Write2Matrix()
        {
            //https://stackoverflow.com/questions/3836313/getting-the-index-of-multiple-selected-items-in-a-listbox-using-silverlight
            List<int> selectedItemIndexes = (from object o in ImgList.SelectedItems select ImgList.Items.IndexOf(o)).ToList();

            if (selectedItemIndexes.Count == ImageCollectNUM)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (selectedItemIndexes.Contains(i))
                    {
                        selectedTable[watchingIndex, i] = 1;
                    }
                }
                Console.WriteLine(watchingIndex + ":" + selectedItemIndexes[0] + "-" + selectedItemIndexes[1] + "-" + selectedItemIndexes[2] + "-" + selectedItemIndexes[3]);
            }
            else
            {
                Console.WriteLine("Select " + ImageCollectNUM + "Pictures!!!");
            }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            //選択行列の更新
            Write2Matrix();

            watchingIndex++;

            if (watchingIndex >= 50)
                watchingIndex = 49;

            BindList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            watchingIndex--;

            if (watchingIndex < 0)
                watchingIndex = 0;

            BindList();
        }

        private void WriteCSVBtn_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = new StreamWriter($"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.csv", true, Encoding.UTF8);

            List<String> csvLine = new List<String>();

            for(int imageNum = 0; imageNum < 50; imageNum++)
            {
                csvLine = new List<string>();

                for (int i = 0; i < 100; i++)
                {
                    csvLine.Add(selectedTable[imageNum, i].ToString());
                }

                sw.WriteLine(String.Join(",", csvLine.ToArray()));
            }
            
            sw.Dispose();

            Console.WriteLine("Exported CSV");
        }
    }
}

