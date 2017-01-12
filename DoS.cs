using System;
using System.ServiceProcess;
using System.Timers;

namespace Detection_of_State{


    public partial class DoS : ServiceBase{

        private string pcName = String.Empty;
        private string currentState = String.Empty;
        private Timer timer = null;


        public DoS(){
            InitializeComponent();

            pcName = Environment.MachineName;

            this.CanHandleSessionChangeEvent = true;    // Allow service to handle terminal server session change events 
            this.CanPauseAndContinue = true;
            this.CanStop = true;
            this.CanShutdown = true;
        }



        protected override void OnStart(string[] args) {

            timer = new Timer();         // Create a timer
            timer.Interval = 60000;  // 60 sec
            timer.Elapsed += new ElapsedEventHandler(timerEvent);   // Setup event
            timer.Start();

            RecordInformation.WriteLog("DoS started, " + pcName);
        }



        protected override void OnStop(){
            timer.Stop();
            RecordInformation.WriteLog("DoS stopped, " + pcName);
        }




        private void timerEvent(object sender, ElapsedEventArgs e){
                RecordInformation.WriteLog(currentState + ", " + pcName);
        }
        


        // Override ServiceBase's class method
        protected override void OnSessionChange(SessionChangeDescription changeDescription){


            switch (changeDescription.Reason){

                case SessionChangeReason.SessionLogon:
                    currentState = "Logged On";
                    break;

                case SessionChangeReason.SessionUnlock:
                    currentState = "Unlocked";
                    break;
                    
                case SessionChangeReason.SessionLock:
                    currentState = "Locked";
                    break;

                case SessionChangeReason.SessionLogoff:
                    currentState = "Logged Off";
                    break;

                case SessionChangeReason.ConsoleConnect:
                    currentState = "Console session connected";
                    break;

                case SessionChangeReason.ConsoleDisconnect:
                    currentState = "Console session disconnected";
                    break;

                case SessionChangeReason.RemoteConnect:
                    currentState = "Remote session connected";
                    break;

                case SessionChangeReason.RemoteDisconnect:
                    currentState = "Remote session disconnected";
                    break;

                case SessionChangeReason.SessionRemoteControl:
                    currentState = "Remote control status of session changed.";
                    break;

                default:
                    currentState = "None!";
                    break;
            }
        }


    }
}
