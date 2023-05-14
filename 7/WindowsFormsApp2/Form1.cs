using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public delegate void ProcessDelegate();

        public static Queue<string> q = new Queue<string>();

        public static int j = 0;
        public static object locker=new object();
        public static object locker1=new object();
        public static object locker2 = new object();

        public static Stopwatch watch=new Stopwatch();
        public static string textbox=string.Empty;
        public int Num = 5;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string urlToCrawl = ("http://cn.bing.com/search?q=" + textBox1.Text);

            //HTTP请求
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlToCrawl);
            //GET方法
            req.Method = "GET";
            //获得HTTP回复
            HttpWebResponse resp=(HttpWebResponse)req.GetResponse();
            //定义编码方式
            string htmlCharset = "utf-8";
            //编码方式
            Encoding htmlEncoding =Encoding.GetEncoding(htmlCharset);
            StreamReader sr = new StreamReader(resp.GetResponseStream(), htmlEncoding);
            //显示html内容
            string resphtml = sr.ReadToEnd();
            textBoxhtml.Text= resphtml;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //找到网站建设类目下面的连接
            string h1userp = @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";

            //捕获匹配
            MatchCollection foundH1user = (new Regex(h1userp)).Matches(textBoxhtml.Text);
            foreach(Match m in foundH1user)
            {
                string url = "http://cn.bing.com/search?q=" + (string)m.Value;

                q.Enqueue(url);
            }

            Thread[] downloadThread;//声明下载线程
            downloadThread = new Thread[21];//为线程申请资源，确定线程总数
            watch.Start();
            for(int i=0;i<Num;i++)
            {
                ThreadStart startDownload = new ThreadStart(DownLoad);
                downloadThread[i]=new Thread(startDownload);//指定线程起始设置
                downloadThread[i].Start();//逐个开启线程
            }
            //while (q.Count!=0);
            watch.Stop();
        }

        public string wenben(string rrh)
        {
            string rh = rrh;
            //抓取标题
            string bt = @"<h2>(.*)</h2>";
            Match nnn = (new Regex(bt)).Match(rh);

            Regex hregex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            string hstrOutput = hregex.Replace(nnn.Value, "");//替换掉"<"和">"之间的内容
            hstrOutput = hstrOutput.Replace("<", "");
            hstrOutput = hstrOutput.Replace(">", "");
            hstrOutput = hstrOutput.Replace(" ", "");

            return hstrOutput;
        }
        public void DownLoad()
        {
            while(true)
            {
                string url;
                string h2;
                if(q.Count!=0)
                {
                    lock(locker1)
                    {
                        url= q.Dequeue();
                        j++;
                    }
                    try
                    {
                        HttpWebRequest rr=(HttpWebRequest)WebRequest.Create(url);
                        rr.Method = "GET"; 
                        HttpWebResponse resp=(HttpWebResponse)rr.GetResponse();

                        string htmlCharset = "utf-8";
                        Encoding htmlEncoding=Encoding.GetEncoding(htmlCharset);
                        StreamReader sr = new StreamReader(resp.GetResponseStream(), htmlEncoding);
                        string rh = sr.ReadToEnd();
                        h2 = wenben(rh);
                        lock(locker2)
                        {
                            ProcessDelegate showProcess = delegate ()
                              {
                                  textBoxurl.AppendText(url + h2 + DateTime.Now.ToString() + "\n");
                              };
                            textBoxurl.Invoke(showProcess);

                        }
                    }
                    catch(Exception eee)
                    {

                    }
                }
                else
                {
                    break;
                }
            }
        }


    }
}
