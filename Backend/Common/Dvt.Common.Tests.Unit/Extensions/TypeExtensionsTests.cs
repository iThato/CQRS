using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dvt.Common.Extensions;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class TypeExtensionsTests
    {
        public class MyTestType : IComparable<int> {
            public int CompareTo(int other)
            {
                throw new NotImplementedException();
            }
        }

        [Fact]
        public void GetFriendlyName_IsGeneric_ShouldBuildNameCorrectly()
        {
            var item = new List<string> { "Dog", "Cat" };
            var result = item.GetType().GetFriendlyName();
            result.ShouldBe("List<String>");
        }

        [Fact]
        public void GetFriendlyName_IsGenericWithMultipleTypes_ShouldBuildNameCorrectly()
        {
            var item = new Tuple<int, int, string>(1,2,"a");
            var result = item.GetType().GetFriendlyName();
            result.ShouldBe("Tuple<Int32,Int32,String>");
        }

        [Fact]
        public void GetFriendlyName_NonGeneric_ShouldEqualTypeName()
        {
            var item = new DirectoryNotFoundException();
            var result = item.GetType().GetFriendlyName();
            result.ShouldBe("DirectoryNotFoundException");
        }

        [Fact]
        public void TypesImplementingInterface_LookForKnowType_ShouldResolve()
        {
            var result = typeof(IComparable<int>).TypesImplementingInterface();
            var expected = typeof(MyTestType).FullName;
            result.Any(t => t.FullName == expected).ShouldBeTrue();
        }

    }
}
