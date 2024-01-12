using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.Email
{
    public class EmailBrevoPayload
    {
        public EmailContact sender { get; set; }
        public List<EmailContact> to { get; set; }
        public int templateId { get; set; }
        public Dictionary<string, string>? @params { get; set; }
        public List<BrevoAttachment> attachment { get; set; }
    }
}
