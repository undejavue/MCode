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

using MClassLib;
using MCode.Views;
using MCode.MServiceD;

namespace MCode.Views
{
    public partial class cwnd_ListItemDF : ChildWindow
    {
        cListItem item;

        string table;

        public cwnd_ListItemDF(string tbl)
        {
            InitializeComponent();

            table = tbl;

            Loaded += new RoutedEventHandler(cwnd_ListItemDF_Loaded);
        }

        void cwnd_ListItemDF_Loaded(object sender, RoutedEventArgs e)
        {
            item = new cListItem();
            List<cListItem> list = new List<cListItem>();
            list.Add(item);
            this.DataContext = list;
        }


        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            item = dataForm.CurrentItem as cListItem;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

