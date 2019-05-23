using System;
using Dvt.Common.Extensions;
using Shouldly;
using Xunit;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertToConstant.Local
// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable UnusedMember.Local

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class ReflectionExtensionsTests
    {
        internal class CustomerValue
        {
            public bool IsValid { get; set; }

            private bool IsPrivateValid { get; set; }

            private int PrivateProperty { get; set; }
            public  int PublicProperty { get; set; }

            private int _privateField;

            internal bool IsInternalValid { get; set; }

            private int NoAccess(int input)
            {
                return input + 10;
            }

            public bool GetIsPrivateValid()
            {
                return IsPrivateValid;
            }

            public void SetPrivateProperty(int value)
            {
                PrivateProperty = value;
            }

            public int GetPrivateProperty()
            {
                return PrivateProperty;
            }

            public void SetPrivateField(int value)
            {
                _privateField = value;
            }

            public int GetPrivateField()
            {
                return _privateField;
            }
        }

        internal CustomerValue Customer { get; set; }

        public ReflectionExtensionsTests()
        {
            Customer = new CustomerValue();
        }

        [Fact]
        public void SetProperty_ValidProperty_ShouldBeSet()
        {
            Customer.SetProperty("IsValid", true);
            Customer.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void SetProperty_NullObject_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ((CustomerValue) null).SetProperty("IsValid", true));
            exception.Message.ShouldContain("target");
        }

        [Fact]
        public void SetProperty_ValidPrivateProperty_ShouldBeSet()
        {
            Customer.SetProperty("IsPrivateValid", true);
            Customer.GetIsPrivateValid().ShouldBeTrue();
        }

        [Fact]
        public void SetProperty_NotValid_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Customer.SetProperty("DoesNotExist", true));
            exception.Message.ShouldContain("DoesNotExist");
        }

        [Fact]
        public void GetInstanceMethod_ValidPrivateMethod_ShouldHaveValue()
        {
            var result = ReflectionExtensions.GetInstanceMethod(Customer, "NoAccess");
            result.ShouldNotBeNull();
        }

        [Fact]
        public void GetInstanceMethod_ValidPrivateMethod_ShouldBeNull()
        {
            var result = ReflectionExtensions.GetInstanceMethod(Customer, "DoesNotExist");
            result.ShouldBeNull();
        }


        [Fact]
        public void InvokeInstanceMethod_ValidPrivateMethod_ShouldExecute()
        {
            var result = ReflectionExtensions.InvokeInstanceMethod(Customer, "NoAccess", 7);
            result.ShouldBe(17);
        }

        [Fact]
        public void InvokeInstanceMethod_InvalidMethod_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => ReflectionExtensions.InvokeInstanceMethod(Customer, "NoAccess1", 7));
            exception.Message.ShouldContain("NoAccess1");
        }

        [Fact]
        public void GetPropertyValue_ValidProperty_ShouldReturnValue()
        {
            Customer.PublicProperty = 114;
            var result = Customer.GetPropertyValue<int>("PublicProperty");
            result.ShouldBe(114);
        }

        [Fact]
        public void GetPropertyValue_NotValid_ShouldReturnDefault()
        {
            var result = Customer.GetPropertyValue<int>("DoesNotExist");
            result.ShouldBe(0);
        }

        [Fact]
        public void GetPropertyValue_NullObject_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ((CustomerValue)null).GetPropertyValue<int>("PublicProperty"));
            exception.Message.ShouldContain("target");
        }

        [Fact]
        public void GetPrivatePropertyValue_ValidProperty_ShouldReturnValue()
        {
            Customer.SetPrivateProperty(104);
            var result = Customer.GetPrivatePropertyValue<int>("PrivateProperty");
            result.ShouldBe(104);
        }

        [Fact]
        public void GetPrivatePropertyValue_NotValid_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Customer.GetPrivatePropertyValue<int>("DoesNotExist"));
            exception.Message.ShouldContain("DoesNotExist");
        }

        [Fact]
        public void GetPrivatePropertyValue_NullObject_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ((CustomerValue)null).GetPrivatePropertyValue<int>("PrivateProperty"));
            exception.Message.ShouldContain("target");
        }

        [Fact]
        public void GetPrivateFieldValue_ValidProperty_ShouldReturnValue()
        {
            Customer.SetPrivateField(77);
            var result = Customer.GetPrivateFieldValue<int>("_privateField");
            result.ShouldBe(77);
        }

        [Fact]
        public void GetPrivateFieldValue_NotValid_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Customer.GetPrivateFieldValue<int>("DoesNotExist"));
            exception.Message.ShouldContain("DoesNotExist");
        }

        [Fact]
        public void GetPrivateFieldValue_NullObject_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ((CustomerValue)null).GetPrivateFieldValue<int>("_privateProperty"));
            exception.Message.ShouldContain("target");
        }

        [Fact]
        public void SetPrivatePropertyValue_ValidProperty_ShouldReturnValue()
        {
            Customer.SetPrivatePropertyValue("PrivateProperty", 7);
            Customer.GetPrivateProperty().ShouldBe(7);
        }

        [Fact]
        public void SetPrivatePropertyValue_NotValid_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Customer.SetPrivatePropertyValue("DoesNotExist", 8));
            exception.Message.ShouldContain("DoesNotExist");
        }

        [Fact]
        public void SetPrivatePropertyValue_NullObject_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ((CustomerValue)null).SetPrivatePropertyValue("PrivateProperty", 73));
            exception.Message.ShouldContain("target");
        }

        [Fact]
        public void SetPrivateFieldValue_ValidProperty_ShouldReturnValue()
        {
            Customer.SetPrivateFieldValue("_privateField", 1437);
            Customer.GetPrivateField().ShouldBe(1437);
        }

        [Fact]
        public void SetPrivateFieldValue_NotValid_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => Customer.SetPrivateFieldValue("DoesNotExist", 8));
            exception.Message.ShouldContain("DoesNotExist");
        }

        [Fact]
        public void SetPrivateFieldValue_NullObject_ShouldThrow()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => ((CustomerValue)null).SetPrivateFieldValue("_privateField", 73));
            exception.Message.ShouldContain("target");
        }
    }
}
