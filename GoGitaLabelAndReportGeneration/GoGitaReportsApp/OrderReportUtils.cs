using GoGitaReportsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xceed.Document.NET;
using Xceed.Words.NET;
using static GoGitaReportsApp.HttpUtils;

namespace GoGitaReportsApp
{
    public class OrderReportUtils
    {
        private static Dictionary<string, string> statusTypeToWooCommerceStatus = 
            new Dictionary<string, string>() 
            {
                { "Pending payment", "pending" },
                { "Processing", "processing" },
                { "On hold", "on-hold" },
                { "Completed", "completed" },
                { "Cancelled", "cancelled" },
                { "Refunded", "refunded" },
                { "Failed", "failed" },
                { "Draft", "trash" },
                { "All", "any"},
            };
        
        public delegate void OrderReportFunction(string filePath, string deliveryMethod, List<string> paymentStatusList, DateTime dateTimeFrom, DateTime dateTimeTo);

        public static Dictionary<string, OrderReportFunction> orderReportsHandler = new Dictionary<string, OrderReportFunction>()
        {
            { "Order Address Labels", (filePath, deliveryMethod, paymentStatusList, dateTimeFrom, dateTimeTo) 
                => CreateOrderAddressLabels(filePath, deliveryMethod, paymentStatusList, dateTimeFrom, dateTimeTo)},
            { "Order Book list", (filePath, deliveryMethod, paymentStatusList, dateTimeFrom, dateTimeTo)
                => CreateOrderBookList(filePath, deliveryMethod, paymentStatusList, dateTimeFrom, dateTimeTo)},
            { "Order Summary", (filePath, deliveryMethod, paymentStatusList, dateTimeFrom, dateTimeTo)
                => CreateOrderSummary(filePath, deliveryMethod, paymentStatusList, dateTimeFrom, dateTimeTo)}
        };

        public static void GenerateReport(string reportType, string filePath, string deliveryMethod, List<string> paymentStatusList, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            orderReportsHandler[reportType](filePath, deliveryMethod, paymentStatusList, dateTimeFrom, dateTimeTo);
        }

        public static void CreateOrderAddressLabels(string filePath, string deliveryMethod, List<string> paymentStatusList, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var document = DocX.Create(filePath);
            document.InsertParagraph("Order Address Labels").FontSize(15d).SpacingAfter(10d).Alignment = Alignment.left;
            List<string> finalPaymentStatusList = new List<string>();
            paymentStatusList.ForEach(a => finalPaymentStatusList.Add(statusTypeToWooCommerceStatus[a]));

            StringBuilder urlParamsBuilder = new StringBuilder();
            urlParamsBuilder.Append(Constants.BASE_URL_PARAM_KEY);
            urlParamsBuilder.Append("&after=" + dateTimeFrom.ToString("yyyy-MM-ddTHH:mm:ss"));
            urlParamsBuilder.Append("&before=" + dateTimeTo.ToString("yyyy-MM-ddTHH:mm:ss"));
            urlParamsBuilder.Append("&status=" + string.Join(",", finalPaymentStatusList.ToArray()));
            urlParamsBuilder.Append("&page={0}");
            int pageNum = 1;

            //setting HTTP request parameters.
            HttpRequestParams httpRequestParams = new HttpRequestParams();
            httpRequestParams.url = Constants.BASE_URL + "orders";
            httpRequestParams.parameters = string.Format(urlParamsBuilder.ToString(), pageNum);
            httpRequestParams.method = "GET";
            httpRequestParams.mediaType = "application/json";
            httpRequestParams.timeOut = 300;

            //sending http request
            var httpResponse = HttpUtils.GetHttpResponseMessage(httpRequestParams, new Dictionary<string, string>());
            List<Order> orders = httpResponse.Content.ReadAsAsync<List<Order>>().Result;
            orders.Select(order => order.shipping_lines[0].method_id == deliveryMethod).ToList();
            // Create a table.
            var table = document.AddTable(1, 3);
            table.Alignment = Alignment.left;
            int blockNum = 0, rowNum, colNum;

            while (orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    if (blockNum != 0 && blockNum % 3 == 0)
                        table.InsertRow();
                    rowNum = blockNum / 3;
                    colNum = blockNum % 3;

                    //var p = document.InsertParagraph();
                    StringBuilder addressLabelText = new StringBuilder();
                    addressLabelText.AppendLine(order.shipping.first_name + " " + order.shipping.last_name + ",");
                    addressLabelText.AppendLine(order.shipping.address_1 + " " + order.shipping.address_2);
                    addressLabelText.AppendLine(order.shipping.city + ", " + order.shipping.country);
                    addressLabelText.AppendLine("PIN: " + order.shipping.postcode);
                    addressLabelText.AppendLine("Phone: " + order.shipping.phone);
                    table.Rows[rowNum].Cells[colNum].Paragraphs[0].Append("ID: #" + order.id + "\nDate:" + order.date_created
                        + "\n-----------------------------------------").Font(new Xceed.Document.NET.Font("Calibri (Body)")).FontSize(11);

                    table.Rows[rowNum].Cells[colNum].InsertParagraph();
                    table.Rows[rowNum].Cells[colNum].Paragraphs[1].Append(addressLabelText.ToString()).Font(new Xceed.Document.NET.Font("Calibri (Body)")).FontSize(11);
                    blockNum++;

                    //This if block is to keep only four records in a page.
                    if (rowNum >= 3 && colNum >= 2)
                    {
                        document.InsertTable(table).InsertPageBreakAfterSelf();
                        table = document.AddTable(1, 3);
                        blockNum = rowNum = colNum = 0;
                    }
                }
                pageNum++;
                httpRequestParams.parameters = string.Format(urlParamsBuilder.ToString(), pageNum);
                httpResponse = HttpUtils.GetHttpResponseMessage(httpRequestParams, new Dictionary<string, string>());
                orders = httpResponse.Content.ReadAsAsync<List<Order>>().Result;
                orders.Select(order => order.shipping_lines[0].method_id == deliveryMethod).ToList();
            }
            table.SetWidths(new float[] { 200f, 200f, 200f });
            table.AutoFit = AutoFit.Contents;
            document.InsertTable(table).InsertPageBreakAfterSelf();
            document.Save();
        }

