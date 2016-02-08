using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Runtime.Serialization;

using System.Linq;

using MCode.MServiceD;
using MClassLib;

namespace MCode
{
    public static class MDSLocalClient //: INotifyPropertyChanged
    {

        public static MDServiceClient proxy;

        private static bool isConnected;
        public static bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
            }
        }

        private static ObservableCollection<cListItem> _list_status;
        public static ObservableCollection<cListItem> list_status
        {
            get { return _list_status; }
            set
            {
                _list_status = value;
            }
        }

        private static ObservableCollection<cBaseItem> _list_clients;
        public static ObservableCollection<cBaseItem> list_clients
        {
            get { return _list_clients; }
            set
            {
                _list_clients = value;
            }
        }

        public static void createProxy()
        {
            proxy = new MDServiceClient();
            proxy.ConnectCompleted +=new EventHandler<ConnectCompletedEventArgs>(own_proxy_ConnectCompleted);
            proxy.srv_select_StatusListCompleted += new EventHandler<srv_select_StatusListCompletedEventArgs>(own_proxy_srv_select_StatusListCompleted);
            //proxy.SendUpdateClientsReceived += new EventHandler<SendUpdateClientsReceivedEventArgs>(own_proxy_SendUpdateClientsReceived);
            proxy.uploadClientsCompleted += new EventHandler<uploadClientsCompletedEventArgs>(own_proxy_uploadClientsCompleted);

        }

        public static void destroyProxy(string name)
        {
            if (IsConnected)
                proxy.DisonnectAsync(name);
            proxy = null;
        }


        //--- Прокси подключен ----------------------------------------------------
        static void own_proxy_ConnectCompleted(object sender, ConnectCompletedEventArgs e)
        {
            IsConnected = e.Result;
        }

        static void own_proxy_srv_select_StatusListCompleted(object sender, srv_select_StatusListCompletedEventArgs e)
        {
            list_status = e.Result;
        }

        static void own_proxy_SendUpdateClientsReceived(object sender, SendUpdateClientsReceivedEventArgs e)
        {
            proxy.uploadClientsAsync();
        }

        static void own_proxy_uploadClientsCompleted(object sender, uploadClientsCompletedEventArgs e)
        {
            list_clients = e.Result;
        }

        ////---- Property changes interface --------------------------
        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged(string propertyName)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //    {
        //        handler(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}
