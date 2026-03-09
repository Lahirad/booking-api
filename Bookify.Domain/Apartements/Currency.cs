using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Domain.Apartements
{
    public record Currency
    {
        public static readonly Currency sar = new("SAR");
        public static readonly Currency usd = new("USD");
        internal static readonly Currency none = new("");
        private Currency(string code)
        {
            Code = code;
        }

        //private Currency(string code) => Code = code;
        public string Code { get; init; }
        public static Currency FromCode(string code)
        {
            return All.FirstOrDefault(c => c.Code == code) ?? throw new ApplicationException("The currency code is invalid");
        }

        //public static Currency FromCode(string code) => All.FirstOrDefault(c => c.Code == code) ?? throw new ApplicationException("The Currency code is invalid.");
        public static readonly IReadOnlyCollection<Currency> All = new[]
        {
            usd,
            sar,
            none
        };
    }
}