        public static void CreateOrderBookList(string filePath, string deliveryMethod, List<string> paymentStatusList, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            var document = DocX.Create(filePath);
            document.InsertParagraph("Order Book List").FontSize(15d).SpacingAfter(10d).Alignment = Alignment.left;
            List<string> finalPaymentStatusList = new List<string>();
            paymentStatusList.ForEach(a => finalPaymentStatusList.Add(statusTypeToWooCommerceStatus[a]));

            StringBuilder urlParamsBuilder = new StringBuilder();
            urlParamsBuilder.Append(Constants.BASE_URL_PARAM_KEY);
            urlParamsBuilder.Append("&after=" + dateTimeFrom.ToString("yyyy-MM-ddTHH:mm:ss"));
            urlParamsBuilder.Append("&before=" + dateTimeTo.ToString("yyyy-MM-ddTHH:mm:ss"));
            urlParamsBuilder.Append("&status=" + string.Join(",", finalPaymentStatusList.ToArray()));
            urlParamsBuilder.Append("&page={0}");
            int pageNum = 1;

            //setting HTTP request parameters.
            HttpRequestParams httpRequestParams = new HttpRequestParams();
            httpRequestParams.url = Constants.BASE_URL + "orders";
            httpRequestParams.parameters = string.Format(urlParamsBuilder.ToString(), pageNum);
            httpRequestParams.method = "GET";
            httpRequestParams.mediaType = "application/json";
            httpRequestParams.timeOut = 300;

            //sending http request
            var httpResponse = HttpUtils.GetHttpResponseMessage(httpRequestParams, new Dictionary<string, string>());
            List<Order> orders = httpResponse.Content.ReadAsAsync<List<Order>>().Result;
            while (orders.Count > 0)
            {
                foreach (Order order in orders)
                {
                    var p1 = document.InsertParagraph();
                    p1.AppendLine("ID:#" + order.id + " Name:" + order.shipping.first_name + " " + order.shipping.last_name + ",Phone:" + order.shipping.phone);
                    p1.AppendLine("Date:" + order.date_created);
                    List<BookInfo> books = order.line_items;
                    foreach (var book in books)
                    {
                        p1.AppendLine(book.name + " " + book.quantity);
                    }
                    p1.AppendLine("-------------------------------------------------------------");
                    p1.SpacingAfter(5);
                }
                pageNum++;
                httpRequestParams.parameters = string.Format(urlParamsBuilder.ToString(), pageNum);
                httpResponse = HttpUtils.GetHttpResponseMessage(httpRequestParams, new Dictionary<string, string>());
                orders = httpResponse.Content.ReadAsAsync<List<Order>>().Result;
                orders.Select(order => order.shipping_lines[0].method_id == deliveryMethod).ToList();
            }
            document.Save();
        }

