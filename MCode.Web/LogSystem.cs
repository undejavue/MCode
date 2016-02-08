using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MClassLib;

namespace MCode.Web
{
    public class LogSystem
    {

        private string _rootDir;
        public string rootDir { get { return _rootDir; } set { _rootDir = value; } }

        private string _logFileName;
        public string logFileName { get { return _logFileName; } set { _logFileName = value; } }

        public void createLog()
        {
            //DateTime dt = DateTime.Now;
            //logFileName = rootDir + "\\MDServiceLog_" + dt.Year+ "."+dt.Month+"."+dt.Day+"__" + dt.Hour+"_"+dt.Minute+"_"+dt.Second + ".log";
        }

        public bool addMessageToLog(string s)
        {
            try
            {
                FileStream fs = new FileStream(logFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(s);
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public List<string> transferLog()
        {
            List<string> list;
            list =  File.ReadLines(logFileName).ToList();
            return list;
        }

        public LogSystem() 
        {
            //rootDir = HttpContext.Current.Server.MapPath("~/ClientBin");
            rootDir = System.Web.Hosting.HostingEnvironment.MapPath("~/MDServiceLog");
            logFileName = rootDir + "\\MDService.log";
        }


    }
}