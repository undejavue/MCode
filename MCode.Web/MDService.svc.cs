using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Text;
using MClassLib;

namespace MCode.Web
{


    //--- Интерфейс для обратного вызова клиента ------------------------------------------------------
    [ServiceContract(Namespace = "MCodeSilverlightClient")]
    public interface IMDSClient
    {
        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        IAsyncResult BeginSendMessage(string Message, AsyncCallback callback, object state);

        void EndSendMessage(IAsyncResult result);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        IAsyncResult BeginSendListItem(List<cListItem> list_items, AsyncCallback callback, object state);

        void EndSendListItem(IAsyncResult result);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        IAsyncResult BeginSendListTTN(List<cConsignmentItem> list_TTN, AsyncCallback callback, object state);

        void EndSendListTTN(IAsyncResult result);

        //--- Notify that table updated
        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        IAsyncResult BeginSendTableUpdated(string tableName, AsyncCallback callback, object state);

        void EndSendTableUpdated(IAsyncResult result);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        IAsyncResult BeginUploadMessageArhive(List<cMessageData> data, AsyncCallback callback, object state);

        void EndUploadMessageArhive(IAsyncResult result);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        IAsyncResult BeginSendSingleMessage(cMessageData data, AsyncCallback callback, object state);

        void EndSendSingleMessage(IAsyncResult result);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        IAsyncResult BeginSendUpdateClients(int code, AsyncCallback callback, object state);

        void EndSendUpdateClients(IAsyncResult result);
    }




