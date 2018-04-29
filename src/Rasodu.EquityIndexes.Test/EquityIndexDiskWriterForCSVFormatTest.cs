﻿using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Rasodu.EquityIndexes.Test
{
    public class EquityIndexDiskWriterForCSVFormatTest
    {
        [Fact]
        public void ReplaceAllTest()
        {
            //arrange
            var expectedCSVText = "StockExchange,Identifier\nNYSE,AXP\nNYSE,MMM\n";
            var expectedEquityList = new List<Equity>()
            {
                new Equity
                {
                    StockExchange = "NYSE",
                    Identifier = "AXP",
                },
                new Equity
                {
                    StockExchange = "NYSE",
                    Identifier = "MMM",
                }
            };
            TextWriter writer = new StringWriter();
            var equityStore = new EquityIndexDiskWriterForCSVFormat(writer);
            //act
            equityStore.ReplaceAll(expectedEquityList);
            var actualCSVText = writer.ToString();
            //assert
            Assert.Equal(expectedCSVText, actualCSVText);
        }
    }
}