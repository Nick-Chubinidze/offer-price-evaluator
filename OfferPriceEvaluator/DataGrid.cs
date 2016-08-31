using OfferPriceEvaluator.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OfferPriceEvaluator.Abstract;

namespace OfferPriceEvaluator
{
    public partial class DataGrid : Form
    {
        readonly IOfferExctactor _iOfferExctactor;
        readonly IAlternativeOfferLinkGenerator _iAlternativeOfferLinkGenerator;
        readonly IAlternativeOfferExctractor _iAlternativeOfferExctractor;

        public DataGrid(
            IOfferExctactor iOfferExctactor,
            IAlternativeOfferLinkGenerator iAlternativeOfferLinkGenerator,
            IAlternativeOfferExctractor iAlternativeOfferExctractor)
        {
            _iOfferExctactor = iOfferExctactor;
            _iAlternativeOfferLinkGenerator = iAlternativeOfferLinkGenerator;
            _iAlternativeOfferExctractor = iAlternativeOfferExctractor;
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            List<IdAndDate> words = _iOfferExctactor.GetPageHtml("9");
            var t = _iOfferExctactor.GetProperties(words);
            _iAlternativeOfferExctractor.GetAlternativeOffers(_iAlternativeOfferLinkGenerator.SearchToComparePrice(t));

            grdOffers.DataSource = t;
        }
    }
}
