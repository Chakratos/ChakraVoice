using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace ChakraVoice
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
        GrammarBuilder gb = new GrammarBuilder();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();
        bool activ = true;
        bool autoMute = false;
        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
        }

		private void button_Click(object sender, RoutedEventArgs e)
		{
            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

            clist.Add(new string[]{"Demute","Auto Mute","Befehle","Musik","Play","Pause","Lauter","leiser","Nächtes","Letztes","Firefox","Schließen","Winamp Mode","General Mode","Winamp Schließen"} );
            Grammar gr = new Grammar(new GrammarBuilder(clist));
            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error");
            }
		}
        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "Auto Mute":
                    autoMute = true;
                    break;
                case "Demute":
                    activ = true;
                    break;
                case "Commands":
                    GrammarBuilder gb = new GrammarBuilder(clist);
                    string[] commands = gb.DebugShowPhrases.Trim('"').Trim('[').Trim(']').Split(',');
                    SpeechLog.AppendText(String.Join("\n",commands) + Environment.NewLine);
                    break;
                case "Winamp Mode":
                    lblCurMode.Content = "Winamp";
                    break;
                case "General Mode":
                    lblCurMode.Content = "General";
                    break;
            }
            if (activ)
            {
                Mode_Select.ModeSelect(e.Result.Text.ToString());
                toConsole(e.Result.Text.ToString());
                SpeechLog.ScrollToEnd();
                 if (autoMute)
                {
                    dispatcherTimer.Stop();
                    dispatcherTimer.Start();
                }
            }
            else
            {
                SpeechLog.AppendText("Du bist gemuted!" + Environment.NewLine);
            }
        }
        public void toConsole(string text)
        {
            SpeechLog.AppendText(text + Environment.NewLine);
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            sre.RecognizeAsyncStop();
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
            
        }
        public bool ApplicationIsActivated()
        {
            var activatedHandle = GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            activ = false;
            SpeechLog.AppendText("Auto Mute!" + Environment.NewLine);
        }
	}
}