using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace UnitTests
{
    [TestClass]
    public class UnitTest5
    {
        /// <summary>
        /// imagetests
        /// </summary>
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            Product prod = new Product { ProductID = 2, Name = "Test", ImageData = new byte[] { }, ImageMimeType = "image/png" };

            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1" }, prod, new Product { ProductID = 3, Name = "P3" } }.AsQueryable());

            ProductController target = new ProductController(mock.Object);

            ActionResult result = target.GetImage(2);

            Assert.IsNotNull(result); Assert.IsInstanceOfType(result, typeof(FileResult)); Assert.AreEqual(prod.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>(); mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1" }, new Product { ProductID = 2, Name = "P2" } }.AsQueryable());

            ProductController target = new ProductController(mock.Object);

            ActionResult result = target.GetImage(100);

            Assert.IsNull(result);
        }
    }
}
