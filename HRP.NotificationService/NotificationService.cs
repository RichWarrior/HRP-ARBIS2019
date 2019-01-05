using FarukSahin.MailService;
using HRP.Arbis.DataAccessLayer;
using HRP.Arbis.DataAccessLayer.ServerParameter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HRP.NotificationService
{
    partial class NotificationService : ServiceBase
    {
        private Smtp smtp;
        private BLL bll = new BLL();
        public NotificationService()
        {
            InitializeComponent();
            var smtpInfo = bll.GetServerParameter<SmtpModel>().Result;
            smtp = new Smtp(smtpInfo.smtp_sender,smtpInfo.smtp_password,smtpInfo.smtp_host,smtpInfo.smtp_port);
            this.OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            Timer t = new Timer();
            t.Interval = TimeSpan.FromSeconds(30).TotalMilliseconds;
            t.Start();
            t.Elapsed += T_Elapsed;
        }

        private async void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            (sender as Timer).Stop();
            var value =await bll.Action().ListAsync();
            Console.WriteLine(String.Format("{0} Adet Mail Gönderilecek!",value.Count));
            foreach (var item in value)
            {
                smtp.AddMail("HRP_ARBIS-2019",item.Message);
                var result = await smtp.SendAsync(item.email);
            }
            (sender as Timer).Start();
        }

        protected override void OnStop()
        {            
        }
    }
}
