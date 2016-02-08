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
    public partial class cwnd_Message : ChildWindow
    {
        public cwnd_Message(string message="Текст сообщения", string title="Сообщение" )
        {
            InitializeComponent();
            this.Title = title;
            this.txt_Message.Text = message;

        }

        public void configure(string message, string title = "Сообщение")
        {
            this.Title = title;
            this.txt_Message.Text = message;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

    }
}

