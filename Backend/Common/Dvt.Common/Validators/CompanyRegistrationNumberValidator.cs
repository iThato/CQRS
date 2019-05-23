using System.Text.RegularExpressions;

namespace Dvt.Common.Validators
{
    public class CompanyRegistrationNumberValidator
    {
        public bool IsValid { get; private set; }

        public CompanyRegistrationNumberValidator(string registrationNumber)
        {
            /*
            Assert position at the beginning of the string «^»
            Match a single character that is a “digit” (any decimal number in any Unicode script) «\d{2,4}»
               Between 2 and 4 times, as many times as possible, giving back as needed (greedy) «{2,4}»
            Match the character “/” literally «/»
            Match a single character that is a “digit” (any decimal number in any Unicode script) «\d{5,6}»
               Between 5 and 6 times, as many times as possible, giving back as needed (greedy) «{5,6}»
            Match the character “/” literally «/»
            Match a single character that is a “digit” (any decimal number in any Unicode script) «\d{2}»
               Exactly 2 times «{2}»

            or

            Assert position at the beginning of the string «^»
            Match a single character that is a “digit” (any decimal number in any Unicode script) «\d{9,12}»
               Between 9 and 12 times, as many times as possible, giving back as needed (greedy) «{9,12}»
            Assert position at the end of the string, or before the line break at the end of the string, if any (line feed) «$»
            */
            const string firstPattern = @"^\d{2,4}/\d{5,6}/\d{2}$";
            const string secondPattern = @"^\d{9,12}$";
            var isMatch = Regex.IsMatch(registrationNumber, firstPattern) || Regex.IsMatch(registrationNumber, secondPattern);
            IsValid = isMatch;
        }
    }
}
