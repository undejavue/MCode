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
using System.Windows.Navigation;

using System.ServiceModel;
using System.ServiceModel.Channels;

using MCode.Views;
using MCode.MServiceD;
using MClassLib;
using MCode.UC;

namespace MCode.Views
{
    public partial class Reception : Page
    {
        //--- Локальный клиент работы с БД
        string thisName = "Reception";
        MDServiceClient mc = MDSLocalClient.proxy;

        //--- Конфигурация текущей страницы
        pageConfigData ucPconfig;       // настройка статусов, размеров списков

        uc_TTN_list ucTTNlist_1;        // список "К исполнению / В работе"
        uc_TTN_list ucTTNlist_2;        // список "Выполнено"
        uc_TTN_item ucTTNitem;

        //--- Окно для UserControl
        cwnd_UC ucw; 


        //--- Конструктор -----------------------------------------------------
        public Reception()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Reception_Loaded);
            Unloaded += new RoutedEventHandler(Reception_Unloaded);
        }

        //--- Загрузка элемента -----------------------------------------------
        void Reception_Loaded(object sender, RoutedEventArgs e)
        {
            //---- Конфигурация ------------------------------------------
            ucPconfig = new pageConfigData(thisName);

            //---- Создание клиента -----------------------
            mc.ConnectCompleted += new EventHandler<ConnectCompletedEventArgs>(proxy_ConnectCompleted);
            mc.srv_select_StatusListCompleted += new EventHandler<srv_select_StatusListCompletedEventArgs>(proxy_srv_select_StatusListCompleted);
            mc.SendMessageReceived += new EventHandler<SendMessageReceivedEventArgs>(proxy_SendMessageReceived);
            
            if (!MDSLocalClient.IsConnected)
            {
                mc.ConnectAsync(thisName);
            }
            else if (MDSLocalClient.list_status == null)
            {
                mc.srv_select_StatusListAsync();
            }
            else
            {
                show_receivedList(MDSLocalClient.list_status.ToList());
                show_sendList(MDSLocalClient.list_status.ToList());
            }
            //------------------------------------------------
        }

        //--- Выгрузка элемента -----------------------------------------------
        void Reception_Unloaded(object sender, RoutedEventArgs e)
        {
        }


        //--- Обработчики событий ----------------------------------------------
        void proxy_SendMessageReceived(object sender, SendMessageReceivedEventArgs e)
        {
            //cwnd_Message w = new cwnd_Message(e.Message);
            //w.Show();
        }

        //--- Прокси подключен ----------------------------------------------------
        void proxy_ConnectCompleted(object sender, ConnectCompletedEventArgs e)
        {
            mc.srv_select_StatusListAsync();
        }

        //--- Список статусов загружен --------------------------------------------
        void proxy_srv_select_StatusListCompleted(object sender, srv_select_StatusListCompletedEventArgs e)
        {
            show_receivedList(e.Result.ToList());
            show_sendList(e.Result.ToList());
        }

        //--- Код страницы ---------------------------------------------------------

        //---- Показать список 1 ---------------------------------------------------
        private void show_receivedList(List<cListItem> list) 
        {
            if (ucTTNlist_1 == null)
            {
                ucTTNlist_1 = new uc_TTN_list(thisName);
                panel_TTN_receivedList.Children.Add(ucTTNlist_1);
            }

            // Конфигурация фильтра
            if (list != null)
            {
                ucTTNlist_1.ucFilter.list_allStatus = list;
                ucTTNlist_1.ucFilter.selectedStatusLisClear();
                ucTTNlist_1.ucFilter.selectedStatusListAdd(ucPconfig.st_Prev);
                ucTTNlist_1.ucFilter.selectedStatusListAdd(ucPconfig.st_Current);
            }
        }


        //---- Показать список 2 ----------------------------------------------------
        private void show_sendList(List<cListItem> list) 
        {
            if (ucTTNlist_2 == null)
            {
                ucTTNlist_2 = new uc_TTN_list(thisName);
                panel_TTN_sendList.Children.Add(ucTTNlist_2);
            }

            // Конфигурация фильтра
            if (list != null)
            {
                ucTTNlist_2.ucFilter.list_allStatus = list;
                ucTTNlist_2.ucFilter.selectedStatusLisClear();
                ucTTNlist_2.ucFilter.selectedStatusListAdd(ucPconfig.st_Next);
            }
        }

        private void show_TTNitem(int? TTN_ID)
        {
            if (ucTTNitem == null)
            {
                ucTTNitem = new uc_TTN_item(thisName);
                ucTTNitem.btn_Cancel.Click += new RoutedEventHandler(ucTTNitem_btn_Cancel_Click);
                ucTTNitem.btn_Update.Click += new RoutedEventHandler(ucTTNitem_btn_Update_Click);
            }

            ucTTNitem.configure(ucPconfig, TTN_ID);

            if (ucw == null)
            {
                ucw = new cwnd_UC();
            }

            ucw.configure("Приемка", ucTTNitem);
            ucw.Show();   
        }



        void ucTTNitem_btn_Update_Click(object sender, RoutedEventArgs e)
        {
            ucw.Close();
        }

        void ucTTNitem_btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ucw.Close();
        }


        private void btn_Receive_Click(object sender, RoutedEventArgs e)
        {
            int id = ucTTNlist_1.ucSelectedItemID;
            if (id != 0)
            {
                if (id != ucPconfig.st_Current)
                    mc.srv_upd_StatusChangeAsync(id, ucPconfig.st_Current);

                show_TTNitem(id);
            }
        }
    }
}
