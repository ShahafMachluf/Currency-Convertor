using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace CurrencyConvertor.Engine
{
    public class Currency : IConvertible
    {
        private const string k_ApiUrl = "https://free.currconv.com";
        private const string k_ApiKey = "8e148ee4584f0e546ca6";
        private float m_Value;
        private string m_CurrencyType;

        private string callAPI(string i_Request)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(i_Request);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private string createExchangeRequestURL(string i_ConvertTo)
        {
            StringBuilder request = new StringBuilder();

            request.Append(k_ApiUrl);
            request.Append("/api/v7/convert?q=");
            request.Append(m_CurrencyType);
            request.Append("_");
            request.Append(i_ConvertTo);
            request.Append("&compact=ultra&apiKey=");
            request.Append(k_ApiKey);

            return request.ToString();
        }

        private string createSupportedCurrenciesRequestURL()
        {
            StringBuilder URL = new StringBuilder();
            URL.Append(k_ApiUrl);
            URL.Append("/api/v7/currencies?apiKey=");
            URL.Append(k_ApiKey);

            return URL.ToString();
        }

        public object Convert(string i_ConvertTo)
        {
            string jsonString, requestURL, extractedValueFromJson;

            requestURL = createExchangeRequestURL(i_ConvertTo);
            jsonString = callAPI(requestURL);
            if(jsonString.Length == 2)
            {
                throw new ArgumentException("ERROR. currency type: " + m_CurrencyType + " or " + i_ConvertTo + " is not supported by the API.");
            }
            extractedValueFromJson = jsonString.Substring(11);
            extractedValueFromJson = extractedValueFromJson.Trim('}');

            return float.Parse(extractedValueFromJson) * m_Value;
        }

        public void SetValue(object i_Value)
        {
            float.TryParse(i_Value.ToString(), out m_Value);
        }

        public void SetType(object i_Type)
        {
            m_CurrencyType = i_Type.ToString();
        }

        public List<string> GetConvertibleTypes()
        {
            string requestURL = createSupportedCurrenciesRequestURL();
            string jsonString = callAPI(requestURL);
            JContainer root = (JContainer)JToken.Parse(jsonString);
            List<string> currencies = root.DescendantsAndSelf().OfType<JProperty>().Where(property => property.Name == "id").Select(property => property.Value.Value<string>()).ToList();

            return currencies;
        }
    }
}
