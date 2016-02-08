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
    public partial class cwnd_UC : ChildWindow
    {
        public cwnd_UC()
        {
            InitializeComponent();

            Closed += new EventHandler(cwnd_UC_Closed);
        }

        void cwnd_UC_Closed(object sender, EventArgs e)
        {
            panel_Root.Children.Clear();
        }

        public bool configure(string title, UIElement uc)
        {
            this.Title = title;
            panel_Root.Children.Add(uc);

            return true;
        }


    }
}

