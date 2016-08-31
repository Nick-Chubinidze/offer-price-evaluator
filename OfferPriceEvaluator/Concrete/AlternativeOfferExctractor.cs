using System;
using System.Collections.Generic;
using System.Linq;
using OfferPriceEvaluator.Helpers;
using OfferPriceEvaluator.Abstract;
using OfferPriceEvaluator.Domain.Entities;
using OfferPriceEvaluator.Domain.Abstract;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OfferPriceEvaluator
{
    public class AlternativeOfferExctractor : IAlternativeOfferExctractor
    {
        private readonly IHtmlWebWrapper _htmlWebWrapper;
        private readonly BaseRepository<AlternativePriceItemTag> _alternativePriceItemTagRepository;
        private readonly BaseRepository<Seller> _sellerRepository;
        private readonly BaseRepository<Item> _itemRepository;
        private readonly BaseRepository<Tag> _tagRepository;
        //private readonly IWebClientWrapper _iWebClientWrapper;

        public AlternativeOfferExctractor(
            IHtmlWebWrapper htmlWebWrapper,
            BaseRepository<AlternativePriceItemTag> alternativePriceItemTagRepository,
            BaseRepository<Item> itemRepository,
            BaseRepository<Seller> sellerRepository,
            BaseRepository<Tag> tagRepository
            /*IWebClientWrapper iWebClientWrapper*/)
        {
            _htmlWebWrapper = htmlWebWrapper;
            _alternativePriceItemTagRepository = alternativePriceItemTagRepository;
            _itemRepository = itemRepository;
            _sellerRepository = sellerRepository;
            _tagRepository = tagRepository;
            //_iWebClientWrapper = iWebClientWrapper;
        }

        public void GetAlternativeOffers(List<IdAndLinks> links)
        { 
               foreach (var idAndLink in links)
               {
                   foreach (var link in idAndLink.Links)
                   {
                       GetOffersFromEbay(link.EbayLink, idAndLink.Id, link.Id);
                        //GetOffersFromAmazon(link.AmazonLink, idAndLink.Id, link.Id);
                    }
               } 
        }

        private void GetOffersFromEbay(string ebayLink, int itemId, int tagId)
        {
            var item = _itemRepository.Fetch(itemId);
            var tag = _tagRepository.Fetch(tagId);
            var seller = _sellerRepository.Fetch(1);

            var ebayHtmlNode = _htmlWebWrapper
                .Load(ebayLink)
                .DocumentNode.Descendants()
                .FirstOrDefault(n => n.GetAttributeValue("class", "").Equals("clearfix"));

            if (ebayHtmlNode == null) return;

            var prices = ebayHtmlNode.Descendants().SelectMany(s => s.Descendants("li"))
                .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lvprice prc")).Take(5).ToArray();

            var linkss = ebayHtmlNode.Descendants().SelectMany(s => s.Descendants("h3"))
                .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lvtitle")).Take(5)
                .SelectMany(s => s.Descendants("a")).Where(x => x.Attributes.Contains("href")).ToArray();

            if (prices.Length<1) return; 

            for (int i = 0; i < 5; i++)
            { 
                decimal price;
                if (!prices[i].InnerText.Contains("&nbsp") && !prices[i].InnerText.Contains("Trending at") &&
                    !prices[i].InnerText.Contains("to"))
                    price = Convert.ToDecimal(Regex.Replace(prices[i].InnerText, @"[^\d.]", ""));
                else
                {
                    var c = Regex.Replace(prices[i].InnerText, @"[^\d.]", "");
                    price = Convert.ToDecimal(c.Substring(0, c.IndexOf('.') + 3));
                }

                var apit = new AlternativePriceItemTag()
                {
                    Item = item,
                    Tag = tag,
                    Seller = seller,
                    Link = linkss[i].Attributes["href"].Value,
                    Price = price,
                    Currency = "USD"
                };

                _alternativePriceItemTagRepository.Save(apit);
            }
        }

        private void GetOffersFromAmazon(string amazonLink, string itemId, int tagId)
        {
            //var amazonHtmlNode = _htmlWebWrapper.Load(amazonLink).DocumentNode.Descendants("div")
            //    .Where(m => m.GetAttributeValue("class", "").Equals("a-column a-span7")).Take(5);


            //foreach (var v in amazonHtmlNode)
            //{
            //    Console.WriteLine(v.FirstChild.InnerText, v.FirstChild.FirstChild.Attributes["href"].Value);
            //} 
        }
    }
}
