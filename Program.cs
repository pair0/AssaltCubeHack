using System.Diagnostics;
using System.Security.Principal;

namespace AssaltCubeH
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        //���� ù �κ� main �Լ�
        [STAThread]
        static void Main()
        {
            if (IsAdministrator() == false) // ������ �������� ������� �ʴ� ����� ..
            {
                try
                {
                    ProcessStartInfo procInfo = new ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = Application.ExecutablePath,
                        WorkingDirectory = Environment.CurrentDirectory,
                        Verb = "runas"
                    };

                    Process.Start(procInfo); //���μ��� ����� (������ ��������)
                }
                catch (Exception ex)
                {
                    // ����ڰ� ���α׷��� ������ �������� �����ϱ⸦ ������ ���� ��쿡 ���� ó��
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            else
            { // ó������ ���α׷��� ������ �������� ����ǰ� �ִ� ����� ..
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return false;
        }
    }
}