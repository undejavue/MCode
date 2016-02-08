using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Channels;

using MClassLib;


namespace MTestConsole
{
    class Program
    {
        private static MServiceD.MDuplexServiceClient proxy;
        private static EndpointAddress address;
        private static CustomBinding binding;

        static void Main(string[] args)
        {
            string OpStatus;
            int id = 152;

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
            }
            catch (Exception err)
            {
                OpStatus = err.ToString();
            }

        }


    }
}
