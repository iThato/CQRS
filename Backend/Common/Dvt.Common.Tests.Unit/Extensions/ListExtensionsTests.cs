using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Dvt.Common.Extensions;
using Shouldly;
using Xunit;

// ReSharper disable InconsistentNaming
// ReSharper disable ExpressionIsAlwaysNull

namespace Dvt.Common.Tests.Unit.Extensions
{
    [Trait("Common", "Extensions")]
    public class ListExtensionsTests
    {
        [Fact]
        public void HasItems_ItemsInList_IsTrue()
        {
            var item = new List<string> {"Dog", "Cat"};
            var result = item.HasItems();
            result.ShouldBeTrue();
        }

        [Fact]
        public void HasItems_NoItemsInList_IsFalse()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var item = new List<string>();
            var result = item.HasItems();
            result.ShouldBeFalse();
        }

        [Fact]
        public void HasNoItems_NoItemsInList_IsTrue()
        {
            // ReSharper disable once CollectionNeverUpdated.Local
            var item = new List<string>();
            var result = item.HasNoItems();
            result.ShouldBeTrue();
        }

        [Fact]
        public void HasNoItems_ItemsInList_IsFalse()
        {
            var item = new List<string> {"dog", "cat"};
            var result = item.HasNoItems();
            result.ShouldBeFalse();
        }

        [Fact]
        public void Match_SupplyNullList_IsFalse()
        {
            IList<string> item = null;
            var result = item.Match(null);
            result.ShouldBeFalse();
        }

        [Fact]
        public void Match_SupplyNullPredicate_IsFalse()
        {
            IList<string> item = new List<string>();
            item.Add("dog");
            item.Add("cat");
            item.Add("horse");
            var result = item.Match(null);
            result.ShouldBeFalse();
        }

        [Fact]
        public void Match_SupplyEmptyList_IsFalse()
        {
            IList<string> item = new List<string>();
            var result = item.Match(s => s.StartsWith("c"));
            result.ShouldBeFalse();
        }

        [Fact]
        public void Match_SupplyNonMatchingPredicate_IsFalse()
        {
            IList<string> item = new List<string>();
            item.Add("dog");
            item.Add("cat");
            item.Add("horse");
            var result = item.Match(x => x.StartsWith("9"));
            result.ShouldBeFalse();
        }

        [Fact]
        public void Match_SupplyMatchingPredicate_IsTrue()
        {
            IList<string> item = new List<string>();
            item.Add("dog");
            item.Add("cat");
            item.Add("horse");
            var result = item.Match(x => x.EndsWith("e"));
            result.ShouldBeTrue();
        }

        [Fact]
        public void Concatenate_SupplyNullList_ReturnEmptyString()
        {
            IList<string> item = null;
            var result = item.Concatenate(",", x => x);
            result.ShouldBe(string.Empty);
        }

        [Fact]
        public void Concatenate_SupplyNullDelimiter_ThrowArgumentNullException()
        {
            IList<string> item = new List<string>();
            item.Add("cat");
            item.Add("dog");
            var exception = Assert.Throws<ArgumentNullException>(() => item.Concatenate(null, x => x) == string.Empty);
            exception.Message.ShouldContain("delimiter");
        }

        [Fact]
        public void Concatenate_SupplyNullConverter_ThrowArgumentNullException()
        {
            IList<string> item = new List<string>();
            item.Add("cat");
            item.Add("dog");
            var exception = Assert.Throws<ArgumentNullException>(() => item.Concatenate(",", null) == string.Empty);
            exception.Message.ShouldContain("converter");
        }

        [Fact]
        public void Concatenate_SupplyNoDelimiter_Match()
        {
            IList<string> item = new List<string>();
            item.Add("cat");
            item.Add("dog");
            item.Add("horse");
            var result = item.Concatenate(string.Empty, s => s.Substring(0, 1));
            result.ShouldBe("cdh");
        }

        [Fact]
        public void Concatenate_SupplyItemsAndDelimiter_EnsureLastDelimiterRemoved()
        {
            IList<string> item = new List<string>();
            item.Add("cat");
            item.Add("dog");
            item.Add("horse");
            var result = item.Concatenate(";", s => s.Substring(0, 1));
            result.ShouldBe("c;d;h");
        }

        [Fact]
        public void Concatenate_SupplyEmptyItems_EnsureLastDelimiterRemoved()
        {
            IList<string> item = new List<string>();
            item.Add(string.Empty);
            item.Add(string.Empty);
            item.Add(string.Empty);
            var result = item.Concatenate("|", s => s);
            result.ShouldBe("||");
        }

        [Fact]
        public void ForEach_SupplyKnownValue_EachItemIsInvoked()
        {
            var sum = 0;
            var itemList = new List<int> {1, 2, 3};
            ((IEnumerable<int>)itemList).ForEach(i => sum += i);
            sum.ShouldBe(6);
        }

        [Fact]
        public void ForEach_SupplyNullAction_NoActionInvoked()
        {
            const int sum = 0;
            var itemList = new List<int> { 1, 2, 3 };
            ((IEnumerable<int>) itemList).ForEach(null);
            sum.ShouldBe(0);
        }

        [Fact]
        public void ForEach_SupplyNullItems_NoItemIsInvoked()
        {
            var sum = 0;
            IList<int> itemList = null;
            itemList.ForEach(i => sum += i);
            sum.ShouldBe(0);
        }

        [Fact]
        public void ForEach_SupplyItems_ItemAreInvoked()
        {
            var sum = 0;
            var itemList = new List<int> {1, 2, 3, 4, 5};
            ((IEnumerable<int>)itemList).ForEach(i => sum += i);
            sum.ShouldBe(15);
        }

        [Fact]
        public void EmptyIfNull_SupplyNull_ShouldBeEmpty()
        {
            IEnumerable<string> itemList = null;
            var result = itemList.EmptyIfNull();
            result.ShouldBe(Enumerable.Empty<string>());
        }

        [Fact]
        public void EmptyIfNull_SupplyItems_ReturnsItems()
        {
            IEnumerable<int> itemList = new List<int> {1, 2, 3};
            var result = itemList.EmptyIfNull();
            result.ShouldBe(itemList);
        }

        [Fact]
        public void AddRange_TwoValidLists_ResultIsConcatenatedCorrectly()
        {
            var source = new Collection<int> {1, 2, 3, 4, 5};
            var range = new List<int> {6, 7};
            source.AddRange(range);
            IEnumerable<int> expectedResult = new Collection<int> {1, 2, 3, 4, 5, 6, 7};
            source.ShouldBe(expectedResult);
        }

        [Fact]
        public void AddRange_RangeListIsEmpty_SourceHasNochanges()
        {
            var source = new Collection<int> { 1, 2, 3, 4, 5 };
            // ReSharper disable once CollectionNeverUpdated.Local
            var range = new List<int>();
            source.AddRange(range);
            source.ShouldBe(source);
        }

        [Fact]
        public void AddRange_NullRangeList_NoExceptionThrown()
        {
            var source = new Collection<int> { 1, 2, 3, 4, 5 };
            List<int> range = null;
            source.AddRange(range);
            source.ShouldBe(source);
        }

        [Fact]
        public void AddRange_NullSourceList_NoExceptionThrown()
        {
            Collection<int> source = null;
            var range = new List<int> { 6, 7 };
            source.AddRange(range);
            source.ShouldBeNull();
        }
    }
}

