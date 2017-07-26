using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models.ErrorHandling
{
    public class ErrorLog
    {
        private string _mExePath = "";


        public void LogError(string logMessage)
        {

            _mExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public void Log(string logMessage)
        {
            _mExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            try
            {
                using (StreamWriter w = File.AppendText(_mExePath + "\\" + "log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception)
            {
                throw new Exception("There is a problem. Please try again");
            }
        }

        private void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write(Environment.NewLine + "Log Entry : ");
                txtWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}\t:");
                txtWriter.WriteLine($"  :{logMessage}");
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}




