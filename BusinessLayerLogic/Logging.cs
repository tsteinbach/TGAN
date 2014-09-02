using System;
using System.IO;
using System.Net;
using System.Web;



namespace BusinessLayerLogic  
{
	/// <summary>
	/// Zusammenfassung für Logging.
	/// </summary>
	public class Logging : System.Web.UI.Page
	{
		StreamWriter m_logger;
		

		public Logging()
		{
			StreamWriter sw = LoggWriter;
			m_logger = sw;
			this.RecordLogsWithTimeBeforeMessage();
		}

		private FileStream file
		{
			get
			{
				return new FileStream(logfile,FileMode.OpenOrCreate,FileAccess.Write);
			}
		}

		private string logfile
		{
			get
			{	
				return Path.Combine(Server.MapPath("~/tipp-game"),DateTime.Today.ToShortDateString()+"_tgan.txt");				
			}
		}

		private StreamWriter LoggWriter
		{
			get
			{
				if(File.Exists(logfile))
					return new StreamWriter(logfile,true);
                else
					return new StreamWriter(file);
			}
		}

		/// <summary>
		/// mit Angabe der Zeit im Log
		/// </summary>
		/// <param name="message"></param>
		private void RecordLogsWithTimeBeforeMessage()
		{
			m_logger.WriteLine(DateTime.Now.ToString());
			m_logger.WriteLine(Environment.StackTrace);
			try
			{
				m_logger.WriteLine("UserDomainName: "+Environment.UserDomainName);
				m_logger.WriteLine("UserName: "+Environment.UserName);
				m_logger.WriteLine("Directory: "+Environment.CurrentDirectory);
			}
			catch{}
			m_logger.WriteLine("--------------------------------------");
		}

		/// <summary>
		/// ohne Angabe der Zeit im Log
		/// </summary>
		/// <param name="logmessage"></param>
		/// <param name="sw"></param>
		public void RecordLogsWithoutTimeBeforeMessage(string logmessage)
		{
			m_logger.WriteLine("Message: => "+logmessage);
			m_logger.WriteLine("======================================");
		}

		public void closeLoggingProcess()
		{
			m_logger.Close();
			file.Close();
		}
	}
}
