```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow product) => {
    
    /*** creating IMobileScreen 'ProductDetail' ***/
    var productDetail = product.CreateScreen("ProductDetail");
    productDetail.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = product.GetESpace();
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    productDetail.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    productDetail.Roles.Add(orderManager);
    productDetail.AnonymousAccess = false; // default value
    productDetail.CustomURL = false; // default value
    productDetail.Description = "Screen that displays all the details based on a given product Id.";
    productDetail.HorizontalPosition = 6800;
    productDetail.Icon = null; // default value
    productDetail.IsDisabled = false; // default value
    productDetail.PageName = null; // default value
    productDetail.Public = false; // default value
    productDetail.Shareable = false; // default value
    productDetail.StyleSheet = "/* -------------------------------------------------------------------------------- */\n/* OS ScreenTemplate - Product Detail                                               */\n/* -------------------------------------------------------------------------------- */\n\n.form-control[data-textarea] {\n    resize: none;\n}\n\n/* hide the (*) mandatory synbols */\n[data-label].mandatory:after {\n    display: none;\n}\n\n[data-upload].disabled {\n    pointer-events: none;\n}\n\n";
    productDetail.SetTitle("\"Product Detail\"");
    productDetail.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    productDetail.VerticalPosition = 2285;
    
    var getModelsByBrand = productDetail.CreateScreenAggregate(false, "GetModelsByBrand");
    getModelsByBrand.CacheInMinutes = null; // default value
    getModelsByBrand.Description = null; // default value
    getModelsByBrand.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.OnDemand;
    getModelsByBrand.SetMaxRecords("100");
    getModelsByBrand.SetStartIndex(null); // default value
    getModelsByBrand.Timeout = null; // default value
    var model2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Model");
    var model = getModelsByBrand.AsDatabaseAggregate.CreateSource(model2);
    getModelsByBrand.AsDatabaseAggregate.CreateFilter("Model.BrandId = GetProductInfo.ProductDetails.Model.BrandId");
    getModelsByBrand.AsDatabaseAggregate.CreateFilter("Model.IsActive = True");
    
    
    var getProductInfo = productDetail.CreateDataAction("GetProductInfo");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getProductInfo.OutputParameters.ToList().ForEach(x => x.Delete());
    getProductInfo.Description = "Get all info related with given id product and all the product nedded info.";
    getProductInfo.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getProductInfo.ServerRequestTimeout = null; // default value
    
    var numberFeaturesToAdd = getProductInfo.CreateLocalVariable("NumberFeaturesToAdd");
    numberFeaturesToAdd.DataType = eSpace.IntegerType;
    numberFeaturesToAdd.SetDefaultValue(null); // default value
    numberFeaturesToAdd.Description = null; // default value
    
    var numberImagesUpload = getProductInfo.CreateLocalVariable("NumberImagesUpload");
    numberImagesUpload.DataType = eSpace.IntegerType;
    numberImagesUpload.SetDefaultValue(null); // default value
    numberImagesUpload.Description = null; // default value
    
    var startNode = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 3199;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 1371;
    
    var getProductDetails = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProductDetails").ConnectedBelow(startNode, 1600);
    getProductDetails.CacheInMinutes = null; // default value
    getProductDetails.Description = "Fetches the product details from sample data based on a given Id.";
    getProductDetails.IsDisabled = false; // default value
    getProductDetails.SetMaxRecords("1");
    getProductDetails.SetStartIndex(null); // default value
    getProductDetails.Timeout = null; // default value
    var product3 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Product");
    var product2 = getProductDetails.AsDatabaseAggregate.CreateSource(product3);
    var model3 = getProductDetails.AsDatabaseAggregate.CreateSource(model2);
    var join = getProductDetails.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = product2;
    join.RightSource = model3;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join.SetCondition("Product.ModelId = Model.Id");
    getProductDetails.AsDatabaseAggregate.CreateFilter("Product.Id = ProductId");
    
    
    var assignNode = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedToTheRightOf(getProductDetails, 1830);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Set ProductDetails";
    
    var assignment = assignNode.CreateAssignment("ProductDetails.Product", "GetProductDetails.List.Current.Product");
    
    var assignment2 = assignNode.CreateAssignment("ProductDetails.IsEmpty", "GetProductDetails.List.Empty");
    
    var assignment3 = assignNode.CreateAssignment("ProductDetails.FinalPrice", "Round(GetProductDetails.List.Current.Product.Price * (1 - GetProductDetails.List.Current.Product.Discount), 2)\n");
    
    var assignment4 = assignNode.CreateAssignment("ProductDetails.ModelName", "GetProductDetails.List.Current.Model.Name");
    
    var assignment5 = assignNode.CreateAssignment("ProductDetails.Product.Discount", "ProductDetails.Product.Discount * 100");
    
    var assignment6 = assignNode.CreateAssignment("ProductDetails.Model", "GetProductDetails.List.Current.Model");
    
    var getCategories = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetCategories");
    getCategories.CacheInMinutes = null; // default value
    getCategories.Description = "Fetches the product categories from sample data.";
    getCategories.HorizontalPosition = 3200;
    getCategories.IsDisabled = false; // default value
    getCategories.SetMaxRecords(null); // default value
    getCategories.SetStartIndex(null); // default value
    getCategories.Timeout = null; // default value
    getCategories.VerticalPosition = 4343;
    var productCategory2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("ProductCategory");
    var productCategory = getCategories.AsDatabaseAggregate.CreateSource(productCategory2);
    getCategories.AsDatabaseAggregate.CreateFilter("ProductCategory.IsActive or \nProductCategory.Id = GetProductDetails.List.Current.Product.CategoryId");
    getCategories.AsDatabaseAggregate.CreateSort().SetAttribute("ProductCategory.Name");
    
    assignNode.Target = getCategories;
    
    var assignNode2 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedToTheRightOf(getCategories, 1829);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Set Categories";
    
    var assignment7 = assignNode2.CreateAssignment("Categories", "GetCategories.List mapTo {  }");
    
    var getProductPictureById = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProductPictureById");
    getProductPictureById.CacheInMinutes = null; // default value
    getProductPictureById.Description = "Fetches all the product pictures by the product Id.";
    getProductPictureById.HorizontalPosition = 3200;
    getProductPictureById.IsDisabled = false; // default value
    getProductPictureById.SetMaxRecords(null); // default value
    getProductPictureById.SetStartIndex(null); // default value
    getProductPictureById.Timeout = null; // default value
    getProductPictureById.VerticalPosition = 5714;
    var productPicture2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("ProductPicture");
    var productPicture = getProductPictureById.AsDatabaseAggregate.CreateSource(productPicture2);
    getProductPictureById.AsDatabaseAggregate.CreateFilter("ProductPicture.IsActive and ProductPicture.ProductId = ProductId");
    getProductPictureById.AsDatabaseAggregate.CreateSort().SetAttribute("ProductPicture.IsCover");
    getProductPictureById.AsDatabaseAggregate.CreateSort().SetAttribute("ProductPicture.Id");
    
    assignNode2.Target = getProductPictureById;
    
    var assignNode3 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getProductPictureById, 1372);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Set ProdPictures";
    
    var assignment8 = assignNode3.CreateAssignment("ProductPictures", "GetProductPictureById.List mapTo { IsModified: True, IsNew: False, IsPlaceholder: False, ProductPicture: ProductPicture }");
    
    var ifNode = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode3, 1371);
    ifNode.SetCondition("ProductPictures.Length < 6");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Images length < 6?";
    
    var getBrands = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetBrands").Below(ifNode, 2972);
    getBrands.CacheInMinutes = null; // default value
    getBrands.Description = null; // default value
    getBrands.IsDisabled = false; // default value
    getBrands.SetMaxRecords(null); // default value
    getBrands.SetStartIndex(null); // default value
    getBrands.Timeout = null; // default value
    var brand2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Brand");
    var brand = getBrands.AsDatabaseAggregate.CreateSource(brand2);
    getBrands.AsDatabaseAggregate.CreateFilter("Brand.IsActive = True or \nBrand.Id = GetProductDetails.List.Current.Model.BrandId");
    getBrands.AsDatabaseAggregate.CreateSort().SetAttribute("Brand.Name");
    
    ifNode.FalseTarget = getBrands;
    
    var assignNode4 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1829);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Set Number of upload images";
    
    var assignment9 = assignNode4.CreateAssignment("NumberImagesUpload", "ProductPictures.Length");
    ifNode.TrueTarget = assignNode4;
    
    var assignNode5 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedToTheRightOf(getBrands, 1829);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = "Set Brands";
    
    var assignment10 = assignNode5.CreateAssignment("Brands", "GetBrands.List mapTo {  }");
    
    var ifNode2 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode4, 1372);
    ifNode2.SetCondition("NumberImagesUpload < 6");
    ifNode2.FalseTarget = getBrands;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Less than 6 images uploaded?";
    
    var getProductFeaturesByProductId = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAggregateNode>("GetProductFeaturesByProductId");
    getProductFeaturesByProductId.CacheInMinutes = null; // default value
    getProductFeaturesByProductId.Description = null; // default value
    getProductFeaturesByProductId.HorizontalPosition = 3200;
    getProductFeaturesByProductId.IsDisabled = false; // default value
    getProductFeaturesByProductId.SetMaxRecords(null); // default value
    getProductFeaturesByProductId.SetStartIndex(null); // default value
    getProductFeaturesByProductId.Timeout = null; // default value
    getProductFeaturesByProductId.VerticalPosition = 13029;
    var productFeature2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("ProductFeature");
    var productFeature = getProductFeaturesByProductId.AsDatabaseAggregate.CreateSource(productFeature2);
    getProductFeaturesByProductId.AsDatabaseAggregate.CreateFilter("ProductFeature.ProductId = ProductId");
    getProductFeaturesByProductId.AsDatabaseAggregate.CreateSort().SetAttribute("ProductFeature.Id");
    
    assignNode5.Target = getProductFeaturesByProductId;
    
    var listAppendImage = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppendImage");
    var _System_ = eSpace.References.Named("(System)");
    var listAppend = _System_.ServerActions.Named("ListAppend");
    listAppendImage.Action = listAppend;
    listAppendImage.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppendImage.HorizontalPosition = 6629;
    listAppendImage.IsDisabled = false; // default value
    listAppendImage.ServerRequestTimeout = null; // default value
    listAppendImage.VerticalPosition = 8457;
    var list = listAppend.InputParameters.Named("List");
    listAppendImage.SetArgumentValue(list, "ProductPictures");
    var element = listAppend.InputParameters.Named("Element");
    listAppendImage.SetArgumentValue(element, "{ IsModified: False, IsNew: False, IsPlaceholder: True, ProductPicture: { Id: NullIdentifier(), ProductId: GetProductDetails.List.Current.Product.Id } }");
    ifNode2.TrueTarget = listAppendImage;
    
    var assignNode6 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getProductFeaturesByProductId, 1600);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "Set Product features";
    
    var assignment11 = assignNode6.CreateAssignment("ProductFeatures", "GetProductFeaturesByProductId.List mapTo { Description: ProductFeature.Description, Id: ProductFeature.Id, IsEditMode: False, IsModified: True, IsNew: False, IsPlaceholder: False }");
    
    var assignNode7 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listAppendImage, 1372);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    assignNode7.Target = ifNode2;
    
    var assignment12 = assignNode7.CreateAssignment("NumberImagesUpload", "NumberImagesUpload + 1");
    
    var ifNode3 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode6, 1600);
    ifNode3.SetCondition("ProductFeatures.Length < 4 ");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = null; // default value
    
    var endNode = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().Below(ifNode3, 3885);
    endNode.IsDisabled = false; // default value
    ifNode3.FalseTarget = endNode;
    
    var assignNode8 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode3, 1600);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = "Set Number Features To Add";
    
    var assignment13 = assignNode8.CreateAssignment("NumberFeaturesToAdd", "ProductFeatures.Length");
    ifNode3.TrueTarget = assignNode8;
    
    var ifNode4 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode8, 1600);
    ifNode4.SetCondition("NumberFeaturesToAdd < 4");
    ifNode4.FalseTarget = endNode;
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "Less than 4?";
    
    var listAppendFeature = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("ListAppendFeature").ToTheRightOf(ifNode4, 1371);
    listAppendFeature.Action = listAppend;
    listAppendFeature.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    listAppendFeature.IsDisabled = false; // default value
    listAppendFeature.ServerRequestTimeout = null; // default value
    listAppendFeature.SetArgumentValue(list, "ProductFeatures");
    listAppendFeature.SetArgumentValue(element, "{ Description: NullTextIdentifier(), Id: NullIdentifier(), IsEditMode: False, IsModified: False, IsNew: False, IsPlaceholder: True }");
    ifNode4.TrueTarget = listAppendFeature;
    
    var assignNode9 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode9.HorizontalPosition = 4800;
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = null; // default value
    assignNode9.Target = ifNode4;
    assignNode9.VerticalPosition = 19200;
    
    var assignment14 = assignNode9.CreateAssignment("NumberFeaturesToAdd", "NumberFeaturesToAdd + 1");
    listAppendFeature.Target = assignNode9;
    
    var commentNode = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode.HorizontalPosition = 4800;
    commentNode.IsDisabled = false; // default value
    commentNode.IsReminder = false; // default value
    commentNode.Text = "Checks the number of existing images and \nif less than 6, creates a placeholder";
    commentNode.VerticalPosition = 6400;
    commentNode.CreateConnector(ifNode);
    
    var commentNode2 = getProductInfo.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode2.HorizontalPosition = 4571;
    commentNode2.IsDisabled = false; // default value
    commentNode2.IsReminder = false; // default value
    commentNode2.Text = "Checks the number of existing features and \nif less than 4, creates a placeholder";
    commentNode2.VerticalPosition = 14629;
    commentNode2.CreateConnector(ifNode3);
    
    var productDetails = getProductInfo.CreateOutputParameter("ProductDetails");
    productDetails.DataType = eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("FinalPrice", eSpace.CurrencyType), Tuple.Create<string, ITypeSignature>("IsEmpty", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("Model", model2), Tuple.Create<string, ITypeSignature>("ModelName", eSpace.TextType), Tuple.Create<string, ITypeSignature>("Product", product3));
    productDetails.Description = "Returns a record with the product info by a given product id.";
    
    var getProductInfoOnAfterFetch = productDetail.CreateScreenAction("GetProductInfoOnAfterFetch");
    getProductInfoOnAfterFetch.Description = null; // default value
    
    var startNode2 = getProductInfoOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var refreshDataNode = getProductInfoOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>();
    refreshDataNode.DataSource = getModelsByBrand;
    refreshDataNode.HorizontalPosition = 3200;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords("100");
    refreshDataNode.SetStartIndex(null); // default value
    refreshDataNode.VerticalPosition = 2743;
    
    var endNode2 = getProductInfoOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1600);
    endNode2.IsDisabled = false; // default value
    getProductInfo.OnAfterFetch.Destination = getProductInfoOnAfterFetch;
    startNode2.Target = refreshDataNode;
    
    var productPictures = getProductInfo.CreateOutputParameter("ProductPictures");
    productPictures.DataType = eSpace.GetOrCreateListType(eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("IsModified", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsNew", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsPlaceholder", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("ProductPicture", productPicture2)));
    productPictures.Description = "Returns a list of all product images by a given product id.";
    
    var productFeatures = getProductInfo.CreateOutputParameter("ProductFeatures");
    productFeatures.DataType = eSpace.GetOrCreateListType(eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("Description", eSpace.TextType), Tuple.Create<string, ITypeSignature>("Id", productFeature2.IdentifierType), Tuple.Create<string, ITypeSignature>("IsEditMode", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsModified", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsNew", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsPlaceholder", eSpace.BooleanType)));
    productFeatures.Description = null; // default value
    
    var categories = getProductInfo.CreateOutputParameter("Categories");
    categories.DataType = eSpace.GetOrCreateListType(productCategory2);
    categories.Description = "Returns the list of categories.";
    
    var brands = getProductInfo.CreateOutputParameter("Brands");
    brands.DataType = eSpace.GetOrCreateListType(brand2);
    brands.Description = null; // default value
    
    var productId = productDetail.CreateInputParameter("ProductId");
    productId.DataType = product3.IdentifierType;
    productId.Description = "Input variable with the identifier of the product item.";
    productId.IsMandatory = true; // default value
    
    var isEditingMode = productDetail.CreateLocalVariable("IsEditingMode");
    isEditingMode.DataType = eSpace.BooleanType;
    isEditingMode.SetDefaultValue("False");
    isEditingMode.Description = "Toogle if the input are available for edit or not.";
    
    var isSaving = productDetail.CreateLocalVariable("IsSaving");
    isSaving.DataType = eSpace.BooleanType;
    isSaving.SetDefaultValue("False");
    isSaving.Description = "Toogle when the process of saving the chnages made in the product starts and ends.";
    
    var deleteImages = productDetail.CreateLocalVariable("DeleteImages");
    deleteImages.DataType = eSpace.GetOrCreateListType(productPicture2.IdentifierType);
    deleteImages.Description = "List that stores the id of the images deleted when editing the product.";
    
    var deleteFeatures = productDetail.CreateLocalVariable("DeleteFeatures");
    deleteFeatures.DataType = eSpace.GetOrCreateListType(productFeature2.IdentifierType);
    deleteFeatures.Description = "List that stores the id of the features deleted when editing the product.";
    productDetail.OnDestroy.Destination = null; // default value
    productDetail.OnReady.Destination = null; // default value
    productDetail.OnRender.Destination = null; // default value
    
    var clear_DeleteList = productDetail.CreateScreenAction("Clear_DeleteList");
    clear_DeleteList.Description = null; // default value
    
    var startNode3 = clear_DeleteList.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.IsDisabled = false; // default value
    
    var onInitialize = productDetail.CreateScreenAction("OnInitialize");
    onInitialize.Description = null; // default value
    
    var startNode4 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.IsDisabled = false; // default value
    
    var commentNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode3.HorizontalPosition = 8457;
    commentNode3.IsDisabled = false; // default value
    commentNode3.IsReminder = false; // default value
    commentNode3.Text = "This action will run before navigating to this Screen and rendering it.\n\nDo:\n– initialize local variables;\n– any changes that do not involve the Screen’s DOM.\n\nDon’t:\n– use server calls;\n– perform local storage operations.\n\n<a href=\"30092\">Check the documentation for examples and more information.</a>";
    commentNode3.VerticalPosition = 914;
    
    var ifNode5 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode5.SetCondition("ProductId = NullIdentifier()");
    ifNode5.HorizontalPosition = 3192;
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "New Product?";
    ifNode5.VerticalPosition = 2280;
    
    var assignNode10 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode5, 1368);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = null; // default value
    
    var assignment15 = assignNode10.CreateAssignment("IsEditingMode", "True");
    
    var endNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode3.HorizontalPosition = 3200;
    endNode3.IsDisabled = false; // default value
    endNode3.VerticalPosition = 4114;
    productDetail.OnInitialize.Destination = onInitialize;
    
    var listClearImages = clear_DeleteList.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListClearImages");
    var listClear = _System_.ClientActions.Named("ListClear");
    listClearImages.Action = listClear;
    listClearImages.HorizontalPosition = 3200;
    listClearImages.IsDisabled = false; // default value
    listClearImages.VerticalPosition = 2514;
    var list2 = listClear.InputParameters.Named("List");
    listClearImages.SetArgumentValue(list2, "DeleteImages");
    startNode3.Target = listClearImages;
    startNode4.Target = ifNode5;
    ifNode5.FalseTarget = endNode3;
    ifNode5.TrueTarget = assignNode10;
    assignNode10.Target = endNode3;
    
    var listClearFeatures = clear_DeleteList.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListClearFeatures").ConnectedBelow(listClearImages, 1600);
    listClearFeatures.Action = listClear;
    listClearFeatures.IsDisabled = false; // default value
    listClearFeatures.SetArgumentValue(list2, "DeleteFeatures");
    
    var endNode4 = clear_DeleteList.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(listClearFeatures, 1600);
    endNode4.IsDisabled = false; // default value
    
    var feature_OnDelete = productDetail.CreateScreenAction("Feature_OnDelete");
    feature_OnDelete.Description = null; // default value
    
    var emptyFeature = feature_OnDelete.CreateLocalVariable("EmptyFeature");
    emptyFeature.DataType = eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("Description", eSpace.TextType), Tuple.Create<string, ITypeSignature>("Id", productFeature2.IdentifierType), Tuple.Create<string, ITypeSignature>("IsEditMode", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsModified", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsNew", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsPlaceholder", eSpace.BooleanType));
    emptyFeature.Description = null; // default value
    
    var startNode5 = feature_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.IsDisabled = false; // default value
    
    var assignNode11 = feature_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode11.HorizontalPosition = 3200;
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = "Set Empty Feature";
    assignNode11.VerticalPosition = 2514;
    
    var assignment16 = assignNode11.CreateAssignment("EmptyFeature.IsPlaceholder", "True");
    startNode5.Target = assignNode11;
    
    var ifNode6 = feature_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode11, 1372);
    ifNode6.SetCondition("GetProductInfo.ProductFeatures.Current.Id = NullIdentifier()");
    ifNode6.IsDisabled = false; // default value
    ifNode6.Label = "Current ID is null?";
    
    var listAppendToDelete = feature_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppendToDelete").ToTheRightOf(ifNode6, 1600);
    var listAppend2 = _System_.ClientActions.Named("ListAppend");
    listAppendToDelete.Action = listAppend2;
    listAppendToDelete.IsDisabled = false; // default value
    var list3 = listAppend2.InputParameters.Named("List");
    listAppendToDelete.SetArgumentValue(list3, "DeleteFeatures");
    var element2 = listAppend2.InputParameters.Named("Element");
    listAppendToDelete.SetArgumentValue(element2, "GetProductInfo.ProductFeatures.Current.Id");
    ifNode6.FalseTarget = listAppendToDelete;
    
    var listAppendEmpty = feature_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppendEmpty").Below(ifNode6, 1600);
    listAppendEmpty.Action = listAppend2;
    listAppendEmpty.IsDisabled = false; // default value
    listAppendEmpty.SetArgumentValue(element2, "EmptyFeature");
    listAppendEmpty.SetArgumentValue(list3, "GetProductInfo.ProductFeatures");
    ifNode6.TrueTarget = listAppendEmpty;
    listAppendToDelete.Target = listAppendEmpty;
    
    var listRemoveOriginal = feature_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListRemoveOriginal").ConnectedBelow(listAppendEmpty, 1600);
    var listRemove = _System_.ClientActions.Named("ListRemove");
    listRemoveOriginal.Action = listRemove;
    listRemoveOriginal.IsDisabled = false; // default value
    var list4 = listRemove.InputParameters.Named("List");
    listRemoveOriginal.SetArgumentValue(list4, "GetProductInfo.ProductFeatures");
    var position = listRemove.InputParameters.Named("Position");
    listRemoveOriginal.SetArgumentValue(position, "GetProductInfo.ProductFeatures.CurrentRowNumber");
    
    var endNode5 = feature_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(listRemoveOriginal, 1600);
    endNode5.IsDisabled = false; // default value
    
    var feature_OnEdit = productDetail.CreateScreenAction("Feature_OnEdit");
    feature_OnEdit.Description = null; // default value
    
    var startNode6 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 4114;
    startNode6.IsDisabled = false; // default value
    startNode6.VerticalPosition = 800;
    
    var ifNode7 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode6, 1943);
    ifNode7.SetCondition("IsEditingMode");
    ifNode7.IsDisabled = false; // default value
    ifNode7.Label = null; // default value
    
    var endNode6 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode7, 1600);
    endNode6.IsDisabled = false; // default value
    ifNode7.FalseTarget = endNode6;
    
    var ifNode8 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode7, 1828);
    ifNode8.SetCondition("Trim(GetProductInfo.ProductFeatures.Current.Description) = NullTextIdentifier()");
    ifNode8.IsDisabled = false; // default value
    ifNode8.Label = "No Description?";
    ifNode7.TrueTarget = ifNode8;
    
    var ifNode9 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode8, 1829);
    ifNode9.SetCondition("GetProductInfo.ProductFeatures.Current.Id = NullIdentifier()");
    ifNode9.IsDisabled = false; // default value
    ifNode9.Label = "NullIdentifier?";
    ifNode8.FalseTarget = ifNode9;
    
    var feature_OnDelete2 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("Feature_OnDelete").ToTheRightOf(ifNode8, 1600);
    feature_OnDelete2.Action = feature_OnDelete;
    feature_OnDelete2.IsDisabled = false; // default value
    ifNode8.TrueTarget = feature_OnDelete2;
    
    var assignNode12 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode9, 1829);
    assignNode12.IsDisabled = false; // default value
    assignNode12.Label = "Update Feature";
    
    var assignment17 = assignNode12.CreateAssignment("GetProductInfo.ProductFeatures.Current.IsEditMode", "False");
    
    var assignment18 = assignNode12.CreateAssignment("GetProductInfo.ProductFeatures.Current.IsModified", "True");
    
    var assignment19 = assignNode12.CreateAssignment("GetProductInfo.ProductFeatures.Current.IsNew", "False");
    
    var assignment20 = assignNode12.CreateAssignment("GetProductInfo.ProductFeatures.Current.IsPlaceholder", "False");
    ifNode9.FalseTarget = assignNode12;
    
    var feature_OnNew = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("Feature_OnNew").ToTheRightOf(ifNode9, 1600);
    feature_OnNew.IsDisabled = false; // default value
    ifNode9.TrueTarget = feature_OnNew;
    
    var endNode7 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedToTheRightOf(feature_OnDelete2, 1600);
    endNode7.IsDisabled = false; // default value
    
    var endNode8 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode12, 1828);
    endNode8.IsDisabled = false; // default value
    
    var feature_OnNew2 = productDetail.CreateScreenAction("Feature_OnNew");
    feature_OnNew2.Description = null; // default value
    
    var startNode7 = feature_OnNew2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.IsDisabled = false; // default value
    
    var listDuplicate = feature_OnNew2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListDuplicate");
    var listDuplicate2 = _System_.ClientActions.Named("ListDuplicate");
    listDuplicate.Action = listDuplicate2;
    listDuplicate.HorizontalPosition = 3200;
    listDuplicate.IsDisabled = false; // default value
    listDuplicate.VerticalPosition = 2514;
    var sourceList = listDuplicate2.InputParameters.Named("SourceList");
    listDuplicate.SetArgumentValue(sourceList, "GetProductInfo.ProductFeatures");
    
    var listFilterNonPlaceholders = feature_OnNew2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListFilterNonPlaceholders").ConnectedBelow(listDuplicate, 1372);
    var listFilter = _System_.ClientActions.Named("ListFilter");
    listFilterNonPlaceholders.Action = listFilter;
    listFilterNonPlaceholders.IsDisabled = false; // default value
    var condition = listFilter.InputParameters.Named("Condition");
    listFilterNonPlaceholders.SetArgumentValue(condition, "not IsPlaceholder");
    var sourceList2 = listFilter.InputParameters.Named("SourceList");
    listFilterNonPlaceholders.SetArgumentValue(sourceList2, "ListDuplicate.DuplicatedList");
    
    var assignNode13 = feature_OnNew2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listFilterNonPlaceholders, 1371);
    assignNode13.IsDisabled = false; // default value
    assignNode13.Label = "Update Feature";
    
    var assignment21 = assignNode13.CreateAssignment("GetProductInfo.ProductFeatures.Current.IsEditMode", "False");
    
    var assignment22 = assignNode13.CreateAssignment("GetProductInfo.ProductFeatures.Current.IsModified", "False");
    
    var assignment23 = assignNode13.CreateAssignment("GetProductInfo.ProductFeatures.Current.IsNew", "True");
    
    var assignment24 = assignNode13.CreateAssignment("GetProductInfo.ProductFeatures.Current.IsPlaceholder", "False");
    
    var ifNode10 = feature_OnNew2.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode13, 1600);
    ifNode10.SetCondition("ListFilterNonPlaceholders.FilteredList.Length = GetProductInfo.ProductPictures.CurrentRowNumber");
    ifNode10.IsDisabled = false; // default value
    ifNode10.Label = "Rearrange list?";
    
    var endNode9 = feature_OnNew2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode10, 1600);
    endNode9.IsDisabled = false; // default value
    
    var listInsert = feature_OnNew2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListInsert").Below(ifNode10, 1600);
    var listInsert2 = _System_.ClientActions.Named("ListInsert");
    listInsert.Action = listInsert2;
    listInsert.IsDisabled = false; // default value
    var element3 = listInsert2.InputParameters.Named("Element");
    listInsert.SetArgumentValue(element3, "GetProductInfo.ProductFeatures.Current");
    var list5 = listInsert2.InputParameters.Named("List");
    listInsert.SetArgumentValue(list5, "GetProductInfo.ProductFeatures");
    var position2 = listInsert2.InputParameters.Named("Position");
    listInsert.SetArgumentValue(position2, "ListFilterNonPlaceholders.FilteredList.Length");
    
    var listRemove2 = feature_OnNew2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListRemove").ConnectedBelow(listInsert, 1600);
    listRemove2.Action = listRemove;
    listRemove2.IsDisabled = false; // default value
    listRemove2.SetArgumentValue(position, "GetProductInfo.ProductFeatures.CurrentRowNumber");
    listRemove2.SetArgumentValue(list4, "GetProductInfo.ProductFeatures");
    
    var endNode10 = feature_OnNew2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(listRemove2, 1600);
    endNode10.IsDisabled = false; // default value
    feature_OnNew.Action = feature_OnNew2;
    
    var endNode11 = feature_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedToTheRightOf(feature_OnNew, 1600);
    endNode11.IsDisabled = false; // default value
    startNode7.Target = listDuplicate;
    ifNode10.FalseTarget = listInsert;
    ifNode10.TrueTarget = endNode9;
    
    var feature_ToggleEdit = productDetail.CreateScreenAction("Feature_ToggleEdit");
    feature_ToggleEdit.Description = null; // default value
    
    var startNode8 = feature_ToggleEdit.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.IsDisabled = false; // default value
    
    var ifNode11 = feature_ToggleEdit.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode11.SetCondition("IsEditingMode");
    ifNode11.HorizontalPosition = 3200;
    ifNode11.IsDisabled = false; // default value
    ifNode11.Label = null; // default value
    ifNode11.VerticalPosition = 2514;
    startNode8.Target = ifNode11;
    
    var endNode12 = feature_ToggleEdit.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode11, 1600);
    endNode12.IsDisabled = false; // default value
    ifNode11.FalseTarget = endNode12;
    
    var assignNode14 = feature_ToggleEdit.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode11, 1829);
    assignNode14.IsDisabled = false; // default value
    assignNode14.Label = null; // default value
    
    var assignment25 = assignNode14.CreateAssignment("GetProductInfo.ProductFeatures.Current.IsEditMode", "not GetProductInfo.ProductFeatures.Current.IsEditMode");
    ifNode11.TrueTarget = assignNode14;
    
    var endNode13 = feature_ToggleEdit.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode14, 1828);
    endNode13.IsDisabled = false; // default value
    
    var onSelectBrand = productDetail.CreateScreenAction("OnSelectBrand");
    onSelectBrand.Description = null; // default value
    
    var selectedOptionList = onSelectBrand.CreateInputParameter("SelectedOptionList");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var dropdownOption = outSystemsUI.Structures.Named("DropdownOption");
    selectedOptionList.DataType = eSpace.GetOrCreateListType(dropdownOption);
    selectedOptionList.Description = null; // default value
    selectedOptionList.IsMandatory = true; // default value
    
    var startNode9 = onSelectBrand.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.IsDisabled = false; // default value
    
    var assignNode15 = onSelectBrand.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode15.HorizontalPosition = 3200;
    assignNode15.IsDisabled = false; // default value
    assignNode15.Label = "Set Brand";
    assignNode15.VerticalPosition = 2286;
    
    var assignment26 = assignNode15.CreateAssignment("GetProductInfo.ProductDetails.Model.BrandId", "LongIntegerToIdentifier(TextToLongInteger(SelectedOptionList.Current.Value))");
    startNode9.Target = assignNode15;
    
    var refreshDataNode2 = onSelectBrand.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode15, 1371);
    refreshDataNode2.DataSource = getModelsByBrand;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords("100");
    refreshDataNode2.SetStartIndex(null); // default value
    
    var endNode14 = onSelectBrand.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode14.HorizontalPosition = 3196;
    endNode14.IsDisabled = false; // default value
    endNode14.VerticalPosition = 5120;
    refreshDataNode2.Target = endNode14;
    
    var onSelectModel = productDetail.CreateScreenAction("OnSelectModel");
    onSelectModel.Description = null; // default value
    
    var selectedOptionList2 = onSelectModel.CreateInputParameter("SelectedOptionList");
    selectedOptionList2.DataType = eSpace.GetOrCreateListType(dropdownOption);
    selectedOptionList2.Description = null; // default value
    selectedOptionList2.IsMandatory = true; // default value
    
    var startNode10 = onSelectModel.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.IsDisabled = false; // default value
    
    var assignNode16 = onSelectModel.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode16.HorizontalPosition = 3192;
    assignNode16.IsDisabled = false; // default value
    assignNode16.Label = "Set Model";
    assignNode16.VerticalPosition = 2736;
    
    var assignment27 = assignNode16.CreateAssignment("GetProductInfo.ProductDetails.Product.ModelId", "LongIntegerToIdentifier(TextToLongInteger(SelectedOptionList.Current.Value))");
    startNode10.Target = assignNode16;
    
    var endNode15 = onSelectModel.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode15.HorizontalPosition = 3196;
    endNode15.IsDisabled = false; // default value
    endNode15.VerticalPosition = 5120;
    assignNode16.Target = endNode15;
    
    var product_GenericInfoFormValidation = productDetail.CreateScreenAction("Product_GenericInfoFormValidation");
    product_GenericInfoFormValidation.Description = "Verify if the generic form is valid.";
    
    var startNode11 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.HorizontalPosition = 4571;
    startNode11.IsDisabled = false; // default value
    startNode11.VerticalPosition = 457;
    
    var ifNode12 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode12.SetCondition("Trim(GetProductInfo.ProductDetails.Product.Name) <> NullTextIdentifier()");
    ifNode12.HorizontalPosition = 4541;
    ifNode12.IsDisabled = false; // default value
    ifNode12.Label = "Valid name?";
    ifNode12.VerticalPosition = 1799;
    startNode11.Target = ifNode12;
    
    var assignNode17 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode12, 1828);
    assignNode17.IsDisabled = false; // default value
    assignNode17.Label = "Set name invalid";
    
    var assignment28 = assignNode17.CreateAssignment("ProdDetail_Name_Input.Valid", "False");
    
    var assignment29 = assignNode17.CreateAssignment("ProdDetail_Name_Input.ValidationMessage", "\"Product name must be filled\"");
    ifNode12.FalseTarget = assignNode17;
    
    var assignNode18 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode12, 1371);
    assignNode18.IsDisabled = false; // default value
    assignNode18.Label = "Set name valid";
    
    var assignment30 = assignNode18.CreateAssignment("ProdDetail_Name_Input.Valid", "True");
    ifNode12.TrueTarget = assignNode18;
    
    var ifNode13 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode18, 1372);
    ifNode13.SetCondition("GetProductInfo.ProductDetails.Product.CategoryId <> NullIdentifier()");
    ifNode13.IsDisabled = false; // default value
    ifNode13.Label = "Valid category?";
    assignNode17.Target = ifNode13;
    
    var assignNode19 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode13, 1828);
    assignNode19.IsDisabled = false; // default value
    assignNode19.Label = "Set category invalid";
    
    var assignment31 = assignNode19.CreateAssignment("ProdDetail_Category_Select.Valid", "False");
    
    var assignment32 = assignNode19.CreateAssignment("ProdDetail_Category_Select.ValidationMessage", "\"A category must be chosen\"");
    ifNode13.FalseTarget = assignNode19;
    
    var assignNode20 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode13, 1600);
    assignNode20.IsDisabled = false; // default value
    assignNode20.Label = "Set category valid";
    
    var assignment33 = assignNode20.CreateAssignment("ProdDetail_Category_Select.Valid", "True");
    ifNode13.TrueTarget = assignNode20;
    
    var ifNode14 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode20, 1600);
    ifNode14.SetCondition("Trim(GetProductInfo.ProductDetails.Product.Description) <> NullTextIdentifier()");
    ifNode14.IsDisabled = false; // default value
    ifNode14.Label = "Valid description?";
    assignNode19.Target = ifNode14;
    
    var assignNode21 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode14, 1828);
    assignNode21.IsDisabled = false; // default value
    assignNode21.Label = "Set description invalid";
    
    var assignment34 = assignNode21.CreateAssignment("TextArea_Description.Valid", "False");
    
    var assignment35 = assignNode21.CreateAssignment("TextArea_Description.ValidationMessage", "\"Description must be filled\"");
    ifNode14.FalseTarget = assignNode21;
    
    var assignNode22 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode14, 1828);
    assignNode22.IsDisabled = false; // default value
    assignNode22.Label = "Set description valid";
    
    var assignment36 = assignNode22.CreateAssignment("TextArea_Description.Valid", "True");
    ifNode14.TrueTarget = assignNode22;
    
    var ifNode15 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode22, 1600);
    ifNode15.SetCondition("GetProductInfo.ProductDetails.Product.Price > 0");
    ifNode15.IsDisabled = false; // default value
    ifNode15.Label = "Valid price?";
    assignNode21.Target = ifNode15;
    
    var assignNode23 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode15, 1828);
    assignNode23.IsDisabled = false; // default value
    assignNode23.Label = "Set price invalid";
    
    var assignment37 = assignNode23.CreateAssignment("ProdDetail_Price_Input.Valid", "False");
    
    var assignment38 = assignNode23.CreateAssignment("ProdDetail_Price_Input.ValidationMessage", "\"Price must be filled and higher than zero\"");
    ifNode15.FalseTarget = assignNode23;
    
    var assignNode24 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode15, 1600);
    assignNode24.IsDisabled = false; // default value
    assignNode24.Label = "Set price valid";
    
    var assignment39 = assignNode24.CreateAssignment("ProdDetail_Price_Input.Valid", "True");
    ifNode15.TrueTarget = assignNode24;
    
    var ifNode16 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode16.SetCondition("GetProductInfo.ProductDetails.Product.Discount >= 0 and\nGetProductInfo.ProductDetails.Product.Discount <= 100");
    ifNode16.HorizontalPosition = 4571;
    ifNode16.IsDisabled = false; // default value
    ifNode16.Label = "Valid discount?";
    ifNode16.VerticalPosition = 14629;
    assignNode23.Target = ifNode16;
    assignNode24.Target = ifNode16;
    
    var assignNode25 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode16, 1829);
    assignNode25.IsDisabled = false; // default value
    assignNode25.Label = "Set discount invalid";
    
    var assignment40 = assignNode25.CreateAssignment("ProdDetail_Discount_Input.Valid", "False");
    
    var assignment41 = assignNode25.CreateAssignment("ProdDetail_Discount_Input.ValidationMessage", "\"Discount value not valid\"");
    ifNode16.FalseTarget = assignNode25;
    
    var assignNode26 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode16, 1600);
    assignNode26.IsDisabled = false; // default value
    assignNode26.Label = "Set discount valid";
    
    var assignment42 = assignNode26.CreateAssignment("ProdDetail_Discount_Input.Valid", "True");
    ifNode16.TrueTarget = assignNode26;
    
    var ifNode17 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode26, 1600);
    ifNode17.SetCondition("GetProductInfo.ProductDetails.Product.Stock >= 0");
    ifNode17.IsDisabled = false; // default value
    ifNode17.Label = "Valid stock?";
    assignNode25.Target = ifNode17;
    
    var assignNode27 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode17, 1829);
    assignNode27.IsDisabled = false; // default value
    assignNode27.Label = "Set stock invalid";
    
    var assignment43 = assignNode27.CreateAssignment("ProdDetail_Stock_Input.Valid", "False");
    
    var assignment44 = assignNode27.CreateAssignment("ProdDetail_Stock_Input.ValidationMessage", "\"Stock can\'t be negative\"");
    ifNode17.FalseTarget = assignNode27;
    
    var assignNode28 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode17, 1600);
    assignNode28.IsDisabled = false; // default value
    assignNode28.Label = "Set stock valid";
    
    var assignment45 = assignNode28.CreateAssignment("ProdDetail_Stock_Input.Valid", "True");
    ifNode17.TrueTarget = assignNode28;
    
    var endNode16 = product_GenericInfoFormValidation.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode28, 1600);
    endNode16.IsDisabled = false; // default value
    assignNode27.Target = endNode16;
    
    var product_OnCancel = productDetail.CreateScreenAction("Product_OnCancel");
    product_OnCancel.Description = "Cancel the editing mode and ignore all the changes.";
    
    var startNode12 = product_OnCancel.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode12.IsDisabled = false; // default value
    
    var ifNode18 = product_OnCancel.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode18.SetCondition("ProductId = NullIdentifier()");
    ifNode18.HorizontalPosition = 3192;
    ifNode18.IsDisabled = false; // default value
    ifNode18.Label = "New Product?";
    ifNode18.VerticalPosition = 2508;
    startNode12.Target = ifNode18;
    
    var clear_DeleteLists = product_OnCancel.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("Clear_DeleteLists").Below(ifNode18, 1824);
    clear_DeleteLists.Action = clear_DeleteList;
    clear_DeleteLists.IsDisabled = false; // default value
    ifNode18.FalseTarget = clear_DeleteLists;
    
    var destinationNode = product_OnCancel.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode18, 1596);
    var products = product.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Products");
    destinationNode.Destination = products;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    ifNode18.TrueTarget = destinationNode;
    
    var assignNode29 = product_OnCancel.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode29.HorizontalPosition = 3199;
    assignNode29.IsDisabled = false; // default value
    assignNode29.Label = null; // default value
    assignNode29.VerticalPosition = 5700;
    
    var assignment46 = assignNode29.CreateAssignment("IsEditingMode", "not IsEditingMode");
    clear_DeleteLists.Target = assignNode29;
    
    var refreshDataNode3 = product_OnCancel.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode29, 1596);
    refreshDataNode3.DataSource = getProductInfo;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords(null); // default value
    refreshDataNode3.SetStartIndex(null); // default value
    
    var endNode17 = product_OnCancel.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode17.HorizontalPosition = 3196;
    endNode17.IsDisabled = false; // default value
    endNode17.VerticalPosition = 9120;
    refreshDataNode3.Target = endNode17;
    
    var product_OnSave = productDetail.CreateScreenAction("Product_OnSave");
    product_OnSave.Description = "Trigger the saving changes behaviours.";
    
    var startNode13 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode13.HorizontalPosition = 3429;
    startNode13.IsDisabled = false; // default value
    startNode13.VerticalPosition = 914;
    
    var assignNode30 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode13, 1600);
    assignNode30.IsDisabled = false; // default value
    assignNode30.Label = null; // default value
    
    var assignment47 = assignNode30.CreateAssignment("IsSaving", "True");
    
    var assignNode31 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(assignNode30, 1600);
    assignNode31.IsDisabled = false; // default value
    assignNode31.Label = "Round Values to 2 Decimal Digits";
    
    var assignment48 = assignNode31.CreateAssignment("GetProductInfo.ProductDetails.Product.Price", "Round(GetProductInfo.ProductDetails.Product.Price, 2)\n");
    
    var assignment49 = assignNode31.CreateAssignment("GetProductInfo.ProductDetails.Product.Discount", "Round(GetProductInfo.ProductDetails.Product.Discount, 2)");
    
    var product_GenericInfoFormValidation2 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("Product_GenericInfoFormValidation").ConnectedBelow(assignNode31, 1829);
    product_GenericInfoFormValidation2.Action = product_GenericInfoFormValidation;
    product_GenericInfoFormValidation2.IsDisabled = false; // default value
    
    var ifNode19 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(product_GenericInfoFormValidation2, 1828);
    ifNode19.SetCondition("Trim(GetProductInfo.ProductDetails.Product.Reference) <> NullTextIdentifier()");
    ifNode19.IsDisabled = false; // default value
    ifNode19.Label = "Valid reference?";
    
    var assignNode32 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode19, 1371);
    assignNode32.IsDisabled = false; // default value
    assignNode32.Label = "Set reference invalid";
    
    var assignment50 = assignNode32.CreateAssignment("ProdDetail_REF_Input.Valid", "False");
    
    var assignment51 = assignNode32.CreateAssignment("ProdDetail_REF_Input.ValidationMessage", "\"Reference must be filled\"");
    ifNode19.FalseTarget = assignNode32;
    
    var assignNode33 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode19, 1600);
    assignNode33.IsDisabled = false; // default value
    assignNode33.Label = "Set Reference valid";
    
    var assignment52 = assignNode33.CreateAssignment("ProdDetail_REF_Input.Valid", "True");
    ifNode19.TrueTarget = assignNode33;
    
    var listAny = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAny");
    var listAny2 = _System_.ClientActions.Named("ListAny");
    listAny.Action = listAny2;
    listAny.HorizontalPosition = 3420;
    listAny.IsDisabled = false; // default value
    listAny.VerticalPosition = 11172;
    var list6 = listAny2.InputParameters.Named("List");
    listAny.SetArgumentValue(list6, "GetProductInfo.ProductPictures");
    var condition2 = listAny2.InputParameters.Named("Condition");
    listAny.SetArgumentValue(condition2, "IsPlaceholder = False");
    assignNode32.Target = listAny;
    assignNode33.Target = listAny;
    
    var ifNode20 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode20.SetCondition("not ListAny.Result");
    ifNode20.HorizontalPosition = 3429;
    ifNode20.IsDisabled = false; // default value
    ifNode20.Label = "No Images?";
    ifNode20.VerticalPosition = 12996;
    listAny.Target = ifNode20;
    
    var ifNode21 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode20, 1824);
    ifNode21.SetCondition("ProductDetail_SpecificationsInfoForm.Valid and\nProductDetail_GeneralInfoForm.Valid and\nListAny.Result");
    ifNode21.IsDisabled = false; // default value
    ifNode21.Label = "Forms Valid?";
    ifNode20.FalseTarget = ifNode21;
    
    var messageNode = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ToTheRightOf(ifNode20, 1371);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("\"Please upload at least one image for the cover of the product\"");
    messageNode.Target = ifNode21;
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Info; // default value
    ifNode20.TrueTarget = messageNode;
    
    var assignNode34 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode21, 1371);
    assignNode34.IsDisabled = false; // default value
    assignNode34.Label = null; // default value
    
    var assignment53 = assignNode34.CreateAssignment("IsSaving", "False");
    ifNode21.FalseTarget = assignNode34;
    
    var assignNode35 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode21, 1824);
    assignNode35.IsDisabled = false; // default value
    assignNode35.Label = "Force cover image";
    
    var assignment54 = assignNode35.CreateAssignment("GetProductInfo.ProductPictures[0].ProductPicture.IsCover", "True");
    ifNode21.TrueTarget = assignNode35;
    
    var endNode18 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedToTheRightOf(assignNode34, 1371);
    endNode18.IsDisabled = false; // default value
    
    var getImagesToSave = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("GetImagesToSave").ConnectedBelow(assignNode35, 1596);
    getImagesToSave.Action = listFilter;
    getImagesToSave.IsDisabled = false; // default value
    getImagesToSave.SetArgumentValue(condition, "IsNew or IsModified or ProductPicture.Id <> NullIdentifier()");
    getImagesToSave.SetArgumentValue(sourceList2, "GetProductInfo.ProductPictures");
    
    var getFeaturesToSave = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("GetFeaturesToSave").ConnectedBelow(getImagesToSave, 1368);
    getFeaturesToSave.Action = listFilter;
    getFeaturesToSave.IsDisabled = false; // default value
    getFeaturesToSave.SetArgumentValue(sourceList2, "GetProductInfo.ProductFeatures");
    getFeaturesToSave.SetArgumentValue(condition, "IsNew or IsModified or Id <> NullIdentifier()");
    
    var assignNode36 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getFeaturesToSave, 1596);
    assignNode36.IsDisabled = false; // default value
    assignNode36.Label = "Reformat Discount to save";
    
    var assignment55 = assignNode36.CreateAssignment("GetProductInfo.ProductDetails.Product.Discount", "GetProductInfo.ProductDetails.Product.Discount / 100");
    
    var product_Edit = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Product_Edit").ConnectedBelow(assignNode36, 1596);
    var product_Edit2 = orderManagement_CS.ServerActions.Named("Product_Edit");
    product_Edit.Action = product_Edit2;
    product_Edit.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    product_Edit.IsDisabled = false; // default value
    product_Edit.ServerRequestTimeout = null; // default value
    var deletedFeatures = product_Edit2.InputParameters.Named("DeletedFeatures");
    product_Edit.SetArgumentValue(deletedFeatures, "DeleteFeatures");
    var product4 = product_Edit2.InputParameters.Named("Product");
    product_Edit.SetArgumentValue(product4, "GetProductInfo.ProductDetails.Product");
    var deletedProductPictures = product_Edit2.InputParameters.Named("DeletedProductPictures");
    product_Edit.SetArgumentValue(deletedProductPictures, "DeleteImages");
    var productPictures2 = product_Edit2.InputParameters.Named("ProductPictures");
    product_Edit.SetArgumentValue(productPictures2, "GetImagesToSave.FilteredList mapTo { Value: ProductPicture }");
    var productFeatures2 = product_Edit2.InputParameters.Named("ProductFeatures");
    product_Edit.SetArgumentValue(productFeatures2, "GetFeaturesToSave.FilteredList mapTo { Id: Id, ProductId: GetProductInfo.ProductDetails.Product.Id, Description: Description, IsActive: True }");
    
    var assignNode37 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(product_Edit, 1824);
    assignNode37.IsDisabled = false; // default value
    assignNode37.Label = "Reformat Discount to show on screen";
    
    var assignment56 = assignNode37.CreateAssignment("GetProductInfo.ProductDetails.Product.Discount", "GetProductInfo.ProductDetails.Product.Discount * 100");
    
    var clear_DeleteLists2 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("Clear_DeleteLists").ConnectedBelow(assignNode37, 1140);
    clear_DeleteLists2.Action = clear_DeleteList;
    clear_DeleteLists2.IsDisabled = false; // default value
    
    var refreshDataNode4 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(clear_DeleteLists2, 1368);
    refreshDataNode4.DataSource = getProductInfo;
    refreshDataNode4.IsDisabled = false; // default value
    refreshDataNode4.SetMaxRecords(null); // default value
    refreshDataNode4.SetStartIndex(null); // default value
    
    var messageNode2 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode4, 1368);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = null; // default value
    messageNode2.SetMessage("\"Product updated successfully.\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var ifNode22 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode22.SetCondition("ProductId = NullIdentifier()");
    ifNode22.HorizontalPosition = 3420;
    ifNode22.IsDisabled = false; // default value
    ifNode22.Label = "New Product?";
    ifNode22.VerticalPosition = 29868;
    messageNode2.Target = ifNode22;
    
    var assignNode38 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode38.HorizontalPosition = 3429;
    assignNode38.IsDisabled = false; // default value
    assignNode38.Label = null; // default value
    assignNode38.VerticalPosition = 31236;
    
    var assignment57 = assignNode38.CreateAssignment("IsEditingMode", "not IsEditingMode");
    ifNode22.FalseTarget = assignNode38;
    
    var destinationNode2 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>();
    destinationNode2.Destination = products;
    destinationNode2.HorizontalPosition = 5257;
    destinationNode2.IsDisabled = false; // default value
    destinationNode2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    destinationNode2.VerticalPosition = 29943;
    ifNode22.TrueTarget = destinationNode2;
    
    var assignNode39 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(assignNode38, 1368);
    assignNode39.IsDisabled = false; // default value
    assignNode39.Label = null; // default value
    
    var assignment58 = assignNode39.CreateAssignment("IsSaving", "False");
    
    var endNode19 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode39, 1368);
    endNode19.IsDisabled = false; // default value
    
    var allExceptions = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = true; // default value
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 11200;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 914;
    
    var messageNode3 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(allExceptions, 1600);
    messageNode3.IsDisabled = false; // default value
    messageNode3.Label = null; // default value
    messageNode3.SetMessage("\"An unexpected error occurred. Please try again later.\"");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var assignNode40 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(messageNode3, 1372);
    assignNode40.IsDisabled = false; // default value
    assignNode40.Label = null; // default value
    
    var assignment59 = assignNode40.CreateAssignment("IsSaving", "False");
    
    var endNode20 = product_OnSave.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode40, 1371);
    endNode20.IsDisabled = false; // default value
    
    var product_ToggleEdit = productDetail.CreateScreenAction("Product_ToggleEdit");
    product_ToggleEdit.Description = "Trigger the editing mode.";
    
    var startNode14 = product_ToggleEdit.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode14.IsDisabled = false; // default value
    
    var assignNode41 = product_ToggleEdit.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode41.HorizontalPosition = 3192;
    assignNode41.IsDisabled = false; // default value
    assignNode41.Label = null; // default value
    assignNode41.VerticalPosition = 2971;
    
    var assignment60 = assignNode41.CreateAssignment("IsEditingMode", "not IsEditingMode");
    startNode14.Target = assignNode41;
    
    var endNode21 = product_ToggleEdit.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode21.HorizontalPosition = 3196;
    endNode21.IsDisabled = false; // default value
    endNode21.VerticalPosition = 5120;
    assignNode41.Target = endNode21;
    
    var product_UpdateFinalPrice = productDetail.CreateScreenAction("Product_UpdateFinalPrice");
    product_UpdateFinalPrice.Description = null; // default value
    
    var startNode15 = product_UpdateFinalPrice.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode15.IsDisabled = false; // default value
    
    var ifNode23 = product_UpdateFinalPrice.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode23.SetCondition("GetProductInfo.ProductDetails.Product.Price >= 0");
    ifNode23.HorizontalPosition = 3200;
    ifNode23.IsDisabled = false; // default value
    ifNode23.Label = "Valid Price?";
    ifNode23.VerticalPosition = 2971;
    startNode15.Target = ifNode23;
    
    var assignNode42 = product_UpdateFinalPrice.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode23, 1371);
    assignNode42.IsDisabled = false; // default value
    assignNode42.Label = "Set Final Price";
    
    var assignment61 = assignNode42.CreateAssignment("GetProductInfo.ProductDetails.FinalPrice", "0");
    ifNode23.FalseTarget = assignNode42;
    
    var assignNode43 = product_UpdateFinalPrice.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode23, 1829);
    assignNode43.IsDisabled = false; // default value
    assignNode43.Label = "Set Final Price";
    
    var assignment62 = assignNode43.CreateAssignment("GetProductInfo.ProductDetails.FinalPrice", "Round(GetProductInfo.ProductDetails.Product.Price * (1 - (GetProductInfo.ProductDetails.Product.Discount/100)), 2)");
    ifNode23.TrueTarget = assignNode43;
    
    var endNode22 = product_UpdateFinalPrice.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedToTheRightOf(assignNode42, 1372);
    endNode22.IsDisabled = false; // default value
    
    var endNode23 = product_UpdateFinalPrice.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode43, 2286);
    endNode23.IsDisabled = false; // default value
    
    var productImage_OnDelete = productDetail.CreateScreenAction("ProductImage_OnDelete");
    productImage_OnDelete.Description = "Trigger the removing image behaviour.";
    
    var emptyImage = productImage_OnDelete.CreateLocalVariable("EmptyImage");
    emptyImage.DataType = eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("IsModified", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsNew", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("IsPlaceholder", eSpace.BooleanType), Tuple.Create<string, ITypeSignature>("ProductPicture", productPicture2));
    emptyImage.Description = "Returns a list of all product images by a given product id.";
    
    var startNode16 = productImage_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode16.IsDisabled = false; // default value
    
    var assignNode44 = productImage_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode44.HorizontalPosition = 3200;
    assignNode44.IsDisabled = false; // default value
    assignNode44.Label = "Set Empty Image";
    assignNode44.VerticalPosition = 2514;
    
    var assignment63 = assignNode44.CreateAssignment("EmptyImage.IsPlaceholder", "True");
    startNode16.Target = assignNode44;
    
    var ifNode24 = productImage_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode44, 1372);
    ifNode24.SetCondition("GetProductInfo.ProductPictures.Current.ProductPicture.Id = NullIdentifier()");
    ifNode24.IsDisabled = false; // default value
    ifNode24.Label = "Current ID is null?";
    
    var listAppendToDelete2 = productImage_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppendToDelete").ToTheRightOf(ifNode24, 1600);
    listAppendToDelete2.Action = listAppend2;
    listAppendToDelete2.IsDisabled = false; // default value
    listAppendToDelete2.SetArgumentValue(list3, "DeleteImages");
    listAppendToDelete2.SetArgumentValue(element2, "GetProductInfo.ProductPictures.Current.ProductPicture.Id");
    ifNode24.FalseTarget = listAppendToDelete2;
    
    var listAppendEmpty2 = productImage_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListAppendEmpty").Below(ifNode24, 1600);
    listAppendEmpty2.Action = listAppend2;
    listAppendEmpty2.IsDisabled = false; // default value
    listAppendEmpty2.SetArgumentValue(element2, "EmptyImage");
    listAppendEmpty2.SetArgumentValue(list3, "GetProductInfo.ProductPictures");
    ifNode24.TrueTarget = listAppendEmpty2;
    listAppendToDelete2.Target = listAppendEmpty2;
    
    var listRemoveOriginal2 = productImage_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListRemoveOriginal").ConnectedBelow(listAppendEmpty2, 1600);
    listRemoveOriginal2.Action = listRemove;
    listRemoveOriginal2.IsDisabled = false; // default value
    listRemoveOriginal2.SetArgumentValue(position, "GetProductInfo.ProductPictures.CurrentRowNumber");
    listRemoveOriginal2.SetArgumentValue(list4, "GetProductInfo.ProductPictures");
    
    var messageNode4 = productImage_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(listRemoveOriginal2, 1371);
    messageNode4.IsDisabled = false; // default value
    messageNode4.Label = null; // default value
    messageNode4.SetMessage("\"Image deleted successfully!\"");
    messageNode4.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode24 = productImage_OnDelete.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode4, 1600);
    endNode24.IsDisabled = false; // default value
    
    var productImage_OnEdit = productDetail.CreateScreenAction("ProductImage_OnEdit");
    productImage_OnEdit.Description = "Updates the product image with the uploaded file";
    
    var startNode17 = productImage_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode17.IsDisabled = false; // default value
    
    var assignNode45 = productImage_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode45.HorizontalPosition = 3200;
    assignNode45.IsDisabled = false; // default value
    assignNode45.Label = "Update ProductImage";
    assignNode45.VerticalPosition = 2971;
    
    var assignment64 = assignNode45.CreateAssignment("GetProductInfo.ProductPictures.Current.ProductPicture.Filetype", "\"application/octet-stream\"");
    
    var assignment65 = assignNode45.CreateAssignment("GetProductInfo.ProductPictures.Current.IsModified", "True");
    
    var assignment66 = assignNode45.CreateAssignment("GetProductInfo.ProductPictures.Current.IsNew", "False");
    
    var assignment67 = assignNode45.CreateAssignment("GetProductInfo.ProductPictures.Current.IsPlaceholder", "False");
    startNode17.Target = assignNode45;
    
    var endNode25 = productImage_OnEdit.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode45, 1600);
    endNode25.IsDisabled = false; // default value
    
    var productImage_OnNew = productDetail.CreateScreenAction("ProductImage_OnNew");
    productImage_OnNew.Description = "Trigger the upload new product image behaviour.";
    
    var startNode18 = productImage_OnNew.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode18.IsDisabled = false; // default value
    
    var listDuplicate3 = productImage_OnNew.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListDuplicate");
    listDuplicate3.Action = listDuplicate2;
    listDuplicate3.HorizontalPosition = 3200;
    listDuplicate3.IsDisabled = false; // default value
    listDuplicate3.VerticalPosition = 2286;
    listDuplicate3.SetArgumentValue(sourceList, "GetProductInfo.ProductPictures");
    startNode18.Target = listDuplicate3;
    
    var listFilterNonPlaceholders2 = productImage_OnNew.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListFilterNonPlaceholders").ConnectedBelow(listDuplicate3, 1600);
    listFilterNonPlaceholders2.Action = listFilter;
    listFilterNonPlaceholders2.IsDisabled = false; // default value
    listFilterNonPlaceholders2.SetArgumentValue(condition, "not IsPlaceholder");
    listFilterNonPlaceholders2.SetArgumentValue(sourceList2, "ListDuplicate.DuplicatedList");
    
    var assignNode46 = productImage_OnNew.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(listFilterNonPlaceholders2, 1600);
    assignNode46.IsDisabled = false; // default value
    assignNode46.Label = "Update ProductImage";
    
    var assignment68 = assignNode46.CreateAssignment("GetProductInfo.ProductPictures.Current.ProductPicture.Filetype", "\"application/octet-stream\"");
    
    var assignment69 = assignNode46.CreateAssignment("GetProductInfo.ProductPictures.Current.IsModified", "False");
    
    var assignment70 = assignNode46.CreateAssignment("GetProductInfo.ProductPictures.Current.IsNew", "True");
    
    var assignment71 = assignNode46.CreateAssignment("GetProductInfo.ProductPictures.Current.IsPlaceholder", "False");
    
    var assignment72 = assignNode46.CreateAssignment("GetProductInfo.ProductPictures.Current.ProductPicture.ProductId", "GetProductInfo.ProductDetails.Product.Id");
    
    var ifNode25 = productImage_OnNew.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(assignNode46, 1371);
    ifNode25.SetCondition("ListFilterNonPlaceholders.FilteredList.Length = GetProductInfo.ProductPictures.CurrentRowNumber");
    ifNode25.IsDisabled = false; // default value
    ifNode25.Label = "Rearrange list?";
    
    var listInsert3 = productImage_OnNew.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListInsert").Below(ifNode25, 1600);
    listInsert3.Action = listInsert2;
    listInsert3.IsDisabled = false; // default value
    listInsert3.SetArgumentValue(position2, "ListFilterNonPlaceholders.FilteredList.Length");
    listInsert3.SetArgumentValue(element3, "GetProductInfo.ProductPictures.Current");
    listInsert3.SetArgumentValue(list5, "GetProductInfo.ProductPictures");
    ifNode25.FalseTarget = listInsert3;
    
    var endNode26 = productImage_OnNew.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode25, 1600);
    endNode26.IsDisabled = false; // default value
    ifNode25.TrueTarget = endNode26;
    
    var listRemove3 = productImage_OnNew.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ListRemove").ConnectedBelow(listInsert3, 1600);
    listRemove3.Action = listRemove;
    listRemove3.IsDisabled = false; // default value
    listRemove3.SetArgumentValue(position, "GetProductInfo.ProductPictures.CurrentRowNumber");
    listRemove3.SetArgumentValue(list4, "GetProductInfo.ProductPictures");
    
    var endNode27 = productImage_OnNew.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(listRemove3, 1600);
    endNode27.IsDisabled = false; // default value
    
    var layoutTopInstance = productDetail.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopInstance.CustomStyle = null; // default value
    layoutTopInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var layoutTop = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTop");
    layoutTopInstance.SourceBlock = layoutTop;
    // delete default content in Footer placeholder
    var placeholderContentWidget = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Footer");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    // delete default content in Header placeholder
    var placeholderContentWidget2 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget2.Widgets.ToList().ForEach(w => w.Delete());
    var hasFixedHeader = layoutTop.InputParameters.Named("HasFixedHeader");
    layoutTopInstance.SetArgumentValue(hasFixedHeader, null);
    
    var menuInstance = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeMenu = menu.InputParameters.Named("ActiveMenu");
    menuInstance.SetArgumentValue(activeMenu, null); // default value
    
    var placeholderContentWidget3 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Breadcrumbs");
    var link = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link.Widgets.ToList().ForEach(x => x.Delete());
    link.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link.ColumnGap = null; // default value
    link.ColumnSpan = null; // default value
    link.SetConfirmationMessage(null); // default value
    link.CustomStyle = null; // default value
    link.SetEnabled("True");
    link.FlexBasis = null; // default value
    link.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link.Gap = null;
    link.GridColumns = null; // default value
    link.GridRows = null; // default value
    link.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link.RowGap = null; // default value
    link.RowSpan = null; // default value
    link.SetStyle(null); // default value
    link.SetVisible("True"); // default value
    link.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link.Width = ""; // default value
    
    var iF_Desktop = link.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IF_Desktop");
    iF_Desktop.Animate = false; // default value
    iF_Desktop.SetCondition("IsDesktop()");
    iF_Desktop.CustomStyle = null; // default value
    iF_Desktop.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var goBack_IconDesktop = iF_Desktop.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>("GoBack_IconDesktop");
    goBack_IconDesktop.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    goBack_IconDesktop.ColumnSpan = null; // default value
    goBack_IconDesktop.CustomStyle = null; // default value
    goBack_IconDesktop.FlexBasis = null; // default value
    goBack_IconDesktop.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    goBack_IconDesktop.Icon = "angle-left";
    goBack_IconDesktop.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    goBack_IconDesktop.RowSpan = null; // default value
    goBack_IconDesktop.SetStyle("\"icon text-primary\"");
    goBack_IconDesktop.SetVisible("True"); // default value
    goBack_IconDesktop.Weight = null; // default value
    
    var textWidget = iF_Desktop.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses("\"padding-left-xs\"");
    textWidget.Text = "Back";
    
    var goBack_IconMobile = iF_Desktop.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>("GoBack_IconMobile");
    goBack_IconMobile.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    goBack_IconMobile.ColumnSpan = null; // default value
    goBack_IconMobile.CustomStyle = null; // default value
    goBack_IconMobile.FlexBasis = null; // default value
    goBack_IconMobile.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    goBack_IconMobile.Icon = "angle-left";
    goBack_IconMobile.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    goBack_IconMobile.RowSpan = null; // default value
    goBack_IconMobile.SetStyle("\"icon text-primary\"");
    goBack_IconMobile.SetVisible("True"); // default value
    goBack_IconMobile.Weight = null; // default value
    var builtinEvent = link.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = eSpace.PreviousScreen;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.UseHistory;
    
    var placeholderContentWidget4 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget2 = placeholderContentWidget4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Product details";
    
    var placeholderContentWidget5 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Actions");
    var productDetail_EditableActions = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_EditableActions");
    productDetail_EditableActions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_EditableActions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_EditableActions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_EditableActions.Animate = false; // default value
    productDetail_EditableActions.ColumnGap = null; // default value
    productDetail_EditableActions.ColumnSpan = null; // default value
    productDetail_EditableActions.CustomStyle = null; // default value
    productDetail_EditableActions.FlexBasis = null; // default value
    productDetail_EditableActions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_EditableActions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_EditableActions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_EditableActions.Gap = null;
    productDetail_EditableActions.GridColumns = null; // default value
    productDetail_EditableActions.GridRows = null; // default value
    productDetail_EditableActions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_EditableActions.RowGap = null; // default value
    productDetail_EditableActions.RowSpan = null; // default value
    productDetail_EditableActions.SetStyle("\"flex1 text-align-right\"");
    productDetail_EditableActions.SetVisible("True"); // default value
    productDetail_EditableActions.VisibleWasExpression = false; // default value
    productDetail_EditableActions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_EditableActions.Width = "(fill parent)"; // default value
    
    var isNotInEditingMode = productDetail_EditableActions.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsNotInEditingMode");
    isNotInEditingMode.Animate = false; // default value
    isNotInEditingMode.SetCondition("not IsEditingMode");
    isNotInEditingMode.CustomStyle = null; // default value
    isNotInEditingMode.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var button_Edit = isNotInEditingMode.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("Button_Edit");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    button_Edit.Widgets.ToList().ForEach(x => x.Delete());
    button_Edit.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button_Edit.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button_Edit.ColumnGap = null; // default value
    button_Edit.ColumnSpan = null; // default value
    button_Edit.SetConfirmationMessage(null); // default value
    button_Edit.CustomStyle = null; // default value
    button_Edit.SetEnabled("True");
    button_Edit.FlexBasis = null; // default value
    button_Edit.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button_Edit.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button_Edit.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button_Edit.Gap = null;
    button_Edit.GridColumns = null; // default value
    button_Edit.GridRows = null; // default value
    button_Edit.IsDefault = false; // default value
    button_Edit.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button_Edit.RowGap = null; // default value
    button_Edit.RowSpan = null; // default value
    button_Edit.SetStyle("\"btn\""); // default value
    button_Edit.SetVisible("True"); // default value
    button_Edit.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button_Edit.Width = ""; // default value
    
    var icon = button_Edit.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon.ColumnSpan = null; // default value
    icon.CustomStyle = null; // default value
    icon.FlexBasis = null; // default value
    icon.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon.Icon = "pencil";
    icon.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon.RowSpan = null; // default value
    icon.SetStyle("\"icon margin-right-s\"");
    icon.SetVisible("True"); // default value
    icon.Weight = null; // default value
    
    var textWidget3 = button_Edit.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Edit";
    var builtinEvent2 = button_Edit.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = product_ToggleEdit;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container = isNotInEditingMode.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container.Animate = false; // default value
    container.ColumnGap = null; // default value
    container.ColumnSpan = null; // default value
    container.CustomStyle = null; // default value
    container.FlexBasis = null; // default value
    container.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container.Gap = null;
    container.GridColumns = null; // default value
    container.GridRows = null; // default value
    container.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container.RowGap = null; // default value
    container.RowSpan = null; // default value
    container.SetStyle("If(not IsPhone(), \"display-flex justify-content-flex-end\", \"\")");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var button_Cancel = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("Button_Cancel");
    button_Cancel.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button_Cancel.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button_Cancel.ColumnGap = null; // default value
    button_Cancel.ColumnSpan = null; // default value
    button_Cancel.SetConfirmationMessage(null); // default value
    button_Cancel.CustomStyle = null; // default value
    button_Cancel.SetEnabled("True");
    button_Cancel.FlexBasis = null; // default value
    button_Cancel.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button_Cancel.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button_Cancel.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button_Cancel.Gap = null;
    button_Cancel.GridColumns = null; // default value
    button_Cancel.GridRows = null; // default value
    button_Cancel.IsDefault = false; // default value
    button_Cancel.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button_Cancel.RowGap = null; // default value
    button_Cancel.RowSpan = null; // default value
    button_Cancel.SetStyle("\"btn text-neutral-7 margin-right-base\"");
    button_Cancel.SetVisible("True"); // default value
    button_Cancel.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button_Cancel.Width = ""; // default value
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button_Cancel.Widgets.First();
    textWidget4.CustomStyle = null; // default value
    textWidget4.Name = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Cancel";
    var builtinEvent3 = button_Cancel.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = product_OnCancel;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container2 = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container2.Animate = false; // default value
    container2.ColumnGap = null; // default value
    container2.ColumnSpan = null; // default value
    container2.CustomStyle = null; // default value
    container2.FlexBasis = null; // default value
    container2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container2.Gap = null;
    container2.GridColumns = null; // default value
    container2.GridRows = null; // default value
    container2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container2.RowGap = null; // default value
    container2.RowSpan = null; // default value
    container2.SetStyle("If(not IsPhone(), \"\", \"margin-top-base\")");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var buttonLoadingInstance = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    buttonLoadingInstance.CustomStyle = null; // default value
    buttonLoadingInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var utilities = outSystemsUI.MobileFlows.Named("Utilities");
    var buttonLoading = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ButtonLoading");
    buttonLoadingInstance.SourceBlock = buttonLoading;
    // delete default content in Button placeholder
    var placeholderContentWidget6 = buttonLoadingInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Button");
    placeholderContentWidget6.Widgets.ToList().ForEach(w => w.Delete());
    var showLabelOnLoading = buttonLoading.InputParameters.Named("ShowLabelOnLoading");
    buttonLoadingInstance.SetArgumentValue(showLabelOnLoading, null);
    var isLoading = buttonLoading.InputParameters.Named("IsLoading");
    buttonLoadingInstance.SetArgumentValue(isLoading, "IsSaving");
    var extendedClass = buttonLoading.InputParameters.Named("ExtendedClass");
    buttonLoadingInstance.SetArgumentValue(extendedClass, null); // default value
    var eventHandler = buttonLoadingInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler.Handler = null; // default value
    
    var button = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    button.Widgets.ToList().ForEach(x => x.Delete());
    button.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button.ColumnGap = null; // default value
    button.ColumnSpan = null; // default value
    button.SetConfirmationMessage(null); // default value
    button.CustomStyle = null; // default value
    button.SetEnabled("True");
    button.FlexBasis = null; // default value
    button.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button.Gap = null;
    button.GridColumns = null; // default value
    button.GridRows = null; // default value
    button.IsDefault = false; // default value
    button.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button.RowGap = null; // default value
    button.RowSpan = null; // default value
    button.SetStyle("\"btn btn-primary\"");
    button.SetVisible("True"); // default value
    button.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button.Width = "(fill parent)";
    
    var container3 = button.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container3.Animate = false; // default value
    container3.ColumnGap = null; // default value
    container3.ColumnSpan = null; // default value
    container3.CustomStyle = null; // default value
    container3.FlexBasis = null; // default value
    container3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container3.Gap = null;
    container3.GridColumns = null; // default value
    container3.GridRows = null; // default value
    container3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container3.RowGap = null; // default value
    container3.RowSpan = null; // default value
    container3.SetStyle("\"osui-btn-loading__spinner-animation\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var textWidget5 = button.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Save";
    var builtinEvent4 = button.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = product_OnSave;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var placeholderContentWidget7 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var productDetail_Wrapper = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_Wrapper");
    productDetail_Wrapper.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_Wrapper.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_Wrapper.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_Wrapper.Animate = false; // default value
    productDetail_Wrapper.ColumnGap = null; // default value
    productDetail_Wrapper.ColumnSpan = null; // default value
    productDetail_Wrapper.CustomStyle = null; // default value
    productDetail_Wrapper.FlexBasis = null; // default value
    productDetail_Wrapper.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_Wrapper.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_Wrapper.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_Wrapper.Gap = null;
    productDetail_Wrapper.GridColumns = null; // default value
    productDetail_Wrapper.GridRows = null; // default value
    productDetail_Wrapper.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_Wrapper.RowGap = null; // default value
    productDetail_Wrapper.RowSpan = null; // default value
    productDetail_Wrapper.SetStyle(null); // default value
    productDetail_Wrapper.SetVisible("True"); // default value
    productDetail_Wrapper.VisibleWasExpression = false; // default value
    productDetail_Wrapper.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_Wrapper.Width = "(fill parent)"; // default value
    
    var iF_IsFetchingDataOrHasDataToShowContent = productDetail_Wrapper.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IF_IsFetchingDataOrHasDataToShowContent");
    iF_IsFetchingDataOrHasDataToShowContent.Animate = false; // default value
    iF_IsFetchingDataOrHasDataToShowContent.SetCondition("not GetProductInfo.IsDataFetched or\nProductId = NullIdentifier() or\n(GetProductInfo.IsDataFetched and GetProductInfo.ProductDetails.Product.Id <> NullIdentifier())");
    iF_IsFetchingDataOrHasDataToShowContent.CustomStyle = null; // default value
    iF_IsFetchingDataOrHasDataToShowContent.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var productDetail_InfoTitle = iF_IsFetchingDataOrHasDataToShowContent.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_InfoTitle");
    productDetail_InfoTitle.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_InfoTitle.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_InfoTitle.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_InfoTitle.Animate = false; // default value
    productDetail_InfoTitle.ColumnGap = null; // default value
    productDetail_InfoTitle.ColumnSpan = null; // default value
    productDetail_InfoTitle.CustomStyle = null; // default value
    productDetail_InfoTitle.FlexBasis = null; // default value
    productDetail_InfoTitle.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_InfoTitle.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_InfoTitle.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_InfoTitle.Gap = null;
    productDetail_InfoTitle.GridColumns = null; // default value
    productDetail_InfoTitle.GridRows = null; // default value
    productDetail_InfoTitle.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_InfoTitle.RowGap = null; // default value
    productDetail_InfoTitle.RowSpan = null; // default value
    productDetail_InfoTitle.SetStyle("\"margin-bottom-m\" + If(not IsPhone(), \" display-flex\", \"\")");
    productDetail_InfoTitle.SetVisible("True"); // default value
    productDetail_InfoTitle.VisibleWasExpression = false; // default value
    productDetail_InfoTitle.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_InfoTitle.Width = "(fill parent)"; // default value
    
    var productDetail_Subtitle = productDetail_InfoTitle.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_Subtitle");
    productDetail_Subtitle.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_Subtitle.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_Subtitle.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_Subtitle.Animate = false; // default value
    productDetail_Subtitle.ColumnGap = null; // default value
    productDetail_Subtitle.ColumnSpan = null; // default value
    productDetail_Subtitle.CustomStyle = null; // default value
    productDetail_Subtitle.FlexBasis = null; // default value
    productDetail_Subtitle.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_Subtitle.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_Subtitle.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_Subtitle.Gap = null;
    productDetail_Subtitle.GridColumns = null; // default value
    productDetail_Subtitle.GridRows = null; // default value
    productDetail_Subtitle.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_Subtitle.RowGap = null; // default value
    productDetail_Subtitle.RowSpan = null; // default value
    productDetail_Subtitle.SetStyle("\"heading4\" + If(not IsPhone(), \" flex1\", \"\")");
    productDetail_Subtitle.SetVisible("True"); // default value
    productDetail_Subtitle.VisibleWasExpression = false; // default value
    productDetail_Subtitle.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_Subtitle.Width = "(fill parent)"; // default value
    
    var textWidget6 = productDetail_Subtitle.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "General info";
    
    var productDetail_ProductIsActive = productDetail_InfoTitle.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_ProductIsActive");
    productDetail_ProductIsActive.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_ProductIsActive.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_ProductIsActive.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_ProductIsActive.Animate = false; // default value
    productDetail_ProductIsActive.ColumnGap = null; // default value
    productDetail_ProductIsActive.ColumnSpan = null; // default value
    productDetail_ProductIsActive.CustomStyle = null; // default value
    productDetail_ProductIsActive.FlexBasis = null; // default value
    productDetail_ProductIsActive.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_ProductIsActive.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_ProductIsActive.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_ProductIsActive.Gap = null;
    productDetail_ProductIsActive.GridColumns = null; // default value
    productDetail_ProductIsActive.GridRows = null; // default value
    productDetail_ProductIsActive.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_ProductIsActive.RowGap = null; // default value
    productDetail_ProductIsActive.RowSpan = null; // default value
    productDetail_ProductIsActive.SetStyle("\"font-size-base display-flex align-items-center\"");
    productDetail_ProductIsActive.SetVisible("True"); // default value
    productDetail_ProductIsActive.VisibleWasExpression = false; // default value
    productDetail_ProductIsActive.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_ProductIsActive.Width = "(fill parent)"; // default value
    
    var container4 = productDetail_ProductIsActive.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container4.Animate = false; // default value
    container4.ColumnGap = null; // default value
    container4.ColumnSpan = null; // default value
    container4.CustomStyle = null; // default value
    container4.FlexBasis = null; // default value
    container4.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container4.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container4.Gap = null;
    container4.GridColumns = null; // default value
    container4.GridRows = null; // default value
    container4.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container4.RowGap = null; // default value
    container4.RowSpan = null; // default value
    container4.SetStyle("\"flex1\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var textWidget7 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Product is active";
    
    var switch1 = productDetail_ProductIsActive.CreateWidget<ServiceStudio.Plugin.NRWidgets.ISwitch>("Switch1");
    switch1.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    switch1.ColumnSpan = null; // default value
    switch1.CustomStyle = null; // default value
    switch1.SetEnabled("IsEditingMode");
    switch1.FlexBasis = null; // default value
    switch1.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    switch1.RowSpan = null; // default value
    switch1.SetStyle("\"switch\" + If(not IsPhone(), \" margin-left-base\", \" text-align-right\")");
    switch1.SetVariable("GetProductInfo.ProductDetails.Product.IsActive");
    var builtinEvent5 = switch1.OnChange;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = null; // default value
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var productDetail_GeneralInfoForm = iF_IsFetchingDataOrHasDataToShowContent.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IForm>("ProductDetail_GeneralInfoForm");
    productDetail_GeneralInfoForm.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_GeneralInfoForm.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_GeneralInfoForm.ColumnGap = null; // default value
    productDetail_GeneralInfoForm.ColumnSpan = null; // default value
    productDetail_GeneralInfoForm.CustomStyle = null; // default value
    productDetail_GeneralInfoForm.FlexBasis = null; // default value
    productDetail_GeneralInfoForm.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_GeneralInfoForm.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_GeneralInfoForm.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_GeneralInfoForm.Gap = null;
    productDetail_GeneralInfoForm.GridColumns = null; // default value
    productDetail_GeneralInfoForm.GridRows = null; // default value
    productDetail_GeneralInfoForm.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_GeneralInfoForm.RowGap = null; // default value
    productDetail_GeneralInfoForm.RowSpan = null; // default value
    productDetail_GeneralInfoForm.SetStyle("\"form card font-size-base padding-bottom-s margin-bottom-l\"");
    productDetail_GeneralInfoForm.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_GeneralInfoForm.Width = "(fill parent)"; // default value
    
    var columns2Instance = productDetail_GeneralInfoForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance.CustomStyle = null; // default value
    columns2Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var adaptive = outSystemsUI.MobileFlows.Named("Adaptive");
    var columns2 = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Columns2");
    columns2Instance.SourceBlock = columns2;
    var tabletBehavior = columns2.InputParameters.Named("TabletBehavior");
    columns2Instance.SetArgumentValue(tabletBehavior, null);
    var gutterSize = columns2.InputParameters.Named("GutterSize");
    columns2Instance.SetArgumentValue(gutterSize, null);
    var phoneBehavior = columns2.InputParameters.Named("PhoneBehavior");
    columns2Instance.SetArgumentValue(phoneBehavior, "Entities.BreakColumns.All");
    var extendedClass2 = columns2.InputParameters.Named("ExtendedClass");
    columns2Instance.SetArgumentValue(extendedClass2, null); // default value
    
    var placeholderContentWidget8 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var prodDetail_Name = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Name");
    prodDetail_Name.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Name.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Name.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Name.Animate = false; // default value
    prodDetail_Name.ColumnGap = null; // default value
    prodDetail_Name.ColumnSpan = null; // default value
    prodDetail_Name.CustomStyle = null; // default value
    prodDetail_Name.FlexBasis = null; // default value
    prodDetail_Name.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Name.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Name.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Name.Gap = null;
    prodDetail_Name.GridColumns = null; // default value
    prodDetail_Name.GridRows = null; // default value
    prodDetail_Name.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Name.RowGap = null; // default value
    prodDetail_Name.RowSpan = null; // default value
    prodDetail_Name.SetStyle(null); // default value
    prodDetail_Name.SetVisible("True"); // default value
    prodDetail_Name.VisibleWasExpression = false; // default value
    prodDetail_Name.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Name.Width = "(fill parent)"; // default value
    
    var label = prodDetail_Name.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label.ColumnGap = null; // default value
    label.ColumnSpan = null; // default value
    label.CustomStyle = null; // default value
    label.FlexBasis = null; // default value
    label.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label.Gap = null;
    label.GridColumns = null; // default value
    label.GridRows = null; // default value
    label.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label.RowGap = null; // default value
    label.RowSpan = null; // default value
    label.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label.Width = "(fill parent)"; // default value
    
    var textWidget8 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label.Widgets.First();
    textWidget8.CustomStyle = null; // default value
    textWidget8.Name = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Product title *";
    
    var prodDetail_Name_Input = prodDetail_Name.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("ProdDetail_Name_Input");
    prodDetail_Name_Input.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Name_Input.ColumnSpan = null; // default value
    prodDetail_Name_Input.CustomStyle = null; // default value
    prodDetail_Name_Input.SetEnabled("IsEditingMode");
    prodDetail_Name_Input.FlexBasis = null; // default value
    prodDetail_Name_Input.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Name_Input.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    prodDetail_Name_Input.SetMandatory("True");
    prodDetail_Name_Input.MaxLength = 250;
    prodDetail_Name_Input.SetPrompt("\"Enter product title\"");
    prodDetail_Name_Input.RowSpan = null; // default value
    prodDetail_Name_Input.SetStyle("\"form-control\""); // default value
    prodDetail_Name_Input.SetVariable("GetProductInfo.ProductDetails.Product.Name");
    prodDetail_Name_Input.Width = "(fill parent)"; // default value
    var builtinEvent6 = prodDetail_Name_Input.OnChange;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = null; // default value
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label.TargetWidget = prodDetail_Name_Input;
    
    var placeholderContentWidget9 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var prodDetail_Category = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Category");
    prodDetail_Category.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Category.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Category.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Category.Animate = false; // default value
    prodDetail_Category.ColumnGap = null; // default value
    prodDetail_Category.ColumnSpan = null; // default value
    prodDetail_Category.CustomStyle = null; // default value
    prodDetail_Category.FlexBasis = null; // default value
    prodDetail_Category.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Category.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Category.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Category.Gap = null;
    prodDetail_Category.GridColumns = null; // default value
    prodDetail_Category.GridRows = null; // default value
    prodDetail_Category.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Category.RowGap = null; // default value
    prodDetail_Category.RowSpan = null; // default value
    prodDetail_Category.SetStyle(null); // default value
    prodDetail_Category.SetVisible("True"); // default value
    prodDetail_Category.VisibleWasExpression = false; // default value
    prodDetail_Category.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Category.Width = "(fill parent)"; // default value
    
    var label2 = prodDetail_Category.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label2.ColumnGap = null; // default value
    label2.ColumnSpan = null; // default value
    label2.CustomStyle = null; // default value
    label2.FlexBasis = null; // default value
    label2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label2.Gap = null;
    label2.GridColumns = null; // default value
    label2.GridRows = null; // default value
    label2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label2.RowGap = null; // default value
    label2.RowSpan = null; // default value
    label2.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label2.Width = "(fill parent)"; // default value
    
    var textWidget9 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label2.Widgets.First();
    textWidget9.CustomStyle = null; // default value
    textWidget9.Name = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "Category *";
    
    var prodDetail_Category_Select = prodDetail_Category.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("ProdDetail_Category_Select");
    prodDetail_Category_Select.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Category_Select.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Category_Select.ColumnGap = null; // default value
    prodDetail_Category_Select.ColumnSpan = null; // default value
    prodDetail_Category_Select.CustomStyle = null; // default value
    prodDetail_Category_Select.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Custom; // default value
    prodDetail_Category_Select.SetEmptyValue("\"Select category\"");
    prodDetail_Category_Select.SetEnabled("IsEditingMode");
    prodDetail_Category_Select.FlexBasis = null; // default value
    prodDetail_Category_Select.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Category_Select.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Category_Select.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Category_Select.Gap = null;
    prodDetail_Category_Select.GridColumns = null; // default value
    prodDetail_Category_Select.GridRows = null; // default value
    prodDetail_Category_Select.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Category_Select.SetLabels(null); // default value
    prodDetail_Category_Select.SetList("GetProductInfo.Categories");
    prodDetail_Category_Select.SetMandatory("True");
    prodDetail_Category_Select.RowGap = null; // default value
    prodDetail_Category_Select.RowSpan = null; // default value
    prodDetail_Category_Select.SetStyle("\"dropdown\""); // default value
    prodDetail_Category_Select.SetValues("Id");
    prodDetail_Category_Select.SetVariable("GetProductInfo.ProductDetails.Product.CategoryId");
    prodDetail_Category_Select.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Category_Select.Width = ""; // default value
    
    var expression = prodDetail_Category_Select.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetProductInfo.Categories.Current.Name");
    expression.Width = ""; // default value
    var builtinEvent7 = prodDetail_Category_Select.OnChange;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = null; // default value
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label2.TargetWidget = prodDetail_Category_Select;
    
    var prodDetail_Description = productDetail_GeneralInfoForm.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Description");
    prodDetail_Description.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Description.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Description.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Description.Animate = false; // default value
    prodDetail_Description.ColumnGap = null; // default value
    prodDetail_Description.ColumnSpan = null; // default value
    prodDetail_Description.CustomStyle = null; // default value
    prodDetail_Description.FlexBasis = null; // default value
    prodDetail_Description.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Description.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Description.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Description.Gap = null;
    prodDetail_Description.GridColumns = null; // default value
    prodDetail_Description.GridRows = null; // default value
    prodDetail_Description.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Description.RowGap = null; // default value
    prodDetail_Description.RowSpan = null; // default value
    prodDetail_Description.SetStyle(null); // default value
    prodDetail_Description.SetVisible("True"); // default value
    prodDetail_Description.VisibleWasExpression = false; // default value
    prodDetail_Description.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Description.Width = "(fill parent)"; // default value
    
    var label3 = prodDetail_Description.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label3.ColumnGap = null; // default value
    label3.ColumnSpan = null; // default value
    label3.CustomStyle = null; // default value
    label3.FlexBasis = null; // default value
    label3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label3.Gap = null;
    label3.GridColumns = null; // default value
    label3.GridRows = null; // default value
    label3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label3.RowGap = null; // default value
    label3.RowSpan = null; // default value
    label3.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label3.Width = "(fill parent)"; // default value
    
    var textWidget10 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label3.Widgets.First();
    textWidget10.CustomStyle = null; // default value
    textWidget10.Name = null; // default value
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Description *";
    
    var textArea_Description = prodDetail_Description.CreateWidget<ServiceStudio.Plugin.NRWidgets.ITextArea>("TextArea_Description");
    textArea_Description.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    textArea_Description.ColumnSpan = null; // default value
    textArea_Description.CustomStyle = null; // default value
    textArea_Description.SetEnabled("IsEditingMode");
    textArea_Description.FlexBasis = null; // default value
    textArea_Description.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    textArea_Description.SetMandatory("False");
    textArea_Description.MaxLength = 1000;
    textArea_Description.SetPrompt("\"Enter a description\"");
    textArea_Description.RowSpan = null; // default value
    textArea_Description.SetStyle("\"form-control\""); // default value
    textArea_Description.TextLines = 2;
    textArea_Description.SetVariable("GetProductInfo.ProductDetails.Product.Description");
    textArea_Description.Width = "(fill parent)"; // default value
    var builtinEvent8 = textArea_Description.OnChange;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = null; // default value
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label3.TargetWidget = textArea_Description;
    
    var columns2Instance2 = productDetail_GeneralInfoForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance2.CustomStyle = null; // default value
    columns2Instance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    columns2Instance2.SourceBlock = columns2;
    columns2Instance2.SetArgumentValue(gutterSize, null);
    columns2Instance2.SetArgumentValue(extendedClass2, null); // default value
    columns2Instance2.SetArgumentValue(phoneBehavior, "Entities.BreakColumns.All");
    columns2Instance2.SetArgumentValue(tabletBehavior, null);
    
    var placeholderContentWidget10 = columns2Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var prodDetail_Price = placeholderContentWidget10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Price");
    prodDetail_Price.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Price.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Price.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Price.Animate = false; // default value
    prodDetail_Price.ColumnGap = null; // default value
    prodDetail_Price.ColumnSpan = null; // default value
    prodDetail_Price.CustomStyle = null; // default value
    prodDetail_Price.FlexBasis = null; // default value
    prodDetail_Price.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Price.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Price.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Price.Gap = null;
    prodDetail_Price.GridColumns = null; // default value
    prodDetail_Price.GridRows = null; // default value
    prodDetail_Price.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Price.RowGap = null; // default value
    prodDetail_Price.RowSpan = null; // default value
    prodDetail_Price.SetStyle("\"margin-bottom-s\"");
    prodDetail_Price.SetVisible("True"); // default value
    prodDetail_Price.VisibleWasExpression = false; // default value
    prodDetail_Price.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Price.Width = "(fill parent)"; // default value
    
    var label4 = prodDetail_Price.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label4.ColumnGap = null; // default value
    label4.ColumnSpan = null; // default value
    label4.CustomStyle = null; // default value
    label4.FlexBasis = null; // default value
    label4.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label4.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label4.Gap = null;
    label4.GridColumns = null; // default value
    label4.GridRows = null; // default value
    label4.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label4.RowGap = null; // default value
    label4.RowSpan = null; // default value
    label4.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label4.Width = "(fill parent)"; // default value
    
    var textWidget11 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label4.Widgets.First();
    textWidget11.CustomStyle = null; // default value
    textWidget11.Name = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "Price *";
    
    var prodDetail_Price_Input = prodDetail_Price.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("ProdDetail_Price_Input");
    prodDetail_Price_Input.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Price_Input.ColumnSpan = null; // default value
    prodDetail_Price_Input.CustomStyle = null; // default value
    prodDetail_Price_Input.SetEnabled("IsEditingMode");
    prodDetail_Price_Input.FlexBasis = null; // default value
    prodDetail_Price_Input.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Price_Input.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Number;
    prodDetail_Price_Input.SetMandatory("True");
    prodDetail_Price_Input.MaxLength = null;
    prodDetail_Price_Input.SetPrompt("\"Enter product price\"");
    prodDetail_Price_Input.RowSpan = null; // default value
    prodDetail_Price_Input.SetStyle("\"form-control\""); // default value
    prodDetail_Price_Input.SetVariable("GetProductInfo.ProductDetails.Product.Price");
    prodDetail_Price_Input.Width = "(fill parent)"; // default value
    var builtinEvent9 = prodDetail_Price_Input.OnChange;
    builtinEvent9.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent9.Destination = product_UpdateFinalPrice;
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label4.TargetWidget = prodDetail_Price_Input;
    
    var placeholderContentWidget11 = columns2Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var prodDetail_Discount = placeholderContentWidget11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Discount");
    prodDetail_Discount.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Discount.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Discount.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Discount.Animate = false; // default value
    prodDetail_Discount.ColumnGap = null; // default value
    prodDetail_Discount.ColumnSpan = null; // default value
    prodDetail_Discount.CustomStyle = null; // default value
    prodDetail_Discount.FlexBasis = null; // default value
    prodDetail_Discount.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Discount.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Discount.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Discount.Gap = null;
    prodDetail_Discount.GridColumns = null; // default value
    prodDetail_Discount.GridRows = null; // default value
    prodDetail_Discount.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Discount.RowGap = null; // default value
    prodDetail_Discount.RowSpan = null; // default value
    prodDetail_Discount.SetStyle("\"margin-bottom-s\"");
    prodDetail_Discount.SetVisible("True"); // default value
    prodDetail_Discount.VisibleWasExpression = false; // default value
    prodDetail_Discount.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Discount.Width = "(fill parent)"; // default value
    
    var label5 = prodDetail_Discount.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label5.ColumnGap = null; // default value
    label5.ColumnSpan = null; // default value
    label5.CustomStyle = null; // default value
    label5.FlexBasis = null; // default value
    label5.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label5.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label5.Gap = null;
    label5.GridColumns = null; // default value
    label5.GridRows = null; // default value
    label5.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label5.RowGap = null; // default value
    label5.RowSpan = null; // default value
    label5.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label5.Width = "(fill parent)"; // default value
    
    var textWidget12 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label5.Widgets.First();
    textWidget12.CustomStyle = null; // default value
    textWidget12.Name = null; // default value
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "Discount (%)";
    
    var prodDetail_Discount_Input = prodDetail_Discount.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("ProdDetail_Discount_Input");
    prodDetail_Discount_Input.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Discount_Input.ColumnSpan = null; // default value
    prodDetail_Discount_Input.CustomStyle = null; // default value
    prodDetail_Discount_Input.SetEnabled("IsEditingMode");
    prodDetail_Discount_Input.FlexBasis = null; // default value
    prodDetail_Discount_Input.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Discount_Input.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Number;
    prodDetail_Discount_Input.SetMandatory("True");
    prodDetail_Discount_Input.MaxLength = null;
    prodDetail_Discount_Input.SetPrompt("\"Enter product discount\"");
    prodDetail_Discount_Input.RowSpan = null; // default value
    prodDetail_Discount_Input.SetStyle("\"form-control\""); // default value
    prodDetail_Discount_Input.SetVariable("GetProductInfo.ProductDetails.Product.Discount");
    prodDetail_Discount_Input.Width = "(fill parent)"; // default value
    var builtinEvent10 = prodDetail_Discount_Input.OnChange;
    builtinEvent10.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent10.Destination = product_UpdateFinalPrice;
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label5.TargetWidget = prodDetail_Discount_Input;
    
    var columns2Instance3 = productDetail_GeneralInfoForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance3.CustomStyle = null; // default value
    columns2Instance3.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    columns2Instance3.SourceBlock = columns2;
    columns2Instance3.SetArgumentValue(gutterSize, null);
    columns2Instance3.SetArgumentValue(phoneBehavior, "Entities.BreakColumns.All");
    columns2Instance3.SetArgumentValue(tabletBehavior, null);
    columns2Instance3.SetArgumentValue(extendedClass2, null); // default value
    
    var placeholderContentWidget12 = columns2Instance3.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var prodDetail_TotalPrice = placeholderContentWidget12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_TotalPrice");
    prodDetail_TotalPrice.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_TotalPrice.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_TotalPrice.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_TotalPrice.Animate = false; // default value
    prodDetail_TotalPrice.ColumnGap = null; // default value
    prodDetail_TotalPrice.ColumnSpan = null; // default value
    prodDetail_TotalPrice.CustomStyle = null; // default value
    prodDetail_TotalPrice.FlexBasis = null; // default value
    prodDetail_TotalPrice.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_TotalPrice.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_TotalPrice.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_TotalPrice.Gap = null;
    prodDetail_TotalPrice.GridColumns = null; // default value
    prodDetail_TotalPrice.GridRows = null; // default value
    prodDetail_TotalPrice.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_TotalPrice.RowGap = null; // default value
    prodDetail_TotalPrice.RowSpan = null; // default value
    prodDetail_TotalPrice.SetStyle(null); // default value
    prodDetail_TotalPrice.SetVisible("True"); // default value
    prodDetail_TotalPrice.VisibleWasExpression = false; // default value
    prodDetail_TotalPrice.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_TotalPrice.Width = "(fill parent)"; // default value
    
    var label6 = prodDetail_TotalPrice.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label6.ColumnGap = null; // default value
    label6.ColumnSpan = null; // default value
    label6.CustomStyle = null; // default value
    label6.FlexBasis = null; // default value
    label6.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label6.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label6.Gap = null;
    label6.GridColumns = null; // default value
    label6.GridRows = null; // default value
    label6.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label6.RowGap = null; // default value
    label6.RowSpan = null; // default value
    label6.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label6.Width = "(fill parent)"; // default value
    
    var textWidget13 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label6.Widgets.First();
    textWidget13.CustomStyle = null; // default value
    textWidget13.Name = null; // default value
    textWidget13.SetStyleClasses(null); // default value
    textWidget13.Text = "Final price";
    
    var prodDetail_TotalPrice_Input = prodDetail_TotalPrice.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("ProdDetail_TotalPrice_Input");
    prodDetail_TotalPrice_Input.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_TotalPrice_Input.ColumnSpan = null; // default value
    prodDetail_TotalPrice_Input.CustomStyle = null; // default value
    prodDetail_TotalPrice_Input.SetEnabled("False");
    prodDetail_TotalPrice_Input.FlexBasis = null; // default value
    prodDetail_TotalPrice_Input.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_TotalPrice_Input.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Number;
    prodDetail_TotalPrice_Input.SetMandatory("True");
    prodDetail_TotalPrice_Input.MaxLength = null;
    prodDetail_TotalPrice_Input.SetPrompt(null); // default value
    prodDetail_TotalPrice_Input.RowSpan = null; // default value
    prodDetail_TotalPrice_Input.SetStyle("\"form-control\""); // default value
    prodDetail_TotalPrice_Input.SetVariable("GetProductInfo.ProductDetails.FinalPrice");
    prodDetail_TotalPrice_Input.Width = "(fill parent)"; // default value
    var builtinEvent11 = prodDetail_TotalPrice_Input.OnChange;
    builtinEvent11.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent11.Destination = null; // default value
    builtinEvent11.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label6.TargetWidget = prodDetail_TotalPrice_Input;
    
    var placeholderContentWidget13 = columns2Instance3.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var prodDetail_Stock = placeholderContentWidget13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Stock");
    prodDetail_Stock.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Stock.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Stock.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Stock.Animate = false; // default value
    prodDetail_Stock.ColumnGap = null; // default value
    prodDetail_Stock.ColumnSpan = null; // default value
    prodDetail_Stock.CustomStyle = null; // default value
    prodDetail_Stock.FlexBasis = null; // default value
    prodDetail_Stock.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Stock.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Stock.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Stock.Gap = null;
    prodDetail_Stock.GridColumns = null; // default value
    prodDetail_Stock.GridRows = null; // default value
    prodDetail_Stock.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Stock.RowGap = null; // default value
    prodDetail_Stock.RowSpan = null; // default value
    prodDetail_Stock.SetStyle(null); // default value
    prodDetail_Stock.SetVisible("True"); // default value
    prodDetail_Stock.VisibleWasExpression = false; // default value
    prodDetail_Stock.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Stock.Width = "(fill parent)"; // default value
    
    var label7 = prodDetail_Stock.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label7.ColumnGap = null; // default value
    label7.ColumnSpan = null; // default value
    label7.CustomStyle = null; // default value
    label7.FlexBasis = null; // default value
    label7.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label7.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label7.Gap = null;
    label7.GridColumns = null; // default value
    label7.GridRows = null; // default value
    label7.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label7.RowGap = null; // default value
    label7.RowSpan = null; // default value
    label7.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label7.Width = "(fill parent)"; // default value
    
    var textWidget14 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label7.Widgets.First();
    textWidget14.CustomStyle = null; // default value
    textWidget14.Name = null; // default value
    textWidget14.SetStyleClasses(null); // default value
    textWidget14.Text = "Stock *";
    
    var prodDetail_Stock_Input = prodDetail_Stock.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("ProdDetail_Stock_Input");
    prodDetail_Stock_Input.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Stock_Input.ColumnSpan = null; // default value
    prodDetail_Stock_Input.CustomStyle = null; // default value
    prodDetail_Stock_Input.SetEnabled("IsEditingMode");
    prodDetail_Stock_Input.FlexBasis = null; // default value
    prodDetail_Stock_Input.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Stock_Input.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Number;
    prodDetail_Stock_Input.SetMandatory("True");
    prodDetail_Stock_Input.MaxLength = null;
    prodDetail_Stock_Input.SetPrompt("If((IsEditingMode and ProductId = NullIdentifier()), \"Enter product stock\", 0)");
    prodDetail_Stock_Input.RowSpan = null; // default value
    prodDetail_Stock_Input.SetStyle("\"form-control\""); // default value
    prodDetail_Stock_Input.SetVariable("GetProductInfo.ProductDetails.Product.Stock");
    prodDetail_Stock_Input.Width = "(fill parent)"; // default value
    var builtinEvent12 = prodDetail_Stock_Input.OnChange;
    builtinEvent12.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent12.Destination = null; // default value
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label7.TargetWidget = prodDetail_Stock_Input;
    
    var productDetail_ImagesTitle = iF_IsFetchingDataOrHasDataToShowContent.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_ImagesTitle");
    productDetail_ImagesTitle.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_ImagesTitle.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_ImagesTitle.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_ImagesTitle.Animate = false; // default value
    productDetail_ImagesTitle.ColumnGap = null; // default value
    productDetail_ImagesTitle.ColumnSpan = null; // default value
    productDetail_ImagesTitle.CustomStyle = null; // default value
    productDetail_ImagesTitle.FlexBasis = null; // default value
    productDetail_ImagesTitle.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_ImagesTitle.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_ImagesTitle.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_ImagesTitle.Gap = null;
    productDetail_ImagesTitle.GridColumns = null; // default value
    productDetail_ImagesTitle.GridRows = null; // default value
    productDetail_ImagesTitle.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_ImagesTitle.RowGap = null; // default value
    productDetail_ImagesTitle.RowSpan = null; // default value
    productDetail_ImagesTitle.SetStyle("\"margin-bottom-m heading4\"");
    productDetail_ImagesTitle.SetVisible("True"); // default value
    productDetail_ImagesTitle.VisibleWasExpression = false; // default value
    productDetail_ImagesTitle.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_ImagesTitle.Width = "(fill parent)"; // default value
    
    var textWidget15 = productDetail_ImagesTitle.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget15.CustomStyle = null; // default value
    textWidget15.SetStyleClasses(null); // default value
    textWidget15.Text = "Images";
    
    var productDetail_Images = iF_IsFetchingDataOrHasDataToShowContent.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("ProductDetail_Images");
    productDetail_Images.CustomStyle = null; // default value
    productDetail_Images.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var content = outSystemsUI.MobileFlows.Named("Content");
    var card = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Card");
    productDetail_Images.SourceBlock = card;
    var usePadding = card.InputParameters.Named("UsePadding");
    productDetail_Images.SetArgumentValue(usePadding, null);
    var extendedClass3 = card.InputParameters.Named("ExtendedClass");
    productDetail_Images.SetArgumentValue(extendedClass3, "\"margin-bottom-l\"");
    
    var placeholderContentWidget14 = productDetail_Images.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var ifWidget = placeholderContentWidget14.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.Animate = false; // default value
    ifWidget.SetCondition("GetProductInfo.IsDataFetched");
    ifWidget.CustomStyle = null; // default value
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var productDetail_ImagesGallery = ifWidget.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("ProductDetail_ImagesGallery");
    productDetail_ImagesGallery.CustomStyle = null; // default value
    productDetail_ImagesGallery.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var gallery = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Gallery");
    productDetail_ImagesGallery.SourceBlock = gallery;
    // delete default content in Content placeholder
    var placeholderContentWidget15 = productDetail_ImagesGallery.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    placeholderContentWidget15.Widgets.ToList().ForEach(w => w.Delete());
    var rowItemsDesktop = gallery.InputParameters.Named("RowItemsDesktop");
    productDetail_ImagesGallery.SetArgumentValue(rowItemsDesktop, "3");
    var itemsGap = gallery.InputParameters.Named("ItemsGap");
    productDetail_ImagesGallery.SetArgumentValue(itemsGap, null);
    var rowItemsTablet = gallery.InputParameters.Named("RowItemsTablet");
    productDetail_ImagesGallery.SetArgumentValue(rowItemsTablet, "3");
    var rowItemsPhone = gallery.InputParameters.Named("RowItemsPhone");
    productDetail_ImagesGallery.SetArgumentValue(rowItemsPhone, "1");
    var extendedClass4 = gallery.InputParameters.Named("ExtendedClass");
    productDetail_ImagesGallery.SetArgumentValue(extendedClass4, null); // default value
    var eventHandler2 = productDetail_ImagesGallery.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler2.Handler = null; // default value
    
    var productDetail_ImagesList = placeholderContentWidget15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("ProductDetail_ImagesList");
    productDetail_ImagesList.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_ImagesList.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_ImagesList.AnimateItems = false;
    productDetail_ImagesList.ColumnGap = null; // default value
    productDetail_ImagesList.ColumnSpan = null; // default value
    productDetail_ImagesList.CustomStyle = null; // default value
    productDetail_ImagesList.ExpandedInWebEditor = true; // default value
    productDetail_ImagesList.FlexBasis = null; // default value
    productDetail_ImagesList.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_ImagesList.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_ImagesList.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_ImagesList.Gap = null;
    productDetail_ImagesList.GridColumns = null; // default value
    productDetail_ImagesList.GridRows = null; // default value
    productDetail_ImagesList.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_ImagesList.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    productDetail_ImagesList.RowGap = null; // default value
    productDetail_ImagesList.RowSpan = null; // default value
    productDetail_ImagesList.SetSource("GetProductInfo.ProductPictures");
    productDetail_ImagesList.SetStyle("\"list list-group\""); // default value
    productDetail_ImagesList.Tag = "div"; // default value
    productDetail_ImagesList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_ImagesList.Width = "(fill parent)"; // default value
    
    var productImage_Item = productDetail_ImagesList.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductImage_Item");
    productImage_Item.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productImage_Item.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productImage_Item.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productImage_Item.Animate = false; // default value
    productImage_Item.ColumnGap = null; // default value
    productImage_Item.ColumnSpan = null; // default value
    productImage_Item.CustomStyle = null; // default value
    productImage_Item.FlexBasis = null; // default value
    productImage_Item.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productImage_Item.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productImage_Item.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productImage_Item.Gap = null;
    productImage_Item.GridColumns = null; // default value
    productImage_Item.GridRows = null; // default value
    productImage_Item.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productImage_Item.RowGap = null; // default value
    productImage_Item.RowSpan = null; // default value
    productImage_Item.SetStyle("\"position-relative\"");
    productImage_Item.SetVisible("True"); // default value
    productImage_Item.VisibleWasExpression = false; // default value
    productImage_Item.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productImage_Item.Width = "(fill parent)"; // default value
    
    var productImage_ImageWrapper = productImage_Item.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductImage_ImageWrapper");
    productImage_ImageWrapper.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productImage_ImageWrapper.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productImage_ImageWrapper.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productImage_ImageWrapper.Animate = false; // default value
    productImage_ImageWrapper.ColumnGap = null; // default value
    productImage_ImageWrapper.ColumnSpan = null; // default value
    productImage_ImageWrapper.CustomStyle = null; // default value
    productImage_ImageWrapper.FlexBasis = null; // default value
    productImage_ImageWrapper.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productImage_ImageWrapper.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productImage_ImageWrapper.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productImage_ImageWrapper.Gap = null;
    productImage_ImageWrapper.GridColumns = null; // default value
    productImage_ImageWrapper.GridRows = null; // default value
    productImage_ImageWrapper.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productImage_ImageWrapper.RowGap = null; // default value
    productImage_ImageWrapper.RowSpan = null; // default value
    productImage_ImageWrapper.SetStyle(null); // default value
    productImage_ImageWrapper.SetVisible("True"); // default value
    productImage_ImageWrapper.VisibleWasExpression = false; // default value
    productImage_ImageWrapper.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productImage_ImageWrapper.Width = "(fill parent)"; // default value
    
    var iF_HasImage = productImage_ImageWrapper.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IF_HasImage");
    iF_HasImage.Animate = false; // default value
    iF_HasImage.SetCondition("not GetProductInfo.ProductPictures.Current.IsPlaceholder");
    iF_HasImage.CustomStyle = null; // default value
    iF_HasImage.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var image = iF_HasImage.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image.ColumnSpan = null; // default value
    image.CustomStyle = null; // default value
    var emptyImage2 = eSpace.Images.Named("EmptyImage");
    image.DefaultImage = emptyImage2;
    image.FlexBasis = null; // default value
    image.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    image.Image = null; // default value
    image.SetImageContent("GetProductInfo.ProductPictures.Current.ProductPicture.File");
    image.RowSpan = null; // default value
    image.SetStyle("\"img-cover border-radius-soft border-size-s text-neutral-4\"");
    image.Type = ServiceStudio.Plugin.NRWidgets.Enumerations.Type.Binary;
    image.SetUrl(null); // default value
    image.Width = "(fill parent)";
    
    var extendedProperty = image.CreateExtendedProperty();
    extendedProperty.Property = "alt";
    extendedProperty.SetValue("If(GetProductInfo.ProductPictures.Current.ProductPicture.File <> NullBinary(),\n    GetProductInfo.ProductPictures.Current.ProductPicture.Filename + \" image\",\n    \"Product empty image\"\n)");
    
    var extendedProperty2 = image.CreateExtendedProperty();
    extendedProperty2.Property = "style";
    extendedProperty2.SetValue("If(not IsPhone(), \"height: 269px;\", \"height: 192px;\")");
    
    var uploadNewProdImage = iF_HasImage.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IUpload>("UploadNewProdImage");
    /*** The Upload Widget Accept property deletes and generates widgets inside it so it must be set immediately ***/
    uploadNewProdImage.Accept = ServiceStudio.Plugin.NRWidgets.Enumerations.Accept.Image;
    /*** Deleting Widgets that are created by default when instantiating an IUpload ***/
    uploadNewProdImage.Widgets.ToList().ForEach(x => x.Delete());
    uploadNewProdImage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    uploadNewProdImage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    uploadNewProdImage.ColumnGap = null; // default value
    uploadNewProdImage.ColumnSpan = null; // default value
    uploadNewProdImage.CustomStyle = null; // default value
    uploadNewProdImage.SetFileContent("GetProductInfo.ProductPictures.Current.ProductPicture.File");
    uploadNewProdImage.SetFileName("GetProductInfo.ProductPictures.Current.ProductPicture.Filename");
    uploadNewProdImage.FlexBasis = null; // default value
    uploadNewProdImage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    uploadNewProdImage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    uploadNewProdImage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    uploadNewProdImage.Gap = null;
    uploadNewProdImage.GridColumns = null; // default value
    uploadNewProdImage.GridRows = null; // default value
    uploadNewProdImage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    uploadNewProdImage.SetMandatory("False");
    uploadNewProdImage.RowGap = null; // default value
    uploadNewProdImage.RowSpan = null; // default value
    uploadNewProdImage.SetStyle("\"full-height display-flex center border-radius-soft\" + If(IsEditingMode, \"\", \" disabled background-neutral-2\")");
    uploadNewProdImage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    uploadNewProdImage.Width = "(fill parent)"; // default value
    
    var icon2 = uploadNewProdImage.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon2.ColumnSpan = null; // default value
    icon2.CustomStyle = null; // default value
    icon2.FlexBasis = null; // default value
    icon2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon2.Icon = "camera";
    icon2.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    icon2.RowSpan = null; // default value
    icon2.SetStyle("\"icon text-neutral-5\"");
    icon2.SetVisible("True"); // default value
    icon2.Weight = null; // default value
    
    var extendedProperty3 = icon2.CreateExtendedProperty();
    extendedProperty3.Property = "title";
    extendedProperty3.SetValue("\"click to upload an image\"");
    var builtinEvent13 = uploadNewProdImage.OnChange;
    builtinEvent13.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent13.Destination = productImage_OnNew;
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty4 = uploadNewProdImage.CreateExtendedProperty();
    extendedProperty4.Property = "style";
    extendedProperty4.SetValue("If(not IsPhone(), \"height: 269px;\", \"height: 192px;\")");
    
    var isFirstImage = productImage_Item.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsFirstImage");
    isFirstImage.Animate = false; // default value
    isFirstImage.SetCondition("GetProductInfo.ProductPictures.CurrentRowNumber = 0");
    isFirstImage.CustomStyle = null; // default value
    isFirstImage.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var productImage_CoverTag = isFirstImage.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductImage_CoverTag");
    productImage_CoverTag.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productImage_CoverTag.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productImage_CoverTag.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productImage_CoverTag.Animate = false; // default value
    productImage_CoverTag.ColumnGap = null; // default value
    productImage_CoverTag.ColumnSpan = null; // default value
    productImage_CoverTag.CustomStyle = null; // default value
    productImage_CoverTag.FlexBasis = null; // default value
    productImage_CoverTag.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productImage_CoverTag.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productImage_CoverTag.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productImage_CoverTag.Gap = null;
    productImage_CoverTag.GridColumns = null; // default value
    productImage_CoverTag.GridRows = null; // default value
    productImage_CoverTag.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productImage_CoverTag.RowGap = null; // default value
    productImage_CoverTag.RowSpan = null; // default value
    productImage_CoverTag.SetStyle("\"position-absolute absolute-top-right padding-top-s padding-right-s\"");
    productImage_CoverTag.SetVisible("True"); // default value
    productImage_CoverTag.VisibleWasExpression = false; // default value
    productImage_CoverTag.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productImage_CoverTag.Width = "";
    
    var tagInstance = productImage_CoverTag.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    tagInstance.CustomStyle = null; // default value
    tagInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var tag = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Tag");
    tagInstance.SourceBlock = tag;
    var shape = tag.InputParameters.Named("Shape");
    tagInstance.SetArgumentValue(shape, null);
    var extendedClass5 = tag.InputParameters.Named("ExtendedClass");
    tagInstance.SetArgumentValue(extendedClass5, null); // default value
    var size = tag.InputParameters.Named("Size");
    tagInstance.SetArgumentValue(size, null); // default value
    var isLight = tag.InputParameters.Named("IsLight");
    tagInstance.SetArgumentValue(isLight, null);
    var color = tag.InputParameters.Named("Color");
    tagInstance.SetArgumentValue(color, "Entities.Color.Secondary");
    
    var placeholderContentWidget16 = tagInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Tag");
    var textWidget16 = placeholderContentWidget16.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget16.CustomStyle = null; // default value
    textWidget16.SetStyleClasses(null); // default value
    textWidget16.Text = "Cover";
    
    var iF_HasImageToEdit = productImage_Item.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IF_HasImageToEdit");
    iF_HasImageToEdit.Animate = false; // default value
    iF_HasImageToEdit.SetCondition("not GetProductInfo.ProductPictures.Current.IsPlaceholder and \nIsEditingMode");
    iF_HasImageToEdit.CustomStyle = null; // default value
    iF_HasImageToEdit.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var productImage_ActionsBtn = iF_HasImageToEdit.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductImage_ActionsBtn");
    productImage_ActionsBtn.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productImage_ActionsBtn.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productImage_ActionsBtn.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productImage_ActionsBtn.Animate = false; // default value
    productImage_ActionsBtn.ColumnGap = null; // default value
    productImage_ActionsBtn.ColumnSpan = null; // default value
    productImage_ActionsBtn.CustomStyle = null; // default value
    productImage_ActionsBtn.FlexBasis = null; // default value
    productImage_ActionsBtn.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productImage_ActionsBtn.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productImage_ActionsBtn.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productImage_ActionsBtn.Gap = null;
    productImage_ActionsBtn.GridColumns = null; // default value
    productImage_ActionsBtn.GridRows = null; // default value
    productImage_ActionsBtn.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productImage_ActionsBtn.RowGap = null; // default value
    productImage_ActionsBtn.RowSpan = null; // default value
    productImage_ActionsBtn.SetStyle("\"position-absolute display-flex absolute-bottom-right padding-right-s padding-bottom-s\"");
    productImage_ActionsBtn.SetVisible("True"); // default value
    productImage_ActionsBtn.VisibleWasExpression = false; // default value
    productImage_ActionsBtn.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productImage_ActionsBtn.Width = "";
    
    var editImage_Button = productImage_ActionsBtn.CreateWidget<ServiceStudio.Plugin.NRWidgets.IUpload>("EditImage_Button");
    /*** The Upload Widget Accept property deletes and generates widgets inside it so it must be set immediately ***/
    editImage_Button.Accept = ServiceStudio.Plugin.NRWidgets.Enumerations.Accept.Image;
    /*** Deleting Widgets that are created by default when instantiating an IUpload ***/
    editImage_Button.Widgets.ToList().ForEach(x => x.Delete());
    editImage_Button.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    editImage_Button.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    editImage_Button.ColumnGap = null; // default value
    editImage_Button.ColumnSpan = null; // default value
    editImage_Button.CustomStyle = null; // default value
    editImage_Button.SetFileContent("GetProductInfo.ProductPictures.Current.ProductPicture.File");
    editImage_Button.SetFileName("GetProductInfo.ProductPictures.Current.ProductPicture.Filename");
    editImage_Button.FlexBasis = null; // default value
    editImage_Button.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    editImage_Button.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    editImage_Button.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    editImage_Button.Gap = null;
    editImage_Button.GridColumns = null; // default value
    editImage_Button.GridRows = null; // default value
    editImage_Button.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    editImage_Button.SetMandatory("False");
    editImage_Button.RowGap = null; // default value
    editImage_Button.RowSpan = null; // default value
    editImage_Button.SetStyle("\"btn text-primary border-radius-circle\" + If(not IsPhone(), \" btn-large\", \"\")");
    editImage_Button.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    editImage_Button.Width = "(fill parent)"; // default value
    
    var icon3 = editImage_Button.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon3.ColumnSpan = null; // default value
    icon3.CustomStyle = "color: var(--color-primary); margin-right: 0px;";
    icon3.FlexBasis = null; // default value
    icon3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon3.Icon = "pencil";
    icon3.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon3.RowSpan = null; // default value
    icon3.SetStyle("\"icon\""); // default value
    icon3.SetVisible("True"); // default value
    icon3.Weight = null; // default value
    var builtinEvent14 = editImage_Button.OnChange;
    builtinEvent14.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent14.Destination = productImage_OnEdit;
    builtinEvent14.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var deleteImage_ButtonWrapper = productImage_ActionsBtn.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("DeleteImage_ButtonWrapper");
    deleteImage_ButtonWrapper.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    deleteImage_ButtonWrapper.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    deleteImage_ButtonWrapper.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    deleteImage_ButtonWrapper.Animate = false; // default value
    deleteImage_ButtonWrapper.ColumnGap = null; // default value
    deleteImage_ButtonWrapper.ColumnSpan = null; // default value
    deleteImage_ButtonWrapper.CustomStyle = null; // default value
    deleteImage_ButtonWrapper.FlexBasis = null; // default value
    deleteImage_ButtonWrapper.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    deleteImage_ButtonWrapper.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    deleteImage_ButtonWrapper.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    deleteImage_ButtonWrapper.Gap = null;
    deleteImage_ButtonWrapper.GridColumns = null; // default value
    deleteImage_ButtonWrapper.GridRows = null; // default value
    deleteImage_ButtonWrapper.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    deleteImage_ButtonWrapper.RowGap = null; // default value
    deleteImage_ButtonWrapper.RowSpan = null; // default value
    deleteImage_ButtonWrapper.SetStyle(" \"margin-left-base\"");
    deleteImage_ButtonWrapper.SetVisible("True"); // default value
    deleteImage_ButtonWrapper.VisibleWasExpression = false; // default value
    deleteImage_ButtonWrapper.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    deleteImage_ButtonWrapper.Width = "(fill parent)"; // default value
    
    var deleteImage_Button = deleteImage_ButtonWrapper.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("DeleteImage_Button");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    deleteImage_Button.Widgets.ToList().ForEach(x => x.Delete());
    deleteImage_Button.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    deleteImage_Button.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    deleteImage_Button.ColumnGap = null; // default value
    deleteImage_Button.ColumnSpan = null; // default value
    deleteImage_Button.SetConfirmationMessage(null); // default value
    deleteImage_Button.CustomStyle = null; // default value
    deleteImage_Button.SetEnabled("GetProductInfo.ProductPictures.CurrentRowNumber > 0");
    deleteImage_Button.FlexBasis = null; // default value
    deleteImage_Button.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    deleteImage_Button.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    deleteImage_Button.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    deleteImage_Button.Gap = null;
    deleteImage_Button.GridColumns = null; // default value
    deleteImage_Button.GridRows = null; // default value
    deleteImage_Button.IsDefault = false; // default value
    deleteImage_Button.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    deleteImage_Button.RowGap = null; // default value
    deleteImage_Button.RowSpan = null; // default value
    deleteImage_Button.SetStyle("\"btn text-neutral-7 border-radius-circle\" + If(not IsPhone(), \" btn-large\", \"\")");
    deleteImage_Button.SetVisible("True"); // default value
    deleteImage_Button.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    deleteImage_Button.Width = ""; // default value
    
    var icon4 = deleteImage_Button.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon4.ColumnSpan = null; // default value
    icon4.CustomStyle = null; // default value
    icon4.FlexBasis = null; // default value
    icon4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon4.Icon = "trash-o";
    icon4.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon4.RowSpan = null; // default value
    icon4.SetStyle("\"icon\""); // default value
    icon4.SetVisible("True"); // default value
    icon4.Weight = null; // default value
    var builtinEvent15 = deleteImage_Button.OnClick;
    builtinEvent15.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent15.Destination = productImage_OnDelete;
    builtinEvent15.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var builtinEvent16 = productDetail_ImagesList.OnScrollEnding;
    builtinEvent16.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent16.Destination = null; // default value
    builtinEvent16.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty5 = productDetail_ImagesList.CreateExtendedProperty();
    extendedProperty5.Property = "disable-virtualization";
    extendedProperty5.SetValue("True");
    
    var fetchingRecords2 = ifWidget.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FetchingRecords2");
    fetchingRecords2.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    fetchingRecords2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fetchingRecords2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fetchingRecords2.Animate = false; // default value
    fetchingRecords2.ColumnGap = null; // default value
    fetchingRecords2.ColumnSpan = null; // default value
    fetchingRecords2.CustomStyle = null; // default value
    fetchingRecords2.FlexBasis = null; // default value
    fetchingRecords2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fetchingRecords2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fetchingRecords2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fetchingRecords2.Gap = null;
    fetchingRecords2.GridColumns = null; // default value
    fetchingRecords2.GridRows = null; // default value
    fetchingRecords2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fetchingRecords2.RowGap = null; // default value
    fetchingRecords2.RowSpan = null; // default value
    fetchingRecords2.SetStyle("\"list-updating\"");
    fetchingRecords2.SetVisible("True"); // default value
    fetchingRecords2.VisibleWasExpression = false; // default value
    fetchingRecords2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fetchingRecords2.Width = "(fill parent)"; // default value
    
    var productDetail_SpecificationsTitle = iF_IsFetchingDataOrHasDataToShowContent.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_SpecificationsTitle");
    productDetail_SpecificationsTitle.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_SpecificationsTitle.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_SpecificationsTitle.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_SpecificationsTitle.Animate = false; // default value
    productDetail_SpecificationsTitle.ColumnGap = null; // default value
    productDetail_SpecificationsTitle.ColumnSpan = null; // default value
    productDetail_SpecificationsTitle.CustomStyle = null; // default value
    productDetail_SpecificationsTitle.FlexBasis = null; // default value
    productDetail_SpecificationsTitle.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_SpecificationsTitle.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_SpecificationsTitle.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_SpecificationsTitle.Gap = null;
    productDetail_SpecificationsTitle.GridColumns = null; // default value
    productDetail_SpecificationsTitle.GridRows = null; // default value
    productDetail_SpecificationsTitle.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_SpecificationsTitle.RowGap = null; // default value
    productDetail_SpecificationsTitle.RowSpan = null; // default value
    productDetail_SpecificationsTitle.SetStyle("\"heading4 margin-bottom-m\"");
    productDetail_SpecificationsTitle.SetVisible("True"); // default value
    productDetail_SpecificationsTitle.VisibleWasExpression = false; // default value
    productDetail_SpecificationsTitle.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_SpecificationsTitle.Width = "(fill parent)"; // default value
    
    var textWidget17 = productDetail_SpecificationsTitle.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget17.CustomStyle = null; // default value
    textWidget17.SetStyleClasses(null); // default value
    textWidget17.Text = "Specifications";
    
    var productDetail_SpecificationsInfoForm = iF_IsFetchingDataOrHasDataToShowContent.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IForm>("ProductDetail_SpecificationsInfoForm");
    productDetail_SpecificationsInfoForm.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_SpecificationsInfoForm.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_SpecificationsInfoForm.ColumnGap = null; // default value
    productDetail_SpecificationsInfoForm.ColumnSpan = null; // default value
    productDetail_SpecificationsInfoForm.CustomStyle = null; // default value
    productDetail_SpecificationsInfoForm.FlexBasis = null; // default value
    productDetail_SpecificationsInfoForm.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_SpecificationsInfoForm.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_SpecificationsInfoForm.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_SpecificationsInfoForm.Gap = null;
    productDetail_SpecificationsInfoForm.GridColumns = null; // default value
    productDetail_SpecificationsInfoForm.GridRows = null; // default value
    productDetail_SpecificationsInfoForm.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_SpecificationsInfoForm.RowGap = null; // default value
    productDetail_SpecificationsInfoForm.RowSpan = null; // default value
    productDetail_SpecificationsInfoForm.SetStyle("\"form card padding-bottom-s margin-bottom-l\"");
    productDetail_SpecificationsInfoForm.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_SpecificationsInfoForm.Width = "(fill parent)"; // default value
    
    var columns3Instance = productDetail_SpecificationsInfoForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns3Instance.CustomStyle = null; // default value
    columns3Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var columns3 = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Columns3");
    columns3Instance.SourceBlock = columns3;
    var extendedClass6 = columns3.InputParameters.Named("ExtendedClass");
    columns3Instance.SetArgumentValue(extendedClass6, null); // default value
    var tabletBehavior2 = columns3.InputParameters.Named("TabletBehavior");
    columns3Instance.SetArgumentValue(tabletBehavior2, null);
    var phoneBehavior2 = columns3.InputParameters.Named("PhoneBehavior");
    columns3Instance.SetArgumentValue(phoneBehavior2, "Entities.BreakColumns.All");
    var gutterSize2 = columns3.InputParameters.Named("GutterSize");
    columns3Instance.SetArgumentValue(gutterSize2, null);
    
    var placeholderContentWidget17 = columns3Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var prodDetail_REF = placeholderContentWidget17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_REF");
    prodDetail_REF.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_REF.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_REF.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_REF.Animate = false; // default value
    prodDetail_REF.ColumnGap = null; // default value
    prodDetail_REF.ColumnSpan = null; // default value
    prodDetail_REF.CustomStyle = null; // default value
    prodDetail_REF.FlexBasis = null; // default value
    prodDetail_REF.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_REF.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_REF.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_REF.Gap = null;
    prodDetail_REF.GridColumns = null; // default value
    prodDetail_REF.GridRows = null; // default value
    prodDetail_REF.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_REF.RowGap = null; // default value
    prodDetail_REF.RowSpan = null; // default value
    prodDetail_REF.SetStyle(null); // default value
    prodDetail_REF.SetVisible("True"); // default value
    prodDetail_REF.VisibleWasExpression = false; // default value
    prodDetail_REF.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_REF.Width = "(fill parent)"; // default value
    
    var label8 = prodDetail_REF.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label8.ColumnGap = null; // default value
    label8.ColumnSpan = null; // default value
    label8.CustomStyle = null; // default value
    label8.FlexBasis = null; // default value
    label8.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label8.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label8.Gap = null;
    label8.GridColumns = null; // default value
    label8.GridRows = null; // default value
    label8.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label8.RowGap = null; // default value
    label8.RowSpan = null; // default value
    label8.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label8.Width = "(fill parent)"; // default value
    
    var textWidget18 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label8.Widgets.First();
    textWidget18.CustomStyle = null; // default value
    textWidget18.Name = null; // default value
    textWidget18.SetStyleClasses(null); // default value
    textWidget18.Text = "Reference *";
    
    var prodDetail_REF_Input = prodDetail_REF.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("ProdDetail_REF_Input");
    prodDetail_REF_Input.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_REF_Input.ColumnSpan = null; // default value
    prodDetail_REF_Input.CustomStyle = null; // default value
    prodDetail_REF_Input.SetEnabled("IsEditingMode");
    prodDetail_REF_Input.FlexBasis = null; // default value
    prodDetail_REF_Input.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_REF_Input.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    prodDetail_REF_Input.SetMandatory("False");
    prodDetail_REF_Input.MaxLength = 50; // default value
    prodDetail_REF_Input.SetPrompt("\"Enter product reference\"");
    prodDetail_REF_Input.RowSpan = null; // default value
    prodDetail_REF_Input.SetStyle("\"form-control\""); // default value
    prodDetail_REF_Input.SetVariable("GetProductInfo.ProductDetails.Product.Reference");
    prodDetail_REF_Input.Width = "(fill parent)"; // default value
    var builtinEvent17 = prodDetail_REF_Input.OnChange;
    builtinEvent17.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent17.Destination = null; // default value
    builtinEvent17.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label8.TargetWidget = prodDetail_REF_Input;
    
    var placeholderContentWidget18 = columns3Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var prodDetail_EAN = placeholderContentWidget18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_EAN");
    prodDetail_EAN.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_EAN.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_EAN.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_EAN.Animate = false; // default value
    prodDetail_EAN.ColumnGap = null; // default value
    prodDetail_EAN.ColumnSpan = null; // default value
    prodDetail_EAN.CustomStyle = null; // default value
    prodDetail_EAN.FlexBasis = null; // default value
    prodDetail_EAN.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_EAN.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_EAN.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_EAN.Gap = null;
    prodDetail_EAN.GridColumns = null; // default value
    prodDetail_EAN.GridRows = null; // default value
    prodDetail_EAN.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_EAN.RowGap = null; // default value
    prodDetail_EAN.RowSpan = null; // default value
    prodDetail_EAN.SetStyle(null); // default value
    prodDetail_EAN.SetVisible("True"); // default value
    prodDetail_EAN.VisibleWasExpression = false; // default value
    prodDetail_EAN.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_EAN.Width = "(fill parent)"; // default value
    
    var label9 = prodDetail_EAN.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label9.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label9.ColumnGap = null; // default value
    label9.ColumnSpan = null; // default value
    label9.CustomStyle = null; // default value
    label9.FlexBasis = null; // default value
    label9.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label9.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label9.Gap = null;
    label9.GridColumns = null; // default value
    label9.GridRows = null; // default value
    label9.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label9.RowGap = null; // default value
    label9.RowSpan = null; // default value
    label9.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label9.Width = "(fill parent)"; // default value
    
    var textWidget19 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label9.Widgets.First();
    textWidget19.CustomStyle = null; // default value
    textWidget19.Name = null; // default value
    textWidget19.SetStyleClasses(null); // default value
    textWidget19.Text = "EAN";
    
    var prodDetail_EAN_Input = prodDetail_EAN.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("ProdDetail_EAN_Input");
    prodDetail_EAN_Input.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_EAN_Input.ColumnSpan = null; // default value
    prodDetail_EAN_Input.CustomStyle = null; // default value
    prodDetail_EAN_Input.SetEnabled("IsEditingMode");
    prodDetail_EAN_Input.FlexBasis = null; // default value
    prodDetail_EAN_Input.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_EAN_Input.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    prodDetail_EAN_Input.SetMandatory("False");
    prodDetail_EAN_Input.MaxLength = 50; // default value
    prodDetail_EAN_Input.SetPrompt("\"Enter EAN\"");
    prodDetail_EAN_Input.RowSpan = null; // default value
    prodDetail_EAN_Input.SetStyle("\"form-control\""); // default value
    prodDetail_EAN_Input.SetVariable("GetProductInfo.ProductDetails.Product.EAN");
    prodDetail_EAN_Input.Width = "(fill parent)"; // default value
    var builtinEvent18 = prodDetail_EAN_Input.OnChange;
    builtinEvent18.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent18.Destination = null; // default value
    builtinEvent18.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label9.TargetWidget = prodDetail_EAN_Input;
    
    var placeholderContentWidget19 = columns3Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column3");
    var prodDetail_Brand = placeholderContentWidget19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Brand");
    prodDetail_Brand.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Brand.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Brand.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Brand.Animate = false; // default value
    prodDetail_Brand.ColumnGap = null; // default value
    prodDetail_Brand.ColumnSpan = null; // default value
    prodDetail_Brand.CustomStyle = null; // default value
    prodDetail_Brand.FlexBasis = null; // default value
    prodDetail_Brand.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Brand.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Brand.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Brand.Gap = null;
    prodDetail_Brand.GridColumns = null; // default value
    prodDetail_Brand.GridRows = null; // default value
    prodDetail_Brand.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Brand.RowGap = null; // default value
    prodDetail_Brand.RowSpan = null; // default value
    prodDetail_Brand.SetStyle(null); // default value
    prodDetail_Brand.SetVisible("True"); // default value
    prodDetail_Brand.VisibleWasExpression = false; // default value
    prodDetail_Brand.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Brand.Width = "(fill parent)"; // default value
    
    var label10 = prodDetail_Brand.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label10.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label10.ColumnGap = null; // default value
    label10.ColumnSpan = null; // default value
    label10.CustomStyle = null; // default value
    label10.FlexBasis = null; // default value
    label10.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label10.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label10.Gap = null;
    label10.GridColumns = null; // default value
    label10.GridRows = null; // default value
    label10.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label10.RowGap = null; // default value
    label10.RowSpan = null; // default value
    label10.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label10.TargetWidget = null; // default value
    label10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label10.Width = "(fill parent)"; // default value
    
    var textWidget20 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label10.Widgets.First();
    textWidget20.CustomStyle = null; // default value
    textWidget20.Name = null; // default value
    textWidget20.SetStyleClasses(null); // default value
    textWidget20.Text = "Brand";
    
    var dropdownSearchInstance = prodDetail_Brand.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dropdownSearchInstance.CustomStyle = null; // default value
    dropdownSearchInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var interaction = outSystemsUI.MobileFlows.Named("Interaction");
    var dropdownSearch = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DropdownSearch");
    dropdownSearchInstance.SourceBlock = dropdownSearch;
    var optionalConfigs = dropdownSearch.InputParameters.Named("OptionalConfigs");
    dropdownSearchInstance.SetArgumentValue(optionalConfigs, "{ IsDisabled: not IsEditingMode, SearchPrompt: \"Select brand\" }");
    var optionsList = dropdownSearch.InputParameters.Named("OptionsList");
    dropdownSearchInstance.SetArgumentValue(optionsList, "GetProductInfo.Brands mapTo { Value: Id, Label: Name }");
    var extendedClass7 = dropdownSearch.InputParameters.Named("ExtendedClass");
    dropdownSearchInstance.SetArgumentValue(extendedClass7, null); // default value
    var startingSelection = dropdownSearch.InputParameters.Named("StartingSelection");
    dropdownSearchInstance.SetArgumentValue(startingSelection, "[ { Value: GetProductInfo.ProductDetails.Model.BrandId, Label: GetProductInfo.ProductDetails.Model.BrandId } ]");
    var prompt = dropdownSearch.InputParameters.Named("Prompt");
    dropdownSearchInstance.SetArgumentValue(prompt, "\"Enter brand\"");
    var eventHandler3 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler3.Handler = null; // default value
    var eventHandler4 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnChanged");
    eventHandler4.Handler = onSelectBrand;
    eventHandler4.SetArgumentValue(selectedOptionList, "SelectedOptionList");
    
    var columns3Instance2 = productDetail_SpecificationsInfoForm.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns3Instance2.CustomStyle = null; // default value
    columns3Instance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    columns3Instance2.SourceBlock = columns3;
    columns3Instance2.SetArgumentValue(phoneBehavior2, "Entities.BreakColumns.All");
    columns3Instance2.SetArgumentValue(gutterSize2, null);
    columns3Instance2.SetArgumentValue(extendedClass6, null); // default value
    columns3Instance2.SetArgumentValue(tabletBehavior2, null);
    
    var placeholderContentWidget20 = columns3Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var prodDetail_Model = placeholderContentWidget20.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Model");
    prodDetail_Model.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Model.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Model.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Model.Animate = false; // default value
    prodDetail_Model.ColumnGap = null; // default value
    prodDetail_Model.ColumnSpan = null; // default value
    prodDetail_Model.CustomStyle = null; // default value
    prodDetail_Model.FlexBasis = null; // default value
    prodDetail_Model.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Model.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Model.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Model.Gap = null;
    prodDetail_Model.GridColumns = null; // default value
    prodDetail_Model.GridRows = null; // default value
    prodDetail_Model.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Model.RowGap = null; // default value
    prodDetail_Model.RowSpan = null; // default value
    prodDetail_Model.SetStyle(null); // default value
    prodDetail_Model.SetVisible("True"); // default value
    prodDetail_Model.VisibleWasExpression = false; // default value
    prodDetail_Model.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Model.Width = "(fill parent)"; // default value
    
    var label11 = prodDetail_Model.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label11.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label11.ColumnGap = null; // default value
    label11.ColumnSpan = null; // default value
    label11.CustomStyle = null; // default value
    label11.FlexBasis = null; // default value
    label11.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label11.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label11.Gap = null;
    label11.GridColumns = null; // default value
    label11.GridRows = null; // default value
    label11.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label11.RowGap = null; // default value
    label11.RowSpan = null; // default value
    label11.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label11.TargetWidget = null; // default value
    label11.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label11.Width = "(fill parent)"; // default value
    
    var textWidget21 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label11.Widgets.First();
    textWidget21.CustomStyle = null; // default value
    textWidget21.Name = null; // default value
    textWidget21.SetStyleClasses(null); // default value
    textWidget21.Text = "Model";
    
    var dropdownSearchInstance2 = prodDetail_Model.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dropdownSearchInstance2.CustomStyle = null; // default value
    dropdownSearchInstance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    dropdownSearchInstance2.SourceBlock = dropdownSearch;
    dropdownSearchInstance2.SetArgumentValue(optionalConfigs, "{ IsDisabled: not IsEditingMode, SearchPrompt: \"Select model\" }");
    dropdownSearchInstance2.SetArgumentValue(optionsList, "GetModelsByBrand.List mapTo { Value: Model.Id, Label: Model.Name }");
    dropdownSearchInstance2.SetArgumentValue(startingSelection, "[ { Value: GetProductInfo.ProductDetails.Product.ModelId, Label: GetProductInfo.ProductDetails.ModelName } ]");
    dropdownSearchInstance2.SetArgumentValue(extendedClass7, null); // default value
    dropdownSearchInstance2.SetArgumentValue(prompt, "\"Enter model\"");
    var eventHandler5 = dropdownSearchInstance2.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnChanged");
    eventHandler5.Handler = onSelectModel;
    eventHandler5.SetArgumentValue(selectedOptionList2, "SelectedOptionList");
    var eventHandler6 = dropdownSearchInstance2.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler6.Handler = null; // default value
    
    var placeholderContentWidget21 = columns3Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var prodDetail_Weight = placeholderContentWidget21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Weight");
    prodDetail_Weight.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Weight.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Weight.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Weight.Animate = false; // default value
    prodDetail_Weight.ColumnGap = null; // default value
    prodDetail_Weight.ColumnSpan = null; // default value
    prodDetail_Weight.CustomStyle = null; // default value
    prodDetail_Weight.FlexBasis = null; // default value
    prodDetail_Weight.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Weight.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Weight.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Weight.Gap = null;
    prodDetail_Weight.GridColumns = null; // default value
    prodDetail_Weight.GridRows = null; // default value
    prodDetail_Weight.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Weight.RowGap = null; // default value
    prodDetail_Weight.RowSpan = null; // default value
    prodDetail_Weight.SetStyle(null); // default value
    prodDetail_Weight.SetVisible("True"); // default value
    prodDetail_Weight.VisibleWasExpression = false; // default value
    prodDetail_Weight.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Weight.Width = "(fill parent)"; // default value
    
    var label12 = prodDetail_Weight.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label12.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label12.ColumnGap = null; // default value
    label12.ColumnSpan = null; // default value
    label12.CustomStyle = null; // default value
    label12.FlexBasis = null; // default value
    label12.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label12.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label12.Gap = null;
    label12.GridColumns = null; // default value
    label12.GridRows = null; // default value
    label12.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label12.RowGap = null; // default value
    label12.RowSpan = null; // default value
    label12.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label12.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label12.Width = "(fill parent)"; // default value
    
    var textWidget22 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label12.Widgets.First();
    textWidget22.CustomStyle = null; // default value
    textWidget22.Name = null; // default value
    textWidget22.SetStyleClasses(null); // default value
    textWidget22.Text = "Weight";
    
    var prodDetail_Weight_Input = prodDetail_Weight.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("ProdDetail_Weight_Input");
    prodDetail_Weight_Input.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Weight_Input.ColumnSpan = null; // default value
    prodDetail_Weight_Input.CustomStyle = null; // default value
    prodDetail_Weight_Input.SetEnabled("IsEditingMode");
    prodDetail_Weight_Input.FlexBasis = null; // default value
    prodDetail_Weight_Input.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Weight_Input.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    prodDetail_Weight_Input.SetMandatory("False");
    prodDetail_Weight_Input.MaxLength = 50; // default value
    prodDetail_Weight_Input.SetPrompt("\"Enter weight\"");
    prodDetail_Weight_Input.RowSpan = null; // default value
    prodDetail_Weight_Input.SetStyle("\"form-control\""); // default value
    prodDetail_Weight_Input.SetVariable("GetProductInfo.ProductDetails.Product.Weight");
    prodDetail_Weight_Input.Width = "(fill parent)"; // default value
    var builtinEvent19 = prodDetail_Weight_Input.OnChange;
    builtinEvent19.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent19.Destination = null; // default value
    builtinEvent19.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label12.TargetWidget = prodDetail_Weight_Input;
    
    var placeholderContentWidget22 = columns3Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column3");
    var prodDetail_Dimensions = placeholderContentWidget22.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProdDetail_Dimensions");
    prodDetail_Dimensions.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    prodDetail_Dimensions.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    prodDetail_Dimensions.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Dimensions.Animate = false; // default value
    prodDetail_Dimensions.ColumnGap = null; // default value
    prodDetail_Dimensions.ColumnSpan = null; // default value
    prodDetail_Dimensions.CustomStyle = null; // default value
    prodDetail_Dimensions.FlexBasis = null; // default value
    prodDetail_Dimensions.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    prodDetail_Dimensions.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Dimensions.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    prodDetail_Dimensions.Gap = null;
    prodDetail_Dimensions.GridColumns = null; // default value
    prodDetail_Dimensions.GridRows = null; // default value
    prodDetail_Dimensions.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    prodDetail_Dimensions.RowGap = null; // default value
    prodDetail_Dimensions.RowSpan = null; // default value
    prodDetail_Dimensions.SetStyle(null); // default value
    prodDetail_Dimensions.SetVisible("True"); // default value
    prodDetail_Dimensions.VisibleWasExpression = false; // default value
    prodDetail_Dimensions.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    prodDetail_Dimensions.Width = "(fill parent)"; // default value
    
    var label13 = prodDetail_Dimensions.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label13.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label13.ColumnGap = null; // default value
    label13.ColumnSpan = null; // default value
    label13.CustomStyle = null; // default value
    label13.FlexBasis = null; // default value
    label13.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    label13.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    label13.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    label13.Gap = null;
    label13.GridColumns = null; // default value
    label13.GridRows = null; // default value
    label13.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    label13.RowGap = null; // default value
    label13.RowSpan = null; // default value
    label13.SetStyle("\"font-neutral-8 margin-bottom-s\"");
    label13.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label13.Width = "(fill parent)"; // default value
    
    var textWidget23 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label13.Widgets.First();
    textWidget23.CustomStyle = null; // default value
    textWidget23.Name = null; // default value
    textWidget23.SetStyleClasses(null); // default value
    textWidget23.Text = "Dimensions";
    
    var prodDetail_Dimensions_Input = prodDetail_Dimensions.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("ProdDetail_Dimensions_Input");
    prodDetail_Dimensions_Input.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    prodDetail_Dimensions_Input.ColumnSpan = null; // default value
    prodDetail_Dimensions_Input.CustomStyle = null; // default value
    prodDetail_Dimensions_Input.SetEnabled("IsEditingMode");
    prodDetail_Dimensions_Input.FlexBasis = null; // default value
    prodDetail_Dimensions_Input.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    prodDetail_Dimensions_Input.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    prodDetail_Dimensions_Input.SetMandatory("False");
    prodDetail_Dimensions_Input.MaxLength = 150;
    prodDetail_Dimensions_Input.SetPrompt("\"Enter dimensions\"");
    prodDetail_Dimensions_Input.RowSpan = null; // default value
    prodDetail_Dimensions_Input.SetStyle("\"form-control\""); // default value
    prodDetail_Dimensions_Input.SetVariable("GetProductInfo.ProductDetails.Product.Dimensions");
    prodDetail_Dimensions_Input.Width = "(fill parent)"; // default value
    var builtinEvent20 = prodDetail_Dimensions_Input.OnChange;
    builtinEvent20.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent20.Destination = null; // default value
    builtinEvent20.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    label13.TargetWidget = prodDetail_Dimensions_Input;
    
    var productDetail_FeaturesTitle = iF_IsFetchingDataOrHasDataToShowContent.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_FeaturesTitle");
    productDetail_FeaturesTitle.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_FeaturesTitle.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_FeaturesTitle.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_FeaturesTitle.Animate = false; // default value
    productDetail_FeaturesTitle.ColumnGap = null; // default value
    productDetail_FeaturesTitle.ColumnSpan = null; // default value
    productDetail_FeaturesTitle.CustomStyle = null; // default value
    productDetail_FeaturesTitle.FlexBasis = null; // default value
    productDetail_FeaturesTitle.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_FeaturesTitle.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_FeaturesTitle.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_FeaturesTitle.Gap = null;
    productDetail_FeaturesTitle.GridColumns = null; // default value
    productDetail_FeaturesTitle.GridRows = null; // default value
    productDetail_FeaturesTitle.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_FeaturesTitle.RowGap = null; // default value
    productDetail_FeaturesTitle.RowSpan = null; // default value
    productDetail_FeaturesTitle.SetStyle("\"heading4 margin-bottom-m\"");
    productDetail_FeaturesTitle.SetVisible("True"); // default value
    productDetail_FeaturesTitle.VisibleWasExpression = false; // default value
    productDetail_FeaturesTitle.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_FeaturesTitle.Width = "(fill parent)"; // default value
    
    var textWidget24 = productDetail_FeaturesTitle.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget24.CustomStyle = null; // default value
    textWidget24.SetStyleClasses(null); // default value
    textWidget24.Text = "Features";
    
    var productDetail_Features = iF_IsFetchingDataOrHasDataToShowContent.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("ProductDetail_Features");
    productDetail_Features.CustomStyle = null; // default value
    productDetail_Features.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    productDetail_Features.SourceBlock = card;
    productDetail_Features.SetArgumentValue(usePadding, null);
    productDetail_Features.SetArgumentValue(extendedClass3, null); // default value
    
    var placeholderContentWidget23 = productDetail_Features.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var ifWidget2 = placeholderContentWidget23.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget2.Animate = false; // default value
    ifWidget2.SetCondition("GetProductInfo.IsDataFetched");
    ifWidget2.CustomStyle = null; // default value
    ifWidget2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var galleryInstance = ifWidget2.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    galleryInstance.CustomStyle = null; // default value
    galleryInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    galleryInstance.SourceBlock = gallery;
    // delete default content in Content placeholder
    var placeholderContentWidget24 = galleryInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    placeholderContentWidget24.Widgets.ToList().ForEach(w => w.Delete());
    galleryInstance.SetArgumentValue(rowItemsTablet, "2");
    galleryInstance.SetArgumentValue(itemsGap, "If(GetProductInfo.ProductFeatures.Empty, Entities.Space.None, Entities.Space.Base)");
    galleryInstance.SetArgumentValue(rowItemsDesktop, "2");
    galleryInstance.SetArgumentValue(rowItemsPhone, "1");
    galleryInstance.SetArgumentValue(extendedClass4, null); // default value
    var eventHandler7 = galleryInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler7.Handler = null; // default value
    
    var list7 = placeholderContentWidget24.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>();
    list7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    list7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    list7.AnimateItems = true; // default value
    list7.ColumnGap = null; // default value
    list7.ColumnSpan = null; // default value
    list7.CustomStyle = null; // default value
    list7.ExpandedInWebEditor = true; // default value
    list7.FlexBasis = null; // default value
    list7.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    list7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    list7.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    list7.Gap = null;
    list7.GridColumns = null; // default value
    list7.GridRows = null; // default value
    list7.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    list7.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    list7.RowGap = null; // default value
    list7.RowSpan = null; // default value
    list7.SetSource("GetProductInfo.ProductFeatures");
    list7.SetStyle("\"list list-group\""); // default value
    list7.Tag = "div"; // default value
    list7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    list7.Width = "(fill parent)"; // default value
    
    var productDetail_FeatureList = list7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_FeatureList");
    productDetail_FeatureList.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_FeatureList.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_FeatureList.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_FeatureList.Animate = false; // default value
    productDetail_FeatureList.ColumnGap = null; // default value
    productDetail_FeatureList.ColumnSpan = null; // default value
    productDetail_FeatureList.CustomStyle = null; // default value
    productDetail_FeatureList.FlexBasis = null; // default value
    productDetail_FeatureList.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_FeatureList.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_FeatureList.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_FeatureList.Gap = null;
    productDetail_FeatureList.GridColumns = null; // default value
    productDetail_FeatureList.GridRows = null; // default value
    productDetail_FeatureList.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_FeatureList.RowGap = null; // default value
    productDetail_FeatureList.RowSpan = null; // default value
    productDetail_FeatureList.SetStyle(null); // default value
    productDetail_FeatureList.SetVisible("True"); // default value
    productDetail_FeatureList.VisibleWasExpression = false; // default value
    productDetail_FeatureList.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_FeatureList.Width = "(fill parent)"; // default value
    
    var if_FeatureDisplay = productDetail_FeatureList.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("If_FeatureDisplay");
    if_FeatureDisplay.Animate = false; // default value
    if_FeatureDisplay.SetCondition("not GetProductInfo.ProductFeatures.Current.IsPlaceholder or \nGetProductInfo.ProductFeatures.CurrentRowNumber = 0 or \nnot GetProductInfo.ProductFeatures[GetProductInfo.ProductFeatures.CurrentRowNumber-1].IsPlaceholder ");
    if_FeatureDisplay.CustomStyle = null; // default value
    if_FeatureDisplay.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var if_NotEditableMode = if_FeatureDisplay.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("If_NotEditableMode");
    if_NotEditableMode.Animate = false; // default value
    if_NotEditableMode.SetCondition("not GetProductInfo.ProductFeatures.Current.IsEditMode\n");
    if_NotEditableMode.CustomStyle = null; // default value
    if_NotEditableMode.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var productDetail_FeatureCard = if_NotEditableMode.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("ProductDetail_FeatureCard");
    productDetail_FeatureCard.CustomStyle = null; // default value
    productDetail_FeatureCard.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    productDetail_FeatureCard.SourceBlock = card;
    productDetail_FeatureCard.SetArgumentValue(usePadding, "False");
    productDetail_FeatureCard.SetArgumentValue(extendedClass3, "If(IsEditingMode, \"\", \" background-neutral-2 text-neutral-6\")");
    
    var placeholderContentWidget25 = productDetail_FeatureCard.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var productDetail_Feature = placeholderContentWidget25.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_Feature");
    productDetail_Feature.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_Feature.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_Feature.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_Feature.Animate = false; // default value
    productDetail_Feature.ColumnGap = null; // default value
    productDetail_Feature.ColumnSpan = null; // default value
    productDetail_Feature.CustomStyle = null; // default value
    productDetail_Feature.FlexBasis = null; // default value
    productDetail_Feature.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_Feature.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_Feature.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_Feature.Gap = null;
    productDetail_Feature.GridColumns = null; // default value
    productDetail_Feature.GridRows = null; // default value
    productDetail_Feature.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_Feature.RowGap = null; // default value
    productDetail_Feature.RowSpan = null; // default value
    productDetail_Feature.SetStyle("\"display-flex font-size-base padding-base\"");
    productDetail_Feature.SetVisible("True"); // default value
    productDetail_Feature.VisibleWasExpression = false; // default value
    productDetail_Feature.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_Feature.Width = "(fill parent)"; // default value
    
    var productDetail_FeatureIcon = productDetail_Feature.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_FeatureIcon");
    productDetail_FeatureIcon.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_FeatureIcon.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_FeatureIcon.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_FeatureIcon.Animate = false; // default value
    productDetail_FeatureIcon.ColumnGap = null; // default value
    productDetail_FeatureIcon.ColumnSpan = null; // default value
    productDetail_FeatureIcon.CustomStyle = null; // default value
    productDetail_FeatureIcon.FlexBasis = null; // default value
    productDetail_FeatureIcon.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_FeatureIcon.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_FeatureIcon.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_FeatureIcon.Gap = null;
    productDetail_FeatureIcon.GridColumns = null; // default value
    productDetail_FeatureIcon.GridRows = null; // default value
    productDetail_FeatureIcon.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_FeatureIcon.RowGap = null; // default value
    productDetail_FeatureIcon.RowSpan = null; // default value
    productDetail_FeatureIcon.SetStyle(null); // default value
    productDetail_FeatureIcon.SetVisible("True"); // default value
    productDetail_FeatureIcon.VisibleWasExpression = false; // default value
    productDetail_FeatureIcon.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_FeatureIcon.Width = "(fill parent)"; // default value
    
    var iF_IsAFeature = productDetail_FeatureIcon.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IF_IsAFeature");
    iF_IsAFeature.Animate = false; // default value
    iF_IsAFeature.SetCondition("not GetProductInfo.ProductFeatures.Current.IsPlaceholder");
    iF_IsAFeature.CustomStyle = null; // default value
    iF_IsAFeature.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var icon5 = iF_IsAFeature.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon5.ColumnSpan = null; // default value
    icon5.CustomStyle = null; // default value
    icon5.FlexBasis = null; // default value
    icon5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon5.Icon = "check-circle";
    icon5.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon5.RowSpan = null; // default value
    icon5.SetStyle("\"icon\" + If(IsEditingMode, \" text-primary\", \" text-neutral-6\")");
    icon5.SetVisible("True"); // default value
    icon5.Weight = null; // default value
    
    var icon6 = iF_IsAFeature.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon6.ColumnSpan = null; // default value
    icon6.CustomStyle = null; // default value
    icon6.FlexBasis = null; // default value
    icon6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon6.Icon = "plus-circle";
    icon6.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon6.RowSpan = null; // default value
    icon6.SetStyle("\"icon\" + If(IsEditingMode, \" text-neutral-7\", \"\")");
    icon6.SetVisible("True"); // default value
    icon6.Weight = null; // default value
    
    var productDetail_FeatureDescription = productDetail_Feature.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_FeatureDescription");
    productDetail_FeatureDescription.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_FeatureDescription.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_FeatureDescription.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_FeatureDescription.Animate = false; // default value
    productDetail_FeatureDescription.ColumnGap = null; // default value
    productDetail_FeatureDescription.ColumnSpan = null; // default value
    productDetail_FeatureDescription.CustomStyle = null; // default value
    productDetail_FeatureDescription.FlexBasis = null; // default value
    productDetail_FeatureDescription.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_FeatureDescription.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_FeatureDescription.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_FeatureDescription.Gap = null;
    productDetail_FeatureDescription.GridColumns = null; // default value
    productDetail_FeatureDescription.GridRows = null; // default value
    productDetail_FeatureDescription.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_FeatureDescription.RowGap = null; // default value
    productDetail_FeatureDescription.RowSpan = null; // default value
    productDetail_FeatureDescription.SetStyle("\"margin-left-s flex1\"");
    productDetail_FeatureDescription.SetVisible("True"); // default value
    productDetail_FeatureDescription.VisibleWasExpression = false; // default value
    productDetail_FeatureDescription.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_FeatureDescription.Width = "(fill parent)"; // default value
    
    var iF_NotAPlaceholder = productDetail_FeatureDescription.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IF_NotAPlaceholder");
    iF_NotAPlaceholder.Animate = false; // default value
    iF_NotAPlaceholder.SetCondition("not GetProductInfo.ProductFeatures.Current.IsPlaceholder");
    iF_NotAPlaceholder.CustomStyle = null; // default value
    iF_NotAPlaceholder.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var expression2 = iF_NotAPlaceholder.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = "Active noise cancellation for immersive sound";
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetProductInfo.ProductFeatures.Current.Description");
    expression2.Width = ""; // default value
    
    var textWidget25 = iF_NotAPlaceholder.FalseBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget25.CustomStyle = null; // default value
    textWidget25.SetStyleClasses(null); // default value
    textWidget25.Text = "Add a feature here";
    
    var iF_IsAProductFeature = productDetail_Feature.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IF_IsAProductFeature");
    iF_IsAProductFeature.Animate = false; // default value
    iF_IsAProductFeature.SetCondition("not GetProductInfo.ProductFeatures.Current.IsPlaceholder");
    iF_IsAProductFeature.CustomStyle = null; // default value
    iF_IsAProductFeature.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var link2 = iF_IsAProductFeature.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link2.Widgets.ToList().ForEach(x => x.Delete());
    link2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link2.ColumnGap = null; // default value
    link2.ColumnSpan = null; // default value
    link2.SetConfirmationMessage(null); // default value
    link2.CustomStyle = null; // default value
    link2.SetEnabled("True");
    link2.FlexBasis = null; // default value
    link2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link2.Gap = null;
    link2.GridColumns = null; // default value
    link2.GridRows = null; // default value
    link2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link2.RowGap = null; // default value
    link2.RowSpan = null; // default value
    link2.SetStyle(null); // default value
    link2.SetVisible("True"); // default value
    link2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link2.Width = ""; // default value
    
    var productDetail_FeatureRemove = link2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("ProductDetail_FeatureRemove");
    productDetail_FeatureRemove.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    productDetail_FeatureRemove.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    productDetail_FeatureRemove.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    productDetail_FeatureRemove.Animate = true;
    productDetail_FeatureRemove.ColumnGap = null; // default value
    productDetail_FeatureRemove.ColumnSpan = null; // default value
    productDetail_FeatureRemove.CustomStyle = null; // default value
    productDetail_FeatureRemove.FlexBasis = null; // default value
    productDetail_FeatureRemove.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    productDetail_FeatureRemove.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    productDetail_FeatureRemove.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    productDetail_FeatureRemove.Gap = null;
    productDetail_FeatureRemove.GridColumns = null; // default value
    productDetail_FeatureRemove.GridRows = null; // default value
    productDetail_FeatureRemove.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    productDetail_FeatureRemove.RowGap = null; // default value
    productDetail_FeatureRemove.RowSpan = null; // default value
    productDetail_FeatureRemove.SetStyle("\"padding-left-s\"");
    productDetail_FeatureRemove.SetVisible("IsEditingMode");
    productDetail_FeatureRemove.VisibleWasExpression = true;
    productDetail_FeatureRemove.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    productDetail_FeatureRemove.Width = "(fill parent)"; // default value
    
    var icon7 = productDetail_FeatureRemove.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon7.ColumnSpan = null; // default value
    icon7.CustomStyle = null; // default value
    icon7.FlexBasis = null; // default value
    icon7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon7.Icon = "times";
    icon7.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon7.RowSpan = null; // default value
    icon7.SetStyle("\"icon text-neutral-6\"");
    icon7.SetVisible("True"); // default value
    icon7.Weight = null; // default value
    var builtinEvent21 = link2.OnClick;
    builtinEvent21.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent21.Destination = feature_OnDelete;
    builtinEvent21.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var event2 = productDetail_Feature.CreateEvent();
    event2.Event = "onclick";
    event2.Handler = feature_ToggleEdit;
    
    var textArea_Description2 = if_NotEditableMode.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ITextArea>("TextArea_Description2");
    textArea_Description2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    textArea_Description2.ColumnSpan = null; // default value
    textArea_Description2.CustomStyle = null; // default value
    textArea_Description2.SetEnabled("IsEditingMode");
    textArea_Description2.FlexBasis = null; // default value
    textArea_Description2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    textArea_Description2.SetMandatory("False");
    textArea_Description2.MaxLength = null;
    textArea_Description2.SetPrompt("\"Enter a feature\"");
    textArea_Description2.RowSpan = null; // default value
    textArea_Description2.SetStyle("\"form-control padding-s\"");
    textArea_Description2.TextLines = 2;
    textArea_Description2.SetVariable("GetProductInfo.ProductFeatures.Current.Description");
    textArea_Description2.Width = "(fill parent)"; // default value
    var builtinEvent22 = textArea_Description2.OnChange;
    builtinEvent22.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent22.Destination = null; // default value
    builtinEvent22.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty6 = textArea_Description2.CreateExtendedProperty();
    extendedProperty6.Property = "autofocus";
    extendedProperty6.SetValue("True");
    
    var event3 = textArea_Description2.CreateEvent();
    event3.Event = "onblur";
    event3.Handler = feature_OnEdit;
    var builtinEvent23 = list7.OnScrollEnding;
    builtinEvent23.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent23.Destination = null; // default value
    builtinEvent23.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty7 = list7.CreateExtendedProperty();
    extendedProperty7.Property = "disable-virtualization";
    extendedProperty7.SetValue("True");
    
    var fetchingRecords = ifWidget2.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FetchingRecords");
    fetchingRecords.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    fetchingRecords.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    fetchingRecords.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    fetchingRecords.Animate = false; // default value
    fetchingRecords.ColumnGap = null; // default value
    fetchingRecords.ColumnSpan = null; // default value
    fetchingRecords.CustomStyle = null; // default value
    fetchingRecords.FlexBasis = null; // default value
    fetchingRecords.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    fetchingRecords.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    fetchingRecords.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    fetchingRecords.Gap = null;
    fetchingRecords.GridColumns = null; // default value
    fetchingRecords.GridRows = null; // default value
    fetchingRecords.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    fetchingRecords.RowGap = null; // default value
    fetchingRecords.RowSpan = null; // default value
    fetchingRecords.SetStyle("\"list-updating\"");
    fetchingRecords.SetVisible("True"); // default value
    fetchingRecords.VisibleWasExpression = false; // default value
    fetchingRecords.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    fetchingRecords.Width = "(fill parent)"; // default value
    
    var productDetail_BlankSlate = iF_IsFetchingDataOrHasDataToShowContent.FalseBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("ProductDetail_BlankSlate");
    productDetail_BlankSlate.CustomStyle = null; // default value
    productDetail_BlankSlate.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var blankSlate = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("BlankSlate");
    productDetail_BlankSlate.SourceBlock = blankSlate;
    // delete default content in Icon placeholder
    var placeholderContentWidget26 = productDetail_BlankSlate.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    placeholderContentWidget26.Widgets.ToList().ForEach(w => w.Delete());
    var fullHeight = blankSlate.InputParameters.Named("FullHeight");
    productDetail_BlankSlate.SetArgumentValue(fullHeight, "False");
    var extendedClass8 = blankSlate.InputParameters.Named("ExtendedClass");
    productDetail_BlankSlate.SetArgumentValue(extendedClass8, "\"card\"");
    
    var image2 = placeholderContentWidget26.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image2.ColumnSpan = null; // default value
    image2.CustomStyle = null; // default value
    image2.DefaultImage = null; // default value
    image2.FlexBasis = null; // default value
    image2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    var blankSlate_NothingToShow = eSpace.Images.Named("BlankSlate_NothingToShow");
    image2.Image = blankSlate_NothingToShow;
    image2.SetImageContent(null); // default value
    image2.RowSpan = null; // default value
    image2.SetStyle(null); // default value
    image2.Type = ServiceStudio.Plugin.NRWidgets.Enumerations.Type.Static; // default value
    image2.SetUrl(null); // default value
    image2.Width = ""; // default value
    
    var placeholderContentWidget27 = productDetail_BlankSlate.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var textWidget26 = placeholderContentWidget27.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget26.CustomStyle = null; // default value
    textWidget26.SetStyleClasses(null); // default value
    textWidget26.Text = "The product you were looking for doesn’t exist.";
    
    var placeholderContentWidget28 = productDetail_BlankSlate.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Actions");
    var button2 = placeholderContentWidget28.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    button2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button2.ColumnGap = null; // default value
    button2.ColumnSpan = null; // default value
    button2.SetConfirmationMessage(null); // default value
    button2.CustomStyle = null; // default value
    button2.SetEnabled("True");
    button2.FlexBasis = null; // default value
    button2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button2.Gap = null;
    button2.GridColumns = null; // default value
    button2.GridRows = null; // default value
    button2.IsDefault = false; // default value
    button2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button2.RowGap = null; // default value
    button2.RowSpan = null; // default value
    button2.SetStyle("\"btn btn-primary\"");
    button2.SetVisible("True"); // default value
    button2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button2.Width = ""; // default value
    
    var textWidget27 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button2.Widgets.First();
    textWidget27.CustomStyle = null; // default value
    textWidget27.Name = null; // default value
    textWidget27.SetStyleClasses(null); // default value
    textWidget27.Text = "Go to Homepage";
    var builtinEvent24 = button2.OnClick;
    builtinEvent24.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var redirectToURL = product.Nodes.OfType<OutSystems.Model.UI.Mobile.IExternalSite>().Named("RedirectToURL");
    builtinEvent24.Destination = redirectToURL;
    builtinEvent24.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var uRL = redirectToURL.InputParameters.Named("URL");
    builtinEvent24.SetArgumentValue(uRL, "GetOwnerURLPath()");
}

```