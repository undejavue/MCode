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
using MCode;

namespace MCode.UC
{
    public partial class uc_TTN_item : UserControl
    {
        cConsignmentEx TTN; 
        ObservableCollection<cConsignmentEx> TTN_collection;

        string thisName = "TTN_item";
        cFilterItem ucFilter;
        MDServiceClient mc = MDSLocalClient.proxy;

        //-----------------------------------
        public cwnd_Message resultWindow; 

        //--- Конструктор -----------------------------------------------------
        public uc_TTN_item(string name)
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(uc_TTN_item_Loaded);
            Unloaded += new RoutedEventHandler(uc_TTN_item_Unloaded);

            thisName = name + ":" + thisName;
        }

        //--- Загрузка элемента -----------------------------------------------
        void uc_TTN_item_Loaded(object sender, RoutedEventArgs e)
        {
            mc.srv_select_ConsignmentExCompleted += new EventHandler<srv_select_ConsignmentExCompletedEventArgs>(proxy_srv_select_ConsignmentExCompleted);
            TTN = new cConsignmentEx();
            resultWindow = new cwnd_Message();
            mc.srv_select_ConsignmentExAsync(ucFilter.ID);
        }

        //--- Выгрузка элемента -----------------------------------------------
        void uc_TTN_item_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        //--- Конфигурация ------------------------------------------------------
        public void configure(pageConfigData p, int? TTN_ID=null)
        {
            //--- Конфигурация фильтра для накладной
            ucFilter = new cFilterItem();
            ucFilter.ID = TTN_ID;
            ucFilter.st_Current = p.st_Current;
            ucFilter.st_Next = p.st_Next;
            ucFilter.ItemMode = p.TTNmode;         
            selectMode();
        }

        //--- Обработчики событий ----------------------------------------------
        void proxy_srv_select_ConsignmentExCompleted(object sender, srv_select_ConsignmentExCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                TTN = e.Result;
                TTN.setMode(ucFilter.ItemMode);
                TTN_collection = new ObservableCollection<cConsignmentEx>();
                TTN_collection.Add(TTN);
                dg_Root.ItemsSource = TTN_collection;
                dg_TTNbase.DataContext = TTN_collection[0];
            }
            else
            {
                cwnd_Err w = new cwnd_Err("Ошибка SELECT TTN", e.OpStatus);
                w.Show();
            }
        }

        //--- Код страницы ---------------------------------------------------------
        private void selectMode()
        {
            switch (ucFilter.ItemMode)
            {
                case 'L':
                    btn_Insert.Visibility = Visibility.Visible;
                    btn_Update.Visibility = Visibility.Collapsed;
                    break;
                case 'R':
                    btn_Insert.Visibility = Visibility.Collapsed;
                    btn_Update.Visibility = Visibility.Visible;
                    break;
            }
        }


        private void box_Contragents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cConsignmentEx C = TTN_collection[0];
        }

        private void btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            TTN_collection[0].CNote.item_Status.ID = ucFilter.st_Next;
            TTN_collection[0].CNote.ID = null;
            TTN_collection[0].CNote.DT_Out = DateTime.Now;
            mc.srv_work_ConsignmentAsync(TTN_collection[0].CNote);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void dg_Root_LoadingRow(object sender, DataGridRowEventArgs e)
        {
        }

        private void dg_TTN_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            TTN_collection[0].CNote.item_Status.ID = ucFilter.st_Next;
            TTN_collection[0].CNote.DT_Out = DateTime.Now;
            mc.srv_work_ConsignmentAsync(TTN_collection[0].CNote, ucFilter);
        }
    }
}
