using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
namespace TaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ListAllRunningProcesses();
            ImWillBreakYou(); 

        }

        Process[] processList;
        List<long> allMemoryBaby = new List<long>();
        double allMamoryGB = 0;
         public void ListAllRunningProcesses()
         {
            listView1.Items.Clear();
            processList = Process.GetProcesses();

         
           
            foreach (var p in processList)
            {
               ListViewItem v = new ListViewItem(p.ProcessName);
               v.SubItems.Add(p.Id.ToString());
                long allMemory = p.VirtualMemorySize64 / 1000000;
                allMemoryBaby.Add(allMemory);
                allMamoryGB += allMemory;
                v.SubItems.Add(allMemory.ToString());
               listView1.Items.Add(v);
            }
         }

         public void ImWillBreakYou() 

         {
             label2.Text = (allMamoryGB / 10000).ToString();
             label4.Text = allMemoryBaby.Capacity.ToString();
         }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int index = -1;
                if (listView1.SelectedItems.Count > 0)
                {
                    index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                }

                processList[index].Kill();
                ListAllRunningProcesses();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                Process p = new Process();
                p.StartInfo.FileName =textBox1.Text;
                p.Start();
                ListAllRunningProcesses();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

