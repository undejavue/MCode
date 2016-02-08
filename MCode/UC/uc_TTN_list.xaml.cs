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

using System.ServiceModel;
using System.ServiceModel.Channels;

using MCode.Views;
using MCode.MServiceD;
using MClassLib;

namespace MCode.UC
{
    public partial class uc_TTN_list : UserControl
    {
        string thisName = "TTN_list";
        MDServiceClient mc = MDSLocalClient.proxy;

        public cFilterList ucFilter; 
        public int ucSelectedItemID;

        //--- Конструктор -----------------------------------------------------
        public uc_TTN_list(string name)
        {          
            InitializeComponent();

            Loaded += new RoutedEventHandler(uc_TTNList_Loaded);
            Unloaded += new RoutedEventHandler(uc_TTN_list_Unloaded);

            thisName = name + ":" + thisName;

            ucFilter = new cFilterList();
            ucFilter.refreshFilterDatePeriod(null, null);
            panel_control.DataContext = ucFilter;
            dg_Root.ItemsSource = new List<cConsignmentItem>();
        }

        //--- Загрузка элемента -----------------------------------------------
        void uc_TTNList_Loaded(object sender, RoutedEventArgs e)
        {
            mc.srv_select_ConsignmentListCompleted += new EventHandler<srv_select_ConsignmentListCompletedEventArgs>(proxy_srv_select_ConsignmentListCompleted);
            mc.SendTableUpdatedReceived += new EventHandler<SendTableUpdatedReceivedEventArgs>(proxy_SendTableUpdatedReceived);
            mc.srv_select_ConsignmentListAsync(ucFilter);
        }

        //--- Выгрузка элемента -----------------------------------------------
        void uc_TTN_list_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        //--- Конфигурация ------------------------------------------------------
        public void configure(cFilterList filter, int DataGridHeight = 400)
        {
        }

        //--- Обработчики событий ----------------------------------------------
        void proxy_SendTableUpdatedReceived(object sender, SendTableUpdatedReceivedEventArgs e)
        {
            if (string.Compare(e.tableName, "Consignments") == 0)
            {
                ucFilter.refreshFilterDatePeriod(ucFilter.BeginDT, null);
                mc.srv_select_ConsignmentListAsync(ucFilter);
            }
        }

        void proxy_srv_select_ConsignmentListCompleted(object sender, srv_select_ConsignmentListCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                if (string.Compare(e.statusString,ucFilter.StatusIDString)==0)

                dg_Root.ItemsSource = e.Result;
                if (e.Result.Count == 0) ucSelectedItemID = 0;
            }
        }

        //--- Код страницы ---------------------------------------------------------
        private void btn_SetFilterDate_Click(object sender, RoutedEventArgs e)
        {
            mc.srv_select_ConsignmentListAsync(ucFilter);
        }

        private void dg_Root_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((sender as DataGrid).SelectedItem as cConsignmentItem) != null)
                ucSelectedItemID = (int)((sender as DataGrid).SelectedItem as cConsignmentItem).ID;
        }


    }
}
