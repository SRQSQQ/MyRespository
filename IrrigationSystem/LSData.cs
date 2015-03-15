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

namespace IrrigationSystem
{
    public partial class LSData : Form
    {
        string type;
        String item1, item2, item3;
        PointPairList list = new PointPairList();  //坐标

        public LSData()
        {
            InitializeComponent();
        }

        //范围查询
        private void button2_Click(object sender, EventArgs e)
        {
            string time = item1 +"年"+ item2 +"月"+ item3 + "日";
            try
            {
                if (cboxPoint.Text == "节点1")
                {
                    String ConStr = string.Format(//设置数据库连接字符串
@"Provider=Microsoft.Jet.OLEDB.4.0;Data source='.\node1.mdb'");
                    OleDbConnection oleCon = new OleDbConnection(ConStr);//创建数据库连接对象
                    OleDbDataAdapter oleDap = new OleDbDataAdapter(//创建数据适配器对象
                        "select * from 历史数据 where  时间 LIKE '%" + time + "%'", oleCon);
                    DataSet ds = new DataSet();//创建数据集
                    oleDap.Fill(ds, type);//填充数据集
                    oleCon.Close();//关闭数据库连接
                    oleCon.Dispose();//释放连接资源

                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        string data = ds.Tables[0].Rows[x][1].ToString();
                        double wd = Convert.ToDouble(data.Substring(1, 4));
                        double sd = Convert.ToDouble(data.Substring(5, 2));
                        double ph = Convert.ToDouble(data.Substring(7, 3));
                        if (type == "温度")
                        {
                            list.Add(x, wd);
                        }
                        else if (type == "湿度")
                        {
                            list.Add(x, sd);
                        }
                        else if (type == "PH")
                        {
                            list.Add(x, ph);
                        }  
                    }
                    GraphPane myPane = zgc.GraphPane;

                    LineItem curve = myPane.AddCurve(null, list, Color.Red, SymbolType.Circle);
                    curve.Line.Width = 2F;
                    curve.Symbol.Fill = new Fill(Color.White);
                    curve.Symbol.Size = 5;

                    zgc.AxisChange();
                    zgc.Refresh();   
                }

                else if (cboxPoint.Text == "节点2")
                {
                    String ConStr = string.Format(//设置数据库连接字符串
@"Provider=Microsoft.Jet.OLEDB.4.0;Data source='.\node2.mdb'");
                    OleDbConnection oleCon = new OleDbConnection(ConStr);//创建数据库连接对象
                    OleDbDataAdapter oleDap = new OleDbDataAdapter(//创建数据适配器对象
                        "select * from 历史数据 where  时间 LIKE '%" + time + "%'", oleCon);
                    DataSet ds = new DataSet();//创建数据集
                    oleDap.Fill(ds, type);//填充数据集
                    oleCon.Close();//关闭数据库连接
                    oleCon.Dispose();//释放连接资源

                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        string data = ds.Tables[0].Rows[x][1].ToString();
                        double wd = Convert.ToDouble(data.Substring(1, 4));
                        double sd = Convert.ToDouble(data.Substring(5, 2));
                        double ph = Convert.ToDouble(data.Substring(7, 3));
                        if (type == "温度")
                        {
                            list.Add(x, wd);
                        }
                        else if (type == "湿度")
                        {
                            list.Add(x, sd);
                        }
                        else if (type == "PH")
                        {
                            list.Add(x, ph);
                        } 
                    }

                    GraphPane myPane = zgc.GraphPane;

                    LineItem curve = myPane.AddCurve(null, list, Color.Red, SymbolType.Circle);
                    curve.Line.Width = 2F;
                    curve.Symbol.Fill = new Fill(Color.White);
                    curve.Symbol.Size = 5;

                    zgc.AxisChange();
                    zgc.Refresh();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请选择查询节点，数据类型，时间","提示");
            }
        }
        #region 温度
        private void btnTemperature_Click_1(object sender, EventArgs e)
        {
            list.Clear();

            type = "温度";

            GraphPane myPane = zgc.GraphPane;

            myPane.Title.Text = "温度实时曲线图";
            myPane.XAxis.Title.Text = "时间";
            myPane.YAxis.Title.Text = "温度（摄氏）";
            myPane.XAxis.Scale.Max = 24;
            myPane.XAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 30;
            myPane.YAxis.Scale.Min = 0;
            myPane.Chart.Fill = new Fill(Color.White, Color.Black, 23.0F);

            zgc.AxisChange();
            zgc.Refresh();
        }
        #endregion
        #region 湿度
        private void btnHumidity_Click(object sender, EventArgs e)
        {
            list.Clear();

            type = "湿度";

            GraphPane myPane = zgc.GraphPane;

            myPane.Title.Text = "湿度实时曲线图";
            myPane.XAxis.Title.Text = "时间";
            myPane.YAxis.Title.Text = "湿度（%）";
            myPane.XAxis.Scale.Max = 24;
            myPane.XAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 100;
            myPane.YAxis.Scale.Min = 0;
            myPane.Chart.Fill = new Fill(Color.White, Color.Black, 23.0F);

            zgc.AxisChange();
            zgc.Refresh();
        }
        #endregion
        #region PH值
        private void btnPH_Click(object sender, EventArgs e)
        {
            list.Clear();

            type = "PH";

            GraphPane myPane = zgc.GraphPane;

            myPane.Title.Text = "PH值实时曲线图";
            myPane.XAxis.Title.Text = "时间";
            myPane.YAxis.Title.Text = "PH值";
            myPane.XAxis.Scale.Max = 24;
            myPane.XAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 14;
            myPane.YAxis.Scale.Min = 0;
            myPane.Chart.Fill = new Fill(Color.White, Color.Black, 23.0F);

            zgc.AxisChange();
            zgc.Refresh();
        }
        #endregion
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            item1 = comboBox1.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            item2 = comboBox2.Text;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            item3 = comboBox3.Text;
        }

        private void vScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.YAxis.Scale.Max = 30 + (91 - vScrollBar2.Value);
            myPane.YAxis.Scale.Min = 0 + (91 - vScrollBar2.Value);
            zgc.AxisChange();
            zgc.Refresh();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.YAxis.Scale.Max = 30 - vScrollBar1.Value;
            myPane.YAxis.Scale.Min = 0 - vScrollBar1.Value;
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

        private void LSData_Load(object sender, EventArgs e)
        {
            vScrollBar2.Value = 91;
        }
    }
}
