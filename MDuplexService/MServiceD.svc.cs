using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Text;
using MClassLib;

namespace MCode
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MServiceD : IDuplexServer
    {
        IDuplexClient client = OperationContext.Current.GetCallbackChannel<IDuplexClient>();

        public void TestWCF()
        {
            client = OperationContext.Current.GetCallbackChannel<IDuplexClient>();
            client.notify(1,"Ответ");
        }

        //---- SELECt из однотипных таблиц
        public List<cListItem> srv_select_DataFromLists(string tbl, out string OpStatus)
        {
           
           try
           {
               OpStatus = "Операция выполнена успешно";
               return DataManager.sel_DatFromLists(tbl) ;
           }
           catch (Exception err)
           {
               OpStatus = err.ToString();
               return null;
           }
        }

        //---- Работа с однотипными таблицами
        public void srv_work_DataFromLists(string tbl, string action, cListItem item)
        {
            client = OperationContext.Current.GetCallbackChannel<IDuplexClient>();
            try
            {
                DataManager.work_DataFromLists(tbl, action, item);
                client.notifyWorkDataFromList(1);
            }
            catch
            {
                client.notifyWorkDataFromList(0);
            }
        }

        //---- INSERT накладная
        public void srv_work_Consignment(cConsignmentItem item,cFilter filter)
        {
            try
            {
                DataManager.ins_upd_ConsignmentNote(item);

                client.notifyWorkConsignmentItem(1);
                client.receiveConsignmentList(DataManager.sel_ConsignmentNote(filter));
            }
            catch 
            {
                client.notifyWorkConsignmentItem(0);
            }
        }

        //---- SELECT список накладных или накладная
        public void srv_select_ConsignmentList(cFilter filter)
        {
            client = OperationContext.Current.GetCallbackChannel<IDuplexClient>();
            try
            {
                client.receiveConsignmentList( DataManager.sel_ConsignmentNote(filter) );
            }
            catch 
            {
                client.receiveConsignmentList( null );
            }
        }

        //---- SELECT накладной расширенный
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
                    cFilter filter = new cFilter();
                    filter.ConsignmentNoteID = id;
                    C_Ex.CNote = DataManager.sel_ConsignmentNote(filter).ToList()[0];
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
        public void srv_upd_StatusChange(int ConsignmentNoteID, int StatusID)
        {
            client = OperationContext.Current.GetCallbackChannel<IDuplexClient>();
            try
            {
                DataManager.upd_StatusChange(ConsignmentNoteID, StatusID);

                client.notifyStatusChanged(1);
            }
            catch 
            {
                client.notifyStatusChanged(0);
            }
        }

        //---- SELECT Status List
        public void srv_select_StatusList()
        {
            client = OperationContext.Current.GetCallbackChannel<IDuplexClient>();
            try
            {
                client.receiveItemsList(DataManager.sel_DatFromLists("StatusList"));
            }
            catch 
            {
                client.receiveItemsList(null);
            }
        }


        //------------------- Клиент -----------------------------------------------

        //public void Receive(int i) { }
        //public void ReceiveItem(cConsignmentEx item) { }
        //public void ReceiveList(List<cListItem> list) { }
        //public void ReceiveTTNList(List<cConsignmentItem> list) { }

        public void notify(int i, string s) 
        {
            int OpCode = i;
            string OpStatus = s;
        
        }

    }
}
