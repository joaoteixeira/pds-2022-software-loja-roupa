﻿using System;
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
using System.Windows.Shapes;
using Projeto_PDS.Views;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Projeto_PDS
{
    /// <summary>
    /// Lógica interna para SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            media.Source = new Uri(Environment.CurrentDirectory + @"\video.mp4");
            
        }
        DispatcherTimer timer = new DispatcherTimer();

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {

        }
        private void timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            var form = new Projeto_PDS.SplashScreen();
            form.Show();
            this.Close();
        }
        void Loading()
        {
            timer.Tick += timer_tick;
            timer.Interval = new TimeSpan(0,0,4);
            timer.Start();
        }
    }
}
