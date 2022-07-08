using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL.EntitiesFramwork;
using RestSharp;

namespace BLL.DAO
{
    public class BaremDAO
    {

        /// <summary>
        /// Tìm mặt hàng
        /// </summary>
        /// <param name="MaMH"> mã mặt hàng</param>
        /// <returns>đói tượng mặt hàng</returns>
        public Barem FindBarem(string MaMH)
        {
            using (DbConnection db = new DbConnection())
            {
                Barem barem = new Barem();

                barem = db.Barems.Find(MaMH);
                if(barem == null)
                {
                    return null;
                }
                return barem;
            }
        }

        /// <summary>
        /// Tìm mặt hàng trên api
        /// </summary>
        /// <param name="CompanyCode"> CompanyCode</param>
        /// <param name="MaMH"> mã mặt hàng</param>
        /// <returns>đói tượng mặt hàng</returns>
        public Barem FindBaremWithAPI(string CompanyCode,string MaMH)
        {
            var client = new RestClient("http://192.168.100.73:8003/sap/bc/srt/rfc/sap/zws_barem/800/zws_barem/zws_barem");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "text/xml; charset=utf-8");
            request.AddHeader("SOAPAction", "urn:sap-com:document:sap:rfc:functions:ZWS_barem:ZVAS_BAREMRequest");
            request.AddHeader("Authorization", "Basic TlpaLklUMDE6MjAxMUAxMjM0NQ==");
            var body = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:sap-com:document:sap:rfc:functions"">" + "\n" +
            @"   <soapenv:Header/>" + "\n" +
            @"   <soapenv:Body>" + "\n" +
            @"      <urn:ZVAS_BAREM>" + "\n" +
            @"         <COMPANY>" + CompanyCode + "</COMPANY>" + "\n" +
            @"         <!--Optional:-->" + "\n" +
            @"         <MAC></MAC>" + "\n" +
            @"         <!--Optional:-->" + "\n" +
            @"         <MATERIAL>" + MaMH + "</MATERIAL>" + "\n" +
            @"         <!--Optional:-->" + "\n" +
            @"         <PHI></PHI>" + "\n" +
            @"         <ZBAREM>" + "\n" +
            @"            <!--Zero or more repetitions:-->" + "\n" +
            @"            <item>" + "\n" +
            @"               <MANDT></MANDT>" + "\n" +
            @"               <MATNR></MATNR>" + "\n" +
            @"               <VKORG></VKORG>" + "\n" +
            @"               <MACTHEP></MACTHEP>" + "\n" +
            @"               <PHITHEP></PHITHEP>" + "\n" +
            @"               <BAREM></BAREM>" + "\n" +
            @"               <CAY_BO></CAY_BO>" + "\n" +
            @"            </item>" + "\n" +
            @"         </ZBAREM>" + "\n" +
            @"      </urn:ZVAS_BAREM>" + "\n" +
            @"   </soapenv:Body>" + "\n" +
            @"</soapenv:Envelope>";
            request.AddParameter("text/xml; charset=utf-8", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string xml = response.Content.ToString();
            XDocument xmlResponse = XDocument.Parse(xml);
            int count = xmlResponse.Descendants("ZBAREM").Descendants("item").Count();
            Barem xklt = new Barem();
            if (count != 0)
            {
                foreach (XElement element in xmlResponse.Descendants("ZBAREM").Descendants("item"))
                {

                    xklt.Material = element.Element("MATNR").Value;
                    xklt.MANDT = element.Element("MATNR").Value;
                    xklt.VKORG = element.Element("VKORG").Value;
                    xklt.MACTHEP = element.Element("MACTHEP").Value;
                    xklt.PHITHEP = element.Element("PHITHEP").Value;
                    string uiSep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                    if (uiSep.Equals(","))
                    {
                        xklt.BAREM1 = decimal.Parse(element.Element("BAREM").Value.Replace('.', ','));

                        if (xklt.BAREM1 == 0 || string.IsNullOrEmpty(element.Element("BAREM").Value.Replace('.', ',')))
                        {
                            return null;
                        }

                        if (string.IsNullOrEmpty(element.Element("CAY_BO").Value))
                        {
                            return null;
                        }
                    }
                    else
                    {
                        xklt.BAREM1 = decimal.Parse(element.Element("BAREM").Value);

                        if (xklt.BAREM1 == 0 || string.IsNullOrEmpty(element.Element("BAREM").Value))
                        {
                            return null;
                        }
                        if (string.IsNullOrEmpty(element.Element("CAY_BO").Value))
                        {
                            return null;
                        }
                    }
                    xklt.CAY_BO = int.Parse(element.Element("CAY_BO").Value);

                }
                return xklt;
            }
            else
            {

                return null;
            }
        }


        /// <summary>
        /// Nhập số bó trả về số cây
        /// </summary>
        /// <param name="barem">Đối tượng mặt hàng</param>
        /// <param name="SOBC">Số bó cuộn</param>
        /// <returns>Số cây </returns>
        public int ChangeSOBCtoSOCAY(Barem barem, int SOBC)
        {
            int a = SOBC * (int)barem.CAY_BO;
            return a;

        }

        /// <summary>
        /// Nhập số cây đưa ra số kg
        /// </summary>
        /// <param name="barem">Đối tượng mặt hàng</param>
        /// <param name="SOCAY">Số cây</param>
        /// <returns>Trọng lượng tấn</returns>
        public decimal ChangeSOBCtoTrongLuong(Barem barem, int SOCAY)
        {
            decimal a = (decimal)Math.Round(((decimal)SOCAY * barem.BAREM1.Value) / 1000, 3);

            decimal t = (decimal)Math.Round((decimal)SOCAY * barem.BAREM1.Value, 0) / 1000;

            return a;
        }


    }
}
