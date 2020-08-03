using System.Collections.Generic;

namespace CurrencyConvertor.Engine
{
    public interface IConvertible
    {
        object Convert(string i_ConvertTo);

        List<string> GetConvertibleTypes();

        void SetValue(object i_Value);

        void SetType(object i_Type);
    }
}
