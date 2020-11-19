using Facebook_Bot_.Model;
using Facebook_Bot_.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Facebook_Bot_
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool random = false;
        Posts posts;
        PostingService service;
        public MainWindow()
        {
            InitializeComponent();
            timerBox.SelectedIndex = 0;
            posts = new Posts();
           
        }

        private void Result()
        {
            Dispatcher.BeginInvoke((Action)(() => {

                stopBtn.IsEnabled = false;
                runBtn.IsEnabled = true;
            }));
        }

        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();           
            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                posts.FolderPath = path.Text = dialog.SelectedPath;

            random = false;
        }

        private void runBtn_Click(object sender, RoutedEventArgs e)
        {
            var date = new DateTime(2020, 11, 30);//year, m, d

            if(date < DateTime.Now)
            {
                System.Windows.MessageBox.Show("Update required!");
                System.Windows.Application.Current.Shutdown();
                return;
            }

            

            if (String.IsNullOrEmpty(loginBox.Text) || String.IsNullOrEmpty(passBox.Text) || String.IsNullOrEmpty(path.Text))
            {
                System.Windows.MessageBox.Show("Fill in all the fields");
                return;
            }
            else
            {
                service = new PostingService();
                service.endPosting += Result;

                posts.Login = loginBox.Text;
                posts.Password = passBox.Text;

                if (!String.IsNullOrEmpty(loginBox.Text))
                    posts.Message = messageBox.Text;

                if (timerBox.SelectedIndex != 0)
                    posts.Interval = Convert.ToInt32(timerBox.Text);

                posts.FolderPath = path.Text;

                foreach (var item in Directory.GetFiles(posts.FolderPath).ToList())
                {
                    var res = item.Replace("\\", "/");
                    posts.PhotosFullName.Add(res);
                }

                runBtn.IsEnabled = false;
                stopBtn.IsEnabled = true;

                service.Login(posts.Login, posts.Password);

                if(random)
                    posts.Random = true;

                var task = Task.Factory.StartNew(() =>
                {
                    service.Start(posts);
                });
            }
                     
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            service.Stop();
            stopBtn.IsEnabled = false;
            runBtn.IsEnabled = true;
        }

        private void randomBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                posts.FolderPath = path.Text = dialog.SelectedPath;
           
            random = true;
        }
    }
}
