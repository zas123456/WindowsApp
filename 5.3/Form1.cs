using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace homework5._3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string directory_path = "C:\\Users\\lenovo\\source\\repos\\homework5.3\\homework5.3\\files\\";
        public string DirectoryPath;
        char file = (char)1;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //打开文本文件
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DirectoryPath = directory_path + richTextBox2.Text.Trim() + ".txt";
                FileStream textfile = File.Open(DirectoryPath, FileMode.OpenOrCreate, FileAccess.Read);
                ArrayList arr=new ArrayList();

                byte[]bytes=new byte[textfile.Length];
                textfile.Read(bytes, 0, bytes.Length);
                string str=Encoding.UTF8.GetString(bytes);
                richTextBox1.Text += str;
                textfile.Close();
            }
            catch (Exception mm)
            {
                MessageBox.Show("文件读取失败" + Convert.ToString(mm), "消息");
            }
        }

        //保存编辑文本
        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryPath = directory_path + richTextBox2.Text.Trim() + ".txt";
            try
            {
                FileStream textfile=File.Open(DirectoryPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(textfile, Encoding.GetEncoding("GB2312"));
                string str = richTextBox1.Text;
                byte[]bytes=Encoding.UTF8.GetBytes(str);
                textfile.Write(bytes, (int)textfile.Length, richTextBox1.Text.Length);
                switch(file)
                {
                    case (char)1:
                        label1.Text = richTextBox2.Text;
                        checkedListBox1.Items.Add(richTextBox2.Text);break;
                    case (char)2:
                        label2.Text = richTextBox2.Text;
                        checkedListBox1.Items.Add(richTextBox2.Text); break;
                    case (char)3:
                        label3.Text = richTextBox2.Text;
                        checkedListBox1.Items.Add(richTextBox2.Text); break;
                    case (char)4:
                        label4.Text = richTextBox2.Text;
                        checkedListBox1.Items.Add(richTextBox2.Text); break;
                    case (char)5:
                        label5.Text = richTextBox2.Text;
                        checkedListBox1.Items.Add(richTextBox2.Text); break;
                    case (char)6:
                        label6.Text = richTextBox2.Text;
                        checkedListBox1.Items.Add(richTextBox2.Text); break;
                }
                file++;
                MessageBox.Show("文件写成功", "警报");
                textfile.Close();
            }
            catch(Exception mm)
            {
                MessageBox.Show("文件未能建立" + Convert.ToString(mm), "消息");
            }
        }

        //创建文本文件
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(richTextBox2.Text.Length==0)
                {
                    MessageBox.Show("文件名禁止为空", "警报");
                }
                else
                {
                    DirectoryPath = directory_path + richTextBox2.Text.Trim() + ".txt";
                    StreamWriter sw = File.CreateText(DirectoryPath);
                    MessageBox.Show("文件成功建立", "消息");
                    sw.Close();
                }
            }
            catch(Exception mm)
            {
                MessageBox.Show("文件未能建立" + Convert.ToString(mm), "消息");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
