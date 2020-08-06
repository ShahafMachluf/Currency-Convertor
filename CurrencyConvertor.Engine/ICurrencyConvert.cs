using System.Collections.Generic;

namespace CurrencyConvertor.Engine
{
    public interface ICurrencyConvert
    {
        float Convert(string i_ConvertTo);

        List<string> GetConvertibleTypes();
    }
}