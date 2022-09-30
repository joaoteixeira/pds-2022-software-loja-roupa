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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projeto_PDS.Views
{
    /// <summary>
    /// Interação lógica para PageRelatorio.xam
    /// </summary>
    public partial class PageRelatorio : Page
    {
        public PageRelatorio()
        {
            InitializeComponent();
        }
        public void btListFun_Click(object sender, RoutedEventArgs e)
        {
            var form = new WindowFuncionario();
            form.ShowDialog();
        }
    }
}
