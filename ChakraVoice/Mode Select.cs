using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChakraVoice
{
    class Mode_Select
    {
        static string mode = "Winamp";
        public static void ModeSelect(string Speech)
        {
            switch (Speech)
            {
                case "Winamp Mode":
                    mode = "Winamp";
                    break;
                case "General Mode":
                    mode = "General";
                    break;
            }
            switch (mode)
            {
                case "Winamp":
                    Mode_Winamp.Winamp(Speech);
                    break;
                case "General":
                    Mode_General.General(Speech);
                    break;
            }
        }
    }
}
