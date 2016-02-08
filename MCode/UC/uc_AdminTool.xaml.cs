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

using MCode.Views;
using MCode.MServiceD;
using MCode;

using MClassLib;


namespace MCode.UC
{
    public partial class uc_AdminTool : UserControl
    {
        string thisName = "Admin";
        MDServiceClient mc = MDSLocalClient.proxy;

        ObservableCollection<cMessageData> messagesCollection = new ObservableCollection<cMessageData>();
        
        public uc_AdminTool()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(uc_AdminTool_Loaded);
            Unloaded += new RoutedEventHandler(uc_AdminTool_Unloaded);
        }

        void uc_AdminTool_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        void uc_AdminTool_Loaded(object sender, RoutedEventArgs e)
        {
            mc.uploadMessageArhiveCompleted += new EventHandler<uploadMessageArhiveCompletedEventArgs>(mc_uploadMessageArhiveCompleted);
            mc.SendSingleMessageReceived += new EventHandler<SendSingleMessageReceivedEventArgs>(mc_SendSingleMessageReceived);
            mc.uploadClientsCompleted += new EventHandler<uploadClientsCompletedEventArgs>(mc_uploadClientsCompleted);
            mc.SendUpdateClientsReceived += new EventHandler<SendUpdateClientsReceivedEventArgs>(mc_SendUpdateClientsReceived);

            dg_Messages.ItemsSource = messagesCollection;

            mc.uploadClientsAsync();
            mc.uploadMessageArhiveAsync();

        }

        void mc_uploadMessageArhiveCompleted(object sender, uploadMessageArhiveCompletedEventArgs e)
        {
            messagesCollection.Clear();
            messagesCollection = e.Result;
            dg_Messages.ItemsSource = messagesCollection;
        }

        void mc_SendSingleMessageReceived(object sender, SendSingleMessageReceivedEventArgs e)
        {
            messagesCollection.Add(e.data);
        }

        void mc_uploadClientsCompleted(object sender, uploadClientsCompletedEventArgs e)
        {
            dg_Clients.ItemsSource = e.Result;
        }

        void mc_SendUpdateClientsReceived(object sender, SendUpdateClientsReceivedEventArgs e)
        {
            mc.uploadClientsAsync();
        }

        private void btn_clients_Click(object sender, RoutedEventArgs e)
        {
            mc.uploadClientsAsync();
        }

        private void btn_UploadMessages_Click(object sender, RoutedEventArgs e)
        {
            mc.uploadMessageArhiveAsync();
        }

    }
}
