using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Execute_Selected_Tests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                // first we want to setup the mail client
                var hostName = Dns.GetHostName();
                // Get the IP
                var myIP = Dns.GetHostAddresses(hostName)[1].ToString();
                var condition = myIP.StartsWith("10.3");
                var address = condition ? "10.3.50.2" : "10.1.1.2";
                const int port = 587;
                const string user = "mail_comtestmanager";
                const string pass = "knPIZ2!Ut%VqTQMZBhGuieO8";

                var client = new SmtpClient
                {
                    Host = address,
                    Port = port,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(user, pass),
                    EnableSsl = true
                    
                };

                var cert = new X509Certificate2();
                cert.Import(File.ReadAllBytes(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    condition ? "letsencrypt-f1wall-dd.ipetronik.com.pem" : "letsencrypt-f1wall.ipetronik.com.pem")));
                client.ClientCertificates.Add(cert);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("comtest@ipetronik.com");
                mail.To.Add("subin.sunilkumar@ipetronik.com");
                client.Send(mail);
                MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
    
}