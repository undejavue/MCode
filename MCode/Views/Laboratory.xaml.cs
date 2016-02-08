using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.ServiceModel;
using System.ServiceModel.Channels;

using MCode.Views;
using MCode.UC;
using MCode.MServiceD;
using MClassLib;

namespace MCode
{
    public partial class Laboratory : Page
    {
        //--- Локальный клиент работы с БД
        string thisName = "Laboratory";
        MDServiceClient mc = MDSLocalClient.proxy;

        //--- Конфигурация текущей страницы
        pageConfigData ucPconfig;       // настройка статусов, размеров списков
        uc_TTN_item ucTTNitem;
        uc_TTN_list ucTTNlist_1;

        //--- Окно для UserControl
        cwnd_UC ucw; 
        
        public Laboratory()
        {
            InitializeComponent();
            this.Title = ApplicationStrings.HomePageTitle;
            Loaded += new System.Windows.RoutedEventHandler(Laboratory_Loaded);
            Unloaded += new RoutedEventHandler(Laboratory_Unloaded);
        }

        //--- Загрузка элемента -----------------------------------------------
        void Laboratory_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //---- Конфигурация -----------------------------------------------
            ucPconfig = new pageConfigData(thisName);

            //---- Создание клиента -------------------------------------------
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
               show_TTNlist(MDSLocalClient.list_status.ToList());
            }
            //------------------------------------------------
        }

        //--- Выгрузка элемента ---------------------------------------------------
        void Laboratory_Unloaded(object sender, RoutedEventArgs e)
        {
            mc.ConnectCompleted -= new EventHandler<ConnectCompletedEventArgs>(proxy_ConnectCompleted);
            mc.srv_select_StatusListCompleted -= new EventHandler<srv_select_StatusListCompletedEventArgs>(proxy_srv_select_StatusListCompleted);
            mc.SendMessageReceived -= new EventHandler<SendMessageReceivedEventArgs>(proxy_SendMessageReceived);
        }


        //--- Обработчики событий -------------------------------------------------
        void proxy_SendMessageReceived(object sender, SendMessageReceivedEventArgs e)
        {
            cwnd_Message w = new cwnd_Message(e.Message);
            w.Show();
        }

        //--- Прокси подключен ----------------------------------------------------
        void proxy_ConnectCompleted(object sender, ConnectCompletedEventArgs e)
        {
            mc.srv_select_StatusListAsync();
        }

        //--- Список статусов загружен --------------------------------------------
        void proxy_srv_select_StatusListCompleted(object sender, srv_select_StatusListCompletedEventArgs e)
        {
            show_TTNlist(e.Result.ToList());
        }


        //--- Код страницы ---------------------------------------------------------

        //---- Кнопка "Вставить" на форме вставки ТТН
        void ucTTNitem_btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            if (ucw != null)
                ucw.Close();
        }

        //---- Кнопка "Отмена" на форме вставки ТТН
        void ucTTNitem_btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (ucw != null)
                ucw.Close();
        }

        //---- Кнопка "Новая накладная"
        private void btn_New_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            show_TTNitem();
        }

        //--- Показать интерфейс новой накладной
        private void show_TTNitem()
        {
            if (ucTTNitem == null)
            {
                ucTTNitem = new uc_TTN_item(thisName);
                ucTTNitem.btn_Cancel.Click += new RoutedEventHandler(ucTTNitem_btn_Cancel_Click);
                ucTTNitem.btn_Insert.Click += new RoutedEventHandler(ucTTNitem_btn_Insert_Click); 
            }
            ucTTNitem.configure(ucPconfig);

            if (ucw == null)
            {
                ucw = new cwnd_UC();
            }

            ucw.configure("Новая накладная",ucTTNitem);
            ucw.Show();   
        }


        //---- Показать список 1 ---------------------
        private void show_TTNlist(List<cListItem> list)
        {
            if (ucTTNlist_1 == null)
            {
                ucTTNlist_1 = new uc_TTN_list(thisName);
                panel_list.Children.Add(ucTTNlist_1);
            }

            // Конфигурация фильтра
            if (list != null)
            {
                ucTTNlist_1.ucFilter.list_allStatus = list;
                ucTTNlist_1.ucFilter.selectedStatusLisClear();
                ucTTNlist_1.ucFilter.selectedStatusListAdd(ucPconfig.st_Current);
                ucTTNlist_1.ucFilter.selectedStatusListAdd(ucPconfig.st_Next);
            }
        }
    }
}