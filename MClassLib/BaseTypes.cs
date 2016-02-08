using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Linq;

[assembly: CLSCompliant(true)]
namespace MClassLib
{
    //--- Base item class
    [DataContract(Name = "cBaseItem", Namespace = "http://www.MCode.com/MClassLib/cBaseItem")]
    public class cBaseItem : INotifyPropertyChanged
    {
        private int? _ID;
        [DataMember]
        [Display(Name = "ID")]
        public int? ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ID"));
            }
        }

        private string _Name;
        [DataMember]
        [Display(Name = "Имя")]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        private string _Description;
        [DataMember]
        [Display(Name = "Описание")]
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Description"));
            }
        }

        public cBaseItem() { }

        public cBaseItem(int initID, string initName, string initD)
        {
            ID = initID;
            Name = initName;
            Description = initD;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }

    //--- List item class
    [DataContract(Name = "cListItem", Namespace = "http://www.MCode.com/MClassLib/cListItem")]
    public class cListItem : INotifyPropertyChanged
    {
        private int? _ID;
        [DataMember]
        [Display(Name = "ID")]
        public int? ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ID"));
            }
        }

        private string _Name;
        [DataMember]
        [Display(Name = "Имя")]
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        private string _Short;
        [DataMember]
        [Display(Name = "Сокращение")]
        public string Short
        {
            get { return _Short; }
            set
            {
                _Short = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Short"));
            }
        }

        private int _orderN;
        [DataMember]
        [Display(Name = "ID")]
        public int orderN
        {
            get { return _orderN; }
            set
            {
                _orderN = value;
                OnPropertyChanged(new PropertyChangedEventArgs("orderN"));
            }
        }

        private bool _IsSelected;
        [DataMember]
        [Display(Name = "IsSelected")]
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsSelected"));
            }
        }

        public cListItem() { }

        public cListItem(int initID, string initName, string initShort)
        {
            ID = initID;
            Name = initName;
            Short = initShort;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }

    //--- ConsignmentItem class
    [DataContract(Name = "cConsignmentItem", Namespace = "http://www.MCode.com/MClassLib/cConsignmentItem")]
    public class cConsignmentItem : cBaseItem
    {
        private DateTime _DT_In;
        [DataMember]
        [Display(Name = "Дата прихода")]
        public DateTime DT_In
        {
            get { return _DT_In; }
            set
            {
                _DT_In = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DT_In"));
            }
        }

        private DateTime _DT_Out;
        [DataMember]
        [Display(Name = "Дата ухода")]
        public DateTime DT_Out
        {
            get { return _DT_Out; }
            set
            {
                _DT_Out = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DT_Out"));
            }
        }

        private string _ConsigmentNote_N;
        [DataMember]
        [Display(Name = "№ накладной")]
        public string ConsigmentNote_N
        {
            get { return _ConsigmentNote_N; }
            set
            {
                _ConsigmentNote_N = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ConsigmentNote_N"));
            }
        }

        private double _RichnessVal;
        [DataMember]
        [Display(Name = "Содержание жира")]
        public double RichnessVal
        {
            get { return _RichnessVal; }
            set
            {
                _RichnessVal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RichnessVal"));
            }
        }

        private double _MassBaseRichnessVal;
        [DataMember]
        [Display(Name = "Масса в пересчете на базисную жирность")]
        public double MassBaseRichnessVal
        {
            get { return _MassBaseRichnessVal; }
            set
            {
                _MassBaseRichnessVal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MassBaseRichnessVal"));
            }
        }

        private double _AcidityVal;
        [DataMember]
        [Display(Name = "Кислотность")]
        public double AcidityVal
        {
            get { return _AcidityVal; }
            set
            {
                _AcidityVal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AcidityVal"));
            }
        }

        private double _TemperatureVal;
        [DataMember]
        [Display(Name = "Температура")]
        public double TemperatureVal
        {
            get { return _TemperatureVal; }
            set
            {
                _TemperatureVal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("TemperatureVal"));
            }
        }

        private double _DensityVal;
        [DataMember]
        [Display(Name = "Плотность")]
        public double DensityVal
        {
            get { return _DensityVal; }
            set
            {
                _DensityVal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DensityVal"));
            }
        }

        private double _MassNetVal;
        [DataMember]
        [Display(Name = "Масса нетто")]
        public double MassNetVal
        {
            get { return _MassNetVal; }
            set
            {
                _MassNetVal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MassNetVal"));
            }
        }

        private double _ProteinVal;
        [DataMember]
        [Display(Name = "Содержание белка")]
        public double ProteinVal
        {
            get { return _ProteinVal; }
            set
            {
                _ProteinVal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProteinVal"));
            }
        }

        private string _Comments;
        [DataMember]
        [Display(Name = "Комментарий")]
        public string Comments
        {
            get { return _Comments; }
            set
            {
                _Comments = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Comments"));
            }
        }

        private double _VolumeVal;
        [DataMember]
        [Display(Name = "Масса нетто (литры)")]
        public double VolumeVal
        {
            get { return _VolumeVal; }
            set
            {
                _VolumeVal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("VolumeVal"));
            }
        }

        private double _MassNetFactVal;
        [DataMember]
        [Display(Name = "Масса нетто фактическая")]
        public double MassNetFactVal
        {
            get { return _MassNetFactVal; }
            set
            {
                _MassNetFactVal = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MassNetFactVal"));
            }
        }

        //--------------------------------------------------------------

        private cListItem _Status;
        [DataMember]
        [Display(Name = "Состояние")]
        public cListItem item_Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Status"));
            }
        }

        private cListItem _Contractor;
        [DataMember]
        [Display(Name = "Контрагент")]
        public cListItem item_Contractor
        {
            get { return _Contractor; }
            set
            {
                _Contractor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Contractor"));
            }
        }

        private cListItem _Sort;
        [DataMember]
        [Display(Name = "Сорт")]
        public cListItem item_Sort
        {
            get { return _Sort; }
            set
            {
                _Sort = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sort"));
            }
        }

        private cListItem _PurityGroup;
        [DataMember]
        [Display(Name = "Группа по степени чистоты")]
        public cListItem item_PurityGroup
        {
            get { return _PurityGroup; }
            set
            {
                _PurityGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs("PurityGroup"));
            }
        }

        private cListItem _BacterialInsiminateClass;
        [DataMember]
        [Display(Name = "Класс по бактериальной обсемененности")]
        public cListItem item_BacterialInsiminateClass
        {
            get { return _BacterialInsiminateClass; }
            set
            {
                _BacterialInsiminateClass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BacterialInsiminateClass"));
            }
        }

        public cConsignmentItem()
        {
            item_BacterialInsiminateClass = new cListItem();
            item_Contractor = new cListItem();
            item_PurityGroup = new cListItem();
            item_Sort = new cListItem();
            item_Status = new cListItem();

            DT_In = DateTime.Now;
        }
    }

    //--- Consignment class
    [DataContract(Name = "cConsignmentEx", Namespace = "http://www.MCode.com/MClassLib/cConsignmentEx")]
    public class cConsignmentEx : INotifyPropertyChanged
    {
        private bool _IsLF_ReadOnly;
        [DataMember]
        [Display(Name = "Режим лаборатория (L)")]
        public bool IsLF_ReadOnly
        {
            get { return _IsLF_ReadOnly; }
            set
            {
                _IsLF_ReadOnly = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsLF_ReadOnly"));
            }
        }

        private bool _IsRF_ReadOnly;
        [DataMember]
        [Display(Name = "Режим приемка (R)")]
        public bool IsRF_ReadOnly
        {
            get { return _IsRF_ReadOnly; }
            set
            {
                _IsRF_ReadOnly = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsRF_ReadOnly"));
            }
        }


        private cConsignmentItem _CNote;
        [DataMember]
        [Display(Name = "Накладная")]
        public cConsignmentItem CNote
        {
            get { return _CNote; }
            set
            {
                _CNote = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CNote"));
            }
        }

        private List<cListItem> _list_Contractor;
        [DataMember]
        [Display(Name = "Контрагент")]
        public List<cListItem> list_Contractor
        {
            get { return _list_Contractor; }
            set
            {
                _list_Contractor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("list_Contractor"));
            }
        }

        private List<cListItem> _list_Sort;
        [DataMember]
        [Display(Name = "Сорт")]
        public List<cListItem> list_Sort
        {
            get { return _list_Sort; }
            set
            {
                _list_Sort = value;
                OnPropertyChanged(new PropertyChangedEventArgs("list_Sort"));
            }
        }

        private List<cListItem> _list_PurityGroup;
        [DataMember]
        [Display(Name = "Группа по степени чистоты")]
        public List<cListItem> list_PurityGroup
        {
            get { return _list_PurityGroup; }
            set
            {
                _list_PurityGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs("list_PurityGroup"));
            }
        }

        private List<cListItem> _list_BacterialInsiminateClass;
        [DataMember]
        [Display(Name = "Класс по бактериальной обсемененности")]
        public List<cListItem> list_BacterialInsiminateClass
        {
            get { return _list_BacterialInsiminateClass; }
            set
            {
                _list_BacterialInsiminateClass = value;
                OnPropertyChanged(new PropertyChangedEventArgs("list_BacterialInsiminateClass"));
            }
        }

        private List<cListItem> _list_Status;
        [DataMember]
        [Display(Name = "Операция")]
        public List<cListItem> list_Status
        {
            get { return _list_Status; }
            set
            {
                _list_Status = value;
                OnPropertyChanged(new PropertyChangedEventArgs("list_Status"));
            }
        }

        public cConsignmentEx()
        {
            CNote = new cConsignmentItem();
            list_BacterialInsiminateClass = new List<cListItem>();
            list_Contractor = new List<cListItem>();
            list_PurityGroup = new List<cListItem>();
            list_Sort = new List<cListItem>();
            list_Status = new List<cListItem>();

            //setMode('D');

        }

        public void setMode(char m)
        {
            switch (m)
            {
                case 'L': // Лаборатория (Laboratory)
                    IsLF_ReadOnly = false;
                    IsRF_ReadOnly = true;
                    break;
                case 'R': // Приемка (Reception)
                    IsLF_ReadOnly = true;
                    IsRF_ReadOnly = false;
                    break;
                default:
                    IsLF_ReadOnly = true;
                    IsRF_ReadOnly = true;
                    break;
            }
        }

        public void setCurrentItemsIndexes()
        {
            setItem(CNote.item_BacterialInsiminateClass, list_BacterialInsiminateClass);
            setItem(CNote.item_Contractor, list_Contractor);
            setItem(CNote.item_PurityGroup, list_PurityGroup);
            setItem(CNote.item_Sort, list_Sort);
        }

        private void setItem(cListItem item, List<cListItem> list)
        {
            if ((list != null) & (item != null))
            {
                foreach (var i in list)
                {
                    if (i.ID == item.ID)
                    {
                        item.orderN = i.orderN;
                        break;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

    }


    //--- Filter class
    [DataContract(Name = "cFilterlist", Namespace = "http://www.MCode.com/MClassLib/cFilterlist")]
    public class cFilterList : INotifyPropertyChanged
    {
        private int? _ConsignmentNoteID;
        [DataMember]
        public int? ConsignmentNoteID
        {
            get { return _ConsignmentNoteID; }
            set 
            {
                _ConsignmentNoteID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ConsignmentNoteID"));
            }

        }

        private List<cListItem> _list_allStatus;
        [DataMember]
        public List<cListItem> list_allStatus
        {
            get { return _list_allStatus; }
            set { _list_allStatus = value; OnPropertyChanged(new PropertyChangedEventArgs("list_allStatus")); }
        }

        private List<cListItem> _list_selectedStatus;
        [DataMember]
        public List<cListItem> list_selectedStatus
        {
            get { return _list_selectedStatus; }
            set { _list_selectedStatus = value; OnPropertyChanged(new PropertyChangedEventArgs("list_selectedStatus")); }
        }

        private DateTime? _BeginDT;
        [DataMember]
        public DateTime? BeginDT
        {
            get { return _BeginDT; }
            set
            {
                _BeginDT = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BeginDT"));
            }

        }

        private DateTime? _EndDT;
        [DataMember]
        public DateTime? EndDT
        {
            get { return _EndDT; }
            set
            {
                _EndDT = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EndDT"));
            }

        }

        private string _StatusIDString;
        [DataMember]
        public string StatusIDString
        {
            get { return _StatusIDString; }
            set 
            {
                _StatusIDString = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StatusIDString"));
            }
        }

        private string _StatusNameString;
        [DataMember]
        public string StatusNameString
        {
            get { return _StatusNameString; }
            set
            {
                _StatusNameString = value;
                OnPropertyChanged(new PropertyChangedEventArgs("StatusNameString"));
            }
        }

        public cFilterList() 
        {
            list_allStatus = new List<cListItem>();
            list_selectedStatus = new List<cListItem>();
            StatusIDString = "";
            StatusNameString = "";
        }

        public void selectedStatusLisClear()
        {
            list_selectedStatus.Clear();
            StatusIDString = "";
            StatusNameString = "";
        }

        public void selectedStatusListAdd(int id)
        {
            if (id != 0)
            {
                foreach (cListItem item in list_allStatus)
                {
                    if (item.ID == id)
                    {
                        list_selectedStatus.Add(item);
                        if (StatusIDString != "")
                        {
                            StatusIDString += ",";
                            StatusNameString += ",";
                        }
                        StatusIDString += item.ID.ToString();
                        StatusNameString += item.Name;
                    }
                }
            }
        }

        public string selectedStatusString()
        {
            if (StatusIDString == "")
            {
                return null; 
            }
            else
                return StatusIDString;
        }


        public void refreshFilterDatePeriod(DateTime? begin, DateTime? end)
        {
            if (end == null)
                EndDT = DateTime.Now;
            else
                EndDT = end;

            if (begin == null)
                BeginDT = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            else
                BeginDT = begin;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }


    //--- Filter class
    [DataContract(Name = "cFilterItem", Namespace = "http://www.MCode.com/MClassLib/cFilterItem")]
    public class cFilterItem : INotifyPropertyChanged
    {
        private int? _ID;
        [DataMember]
        public int? ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ID"));
            }

        }

        private char _ItemMode;
        [DataMember]
        public char ItemMode
        {
            get { return _ItemMode; }
            set
            {
                _ItemMode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ItemMode"));
            }

        }

        private int _st_Current;
        [DataMember]
        public int st_Current
        {
            get { return _st_Current; }
            set
            {
                _st_Current = value;
                OnPropertyChanged(new PropertyChangedEventArgs("st_Current"));
            }
        }

        private int _st_Next;
        [DataMember]
        public int st_Next
        {
            get { return _st_Next; }
            set
            {
                _st_Next = value;
                OnPropertyChanged(new PropertyChangedEventArgs("st_Next"));
            }
        }

        

        public cFilterItem()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }

    [DataContract(Name = "cTableList", Namespace = "http://www.MCode.com/MClassLib/cTableList")]
    public class cTableList : INotifyPropertyChanged
    {
        private List<cBaseItem> _list_tables;
        [DataMember]
        public List<cBaseItem> list_tables
        {
            get { return _list_tables; }
            set
            {
                _list_tables = value;
                OnPropertyChanged(new PropertyChangedEventArgs("list_tables"));
            }
        }

        private cBaseItem _selected_table;
        [DataMember]
        public cBaseItem selected_table
        {
            get { return _selected_table; }
            set
            {
                _selected_table = value;
                OnPropertyChanged(new PropertyChangedEventArgs("selected_table"));
            }
        }

        public cTableList()
        {
            list_tables = new List<cBaseItem>();

            selected_table = new cBaseItem { Name = "StatusList", Description = "Статусы" };

            list_tables.Add(selected_table);
            list_tables.Add(new cBaseItem { Name = "SortList", Description = "Сорта" });
            list_tables.Add(new cBaseItem { Name = "PurityGroupList", Description = "Группы чистоты" });
            list_tables.Add(new cBaseItem { Name = "ContractorList", Description = "Контрагенты" });
            list_tables.Add(new cBaseItem { Name = "BacterialInsiminateClassList", Description = "Классы бактериального осеменения" });

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

    }

        [DataContract(Name = "cMessageData", Namespace = "http://www.MCode.com/MClassLib/cMessageData")]
        public class cMessageData : INotifyPropertyChanged
        {
            private DateTime _msgDate;
            [DataMember]
            public DateTime msgDate
            {
                get { return _msgDate; }
                set
                {
                    _msgDate = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("msgDate"));
                }
            }
            private long _msgNum;
            [DataMember]
            public long msgNum
            {
                get { return _msgNum; }
                set
                {
                    _msgNum = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("msgNum"));
                }
            }
            private string  _msgText;
            [DataMember]
            public string msgText
            {
                get { return _msgText; }
                set
                {
                    _msgText = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("msgText"));
                }
            }

            public cMessageData() { }

            public string msgToString()
            {
                return msgNum + ": " + msgDate + " " + msgText;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, e);
            }
        }


        //---RDclass

        [DataContract(Name = "cRD", Namespace = "http://www.MCode.com/MClassLib/cRD")]
        public class cRD
        {
            private string _Contractor_Name;
            [DataMember]
            [Display(Name = "Контрагент")]
            public string Contractor_Name
            { get; set; }

            private string _MassType;
            [DataMember]
            [Display(Name = "Тип массы")]
            public string MassType
            { get; set; }

            private int _Sort_Name;
            [DataMember]
            [Display(Name = "Сорт")]
            public string Sort_Name
            { get; set; }

            private double _Val;
            [DataMember]
            [Display(Name = "Значение")]
            public double Val
            { get; set; }

        }
}
