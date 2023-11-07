using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com",587);//for Host And Port
			Client.EnableSsl = true;//Used To make my mail increpted
			Client.Credentials = new NetworkCredential("om266266@gmail.com", "eevgjaqzlerrrdkw");
			Client.Send("om266266@gmail.com", email.To, email.Subject, email.Body);

		}
	}
}
