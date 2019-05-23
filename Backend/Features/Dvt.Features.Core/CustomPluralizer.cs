using System.Threading;
using Inflector;
using Microsoft.EntityFrameworkCore.Design;

namespace Dvt.Features.Core
{
    public class CustomPluralizer : IPluralizer
    {
        public string Pluralize(string name)
        {
            Inflector.Inflector.SetDefaultCultureFunc = () => Thread.CurrentThread.CurrentUICulture; // must be setled before using extension methods

            return name.Pluralize();
        }
        public string Singularize(string name)
        {
            Inflector.Inflector.SetDefaultCultureFunc = () => Thread.CurrentThread.CurrentUICulture; // must be setled before using extension methods

            return name.Singularize();
        }
    }
}
