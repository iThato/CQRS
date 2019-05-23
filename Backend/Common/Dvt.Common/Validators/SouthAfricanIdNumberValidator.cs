using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Dvt.Common.Extensions;

namespace Dvt.Common.Validators
{
    public class SouthAfricanIdNumberValidator
    {
        public SouthAfricanIdNumberValidator(string identityNumber)
        {
            Initialize(identityNumber);
        }

        public string IdentityNumber { get; private set; }


        public bool IsMale { get; private set; }

        public bool IsFemale { get; private set; }

        public bool IsSouthAfrican { get; private set; }

        public bool IsValid { get; private set; }

        private void Initialize(string identityNumber)
        {
            if (identityNumber.IsNullOrEmptyTrimmed()) return;

            const string numericPattern = @"^\d{13}$";
            IdentityNumber = identityNumber.Trim();
            if (IdentityNumber.Length != 13) return;
            var isMatch = Regex.IsMatch(IdentityNumber, numericPattern);
            if (!isMatch) return;

            var month = int.Parse(identityNumber.Substring(2, 2));
            if (month < 1 || month > 12) return;

            var day = int.Parse(identityNumber.Substring(4, 2));
            if (day < 1 || day > 31) return;

            var digits = new int[13];
            for (var i = 0; i < 13; i++)
                digits[i] = int.Parse(IdentityNumber.Substring(i, 1));
            var control1 = digits.Where((v, i) => i%2 == 0 && i < 12).Sum();
            var second = string.Empty;
            digits.Where((v, i) => i%2 != 0 && i < 12).ToList().ForEach(v => second += v.ToString(CultureInfo.InvariantCulture));
            var string2 = (int.Parse(second)*2).ToString(CultureInfo.InvariantCulture);
            var control2 = string2.Select((t, i) => int.Parse(string2.Substring(i, 1))).Sum();
            var control = (10 - (control1 + control2)%10)%10;

            if (digits[12] != control) return;

            IsFemale = digits[6] < 5;
            IsMale = !IsFemale;
            IsSouthAfrican = digits[10] == 0;
            IsValid = true;
        }

        public static IEnumerable<string> DetectIdNumbers(string search)
        {
            var result = new List<string>();
            if (search.IsNullOrEmptyTrimmed())
                return result;

            var matches = Regex.Match(search, @"(?<!\d)\d{13}(?!\d)", RegexOptions.Multiline);
            while (matches.Success)
            {
                if (new SouthAfricanIdNumberValidator(matches.Value).IsValid)
                    result.Add(matches.Value);

                matches = matches.NextMatch();
            }

            return result.Distinct().ToList();
        }
    }
}
