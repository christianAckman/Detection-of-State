using System;
using System.IO;


namespace Detection_of_State{

    public static class RecordInformation{

        static StreamWriter write = null;

        public static void WriteLog(string message){

            try{

                write = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\DoSInfo.txt", true);

                write.WriteLine(DateTime.Now.ToString() + ", " + message);

                write.Flush();
                write.Close();
            }
            catch{}
        }


    }
}
