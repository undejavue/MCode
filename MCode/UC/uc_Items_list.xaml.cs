using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.ServiceModel;
using System.ServiceModel.Channels;

using MCode.Views;
using MCode.MServiceD;
using MClassLib;

namespace MCode.UC
{
    public partial class uc_Items_list : UserControl
    {
        public string ucTableName;
        public ObservableCollection<cListItem> ucCollection = new ObservableCollection<cListItem>();

        public ObservableCollection<cListItem> ucNewItems = new ObservableCollection<cListItem>();
        public ObservableCollection<cListItem> ucUpdatedItems = new ObservableCollection<cListItem>();
        public ObservableCollection<cListItem> ucDeletedItems = new ObservableCollection<cListItem>();

        public cwnd_Message resultWindow = new cwnd_Message();


        public uc_Items_list()
        {
            InitializeComponent();

        }

        public void configure(string tbl, ObservableCollection<cListItem> list)
        {
            ucTableName = tbl;
            ucCollection = list;
            this.DataContext = ucCollection;
            ucCollection.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ucCollection_CollectionChanged);
        }

        void ucCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            btn_update.Visibility = Visibility.Visible;
        }

        private void btn_insertRow_Click(object sender, RoutedEventArgs e)
        {
            ucCollection.Add(new cListItem { Name="Введите текст",Short="текст"});
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            cListItem item = dg_Root.SelectedItem as cListItem;
            ucDeletedItems.Add(item);
            ucCollection.Remove(item);
        }

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ucCollection)
            {
                if (item.ID == null)
                {
                    ucNewItems.Add(item);
                }
                else
                {
                    ucUpdatedItems.Add(item);
                }
            }

            resultWindow.configure("Обновить таблицу?");
            resultWindow.Show();
            btn_update.Visibility = Visibility.Collapsed;
        }
    }
}
