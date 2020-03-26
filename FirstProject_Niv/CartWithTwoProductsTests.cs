﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Utils;
using Extensions;
using FluentAssertions;
using System.Threading;

namespace FirstProject_Niv
{
    [TestClass]
    public class CartWithTwoProductsTests : BaseCartTest
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            HomePage = ProductPane.ContinueShoppingClickInHome();
            CartPage = AddToCartFromHome
                        .LastProduct(HomePage)
                        .ProceedToCheckoutClick();
        }

        [TestMethod]
        public void RemoveProductSuccess()
        {
            string itemName = CartPage.GetFirstProductRow().ProductDescription.ProductNameButton.GetText();
            int countOfProducts = CartPage.CartTable.ProductRows.Count();

            CartPage = CartPage
                .GetFirstProductRow()
                .DeleteClick();

            CartPage
                .CartTable
                .ProductRows
                .ToList()
                .ForEach(productRow =>
                            productRow
                            .ProductDescription.ProductNameButton
                            .GetText()
                            .Should()
                            .NotBe(itemName));

            CartPage
                .CartTable
                .ProductRows
                .Count()
                .Should()
                .Be((countOfProducts - 1));
        }
    }
}
