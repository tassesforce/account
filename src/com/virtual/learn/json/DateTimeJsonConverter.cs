using Newtonsoft.Json.Converters;

namespace compte.Json
{
    class DateTimeJsonConverter : IsoDateTimeConverter
    {
        public DateTimeJsonConverter()
        {
            base.DateTimeFormat = "dd/MM/yyyy";
        }
    }
}