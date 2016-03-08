using WinAmpSDK;
using System.Diagnostics;

namespace ChakraVoice
{
    class Mode_Winamp
    {
        public static void Winamp(string Speech)
        {
            
            Winamp wa;
            double curVol;
            switch (Speech)
            {
                case "Musik":
                    wa = new Winamp(true);
                    break;
                case "Play":
                    wa = new Winamp(true);
                    wa.play();
                    break;
                case "Pause":
                    wa = new Winamp(true);
                    wa.pause();
                    break;
                case "Lauter":
                    wa = new Winamp(true);
                    curVol = wa.Volume / 255.0 * 100.0;
                    wa.Volume = curVol + 20;
                    break;
                case "Leiser":
                    wa = new Winamp(true);
                    curVol = wa.Volume / 255 * 100;
                    wa.Volume = curVol - 20;
                    break;
                case "Nächstes":
                    wa = new Winamp(true);
                    wa.next();
                    break;
                case "Letztes":
                    wa = new Winamp(true);
                    wa.last();
                    break;
                case "Winamp Schließen":
                    try
                    {
	                    Process[] proc = Process.GetProcessesByName("winamp");
	                    proc[0].Kill();
                    }
                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("Fehler");
                    }
                    break;
            }
        }
    }
}