        public static void CreateOrderSummary(string filePath, string deliveryMethod, List<string> paymentStatusList, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            List<string> finalPaymentStatusList = new List<string>();
            paymentStatusList.ForEach(a => finalPaymentStatusList.Add(statusTypeToWooCommerceStatus[a]));

            StringBuilder urlParamsBuilder = new StringBuilder();
            urlParamsBuilder.Append(Constants.BASE_URL_PARAM_KEY);
            urlParamsBuilder.Append("&after=" + dateTimeFrom.ToString("yyyy-MM-ddTHH:mm:ss"));
            urlParamsBuilder.Append("&before=" + dateTimeTo.ToString("yyyy-MM-ddTHH:mm:ss"));
            urlParamsBuilder.Append("&status=" + string.Join(",", finalPaymentStatusList.ToArray()));
            urlParamsBuilder.Append("&page={0}");
            int pageNum = 1;

            //setting HTTP request parameters.
            HttpRequestParams httpRequestParams = new HttpRequestParams();
            httpRequestParams.url = Constants.BASE_URL + "orders";
            httpRequestParams.parameters = string.Format(urlParamsBuilder.ToString(), pageNum);
            httpRequestParams.method = "GET";
            httpRequestParams.mediaType = "application/json";
            httpRequestParams.timeOut = 300;

            //sending http request
            var httpResponse = HttpUtils.GetHttpResponseMessage(httpRequestParams, new Dictionary<string, string>());
            List<Order> currOrders = httpResponse.Content.ReadAsAsync<List<Order>>().Result;
            List<Order> totalOrders = new List<Order>();
            Dictionary<string, BookInfoView> bookCodeToBookInfoView = new Dictionary<string, BookInfoView>();
            Dictionary<string, BookInfoView> bookCodeToBookInfoViewPrePacked = new Dictionary<string, BookInfoView>();
            Dictionary<string, BookInfoView> bookCodeToBookInfoViewExludingPrePacked = new Dictionary<string, BookInfoView>();
            totalOrders.AddRange(currOrders);

            int sNo = 1;
            while (currOrders.Count > 0)
            {
                pageNum++;
                httpRequestParams.parameters = string.Format(urlParamsBuilder.ToString(), pageNum);
                httpResponse = HttpUtils.GetHttpResponseMessage(httpRequestParams, new Dictionary<string, string>());
                currOrders = httpResponse.Content.ReadAsAsync<List<Order>>().Result;
                currOrders.Select(order => order.shipping_lines[0].method_id == deliveryMethod).ToList();
                totalOrders.AddRange(currOrders);
            }

            foreach (Order order in totalOrders)
            {
                List<BookInfo> books = order.line_items;
                foreach (var book in books)
                {
                    if (bookCodeToBookInfoView.ContainsKey(book.sku))
                        bookCodeToBookInfoView[book.sku].quantity = bookCodeToBookInfoView[book.sku].quantity + book.quantity;
                    else
                        bookCodeToBookInfoView.Add(book.sku, new BookInfoView(sNo++, book.name, book.sku, book.quantity));
                }
            }

            sNo = 1;
            foreach (Order order in totalOrders)
            {
                if (order.line_items.Count == 1 && order.line_items[0].name.Contains("Gita") && order.line_items[0].quantity == 1)
                {
                    BookInfo book = order.line_items[0];
                    if (bookCodeToBookInfoViewPrePacked.ContainsKey(book.sku))
                        bookCodeToBookInfoViewPrePacked[book.sku].quantity = bookCodeToBookInfoViewPrePacked[book.sku].quantity + book.quantity;
                    else
                        bookCodeToBookInfoViewPrePacked.Add(book.sku, new BookInfoView(sNo++, book.name, book.sku, book.quantity));
                }
                else
                {
                    List<BookInfo> books = order.line_items;
                    foreach (var book in books)
                    {
                        if (bookCodeToBookInfoViewExludingPrePacked.ContainsKey(book.sku))
                            bookCodeToBookInfoViewExludingPrePacked[book.sku].quantity = bookCodeToBookInfoViewExludingPrePacked[book.sku].quantity + book.quantity;
                        else
                            bookCodeToBookInfoViewExludingPrePacked.Add(book.sku, new BookInfoView(sNo++, book.name, book.sku, book.quantity));
                    }
                }
            }

            var document = DocX.Create(filePath);
            document.InsertParagraph("Order Summary Common").FontSize(15d).SpacingAfter(10d).Alignment = Alignment.center;

            // Create a table.
            var table = document.AddTable(1, 4);
            table.Alignment = Alignment.left;
            int rowNum = 0;

            foreach (var kvp in bookCodeToBookInfoView)
            {
                if (rowNum != 0)
                    table.InsertRow();
                table.Rows[rowNum].Cells[0].Paragraphs[0].Append(kvp.Value.sNo.ToString());
                table.Rows[rowNum].Cells[1].Paragraphs[0].Append(kvp.Value.bookName);
                table.Rows[rowNum].Cells[2].Paragraphs[0].Append(kvp.Value.bookCode);
                table.Rows[rowNum].Cells[3].Paragraphs[0].Append(kvp.Value.quantity.ToString());
                rowNum++;
            }
            table.SetWidths(new float[] { 50f, 150f, 150f, 100f });
            table.AutoFit = AutoFit.Contents;
            document.InsertTable(table).InsertPageBreakAfterSelf();

            document.InsertParagraph("Order Summary With Potential Pre-packed").FontSize(15d).SpacingAfter(10d).Alignment = Alignment.center;
            document.InsertParagraph("Only Potential Pre-packed orders").FontSize(11d).SpacingAfter(10d).Alignment = Alignment.left;

            // Create a table.
            table = document.AddTable(1, 4);
            table.Alignment = Alignment.left;
            rowNum = 0;
            foreach (var kvp in bookCodeToBookInfoViewPrePacked)
            {
                if (rowNum != 0)
                    table.InsertRow();
                table.Rows[rowNum].Cells[0].Paragraphs[0].Append(kvp.Value.sNo.ToString());
                table.Rows[rowNum].Cells[1].Paragraphs[0].Append(kvp.Value.bookName);
                table.Rows[rowNum].Cells[2].Paragraphs[0].Append(kvp.Value.bookCode);
                table.Rows[rowNum].Cells[3].Paragraphs[0].Append(kvp.Value.quantity.ToString());
                rowNum++;
            }
            table.SetWidths(new float[] { 50f, 150f, 150f, 100f });
            table.AutoFit = AutoFit.Contents;
            document.InsertTable(table);

            document.InsertParagraph("Excluding Potential Pre-packed orders").FontSize(11d).SpacingAfter(10d).Alignment = Alignment.left;

            // Create a table.
            table = document.AddTable(1, 4);
            table.Alignment = Alignment.left;
            rowNum = 0;
            foreach (var kvp in bookCodeToBookInfoViewExludingPrePacked)
            {
                if (rowNum != 0)
                    table.InsertRow();
                table.Rows[rowNum].Cells[0].Paragraphs[0].Append(kvp.Value.sNo.ToString());
                table.Rows[rowNum].Cells[1].Paragraphs[0].Append(kvp.Value.bookName);
                table.Rows[rowNum].Cells[2].Paragraphs[0].Append(kvp.Value.bookCode);
                table.Rows[rowNum].Cells[3].Paragraphs[0].Append(kvp.Value.quantity.ToString());
                rowNum++;
            }

            table.SetWidths(new float[] { 50f, 150f, 150f, 100f });
            table.AutoFit = AutoFit.Contents;
            document.InsertTable(table);
            document.Save();
        }

        public class BookInfoView
        {
            public int sNo { get; set; }
            public string bookName { get; set; }
            public string bookCode { get; set; }
            public int quantity { get; set; }

            public BookInfoView(int sNo, string bookName, string bookCode, int quantity)
            {
                this.sNo = sNo;
                this.bookName = bookName;
                this.bookCode = bookCode;
                this.quantity = quantity;
            }
        }
    }
}
