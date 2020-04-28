using Antrian.cpu.Algo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;



namespace Antrian.cpu
{
    public partial class Form1 : Form
    {
        private List<Process> bfp = new List<Process>();

        FIFO fifo;

        RR rr;

        SJF sjf;

        private int clockTime = 0;
        public Form1()
        {
            InitializeComponent();
            richTextBox2.Text += "ID        Priority        CPU Burst Time";
            richTextBox3.Text += "ID        Priority        CPU Burst Time";
            richTextBox4.Text += "ID        Priority        CPU Burst Time";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("test output debug bos");
            using (var fileStream = File.OpenRead(textBox1.Text.ToString()))
            using (StreamReader sr = new StreamReader(fileStream))
            {
                String line;

                while ((line = sr.ReadLine()) != null)
                {


                    List<String> items = new List<string>(line.Split(' '));
                    Process process = new Process(
                        Int32.Parse(items[0]),
                        Int32.Parse(items[1]),
                        Int32.Parse(items[2]),
                        Int32.Parse(items[3]),
                        Int32.Parse(items[4]),
                        Int32.Parse(items[5]),
                        Int32.Parse(items[3])
                     );
                    bfp.Add(process);
                }

                fifo = new FIFO(bfp);
                rr = new RR(bfp);
                sjf = new SJF(bfp);
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = clockTime.ToString();
            foreach (var p in fifo.processes)
            {
                if (p.getTest() > 0)
                {
                    qA.Text += p.getId().ToString() + "\t   " + p.getPrioritas().ToString() + "\t\t" + p.getBurstTime() + Environment.NewLine;
                    qA.Text += "--------------------------------------------------";
                    qA.AppendText(Environment.NewLine);
                }              
                else
                {
                    qA.Text += "PROSES ID : " + p.getId().ToString() + " selesai";
                }
            }

            foreach (var p in rr.processes)
            {
                if (p.getTest() > 0)
                {
                    qB.Text += p.getId().ToString() + "\t   " + p.getPrioritas().ToString() + "\t\t" + (p.getBurstTime()+1) + Environment.NewLine;
                    qB.Text += "--------------------------------------------------";
                    qB.AppendText(Environment.NewLine);
                }
                else
                {
                    qB.Text += "PROSES ID : " + p.getId().ToString() + " selesai , Clock Saat ini = " + clockTime + Environment.NewLine;
                }
            }

            foreach (var p in sjf.processes)
            {
                if (p.getTest() > 0)
                {
                    qC.Text += p.getId().ToString() + "\t   " + p.getPrioritas().ToString() + "\t\t" + (p.getBurstTime() + 1) + Environment.NewLine;
                    qC.Text += "--------------------------------------------------";
                    qC.AppendText(Environment.NewLine);
                }
                else
                {
                    qC.Text += p.getId().ToString() + "\t   " + " " + "\t\t" + "Selesai" + Environment.NewLine;
                    qC.Text += "--------------------------------------------------";
                    qC.AppendText(Environment.NewLine);
                }
            }

            Process ffDemo=fifo.tick();
            Process rrDemo = rr.tick();
            List<Process> sjfDemo = sjf.tick();
            if (sjfDemo.Count > 0)
            {
                foreach (var p in sjfDemo)
                {
                    p.setWaitingClock(0);
                }
                while (fifo.processes.Count < 10 && sjfDemo.Count > 0)
                {
                    rTBlog.Text += "Promosi dari Qc ke Qa dengan ID : " + sjfDemo[0].getId() + " pada clock ke = " + clockTime;
                    fifo.processes.Add(sjfDemo[0]);
                    sjfDemo.RemoveAt(0);
                }
                rTBlog.AppendText(Environment.NewLine);
            }

            if (rrDemo.getId() != 0)
            {
                qB.ResetText();
                rTBlog.Text += "Demosi dari Qb ke Qc dengan ID : " + rrDemo.getId() + " pada clock ke = " + clockTime;
                rrDemo.setWaitingClock(0);
                rrDemo.setBurstTime(rrDemo.getBurstTime() + 1);
                sjf.processes.Add(rrDemo);
                rTBlog.AppendText(Environment.NewLine);
            }

            if (sjfDemo.Count > 0)
            {
                foreach (var p in sjfDemo)
                {
                    rTBlog.Text += "Demosi dari Qa ke Qb dengan ID : " + p.getId() + " pada clock ke = " + clockTime;
                    rr.processes.AddRange(sjfDemo);
                }
                rTBlog.AppendText(Environment.NewLine);
            }


            if (ffDemo.getId() != 0)
            {
                qA.ResetText();
                rTBlog.Text += "Demosi dari Qa ke Qc dengan ID : " + ffDemo.getId() + " pada clock ke = " + clockTime;
                sjf.processes.Add(ffDemo);
                rTBlog.AppendText(Environment.NewLine);
            }
            clockTime++;
        }
    }
}
