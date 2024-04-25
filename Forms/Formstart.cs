using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathWorks.MATLAB.NET.Arrays;
using Energy_absorb_optimize_design;
using System.Threading;

namespace Eenergy_absorb_and_optimize
{
	public partial class Formstart : Form
	{
		public Formstart()
		{
			InitializeComponent();
			label_startver.Text = Application.ProductVersion.ToString();
			backgroundWorker.WorkerReportsProgress = true;
			panelprogress.Width = 0;
			backgroundWorker.RunWorkerAsync();
		}

		//进度条自动前进
		private void Timer_Tick(object sender, EventArgs e)
		{
			if (panelprogress.Width < 576)
			{
				panelprogress.Width += 1;
			}
			else if(backgroundWorker.IsBusy)
			{
				labelinitial.Text = "请稍候...";
			}
		}

		//后台异步调用MATLAB实例初始化
		private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			Class_Optimize_design optimize_Design = new Class_Optimize_design();
			MWArray a = optimize_Design.chushi();
			Thread.Sleep(500);
			worker.ReportProgress(100);
		}

		//后台调用完成后返回完成信号
		private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			panelprogress.Width = 576;
			Thread.Sleep(500);
			DialogResult = DialogResult.OK;
			timer.Stop();
		}
	}
}
