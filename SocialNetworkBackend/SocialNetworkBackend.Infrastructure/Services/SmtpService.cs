﻿using SocialNetworkBackend.Application.Services;
using SocialNetworkBackend.Infrastructure.EF.Options;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace SocialNetworkBackend.Infrastructure.Services;

/// <summary>
/// Implementation for service used for email communication.
/// </summary>
public class SmtpService : ISmtpService
{
    private readonly SmtpOptions _smtpOptions;

    public SmtpService(SmtpOptions smtpOptions)
    {
        _smtpOptions = smtpOptions;
    }

    ///<inheritdoc/>
    public async Task SendMessage(string email, string subject, string message)
    {
        string fromMail = _smtpOptions.FromMail;
        string fromPassword = _smtpOptions.FromPassword;

        MailMessage mailMessage = new MailMessage();

        mailMessage.From = new MailAddress(fromMail);
        mailMessage.Subject = subject;
        mailMessage.To.Add(new MailAddress(email));
        mailMessage.Body = string.Format(_smtpOptions.Body, message);
        mailMessage.IsBodyHtml = true;

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = true
        };
        smtpClient.Send(mailMessage);
    }
}