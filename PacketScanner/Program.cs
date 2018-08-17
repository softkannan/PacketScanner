using PacketScanner.Packer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace PacketScanner
{
    static class Program
    {
        private static Dictionary<string, EmbeddedAssembly> _embeddedAssembly = new Dictionary<string, EmbeddedAssembly>();

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, int dwFlags);

        private static bool ScheduleDelete(string fileFullName)
        {
            if (!File.Exists(fileFullName))
            {
                return false;
            }
            return MoveFileEx(fileFullName, null, 0x04); //MOVEFILE_DELAY_UNTIL_REBOOT = 0x04
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (IsAdministrator() == false)
            {
                // Restart program and run as admin
                var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                Process proc =  System.Diagnostics.Process.Start(startInfo);
                proc.WaitForExit();
                return;
            }
            else
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    EmbeddedAssembly[] loadAssembly = { new EmbeddedAssembly() { AssemblyName = "ObjectListView", FileName = "ObjectListView.dll", ResourceName = "PacketScanner.Packer.ObjectListView.dll" } };

                    foreach (var item in loadAssembly)
                    {
                        //prepare dependant dll files
                        var resFile = new ResourceFile(item.ResourceName);
                        resFile.CopyTo("ObjectListView.dll");
                        item.File = resFile;

                        _embeddedAssembly.Add(item.AssemblyName, item);
                    }
                    AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                    Application.Run(new MainForm());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Unexpected Exception : {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    foreach (var item in _embeddedAssembly)
                    {
                        ScheduleDelete(item.Value.File.TargetfileName);
                    }
                }
            }
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assyName = new AssemblyName(args.Name);
            EmbeddedAssembly foundAssembly;
            if (_embeddedAssembly.TryGetValue(assyName.Name,out foundAssembly))
            {
                return foundAssembly.File.LoadAssembly();
            }
            return null;
        }
    }

    class EmbeddedAssembly
    {
        public string ResourceName;
        public string FileName;
        public string AssemblyName;
        public ResourceFile File;
    }
}
