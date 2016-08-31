using OfferPriceEvaluator.Domain.Concrete;
using System; 
using System.Globalization; 
using System.Net; 
using System.Threading; 
using System.Windows.Forms;
using Autofac; 
using OfferPriceEvaluator.Domain.Abstract;
using OfferPriceEvaluator.Domain;
using OfferPriceEvaluator.Domain.Entities;
using OfferPriceEvaluator.Abstract;

namespace OfferPriceEvaluator
{
    class Program
    {
        //private static IContainerControl _icontainerControl;

        //public Program(IContainerControl iContainerControl)
        //{
        //    _icontainerControl = iContainerControl;
        //}

        [STAThread]
        static void Main(string[] args)
        {
            CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = culture;

            var builder = new ContainerBuilder();

            builder.RegisterType<Program>();
            builder.RegisterType<DataGrid>();
            builder.RegisterType<WebClient>();
            builder.RegisterType<OfferExctractor>();

            //builder.RegisterType<DataGrid>().As<IContainerControl>();
            builder.RegisterType<OfferExctractor>().As<IOfferExctactor>();
            builder.RegisterType<HtmlWebWrapper>().As<IHtmlWebWrapper>();
            builder.RegisterType<ItemRepository>().As<BaseRepository<Item>>();
            builder.RegisterType<SellerRepository>().As<BaseRepository<Seller>>();
            builder.RegisterType<CategoryRepository>().As<BaseRepository<Category>>();
            builder.RegisterType<TagRepository>().As<BaseRepository<Tag>>();
            builder.RegisterType<TagToTagGroupRepository>().As<BaseRepository<TagToTagGroup>>();
            builder.RegisterType<ItemTagValueRepository>().As<BaseRepository<ItemTagValue>>();
            builder.RegisterType<AlternativePriceItemTagRepository>().As<BaseRepository<AlternativePriceItemTag>>(); 
            builder.RegisterType<WebClientWrapper>().As<IWebClientWrapper>(); 
            builder.RegisterType<AlternativeOfferLinkGenerator>().As<IAlternativeOfferLinkGenerator>();
            builder.RegisterType<AlternativeOfferExctractor>().As<IAlternativeOfferExctractor>();
            builder.RegisterType<OfferPriceEvaluatorContext>().As<IDbContextOfferPriceEvaluator>().SingleInstance();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                ////var component = container.Resolve<Program>();
                //var component1 = container.Resolve<DataGrid>();
                //var component2 = container.Resolve<OfferExctractor>(); 
                scope.Resolve<WebClient>();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(scope.Resolve<DataGrid>());

                //DataGrid data = new DataGrid();
                //data.Show();
            }
        }
    }
}
