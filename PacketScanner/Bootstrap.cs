using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PacketScanner
{
    public class Bootstrap
    {
        public void Execute()
        {
            Application.Run(new MainForm());
        }
    }
}
