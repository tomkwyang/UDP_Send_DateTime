using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDP_Send_DateTime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtMsg.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string targetIP = txtIP.Text;
            int targetPort;
            string msg = "";
            IPAddress ip;

            if (!int.TryParse(txtPort.Text, out targetPort))
            {
                msg = "Port Number error.";
            }
            else
            {
                if (IPAddress.TryParse(targetIP, out ip))
                {
                    if (ip.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        msg = "Invalid IP address.";
                    }
                    else
                    {
                        // 取得目前時間字串
                        int weekday = (int)DateTime.Now.DayOfWeek;
                        if (weekday == 0) weekday = 7;
                        string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + weekday;
                        msg = "Send " + now;

                        using (UdpClient udpClient = new UdpClient())
                        {
                            byte[] data = Encoding.ASCII.GetBytes(now);
                            udpClient.Send(data, data.Length, targetIP, targetPort);
                        }
                    }
                }
                else
                {
                    msg = "Invalid IP address.";
                }


                    
            }
            txtMsg.Text = msg;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
