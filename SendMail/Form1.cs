using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace SendMail
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofdAttachment;
        string fileName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            try
            {
                ofdAttachment = new OpenFileDialog();
                ofdAttachment.Filter = "Images(.jpg,.png)|*.png;*jpg;|Pdf files|*.pdf";
                if (ofdAttachment.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofdAttachment.FileName;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //smtp client details
                //gmail >>smtp: smtp.gmail.com,port:587,ssl required
                //yahoo >>smtp.server: smtp.mail.yahoo.com,port:587,ssl required
                SmtpClient clientDetails = new SmtpClient();
                clientDetails.Port = Convert.ToInt32(txtPortNumber.Text.Trim());
                clientDetails.Host =txtSmtpServer.Text.Trim();
                clientDetails.EnableSsl = cbxSSL.Checked;
                clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientDetails.UseDefaultCredentials = false;
                clientDetails.Credentials = new NetworkCredential(txtSenderEmail.Text.Trim(),txtSenderPassword.Text.Trim());

                //Message Details
                MailMessage mailsDetails = new MailMessage();
                mailsDetails.From = new MailAddress(txtSenderEmail.Text.Trim());
                mailsDetails.To.Add(txtRecipientEmail.Text.Trim());
                //for multiple recipients
                //for bcc
                //mailDetails.bcc.add("bcc email adress")
                mailsDetails.Subject = txtSubject.Text.Trim();
                mailsDetails.IsBodyHtml = cbxHtmlBody.Checked;
                mailsDetails.Body=rtbBody.Text.Trim();

                //file attachment
                if (fileName.Length > 0)
                {
                    Attachment attachment = new Attachment(fileName);
                    mailsDetails.Attachments.Add(attachment);
                }

                clientDetails.Send(mailsDetails);
                MessageBox.Show("Your mail has been sent");
                fileName = "";

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
