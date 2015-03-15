using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient; //Oracle程序集
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Media;
using System.Threading;

namespace IrrigationSystem
{
    public partial class Form1 : Form
    {
        //控制
        int flag = 0;
        int temp = 0;
        String type;
        String data;
        int index;  //列表行号
        int count1 = 0;   //计数

        double sum = 0;
        double x = 0;  //秒
        double y;

        PointPairList list = new PointPairList();  //坐标

        public Form1()
        {
            InitializeComponent();
        }

        //折线图显示
        private void btnLine_Click(object sender, EventArgs e)
        {
            zgc.Visible = true;
            dgv.Visible = false;
        }
        //列表显示
        private void btnList_Click(object sender, EventArgs e)
        {
            dgv.Visible = true;
            zgc.Visible = false;
        }

        //接收数据
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                data = serialPort1.ReadExisting();
                this.Invoke(new EventHandler(DisplayText));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "接收数据");
            }
        }

        private void DisplayText(object sender, EventArgs e)
        {
            //即时数据
            if (temp == 1)
            {
                //报文解析
                int node = Convert.ToInt32(data.Substring(0, 1));
                double wd = Convert.ToDouble(data.Substring(1, 4));
                double sd = Convert.ToDouble(data.Substring(5, 2));
                double ph = Convert.ToDouble(data.Substring(7, 3));
                if (cboxPoint.Text == "节点1")
                {
                    if (node == 1)
                    {
                        Save_Data(DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒"), data);
                        #region 数据显示
                        if (type == "温度")
                        {
                            y = wd;
                            x = DateTime.Now.Second;

                            sum = sum + y;

                            list.Add(count1, y);

                            label1.Text = "即时温度：" + y.ToString() + "℃";
                            label2.Text = "即时秒数：" + x.ToString() + "秒";

                            GraphPane myPane = zgc.GraphPane;

                            LineItem curve = myPane.AddCurve(null, list, Color.Red, SymbolType.Circle);
                            curve.Line.Width = 2F;
                            curve.Symbol.Fill = new Fill(Color.White);
                            curve.Symbol.Size = 5;

                            if (count1 == 3600)
                            {
                                count1 = 0;
                                list.RemoveRange(0, list.Count);
                            }

                            zgc.AxisChange();
                            zgc.Refresh();

                            index = dgv.Rows.Add();
                            dgv.Rows[index].Cells[0].Value = DateTime.Now.ToString(
                                            "yyyy年MM月dd日 HH时mm分ss秒");
                            dgv.Rows[index].Cells[1].Value = y;
                            dgv.Columns[0].FillWeight = 50;
                            dgv.Columns[1].FillWeight = 25;

                            count1++;
                        }
                        else if (type == "湿度")
                        {
                            y = sd;
                            x = DateTime.Now.Second;

                            sum = sum + y;

                            list.Add(count1, y);

                            label1.Text = "即时湿度：" + y.ToString() + "％";
                            label2.Text = "即时秒数：" + x.ToString() + "秒";

                            GraphPane myPane = zgc.GraphPane;

                            LineItem curve = myPane.AddCurve(null, list, Color.Red, SymbolType.Circle);
                            curve.Line.Width = 2F;
                            curve.Symbol.Fill = new Fill(Color.White);
                            curve.Symbol.Size = 5;

                            if (count1 == 3600)
                            {
                                count1 = 0;
                                list.RemoveRange(0, list.Count);
                            }

                            zgc.AxisChange();
                            zgc.Refresh();

                            index = dgv.Rows.Add();
                            dgv.Rows[index].Cells[0].Value = DateTime.Now.ToString(
                                            "yyyy年MM月dd日 HH时mm分ss秒");
                            dgv.Rows[index].Cells[1].Value = y;
                            dgv.Columns[0].FillWeight = 50;
                            dgv.Columns[1].FillWeight = 25;

                            count1++;
                        }
                        else if (type == "PH")
                        {
                            y = ph;
                            x = DateTime.Now.Second;

                            sum = sum + y;

                            list.Add(count1, y);

                            label1.Text = "即时PH值：" + y.ToString() + " ";
                            label2.Text = "即时秒数：" + x.ToString() + "秒";

                            GraphPane myPane = zgc.GraphPane;

                            LineItem curve = myPane.AddCurve(null, list, Color.Red, SymbolType.Circle);
                            curve.Line.Width = 2F;
                            curve.Symbol.Fill = new Fill(Color.White);
                            curve.Symbol.Size = 5;

                            if (count1 == 3600)
                            {
                                count1 = 0;
                                list.RemoveRange(0, list.Count);
                            }

                            zgc.AxisChange();
                            zgc.Refresh();

                            index = dgv.Rows.Add();
                            dgv.Rows[index].Cells[0].Value = DateTime.Now.ToString(
                                            "yyyy年MM月dd日 HH时mm分ss秒");
                            dgv.Rows[index].Cells[1].Value = y;
                            dgv.Columns[0].FillWeight = 50;
                            dgv.Columns[1].FillWeight = 25;

                            count1++;
                        }
                        #endregion 
                    }
                }
                if (cboxPoint.Text == "节点2")
                {
                    if (node == 2)
                    {
                        Save_Data(DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒"), data);
                        #region 数据显示
                        if (type == "温度")
                        {
                            y = wd;
                            x = DateTime.Now.Second;

                            sum = sum + y;

                            list.Add(count1, y);

                            label1.Text = "即时温度：" + y.ToString() + "℃";
                            label2.Text = "即时秒数：" + x.ToString() + "秒";

                            GraphPane myPane = zgc.GraphPane;

                            LineItem curve = myPane.AddCurve(null, list, Color.Red, SymbolType.Circle);
                            curve.Line.Width = 2F;
                            curve.Symbol.Fill = new Fill(Color.White);
                            curve.Symbol.Size = 5;

                            if (count1 == 3600)
                            {
                                count1 = 0;
                                list.RemoveRange(0, list.Count);
                            }

                            zgc.AxisChange();
                            zgc.Refresh();

                            index = dgv.Rows.Add();
                            dgv.Rows[index].Cells[0].Value = DateTime.Now.ToString(
                                            "yyyy年MM月dd日 HH时mm分ss秒");
                            dgv.Rows[index].Cells[1].Value = y;
                            dgv.Columns[0].FillWeight = 50;
                            dgv.Columns[1].FillWeight = 25;

                            count1++;
                        }
                        else if (type == "湿度")
                        {
                            y = sd;
                            x = DateTime.Now.Second;

                            sum = sum + y;

                            list.Add(count1, y);

                            label1.Text = "即时湿度：" + y.ToString() + "％";
                            label2.Text = "即时秒数：" + x.ToString() + "秒";

                            GraphPane myPane = zgc.GraphPane;

                            LineItem curve = myPane.AddCurve(null, list, Color.Red, SymbolType.Circle);
                            curve.Line.Width = 2F;
                            curve.Symbol.Fill = new Fill(Color.White);
                            curve.Symbol.Size = 5;

                            if (count1 == 3600)
                            {
                                count1 = 0;
                                list.RemoveRange(0, list.Count);
                            }

                            zgc.AxisChange();
                            zgc.Refresh();

                            index = dgv.Rows.Add();
                            dgv.Rows[index].Cells[0].Value = DateTime.Now.ToString(
                                            "yyyy年MM月dd日 HH时mm分ss秒");
                            dgv.Rows[index].Cells[1].Value = y;
                            dgv.Columns[0].FillWeight = 50;
                            dgv.Columns[1].FillWeight = 25;

                            count1++;
                        }
                        else if (type == "PH")
                        {
                            y = ph;
                            x = DateTime.Now.Second;

                            sum = sum + y;

                            list.Add(count1, y);

                            label1.Text = "即时PH值：" + y.ToString() + " ";
                            label2.Text = "即时秒数：" + x.ToString() + "秒";

                            GraphPane myPane = zgc.GraphPane;

                            LineItem curve = myPane.AddCurve(null, list, Color.Red, SymbolType.Circle);
                            curve.Line.Width = 2F;
                            curve.Symbol.Fill = new Fill(Color.White);
                            curve.Symbol.Size = 5;

                            if (count1 == 3600)
                            {
                                count1 = 0;
                                list.RemoveRange(0, list.Count);
                            }

                            zgc.AxisChange();
                            zgc.Refresh();

                            index = dgv.Rows.Add();
                            dgv.Rows[index].Cells[0].Value = DateTime.Now.ToString(
                                            "yyyy年MM月dd日 HH时mm分ss秒");
                            dgv.Rows[index].Cells[1].Value = y;
                            dgv.Columns[0].FillWeight = 50;
                            dgv.Columns[1].FillWeight = 25;

                            count1++;
                        }
                        #endregion 
                    }
                }
            }
        }
                   
        //连接数据库
        private void Save_Data(String time, String data)
        {
            try
            {
                if (cboxPoint.Text == "节点1")
                {
                    String ConStr = string.Format(//设置数据库连接字符串
    @"Provider=Microsoft.Jet.OLEDB.4.0;Data source='.\node1.mdb'");
                    OleDbConnection oleCon = new OleDbConnection(ConStr);//创建数据库连接对象
                    oleCon.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = oleCon;
                    cmd.CommandText = "insert into 历史数据 values('" + time + "','" + data + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    oleCon.Close();//关闭数据库连接
                    oleCon.Dispose();//释放连接资源
                }
                else if (cboxPoint.Text == "节点2")
                {
                    String ConStr = string.Format(//设置数据库连接字符串
    @"Provider=Microsoft.Jet.OLEDB.4.0;Data source='.\node2.mdb'");
                    OleDbConnection oleCon = new OleDbConnection(ConStr);//创建数据库连接对象
                    oleCon.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = oleCon;
                    cmd.CommandText = "insert into 历史数据 values('" + time + "','" + data + "')";
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    oleCon.Close();//关闭数据库连接
                    oleCon.Dispose();//释放连接资源
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //显示系统时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();//刷新窗体
            Graphics P_Graphics = //创建绘图对象
            CreateGraphics();
            P_Graphics.DrawString(
                DateTime.Now.ToString(
                "yyyy年MM月dd日 HH时mm分ss秒"),
                new Font("宋体", 10),
                Brushes.Red,
                new Point(10, 5));
            this.toolStripStatusLabel6.Text = "系统时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        //控制及时数据
        private void btnImmediate_Click(object sender, EventArgs e)
        {
            try
            {
                //打开串口、初始化
                serialPort1.PortName = cboxSerial.Text;
                serialPort1.BaudRate = Convert.ToInt32(cboxBaud.Text);
                serialPort1.Encoding = Encoding.ASCII;
                
                dgv.Columns.Add("Time", "时间");
                dgv.Columns.Add("Data", "数据");

                serialPort1.Open();
                temp = 1;
            }
            catch
            {
                MessageBox.Show("请选择串口！谢谢合作！","提示");
            }
        }

        //控制显示历史数据
        private void btnHistory_Click(object sender, EventArgs e)
        {
            LSData lsdata = new LSData();
            lsdata.Show();
        }

        //关闭串口
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        //滚动条
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.YAxis.Scale.Max = 30 - vScrollBar1.Value;
            myPane.YAxis.Scale.Min = 0 - vScrollBar1.Value;
            zgc.AxisChange();
            zgc.Refresh();
        }

        private void vScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.YAxis.Scale.Max = 30 + (91 - vScrollBar2.Value);
            myPane.YAxis.Scale.Min = 0 + (91 - vScrollBar2.Value);
            zgc.AxisChange();
            zgc.Refresh();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.XAxis.Scale.Max = 60 + hScrollBar1.Value * 40;
            myPane.XAxis.Scale.Min = 0 + hScrollBar1.Value * 40;
            zgc.AxisChange();
            zgc.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vScrollBar2.Value = 91;
            this.toolStripStatusLabel6.Text = "系统时间：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
        #region 温度
        private void btnTemperature_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.Rows.Clear();
                list.RemoveRange(0, list.Count);
                count1 = 0;
                type = "温度";

                GraphPane myPane = zgc.GraphPane;

                myPane.Title.Text = "温度实时曲线图";
                //myPane.XAxis.Title.Text = "时间（秒）";
                myPane.XAxis.Title.Text = "时间";
                myPane.YAxis.Title.Text = "温度（摄氏）";
                myPane.XAxis.Scale.Max = 60;
                myPane.XAxis.Scale.Min = 0;
                myPane.YAxis.Scale.Max = 30;
                myPane.YAxis.Scale.Min = 0;
                myPane.Chart.Fill = new Fill(Color.White, Color.Black, 23.0F);

                zgc.AxisChange();
                zgc.Refresh();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region 湿度
        private void btnHumidity_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.Rows.Clear();
                list.RemoveRange(0, list.Count);
                count1 = 0;
                type = "湿度";

                GraphPane myPane = zgc.GraphPane;

                myPane.Title.Text = "湿度实时曲线图";
                myPane.XAxis.Title.Text = "时间";
                myPane.YAxis.Title.Text = "湿度（%）";
                myPane.XAxis.Scale.Max = 60;
                myPane.XAxis.Scale.Min = 0;
                myPane.YAxis.Scale.Max = 100;
                myPane.YAxis.Scale.Min = 0;
                myPane.Chart.Fill = new Fill(Color.White, Color.Black, 23.0F);

                zgc.AxisChange();
                zgc.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region PH值
        private void btnPH_Click(object sender, EventArgs e)
        {
            try
            {
                dgv.Rows.Clear();
                list.RemoveRange(0, list.Count);
                count1 = 0;
                type = "PH";

                GraphPane myPane = zgc.GraphPane;

                myPane.Title.Text = "PH值实时曲线图";
                myPane.XAxis.Title.Text = "时间";
                myPane.YAxis.Title.Text = "PH值";
                myPane.XAxis.Scale.Max = 60;
                myPane.XAxis.Scale.Min = 0;
                myPane.YAxis.Scale.Max = 14;
                myPane.YAxis.Scale.Min = 0;
                myPane.Chart.Fill = new Fill(Color.White, Color.Black, 23.0F);

                zgc.AxisChange();
                zgc.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void btnBeep_Click(object sender, EventArgs e)
        {
            String Ss = txtLimit_Ss.Text.Trim();  //湿度上限
            String Sx = txtLimit_Sx.Text.Trim();  //湿度下限
            serialPort1.Write(Ss + Sx); //向下位机发送数据
        }
    }
}
