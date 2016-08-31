using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using OfferPriceEvaluator.Helpers;
using HtmlAgilityPack;
using System.IO;
using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain.Entities;
using OfferPriceEvaluator.Domain.Concrete;

namespace OfferPriceEvaluator.Specs
{
    [TestClass]
    public class When_New_Alternative_Offers_Need_To_Be_Exctracted : BaseTest
    {
        [TestMethod]
        public void Price_Must_Be_Exctracted()
        {
            var mockHtmlWebWrapper = new Mock<IHtmlWebWrapper>();

            #region MyRegion
            string ebayRam = File.ReadAllText("C://Users//Nick//Desktop//ebayRam.html");
            string amazonRam = File.ReadAllText("C://Users//Nick//Desktop//amazonRam.html");
            string ebayGpu = File.ReadAllText("C://Users//Nick//Desktop//ebayGPU.html");
            string amazonGpu = File.ReadAllText("C://Users//Nick//Desktop//amazonGPU.html");
            string ebayCpu = File.ReadAllText("C://Users//Nick//Desktop//ebayCPU.html");
            string amazonCpu = File.ReadAllText("C://Users//Nick//Desktop//amazonCPU.html");
            string ebayHardDrive = File.ReadAllText("C://Users//Nick//Desktop//ebayHardDrive.html");
            string amazonHardDrive = File.ReadAllText("C://Users//Nick//Desktop//amazonHardDrive.html");

            HtmlDocument docEbayRam = new HtmlDocument();
            HtmlDocument docAmazonRam = new HtmlDocument();
            HtmlDocument docEbayGpu = new HtmlDocument();
            HtmlDocument docAmazonGpu = new HtmlDocument();
            HtmlDocument docEbayCpu = new HtmlDocument();
            HtmlDocument docAmazonCpu = new HtmlDocument();
            HtmlDocument docEbayHardDrive = new HtmlDocument();
            HtmlDocument docAmazonHardDrive = new HtmlDocument();

            docEbayRam.LoadHtml(ebayRam);
            docAmazonRam.LoadHtml(amazonRam);
            docEbayGpu.LoadHtml(ebayGpu);
            docAmazonGpu.LoadHtml(amazonGpu);
            docEbayCpu.LoadHtml(ebayCpu);
            docAmazonCpu.LoadHtml(amazonCpu);
            docEbayHardDrive.LoadHtml(ebayHardDrive);
            docAmazonHardDrive.LoadHtml(amazonHardDrive);

            mockHtmlWebWrapper
                .Setup(x => x.Load("http://www.amazon.com/s/ref=nb_sb_ss_c_0_6?url=search-alias%3Daps&field-keywords=4gb+ram&sprefix=4gb+ra%2Caps%2C944"))
                .Returns(docAmazonRam);
            mockHtmlWebWrapper
                .Setup(x => x.Load("http://www.ebay.com/sch/i.html?_from=R40&_sacat=0&_nkw=4gb+ram&rt=nc&LH_BIN=1"))
                .Returns(docEbayRam);
            mockHtmlWebWrapper
                .Setup(x => x.Load("http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=1gb+GPU&rh=i%3Aaps%2Ck%3A1gb+GPU"))
                .Returns(docAmazonGpu);
            mockHtmlWebWrapper
                .Setup(x => x.Load("http://www.ebay.com/sch/i.html?_odkw=Intel+Core+2+Duo+2.90+GHz&LH_BIN=1&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR0.TRC0.H0.X1gb+GPU.TRS0&_nkw=1gb+GPU&_sacat=0"))
                .Returns(docEbayGpu);
            mockHtmlWebWrapper
                .Setup(x => x.Load("http://www.ebay.com/sch/i.html?_odkw=2gb+ram&LH_ItemCondition=3&LH_BIN=1&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR0.TRC0.H0.XIntel+Core+2+Duo+2.90+GHz.TRS0&_nkw=Intel+Core+2+Duo+2.90+GHz&_sacat=0"))
                .Returns(docEbayCpu);
            mockHtmlWebWrapper
                .Setup(x => x.Load("http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=Intel+Core+2+Duo+&rh=i%3Aaps%2Ck%3AIntel+Core+2+Duo+"))
                .Returns(docAmazonCpu);
            mockHtmlWebWrapper
                .Setup(x => x.Load("http://www.ebay.com/sch/i.html?_odkw=Intel+Core+2+Duo+2.90+GHz&LH_BIN=1&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR0.TRC0.H0.X500+gb+hard+drive.TRS0&_nkw=500+gb+hard+drive&_sacat=0"))
                .Returns(docEbayHardDrive);
            mockHtmlWebWrapper
                .Setup(x => x.Load("http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=500+gb+hard+drive&rh=i%3Aaps%2Ck%3A500+gb+hard+drive"))
                .Returns(docAmazonHardDrive); 
            #endregion

            List<IdAndLinks> links = new List<IdAndLinks>()
            {
                #region MyRegion
		        new IdAndLinks()
                {
                    Id = 1,
                    Links = new List<LinksForPriceComparison>()
                    {
                        new LinksForPriceComparison() {
                            Id = 3,
                            AmazonLink = "http://www.amazon.com/s/ref=nb_sb_ss_c_0_6?url=search-alias%3Daps&field-keywords=4gb+ram&sprefix=4gb+ra%2Caps%2C944",
                            EbayLink ="http://www.ebay.com/sch/i.html?_from=R40&_sacat=0&_nkw=4gb+ram&rt=nc&LH_BIN=1" }
                        ,
                        new LinksForPriceComparison()
                        {
                            Id = 5,
                            AmazonLink ="http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=1gb+GPU&rh=i%3Aaps%2Ck%3A1gb+GPU",
                            EbayLink = "http://www.ebay.com/sch/i.html?_odkw=Intel+Core+2+Duo+2.90+GHz&LH_BIN=1&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR0.TRC0.H0.X1gb+GPU.TRS0&_nkw=1gb+GPU&_sacat=0"
                        },
                        new LinksForPriceComparison() {
                            Id = 1,
                            EbayLink ="http://www.ebay.com/sch/i.html?_odkw=2gb+ram&LH_ItemCondition=3&LH_BIN=1&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR0.TRC0.H0.XIntel+Core+2+Duo+2.90+GHz.TRS0&_nkw=Intel+Core+2+Duo+2.90+GHz&_sacat=0",
                             AmazonLink = "http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=Intel+Core+2+Duo+&rh=i%3Aaps%2Ck%3AIntel+Core+2+Duo+"
                        },
                        new LinksForPriceComparison()
                        {
                            Id=35,
                            EbayLink = "http://www.ebay.com/sch/i.html?_odkw=Intel+Core+2+Duo+2.90+GHz&LH_BIN=1&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR0.TRC0.H0.X500+gb+hard+drive.TRS0&_nkw=500+gb+hard+drive&_sacat=0",
                            AmazonLink ="http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=500+gb+hard+drive&rh=i%3Aaps%2Ck%3A500+gb+hard+drive"
                        }
                    }
                }, new IdAndLinks()
                {
                    Id = 2,
                    Links = new List<LinksForPriceComparison>()
                    {
                        new LinksForPriceComparison() {
                            Id = 3,
                            AmazonLink = "http://www.amazon.com/s/ref=nb_sb_ss_c_0_6?url=search-alias%3Daps&field-keywords=4gb+ram&sprefix=4gb+ra%2Caps%2C944",
                            EbayLink ="http://www.ebay.com/sch/i.html?_from=R40&_sacat=0&_nkw=4gb+ram&rt=nc&LH_BIN=1" }
                        ,
                        new LinksForPriceComparison()
                        {
                            Id = 5,
                            AmazonLink ="http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=1gb+GPU&rh=i%3Aaps%2Ck%3A1gb+GPU",
                            EbayLink = "http://www.ebay.com/sch/i.html?_odkw=Intel+Core+2+Duo+2.90+GHz&LH_BIN=1&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR0.TRC0.H0.X1gb+GPU.TRS0&_nkw=1gb+GPU&_sacat=0"
                        },
                        new LinksForPriceComparison() {
                            Id = 1,
                            EbayLink ="http://www.ebay.com/sch/i.html?_odkw=2gb+ram&LH_ItemCondition=3&LH_BIN=1&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR0.TRC0.H0.XIntel+Core+2+Duo+2.90+GHz.TRS0&_nkw=Intel+Core+2+Duo+2.90+GHz&_sacat=0",
                             AmazonLink = "http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=Intel+Core+2+Duo+&rh=i%3Aaps%2Ck%3AIntel+Core+2+Duo+"
                        },
                        new LinksForPriceComparison()
                        {
                            Id=35,
                            EbayLink = "http://www.ebay.com/sch/i.html?_odkw=Intel+Core+2+Duo+2.90+GHz&LH_BIN=1&_osacat=0&_from=R40&_trksid=p2045573.m570.l1313.TR0.TRC0.H0.X500+gb+hard+drive.TRS0&_nkw=500+gb+hard+drive&_sacat=0",
                            AmazonLink ="http://www.amazon.com/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords=500+gb+hard+drive&rh=i%3Aaps%2Ck%3A500+gb+hard+drive"
                        }
                    }
                } 
	#endregion
            };

            var category = new Category()
            {
                Name = "Desktop კომპიუტერები",
                Id = 1
            };

            var t = new Tag() { Id = 35, Name = "h" };
            var tt = new Tag() { Id = 3, Name = "r" };
            var ttt = new Tag() { Id = 5, Name = "g" };
            var tttt = new Tag() { Id = 1, Name = "c" };

            DbContextOfferPriceEvaluator.Tags.Add(t);
            DbContextOfferPriceEvaluator.Tags.Add(tt);
            DbContextOfferPriceEvaluator.Tags.Add(ttt);
            DbContextOfferPriceEvaluator.Tags.Add(tttt);

            DbContextOfferPriceEvaluator.Categories.Add(category);

            DbContextOfferPriceEvaluator.Items.Add(new Item()
            {
                Category = category,
                Id = 1,
                ExternalId = "8807887",
                ContactNumbers = "43679",
                Currency = "GEL",
                IsUsed = true,
                OfferTitle = "aaa",
                OfferLocation = "tbilisi",
                Price = 880,
                ProductDescription = "hfhtdsxcb"
            });
            DbContextOfferPriceEvaluator.Items.Add(new Item()
            {
                Category = category,
                Id = 2,
                ExternalId = "8807889",
                ContactNumbers = "43679",
                Currency = "GEL",
                IsUsed = true,
                OfferTitle = "akaa",
                OfferLocation = "tbilisi",
                Price = 980,
                ProductDescription = "hfhtdsxcb"
            });

            BaseRepository<Item> ii = new ItemRepository(DbContextOfferPriceEvaluator);
            BaseRepository<AlternativePriceItemTag> vvb = new AlternativePriceItemTagRepository(DbContextOfferPriceEvaluator);
            BaseRepository<Tag> tg = new TagRepository(DbContextOfferPriceEvaluator);
            BaseRepository<Seller> sl = new SellerRepository(DbContextOfferPriceEvaluator);

            AlternativeOfferExctractor aox = new AlternativeOfferExctractor(mockHtmlWebWrapper.Object, vvb, ii,sl,tg);
            aox.GetAlternativeOffers(links);


        }
    }
}
