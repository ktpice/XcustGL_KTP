using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XcustGL_KTP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ControlMain Cm = new ControlMain();
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToLower().Equals("XCustINV005_2"))
            {
                Application.Run(new XCustINV005_2(Cm));
            }
            else
            {
                
                Application.Run(new XCustINV005_2(Cm));
            }
        }
    }
}
