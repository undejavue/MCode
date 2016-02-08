using System;
using System.Collections.Generic;
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

using MCode.Views;
using MCode.UC;
using MClassLib;

namespace MCode.Views
{
    public class pageConfigData
    {
        //---- Статусы ----------------------
        // 1 = р/л  (В работе / лаборатория)
        // 2 = и/п  (К исполнению / приемка)
        // 3 = р/п  (В работе / приемка)
        // 4 = в/п  (Выполнено / приемка)

        public string currentPageName;

        public string errorText;
        public bool IsError;

        public int st_Prev;
        public int st_Current;
        public int st_Next;

        public char TTNmode;

        //-----------------------------------
        public int HS = 200; // высота списка накладных, малая
        public int HB = 400; // высота списка накладных, большая

        //----- Фильтр для выборки накладных
       

        public pageConfigData(string pageName)
        {
            this.currentPageName = pageName;

            IsError = false;

            config();
        }

        public void config()
        {
            switch (currentPageName)
            {
                case "Laboratory":
                    st_Prev = 0;
                    st_Current = 1;
                    st_Next = 2;
                    TTNmode = 'L';
                    break;
                case "Reception":
                    st_Prev = 2;
                    st_Current = 3;
                    st_Next = 4;
                    TTNmode = 'R';
                    break;
                case "TViewer":
                    st_Prev = 0;
                    st_Current = 0;
                    st_Next = 0;
                    TTNmode = 'T';
                    break;
            }
        }
    }
}
