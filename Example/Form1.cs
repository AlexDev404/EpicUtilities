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
using fnbr;

namespace Fnbr_Testing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void OnLogged(object sender, Client client)
        {

            File.WriteAllBytes("DeviceAuth.dat", client.GetDeviceAuth());
        }

        void OnError(object o, Exception Error)
        {
            MessageBox.Show(Error.Message);
            throw Error;
        }

        private void SidLogin_Click(object sender, EventArgs e)
        {
            var client = new Client();
            client.SidLogin(SidTXTbox.Text);
            MessageBox.Show(client.AuthSession.displayName);
        }

        private void AuthCode_Click(object sender, EventArgs e)
        {
            var client = new Client();
            client.CodeLogin(AuthCode_textBox.Text); //Using Windows by default
            MessageBox.Show(client.AuthSession.displayName);
        }

        private void DeviceCode_Login(object sender, EventArgs e)
        {
            var client = new Client();
            client.DeviceCodeLogin(OnLogged, OnError);
        }

        private void Exchange_Click(object sender, EventArgs e) //Exchange
        {
            var client = new Client();
            client.ExchangeLogin(ExchangeTextBox.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!File.Exists("DeviceAuth.dat"))
            {
                MessageBox.Show("No DeviceAuth.dat file was found.");
                return;
            }

            var client = new Client();
            client.DeviceAuthLogin(File.ReadAllBytes("DeviceAuth.dat"));
            MessageBox.Show(client.GenerateExchange());
            client.Init();

        }
    }
}
