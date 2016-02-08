using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Data;

using MCode.Views;
using MCode.UC;
using MCode.MServiceD;
using MClassLib;

namespace MCode.Views
{
    public partial class TViewer : Page
    {
        string thisName = "Tehnology";
        uc_TTN_list ucTTNlist_1;
        MDServiceClient mc = MDSLocalClient.proxy;
        cFilterList ucFilter = new cFilterList();
        List<RD_ex> listRD;
        ObservableCollection<cListItem> boxCol = new ObservableCollection<cListItem>();
        PagedCollectionView dgCol;

        public TViewer()
        {
            
            InitializeComponent();

            //show_TTNlist(null);

            mc.srv_select_RDCompleted += new EventHandler<srv_select_RDCompletedEventArgs>(mc_srv_select_RDCompleted);

            mc.srv_select_DataFromListsCompleted += new EventHandler<srv_select_DataFromListsCompletedEventArgs>(mc_srv_select_DataFromListsCompleted);
            mc.srv_select_DataFromListsAsync("ContractorList");

            ucFilter.refreshFilterDatePeriod(null, null);

            panel_control.DataContext = ucFilter;
            
            

        }

        void mc_srv_select_DataFromListsCompleted(object sender, srv_select_DataFromListsCompletedEventArgs e)
        {
            boxCol.Add(new cListItem { IsSelected = true, Name = "Выбраны все" });

            foreach (cListItem item in e.Result)
            {
                boxCol.Add(item);
            }

            box_Contragents.ItemsSource = boxCol;

        }


        void mc_srv_select_RDCompleted(object sender, srv_select_RDCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                if (e.Result.Count > 0)
                {
                    PagedCollectionView c1 = new PagedCollectionView(e.Result);

                    //c1.GroupDescriptions.Add(new PropertyGroupDescription(""));

                    listRD = new List<RD_ex>();
                    string name = e.Result[0].Contractor_Name;
                    RD_ex R = new RD_ex(name);
                    foreach (cRD item in c1)
                    {
                        if (name != item.Contractor_Name)
                        {
                            listRD.Add(R);
                            name = item.Contractor_Name;
                            R = new RD_ex(name);
                        }
                        R.addItem(item.MassType, item.Sort_Name, item.Val.ToString());
                    }

                    dgCol = new PagedCollectionView(GenerateDataB().ToDataSource());

                    dg_B.ItemsSource = dgCol;
                    //dg_F.ItemsSource = GenerateDataF().ToDataSource();
                }
            }

        }

        public IEnumerable<IDictionary> GenerateDataB()
        {
            foreach (RD_ex R in listRD)
            {
                var dict = new Dictionary<string, string>();
                dict = R.dictB;
                yield return dict;
            }
        }

        //public IEnumerable<IDictionary> GenerateDataF()
        //{
        //    foreach (RD_ex R in listRD)
        //    {
        //        var dict = new Dictionary<string, string>();
        //        dict = R.dictF;
        //        yield return dict;
        //    }
        //}

        //---- Показать список 1 ---------------------
        private void show_TTNlist(List<cListItem> list)
        {
            if (ucTTNlist_1 == null)
            {
                ucTTNlist_1 = new uc_TTN_list(thisName);
                panel_Root.Children.Add(ucTTNlist_1);
            }

            // Конфигурация фильтра
            if (list != null)
            {
                ucTTNlist_1.ucFilter.list_allStatus = list;
                ucTTNlist_1.ucFilter.selectedStatusLisClear();
            }
        }


        // Выполняется, когда пользователь переходит на эту страницу.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void btn_SetFilterDate_Click(object sender, RoutedEventArgs e)
        {
            mc.srv_select_RDAsync(null, (DateTime)ucFilter.BeginDT, (DateTime)ucFilter.EndDT);
        }


        class RD_ex
        {
            public Dictionary<string, string> dictB;
            //public Dictionary<string, string> dictF;

            public RD_ex(string name)
            {
                dictB = new Dictionary<string, string>();
                dictB.Add("Contractor", name);
                dictB.Add("B_sortExtra", "");
                dictB.Add("B_sortI", "");
                dictB.Add("B_sortII", "");
                dictB.Add("B_sortBC", "");
                dictB.Add("B_sortFrozen", "");
                dictB.Add("F_sortExtra", "");
                dictB.Add("F_sortI", "");
                dictB.Add("F_sortII", "");
                dictB.Add("F_sortBC", "");

                //dictF = new Dictionary<string, string>();
                //dictF.Add("F_sortExtra", "");
                //dictF.Add("F_sortI", "");
                //dictF.Add("F_sortII", "");
                //dictF.Add("F_sortBC", "");
                //dictF.Add("F_sortFrozen", "");

            }

            public void addItem(string syffics, string name, string value)
            {
            string t = "unknown";
            switch (syffics)
            {
                case "Принято в физическом весе":
                    t = "F_";
                    break;
                case "В пересчете на базисную жирность":
                    t = "B_";
                    break;
            }
            
            switch (name)
            {
                case "\"Экстра\"":
                    t += "sortExtra";
                    break;
                case "В/С":
                    t+= "sortBC";
                    break;
                case "I":
                    t+= "sortI";
                    break;
                case "II":
                    t+= "sortII";
                    break;
                case "Из него охлажденного не выше 10 С":
                    t+= "sortFrozen";
                    break;
            }
            if (dictB.Keys.Contains(t))
            {
                dictB[t] = value;
            }

            //if (dictF.Keys.Contains(t))
            //{
            //    dictF[t] = value;
            //}

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (boxCol[0].IsSelected == true)
            {
                boxCol[0].Name = "Выбраны все";
                foreach (cListItem item in boxCol)
                {
                    item.IsSelected = true;
                }
            }
            else
            {
                boxCol[0].Name = "Выбраны несколько";
            }

            

            //devicesCollection.Filter = delegate(object filterObject)
            //{
            //    wsPassportExtended Pass = (wsPassportExtended)filterObject;
            //    return (Pass.IsCalibrated == true);
            //};
        }
    }
}
