using System; 
using System.IO; 
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq; 
using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain.Entities;
using OfferPriceEvaluator.Helpers;
using System.Collections.Generic;
using OfferPriceEvaluator.Domain.Concrete;
using Shouldly;

namespace OfferPriceEvaluator.Specs
{
    [TestClass]
    public class When_New_Offers_Exctracted_With_Defected_Information : BaseTest
    {
        [TestMethod]
        public void Such_Offer_Id_Should_Be_Ignored()
        {
            var mockHtmlWebWrapper = new Mock<IHtmlWebWrapper>();
            var mockWebClientWrapper = new Mock<IWebClientWrapper>();

            string data1 = File.ReadAllText("C://Users//Nick//Desktop//view-source_www.mymarket.ge_ka_product_view_4233928_.html");
            string data2 = File.ReadAllText("C://Users//Nick//Desktop//view-source_www.mymarket.ge_ka_product_view_8810643_.html");

            HtmlDocument doc1 = new HtmlDocument();
            HtmlDocument doc2 = new HtmlDocument();

            doc1.LoadHtml(data1);
            doc2.LoadHtml(data2);

            var category = new Category()
            {
                Name = "Desktop კომპიუტერები",
                Id = 1
            };

            DbContextOfferPriceEvaluator.Categories.Add(category);

            DbContextOfferPriceEvaluator.Items.Add(new Item()
            {
                Category = category,
                Id = 7,
                ExternalId = "8807887",
                ContactNumbers = "43679",
                Currency = "GEL",
                IsUsed = true,
                OfferTitle = "aaa",
                OfferLocation = "tbilisi",
                Price = 880,
                ProductDescription = "hfhtdsxcb"
            });

            List<IdAndDate> date = new List<IdAndDate>() {
                new IdAndDate()
            {
                Id = "8807887",
                Date = Convert.ToDateTime("19/01/16 14:16")
            },new IdAndDate()
            {
                Id = "8807886",
                Date = Convert.ToDateTime("19/01/16 14:16")
            }
            }; 

            mockHtmlWebWrapper.Setup(x => x.Load("http://www.mymarket.ge/ka/product/view/8807887/")).Returns(doc1);
            mockHtmlWebWrapper.Setup(x => x.Load("http://www.mymarket.ge/ka/product/view/8807886/")).Returns(doc2);

            mockWebClientWrapper.Setup(x => x.DownloadData(It.IsAny<string>())).Returns(new byte[400]);

            BaseRepository<Item> vvb = new ItemRepository(DbContextOfferPriceEvaluator);
            BaseRepository<Category> m = new CategoryRepository(DbContextOfferPriceEvaluator);
            BaseRepository<Tag> bm = new TagRepository(DbContextOfferPriceEvaluator);

            OfferExctractor ox = new OfferExctractor(mockHtmlWebWrapper.Object, mockWebClientWrapper.Object, vvb, m, bm);
            //ox.GetProperties(date).Count.ShouldBe(1);
        }
    }
}
