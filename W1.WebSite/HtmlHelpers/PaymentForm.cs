using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
using W1.Domain.Entities;

namespace W1.HtmlHelpers
{
    public class PaymentForm : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            
            SortedDictionary<string, string> formField = new SortedDictionary<string, string>();

            formField.Add("WMI_MERCHANT_ID", "141659164712");
            formField.Add("WMI_PAYMENT_AMOUNT", ((Cart)context.Items["Cart"]).ComputeTotalValue().ToString().Replace(',', '.'));
            formField.Add("WMI_CURRENCY_ID", "643");
            //formField.Add("WMI_PAYMENT_NO", "12345-001");
            //formField.Add("WMI_DESCRIPTION", "BASE64:" + Convert.ToBase64String(Encoding.UTF8.GetBytes("Payment for order #12345-001 in MYSHOP.com")));
            formField.Add("WMI_EXPIRED_DATE", DateTime.Now.AddDays(1.0).ToString());
            //formField.Add("WMI_SUCCESS_URL", "https://myshop.com/w1/success.php");
            //formField.Add("WMI_FAIL_URL", "https://myshop.com/w1/fail.php");
            formField.Add("Name", ((ShippingDetails)context.Items["ShippingDetails"]).Name); // Дополнительные параметры
            formField.Add("City", ((ShippingDetails)context.Items["ShippingDetails"]).City); // магазина тоже участвуют
            //formField.Add("Address", ((ShippingDetails)context.Items["ShippingDetails"]).Address); // при формировании подписи!

            // Формирование сообщения, путем объединения значений формы, 
            // отсортированных по именам ключей в порядке возрастания и
            // добавление к нему "секретного ключа" интернет-магазина

            // Формирование платежной формы

            StringBuilder output = new StringBuilder();

            output.AppendLine(String.Format(new System.Globalization.CultureInfo("ru-RU"),
            "Заказ {0} на сумму {1,10:C}",
                ((ShippingDetails)context.Items["ShippingDetails"]).Name,
                ((Cart)context.Items["Cart"]).ComputeTotalValue()));

            output.AppendLine("<form method=\"POST\" action=\"https://wl.walletone.com/checkout/checkout/Index\">");


            foreach (string key in formField.Keys)
            {
                output.AppendLine(String.Format("<input name=\"{0}\" value=\"{1}\" type=\"hidden\"/>", key, formField[key]));
            }

            output.AppendLine("<input type=\"submit\" value=\"Оплатить\"/></form>");

            context.Response.ContentType = "text/html; charset=UTF-8";
            context.Response.Write(output.ToString());
        }

        public bool IsReusable { get { return true; } }
    }
}