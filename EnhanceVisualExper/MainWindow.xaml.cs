﻿
using System;
using System.Collections.Generic;
using System.IO;
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
        private int[,] selectedTable = new int[50, 100];

        public MainWindow()
        {
            InitializeComponent();

            BindList();
        }

        private List<string> GetImageName()
        {
            List<string> fileNames = new List<string>();

            foreach (var fileName in System.IO.Directory.GetFiles(@"A:\ドキュメント\Labo\LocalRipository\img", "*", System.IO.SearchOption.AllDirectories))
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

        private void OutputResult()
        {
            //https://stackoverflow.com/questions/3836313/getting-the-index-of-multiple-selected-items-in-a-listbox-using-silverlight
            List<int> selectedItemIndexes = (from object o in ImgList.SelectedItems select ImgList.Items.IndexOf(o)).ToList();

            if (selectedItemIndexes.Count == 3)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (selectedItemIndexes.Contains(i))
                    {
                        selectedTable[watchingIndex, i] = 1;
                    }
                }
                Console.WriteLine(selectedItemIndexes[0] + "-" + selectedItemIndexes[1] + "-" + selectedItemIndexes[2]);
            }



        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            watchingIndex++;

            if (watchingIndex > 50)
                watchingIndex = 50;

            OutputResult();

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
            StreamWriter sw = new StreamWriter(@"test.csv", true, Encoding.UTF8);

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
        }
    }
}

