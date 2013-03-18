using NUnit.Framework;

namespace SQLite.Tests
{
    using System;

    [TestFixture]
    public class ReferenceTest
    {
        TestDb _db;

        [SetUp]
        public void SetUp()
        {
            _db = new TestDb();
            _db.CreateTable<Product>();
            _db.CreateTable<Order>();
            _db.CreateTable<OrderLine>();

            var p1 = new Product { Name = "One", };
            var p2 = new Product { Name = "Two", };
            var p3 = new Product { Name = "Three", };
            _db.InsertAll(new[] { p1, p2, p3 });

            var o1 = new Order { PlacedTime = DateTime.Now, };
            var o2 = new Order { PlacedTime = DateTime.Now, };
            _db.InsertAll(new[] { o1, o2 });

            _db.InsertAll(new[] {
				new OrderLine {
					OrderId = o1.Id,
					ProductId = p1.Id,
					Quantity = 1,
				},
				new OrderLine {
					OrderId = o1.Id,
					ProductId = p2.Id,
					Quantity = 2,
				},
				new OrderLine {
					OrderId = o2.Id,
					ProductId = p3.Id,
					Quantity = 3,
				},
			});
        }

        [Test]
        public void QueryWithReference()
        {
            var orderLines = _db.Query<OrderLine>(@"
select 
    ol.Id as [Id],
    ol.OrderId as [OrderId],
    ol.ProductId as [ProductId],
    ol.Quantity as [Quantity],
    ol.UnitPrice as [UnitPrice],
    ol.Status as [Status],
    o.Id as [Order.Id],
    o.PlacedTime as [Order.PlacedTime]
from 
    OrderLine as ol
        inner join [Order] as o on ol.OrderId = o.Id
");
            
            Assert.AreEqual(3, orderLines.Count);
            Assert.NotNull(orderLines[0].Order);
            Assert.AreEqual(orderLines[0].Order.Id, orderLines[0].OrderId);
            Assert.NotNull(orderLines[1].Order);
            Assert.AreEqual(orderLines[1].Order.Id, orderLines[1].OrderId);
            Assert.NotNull(orderLines[2].Order);
            Assert.AreEqual(orderLines[2].Order.Id, orderLines[2].OrderId);
        }
    }
}
