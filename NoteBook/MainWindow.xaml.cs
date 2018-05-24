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

namespace NoteBook
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath = "";
        bool needSave = false;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void newFileMouseDown(object sender, MouseButtonEventArgs e)
        {
            // 看是否已經最大化  如果是就縮小
            if (needSave)
            {
                var SaveResult = MessageBox.Show("需要存檔嗎?", "尚有未儲存的變更", MessageBoxButton.YesNoCancel);
                if (SaveResult == MessageBoxResult.Yes)
                {
                    if (filePath == "")
                    {
                        SaveAss();
                        Clear();
                    }
                    else
                    {
                        // 存檔覆寫
                        System.IO.File.WriteAllText(filePath, TextArea.Text);
                    }
                }
                else if (SaveResult == MessageBoxResult.No)
                {
                    Clear();
                }
            }
            else
            {
                Clear();
            }
        }

        private void Min_Click(object sender, RoutedEventArgs e)
        {
            // 縮到工具列
            this.WindowState = WindowState.Minimized;
        }

        private void Max_Click(object sender, RoutedEventArgs e)
        {
            // 看是否已經最大化  如果是就縮小
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            // 如果使用者還有未儲存就問一下
            if (needSave)
            {
                var SaveResult = MessageBox.Show("需要存檔嗎?", "尚有未儲存的變更", MessageBoxButton.YesNoCancel);
                if (SaveResult == MessageBoxResult.Yes)
                {
                    SaveAss();
                    this.Close();
                }
                else if (SaveResult == MessageBoxResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void openFile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 如果使用者還有未儲存就問一下
            if (needSave)
            {
                var SaveResult = MessageBox.Show("需要存檔嗎?", "尚有未儲存的變更", MessageBoxButton.YesNoCancel);
                if (SaveResult == MessageBoxResult.Yes)
                {
                    SaveAss();
                    Open();
                }
                else if (SaveResult == MessageBoxResult.No)
                {
                    Open();
                }
            }
            else
            {
                Open();
            }
        }

        private void saveFile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 如果有路徑就直接存  沒有就問要存哪
            if (filePath != "")
            {
                System.IO.File.WriteAllText(filePath, TextArea.Text);

                // 如果存檔就刪去提示
                if(fileTitle.Text[fileTitle.Text.Length - 1] == '*')
                {
                    fileTitle.Text = fileTitle.Text.Substring(0, fileTitle.Text.Length - 1);
                }
                needSave = false;
            }
            else
            {
                SaveAss();
            }
        }

        private void TextChange(object sender, TextChangedEventArgs e)
        {
            // 提示未存檔
            needSave = true;
            if(fileTitle.Text[fileTitle.Text.Length - 1] != '*')
            {
                fileTitle.Text += "*";
            }
        }

        void SaveAss()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // 設置副檔名
            dlg.Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";

            // 防止Null
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                filePath = dlg.FileName;
                System.IO.File.WriteAllText(dlg.FileName, TextArea.Text);
                needSave = false;
            }
        }

        void Clear()
        {
            // 開新文件重置資料
            filePath = "";
            TextArea.Text = "";
            fileTitle.Text = "NewFile";
            needSave = false;
        }

        void Open()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            
            // 需防止Null
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // 設置標題跟輸出文字
                Clear();
                filePath = dlg.FileName;
                TextArea.Text = System.IO.File.ReadAllText(dlg.FileName);
                fileTitle.Text = dlg.SafeFileName;
                needSave = false;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            // 需防止Null
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                filePath = dlg.FileName;
                System.IO.File.WriteAllText(dlg.FileName, TextArea.Text);
            }
        }

        private void saveAsFile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SaveAss();
        }

        private void FontSizeChange(object sender, SelectionChangedEventArgs e)
        {
            // 得到Sender物件
            string text = (sender as ComboBox).SelectedItem as string;
            double fontSize;
            Double.TryParse(text, out fontSize);
            if (fontSize > 0)
            {
                TextArea.FontSize = fontSize;
            }
        }

        private void Gray(object sender, MouseButtonEventArgs e)
        {
            // 設置Gray主題
            fileTitle.Foreground = Brushes.White;
            Min.Foreground = Brushes.White;
            Max.Foreground = Brushes.White;
            Close.Foreground = Brushes.White;
            Min.Background = Brushes.Gray;
            Max.Background = Brushes.Gray;
            Close.Background = Brushes.Gray;
            TitleGround.Background = Brushes.Gray;
            TextArea.Background = Brushes.Gray;
            TextArea.Foreground = Brushes.White;
            TextArea.CaretBrush = Brushes.White;
        }

        private void White(object sender, MouseButtonEventArgs e)
        {
            // 設置White主題
            fileTitle.Foreground = Brushes.Black;
            Min.Foreground = Brushes.Black;
            Max.Foreground = Brushes.Black;
            Close.Foreground = Brushes.Black;
            Min.Background = Brushes.White;
            Max.Background = Brushes.White;
            Close.Background = Brushes.White;
            TitleGround.Background = Brushes.White;
            TextArea.Background = Brushes.White;
            TextArea.Foreground = Brushes.Black;
            TextArea.CaretBrush = Brushes.Black;
        }

        private void OnEnter(object sender, MouseEventArgs e)
        {
            // Get sender
            TextBlock t = (TextBlock)sender;
            t.FontSize = 24;
        }

        private void OnLeave(object sender, MouseEventArgs e)
        {
            // Get sender
            TextBlock t = (TextBlock)sender;
            t.FontSize = 16;
        }
    }
}
