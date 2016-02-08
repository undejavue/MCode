using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

using MClassLib;

namespace MCode.Web
{
    public class MDSClient
    {
        public string ClientName { get; set; }
        public IMDSClient ClientChannel;
        public int Key;

        private volatile bool isSending;
        public bool IsSending
        {
            get { return isSending; }
            set { isSending = value; }
        }


        public MDSClient(string name,int key, IMDSClient clientChannel)
        {
            Key = key;
            ClientName = name;
            ClientChannel = clientChannel;
        }

        public event EventHandler Fault;

        //--- Send update client list --------------------------------------------
        public void SendUpdateClients()
        {
            try
            {
                isSending = true;
                ClientChannel.BeginSendUpdateClients(1, new AsyncCallback(EndSendUpdateClients), null);
            }
            catch (Exception ex)
            {
                isSending = false;
                OnFault();

            }
        }

        private void EndSendUpdateClients(IAsyncResult result)
        {
            try
            {
                isSending = false;
                ClientChannel.EndSendUpdateClients(result);
            }
            catch
            {
                OnFault();
            }
        }
        //-------------------------------------------------------------------------------
      

        //--- Send message arhive --------------------------------------------
        public void SendMessageArhive(List<cMessageData> msgCollection)
        {
            try
            {
                isSending = true;
                ClientChannel.BeginUploadMessageArhive(msgCollection, new AsyncCallback(EndSendMessageArhive), null);
            }
            catch (Exception ex)
            {
                isSending = false;
                OnFault();
                
            }
        }

        private void EndSendMessageArhive(IAsyncResult result)
        {
            try
            {
                isSending = false;
                ClientChannel.EndUploadMessageArhive(result);
            }
            catch
            {
                OnFault();
            }
        }
        //-------------------------------------------------------------------------------

        //--- Send message  -------------------------------------------------------------
        public void SendSingleMessage(cMessageData msg)
        {
            try
            {
                isSending = true;
                ClientChannel.BeginSendSingleMessage(msg, new AsyncCallback(EndSendSingleMessage), null);
            }
            catch (Exception ex)
            {
                isSending = false;
                OnFault();
            }
        }

        private void EndSendSingleMessage(IAsyncResult result)
        {
            try
            {
                isSending = false;
                ClientChannel.EndSendSingleMessage(result);
            }
            catch
            {
                OnFault();
            }
        }
        //-------------------------------------------------------------------------------

        //--- Send notify that table updated --------------------------------------------
        public void SendTableUpdated(string tbl)
        {
            try
            {
                isSending = true;
                ClientChannel.BeginSendTableUpdated(tbl, new AsyncCallback(EndSendTable), null);
            }
            catch (Exception ex)
            {
                isSending = false;
                OnFault();
            }
        }

        private void EndSendTable(IAsyncResult result)
        {
            try
            {
                isSending = false;
                ClientChannel.EndSendTableUpdated(result);
            }
            catch
            {
                OnFault();
            }
        }
        //-------------------------------------------------------------------------------



        //--- Send message -----------------------------------------------------------
        public void SendMessage(string msg)
        {
            try
            {
                isSending = true;
                ClientChannel.BeginSendMessage(msg, new AsyncCallback(EndSendM), null);
            }
            catch (Exception ex)
            {
                isSending = false;
                OnFault();
            }
        }

        private void EndSendM(IAsyncResult result)
        {
            try
            {
                isSending = false;
                ClientChannel.EndSendMessage(result);
            }
            catch
            {
                OnFault();
            }
        }
        //------------------------------------------------------------------------------


        //--- Send items -----------------------------------------------------------
        public void SendItems(List<cListItem> items)
        {
            try
            {
                isSending = true;
                ClientChannel.BeginSendListItem(items, new AsyncCallback(EndSendItems), null);
            }
            catch (Exception ex)
            {
                isSending = false;
                OnFault();
            }
        }

        private void EndSendItems(IAsyncResult result)
        {
            try
            {
                isSending = false;
                ClientChannel.EndSendListItem(result);
            }
            catch
            {
                OnFault();
            }
        }
        //------------------------------------------------------------------------------


        //--- Send TTNs -----------------------------------------------------------
        public void SendTTNs(List<cConsignmentItem> items)
        {
            try
            {
                isSending = true;
                ClientChannel.BeginSendListTTN(items, new AsyncCallback(EndSendTTNs), null);
            }
            catch (Exception ex)
            {
                isSending = false;
                OnFault();
            }
        }

        private void EndSendTTNs(IAsyncResult result)
        {
            try
            {
                isSending = false;
                ClientChannel.EndSendListTTN(result);
            }
            catch
            {
                OnFault();
            }
        }
        //------------------------------------------------------------------------------


        private void OnFault()
        {
            if (ClientChannel != null)
            {
                var handler = Fault;

                if (handler != null)
                    handler(this, EventArgs.Empty);
            }
        }


    }



}