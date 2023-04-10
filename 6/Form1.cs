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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeNode rootNode=new TreeNode("我的电脑",0,0);
            rootNode.Tag = "我的电脑";
            rootNode.Text = "我的电脑";
            this.treeView1.Nodes.Add(rootNode);

            foreach(string drive in Environment.GetLogicalDrives())
            {
                var dir = new DriveInfo(drive);
                switch(dir.DriveType)
                {
                    case DriveType.Fixed:
                        {
                            TreeNode tnode = new TreeNode(dir.Name.Split(':')[0]);
                            tnode.Name = dir.Name;
                            tnode.Tag = tnode.Name;

                            tnode.ImageIndex = 3;
                            tnode.SelectedImageIndex = 3;

                            treeView1.Nodes.Add(tnode);
                            tnode.Nodes.Add("");
                        }
                        break;
                }
                rootNode.Expand();//展开树状视图
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.Expand(); 
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeViewItem.Add(e.Node);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.listView1.View = View.List;
            this.listView1.SmallImageList = this.imageList2;
            this.listView1.BeginUpdate();
            ListViewItem lvi = new ListViewItem();
            lvi.ImageIndex = 0;
            lvi.Text = e.Node.Name.ToString();
            this.listView1.Items.Add(lvi);
            this.listView1.EndUpdate();        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "请选择文件";
            openFileDialog.Filter = "所有文件(*.*)|*.*";
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                e.Node.Name=openFileDialog.FileName;
            }
            
        }
    }

    public static class TreeViewItem
    {
        public static void Add(TreeNode e)
        {
            try
            {
                if(e.Tag.ToString()!="我的电脑")
                {
                    e.Nodes.Clear();

                    TreeNode tNode = e;
                    string path = tNode.Name;

                    string[]dics=Directory.GetDirectories(path);
                    foreach(string dic in dics)
                    {
                        TreeNode subNode= new TreeNode(new DirectoryInfo(dic).Name);
                        subNode.Name=new DirectoryInfo(dic).FullName;
                        subNode.Tag=subNode.Name;
                        subNode.ImageIndex=1;
                        subNode.SelectedImageIndex = 2;
                        tNode.Nodes.Add(subNode);
                        subNode.Nodes.Add("");
                    }
                }
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);
            }
        }
    }
}
