using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
//using System.Web.Configuration;
using System.Runtime.Serialization;


namespace MClassLib
{
      
    public class DataManager
    {

        public static object objectOrNull(object ob)
        {
            string s="";
            if (ob == null)
            {
                return DBNull.Value;
            }
            else if ( Object.ReferenceEquals(ob.GetType(),s.GetType()) )
            {
                if (ob == "")
                return DBNull.Value;
                else return ob;
            }
            else
                return ob;
        }

        //---- SELECT данных из однотипных таблиц 
        public static List<cListItem> sel_DatFromLists(string tblName)
        {
            List<cListItem> list = new List<cListItem>();
            using (SqlConnection conn = new SqlConnection(CommManager.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("lp_work_DataFromLists");
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TableName", tblName);
                cmd.Parameters.AddWithValue("@ActType", "sel");

                cmd.Parameters.Add("@rowcount_out", SqlDbType.Int);
                cmd.Parameters["@rowcount_out"].Direction = ParameterDirection.Output;

                SqlDataReader Reader = cmd.ExecuteReader();
                int i = 0;
                while (Reader.Read())
                {
                    cListItem item = new cListItem();
                    if (!Reader.IsDBNull(Reader.GetOrdinal("ID")))
                        item.ID = (int)Reader["ID"];
                    if (!Reader.IsDBNull(Reader.GetOrdinal("Name")))
                        item.Name = (string)Reader["Name"];
                    if (!Reader.IsDBNull(Reader.GetOrdinal("Short")))
                        item.Short = (string)Reader["Short"];
                    item.orderN = i;
                    i++;
                    list.Add(item);
                }
                Reader.Close();
                conn.Close();
            }

            return list;
        }


        //---- Работа с однотипными таблицами 
        public static int work_DataFromLists(string tblName, string actionType, cListItem insItem)
        {
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(CommManager.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("lp_work_DataFromLists");
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

              // @TableName
              //,@ActType
              //,@ID
              //,@ID_1C
              //,@Name
              //,@Short

                cmd.Parameters.AddWithValue("@TableName", tblName);
                cmd.Parameters.AddWithValue("@ActType", actionType);

                cmd.Parameters.AddWithValue("@ID", objectOrNull(insItem.ID));
                cmd.Parameters.AddWithValue("@Name", objectOrNull(insItem.Name));
                cmd.Parameters.AddWithValue("@Short", objectOrNull(insItem.Short));

                cmd.Parameters.Add("@rowcount_out", SqlDbType.Int);
                cmd.Parameters["@rowcount_out"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                ret = (int)cmd.Parameters["@rowcount_out"].Value;

                conn.Close();
            }
            return ret;
        }

        //----- SELECT списка накладных или одной накладной ------------------------------
        public static List<cConsignmentItem> sel_ConsignmentNote(cFilterList filter)
        {
            List<cConsignmentItem> list = new List<cConsignmentItem>();
            using (SqlConnection conn = new SqlConnection(CommManager.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("lp_sel_ConsignmentNote");
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                
                    // @ConsignmentNoteID int = null
                    // @BeginDT smalldatetime = null
                    // @EndDT	smalldatetime = null
                    // @StatusID int = null

                cmd.Parameters.AddWithValue("@ConsignmentNoteID", objectOrNull(filter.ConsignmentNoteID));
                cmd.Parameters.AddWithValue("@BeginDT", objectOrNull(filter.BeginDT));
                cmd.Parameters.AddWithValue("@EndDT", objectOrNull(filter.EndDT));
                cmd.Parameters.AddWithValue("@StatusID", objectOrNull(filter.StatusIDString));

                SqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    cConsignmentItem item = new cConsignmentItem();

                    if (!Reader.IsDBNull(Reader.GetOrdinal("ID")))
                        item.ID = (int)Reader["ID"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("DT_In")))
                        item.DT_In = (DateTime)Reader["DT_In"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("DT_Out")))
                        item.DT_Out = (DateTime)Reader["DT_Out"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("ConsignmentNote_N")))
                        item.ConsigmentNote_N = (string)Reader["ConsignmentNote_N"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("Contractor_ID")))
                        item.item_Contractor.ID = (int)Reader["Contractor_ID"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("ContractorName")))
                        item.item_Contractor.Name = (string)Reader["ContractorName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("ContractorShortName")))
                        item.item_Contractor.Short = (string)Reader["ContractorShortName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("Sort_ID")))
                        item.item_Sort.ID = (int)Reader["Sort_ID"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("SortName")))
                        item.item_Sort.Name = (string)Reader["SortName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("SortShortName")))
                        item.item_Sort.Short = (string)Reader["SortShortName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("PurityGroup_ID")))
                        item.item_PurityGroup.ID = (int)Reader["PurityGroup_ID"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("PurityGroupName")))
                        item.item_PurityGroup.Name = (string)Reader["PurityGroupName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("PurityGroupShortName")))
                        item.item_PurityGroup.Short = (string)Reader["PurityGroupShortName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("BacterialInsiminateClass_ID")))
                        item.item_BacterialInsiminateClass.ID = (int)Reader["BacterialInsiminateClass_ID"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("BacterialInsiminateClassName")))
                        item.item_BacterialInsiminateClass.Name = (string)Reader["BacterialInsiminateClassName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("BacterialInsiminateClassShortName")))
                        item.item_BacterialInsiminateClass.Short = (string)Reader["BacterialInsiminateClassShortName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("Status_ID")))
                        item.item_Status.ID = (int)Reader["Status_ID"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("StatusName")))
                        item.item_Status.Name = (string)Reader["StatusName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("StatusShortName")))
                        item.item_Status.Short = (string)Reader["StatusShortName"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("RichnessVal")))
                        item.RichnessVal = (double)Reader["RichnessVal"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("MassBaseRichnessVal")))
                        item.MassBaseRichnessVal = (double)Reader["MassBaseRichnessVal"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("AcidityVal")))
                        item.AcidityVal = (double)Reader["AcidityVal"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("TemperatureVal")))
                        item.TemperatureVal = (double)Reader["TemperatureVal"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("DensityVal")))
                        item.DensityVal = (double)Reader["DensityVal"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("MassNetVal")))
                        item.MassNetVal = (double)Reader["MassNetVal"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("VolumeVal")))
                        item.VolumeVal = (double)Reader["VolumeVal"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("MassNetFactVal")))
                        item.MassNetFactVal = (double)Reader["MassNetFactVal"];

                    if (!Reader.IsDBNull(Reader.GetOrdinal("Comments")))
                        item.Comments = (string)Reader["Comments"];

                    list.Add(item);
                }
                Reader.Close();
                conn.Close();
            }

            return list;
        }

        //------ INSERT UPDATE накладной
        public static int ins_upd_ConsignmentNote(cConsignmentItem C)
        {
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(CommManager.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("lp_ins_upd_ConsignmentNote");
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ConsignmentNoteID", objectOrNull(C.ID));
                cmd.Parameters.AddWithValue("@DT_In", objectOrNull(C.DT_In));
                cmd.Parameters.AddWithValue("@DT_Out", objectOrNull(C.DT_Out));
                cmd.Parameters.AddWithValue("@ConsignmentNote_N", C.ConsigmentNote_N);
                cmd.Parameters.AddWithValue("@Contractor_ID", objectOrNull(C.item_Contractor.ID));
                cmd.Parameters.AddWithValue("@Sort_ID", objectOrNull(C.item_Sort.ID));
                cmd.Parameters.AddWithValue("@PurityGroup_ID", objectOrNull(C.item_PurityGroup.ID));
                cmd.Parameters.AddWithValue("@BacterialInsiminateClass_ID", objectOrNull(C.item_BacterialInsiminateClass.ID));
                cmd.Parameters.AddWithValue("@Status_ID", objectOrNull(C.item_Status.ID));

                cmd.Parameters.AddWithValue("@RichnessVal", objectOrNull(C.RichnessVal));
                cmd.Parameters.AddWithValue("@MassBaseRichnessVal", objectOrNull(C.MassBaseRichnessVal));
                cmd.Parameters.AddWithValue("@AcidityVal", objectOrNull(C.AcidityVal));
                cmd.Parameters.AddWithValue("@TemperatureVal", objectOrNull(C.TemperatureVal));
                cmd.Parameters.AddWithValue("@DensityVal", objectOrNull(C.DensityVal));
                cmd.Parameters.AddWithValue("@ProteinVal", objectOrNull(C.ProteinVal));

                cmd.Parameters.AddWithValue("@Comments", objectOrNull(C.Comments));
                cmd.Parameters.AddWithValue("@VolumeVal", objectOrNull(C.VolumeVal));
                cmd.Parameters.AddWithValue("@MassNetFactVal", objectOrNull(C.MassNetVal));


                //cmd.Parameters.Add("@rowcount_out", SqlDbType.Int);
                //cmd.Parameters["@rowcount_out"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                ret = 1;

                //ret = (int)cmd.Parameters["@rowcount_out"].Value;

                conn.Close();
            }
            return ret;
        }


        //------ Изменение статуса накладной
        public static int upd_StatusChange(int ConsignmentNoteID, int StatusID)
        {
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(CommManager.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("lp_upd_StatusChange");
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                // @ConsignmentNoteID
                // @Status_ID

                cmd.Parameters.AddWithValue("@ConsignmentNoteID", ConsignmentNoteID);
                cmd.Parameters.AddWithValue("@StatusID", StatusID);

                cmd.ExecuteNonQuery();
                ret = 1;

                //ret = (int)cmd.Parameters["@rowcount_out"].Value;

                conn.Close();
            }
            return ret;
        }


        //---вызов ХП заполнение реестра
        public static List<cRD> lp_sel_RegistryData(int? ID, DateTime BeginDT, DateTime EndDT)
        {
            List<cRD> list = new List<cRD>();
            using (SqlConnection conn = new SqlConnection(CommManager.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("lp_sel_RegistryData");
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("@Contractor_ID", objectOrNull(ID));
                cmd.Parameters.AddWithValue("@BeginDT", BeginDT);
                cmd.Parameters.AddWithValue("@EndDT", EndDT);


                SqlDataReader Reader = cmd.ExecuteReader();

                while (Reader.Read())
                {
                    cRD item = new cRD();
                    if (!Reader.IsDBNull(Reader.GetOrdinal("Contractor")))
                        item.Contractor_Name = (string)Reader["Contractor"];
                    if (!Reader.IsDBNull(Reader.GetOrdinal("MassType")))
                        item.MassType = (string)Reader["MassType"];
                    if (!Reader.IsDBNull(Reader.GetOrdinal("Sort")))
                        item.Sort_Name = (string)Reader["Sort"];
                    if (!Reader.IsDBNull(Reader.GetOrdinal("MassValue")))
                        item.Val = (double)Reader["MassValue"];
                    list.Add(item);
                }
                Reader.Close();
                conn.Close();
            }

            return list;

        }
    }
}
