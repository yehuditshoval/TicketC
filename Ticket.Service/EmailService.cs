using System.Net.Mail;
using System.Net;
using Ticket.Core.Models;
using Ticket.Core.Repositories;
using Ticket.Core.Service;
using Microsoft.Extensions.Options;

namespace Ticket.Service
{
    public class EmailService : IEmailService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IEventRepository _eventRepository;
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IUsersRepository usersRepository, ISeatRepository seatRepository, IEventRepository eventRepository, IOptions<SmtpSettings> smtpSettings)
        {
            _usersRepository = usersRepository;
            _seatRepository = seatRepository;
            _eventRepository = eventRepository;
            _smtpSettings = smtpSettings.Value;  
        }

        public void SendSeatAssignmentEmails(List<SeatAssignments> assignments)
        {
            foreach (var assignment in assignments)
            {
                var user = _usersRepository.GetUsersById(assignment.UserId);
                var seat = _seatRepository.GetSeatById(assignment.SeatId);
                var event1 = _eventRepository.GetEventById(assignment.EventId);

                if (user == null || seat == null || event1 == null) continue;

                string subject = "אישור שיבוץ מושב להופעה";
                string body = $"שלום {user.Name},\n\n" +
                              $"המיקום שלך באירוע: שורה {seat.numLine}, מושב {seat.SeatNumber}.\n\n" +
                              $"כניסה בתשלום מראש חצי שעה לפני תחילת ההופעה בסך {event1.Price} נא לבא בזמן , לא ינתן לקנות יותר אחרי ההתחלת ההופעה.\n\n" +
                              "לפרטים נוספים, ניתן לפנות אלינו.\n\n" +
                              "בברכה,\nצוות המכירה";

                SendEmail(user.Email, subject, body);
            }
        }

        private void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress(_smtpSettings.FromEmail, "Ticketing System");
                var toAddress = new MailAddress(toEmail);
                var fromPassword = _smtpSettings.Password;

                var smtp = new SmtpClient
                {
                    Host = _smtpSettings.Host,
                    Port = _smtpSettings.Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("שגיאה בשליחת המייל: " + ex.Message);
            }
        }
    }
}
