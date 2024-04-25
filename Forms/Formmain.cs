using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathWorks.MATLAB.NET.Arrays;
using Energy_absorb_optimize_design;
using System.Collections;
using Eenergy_absorb_and_optimize.Forms;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Eenergy_absorb_and_optimize
{
	public partial class Formmain : Form
	{
		public Formmain()
		{
			InitializeComponent();
			//预设模型：多胞型
			ComboBox_mode.SelectedIndex = 1;
			//初始化图表区：吸能
			chart.Series.Clear();
			chart.Titles[0].Text = "多胞薄壁结构三点弯曲吸能曲线";
			chart.ChartAreas[0].AxisX.Title = "弯曲变形转角 θ/rad";
			chart.ChartAreas[0].AxisY.Title = "吸收能量 -EA/J";
			//初始化列表区：吸能
			listresult.Clear();
			listresult.Groups.Clear();
			listresult.Columns.Add("θ", 100);
			listresult.Columns.Add("-EA", 100);
			btndm_cancel.Enabled = false;//取消按钮默认不可用
			btnsmc_cancel.Enabled = false;
			labelwait.Visible = false;//等待标志不可见
			label_time.Visible = false;
			//减少闪烁
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.Opaque, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}

		Class_Optimize_design optimize_Design = new Class_Optimize_design();//初始化算法函数实例
		public TabPage actpage = null;//表示当前活动标签页
		public bool iseng = true;//判断是否为吸能计算

		//单帽型材料牌号转化
		string DMzhuanhua(string str)
		{
			if (805 <= int.Parse(str) && int.Parse(str) <= 891)
			{
				str = "DP780";
			}
			else if (417 <= int.Parse(str) && int.Parse(str) <= 461)
			{
				str = "HSLA340";
			}
			else if (375 <= int.Parse(str) && int.Parse(str) <= 415)
			{
				str = "BLC";
			}
			else if (235 <= int.Parse(str) && int.Parse(str) <= 261)
			{
				str = "6060-T6";
			}
			else if (323 <= int.Parse(str) && int.Parse(str) <= 359)
			{
				str = "6061-T6";
			}
			return str;
		}
		//多胞型材料牌号转化
		string SMCzhuanhua(string str)
		{
			if (197 <= int.Parse(str) && int.Parse(str) <= 219)
			{
				str = "6063-T6";
			}
			else if (235 <= int.Parse(str) && int.Parse(str) <= 261)
			{
				str = "6060-T6";
			}
			else if (323 <= int.Parse(str) && int.Parse(str) <= 359)
			{
				str = "6061-T6";
			}
			return str;
		}

		#region 顶部菜单
		//模型选择下拉框
		private void ComboBox_mode_SelectedIndexChanged(object sender, EventArgs e)
		{
			label_time.Visible = false;
			if (ComboBox_mode.SelectedIndex == 0)
			{
				if (iseng)
				{
					//功能标签页切换
					actpage = tabdm_eng;
					actpage.SuspendLayout();
					actpage.ResumeLayout();
					tabControl.SelectTab(actpage);
					//初始化图表区：吸能
					chart.Series.Clear();
					chart.Titles[0].Text = "单帽型薄壁结构三点弯曲吸能曲线";
					chart.ChartAreas[0].AxisX.Title = "弯曲变形转角 θ/rad";
					chart.ChartAreas[0].AxisY.Title = "吸收能量 -EA/J";
					//初始化列表区：吸能
					listresult.Clear();
					listresult.Groups.Clear();
					listresult.Columns.Add("θ", 100);
					listresult.Columns.Add("-EA", 100);
				}
				else
				{
					//功能标签页切换
					actpage = tabdm_design;
					actpage.SuspendLayout();
					actpage.ResumeLayout();
					tabControl.SelectTab(actpage);
					//初始化图表区
					chart.Series.Clear();
					chart.Titles[0].Text = "单帽型薄壁结构截面设计优化结果";
					chart.ChartAreas[0].AxisX.Title = "吸收能量 -EA/J";
					chart.ChartAreas[0].AxisY.Title = "质量 Mass/kg";
					//初始化列表区
					listresult.Clear();
					listresult.Groups.Clear();
					listresult.Columns.Add("a", 75);//添加表头
					listresult.Columns.Add("b", 75);
					listresult.Columns.Add("t1", 75);
					listresult.Columns.Add("t2", 75);
					listresult.Columns.Add("mat1", 90);
					listresult.Columns.Add("mat2", 90);
					listresult.Columns.Add("-EA", 85);
					listresult.Columns.Add("Mass", 80);
				}
			}
			else
			{
				if (iseng)
				{
					//功能标签页切换
					actpage = tabsmc_eng;
					actpage.SuspendLayout();
					actpage.ResumeLayout();
					tabControl.SelectTab(actpage);
					//初始化图表区：吸能
					chart.Series.Clear();
					chart.Titles[0].Text = "多胞薄壁结构三点弯曲吸能曲线";
					chart.ChartAreas[0].AxisX.Title = "弯曲变形转角 θ/rad";
					chart.ChartAreas[0].AxisY.Title = "吸收能量 -EA/J";
					//初始化列表区：吸能
					listresult.Clear();
					listresult.Groups.Clear();
					listresult.Columns.Add("θ", 100);
					listresult.Columns.Add("-EA", 100);
				}
				else
				{
					//功能标签页切换
					actpage = tabsmc_design;
					actpage.SuspendLayout();
					actpage.ResumeLayout();
					tabControl.SelectTab(actpage);
					//初始化图表区
					chart.Series.Clear();
					chart.Titles[0].Text = "多胞薄壁结构截面设计优化结果";
					chart.ChartAreas[0].AxisX.Title = "吸收能量 -EA/J";
					chart.ChartAreas[0].AxisY.Title = "质量 Mass/kg";
					//初始化列表区
					listresult.Clear();
					listresult.Groups.Clear();
					listresult.Columns.Add("n", 50);//添加表头
					listresult.Columns.Add("a", 75);
					listresult.Columns.Add("b", 75);
					listresult.Columns.Add("t", 75);
					listresult.Columns.Add("mat", 90);
					listresult.Columns.Add("-EA", 85);
					listresult.Columns.Add("Mass", 80);
				}
			}
		}

		//导出图片
		private void Tsmd_daochupic_Click(object sender, EventArgs e)
		{
			sfd_chart.FileName = "Chart";
			ImageFormat format = ImageFormat.Png;
			if (sfd_chart.ShowDialog() == DialogResult.OK)
			{
				switch (sfd_chart.FilterIndex)
				{
					case 1:
						format = ImageFormat.Png;
						break;
					case 2:
						format = ImageFormat.Jpeg;
						break;
					case 3:
						format = ImageFormat.Bmp;
						break;
				}
				chart.SaveImage(sfd_chart.FileName, format);
				MessageBox.Show(this, "图片保存成功！", "信息提示");
			}
		}

		//导出列表
		private void Tsmd_daochulist_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd_list = new SaveFileDialog
			{
				Title = "保存列表结果至...",
				DefaultExt = "xls",
				Filter = "Excel文件(*.xls)|*.xls"
			};
			if (sfd_list.ShowDialog() == DialogResult.OK)
			{
				label_time.Text = "请稍候...";
				label_time.Visible = true;
				DoExport(listresult, sfd_list.FileName);
			}
		}
		#endregion

		#region 侧边栏按钮
		//三点弯曲 按钮
		private void Btn_absorb(object sender, EventArgs e)
		{
			iseng = true;
			label_time.Visible = false;
			//按钮颜色切换
			btn_absorb.BackColor = Color.FromArgb(192, 192, 255);
			btn_design.BackColor = Color.White;
			if (ComboBox_mode.SelectedIndex == 0)
			{
				//功能标签页切换
				actpage = tabdm_eng;
				actpage.SuspendLayout();
				actpage.ResumeLayout();
				tabControl.SelectTab(actpage);
				//初始化图表区
				chart.Series.Clear();
				chart.Titles[0].Text = "单帽型薄壁结构三点弯曲吸能曲线";
				chart.ChartAreas[0].AxisX.Title = "弯曲变形转角 θ/rad";
				chart.ChartAreas[0].AxisY.Title = "吸收能量 -EA/J";
				//初始化列表区
				listresult.Clear();
				listresult.Groups.Clear();
				listresult.Columns.Add("θ", 100);//添加表头
				listresult.Columns.Add("-EA", 100);
			}
			else
			{
				//功能标签页切换
				actpage = tabsmc_eng;
				actpage.SuspendLayout();
				actpage.ResumeLayout();
				tabControl.SelectTab(actpage);
				//初始化图表区：吸能
				chart.Series.Clear();
				chart.Titles[0].Text = "多胞薄壁结构三点弯曲吸能曲线";
				chart.ChartAreas[0].AxisX.Title = "弯曲变形转角 θ/rad";
				chart.ChartAreas[0].AxisY.Title = "吸收能量 -EA/J";
				//初始化列表区：吸能
				listresult.Clear();
				listresult.Groups.Clear();
				listresult.Columns.Add("θ", 100);
				listresult.Columns.Add("-EA", 100);
			}
		}

		//优化设计 按钮
		private void Btn_design_Click(object sender, EventArgs e)
		{
			iseng = false;
			label_time.Visible = false;
			//按钮颜色切换
			btn_design.BackColor = Color.PaleGreen;
			btn_absorb.BackColor = Color.White;
			if (ComboBox_mode.SelectedIndex == 0)
			{
				//功能标签页切换
				actpage = tabdm_design;
				actpage.SuspendLayout();
				actpage.ResumeLayout();
				tabControl.SelectTab(actpage);
				//初始化图表区
				chart.Series.Clear();
				chart.Titles[0].Text = "单帽型薄壁结构截面设计优化结果";
				chart.ChartAreas[0].AxisX.Title = "吸收能量 -EA/J";
				chart.ChartAreas[0].AxisY.Title = "质量 Mass/kg";
				//初始化列表区
				listresult.Clear();
				listresult.Groups.Clear();
				listresult.Columns.Add("a", 75);//添加表头
				listresult.Columns.Add("b", 75);
				listresult.Columns.Add("t1", 75);
				listresult.Columns.Add("t2", 75);
				listresult.Columns.Add("mat1", 90);
				listresult.Columns.Add("mat2", 90);
				listresult.Columns.Add("-EA", 85);
				listresult.Columns.Add("Mass", 80);
			}
			else
			{
				//功能标签页切换
				actpage = tabsmc_design;
				actpage.SuspendLayout();
				actpage.ResumeLayout();
				tabControl.SelectTab(actpage);
				//初始化图表区
				chart.Series.Clear();
				chart.Titles[0].Text = "多胞薄壁结构截面设计优化结果";
				chart.ChartAreas[0].AxisX.Title = "吸收能量 -EA/J";
				chart.ChartAreas[0].AxisY.Title = "质量 Mass/kg";
				//初始化列表区
				listresult.Clear();
				listresult.Groups.Clear();
				listresult.Columns.Add("n", 50);//添加表头
				listresult.Columns.Add("a", 75);
				listresult.Columns.Add("b", 75);
				listresult.Columns.Add("t", 75);
				listresult.Columns.Add("mat", 90);
				listresult.Columns.Add("-EA", 85);
				listresult.Columns.Add("Mass", 80);
			}
		}

		//关于 按钮
		private void Btn_about_Click(object sender, EventArgs e)
		{
			AboutBox about = new AboutBox();//初始化关于窗口
			about.ShowDialog();
		}
		#endregion

		#region 单帽型吸能计算参数校验        
		private void Textdm_a_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_a.Text, out double result) || double.Parse(textdm_a.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_a.Focus();
				textdm_a.SelectAll();
			}
		}

		private void Textdm_b_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_b.Text, out double result) || double.Parse(textdm_b.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_b.Focus();
				textdm_b.SelectAll();
			}
		}

		private void Textdm_t1_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_t1.Text, out double result) || double.Parse(textdm_t1.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_t1.Focus();
				textdm_t1.SelectAll();
			}
		}

		private void Textdm_t2_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_t2.Text, out double result) || double.Parse(textdm_t2.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_t2.Focus();
				textdm_t2.SelectAll();
			}
		}

		private void Textdm_f_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_f.Text, out double result) || double.Parse(textdm_f.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_f.Focus();
				textdm_f.SelectAll();
			}
		}

		private void Textdm_u1_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_u1.Text, out double result) || double.Parse(textdm_u1.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_u1.Focus();
				textdm_u1.SelectAll();
			}
		}

		private void Textdm_u2_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_u2.Text, out double result) || double.Parse(textdm_u2.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_u2.Focus();
				textdm_u2.SelectAll();
			}
		}
		#endregion

		#region 单帽型吸能示意图切换，绘图参数校验
		private void Textdm_xita_Enter(object sender, EventArgs e)
		{
			picboxdm_eng.Image = Properties.Resources.单帽型弯曲转角示意图;
		}

		private void Textdm_xita_Leave(object sender, EventArgs e)
		{
			picboxdm_eng.Image = Properties.Resources.单帽型参数示意图;
			if (!double.TryParse(textdm_xita.Text, out double result) || double.Parse(textdm_xita.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_xita.Focus();
				textdm_xita.SelectAll();
			}
		}

		private void Textdm_xitamax_Enter(object sender, EventArgs e)
		{
			picboxdm_eng.Image = Properties.Resources.单帽型弯曲转角示意图;
		}

		private void Textdm_xitamax_Leave(object sender, EventArgs e)
		{
			picboxdm_eng.Image = Properties.Resources.单帽型参数示意图;
			if (!double.TryParse(textdm_xitamax.Text, out double result) || double.Parse(textdm_xitamax.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_xitamax.Focus();
				textdm_xitamax.SelectAll();
			}
		}

		private void Textdm_n_Leave(object sender, EventArgs e)
		{
			if (!int.TryParse(textdm_n.Text, out int result) || int.Parse(textdm_n.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "请输入大于0的整数";
				alarm.ShowDialog();
				textdm_n.Focus();
				textdm_n.SelectAll();
			}
		}
		#endregion

		#region 单帽型截面设计参数校验
		private void Textdm_min1_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_min1.Text, out double result) || double.Parse(textdm_min1.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_min1.Focus();
				textdm_min1.SelectAll();
			}
		}

		private void Textdm_max1_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_max1.Text, out double result) || double.Parse(textdm_max1.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_max1.Focus();
				textdm_max1.SelectAll();
			}
		}

		private void Textdm_min2_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_min2.Text, out double result) || double.Parse(textdm_min2.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_min2.Focus();
				textdm_min2.SelectAll();
			}
		}

		private void Textdm_max2_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_max2.Text, out double result) || double.Parse(textdm_max2.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_max2.Focus();
				textdm_max2.SelectAll();
			}
		}

		private void Textdm_min3_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_min3.Text, out double result) || double.Parse(textdm_min3.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_min3.Focus();
				textdm_min3.SelectAll();
			}
		}

		private void Textdm_max3_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_max3.Text, out double result) || double.Parse(textdm_max3.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_max3.Focus();
				textdm_max3.SelectAll();
			}
		}

		private void Textdm_min4_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_min4.Text, out double result) || double.Parse(textdm_min4.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_min4.Focus();
				textdm_min4.SelectAll();
			}
		}

		private void Textdm_max4_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textdm_max4.Text, out double result) || double.Parse(textdm_max4.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textdm_max4.Focus();
				textdm_max4.SelectAll();
			}
		}

		private void Textdm_pop_Leave(object sender, EventArgs e)
		{
			if (!int.TryParse(textdm_pop.Text, out int result) || int.Parse(textdm_pop.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "请输入大于0的整数";
				alarm.ShowDialog();
				textdm_pop.Focus();
				textdm_pop.SelectAll();
			}
		}

		private void Textdm_gen_Leave(object sender, EventArgs e)
		{
			if (!int.TryParse(textdm_gen.Text, out int result) || int.Parse(textdm_gen.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "请输入大于0的整数";
				alarm.ShowDialog();
				textdm_gen.Focus();
				textdm_gen.SelectAll();
			}
		}
		#endregion

		#region 单帽型吸能计算与绘图
		//吸能计算 按钮
		private void Btndm_engcalc_Click(object sender, EventArgs e)
		{
			MWArray dma = double.Parse(textdm_a.Text);
			MWArray dmb = double.Parse(textdm_b.Text);
			MWArray dmt1 = double.Parse(textdm_t1.Text);
			MWArray dmt2 = double.Parse(textdm_t2.Text);
			MWArray dmf = double.Parse(textdm_f.Text);
			MWArray dmu1 = double.Parse(textdm_u1.Text);
			MWArray dmu2 = double.Parse(textdm_u2.Text);
			MWArray dmp = double.Parse(textdm_xita.Text);
			MWArray dmEz = optimize_Design.energy_danmao(dma, dmb, dmt1, dmt2, dmf, dmu1, dmu2, dmp);
			textdm_eng.Text = dmEz.ToString();
		}

		//绘图 按钮
		private void Btndm_draw_Click(object sender, EventArgs e)
		{
			#region 调用MATLAB计算各点吸能
			MWArray dma = double.Parse(textdm_a.Text);
			MWArray dmb = double.Parse(textdm_b.Text);
			MWArray dmt1 = double.Parse(textdm_t1.Text);
			MWArray dmt2 = double.Parse(textdm_t2.Text);
			MWArray dmf = double.Parse(textdm_f.Text);
			MWArray dmu1 = double.Parse(textdm_u1.Text);
			MWArray dmu2 = double.Parse(textdm_u2.Text);
			double dmpm = double.Parse(textdm_xitamax.Text);
			int dmn = int.Parse(textdm_n.Text);//数据点个数
			double[] dmx = new double[dmn];
			double[] dmy = new double[dmn];
			double dmxita = 0;
			for (int i = 0; i < dmn; i++)
			{
				dmx[i] = dmxita;
				MWArray dmp = dmxita;
				MWNumericArray Ez = (MWNumericArray)optimize_Design.energy_danmao(dma, dmb, dmt1, dmt2, dmf, dmu1, dmu2, dmp);
				dmy[i] = Ez.ToScalarDouble();
				dmxita += dmpm / dmn;
			}
			#endregion

			#region 将曲线绘制到图上，显示结果
			//对比模式-关
			if (!checkdm_eng.Checked)
			{
				//初始化曲线
				chart.Series.Clear();
				Series line = new Series("Line" + string.Format("{0:G}", chart.Series.Count + 1))
				{
					BorderWidth = 2,
					ChartArea = "ChartArea",
					ChartType = SeriesChartType.Spline,
					Font = new Font("Times New Roman", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
					LabelToolTip = "θ #VALX{F2}",
					Legend = "Legend1",
					MarkerSize = 7,
					MarkerStyle = MarkerStyle.Circle,
					Name = "Line" + string.Format("{0:G}", chart.Series.Count + 1),
					ToolTip = "θ：#VALX{F2}\n-EA：#VAL{F2}",
				};
				chart.Series.Add(line);
				//向曲线添加数据点
				for (int i = 0; i < dmn; i++)
				{
					chart.Series[0].Points.AddXY(dmx[i], dmy[i]);
				}
				chart.Series[0].ToolTip = "θ：#VALX{F2}\n-EA：#VAL{F2}";
				chart.ChartAreas[0].RecalculateAxesScale();
				//添加结果至列表
				listresult.BeginUpdate();
				listresult.Items.Clear();
				listresult.Groups.Clear();
				ListViewGroup lvgp = new ListViewGroup
				{
					Name = listresult.Groups.Count.ToString(),
					Header = chart.Series[0].Name,
					HeaderAlignment = HorizontalAlignment.Center
				};
				listresult.Groups.Add(lvgp);
				for (int i = 0; i < dmn; i++)
				{
					listresult.Items.Add(dmx[i].ToString("F4"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(dmy[i].ToString("F1"));
					listresult.Groups[0].Items.Add(listresult.Items[listresult.Items.Count - 1]);
				}
				listresult.EndUpdate();
			}
			//对比模式-开
			else
			{
				//初始化曲线
				Series newline = new Series("Line" + string.Format("{0:G}", chart.Series.Count + 1))
				{
					BorderWidth = 2,
					ChartArea = "ChartArea",
					ChartType = SeriesChartType.Spline,
					Font = new Font("Times New Roman", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
					LabelToolTip = "θ #VALX{F2}",
					Legend = "Legend1",
					MarkerSize = 7,
					MarkerStyle = MarkerStyle.Circle,
					Name = "Line" + string.Format("{0:G}", chart.Series.Count + 1),
					ToolTip = "θ：#VALX{F2}\n-EA：#VAL{F2}",
				};
				chart.Series.Add(newline);
				//向曲线添加数据点
				for (int i = 0; i < dmn; i++)
				{
					chart.Series[chart.Series.Count - 1].Points.AddXY(dmx[i], dmy[i]);
				}
				chart.Series[chart.Series.Count - 1].ToolTip = "θ：#VALX{F2}\n-EA：#VAL{F2}";
				chart.ChartAreas[0].RecalculateAxesScale();
				//添加结果至列表
				listresult.BeginUpdate();
				ListViewGroup newlvgp = new ListViewGroup
				{
					Name = listresult.Groups.Count.ToString(),
					Header = chart.Series[chart.Series.Count - 1].Name,
					HeaderAlignment = HorizontalAlignment.Center
				};
				listresult.Groups.Add(newlvgp);
				for (int i = 0; i < dmn; i++)
				{
					listresult.Items.Add(dmx[i].ToString("F4"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(dmy[i].ToString("F1"));
					listresult.Groups[chart.Series.Count - 1].Items.Add(listresult.Items[listresult.Items.Count - 1]);
				}
				listresult.EndUpdate();
			}
			#endregion           
		}

		//清除结果 按钮
		private void Btndm_engclear_Click(object sender, EventArgs e)
		{
			chart.Series.Clear();
			listresult.Groups.Clear();
			listresult.Items.Clear();
			textdm_eng.Text = "";
		}
		#endregion

		#region 单帽型截面设计
		//优化求解 按钮
		private void Btndm_solve_Click(object sender, EventArgs e)
		{
			//验证设计参数范围正确性
			if (double.Parse(textdm_min1.Text) > double.Parse(textdm_max1.Text))
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "最小值不能超过最大值";
				alarm.ShowDialog();
				textdm_min1.ForeColor = Color.Red;
				textdm_max1.ForeColor = Color.Red;
			}
			else if (double.Parse(textdm_min2.Text) > double.Parse(textdm_max2.Text))
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "最小值不能超过最大值";
				alarm.ShowDialog();
				textdm_min2.ForeColor = Color.Red;
				textdm_max2.ForeColor = Color.Red;
			}
			else if (double.Parse(textdm_min3.Text) > double.Parse(textdm_max3.Text))
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "最小值不能超过最大值";
				alarm.ShowDialog();
				textdm_min3.ForeColor = Color.Red;
				textdm_max3.ForeColor = Color.Red;
			}
			else if (double.Parse(textdm_min4.Text) > double.Parse(textdm_max4.Text))
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "最小值不能超过最大值";
				alarm.ShowDialog();
				textdm_min4.ForeColor = Color.Red;
				textdm_max4.ForeColor = Color.Red;
			}
			else
			{
				textdm_min1.ForeColor = SystemColors.WindowText;
				textdm_max1.ForeColor = SystemColors.WindowText;
				textdm_min2.ForeColor = SystemColors.WindowText;
				textdm_max2.ForeColor = SystemColors.WindowText;
				textdm_min3.ForeColor = SystemColors.WindowText;
				textdm_max3.ForeColor = SystemColors.WindowText;
				textdm_min4.ForeColor = SystemColors.WindowText;
				textdm_max4.ForeColor = SystemColors.WindowText;
				//后台进行遗传算法优化
				if (!backworker1.IsBusy)
				{
					backworker1.RunWorkerAsync();
				}
				label_time.Visible = false;
				labelwait.Visible = true;
				#region 取消界面操作控制权
				btndm_cancel.Enabled = true;
				ComboBox_mode.Enabled = false;
				tsm_daochu.Enabled = false;
				btn_absorb.Enabled = false;
				btn_design.Enabled = false;
				btn_about.Enabled = false;
				textdm_min1.Enabled = false;
				textdm_max1.Enabled = false;
				textdm_min2.Enabled = false;
				textdm_max2.Enabled = false;
				textdm_min3.Enabled = false;
				textdm_max3.Enabled = false;
				textdm_min4.Enabled = false;
				textdm_max4.Enabled = false;
				textdm_pop.Enabled = false;
				textdm_gen.Enabled = false;
				textdmpre_l1.Enabled = false;
				textdmpre_f.Enabled = false;
				textdmpre_xita.Enabled = false;
				textdmpre_mat.Enabled = false;
				btndm_solve.Enabled = false;
				btndm_designclear.Enabled = false;
				btn_outfig.Enabled = false;
				btn_outlist.Enabled = false;
				#endregion
			}
		}

		//后台遗传算法计算
		private void Backworker1_DoWork(object sender, DoWorkEventArgs e)
		{
			Stopwatch sw1 = new Stopwatch();
			sw1.Start();
			//遗传算法优化
			MWArray dmamin = double.Parse(textdm_min1.Text);
			MWArray dmamax = double.Parse(textdm_max1.Text);
			MWArray dmbmin = double.Parse(textdm_min2.Text);
			MWArray dmbmax = double.Parse(textdm_max2.Text);
			MWArray dmt1min = double.Parse(textdm_min3.Text);
			MWArray dmt1max = double.Parse(textdm_max3.Text);
			MWArray dmt2min = double.Parse(textdm_min4.Text);
			MWArray dmt2max = double.Parse(textdm_max4.Text);
			MWArray dmpop = int.Parse(textdm_pop.Text);
			MWArray dmgen = int.Parse(textdm_gen.Text);
			MWArray dmmaterial = (MWNumericArray)new int[] { 848, 439, 395, 248, 341 };//单帽型优化设计使用的材料
			MWArray[] dmresult = optimize_Design.NSGA_danmao(8, dmpop, dmgen, dmamin, dmamax, dmbmin, dmbmax, dmt1min, dmt1max, dmt2min, dmt2max, dmmaterial);
			//计算结果输出
			MWNumericArray dmjga = (MWNumericArray)dmresult[0];
			MWNumericArray dmjgb = (MWNumericArray)dmresult[1];
			MWNumericArray dmjgt1 = (MWNumericArray)dmresult[2];
			MWNumericArray dmjgt2 = (MWNumericArray)dmresult[3];
			MWNumericArray dmjgmat1 = (MWNumericArray)dmresult[4];
			MWNumericArray dmjgmat2 = (MWNumericArray)dmresult[5];
			MWNumericArray dmjgeng = (MWNumericArray)dmresult[6];
			MWNumericArray dmjgmass = (MWNumericArray)dmresult[7];
			GlobalDATA.dmdata_a = (double[,])dmjga.ToArray();
			GlobalDATA.dmdata_b = (double[,])dmjgb.ToArray();
			GlobalDATA.dmdata_t1 = (double[,])dmjgt1.ToArray();
			GlobalDATA.dmdata_t2 = (double[,])dmjgt2.ToArray();
			string strdmmat1 = dmjgmat1.ToString();
			strdmmat1 = Regex.Replace(strdmmat1, @" ", "");
			strdmmat1 = Regex.Replace(strdmmat1, @"\n", ",");
			GlobalDATA.dmdata_mat1 = strdmmat1.Split(',');
			string strdmmat2 = dmjgmat2.ToString();
			strdmmat2 = Regex.Replace(strdmmat2, @" ", "");
			strdmmat2 = Regex.Replace(strdmmat2, @"\n", ",");
			GlobalDATA.dmdata_mat2 = strdmmat2.Split(',');
			GlobalDATA.dmdata_eng = (double[,])dmjgeng.ToArray();
			GlobalDATA.dmdata_mass = (double[,])dmjgmass.ToArray();
			GlobalDATA.dmdata_yhhtn = dmjga.NumberOfElements;
			sw1.Stop();
			TimeSpan ts = sw1.Elapsed;
			GlobalDATA.dmdata_time = ts.TotalSeconds;
			Thread.Sleep(500);
			if (backworker1.CancellationPending)
			{
				e.Cancel = true;
			}
			else
			{
				backworker1.ReportProgress(100);//后台进度报告
			}
		}

		//计算完成后执行
		private void Backworker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				labelwait.Visible = false;
				label_time.Text = string.Format("求解用时: {0:F2}s", GlobalDATA.dmdata_time);
				label_time.Visible = true;
				#region 归还界面控制权
				btndm_cancel.Enabled = false;
				ComboBox_mode.Enabled = true;
				tsm_daochu.Enabled = true;
				btn_absorb.Enabled = true;
				btn_design.Enabled = true;
				btn_about.Enabled = true;
				textdm_min1.Enabled = true;
				textdm_max1.Enabled = true;
				textdm_min2.Enabled = true;
				textdm_max2.Enabled = true;
				textdm_min3.Enabled = true;
				textdm_max3.Enabled = true;
				textdm_min4.Enabled = true;
				textdm_max4.Enabled = true;
				textdm_pop.Enabled = true;
				textdm_gen.Enabled = true;
				textdmpre_l1.Enabled = true;
				textdmpre_f.Enabled = true;
				textdmpre_xita.Enabled = true;
				textdmpre_mat.Enabled = true;
				btndm_designclear.Enabled = true;
				btn_outfig.Enabled = true;
				btn_outlist.Enabled = true;
				btndm_solve.Enabled = true;
				#endregion
				//初始化曲线
				chart.Series.Clear();
				Series opti = new Series("Optimize" + string.Format("{0:G}", chart.Series.Count + 1))
				{
					BorderWidth = 2,
					ChartArea = "ChartArea",
					ChartType = SeriesChartType.Point,
					Font = new Font("Times New Roman", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
					LabelToolTip = "θ #VALX{F2}",
					IsVisibleInLegend = false,
					MarkerSize = 7,
					MarkerStyle = MarkerStyle.Circle,
					Name = "Optimize" + string.Format("{0:G}", chart.Series.Count + 1),
					ToolTip = "-EA：#VALX{F2}\nMass：#VAL{F2}",
				};
				chart.Series.Add(opti);
				//向曲线添加数据点
				for (int i = 0; i < GlobalDATA.dmdata_yhhtn; i++)
				{
					chart.Series[0].Points.AddXY(GlobalDATA.dmdata_eng[i, 0], GlobalDATA.dmdata_mass[i, 0]);
				}
				chart.Series[0].ToolTip = "-EA：#VALX{F2}\nMass：#VAL{F2}";
				chart.ChartAreas[0].RecalculateAxesScale();
				//添加结果至列表
				listresult.BeginUpdate();
				listresult.Items.Clear();
				listresult.Groups.Clear();
				ListViewGroup lvgp = new ListViewGroup
				{
					Name = listresult.Groups.Count.ToString(),
					Header = chart.Series[0].Name,
					HeaderAlignment = HorizontalAlignment.Center
				};
				listresult.Groups.Add(lvgp);
				for (int i = 0; i < GlobalDATA.dmdata_yhhtn; i++)
				{
					listresult.Items.Add(GlobalDATA.dmdata_a[i, 0].ToString("F2"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.dmdata_b[i, 0].ToString("F2"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.dmdata_t1[i, 0].ToString("F2"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.dmdata_t2[i, 0].ToString("F2"));
					GlobalDATA.dmdata_mat1[i] = DMzhuanhua(GlobalDATA.dmdata_mat1[i]);
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.dmdata_mat1[i]);
					GlobalDATA.dmdata_mat2[i] = DMzhuanhua(GlobalDATA.dmdata_mat2[i]);
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.dmdata_mat2[i]);
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.dmdata_eng[i, 0].ToString("F2"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.dmdata_mass[i, 0].ToString("F2"));
					listresult.Groups[0].Items.Add(listresult.Items[listresult.Items.Count - 1]);
				}
				listresult.EndUpdate();
			}
			else
			{
				btndm_cancel.Text = "取消求解";
				labelwait.Visible = false;
				label_time.Text = "已取消";
				label_time.Visible = true;
				#region 归还界面控制权
				btndm_cancel.Enabled = false;
				ComboBox_mode.Enabled = true;
				tsm_daochu.Enabled = true;
				btn_absorb.Enabled = true;
				btn_design.Enabled = true;
				btn_about.Enabled = true;
				textdm_min1.Enabled = true;
				textdm_max1.Enabled = true;
				textdm_min2.Enabled = true;
				textdm_max2.Enabled = true;
				textdm_min3.Enabled = true;
				textdm_max3.Enabled = true;
				textdm_min4.Enabled = true;
				textdm_max4.Enabled = true;
				textdm_pop.Enabled = true;
				textdm_gen.Enabled = true;
				textdmpre_l1.Enabled = true;
				textdmpre_f.Enabled = true;
				textdmpre_xita.Enabled = true;
				textdmpre_mat.Enabled = true;
				btndm_designclear.Enabled = true;
				btn_outfig.Enabled = true;
				btn_outlist.Enabled = true;
				btndm_solve.Enabled = true;
				#endregion
			}
		}

		//取消求解按钮
		private void Btndm_cancel_Click(object sender, EventArgs e)
		{
			if (backworker1.IsBusy)
			{
				backworker1.CancelAsync();
			}
			btndm_cancel.Text = "正在取消";
			btndm_cancel.Enabled = false;
		}

		//清除结果 按钮
		private void Btndm_designclear_Click(object sender, EventArgs e)
		{
			chart.Series.Clear();
			listresult.Groups.Clear();
			listresult.Items.Clear();
			label_time.Visible = false;
		}
		#endregion

		#region 多胞型吸能计算参数校验
		private void Textsmc_a_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_a.Text, out double result) || double.Parse(textsmc_a.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_a.Focus();
				textsmc_a.SelectAll();
			}
		}

		private void Textsmc_b_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_b.Text, out double result) || double.Parse(textsmc_b.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_b.Focus();
				textsmc_b.SelectAll();
			}
		}

		private void Textsmc_t_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_t.Text, out double result) || double.Parse(textsmc_t.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_t.Focus();
				textsmc_t.SelectAll();
			}
		}

		private void Textsmc_u_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_u.Text, out double result) || double.Parse(textsmc_u.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_u.Focus();
				textsmc_u.SelectAll();
			}
		}

		private void Textsmc_jsn_Leave(object sender, EventArgs e)
		{
			if (!int.TryParse(textsmc_jsn.Text, out int result) || int.Parse(textsmc_jsn.Text) < 2)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "请输入大于等于2的整数";
				alarm.ShowDialog();
				textsmc_jsn.Focus();
				textsmc_jsn.SelectAll();
			}
		}
		#endregion

		#region 多胞型吸能示意图切换，绘图参数校验
		private void Textsmc_xita_Enter(object sender, EventArgs e)
		{
			picboxsmc_eng.Image = Properties.Resources.弯曲转角示意图;
		}

		private void Textsmc_xita_Leave(object sender, EventArgs e)
		{
			picboxsmc_eng.Image = Properties.Resources.多胞型参数示意图;
			if (!double.TryParse(textsmc_xita.Text, out double result) || double.Parse(textsmc_xita.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_xita.Focus();
				textsmc_xita.SelectAll();
			}
		}

		private void Textsmc_xitamax_Enter(object sender, EventArgs e)
		{
			picboxsmc_eng.Image = Properties.Resources.弯曲转角示意图;
		}

		private void Textsmc_xitamax_Leave(object sender, EventArgs e)
		{
			picboxsmc_eng.Image = Properties.Resources.多胞型参数示意图;
			if (!double.TryParse(textsmc_xitamax.Text, out double result) || double.Parse(textsmc_xitamax.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_xitamax.Focus();
				textsmc_xitamax.SelectAll();
			}
		}

		private void Textsmc_htn_Leave(object sender, EventArgs e)
		{
			if (!int.TryParse(textsmc_htn.Text, out int result) || int.Parse(textsmc_htn.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "请输入大于0的整数";
				alarm.ShowDialog();
				textsmc_htn.Focus();
				textsmc_htn.SelectAll();
			}
		}
		#endregion

		#region 多胞型截面设计参数校验
		private void Textsmc_min1_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_min1.Text, out double result) || double.Parse(textsmc_min1.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_min1.Focus();
				textsmc_min1.SelectAll();
			}
		}

		private void Textsmc_max1_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_max1.Text, out double result) || double.Parse(textsmc_max1.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_max1.Focus();
				textsmc_max1.SelectAll();
			}
		}

		private void Textsmc_min2_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_min2.Text, out double result) || double.Parse(textsmc_min2.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_min2.Focus();
				textsmc_min2.SelectAll();
			}
		}

		private void Textsmc_max2_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_max2.Text, out double result) || double.Parse(textsmc_max2.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_max2.Focus();
				textsmc_max2.SelectAll();
			}
		}

		private void Textsmc_min3_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_min3.Text, out double result) || double.Parse(textsmc_min3.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_min3.Focus();
				textsmc_min3.SelectAll();
			}
		}

		private void Textsmc_max3_Leave(object sender, EventArgs e)
		{
			if (!double.TryParse(textsmc_max3.Text, out double result) || double.Parse(textsmc_max3.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.ShowDialog();
				textsmc_max3.Focus();
				textsmc_max3.SelectAll();
			}
		}

		private void Textsmc_yhn_Leave(object sender, EventArgs e)
		{
			if (!int.TryParse(textsmc_yhn.Text, out int result) || int.Parse(textsmc_yhn.Text) < 2)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "请输入大于等于2的整数";
				alarm.ShowDialog();
				textsmc_yhn.Focus();
				textsmc_yhn.SelectAll();
			}
		}

		private void Textsmc_pop_Leave(object sender, EventArgs e)
		{
			if (!int.TryParse(textsmc_pop.Text, out int result) || int.Parse(textsmc_pop.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "请输入大于0的整数";
				alarm.ShowDialog();
				textsmc_pop.Focus();
				textsmc_pop.SelectAll();
			}
		}

		private void Textsmc_gen_Leave(object sender, EventArgs e)
		{
			if (!int.TryParse(textsmc_gen.Text, out int result) || int.Parse(textsmc_gen.Text) <= 0)
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "请输入大于0的整数";
				alarm.ShowDialog();
				textsmc_gen.Focus();
				textsmc_gen.SelectAll();
			}
		}
		#endregion

		#region 多胞型吸能计算与绘图
		//吸能计算 按钮
		private void Btnsmc_engcalc_Click(object sender, EventArgs e)
		{
			MWArray smca = double.Parse(textsmc_a.Text);
			MWArray smcb = double.Parse(textsmc_b.Text);
			MWArray smct = double.Parse(textsmc_t.Text);
			MWArray smcjsn = double.Parse(textsmc_jsn.Text);
			MWArray smcu = double.Parse(textsmc_u.Text);
			MWArray smcp = double.Parse(textsmc_xita.Text);
			MWArray smcEz = optimize_Design.energy_smc(smca, smcb, smct, smcjsn, smcu, smcp);
			textsmc_eng.Text = smcEz.ToString();
		}

		//绘图 按钮
		private void Btnsmc_draw_Click(object sender, EventArgs e)
		{
			#region 调用MATLAB计算各点吸能
			MWArray smca = double.Parse(textsmc_a.Text);
			MWArray smcb = double.Parse(textsmc_b.Text);
			MWArray smct = double.Parse(textsmc_t.Text);
			MWArray smcjsn = double.Parse(textsmc_jsn.Text);
			MWArray smcu = double.Parse(textsmc_u.Text);
			double smcpm = double.Parse(textsmc_xitamax.Text);
			int smchtn = int.Parse(textsmc_htn.Text);//数据点个数
			double[] smcx = new double[smchtn];
			double[] smcy = new double[smchtn];
			double smcxita = 0;
			for (int i = 0; i < smchtn; i++)
			{
				smcx[i] = smcxita;
				MWArray smcp = smcxita;
				MWNumericArray Ez = (MWNumericArray)optimize_Design.energy_smc(smca, smcb, smct, smcjsn, smcu, smcp);
				smcy[i] = Ez.ToScalarDouble();
				smcxita += smcpm / smchtn;
			}
			#endregion

			#region 将曲线绘制到图上，显示结果
			//对比模式-关
			if (!checksmc_eng.Checked)
			{
				//初始化曲线
				chart.Series.Clear();
				Series line = new Series("Line" + string.Format("{0:G}", chart.Series.Count + 1))
				{
					BorderWidth = 2,
					ChartArea = "ChartArea",
					ChartType = SeriesChartType.Spline,
					Font = new Font("Times New Roman", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
					LabelToolTip = "θ #VALX{F2}",
					Legend = "Legend1",
					MarkerSize = 7,
					MarkerStyle = MarkerStyle.Circle,
					Name = "Line" + string.Format("{0:G}", chart.Series.Count + 1),
					ToolTip = "θ：#VALX{F2}\n-EA：#VAL{F2}",
				};
				chart.Series.Add(line);
				//向曲线添加数据点
				for (int i = 0; i < smchtn; i++)
				{
					chart.Series[0].Points.AddXY(smcx[i], smcy[i]);
				}
				chart.Series[0].ToolTip = "θ：#VALX{F2}\n-EA：#VAL{F2}";
				chart.ChartAreas[0].RecalculateAxesScale();
				//添加结果至列表
				listresult.BeginUpdate();
				listresult.Items.Clear();
				listresult.Groups.Clear();
				ListViewGroup lvgp = new ListViewGroup
				{
					Name = listresult.Groups.Count.ToString(),
					Header = chart.Series[0].Name,
					HeaderAlignment = HorizontalAlignment.Center
				};
				listresult.Groups.Add(lvgp);
				for (int i = 0; i < smchtn; i++)
				{
					listresult.Items.Add(smcx[i].ToString("F4"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(smcy[i].ToString("F1"));
					listresult.Groups[0].Items.Add(listresult.Items[listresult.Items.Count - 1]);
				}
				listresult.EndUpdate();
			}
			//对比模式-开
			else
			{
				//初始化曲线
				Series newline = new Series("Line" + string.Format("{0:G}", chart.Series.Count + 1))
				{
					BorderWidth = 2,
					ChartArea = "ChartArea",
					ChartType = SeriesChartType.Spline,
					Font = new Font("Times New Roman", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
					LabelToolTip = "θ #VALX{F2}",
					Legend = "Legend1",
					MarkerSize = 7,
					MarkerStyle = MarkerStyle.Circle,
					Name = "Line" + string.Format("{0:G}", chart.Series.Count + 1),
					ToolTip = "θ：#VALX{F2}\n-EA：#VAL{F2}",
				};
				chart.Series.Add(newline);
				//向曲线添加数据点
				for (int i = 0; i < smchtn; i++)
				{
					chart.Series[chart.Series.Count - 1].Points.AddXY(smcx[i], smcy[i]);
				}
				chart.Series[chart.Series.Count - 1].ToolTip = "θ：#VALX{F2}\n-EA：#VAL{F2}";
				chart.ChartAreas[0].RecalculateAxesScale();
				//添加结果至列表
				listresult.BeginUpdate();
				ListViewGroup newlvgp = new ListViewGroup
				{
					Name = listresult.Groups.Count.ToString(),
					Header = chart.Series[chart.Series.Count - 1].Name,
					HeaderAlignment = HorizontalAlignment.Center
				};
				listresult.Groups.Add(newlvgp);
				for (int i = 0; i < smchtn; i++)
				{
					listresult.Items.Add(smcx[i].ToString("F4"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(smcy[i].ToString("F1"));
					listresult.Groups[chart.Series.Count - 1].Items.Add(listresult.Items[listresult.Items.Count - 1]);
				}
				listresult.EndUpdate();
			}
			#endregion           
		}

		//清除结果 按钮
		private void Btnsmc_engclear_Click(object sender, EventArgs e)
		{
			chart.Series.Clear();
			listresult.Groups.Clear();
			listresult.Items.Clear();
			textdm_eng.Text = "";
		}
		#endregion

		#region 多胞型截面设计
		//优化求解 按钮
		private void Btnsmc_solve_Click(object sender, EventArgs e)
		{
			//验证设计参数范围正确性
			if (double.Parse(textsmc_min1.Text) > double.Parse(textsmc_max1.Text))
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "最小值不能超过最大值";
				alarm.ShowDialog();
				textsmc_min1.ForeColor = Color.Red;
				textsmc_max1.ForeColor = Color.Red;
			}
			else if (double.Parse(textsmc_min2.Text) > double.Parse(textsmc_max2.Text))
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "最小值不能超过最大值";
				alarm.ShowDialog();
				textsmc_min2.ForeColor = Color.Red;
				textsmc_max2.ForeColor = Color.Red;
			}
			else if (double.Parse(textsmc_min3.Text) > double.Parse(textsmc_max3.Text))
			{
				Formalarm alarm = new Formalarm();//初始化警告窗口
				alarm.label_alarm.Text = "最小值不能超过最大值";
				alarm.ShowDialog();
				textsmc_min3.ForeColor = Color.Red;
				textsmc_max3.ForeColor = Color.Red;
			}
			else
			{
				textsmc_min1.ForeColor = SystemColors.WindowText;
				textsmc_max1.ForeColor = SystemColors.WindowText;
				textsmc_min2.ForeColor = SystemColors.WindowText;
				textsmc_max2.ForeColor = SystemColors.WindowText;
				textsmc_min3.ForeColor = SystemColors.WindowText;
				textsmc_max3.ForeColor = SystemColors.WindowText;
				//后台进行遗传算法优化
				if (!backworker2.IsBusy)
				{
					backworker2.RunWorkerAsync();
				}
				label_time.Visible = false;
				labelwait.Visible = true;
				#region 取消界面操作控制权
				btnsmc_cancel.Enabled = true;
				btnsmc_solve.Enabled = false;
				btnsmc_designclear.Enabled = false;
				ComboBox_mode.Enabled = false;
				tsm_daochu.Enabled = false;
				btn_absorb.Enabled = false;
				btn_design.Enabled = false;
				btn_about.Enabled = false;
				textsmc_min1.Enabled = false;
				textsmc_max1.Enabled = false;
				textsmc_min2.Enabled = false;
				textsmc_max2.Enabled = false;
				textsmc_min3.Enabled = false;
				textsmc_max3.Enabled = false;
				textsmc_yhn.Enabled = false;
				textsmc_pop.Enabled = false;
				textsmc_gen.Enabled = false;
				textsmcpre_l1.Enabled = false;
				textsmcpre_xita.Enabled = false;
				textsmcpre_mat.Enabled = false;
				btn_outfig.Enabled = false;
				btn_outlist.Enabled = false;
				#endregion
			}
		}

		//后台遗传算法计算
		private void Backworker2_DoWork(object sender, DoWorkEventArgs e)
		{
			Stopwatch sw2 = new Stopwatch();
			sw2.Start();
			//遗传算法优化
			MWArray smcamin = double.Parse(textsmc_min1.Text);
			MWArray smcamax = double.Parse(textsmc_max1.Text);
			MWArray smcbmin = double.Parse(textsmc_min2.Text);
			MWArray smcbmax = double.Parse(textsmc_max2.Text);
			MWArray smctmin = double.Parse(textsmc_min3.Text);
			MWArray smctmax = double.Parse(textsmc_max3.Text);
			MWArray smcyhn = int.Parse(textsmc_yhn.Text);
			MWArray smcpop = int.Parse(textsmc_pop.Text);
			MWArray smcgen = int.Parse(textsmc_gen.Text);
			MWArray smcmaterial = (MWNumericArray)new int[] { 208, 248, 341 };//单帽型优化设计使用的材料
			MWArray[] smcresult = optimize_Design.NSGA_smc(6, smcpop, smcgen, smcamin, smcamax, smcbmin, smcbmax, smctmin, smctmax, smcmaterial, smcyhn);
			//计算结果输出
			MWNumericArray smcjga = (MWNumericArray)smcresult[0];
			MWNumericArray smcjgb = (MWNumericArray)smcresult[1];
			MWNumericArray smcjgt = (MWNumericArray)smcresult[2];
			MWNumericArray smcjgmat = (MWNumericArray)smcresult[3];
			MWNumericArray smcjgeng = (MWNumericArray)smcresult[4];
			MWNumericArray smcjgmass = (MWNumericArray)smcresult[5];
			GlobalDATA.smcdata_a = (double[,])smcjga.ToArray();
			GlobalDATA.smcdata_b = (double[,])smcjgb.ToArray();
			GlobalDATA.smcdata_t = (double[,])smcjgt.ToArray();
			string strsmcmat = smcjgmat.ToString();
			strsmcmat = Regex.Replace(strsmcmat, @" ", "");
			strsmcmat = Regex.Replace(strsmcmat, @"\n", ",");
			GlobalDATA.smcdata_mat = strsmcmat.Split(',');
			GlobalDATA.smcdata_eng = (double[,])smcjgeng.ToArray();
			GlobalDATA.smcdata_mass = (double[,])smcjgmass.ToArray();
			GlobalDATA.smcdata_yhhtn = smcjga.NumberOfElements;
			sw2.Stop();
			TimeSpan ts = sw2.Elapsed;
			GlobalDATA.smcdata_time = ts.TotalSeconds;
			Thread.Sleep(500);
			if (backworker2.CancellationPending)
			{
				e.Cancel = true;
			}
			else
			{
				backworker2.ReportProgress(100);//后台进度报告
			}
		}

		//计算完成后执行
		private void Backworker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!e.Cancelled)
			{
				labelwait.Visible = false;
				label_time.Text = string.Format("求解用时: {0:F2}s", GlobalDATA.smcdata_time);
				label_time.Visible = true;
				#region 归还界面控制权
				btnsmc_cancel.Enabled = false;
				ComboBox_mode.Enabled = true;
				tsm_daochu.Enabled = true;
				btn_absorb.Enabled = true;
				btn_design.Enabled = true;
				btn_about.Enabled = true;
				textsmc_min1.Enabled = true;
				textsmc_max1.Enabled = true;
				textsmc_min2.Enabled = true;
				textsmc_max2.Enabled = true;
				textsmc_min3.Enabled = true;
				textsmc_max3.Enabled = true;
				textsmc_yhn.Enabled = true;
				textsmc_pop.Enabled = true;
				textsmc_gen.Enabled = true;
				textsmcpre_l1.Enabled = true;
				textsmcpre_xita.Enabled = true;
				textsmcpre_mat.Enabled = true;
				btnsmc_designclear.Enabled = true;
				btn_outfig.Enabled = true;
				btn_outlist.Enabled = true;
				btnsmc_solve.Enabled = true;
				#endregion
				//初始化曲线
				chart.Series.Clear();
				Series opti = new Series("Optimize" + string.Format("{0:G}", chart.Series.Count + 1))
				{
					BorderWidth = 2,
					ChartArea = "ChartArea",
					ChartType = SeriesChartType.Point,
					Font = new Font("Times New Roman", 8F, FontStyle.Regular, GraphicsUnit.Point, 0),
					LabelToolTip = "θ #VALX{F2}",
					IsVisibleInLegend = false,
					MarkerSize = 7,
					MarkerStyle = MarkerStyle.Circle,
					Name = "Optimize" + string.Format("{0:G}", chart.Series.Count + 1),
					ToolTip = "-EA：#VALX{F2}\nMass：#VAL{F2}",
				};
				chart.Series.Add(opti);
				//向曲线添加数据点
				for (int i = 0; i < GlobalDATA.smcdata_yhhtn; i++)
				{
					chart.Series[0].Points.AddXY(GlobalDATA.smcdata_eng[i, 0], GlobalDATA.smcdata_mass[i, 0]);
				}
				chart.Series[0].ToolTip = "-EA：#VALX{F2}\nMass：#VAL{F2}";
				chart.ChartAreas[0].RecalculateAxesScale();
				//添加结果至列表
				listresult.BeginUpdate();
				listresult.Items.Clear();
				listresult.Groups.Clear();
				ListViewGroup lvgp = new ListViewGroup
				{
					Name = listresult.Groups.Count.ToString(),
					Header = chart.Series[0].Name,
					HeaderAlignment = HorizontalAlignment.Center
				};
				listresult.Groups.Add(lvgp);
				for (int i = 0; i < GlobalDATA.smcdata_yhhtn; i++)
				{
					listresult.Items.Add(textsmc_yhn.Text);
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.smcdata_a[i, 0].ToString("F2"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.smcdata_b[i, 0].ToString("F2"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.smcdata_t[i, 0].ToString("F2"));
					GlobalDATA.smcdata_mat[i] = SMCzhuanhua(GlobalDATA.smcdata_mat[i]);
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.smcdata_mat[i]);
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.smcdata_eng[i, 0].ToString("F2"));
					listresult.Items[listresult.Items.Count - 1].SubItems.Add(GlobalDATA.smcdata_mass[i, 0].ToString("F2"));
					listresult.Groups[0].Items.Add(listresult.Items[listresult.Items.Count - 1]);
				}
				listresult.EndUpdate();
			}
			else
			{
				btnsmc_cancel.Text = "取消求解";
				labelwait.Visible = false;
				label_time.Text = "已取消";
				label_time.Visible = true;
				#region 归还界面控制权
				btnsmc_cancel.Enabled = false;
				ComboBox_mode.Enabled = true;
				tsm_daochu.Enabled = true;
				btn_absorb.Enabled = true;
				btn_design.Enabled = true;
				btn_about.Enabled = true;
				textsmc_min1.Enabled = true;
				textsmc_max1.Enabled = true;
				textsmc_min2.Enabled = true;
				textsmc_max2.Enabled = true;
				textsmc_min3.Enabled = true;
				textsmc_max3.Enabled = true;
				textsmc_yhn.Enabled = true;
				textsmc_pop.Enabled = true;
				textsmc_gen.Enabled = true;
				textsmcpre_l1.Enabled = true;
				textsmcpre_xita.Enabled = true;
				textsmcpre_mat.Enabled = true;
				btnsmc_designclear.Enabled = true;
				btn_outfig.Enabled = true;
				btn_outlist.Enabled = true;
				btnsmc_solve.Enabled = true;
				#endregion
			}
		}

		//取消求解按钮
		private void Btnsmc_cancel_Click(object sender, EventArgs e)
		{
			if (backworker2.IsBusy)
			{
				backworker2.CancelAsync();
			}
			btnsmc_cancel.Text = "正在取消";
			btnsmc_cancel.Enabled = false;
		}

		//清除结果 按钮
		private void Btnsmc_designclear_Click(object sender, EventArgs e)
		{
			chart.Series.Clear();
			listresult.Groups.Clear();
			listresult.Items.Clear();
			label_time.Visible = false;
		}
		#endregion

		#region 导出结果
		//导出图表-->图片
		private void Btn_outfig_Click(object sender, EventArgs e)
		{
			sfd_chart.FileName = "Chart";
			ImageFormat format = ImageFormat.Png;
			if (sfd_chart.ShowDialog() == DialogResult.OK)
			{
				switch (sfd_chart.FilterIndex)
				{
					case 1:
						format = ImageFormat.Png;
						break;
					case 2:
						format = ImageFormat.Jpeg;
						break;
					case 3:
						format = ImageFormat.Bmp;
						break;
				}
				chart.SaveImage(sfd_chart.FileName, format);
				MessageBox.Show(this, "图片保存成功！", "信息提示");
			}
		}

		//导出列表-->excel
		private void Btn_outlist_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd_list = new SaveFileDialog
			{
				Title = "保存列表结果至...",
				DefaultExt = "xls",
				Filter = "Excel文件(*.xls)|*.xls"
			};
			if (sfd_list.ShowDialog() == DialogResult.OK)
			{
				label_time.Text = "请稍候...";
				label_time.Visible = true;
				DoExport(listresult, sfd_list.FileName);
			}
		}
		#endregion

		#region 数据点突出
		//选中列表项时，数据点在图表中突出
		//切换列表项选择时，切换数据点
		private void Listresult_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listresult.SelectedIndices != null && listresult.SelectedIndices.Count > 0)
			{
				for (int i = 0; i < listresult.SelectedIndices.Count; i++)
				{
					int actitem = listresult.SelectedIndices[i];
					int actline = int.Parse(listresult.Items[actitem].Group.Name);
					int j = 0;
					while (j < actline)
					{
						int passitem = listresult.Groups[j].Items.Count;
						actitem -= passitem;
						j++;
					}
					chart.Series[actline].Points[actitem].MarkerSize = 11;
					chart.Series[actline].Points[actitem].MarkerStyle = MarkerStyle.Square;
					chart.Series[actline].Points[actitem].MarkerBorderWidth = 2;
					chart.Series[actline].Points[actitem].MarkerBorderColor = Color.Red;
				}
			}
			else
			{
				foreach (var series in chart.Series)
				{
					foreach (var point in series.Points)
					{
						point.MarkerSize = 7;
						point.MarkerStyle = MarkerStyle.Circle;
						point.MarkerBorderColor = Color.Empty;
					}
				}
			}
		}
		#endregion

		#region 导出excel
		// 具体导出的方法
		// "listView" = ListView
		// "strFileName" = 导出到的文件名
		private void DoExport(ListView listView, string strFileName)
		{
			int rownum = listView.Items.Count;
			if (rownum == 0 || string.IsNullOrEmpty(strFileName))//列表为空或导出的文件名为空
			{
				MessageBox.Show("列表为空");
				label_time.Visible = false;
				return;
			}
			if (rownum > 0)
			{
				int colnum = listView.Items[0].SubItems.Count;
				int actrow = 1;//行号
				int actcol = 0;//列号
				//加载Excel
				Excel.Application exruanj = new Excel.Application();
				if (exruanj == null)//判断是否装了Excel
				{
					MessageBox.Show("无法创建excel对象，可能您的系统没有安装excel");
					label_time.Visible = false;
					return;
				}
				exruanj.DefaultFilePath = "";
				exruanj.DisplayAlerts = true;//是否需要显示提示
				exruanj.SheetsInNewWorkbook = 1;//返回或设置Microsoft Excel自动插入到新工作簿中的工作表数。
				Excel.Workbook exgongzb = exruanj.Workbooks.Add(true);//创建工作簿
				//将ListView的列名导入Excel表第一行
				foreach (ColumnHeader biaotou in listView.Columns)
				{
					actcol++;//行号自增
					exruanj.Cells[actrow, actcol] = biaotou.Text;
				}
				//将ListView中的数据导入Excel中
				for (int i = 0; i < rownum; i++)
				{
					actrow++;//列号自增
					actcol = 0;
					for (int j = 0; j < colnum; j++)
					{
						actcol++;
						//加“\t”避免导出的数据显示为科学计数法。可以放在每行的首尾。
						exruanj.Cells[actrow, actcol] = Convert.ToString(listView.Items[i].SubItems[j].Text) + "\t";
					}
				}
				//保存文件
				exgongzb.SaveAs(strFileName, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				//xlApp = null;
				//xlBook = null;
				exgongzb.Close(Type.Missing, Type.Missing, Type.Missing);
				exruanj.Quit();
				MessageBox.Show("导出文件成功！");
				label_time.Visible = false;
				GC.Collect();
			}
		}        
		 // 在已有路径文件追加保存
		private void Save(string pathFile)
		{
			int rowNum = listresult.Items.Count;
			if (rowNum == 0)//列表为空
			{
				MessageBox.Show("列表为空");
				label_time.Visible = false;
				return;
			}
			else
			{
				int columnNum = listresult.Items[0].SubItems.Count;
				int rowIndex = 1;//行号
				int columnIndex = 0;//列号
				//加载Excel
				Excel.Application xlApp = new Excel.Application();
				if (xlApp == null)//判断是否装了Excel
				{
					MessageBox.Show("无法创建excel对象，可能您的系统没有安装excel");
					label_time.Visible = false;
					return;
				}
				xlApp.DefaultFilePath = "";
				//Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(pathFile);//已有模版创建工作簿                               
				//Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Open(pathFile, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, false, Type.Missing, Type.Missing, false, true, Type.Missing, Type.Missing, true, Type.Missing);//创建工作簿
				//Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Open(pathFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
				Excel.Workbook xlBook = xlApp.Workbooks.Open(pathFile, Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, Type.Missing, false, true, Type.Missing, Type.Missing, true, Type.Missing);//创建工作簿
				//将ListView中的数据导入Excel中
				for (int i = 0; i < rowNum; i++)
				{
					rowIndex = Convert.ToInt32(listresult.Items[i].Text) + 2;//行号由表格行号给出,可以结合自己表格修改
					columnIndex = 0;//列号归零
					for (int j = 0; j < columnNum; j++)
					{
						columnIndex++;
						//加“\t”避免导出的数据显示为科学计数法。可以放在每行的首尾。
						xlApp.Cells[rowIndex, columnIndex] = Convert.ToString(listresult.Items[i].SubItems[j].Text) + "\t";
					}
				}
				//保存文件
				xlBook.Save();
				xlBook.Close(Type.Missing, Type.Missing, Type.Missing);
				xlApp.Quit();
				MessageBox.Show("导出文件成功！");
				label_time.Visible = false;
				GC.Collect();
			}
		}
		#endregion
	}
}