﻿using System;
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
            PC();
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
                v.SubItems.Add($"{p.WorkingSet64 / 1000000 }");
                listView1.Items.Add(v);
            }
        }

        public void ImWillBreakYou()

        {
            label2.Text = (allMamoryGB / 100000).ToString();
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("shutdown"," /s /t ");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("shutdown", " /r /t ");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void PC()
        {


            ObjectQuery winQuery = new ObjectQuery("SELECT * FROM Win32_LogicalMemoryConfiguration");

            ManagementObjectSearcher searcher11 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

            foreach (ManagementObject item in searcher11.Get())
            {
                label5.Text = item["AdapterRAM"].ToString();
                label6.Text = item["Caption"].ToString();
                label7.Text = item["Description"].ToString();
                label8.Text = item["VideoProcessor"].ToString();
            }
        }
    }
}

