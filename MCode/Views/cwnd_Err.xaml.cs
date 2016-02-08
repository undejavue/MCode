using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MCode.Views
{
    public partial class cwnd_Err : ChildWindow
    {
        public cwnd_Err(string WindowTitle, string ErrorDetails)
        {
            InitializeComponent();
            this.Title = WindowTitle;
            this.ErrorTextBloxk.Text = ErrorDetails;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}

