using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eenergy_absorb_and_optimize
{
	static class Program
	{
		//实现高DPI支持
		[DllImport("user32.dll")]
		private static extern void SetProcessDPIAware();
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]       
		static void Main()
		{
			if (Environment.OSVersion.Version.Major >= 6)
			SetProcessDPIAware();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Formstart formstart = new Formstart();
			if (formstart.ShowDialog() == DialogResult.OK)
			{
				Application.Run(new Formmain());
			}
		}
	}
}
