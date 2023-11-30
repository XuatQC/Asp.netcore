using System;
using System.Collections.Generic;

namespace FOFB.Shared.Entities
{
	public class AmazonSqsNotification
	{
		public string Type { get; set; }
		public string Message { get; set; }
	}

	/// <summary>
	/// Represents an Amazon SES bounce notification.
	/// </summary>
	public class AmazonSesBounceNotification
	{
		public string NotificationType { get; set; }
		public AmazonSesBounce Bounce { get; set; }
		public MailSend Mail { get; set; }
	}

	/// <summary>
	/// Represents meta data for the bounce notification from Amazon SES
	/// </summary>
	public class AmazonSesBounce
	{
		public string BounceType { get; set; }
		public string BounceSubType { get; set; }
		public DateTime Timestamp { get; set; }
		public List<AmazonSesBouncedRecipient> BouncedRecipients { get; set; }
	}

	/// <summary>
	/// Represents the email address of recipients that bounced when sending from Amazon SES.
	/// </summary>
	public class AmazonSesBouncedRecipient
	{
		public string EmailAddress { get; set; }
		public string Status { get; set; }
		public string DiagnosticCode { get; set; }
	}

	/// <summary>
	/// mail send info
	/// </summary>
	public class MailSend
	{
		public Headers CommonHeaders { get; set; }
	}

	/// <summary>
	/// Header
	/// </summary>
	public class Headers
	{
		public string Subject { get; set; }
	}
}
