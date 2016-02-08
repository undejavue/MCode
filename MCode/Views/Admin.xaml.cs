using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Navigation;
using System.Net;
using System.Net.Browser;
using System.Windows;
using MClassLib;

using MCode.Views;
using MCode.MServiceD;
using MCode.UC;
using MCode;

using System.ServiceModel;
using System.ServiceModel.Channels;


namespace MCode
{
    public partial class Admin : Page
    {
        uc_Items_list ucItems;
        cTableList TL;
        uc_AdminTool ucAdmin;

        string thisName = "Admin";
        MDServiceClient mc = MDSLocalClient.proxy;

        public Admin()
        {
            InitializeComponent();

            this.Title = ApplicationStrings.AdminPageTitle;

            Loaded += new System.Windows.RoutedEventHandler(Admin_Loaded);
            Unloaded += new System.Windows.RoutedEventHandler(Admin_Unloaded);

            mc.srv_select_DataFromListsCompleted += new System.EventHandler<srv_select_DataFromListsCompletedEventArgs>(proxy_srv_select_DataFromListsCompleted);
            mc.SendTableUpdatedReceived += new System.EventHandler<SendTableUpdatedReceivedEventArgs>(proxy_SendTableUpdatedReceived);

            ucItems = new uc_Items_list();
            ucItems.resultWindow.Closed += new System.EventHandler(ucItems_resultWindow_Closed);

            ucAdmin = new uc_AdminTool();
            panel_msg.Children.Add(ucAdmin);

            if (!MDSLocalClient.IsConnected)
            {
                mc.ConnectAsync(thisName);
            }
        }

        void Admin_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            TL = new cTableList();
            panel_control.DataContext = TL;

            if (box_tables.Items.Count > 0)
                box_tables.SelectedIndex = box_tables.Items.IndexOf(TL.selected_table);
        }

        void Admin_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        void proxy_SendTableUpdatedReceived(object sender, SendTableUpdatedReceivedEventArgs e)
        {
            if (string.Compare(e.tableName, TL.selected_table.Name) == 0)
            {
                mc.srv_select_DataFromListsAsync(TL.selected_table.Name);
            }
        }

        void proxy_srv_select_DataFromListsCompleted(object sender, srv_select_DataFromListsCompletedEventArgs e)
        {
            ucItems.configure(TL.selected_table.Name, e.Result);
            if (!panel_T1.Children.Contains(ucItems))
                panel_T1.Children.Add(ucItems);
        }

        void ucItems_resultWindow_Closed(object sender, System.EventArgs e)
        {

            if (ucItems.ucDeletedItems.Count > 0)
                mc.srv_work_DataFromListsAsync(TL.selected_table.Name, "del", ucItems.ucDeletedItems);

            if (ucItems.ucUpdatedItems.Count > 0)
                mc.srv_work_DataFromListsAsync(TL.selected_table.Name, "upd", ucItems.ucUpdatedItems);

            if (ucItems.ucNewItems.Count > 0)
                mc.srv_work_DataFromListsAsync(TL.selected_table.Name, "ins", ucItems.ucNewItems);

        }

        private void box_tables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (box_tables.SelectedItem != null)
            {
                mc.srv_select_DataFromListsAsync(TL.selected_table.Name);
            }
        }

        private void btn_Invisible_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (panel_T1.Visibility == Visibility.Visible)
                panel_T1.Visibility = Visibility.Collapsed;
            else
                panel_T1.Visibility = Visibility.Visible;
        }


    }

    

}