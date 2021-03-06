﻿using FluentAssertions.Primitives;
using Infrastructure.Components;
using FluentAssertions;

namespace Assertions
{
    public class ProductRowAssertions : ReferenceTypeAssertions<ProductRow, ProductRowAssertions>
    {
        public ProductRowAssertions(ProductRow instance)
        {
            Subject = instance;
        }

        protected override string Identifier => "CartPageAssertions";

        public AndConstraint<ProductRow> TotalPriceIsTrue()
        {
            //double totalPrice = Subject.TotalPrice;
            //double totalPriceCalculate = (Subject.Quantity.GetQuantity() * Subject.UnitPrice);

            Subject.TotalPrice
                .Should()
                .Be(Subject.Quantity.GetQuantity() * Subject.UnitPrice,
                "Total Price is not equal to the quantity multiple unit price");
            //Execute
            //    .Assertion
            //    .ForCondition(totalPrice == totalPriceCalculate)
            //    .FailWith("Total Price is not equal to the quantity multiple unit price");

            return new AndConstraint<ProductRow>(Subject);
        }
    }
}
