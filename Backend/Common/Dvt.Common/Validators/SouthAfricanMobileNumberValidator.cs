using System.Text.RegularExpressions;

namespace Dvt.Common.Validators
{
    public class SouthAfricanMobileNumberValidator
    {
        public bool IsValid { get; private set; }

        public SouthAfricanMobileNumberValidator(string registrationNumber)
        {
            /*
            Assert position at the beginning of the string «^»
            Match the regex below and capture its match into backreference number 1 «(\+?27|0)»
                Match this alternative (attempting the next alternative only if this one fails) «\+?27»
                    Match the character “+” literally «\+?»
                        Between zero and one times, as many times as possible, giving back as needed (greedy) «?»
                    Match the character string “27” literally «27»
                Or match this alternative (the entire group fails if this one fails to match) «0»
                    Match the character “0” literally «0»
            Match a single character in the range between “6” and “9” «[6-9]»
            Match a single character in the range between “1” and “7” «[1-7]»
            Match a single character in the range between “0” and “9” «[0-9]{7}»
                Exactly 7 times «{7}»
            Assert position at the end of the string, or before the line break at the end of the string, if any (line feed) «$»
            */
            const string pattern = @"^(\+?27|0)[6-9][1-7][0-9]{7}$";
            var isMatch = Regex.IsMatch(registrationNumber, pattern);
            IsValid = isMatch;
        }
    }
}
