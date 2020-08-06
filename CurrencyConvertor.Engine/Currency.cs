using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace CurrencyConvertor.Engine
{
    public class Currency : ICurrencyConvert
    {
        //private const string k_ApiUrl = "https://free.currconv.com";
        //private const string k_ApiKey = "8e148ee4584f0e546ca6";        
        private const string k_ApiUrl = "https://marketdata.tradermade.com";
        private const string k_ApiKey = "NmuP14cEWkVnese0FsVl";
        private float m_Value;
        private string m_CurrencyType;

        public void SetValue(float i_Value)
        {
            m_Value = i_Value;
        }

        public void SetType(string i_Type)
        {
            m_CurrencyType = i_Type;
        }

        private string issueGetRequest(string i_Request)
        {
            WebClient client = new WebClient();
            return client.DownloadString(i_Request);
        }

        public float Convert(string i_ConvertTo)
        {
            string jsonString, requestURL;

            requestURL = $"{k_ApiUrl}/api/v1/convert?api_key={k_ApiKey}&from={m_CurrencyType}&to={i_ConvertTo}&amount=1";
            jsonString = issueGetRequest(requestURL);
            JObject jObj = JObject.Parse(jsonString);
            string exchangeRate = jObj["total"].ToString();

            return float.Parse(exchangeRate) * m_Value;
        }

        public List<string> GetConvertibleTypes()
        {
            List<string> result = new List<string>();
            string requestURL = $"{k_ApiUrl}/api/v1/live_currencies_list?api_key={k_ApiKey}";
            string jsonString = issueGetRequest(requestURL);
            JObject jObj = JObject.Parse(jsonString);
            IList<JToken> currencies = jObj["available_currencies"].Children().ToList();

            for(int i = 0;i<currencies.Count;i++)
            {
                result.Add(currencies[i].ToString());
                result[i] = result[i].Substring(1, 3);
            }

            return result;
        }

        //public List<string> GetConvertibleTypes()
        //{
        //    string requestURL = createSupportedCurrenciesRequestURL();
        //    string jsonString = callAPI(requestURL);
        //    JContainer root = (JContainer)JToken.Parse(jsonString);
        //    List<string> currencies = root.DescendantsAndSelf().OfType<JProperty>().Where(property => property.Name == "id").Select(property => property.Value.Value<string>()).ToList<string>();

        //    return currencies;
        //}

        //private string createExchangeRequestURL(string i_ConvertTo)
        //{
        //    StringBuilder request = new StringBuilder();

        //    request.Append(k_ApiUrl);
        //    request.Append("/api/v7/convert?q=");
        //    request.Append(m_CurrencyType);
        //    request.Append("_");
        //    request.Append(i_ConvertTo);
        //    request.Append("&compact=ultra&apiKey=");
        //    request.Append(k_ApiKey);

        //    return request.ToString();
        //}

        //private string createSupportedCurrenciesRequestURL()
        //{
        //    StringBuilder URL = new StringBuilder();
        //    URL.Append(k_ApiUrl);
        //    URL.Append("/api/v7/currencies?apiKey=");
        //    URL.Append(k_ApiKey);

        //    return URL.ToString();
        //}

        //public object Convert(string i_ConvertTo)
        //{
        //    string jsonString, requestURL, extractedValueFromJson;

        //    requestURL = createExchangeRequestURL(i_ConvertTo);
        //    jsonString = callAPI(requestURL);
        //    if(jsonString.Length == 2)
        //    {
        //        throw new ArgumentException("ERROR. currency type: " + m_CurrencyType + " or " + i_ConvertTo + " is not supported by the API.");
        //    }
        //    extractedValueFromJson = jsonString.Substring(11);
        //    extractedValueFromJson = extractedValueFromJson.Trim('}');

        //    return float.Parse(extractedValueFromJson) * m_Value;
        //}
    }
}
