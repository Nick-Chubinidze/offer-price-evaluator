using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfferPriceEvaluator.Domain.Entities;
using OfferPriceEvaluator.Domain;
using System.Data.Entity;
using OfferPriceEvaluator.Domain.Abstract;
using Moq;
using System.Globalization;
using System.Threading;

namespace OfferPriceEvaluator.Specs
{
    [TestClass]
    public class BaseTest
    {
        protected IDbContextOfferPriceEvaluator DbContextOfferPriceEvaluator;
        protected IDbSet<Item> _items;
        protected IDbSet<Category> _categories;
        protected IDbSet<ItemTagValue> _itemTagValues;
        protected IDbSet<Tag> _tags;
        protected IDbSet<TagGroup> _tagGroups;
        protected IDbSet<TagToTagGroup> _tagToTagGroups;
        protected IDbSet<Seller> _sellers;
        protected IDbSet<AlternativePriceItemTag> _alternativePriceItemTags;

        [TestInitialize]
        public void Init()
        {
            _items = new FakeDbSet<Item>();
            _categories = new FakeDbSet<Category>();
            _itemTagValues = new FakeDbSet<ItemTagValue>();
            _tags = new FakeDbSet<Tag>();
            _tagGroups = new FakeDbSet<TagGroup>();
            _tagToTagGroups = new FakeDbSet<TagToTagGroup>();
            _sellers = new FakeDbSet<Seller>();
            _alternativePriceItemTags = new FakeDbSet<AlternativePriceItemTag>();

            CultureInfo culture = new System.Globalization.CultureInfo("en-US")
            {
                DateTimeFormat = { ShortDatePattern = "dd/MM/yy" }
            };
            Thread.CurrentThread.CurrentCulture = culture;


            var offerPriceEvaluatorContext = new Mock<IDbContextOfferPriceEvaluator>();
            offerPriceEvaluatorContext.SetupGet(x => x.Items).Returns(_items);
            offerPriceEvaluatorContext.SetupGet(x => x.Categories).Returns(_categories);
            offerPriceEvaluatorContext.SetupGet(x => x.ItemTagValues).Returns(_itemTagValues);
            offerPriceEvaluatorContext.SetupGet(x => x.Tags).Returns(_tags);
            offerPriceEvaluatorContext.SetupGet(x => x.TagGroups).Returns(_tagGroups);
            offerPriceEvaluatorContext.SetupGet(x => x.TagToTagGroups).Returns(_tagToTagGroups);
            offerPriceEvaluatorContext.SetupGet(x => x.AlternativePriceItemTags).Returns(_alternativePriceItemTags);
            offerPriceEvaluatorContext.SetupGet(x => x.Sellers).Returns(_sellers);

            offerPriceEvaluatorContext.Setup(x => x.Set<Item>()).Returns(_items);
            offerPriceEvaluatorContext.Setup(x => x.Set<Category>()).Returns(_categories);
            offerPriceEvaluatorContext.Setup(x => x.Set<Tag>()).Returns(_tags);
            offerPriceEvaluatorContext.Setup(x => x.Set<Seller>()).Returns(_sellers);
            offerPriceEvaluatorContext.Setup(x => x.Set<AlternativePriceItemTag>()).Returns(_alternativePriceItemTags);
            offerPriceEvaluatorContext.Setup(x => x.Set<TagGroup>()).Returns(_tagGroups);
            offerPriceEvaluatorContext.Setup(x => x.Set<TagToTagGroup>()).Returns(_tagToTagGroups);
            offerPriceEvaluatorContext.Setup(x => x.Set<ItemTagValue>()).Returns(_itemTagValues);

            DbContextOfferPriceEvaluator = offerPriceEvaluatorContext.Object; 

            //ItemRepository ir = new ItemRepository(DbContextOfferPriceEvaluator);

            //ir.Fetch(3);

            //var category = new Category() { Name = "Desktop კომპიუტერები", Id = 1 };
            //var itvs = new List<ItemTagValue>() { new ItemTagValue { ItemID = 1 } };
            //var itv = new ItemTagValue() { };

            //var listOfItems = new List<Item>
            //{
            //    new Item() {
            //        Category = category,
            //        Id = 7,
            //        ExternalId ="10",
            //        ContactNumbers ="43679",
            //        Currency ="GEL",
            //        IsUsed =false,
            //        OfferTitle ="bbb",
            //        OfferLocation ="tbilisi",
            //         Price=750,
            //          ProductDescription="hfhtdsxcb"
            //    },
            //    new Item() {
            //        Category = category,
            //        Id =2,
            //        ExternalId ="20" ,
            //        ContactNumbers ="43679",
            //        Currency ="GEL",
            //        IsUsed =true,
            //        OfferTitle ="aaa",
            //        OfferLocation ="tbilisi",
            //         Price=340,
            //          ProductDescription="hfhtdsxcb"
            //    },
            //    new Item() {
            //        Category = category,
            //        Id =3,
            //        ExternalId ="25",
            //        ContactNumbers ="43679",
            //        Currency ="USD",
            //        IsUsed =false,
            //        OfferTitle ="bbb",
            //        OfferLocation ="tbilisi",
            //         Price=550,
            //          ProductDescription="hfhtdsxcb"
            //    },
            //    new Item() {
            //        Category = category,
            //        Id =4,
            //        ExternalId ="30",
            //        ContactNumbers ="43679",
            //        Currency ="GEL",
            //        IsUsed =true,
            //        OfferTitle ="lll",
            //    OfferLocation ="rustavi",
            //         Price=550,
            //          ProductDescription="hfhtdsxcb"
            //    },
            //    new Item() {
            //        Category = category,
            //        Id =5,
            //        ExternalId ="40",
            //        ContactNumbers ="43679",
            //        Currency ="USD",
            //        IsUsed =false,
            //        OfferTitle ="nmn",
            //        OfferLocation ="kutaisi",
            //         Price=750,
            //          ProductDescription="hfhtdsxcb"
            //    },
            //    new Item() {
            //        Category = category,
            //        Id =6,
            //        ExternalId ="60" ,
            //        ContactNumbers ="43679",
            //        Currency ="USD",
            //        IsUsed =true,
            //        OfferTitle ="uytr",
            //        OfferLocation ="tbilisi",
            //         Price=120,
            //          ProductDescription="hfhtdsxcb"
            //    },
            //    new Item() {
            //        Category = category,
            //        Id =7,ExternalId ="80",
            //        ContactNumbers ="43679",
            //        Currency ="GEL",
            //        IsUsed =true,
            //        OfferTitle ="aaa",
            //        OfferLocation ="tbilisi",
            //         Price=880,
            //          ProductDescription="hfhtdsxcb"
            //    }
            //};

            //var linkGenerator = new AlternativeOfferLinkGenerator();

            //linkGenerator.SearchToComparePrice(listOfItems);

            //linkGenerator
            //    .grouper
            //    .Count
            //    .ShouldBe(1);
        }

        [TestMethod]
        public void Returned_Links_Should_()
        {
        }
    }
}
