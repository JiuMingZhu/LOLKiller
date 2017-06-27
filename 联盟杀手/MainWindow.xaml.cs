using System;
using System.Windows;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using Timer = System.Timers.Timer;

namespace 联盟杀手
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Hashtable table = new Hashtable();

        //进程名称
        string processName = "";

        //查杀周期
        private string killCyle;

        private string realProcessName_1;
        private string realProcessName_2;

        private BackgroundWorker backgroundKiller;


        private delegate void ProcessDelegate();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            table.Add("网易云", "NeteaseMusic");
            table.Add("TGP", "tgp_daemon");
            table.Add("LOL", "LeagueClient");
        }


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //processName = ProcessName.Text;
            //killCyle = ScanCycle.Text;
            //backgroundKiller_Initial();
            //backgroundKiller.RunWorkerAsync();
            this.Close();
            while (true)
            {
                ProcessDelegate processDelegate=new ProcessDelegate(Killer);
                this.Dispatcher.Invoke(processDelegate);
                if (ScanCycle.Text.Equals(""))
                    Thread.Sleep(3000);
                else if(ScanCycle.Text.Equals("0"))
                    Thread.Sleep(100);
                else
                {
                    Thread.Sleep(Convert.ToInt32(ScanCycle.Text) * 1000);
                }
            }
        }


        private void backgroundKiller_Initial()
        {
            backgroundKiller = new BackgroundWorker();
            backgroundKiller.DoWork += BackgroundKiller_DoWork;
        }

        private void BackgroundKiller_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0;; i++)
            {
                string realProcessName_1 = (string) table[processName];
                string realProcessName_2 = (string) table["TGP"];
                Process[] processes;
                processes = Process.GetProcesses();
                foreach (Process process in processes)
                {
                    if (process.ProcessName.Equals(realProcessName_1) ||
                        process.ProcessName.Equals(realProcessName_2) || process.ProcessName.Equals("Taskmgr"))
                    {
                        process.Kill();
                        process.Close();
                    }

                }
                if (killCyle.Equals(""))
                {
                    Thread.Sleep(3000);
                }
                else
                {
                    try
                    {
                        Thread.Sleep(Convert.ToInt32(killCyle));
                    }
                    catch (Exception e_Killer)
                    {
                        MessageBox.Show(e_Killer.Message);
                        Thread.Sleep(3000);
                    }
                }
            }
        }



        public void Killer()
        {
                realProcessName_1 = (string)table[ProcessName.Text];
                realProcessName_2 = (string)table["TGP"];
                Process[] processes;
                processes = Process.GetProcesses();
                for (int i = 0; i < processes.Length; i++)
                {
                    if (processes[i].ProcessName.Equals(realProcessName_1) ||
                        processes[i].ProcessName.Equals(realProcessName_2) ||
                        processes[i].ProcessName.Equals("Taskmgr"))
                    {
                        processes[i].Kill();
                        processes[i].Close();
                    }
                }
        }

        private void ScanCycle_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key >= Key.D1 && e.Key <= Key.D9) ||
                e.Key == Key.Back || e.Key == Key.Left || e.Key == Key.Right)
            {
                if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
                    e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
