using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

namespace Stock_Manage_System_API.Email_Services
{
    /// <summary>
    /// A service for sending a daily email with a report attached as a PDF.
    /// </summary>
    public class DailyEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DailyEmailService"/> class.
        /// </summary>
        /// <param name="emailSender">The email sender to use for sending emails.</param>
        public DailyEmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _connectionString = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory()) // Sets the base path for the configuration builder
              .AddJsonFile("appsettings.json") // Adds the appsettings.json file
              .Build()
              .GetConnectionString("MyConnection");
        }

        /// <summary>
        /// Gets the recent actions from the database.
        /// </summary>
        /// <returns>A <see cref="DataTable"/> containing the recent actions.</returns>
        private async Task<DataTable> Get_Recent_Actions()
        {
            DataTable dataTable = new DataTable();
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
            return dataTable;
        }

        /// <summary>
        /// Creates a PDF report from the given data table.
        /// </summary>
        /// <param name="dataTable">The data table to use for creating the report.</param>
        /// <returns>A tuple containing the PDF attachment and the byte array of the PDF.</returns>
        public async Task<(Attachment, byte[])> Create_Reporting_PDF(DataTable dataTable)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4);
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Adjust this value based on your design preference
                float columnWidth = 100f;

                BaseFont boldfont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED);
                BaseFont gujaratifont = BaseFont.CreateFont("https://localhost:7024/Fonts/NotoSansGujarati.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, true);

                // Add title
                Paragraph title = new Paragraph("Daily Reporting", new iTextSharp.text.Font(boldfont, 35));
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                document.Add(new Chunk("\n")); // Add a line break

                iTextSharp.text.Image backimage = iTextSharp.text.Image.GetInstance("https://localhost:7024/Images/Backimg.png");
                backimage.ScaleToFit(300, 300); // Adjust width and height

                // Set position to center of the page (A4 size typically is 595 x 842 points)
                backimage.SetAbsolutePosition((PageSize.A4.Width - backimage.ScaledWidth) / 2, (PageSize.A4.Height - backimage.ScaledHeight) / 2);

                // Apply opacity to the imagePdfContentByte content = pdfWriter.DirectContentUnder;
                PdfGState gs = new PdfGState
                {
                    FillOpacity = 0.4f, // 40% opacity
                    StrokeOpacity = 0.4f
                };
                PdfContentByte content = pdfWriter.DirectContentUnder;


                content.SetGState(gs);
                content.AddImage(backimage);

                PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
                pdfTable.WidthPercentage = 100;
                pdfTable.DefaultCell.Padding = 10;

                // Set the same width for all columns
                pdfTable.SetTotalWidth(Enumerable.Repeat(columnWidth, dataTable.Columns.Count).ToArray());

                // Adding header cells
                foreach (DataColumn column in dataTable.Columns)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, new iTextSharp.text.Font(boldfont, 12)));
                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    headerCell.Padding = 10;
                    pdfTable.AddCell(headerCell);
                }

                // Adding data cells
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
                document.Close(); // Close the document to finalize the PDF content

                byte[] pdfBytes = memoryStream.ToArray(); // Get the byte array of the PDF

                // Create an attachment using a new MemoryStream
                Attachment attachment = new Attachment(new MemoryStream(pdfBytes), $"Report_{DateTime.Now:yyyyMMdd}.pdf", "application/pdf");

                return (attachment, pdfBytes); // Return both the Attachment and the byte array
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
                string body = "Attached is your daily report of the recent activities.";

                // Call the method to create PDF and get the attachment and MemoryStream
                var (attachment, memoryStream) = await Create_Reporting_PDF(actionsData);

                // Send email with attachment
                await _emailSender.SendEmailWithAttachmentAsync(recipientEmail, subject, body, attachment);

            }
        }
    }
}