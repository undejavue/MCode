using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

using System.Text;
using MClassLib;

namespace MCode
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract (Namespace="MCodeSilverlightServer",CallbackContract=typeof(IDuplexClient))]
    
    public interface IDuplexServer
    {
        [OperationContract(IsOneWay = true)]
        void TestWCF();

        //--- SELECt из однотипных таблиц
        [OperationContract]
        List<cListItem> srv_select_DataFromLists(string tbl, out string OpStatus);
    
        
        //---- SELECT список накладных или накладная
        [OperationContract(IsOneWay = true)]
        void srv_select_ConsignmentList(cFilter filter);


        //---- SELECT накладной расширенный
        [OperationContract]
        cConsignmentEx srv_select_ConsignmentEx(int? id, out string OpStatus);


        //---- Работа с однотипными таблицами
        [OperationContract(IsOneWay = true)]
        void srv_work_DataFromLists(string tbl, string action, cListItem item);


        //--- INSERT накладная
        [OperationContract(IsOneWay = true)]
        void srv_work_Consignment(cConsignmentItem item, cFilter filter);


        //--- CHANGE Status
        [OperationContract(IsOneWay = true)]
        void srv_upd_StatusChange(int ConsignmentNoteID, int StatusID);


        //--- SELECT Status List
        [OperationContract(IsOneWay = true)]
        void srv_select_StatusList();
    }


    [ServiceContract(Namespace="MCodeSilverlightClient")]
    public interface IDuplexClient
    {
        [OperationContract(IsOneWay = true,AsyncPattern = true)]
        void notify(int i, string s);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        void notifyWorkDataFromList(int i);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        void notifyWorkConsignmentItem(int i);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        void notifyStatusChanged(int i);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        void receiveConsignmentList(List<cConsignmentItem> list);

        [OperationContract(IsOneWay = true, AsyncPattern = true)]
        void receiveItemsList(List<cListItem> list);

        



    }



}
