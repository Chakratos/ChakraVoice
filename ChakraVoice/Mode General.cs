using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChakraVoice
{
    class Mode_General
    {
        public static void General(string Speech)
        {
            switch (Speech)
            {
                case "Firefox":
                    Process.Start("Firefox");
                    break;
                case "Schließen":
                    SendKeys.SendWait("%({F4})");
                    break;
            }
        }
    }
}
