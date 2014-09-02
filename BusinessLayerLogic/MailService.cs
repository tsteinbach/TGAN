using System;
using System.Net.Mail;


using DataLayerLogic;

namespace BusinessLayerLogic
{
	/// <summary>
	/// Zusammenfassung für MailService.
	/// </summary>
	public class MailService
	{
        //private Configurations m_Config;
		
		public MailService()
		{
			//m_Config = new Configurations();
		}

		public void SendMail()
		{
            //Buisinesses business;
			
            //MailMessage Message = new MailMessage();
            //Message.To = m_Config.MailTo;
            //Message.From = m_Config.MailFrom;
            //SmtpMail.SmtpServer = m_Config.SMTP_Server;
			
			
            //try
            //{
            //    if((Convert.ToDateTime(DateTime.Now.ToShortTimeString()) >= Convert.ToDateTime(m_Config.DateTimeToCheckSQLServerFrom))&&
            //        (Convert.ToDateTime(DateTime.Now.ToShortTimeString()) <= Convert.ToDateTime(m_Config.DateTimeToCheckSQLServerTill)))	
            //    {
            //        // sende Erfolgsmail
            //        try
            //        {
            //            business = new Buisinesses();
            //            business.conn.Open();
            //            business.conn.Close();
					
            //            Message.Subject = m_Config.SubjectSQLServer_IsAvailable;
            //        }
            //            // sende Mißerfolgsmail
            //        catch(Exception e)
            //        {
            //            Message.Subject = m_Config.SubjectSQLServer_NotAvailable;
            //            Message.Body = e.ToString();
            //        }
            //        SmtpMail.Send(Message);
            //    }
            //}
            //catch(Exception e)
            //{
            //    string error = e.ToString();
            //}
			
		}


	}
}
