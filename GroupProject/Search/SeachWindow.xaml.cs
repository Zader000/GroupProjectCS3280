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

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for SeachWindow.xaml
    /// </summary>
    public partial class SeachWindow : Window
    {
        public SeachWindow()
        {
            InitializeComponent();
            SearchIconImg.Source = new BitmapImage(new Uri(@".../../../Assets/Images/search.png", UriKind.Relative));
        }
    }
}