    //--- Cервис с поддержкой дуплекса --------------------------------------------------------------------
    [ServiceContract (Namespace="MCodeSilverlightServer",CallbackContract=typeof(IMDSClient))]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MDService
    {
        private static Dictionary<int, MDSClient> Clients = new Dictionary<int, MDSClient>();
        private static List<cMessageData> msgArhive = new List<cMessageData>();
        private static long staticMsgNum = 0;
        private LogSystem LOG = new LogSystem();
        private static bool IsLogEnabled = true;
        private static bool IsSpamEnabled = true;
   
        [OperationContract]
        public bool Connect(string clientname)
        {
            IMDSClient _CallBackChannel = OperationContext.Current.GetCallbackChannel<IMDSClient>();
            IContextChannel _UserContextChannel = OperationContext.Current.Channel;
            int key = _UserContextChannel.GetHashCode();

            if (!Clients.Keys.Contains(key))
            {
                MDSClient _Client = new MDSClient(clientname, key, _CallBackChannel);
                Clients.Add(key, _Client);

                _Client.Fault += new EventHandler(client_Fault);
                _UserContextChannel.Faulted += new EventHandler(_UserContextChannel_Faulted);
                _UserContextChannel.Closing += new EventHandler(_UserContextChannel_Faulted);
                _UserContextChannel.Closed += new EventHandler(_UserContextChannel_Faulted);


                createNewMessage("Подключился клиент " + clientname + " (" + key.ToString() + ")");

                foreach (MDSClient c in Clients.Values.ToArray())
                {
                    c.SendUpdateClients();
                }
                return true;
            }

            return false;
        }

        void _UserContextChannel_Faulted(object sender, EventArgs e)
        {
            IContextChannel ch = sender as IContextChannel;
            int key = ch.GetHashCode();

            if (Clients.Keys.Contains(key))
            {
                Clients.Remove(key);

                createNewMessage("Client channel " + key.ToString() + " Fault or Closed");
                foreach (MDSClient c in Clients.Values.ToArray())
                {
                    c.SendUpdateClients();
                }
            }
        }

        void client_Fault(object sender, EventArgs e)
        {
            MDSClient clientOffline = (MDSClient)sender;
            int key = clientOffline.Key;

            if (Clients.Keys.Contains(key))
            {
                Clients.Remove(key);

                createNewMessage("Client callback channel " + clientOffline.ClientName + "(" + clientOffline.Key.ToString() + ") Fault ");

                foreach (MDSClient c in Clients.Values.ToArray())
                {
                    c.SendUpdateClients();
                }
            }
        }

        [OperationContract(IsOneWay=true)]
        public void Disonnect(string clientname)
        {
           //var channel = OperationContext.Current.GetCallbackChannel<IMDSClient>();
  
           //if (Clients.Keys.Contains(channel.GetHashCode()))
           //{  
           //    Clients.Remove(channel.GetHashCode());
           //    createNewMessage("Отключился клиент " + clientname + "(" + channel.GetHashCode().ToString() + ")");
           //}

           //foreach (MDSClient c in Clients.Values.ToArray())
           //{
           //    c.SendUpdateClients();
           //}
        }

        [OperationContract]
        public List<cMessageData> uploadMessageArhive()
        {
            if (msgArhive.Count > 0)
            {
                return msgArhive;
            }
            else
            {
                createNewMessage("Сообщения в архиве отсутствуют");
                return msgArhive;
            }
        }

        [OperationContract]
        public List<cBaseItem> uploadClients()
        {
            List<cBaseItem> list = new List<cBaseItem>();

            foreach (var c in Clients.ToArray())
            {
                list.Add(new cBaseItem { Name = c.Key.ToString(), Description = c.Value.ClientName });
            }
            return list;
        }

        private void createNewMessage(string text)
        {
            cMessageData msg = new cMessageData
            {
                msgDate = DateTime.Now,
                msgNum = staticMsgNum++,
                msgText = text
            };

            msgArhive.Add(msg);

            if (IsLogEnabled)
            LOG.addMessageToLog(msg.msgToString());

            if (IsSpamEnabled)
            foreach (MDSClient c in Clients.Values.ToArray())
            {
                c.SendSingleMessage(msg);
            }
        }


        //---- SELECT Status List
        [OperationContract]
        public List<cListItem> srv_select_StatusList()
        {
           return DataManager.sel_DatFromLists("StatusList");
        }

        //---- SELECt из однотипных таблиц
        [OperationContract]
        public List<cListItem> srv_select_DataFromLists(string tbl)
        {
            List<cListItem> items = DataManager.sel_DatFromLists(tbl);

            return items;
        }

        //---- Работа с однотипными таблицами
        [OperationContract(IsOneWay = true)]
        public void srv_work_DataFromLists(string tbl, string action, List<cListItem> items)
        {
            string t = "";

            if (items.Count > 0)
            {
                try
                {
                    foreach (var item in items)
                    {
                        DataManager.work_DataFromLists(tbl, action, item);
                    }
                    t = "Произведены изменения типа " + action + " в таблице " + tbl;

                    foreach (MDSClient c in Clients.Values.ToArray())
                    {
                        c.SendTableUpdated(tbl);
                    }
                }
                catch (Exception err)
                {
                    t = "Попытка изменений типа" + action + " в таблице " + tbl + "вызвала ошибку: " + err.Message;
                }
                createNewMessage(t);
            }
        }

        //---- INSERT накладная
        [OperationContract(IsOneWay=true)]
        public void srv_work_Consignment(cConsignmentItem item)
        {
            string t="";
            try
            {
                DataManager.ins_upd_ConsignmentNote(item);
                t = "Произведены изменения в таблице Consignments";
                foreach (MDSClient c in Clients.Values.ToArray())
                {
                    c.SendTableUpdated("Consignments");
                }
            }
            catch (Exception err)
            {
                foreach (MDSClient c in Clients.Values.ToArray())
                {
                    t = "Попытка вставки накладной не удалась, текст ошибки: " + err.Message;
                    c.SendMessage(t);
                } 
            }

            createNewMessage(t);
        }

        //---- SELECT список накладных или накладная
        [OperationContract]
        public List<cConsignmentItem> srv_select_ConsignmentList(cFilterList filter, out string statusString)
        {
            try
            {
                statusString = filter.StatusIDString;
                return DataManager.sel_ConsignmentNote(filter);
            }
            catch
            {
                statusString = filter.StatusIDString;
                return null;
            }

        }

        //---- SELECT накладной расширенный
        [OperationContract]
        public cConsignmentEx srv_select_ConsignmentEx(int? id, out string OpStatus)
        {
            cConsignmentEx C_Ex = new cConsignmentEx();

            try
            {
                C_Ex.list_BacterialInsiminateClass = DataManager.sel_DatFromLists("BacterialInsiminateClassList");
                C_Ex.list_Contractor = DataManager.sel_DatFromLists("ContractorList");
                C_Ex.list_PurityGroup = DataManager.sel_DatFromLists("PurityGroupList");
                C_Ex.list_Sort = DataManager.sel_DatFromLists("SortList");
                C_Ex.list_Status = DataManager.sel_DatFromLists("StatusList");

                if (id != null)
                {
                    cFilterList filter = new cFilterList();
                    filter.ConsignmentNoteID = id;
                    C_Ex.CNote = DataManager.sel_ConsignmentNote(filter).ToList()[0];
                    C_Ex.setCurrentItemsIndexes();
                }
                else
                {
                    C_Ex.CNote = new cConsignmentItem();

                    C_Ex.CNote.item_BacterialInsiminateClass = C_Ex.list_BacterialInsiminateClass[0];
                    C_Ex.CNote.item_Contractor = C_Ex.list_Contractor[0];
                    C_Ex.CNote.item_PurityGroup = C_Ex.list_PurityGroup[0];
                    C_Ex.CNote.item_Sort = C_Ex.list_Sort[0];
                    C_Ex.CNote.item_Status = C_Ex.list_Status[0];
                }

                OpStatus = "Операция выполнена успешно";
                return C_Ex;
            }
            catch (Exception err)
            {
                OpStatus = err.ToString();
                return null;
            }
        }

        //---- CHANGE Status
        [OperationContract(IsOneWay=true)]
        public void srv_upd_StatusChange(int ConsignmentNoteID, int StatusID)
        {
            DataManager.upd_StatusChange(ConsignmentNoteID, StatusID);
            
            createNewMessage("Для накладной ID=" + ConsignmentNoteID + ", задан статус " + StatusID);

            foreach (MDSClient c in Clients.Values.ToArray())
            {
                c.SendTableUpdated("Consignments");
            }
        }

        [OperationContract]
        public List<cRD> srv_select_RD(int? id, DateTime BeginDT, DateTime EndDT, out string OpStatus)
        {
            try
            {
                OpStatus = "Операция выполнена успешно";
                return DataManager.lp_sel_RegistryData(id, BeginDT, EndDT);
            }
            catch (Exception err)
            {
                OpStatus = err.ToString();
                return null;
            }
        }

    }
}
