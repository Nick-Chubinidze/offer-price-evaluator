using HtmlAgilityPack;
using OfferPriceEvaluator.Domain.Entities;
using OfferPriceEvaluator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferPriceEvaluator
{
    public interface IOfferExctactor
    {
        List<IdAndDate> FindOfferId(string src, string id);

        List<IdAndDate> GetPageHtml(string id, int pages = 0);

        string GetPageRequest(string id, int pages = 0);

        bool SaveDates(List<HtmlNode> x, bool isVip, DateTime date);

        List<Item> GetProperties(List<IdAndDate> x);
    }
}
