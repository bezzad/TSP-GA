using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TSP
{
    public partial class ProcessorAffinityForm : Form
    {
        public ProcessorAffinityForm()
        {
            InitializeComponent();
            chlstProcessors.ItemCheck += new ItemCheckEventHandler(chlstProcessors_ItemCheck);
        }

        private void ProcessorAffinityForm_Load(object sender, EventArgs e)
        {
            lblCurrentProcessName.Text = ("Which processors are allowed to run" + Environment.NewLine +
            @"(" + System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe) ?");

            int pc = Environment.ProcessorCount;
            long pa = System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity.ToInt64();
            
            chlstProcessors.Items.Add("<All Processors>");
            for (int PID = 0; PID < pc; ++PID)
            {
                chlstProcessors.Items.Add(("CPU " + PID.ToString()));
            }

            if (pa == (Math.Pow(2, pc) - 1))
            {
                chlstProcessors.SetItemChecked(0, true);
            }
            else
            {
                string BinaryValue = Convert.ToString(pa, 2);
                char[] chBinaryValue = BinaryValue.ToCharArray().Reverse().ToArray<char>();
                for (int i = 0; i < chBinaryValue.Length; i++)
                    chlstProcessors.SetItemChecked(i + 1, (chBinaryValue[i] == '1') ? true : false);
            }

            txtInfo.Text = "";
            //txtInfo.Text = (Environment.Is64BitOperatingSystem) ?
            //  "64Bit Operation System (x64)" : "32Bit Operation System (x86)";
            //txtInfo.AppendText(Environment.NewLine);
            //txtInfo.AppendText((Environment.Is64BitProcess) ? "64Bit Process" : "32Bit Process");
            //txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText("Machine Name:  " + Environment.MachineName);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText("Operation System Version:  " + Environment.OSVersion);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText("Number of Processor:  " + Environment.ProcessorCount);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText("User Domain Name:  " + Environment.UserDomainName);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText("User Name:  " + Environment.UserName);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText("Current Thread Name:  " + System.Threading.Thread.CurrentThread.Name);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText("Current Process Name:  " + System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText(Environment.NewLine);
            txtInfo.AppendText("Current Process Affinity:  " + System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // if data is uncorrected then Exit this Event
            if (chlstProcessors.CheckedItems.Count <= 0)
                return;

            if (chlstProcessors.GetItemChecked(0)) // All Processors
            {
                // (2^n)-1 is Affinity number of all Processors by 'n' core's
                int Affinity = (int)(Math.Pow(2, Environment.ProcessorCount)) - 1;
                System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(Affinity);
            }
            else // a lot of CPU Core's
            {
                //
                // Bitmask     | Binary value      | Eligible processors   
                // ------------|-------------------|--------------------------
                // 0x0001      | 00000000 00000001 | 1
                // ------------|-------------------|--------------------------
                // 0x0003      | 00000000 00000011 | 1 and 2
                // ------------|-------------------|--------------------------
                // 0x0007      | 00000000 00000111 | 1, 2 and 3
                // ------------|-------------------|--------------------------
                // 0x0009      | 00000000 00001001 | 1 and 4
                // ------------|-------------------|--------------------------
                // 0x007F      | 00000000 01111111 | 1, 2, 3, 4, 5, 6 and 7
                // ------------|-------------------|--------------------------
                // http://msdn.microsoft.com/en-us/library/system.diagnostics.process.processoraffinity.aspx 
                //
                string BinaryValue = "";
                for (int i = 1; i < chlstProcessors.Items.Count; ++i)
                    BinaryValue = BinaryValue.Insert(0, ((chlstProcessors.GetItemChecked(i)) ? "1" : "0"));
                
                long Affinity = Convert.ToInt64(BinaryValue, 2); // Convert Binary to Decimal
                System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(Affinity);
            }
            Dispose();
        }

        private void chlstProcessors_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            chlstProcessors.ItemCheck -= new ItemCheckEventHandler(chlstProcessors_ItemCheck);
            if (e.Index == 0)
            {
                // check other Items
                for (int i = 1; i < chlstProcessors.Items.Count; i++)
                    chlstProcessors.SetItemChecked(i, (e.CurrentValue == CheckState.Checked) ? false : true);
                btnOk.Enabled = (e.CurrentValue == CheckState.Checked) ? false : true;
            }
            else
            {
                if ((e.CurrentValue == CheckState.Checked)) // after this time e.checked will be false
                {
                    chlstProcessors.SetItemChecked(0, false);
                    bool other_checked = false;
                    for (int i = 1; i < chlstProcessors.Items.Count; ++i)
                    {
                        if (i != e.Index && chlstProcessors.GetItemChecked(i))
                        {
                            other_checked = true;
                            break;
                        }
                    }
                    if (!other_checked) btnOk.Enabled = false;
                    else btnOk.Enabled = true;
                }
                else // after this time e.checked will be true
                {
                    btnOk.Enabled = true;
                    bool other_checked = true;
                    for (int i = 1; i < chlstProcessors.Items.Count; ++i)
                    {
                        if (i != e.Index && !chlstProcessors.GetItemChecked(i))
                        {
                            other_checked = false;
                            break;
                        }
                    }
                    if (other_checked)
                        chlstProcessors.SetItemChecked(0, true);
                }
            }
            chlstProcessors.ItemCheck += new ItemCheckEventHandler(chlstProcessors_ItemCheck);
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            txtInfo.Visible = true;
        }

        private void txtInfo_Click(object sender, EventArgs e)
        {
            txtInfo.Visible = false;
        }   
   
    }
}
