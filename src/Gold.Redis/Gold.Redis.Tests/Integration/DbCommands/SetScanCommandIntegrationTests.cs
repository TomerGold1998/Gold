﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Gold.Redis.Tests.Integration.DbCommands
{
    [TestFixture(Category = "SetScan")]
    public class SetScanCommandIntegrationTests : RedisDataBaseClientIntegrationTestsBase
    {
        [Test]
        public async Task SetScan_ShouldReturnAllItemsOfTheSet_WhenNoPatternApplied()
        {
            //Arrange
            var setKey = Guid.NewGuid().ToString();
            var setItems = Enumerable.Range(1, 1000).ToList();
            await _client.SetAddMultiple(setKey, setItems);


            //Act
            var items = (await _client.SetScan<int>(setKey, null, 400)).ToList();

            //Assert
            items.Count().Should().Be(setItems.Count);
            items.Should().BeEquivalentTo(setItems);
        }

        [TestCase("6Empire6", 666)]
        [TestCase("SomePattern", 43)]
        [Test]
        public async Task SetScan_ShouldReturnItemsThatBeginsInPattern_WhenPatternApplied(string pattern, int numberOfPatternItems)
        {
            //Arrange
            var setKey = Guid.NewGuid().ToString();
            var setItems = GetRandomizedSetItemsWithPattern(numberOfPatternItems, 10000, pattern);
            await _client.SetAddMultiple(setKey, setItems);

            //Act
            var items = await _client.SetScan<string>(setKey, "*" + pattern + "*", 1000);

            //Assert
            items.Count().Should().Be(numberOfPatternItems);
        }

        private List<string> GetRandomizedSetItemsWithPattern(int numberOfPatternedItems, int totalItems, string pattern)
        {
            var random = new Random();
            var randomGuidItems = Enumerable.Range(1, totalItems)
                .Select(item => Guid.NewGuid().ToString()).ToList();

            //Can cause a problem but works well
            var randomLocations = Enumerable.Range(0, totalItems).OrderBy(x => random.Next()).Take(numberOfPatternedItems);

            foreach (var randomLocation in randomLocations)
            {
                var str = randomGuidItems[randomLocation];
                str = str.Insert(random.Next(1, str.Length - 2), pattern);
                randomGuidItems[randomLocation] = str;
            }

            return randomGuidItems;
        }

    }
}