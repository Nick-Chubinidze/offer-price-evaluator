using HtmlAgilityPack;
using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain.Entities;
using OfferPriceEvaluator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OfferPriceEvaluator
{
    public class OfferExctractor : IOfferExctactor
    {
        private readonly Item _lastItem;
        private readonly List<IdAndDate> _xDates = new List<IdAndDate>();
        private int _page = 1;
        private readonly BaseRepository<Item> _itemRepository;
        private readonly BaseRepository<Category> _categoryRepository;
        private readonly BaseRepository<Tag> _tagRepository;
        private readonly IHtmlWebWrapper _htmlWebWrapper;
        private readonly IWebClientWrapper _iWebClientWrapper;

        public OfferExctractor(
            //IDbContextOfferPriceEvaluator dbContextOfferPriceEvaluator,
            IHtmlWebWrapper htmlWebWrapper,
            IWebClientWrapper iWebClientWrapper,
            BaseRepository<Item> itemRepository,
            BaseRepository<Category> categoryRepository,
            BaseRepository<Tag> tagRepository)
        {
            _htmlWebWrapper = htmlWebWrapper;
            _iWebClientWrapper = iWebClientWrapper;
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;

            _lastItem = _itemRepository.Set().OrderByDescending(x => x.PublishedOn).First();
        }

        public List<IdAndDate> FindOfferId(string src, string id)
        {
            _page++;
            HtmlDocument document = _htmlWebWrapper.Load(src);

            var root = document.DocumentNode.Descendants("ul")
                .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("pr-search-list row"))
                .SelectMany(s => s.Descendants("li"));

            Func<string, List<HtmlNode>> af = inputSTring =>
                root
               .Where(a => a.Attributes.Contains("class") && a.Attributes["class"].Value.Contains(inputSTring))
               .SelectMany(q => q.Descendants("span"))
               .Where(a => a.ParentNode.Attributes.Contains("class") && a.ParentNode.Attributes["class"].Value.Contains("b-16")).ToList();

            if (SaveDates(af("vip-20"), true, _lastItem.PublishedOn)
                || SaveDates(af("vip-15"), true, _lastItem.PublishedOn)
                || SaveDates(af("vip-10"), true, _lastItem.PublishedOn)
                || SaveDates(af("vip-0"), false, _lastItem.PublishedOn))
            {
                _page = 1;
                return _xDates.ToList();
            }

            return GetPageHtml(id, _page);
        }

        public List<IdAndDate> GetPageHtml(string id, int pages = 0)
        {
            return FindOfferId( GetPageRequest(id, pages), id);
        }

        public string GetPageRequest(string id, int pages = 0)
        {
            if (pages != 0) id = $"{id}&SetTypeID=1&Page={_page}";
            else if (id.Length < 4) id = $"{id}&SetTypeID=1";

            return (!Regex.IsMatch(id, @"^\d+$")) ? $"http://www.mymarket.ge/ka/search/?CatID={id}" : $"http://www.mymarket.ge/ka/product/view/{id}/"; 
        }

        public List<Item> GetProperties(List<IdAndDate> x)
        {
            var items = new List<Item>();

            foreach (var v in x)
            {
                var img = new List<byte[]>();
                Item currentItem;

                var pageUrl = GetPageRequest(v.Id);

                var document = _htmlWebWrapper.Load(pageUrl);

                var root = document.DocumentNode.Descendants()
                    .FirstOrDefault(n => n.GetAttributeValue("class", "").Equals("pr-view"));

                var contactInfo = root.Descendants()
                                    .Where(n => n.GetAttributeValue("class", "").Equals("pr-view-descr-info"))
                                    .SelectMany(s => s.Descendants("span")).ToArray();

                if (contactInfo.FirstOrDefault(f => string.IsNullOrEmpty(f.InnerText)) != null)
                {
                    continue;
                }

                var categoryName = root.Descendants()
                                    .Where(n => n.GetAttributeValue("class", "").Equals("pr-view-cats"))
                                    .SelectMany(s => s.Descendants("a")).Last().InnerText;

                var currentCategory = _categoryRepository.Set().FirstOrDefault(q => q.Name == categoryName) ??
                    new Category { Name = categoryName };

                try
                {
                    var images = root.Descendants()
                        .FirstOrDefault(n => n.GetAttributeValue("class", "").Equals("pr-view-imgs clear-after"))
                        ?.Descendants("li");

                    var title = root.Descendants()
                                        .FirstOrDefault(n => n.GetAttributeValue("class", "").Equals("pr-view-right-block"))
                                        .Descendants("H1").First().InnerText;

                    var description = root.Descendants()
                        .FirstOrDefault(n => n.GetAttributeValue("class", "").Equals("pr-descr pr-view-descr")).InnerText;

                    var price = root
                        .Descendants("p").First(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("pr-view-price ")).InnerText;

                    var condition = root.Descendants("table")
                        .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("pr-view-attrs"))
                        .SelectMany(s => s.Descendants("td")).Skip(1)
                        .First().InnerText;

                    var attributes = root.Descendants("table")
                        .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("pr-view-attrs"))
                        .Skip(1)
                        .SelectMany(s => s.Descendants("td")).ToArray();

                    if (title.Contains("შევიძენ") || title.Contains("შევისყიდი") || title.Contains("ვიყიდი")
                        || title.Contains("ბარებ") || title.Contains("ქირავ") || title.Contains("შევაკეთებ")
                        || title.Contains("იცვლება") || title.Contains("გავცვლი") || title.Contains("საჩუქარი")
                        || title.Contains("აჩუქე") || title.Contains("კომპიუტერებ") || title.Contains("არჩევანი")) continue;

                    if (images != null)
                    {
                        foreach (var imageData in images.Select(item => item.OuterHtml.Split('"')).Where(imageData => imageData[1].Contains("mym")))
                        {
                            try
                            {
                                byte[] imageBytes = _iWebClientWrapper.DownloadData(imageData[1]);
                                img.Add(imageBytes);
                                _iWebClientWrapper.Dispose();
                            }
                            catch (WebException)
                            {
                                //ignore
                            }
                        }
                    }

                    decimal currentPrice = price == "ფასი შეთანხმებით" ? 0 : Convert.ToInt32(Regex.Replace(price, @"[^\d]", ""));

                    string currency;
                    if (price.Contains('')) currency = "GEL";
                    else if (price.Contains('$')) currency = "USD";
                    else currency = currentPrice == 0 ? null : "EUR";

                    currentItem = new Item()
                    {
                        ContactNumbers = contactInfo[0].InnerText,
                        ExternalId = contactInfo[3].InnerText,
                        PublishedOn = v.Date,
                        OfferLocation = contactInfo[6].InnerText,
                        OfferTitle = title,
                        ProductDescription = description,
                        Url = pageUrl,
                        Image = img,
                        IsUsed = condition == "მეორადი",
                        Price = currentPrice,
                        Category = currentCategory,
                        Currency = currency
                    };

                    for (int i = 0; i < attributes.Length; i++)
                    {
                        if (i % 2 == 0) continue;

                        var attributeName = attributes[i - 1].InnerText;
                        var tag =
                            _tagRepository.Set().FirstOrDefault(q => q.Name == attributeName) ??
                            new Tag
                            {
                                Name = attributeName
                            };

                        var itv = new ItemTagValue
                        {
                            Items = currentItem,
                            Tags = tag,
                            Value = attributes[i].InnerHtml
                        };
                        currentItem.ItemTagValues.Add(itv);
                    }
                }
                catch (Exception)
                {
                    continue;
                }

                items.Add(currentItem);
                _itemRepository.Save(currentItem);
            }

            return items;
        }

        public bool SaveDates(List<HtmlNode> x, bool isVip, DateTime lastOfferdate /*string date = "19/01/16 14:16"*/)
        {
            //var w = Convert.ToDateTime(date);
            for (int i = 1; i < x.Count; i += 3)
            {
                var currentOfferDate = Convert.ToDateTime(x[i].InnerText);

                if (currentOfferDate < lastOfferdate && !isVip)
                {
                    _page = 1;
                    return true;
                }
                else if (
                    currentOfferDate < lastOfferdate && isVip
                    || _itemRepository.Set().FirstOrDefault(u => u.ExternalId == x[i - 1].InnerText) != null
                    || _xDates?.FirstOrDefault(u => u.Id == x[i - 1].InnerText) != null) continue;
                else {
                    var ww = new IdAndDate
                    {
                        Id = x[i - 1].InnerText,
                        Date = currentOfferDate
                    };

                    _xDates.Add(ww);
                }
            }
            return false;
        }
    }
}
