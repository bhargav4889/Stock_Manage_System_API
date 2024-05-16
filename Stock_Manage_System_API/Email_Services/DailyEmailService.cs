using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Stock_Manage_System_API.Email_Services
{
    public class DailyEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly string _connectionString;

        public DailyEmailService(IEmailSender emailSender, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _connectionString = configuration.GetConnectionString("MyConnection");
        }

        private async Task<DataTable> Get_Recent_Actions()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand("[dbo].[API_DISPLAY_LAST_24_HOURS_ACTIONS]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving recent actions: {ex.Message}");
            }
            return dataTable;
        }

        public async Task<(Attachment, byte[])> Create_Reporting_PDF(DataTable dataTable)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4);
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                float columnWidth = 100f;
                BaseFont boldfont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);
                BaseFont gujaratifont = BaseFont.CreateFont("https://localhost:7024/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);

                Paragraph title = new Paragraph("Daily Reporting", new iTextSharp.text.Font(boldfont, 35));
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                document.Add(new Chunk("\n"));

                iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://localhost:7024/Images/Backimg.png");
                backimage.ScaleToFit(300, 300);
                backimage.SetAbsolutePosition((PageSize.A4.Width - backimage.ScaledWidth) / 2, (PageSize.A4.Height - backimage.ScaledHeight) / 2);

                PdfGState gs = new PdfGState { FillOpacity = 0.4f, StrokeOpacity = 0.4f };
                PdfContentByte content = pdfWriter.DirectContentUnder;
                content.SetGState(gs);
                content.AddImage(backimage);

                PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
                pdfTable.WidthPercentage = 100;
                pdfTable.DefaultCell.Padding = 10;
                pdfTable.SetTotalWidth(Enumerable.Repeat(columnWidth, dataTable.Columns.Count).ToArray());

                foreach (DataColumn column in dataTable.Columns)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(boldfont, 12)));
                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    headerCell.Padding = 10;
                    pdfTable.AddCell(headerCell);
                }

                foreach (DataRow row in dataTable.Rows)
                {
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        var item = row[column];
                        PdfPCell dataCell;
                        if (item is DateTime dateTimeValue)
                        {
                            dataCell = new PdfPCell(new Phrase(dateTimeValue.ToShortDateString(), new iTextSharp.text.Font(boldfont, 12)));
                        }
                        else
                        {
                            dataCell = new PdfPCell(new Phrase(item?.ToString(), new iTextSharp.text.Font(boldfont, 12)));
                        }
                        dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        dataCell.Padding = 10;
                        pdfTable.AddCell(dataCell);
                    }
                }

                document.Add(pdfTable);
                document.Close();

                byte[] pdfBytes = memoryStream.ToArray();
                Attachment attachment = new Attachment(new MemoryStream(pdfBytes), $"Report_{DateTime.Now:yyyyMMdd}.pdf", "application/pdf");

                return (attachment, pdfBytes);
            }
        }

        /// <summary>
        /// Sends the daily email with the report attached as a PDF.
        /// </summary>
        /// <param name="recipientEmail">The email address of the recipient.</param>
        public async Task SendDailyActionsEmailAsync(string recipientEmail)
        {
            DataTable actionsData = await Get_Recent_Actions();
            if (actionsData != null && actionsData.Rows.Count > 0)
            {
                string subject = $"Daily Reporting - {DateTime.Now:yyyy-MM-dd}";
                string body = "Here Previous Activity Reports.";

                // Call the method to create PDF and get the attachment and MemoryStream
                var (attachment, memoryStream) = await Create_Reporting_PDF(actionsData);

                // Send email with attachment
                await _emailSender.SendEmailWithAttachmentAsync(recipientEmail, subject, body, attachment);
            }
            else
            {
                
            }
        }

    }
}
