using commercetools.Api.Models.Categories;
using commercetools.Api.Models.Common;
using commercetools.Api.Models.Errors;
using commercetools.Api.Models.Products;
using commercetools.Api.Models.ProductTypes;
using commercetools.Api.Models.Projects;
using commercetools.Api.Models.ShippingMethods;
using commercetools.Api.Models.TaxCategories;
using commercetools.Api.Models.Zones;
using commercetools.Base.Client;
using commercetools.Sdk.Api;
using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Extensions;
using commercetools.Sdk.Api.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommerceToolsAPI
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            var clientName = "test";
            var collection = new ServiceCollection();
            collection.UseCommercetoolsApiSerialization();

            collection.AddLogging();

            collection.SetupClient(
                clientName,
                errorTypeMapper => typeof(ErrorResponse),
                s => s.GetService<SerializerService>()
            );

            var serviceProvider = collection.BuildServiceProvider();
            var config = new ClientConfiguration()
            {
                ClientId = "",
                ClientSecret = "",
                ProjectKey = ""
            };


            var clientFactory = serviceProvider.GetService<IHttpClientFactory>();

            var client = ClientFactory.Create(
                clientName,
                config,
                clientFactory,
                serviceProvider.GetService<SerializerService>(),
                TokenProviderFactory.CreateClientCredentialsTokenProvider(config, clientFactory)
            );

            var project = await new ApiRoot(client)
                .WithProjectKey(config.ProjectKey)
                .Get()
                .ExecuteAsync();


            //await UpdateLanguage(client, config, project);
            //await AddTaxCategory(client, config);
            //await AddZone(client, config);
            //await AddShippingMethod(client, config);
            //await AddProductType(client, config);
            //await AddCategory(client, config);
            await AddProducts(client, config);
        }

        public static async Task AddProducts(IClient client, ClientConfiguration config)
        {
            var getZones = await client.WithApi().WithProjectKey(config.ProjectKey).Zones().Get().ExecuteAsync();
            var getDanmarkZone = getZones.Results.Find(x => x.Name.Equals("DanmarkZone"));
            var zoneResourceIdentifier = new ZoneResourceIdentifier { Id = getDanmarkZone.Id };



            var getCategories = await client.WithApi().WithProjectKey(config.ProjectKey).Categories().Get().ExecuteAsync();
            var categoryOneId = "202f3259-08cd-4653-b7b2-3f753c2af121";
            //var categoryOne = getCategories.Results.Find(x => x.Name.Equals("Kategori1"));
            //var categoryOneId = new CategoryResourceIdentifier { Id = categoryOne.Id };

            //var categoryTwo = getCategories.Results.Find(x => x.Name.Equals("Category2"));
            var categoryTwoId = "7c5a8b68-8f88-4441-ad2e-9e6fd8981239";
            //var categoryTwoId = new CategoryResourceIdentifier { Id = categoryTwo.Id };

            //var categoryThree = getCategories.Results.Find(x => x.Name.Equals("Category3"));
            var categoryThreeId = "0876374b-acb6-4777-b9f4-75a36c73025c";
            //var categoryThreeId = new CategoryResourceIdentifier { Id = categoryThree.Id };

            var getProductTypes = await client.WithApi().WithProjectKey(config.ProjectKey).ProductTypes().Get().ExecuteAsync();
            var productType = getProductTypes.Results.Find(x => x.Name.Equals("Clothes"));
            var productTypeId = new ProductTypeResourceIdentifier { Id = productType.Id };

            var productDraftsCategoryOne = new List<IProductDraft>()
            {
                new ProductDraft
                {
                    Name = new LocalizedString { { "da", "Nike Sko" }, { "en", "Nike Shoe" } },
                    ProductType = productTypeId,
                    Slug = new LocalizedString { { "da", "Nike-Sko" }, { "en", "Nike-Shoe" } },
                    Categories = new List<ICategoryResourceIdentifier>()
                        {
                            new CategoryResourceIdentifier()
                            {
                                Id = categoryOneId
                            }
                        }
                },
                new ProductDraft
                {
                    Name = new LocalizedString { { "da", "Adidas Sko" }, { "en", "Adidas Shoe" } },
                    ProductType = productTypeId,
                    Slug = new LocalizedString { { "da", "Adidas-Sko" }, { "en", "Adidas-Shoe" } },
                    Categories = new List<ICategoryResourceIdentifier>()
                        {
                            new CategoryResourceIdentifier()
                            {
                                Id = categoryOneId
                            }
                        }
                },
                new ProductDraft
                {
                    Name = new LocalizedString { { "da", "New Balance Sko" }, { "en", "New Balance Shoe" } },
                    ProductType = productTypeId,
                    Slug = new LocalizedString { { "da", "New-Balance-Sko" }, { "en", "New-Balance-Shoe" } },
                    Categories = new List<ICategoryResourceIdentifier>()
                        {
                            new CategoryResourceIdentifier()
                            {
                                Id = categoryOneId
                            }
                        }
                }
            };

            //foreach (var item in productDraftsCategoryOne)
            //{
            //    var result = await client.WithApi().WithProjectKey(config.ProjectKey).Products().Post(item).ExecuteAsync();
            //}

            var productDraftCategoryTwo = new List<IProductDraft>()
            {
                new ProductDraft
                {
                    Name = new LocalizedString { { "da", "Levi's Trøje" }, { "en", "Levi's Shirt" } },
                    ProductType = productTypeId,
                    Slug = new LocalizedString { { "da", "Levi-Troeje" }, { "en", "Levi-Shirt" } },
                    Categories = new List<ICategoryResourceIdentifier>()
                        {
                            new CategoryResourceIdentifier()
                            {
                                Id = categoryTwoId
                            }
                        }
                },
                new ProductDraft
                {
                    Name = new LocalizedString { { "da", "Adidas Trøje" }, { "en", "Adidas Shirt" } },
                    ProductType = productTypeId,
                    Slug = new LocalizedString { { "da", "Adidas-Troeje" }, { "en", "Adidas-Shirt" } },
                    Categories = new List<ICategoryResourceIdentifier>()
                        {
                            new CategoryResourceIdentifier()
                            {
                                Id = categoryTwoId
                            }
                        }
                },
                new ProductDraft
                {
                    Name = new LocalizedString { { "da", "Hummel Trøje" }, { "en", "Hummel Shirt" } },
                    ProductType = productTypeId,
                    Slug = new LocalizedString { { "da", "Hummel-Troeje" }, { "en", "Hummel-Shirt" } },
                    Categories = new List<ICategoryResourceIdentifier>()
                        {
                            new CategoryResourceIdentifier()
                            {
                                Id = categoryTwoId
                            }
                        }
                }
            };
            //foreach (var item in productDraftCategoryTwo)
            //{
            //    var result = await client.WithApi().WithProjectKey(config.ProjectKey).Products().Post(item).ExecuteAsync();
            //}

            var productDraftCategoryThree = new List<IProductDraft>()
            {
                new ProductDraft
                {
                    Name = new LocalizedString { { "da", "Levi's Bukser" }, { "en", "Levi's Pants" } },
                    ProductType = productTypeId,
                    Slug = new LocalizedString { { "da", "Levi-Bukser" }, { "en", "Levi-Pants" } },
                    Categories = new List<ICategoryResourceIdentifier>()
                        {
                            new CategoryResourceIdentifier()
                            {
                                Id = categoryThreeId
                            }
                        }
                },
                new ProductDraft
                {
                    Name = new LocalizedString { { "da", "Adidas Bukser" }, { "en", "Adidas Pants" } },
                    ProductType = productTypeId,
                    Slug = new LocalizedString { { "da", "Adidas-Bukser" }, { "en", "Adidas-Pants" } },
                    Categories = new List<ICategoryResourceIdentifier>()
                        {
                            new CategoryResourceIdentifier()
                            {
                                Id = categoryThreeId
                            }
                        }
                },
                new ProductDraft
                {
                    Name = new LocalizedString { { "da", "Hummel Bukser" }, { "en", "Hummel Pants" } },
                    ProductType = productTypeId,
                    Slug = new LocalizedString { { "da", "Hummel-Bukser" }, { "en", "Hummel-Pants" } },
                    Categories = new List<ICategoryResourceIdentifier>()
                        {
                            new CategoryResourceIdentifier()
                            {
                                Id = categoryThreeId
                            }
                        }
                }
            };
            foreach (var item in productDraftCategoryThree)
            {
                var result = await client.WithApi().WithProjectKey(config.ProjectKey).Products().Post(item).ExecuteAsync();
            }

        }

        public static async Task AddCategory(IClient client, ClientConfiguration config)
        {
            
            var categoryDraft = new List<ICategoryDraft>()
            {
                new CategoryDraft
                {
                    Name = new LocalizedString { { "da", "Kategori1" }, { "en", "Category1" } },
                    Slug = new LocalizedString { { "da", "kategori1" }, { "en", "category1" } }
                },
                new CategoryDraft
                {
                    Name = new LocalizedString{{"da", "Kategori2"}, { "en", "Category2"} },
                    Slug = new LocalizedString{{"da", "kategori2"}, { "en", "category2"} }
                },
                new CategoryDraft
                {
                    Name = new LocalizedString{{"da", "Kategori3"}, { "en", "Category3"} },
                    Slug = new LocalizedString{{"da", "kategori3"}, { "en", "category3"} }
                }
            };

            foreach (var item in categoryDraft)
            {
                var category = await client.WithApi().WithProjectKey(config.ProjectKey).Categories().Post(item).ExecuteAsync();
            }

        }

        public static async Task AddProductType(IClient client, ClientConfiguration config)
        {
            List<IAttributePlainEnumValue> lande = new List<IAttributePlainEnumValue>()
            {
                new AttributePlainEnumValue()
                {
                    Key = "DK",
                    Label = "Danmark"
                },
                new AttributePlainEnumValue()
                {
                    Key = "EN",
                    Label = "England"
                }
            };

            List<IAttributePlainEnumValue> sizes = new List<IAttributePlainEnumValue>()
            {
                new AttributePlainEnumValue()
                {
                    Key = "Small",
                    Label = "Lille"
                },
                new AttributePlainEnumValue()
                {
                    Key = "Medium",
                    Label = "Mellem"
                },
                new AttributePlainEnumValue()
                {
                    Key = "Large",
                    Label = "Stor"
                }
            };

            List<IAttributePlainEnumValue> colours = new List<IAttributePlainEnumValue>()
            {
                new AttributePlainEnumValue()
                {
                    Key = "Black",
                    Label = "Sort"
                },
                new AttributePlainEnumValue()
                {
                    Key = "Blue",
                    Label = "Blå"
                },
                new AttributePlainEnumValue()
                {
                    Key = "White",
                    Label = "Hvid"
                }
            };

            List<IAttributePlainEnumValue> materials = new List<IAttributePlainEnumValue>()
            {
                new AttributePlainEnumValue()
                {
                    Key = "Polyester",
                    Label = "Polyester"
                },
                new AttributePlainEnumValue()
                {
                    Key = "Cotton",
                    Label = "Bomuld"
                },
                new AttributePlainEnumValue()
                {
                    Key = "Wool",
                    Label = "Uld"
                }
            };

            List<IAttributeDefinitionDraft> attributeDefinitions = new List<IAttributeDefinitionDraft>()
            {
                new AttributeDefinitionDraft()
                {
                    Type = new AttributeEnumType()
                    {
                        Name = "enum",
                        Values = lande
                    },
                    Name = "CountryOrigin",
                    Label = new LocalizedString {{"da", "Oprindelsesland"}, {"en", "Country Of Origin"}},
                    IsRequired = false
                },
                new AttributeDefinitionDraft()
                {
                    Type = new AttributeEnumType()
                    {
                        Name = "enum",
                        Values = sizes
                    },
                    Name = "Sizes",
                    Label = new LocalizedString {{"da", "Størrelse"}, {"en", "Size"}},
                    IsRequired = false
                },
                new AttributeDefinitionDraft()
                {
                    Type = new AttributeEnumType()
                    {
                        Name = "enum",
                        Values = colours
                    },
                    Name = "Colours",
                    Label = new LocalizedString{{"da", "Farve" },{"en", "Colour" }},
                    IsRequired = false
                },
                new AttributeDefinitionDraft()
                {
                    Type = new AttributeEnumType()
                    {
                        Name = "enum",
                        Values = materials
                    },
                    Name = "Materials",
                    Label = new LocalizedString{{"da", "Materiale"}, {"en", "Material"} },
                    IsRequired = false
                }
            };

            var productTypeDraft = new ProductTypeDraft()
            {
                Name = "Clothes",
                Description = "Clothes Description",
                Attributes = attributeDefinitions,
            };

            var productType = await client.WithApi().WithProjectKey(config.ProjectKey).ProductTypes().Post(productTypeDraft).ExecuteAsync();

        }



        public static async Task AddZone(IClient client, ClientConfiguration config)
        {

            var location = new Location
            {
                Country = "DK"
            };
            List<ILocation> locations = new List<ILocation>();
            locations.Add(location);

            var zoneDraft = new ZoneDraft
            {
                Name = "DanmarkZone",
                Locations = locations
            };

            var zone = await client.WithApi().WithProjectKey(config.ProjectKey).Zones().Post(zoneDraft).ExecuteAsync();
        }


        public static async Task UpdateLanguage(IClient client, ClientConfiguration config, Project project)
        {
            var result = await client.WithApi().WithProjectKey(config.ProjectKey).Post(new ProjectUpdate()
            {
                Version = project.Version,
                Actions = new List<IProjectUpdateAction>()
                {
                    new ProjectChangeLanguagesAction()
                    {
                        Action = "changeLanguages",
                        Languages = new List<string>()
                        {
                            "en",
                            "da"
                        }
                    }
                }
            }).ExecuteAsync();
        }

        public static async Task AddShippingMethod(IClient client, ClientConfiguration config)
        {
            List<IShippingRateDraft> shippingRates = new List<IShippingRateDraft>();
            var shippingRateDraft = new ShippingRateDraft
            {
                Price = new Money
                {
                    CurrencyCode = "DKK",
                    CentAmount = 5000
                }
            };
            shippingRates.Add(shippingRateDraft);

            var getZones = await client.WithApi().WithProjectKey(config.ProjectKey).Zones().Get().ExecuteAsync();
            var getDanmarkZone = getZones.Results.Find(x => x.Name.Equals("DanmarkZone"));
            var zoneResourceIdentifier = new ZoneResourceIdentifier { Id = getDanmarkZone.Id };

            List<IZoneRateDraft> zoneRates = new List<IZoneRateDraft>();
            var zoneRateDraft = new ZoneRateDraft
            {
                Zone = zoneResourceIdentifier,
                ShippingRates = shippingRates
            };
            zoneRates.Add(zoneRateDraft);

            var taxCategories = await client.WithApi().WithProjectKey(config.ProjectKey).TaxCategories().Get().ExecuteAsync();
            var taxCategory = taxCategories.Results.Find(x => x.Name.Equals("Danmark taxcategory"));
            var taxResourceIdentifer = new TaxCategoryResourceIdentifier { Id = taxCategory.Id };
           

           var shippingMethodDraft = new ShippingMethodDraft
           {
               Name = "Danmark",
               TaxCategory = taxResourceIdentifer,
               IsDefault = true,
               ZoneRates = zoneRates
           };
            var shippingMethod = await client.WithApi().WithProjectKey(config.ProjectKey).ShippingMethods().Post(shippingMethodDraft).ExecuteAsync();
        }

        public static async Task AddTaxCategory(IClient client, ClientConfiguration config)
        {
            var taxRateDraft = new TaxRateDraft
            {
                Name = "Danmark taxrate",
                Amount = 0.25,
                IncludedInPrice = true,
                Country = "DK"
            };

            List<ITaxRateDraft> taxRates = new List<ITaxRateDraft>();
            taxRates.Add(taxRateDraft);

            var taxCategoryDraft = new TaxCategoryDraft
            {
                Name = "Danmark taxcategory",
                Rates = taxRates
            };

            var taxCategory = await client.WithApi().WithProjectKey(config.ProjectKey).TaxCategories().Post(taxCategoryDraft).ExecuteAsync();
        }
    }
}
