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
            this.WindowState = WindowState.Minimized;
        }

        private void Max_Click(object sender, RoutedEventArgs e)
        {
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
            if (filePath != "")
            {
                System.IO.File.WriteAllText(filePath, TextArea.Text);
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
            needSave = true;
            if(fileTitle.Text[fileTitle.Text.Length - 1] != '*')
            {
                fileTitle.Text += "*";
            }
        }

        void SaveAss()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.csv";
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
            filePath = "";
            TextArea.Text = "";
            fileTitle.Text = "NewFile";
            needSave = false;
        }

        void Open()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
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
            string text = (sender as ComboBox).SelectedItem as string;
            double fontSize;
            Double.TryParse(text, out fontSize);
            if (fontSize > 0)
            {
                TextArea.FontSize = fontSize;
            }
        }
        
    }
}
