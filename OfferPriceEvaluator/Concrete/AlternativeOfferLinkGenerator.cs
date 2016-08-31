using System;
using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain.Entities;
using OfferPriceEvaluator.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace OfferPriceEvaluator
{
    public class AlternativeOfferLinkGenerator : IAlternativeOfferLinkGenerator
    {
        private List<IdAndValue> _grouper;

        private List<IdAndLinks> _searchLinks;

        private List<LinksForPriceComparison> _links;


        private readonly BaseRepository<ItemTagValue> _itemTagValueRepository;
        private readonly BaseRepository<Category> _categoryRepository;
        private readonly BaseRepository<Tag> _tagRepository;
        private readonly BaseRepository<TagToTagGroup> _tagToTagGroupRepository;

        public AlternativeOfferLinkGenerator(
            BaseRepository<ItemTagValue> itemTagValueRepository,
            BaseRepository<Category> categoryRepository,
            BaseRepository<Tag> tagRepository,
            BaseRepository<TagToTagGroup> tagToTagGroupRepository)
        {
            _itemTagValueRepository = itemTagValueRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _tagToTagGroupRepository = tagToTagGroupRepository;
        }

        public List<IdAndLinks>  SearchToComparePrice(List<Item> xe)
        { 
                StringBuilder groupedSearchValue = new StringBuilder();
                var searchValues = new List<string>();

                _grouper = new List<IdAndValue>();
                _searchLinks = new List<IdAndLinks>();
                _links = new List<LinksForPriceComparison>();

                var a = from i in xe
                        join iq in _categoryRepository.Set() on i.Category.Id equals iq.Id
                        join iv in _itemTagValueRepository.Set() on i.Id equals iv.ItemID
                        join ia in _tagRepository.Set() on iv.TagID equals ia.Id
                        where iq.Id == 1
                        select new
                        {
                            itemId = i.Id,
                            i.ContactNumbers,
                            i.Currency,
                            i.ExternalId,
                            i.IsUsed,
                            i.OfferLocation,
                            i.OfferTitle,
                            i.Price,
                            i.ProductDescription,
                            i.PublishedOn,
                            i.Url,
                            iv.Value,
                            ia.Name,
                            ia.Id,
                            ia.NameForSearch
                        };

                var sortedItems = a.ToList();

                for (int i = 0; i < sortedItems.Count; i++)
                {
                    int tagId = sortedItems[i].Id;

                    var tagToTagGroup = _tagToTagGroupRepository.Set().FirstOrDefault(x => x.TagId.Id == tagId);

                    if ((i + 1) < sortedItems.Count && sortedItems[i].ExternalId == sortedItems[i + 1].ExternalId)
                        Linker(tagToTagGroup, sortedItems[i].Value, sortedItems[i].NameForSearch, sortedItems[i].IsUsed, tagId);

                    else
                    {
                        Linker(tagToTagGroup, sortedItems[i].Value, sortedItems[i].NameForSearch, sortedItems[i].IsUsed, tagId);
                        _grouper.OrderByDescending(x => x.Id);

                        for (int j = 0; j < _grouper.Count; j++)
                        {
                            if (((j + 1) < _grouper.Count && _grouper[j].Id == _grouper[j + 1].Id) ||
                                ((j - 1) >= 0 && _grouper[j].Id == _grouper[j - 1].Id))
                            {
                                if (groupedSearchValue.Length == 0) groupedSearchValue.Append(sortedItems[j].Id + "|");

                                groupedSearchValue.Append(_grouper[j].Value + "+");
                            }


                            else
                            {
                                Linker(null, _grouper[j].Value, null, sortedItems[i].IsUsed, sortedItems[j].Id);

                                if (!string.IsNullOrEmpty(groupedSearchValue.ToString()))
                                {
                                    searchValues.Add(groupedSearchValue.ToString().TrimEnd('+'));
                                    groupedSearchValue.Clear();
                                }
                            }
                        }

                        foreach (var val in searchValues)
                        {
                            Linker(null, val.Substring(val.IndexOf("|", StringComparison.Ordinal) + 1), null, sortedItems[i].IsUsed, Convert.ToInt32(val.Substring(0, val.IndexOf("|"))));
                        }

                        _searchLinks.Add(new IdAndLinks
                        {
                            Id = sortedItems[i].itemId,
                            Links = _links
                        });

                        searchValues.Clear();
                        _grouper.Clear();

                        _links = new List<LinksForPriceComparison>();
                    }
                }

                return _searchLinks; 
        }

        private void Linker(TagToTagGroup tag2TagGroup, string searchValue, string searchName, bool condition, int tagId)
        {
            if (searchName != null && searchName.StartsWith("-")) return;

            var conditionInt = condition ? 4 : 3;

            string searchString = (searchValue + searchName).Replace(" ", "+");

            if (tag2TagGroup != null)
            {
                IdAndValue iav = new IdAndValue()
                {
                    Id = tag2TagGroup.TagGroupId.id,
                    Value = searchString
                };

                _grouper.Add(iav);
            }
            else
            {
                _links.Add(new LinksForPriceComparison
                {
                    Id = tagId,
                    AmazonLink = $"http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias%3Daps&field-keywords={searchString}",
                    EbayLink = $"http://www.ebay.com/sch/i.html?_from=R40&_sacat=0&LH_BIN=1&_nkw={searchString}&=&rt=nc&LH_ItemCondition={conditionInt}"
                });
            }
        }
    }
}
