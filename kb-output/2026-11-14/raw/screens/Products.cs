```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow product) => {
    
    /*** creating IMobileScreen 'Products' ***/
    var products = product.CreateScreen("Products");
    products.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = product.GetESpace();
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    products.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    products.Roles.Add(orderManager);
    products.AnonymousAccess = false; // default value
    products.CustomURL = false; // default value
    products.Description = null; // default value
    products.HorizontalPosition = 914;
    products.Icon = null; // default value
    products.IsDisabled = false; // default value
    products.PageName = null; // default value
    products.Public = false; // default value
    products.Shareable = false; // default value
    products.StyleSheet = null; // default value
    products.SetTitle("\"Order Management - Products\"");
    products.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    products.VerticalPosition = 914;
    
    var getCategories = products.CreateScreenAggregate(false, "GetCategories");
    getCategories.CacheInMinutes = null; // default value
    getCategories.Description = null; // default value
    getCategories.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getCategories.SetMaxRecords("500");
    getCategories.SetStartIndex(null); // default value
    getCategories.Timeout = null; // default value
    var productCategory2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("ProductCategory");
    var productCategory = getCategories.AsDatabaseAggregate.CreateSource(productCategory2);
    getCategories.AsDatabaseAggregate.CreateFilter("ProductCategory.Name <> \"\"");
    getCategories.AsDatabaseAggregate.CreateSort().SetAttribute("ProductCategory.Name");
    
    
    var getProductsByProductName = products.CreateScreenAggregate(false, "GetProductsByProductName");
    getProductsByProductName.CacheInMinutes = null; // default value
    getProductsByProductName.Description = null; // default value
    getProductsByProductName.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getProductsByProductName.SetMaxRecords("MaxRecords");
    getProductsByProductName.SetStartIndex("StartIndex");
    getProductsByProductName.Timeout = null; // default value
    var product3 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Product");
    var product2 = getProductsByProductName.AsDatabaseAggregate.CreateSource(product3);
    var productCategory3 = getProductsByProductName.AsDatabaseAggregate.CreateSource(productCategory2);
    var productPicture2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("ProductPicture");
    var productPicture = getProductsByProductName.AsDatabaseAggregate.CreateSource(productPicture2);
    var join = getProductsByProductName.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = product2;
    join.RightSource = productCategory3;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join.SetCondition("Product.CategoryId = ProductCategory.Id");
    var join2 = getProductsByProductName.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = product2;
    join2.RightSource = productPicture;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join2.SetCondition("Product.Id = ProductPicture.ProductId and ProductPicture.IsCover");
    getProductsByProductName.AsDatabaseAggregate.CreateFilter("Product.Name like \"%\" + SearchKeyword + \"%\"");
    getProductsByProductName.AsDatabaseAggregate.CreateFilter("(SelectedCategoryId.Value = \"\") or (SelectedCategoryId.Value = NullIdentifier()) or (LongIntegerToIdentifier(TextToLongInteger(SelectedCategoryId.Value)) = ProductCategory.Id)");
    getProductsByProductName.AsDatabaseAggregate.CreateSort().SetAttribute("SortBy");
    
    
    var getProductSorts = products.CreateScreenAggregate(false, "GetProductSorts");
    getProductSorts.CacheInMinutes = null; // default value
    getProductSorts.Description = null; // default value
    getProductSorts.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getProductSorts.SetMaxRecords("50"); // default value
    getProductSorts.SetStartIndex(null); // default value
    getProductSorts.Timeout = null; // default value
    var productSort2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IStaticEntitySignature>().Named("ProductSort");
    var productSort = getProductSorts.AsDatabaseAggregate.CreateSource(productSort2);
    getProductSorts.AsDatabaseAggregate.CreateFilter("ProductSort.IsActive = True");
    
    
    var getProductSortsById = products.CreateScreenAggregate(false, "GetProductSortsById");
    getProductSortsById.CacheInMinutes = null; // default value
    getProductSortsById.Description = null; // default value
    getProductSortsById.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getProductSortsById.SetMaxRecords("1");
    getProductSortsById.SetStartIndex(null); // default value
    getProductSortsById.Timeout = null; // default value
    var productSort3 = getProductSortsById.AsDatabaseAggregate.CreateSource(productSort2);
    getProductSortsById.AsDatabaseAggregate.CreateFilter("ProductSort.IsActive = True");
    getProductSortsById.AsDatabaseAggregate.CreateFilter("ProductSort.Id = ProductSortId");
    
    
    var searchKeyword = products.CreateLocalVariable("SearchKeyword");
    searchKeyword.DataType = eSpace.TextType;
    searchKeyword.SetDefaultValue(null); // default value
    searchKeyword.Description = null; // default value
    
    var selectedCategoryId = products.CreateLocalVariable("SelectedCategoryId");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var dropdownItem = outSystemsUI.Structures.Named("DropdownItem");
    selectedCategoryId.DataType = dropdownItem;
    selectedCategoryId.Description = null; // default value
    
    var sortBy = products.CreateLocalVariable("SortBy");
    sortBy.DataType = eSpace.TextType;
    sortBy.SetDefaultValue("\"{Product}.[Name]\"");
    sortBy.Description = null; // default value
    
    var productSortId = products.CreateLocalVariable("ProductSortId");
    productSortId.DataType = productSort2.IdentifierType;
    productSortId.SetDefaultValue(null); // default value
    productSortId.Description = null; // default value
    
    var maxRecords = products.CreateLocalVariable("MaxRecords");
    maxRecords.DataType = eSpace.IntegerType;
    maxRecords.SetDefaultValue("12");
    maxRecords.Description = null; // default value
    
    var startIndex = products.CreateLocalVariable("StartIndex");
    startIndex.DataType = eSpace.IntegerType;
    startIndex.SetDefaultValue("0");
    startIndex.Description = null; // default value
    products.OnDestroy.Destination = null; // default value
    products.OnInitialize.Destination = null; // default value
    products.OnReady.Destination = null; // default value
    products.OnRender.Destination = null; // default value
    
    var dropdownSearchOnSelect = products.CreateScreenAction("DropdownSearchOnSelect");
    dropdownSearchOnSelect.Description = null; // default value
    
    var selectedOptionList = dropdownSearchOnSelect.CreateInputParameter("SelectedOptionList");
    var dropdownOption = outSystemsUI.Structures.Named("DropdownOption");
    selectedOptionList.DataType = eSpace.GetOrCreateListType(dropdownOption);
    selectedOptionList.Description = null; // default value
    selectedOptionList.IsMandatory = true; // default value
    
    var startNode = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode.HorizontalPosition = 3200;
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    assignNode.VerticalPosition = 2286;
    
    var assignment = assignNode.CreateAssignment("SelectedCategoryId", "SelectedOptionList.Current mapTo { Value: Value, Text: Label }");
    startNode.Target = assignNode;
    
    var onFiltersChange = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("OnFiltersChange").ConnectedBelow(assignNode, 1371);
    onFiltersChange.IsDisabled = false; // default value
    
    var onFiltersChange2 = products.CreateScreenAction("OnFiltersChange");
    onFiltersChange2.Description = null; // default value
    
    var newStartIndex = onFiltersChange2.CreateInputParameter("NewStartIndex");
    newStartIndex.DataType = eSpace.IntegerType;
    newStartIndex.Description = null; // default value
    newStartIndex.IsMandatory = true; // default value
    
    var startNode2 = onFiltersChange2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var assignNode2 = onFiltersChange2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode2.HorizontalPosition = 3192;
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.VerticalPosition = 2280;
    
    var assignment2 = assignNode2.CreateAssignment("StartIndex", "NewStartIndex");
    
    var refreshDataNode = onFiltersChange2.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>();
    refreshDataNode.DataSource = getProductsByProductName;
    refreshDataNode.HorizontalPosition = 3199;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords("MaxRecords");
    refreshDataNode.SetStartIndex("StartIndex");
    refreshDataNode.VerticalPosition = 3889;
    
    var endNode = onFiltersChange2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1371);
    endNode.IsDisabled = false; // default value
    onFiltersChange.Action = onFiltersChange2;
    
    var endNode2 = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3196;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 5120;
    onFiltersChange.Target = endNode2;
    onFiltersChange.SetArgumentValue(newStartIndex, "0");
    startNode2.Target = assignNode2;
    assignNode2.Target = refreshDataNode;
    
    var loadMoreOnClick = products.CreateScreenAction("LoadMoreOnClick");
    loadMoreOnClick.Description = null; // default value
    
    var startNode3 = loadMoreOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.IsDisabled = false; // default value
    
    var assignNode3 = loadMoreOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode3.HorizontalPosition = 3192;
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    assignNode3.VerticalPosition = 2508;
    
    var assignment3 = assignNode3.CreateAssignment("MaxRecords", "MaxRecords + MaxRecords");
    startNode3.Target = assignNode3;
    
    var refreshDataNode2 = loadMoreOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode3, 1824);
    refreshDataNode2.DataSource = getProductsByProductName;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords("MaxRecords");
    refreshDataNode2.SetStartIndex("StartIndex");
    
    var scrollToElement = loadMoreOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("ScrollToElement").ConnectedBelow(refreshDataNode2, 1596);
    var scrollToElement2 = outSystemsUI.ClientActions.Named("ScrollToElement");
    scrollToElement.Action = scrollToElement2;
    scrollToElement.IsDisabled = false; // default value
    var scrollDelay = scrollToElement2.InputParameters.Named("ScrollDelay");
    scrollToElement.SetArgumentValue(scrollDelay, null);
    var elementParentClass = scrollToElement2.InputParameters.Named("ElementParentClass");
    scrollToElement.SetArgumentValue(elementParentClass, null); // default value
    var widgetId = scrollToElement2.InputParameters.Named("WidgetId");
    scrollToElement.SetArgumentValue(widgetId, "PageFooter.Id");
    var isSmooth = scrollToElement2.InputParameters.Named("IsSmooth");
    scrollToElement.SetArgumentValue(isSmooth, null);
    var offSet = scrollToElement2.InputParameters.Named("OffSet");
    scrollToElement.SetArgumentValue(offSet, "-80");
    
    var endNode3 = loadMoreOnClick.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode3.HorizontalPosition = 3200;
    endNode3.IsDisabled = false; // default value
    endNode3.VerticalPosition = 7524;
    scrollToElement.Target = endNode3;
    
    var sortDropdownOnChange = products.CreateScreenAction("SortDropdownOnChange");
    sortDropdownOnChange.Description = null; // default value
    
    var startNode4 = sortDropdownOnChange.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.IsDisabled = false; // default value
    
    var refreshDataNode3 = sortDropdownOnChange.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>();
    refreshDataNode3.DataSource = getProductSortsById;
    refreshDataNode3.HorizontalPosition = 3192;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords("1");
    refreshDataNode3.SetStartIndex(null); // default value
    refreshDataNode3.VerticalPosition = 2280;
    startNode4.Target = refreshDataNode3;
    
    var assignNode4 = sortDropdownOnChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(refreshDataNode3, 1368);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
    var assignment4 = assignNode4.CreateAssignment("SortBy", "GetProductSortsById.List.Current.ProductSort.Value");
    
    var refreshDataNode4 = sortDropdownOnChange.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode4, 1596);
    refreshDataNode4.DataSource = getProductsByProductName;
    refreshDataNode4.IsDisabled = false; // default value
    refreshDataNode4.SetMaxRecords("MaxRecords");
    refreshDataNode4.SetStartIndex("StartIndex");
    
    var endNode4 = sortDropdownOnChange.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode4.HorizontalPosition = 3196;
    endNode4.IsDisabled = false; // default value
    endNode4.VerticalPosition = 7542;
    refreshDataNode4.Target = endNode4;
    
    var layoutTopHeaderInstance = products.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopHeaderInstance.CustomStyle = null; // default value
    layoutTopHeaderInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var layoutTopHeader = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopHeader");
    layoutTopHeaderInstance.SourceBlock = layoutTopHeader;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var hasHeaderImage = layoutTopHeader.InputParameters.Named("HasHeaderImage");
    layoutTopHeaderInstance.SetArgumentValue(hasHeaderImage, null);
    var hasFixedHeader = layoutTopHeader.InputParameters.Named("HasFixedHeader");
    layoutTopHeaderInstance.SetArgumentValue(hasFixedHeader, "True");
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeMenu = menu.InputParameters.Named("ActiveMenu");
    menuInstance.SetArgumentValue(activeMenu, "Entities.MenuItem.Catalog");
    
    var placeholderContentWidget2 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Catalog";
    
    var placeholderContentWidget3 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Actions");
    var button = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
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
    button.SetStyle("\"btn btn-large\"");
    button.SetVisible("True"); // default value
    button.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button.Width = ""; // default value
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button.Widgets.First();
    textWidget2.CustomStyle = "margin-left: 5px;";
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "New Product";
    var builtinEvent = button.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var productDetail = product.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("ProductDetail");
    builtinEvent.Destination = productDetail;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromRight;
    var productId = productDetail.InputParameters.Named("ProductId");
    builtinEvent.SetArgumentValue(productId, "NullIdentifier()");
    
    var placeholderContentWidget4 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Controls");
    var container = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.SetStyle("\"single-search\"");
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "";
    
    var dEPRECATED_SearchInstance = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_SearchInstance.CustomStyle = null; // default value
    dEPRECATED_SearchInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var interaction = outSystemsUI.MobileFlows.Named("Interaction");
    var dEPRECATED_Search = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DEPRECATED_Search");
    dEPRECATED_SearchInstance.SourceBlock = dEPRECATED_Search;
    // delete default content in Input placeholder
    var placeholderContentWidget5 = dEPRECATED_SearchInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Input");
    placeholderContentWidget5.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass = dEPRECATED_Search.InputParameters.Named("ExtendedClass");
    dEPRECATED_SearchInstance.SetArgumentValue(extendedClass, null); // default value
    var eventHandler = dEPRECATED_SearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnCollapse");
    eventHandler.Handler = null; // default value
    
    var input_TextVar = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_TextVar");
    input_TextVar.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_TextVar.ColumnSpan = null; // default value
    input_TextVar.CustomStyle = "padding: 5px 16px 5px 40px;";
    input_TextVar.SetEnabled("True");
    input_TextVar.FlexBasis = null; // default value
    input_TextVar.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_TextVar.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Search;
    input_TextVar.SetMandatory("False");
    input_TextVar.MaxLength = 50; // default value
    input_TextVar.SetPrompt("\"Type the product name\"");
    input_TextVar.RowSpan = null; // default value
    input_TextVar.SetStyle("\"form-control search-input\"");
    input_TextVar.SetVariable("SearchKeyword");
    input_TextVar.Width = "(fill parent)"; // default value
    var builtinEvent2 = input_TextVar.OnChange;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = onFiltersChange2;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent2.SetArgumentValue(newStartIndex, "0");
    
    var container2 = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle("\"second-search \"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "";
    
    var dropdownSearchInstance = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dropdownSearchInstance.CustomStyle = null; // default value
    dropdownSearchInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dropdownSearch = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DropdownSearch");
    dropdownSearchInstance.SourceBlock = dropdownSearch;
    var optionsList = dropdownSearch.InputParameters.Named("OptionsList");
    dropdownSearchInstance.SetArgumentValue(optionsList, "GetCategories.List mapTo { Value: ProductCategory.Id, Label: ProductCategory.Name, Description: ProductCategory.Description }");
    var extendedClass2 = dropdownSearch.InputParameters.Named("ExtendedClass");
    dropdownSearchInstance.SetArgumentValue(extendedClass2, "\"search-dropdown\"");
    var optionalConfigs = dropdownSearch.InputParameters.Named("OptionalConfigs");
    dropdownSearchInstance.SetArgumentValue(optionalConfigs, "{ SearchPrompt: \"Search by Category\" }");
    var startingSelection = dropdownSearch.InputParameters.Named("StartingSelection");
    dropdownSearchInstance.SetArgumentValue(startingSelection, "[ { Value: SelectedCategoryId.Value, Label: SelectedCategoryId.Text } ]");
    var prompt = dropdownSearch.InputParameters.Named("Prompt");
    dropdownSearchInstance.SetArgumentValue(prompt, "\"All Categories\"");
    var eventHandler2 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnChanged");
    eventHandler2.Handler = dropdownSearchOnSelect;
    eventHandler2.SetArgumentValue(selectedOptionList, "SelectedOptionList");
    var eventHandler3 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler3.Handler = null; // default value
    
    var placeholderContentWidget6 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var container3 = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container3.SetStyle("\"display-flex center-right margin-bottom-base\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var container4 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container4.Animate = false; // default value
    container4.ColumnGap = null; // default value
    container4.ColumnSpan = null; // default value
    container4.CustomStyle = "text-align: right;";
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
    container4.SetStyle(null); // default value
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "4 col";
    
    var sortDropdown = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("SortDropdown");
    sortDropdown.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    sortDropdown.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    sortDropdown.ColumnGap = null; // default value
    sortDropdown.ColumnSpan = null; // default value
    sortDropdown.CustomStyle = null; // default value
    sortDropdown.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Custom; // default value
    sortDropdown.SetEmptyValue("\"(Sort by)\"");
    sortDropdown.SetEnabled("True");
    sortDropdown.FlexBasis = null; // default value
    sortDropdown.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    sortDropdown.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    sortDropdown.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    sortDropdown.Gap = null;
    sortDropdown.GridColumns = null; // default value
    sortDropdown.GridRows = null; // default value
    sortDropdown.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    sortDropdown.SetLabels(null); // default value
    sortDropdown.SetList("GetProductSorts.List");
    sortDropdown.SetMandatory("False");
    sortDropdown.RowGap = null; // default value
    sortDropdown.RowSpan = null; // default value
    sortDropdown.SetStyle("\"dropdown select sort-by font-bold\"");
    sortDropdown.SetValues("ProductSort.Id");
    sortDropdown.SetVariable("ProductSortId");
    sortDropdown.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    sortDropdown.Width = "8 col";
    
    var expression = sortDropdown.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetProductSorts.List.Current.ProductSort.Label");
    expression.Width = ""; // default value
    var builtinEvent3 = sortDropdown.OnChange;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = sortDropdownOnChange;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var orderManagProductsGallery_Wrapper = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OrderManagProductsGallery_Wrapper");
    orderManagProductsGallery_Wrapper.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    orderManagProductsGallery_Wrapper.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    orderManagProductsGallery_Wrapper.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderManagProductsGallery_Wrapper.Animate = false; // default value
    orderManagProductsGallery_Wrapper.ColumnGap = null; // default value
    orderManagProductsGallery_Wrapper.ColumnSpan = null; // default value
    orderManagProductsGallery_Wrapper.CustomStyle = null; // default value
    orderManagProductsGallery_Wrapper.FlexBasis = null; // default value
    orderManagProductsGallery_Wrapper.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    orderManagProductsGallery_Wrapper.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderManagProductsGallery_Wrapper.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    orderManagProductsGallery_Wrapper.Gap = null;
    orderManagProductsGallery_Wrapper.GridColumns = null; // default value
    orderManagProductsGallery_Wrapper.GridRows = null; // default value
    orderManagProductsGallery_Wrapper.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    orderManagProductsGallery_Wrapper.RowGap = null; // default value
    orderManagProductsGallery_Wrapper.RowSpan = null; // default value
    orderManagProductsGallery_Wrapper.SetStyle(null); // default value
    orderManagProductsGallery_Wrapper.SetVisible("True"); // default value
    orderManagProductsGallery_Wrapper.VisibleWasExpression = false; // default value
    orderManagProductsGallery_Wrapper.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    orderManagProductsGallery_Wrapper.Width = "(fill parent)"; // default value
    
    var orderManagProductsGallery_Title = orderManagProductsGallery_Wrapper.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OrderManagProductsGallery_Title");
    orderManagProductsGallery_Title.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    orderManagProductsGallery_Title.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    orderManagProductsGallery_Title.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderManagProductsGallery_Title.Animate = false; // default value
    orderManagProductsGallery_Title.ColumnGap = null; // default value
    orderManagProductsGallery_Title.ColumnSpan = null; // default value
    orderManagProductsGallery_Title.CustomStyle = null; // default value
    orderManagProductsGallery_Title.FlexBasis = null; // default value
    orderManagProductsGallery_Title.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    orderManagProductsGallery_Title.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderManagProductsGallery_Title.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    orderManagProductsGallery_Title.Gap = null;
    orderManagProductsGallery_Title.GridColumns = null; // default value
    orderManagProductsGallery_Title.GridRows = null; // default value
    orderManagProductsGallery_Title.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    orderManagProductsGallery_Title.RowGap = null; // default value
    orderManagProductsGallery_Title.RowSpan = null; // default value
    orderManagProductsGallery_Title.SetStyle("\"margin-bottom-base font-semi-bold\"");
    orderManagProductsGallery_Title.SetVisible("True"); // default value
    orderManagProductsGallery_Title.VisibleWasExpression = false; // default value
    orderManagProductsGallery_Title.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    orderManagProductsGallery_Title.Width = "(fill parent)"; // default value
    
    var expression2 = orderManagProductsGallery_Title.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = "12 of 30 products";
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetProductsByProductName.List.Length + \" of \" + GetProductsByProductName.Count + \" products\"");
    expression2.Width = ""; // default value
    
    var hasLoadedProducts = orderManagProductsGallery_Wrapper.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasLoadedProducts");
    hasLoadedProducts.Animate = false; // default value
    hasLoadedProducts.SetCondition("GetProductsByProductName.IsDataFetched");
    hasLoadedProducts.CustomStyle = null; // default value
    hasLoadedProducts.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var hasProducts = hasLoadedProducts.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("HasProducts");
    hasProducts.Animate = false; // default value
    hasProducts.SetCondition("not GetProductsByProductName.List.Empty");
    hasProducts.CustomStyle = null; // default value
    hasProducts.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var orderManagProductsGallery_GalleryWrapper = hasProducts.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("OrderManagProductsGallery_GalleryWrapper");
    orderManagProductsGallery_GalleryWrapper.CustomStyle = null; // default value
    orderManagProductsGallery_GalleryWrapper.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var adaptive = outSystemsUI.MobileFlows.Named("Adaptive");
    var gallery = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Gallery");
    orderManagProductsGallery_GalleryWrapper.SourceBlock = gallery;
    // delete default content in Content placeholder
    var placeholderContentWidget7 = orderManagProductsGallery_GalleryWrapper.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    placeholderContentWidget7.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass3 = gallery.InputParameters.Named("ExtendedClass");
    orderManagProductsGallery_GalleryWrapper.SetArgumentValue(extendedClass3, null); // default value
    var rowItemsTablet = gallery.InputParameters.Named("RowItemsTablet");
    orderManagProductsGallery_GalleryWrapper.SetArgumentValue(rowItemsTablet, null);
    var rowItemsDesktop = gallery.InputParameters.Named("RowItemsDesktop");
    orderManagProductsGallery_GalleryWrapper.SetArgumentValue(rowItemsDesktop, null);
    var rowItemsPhone = gallery.InputParameters.Named("RowItemsPhone");
    orderManagProductsGallery_GalleryWrapper.SetArgumentValue(rowItemsPhone, null);
    var itemsGap = gallery.InputParameters.Named("ItemsGap");
    orderManagProductsGallery_GalleryWrapper.SetArgumentValue(itemsGap, null);
    var eventHandler4 = orderManagProductsGallery_GalleryWrapper.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler4.Handler = null; // default value
    
    var orderManagProductsGallery_List = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>("OrderManagProductsGallery_List");
    orderManagProductsGallery_List.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    orderManagProductsGallery_List.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderManagProductsGallery_List.AnimateItems = false;
    orderManagProductsGallery_List.ColumnGap = null; // default value
    orderManagProductsGallery_List.ColumnSpan = null; // default value
    orderManagProductsGallery_List.CustomStyle = null; // default value
    orderManagProductsGallery_List.ExpandedInWebEditor = true; // default value
    orderManagProductsGallery_List.FlexBasis = null; // default value
    orderManagProductsGallery_List.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    orderManagProductsGallery_List.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderManagProductsGallery_List.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    orderManagProductsGallery_List.Gap = null;
    orderManagProductsGallery_List.GridColumns = null; // default value
    orderManagProductsGallery_List.GridRows = null; // default value
    orderManagProductsGallery_List.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    orderManagProductsGallery_List.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    orderManagProductsGallery_List.RowGap = null; // default value
    orderManagProductsGallery_List.RowSpan = null; // default value
    orderManagProductsGallery_List.SetSource("GetProductsByProductName.List");
    orderManagProductsGallery_List.SetStyle("\"list list-group\""); // default value
    orderManagProductsGallery_List.Tag = "div"; // default value
    orderManagProductsGallery_List.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    orderManagProductsGallery_List.Width = "(fill parent)"; // default value
    
    var orderManagProductsGallery_ProductCard = orderManagProductsGallery_List.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("OrderManagProductsGallery_ProductCard");
    orderManagProductsGallery_ProductCard.CustomStyle = null; // default value
    orderManagProductsGallery_ProductCard.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var content = outSystemsUI.MobileFlows.Named("Content");
    var card = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Card");
    orderManagProductsGallery_ProductCard.SourceBlock = card;
    var extendedClass4 = card.InputParameters.Named("ExtendedClass");
    orderManagProductsGallery_ProductCard.SetArgumentValue(extendedClass4, null); // default value
    var usePadding = card.InputParameters.Named("UsePadding");
    orderManagProductsGallery_ProductCard.SetArgumentValue(usePadding, "False");
    
    var placeholderContentWidget8 = orderManagProductsGallery_ProductCard.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var orderManagProductsGallery_ImageWrapper = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OrderManagProductsGallery_ImageWrapper");
    orderManagProductsGallery_ImageWrapper.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    orderManagProductsGallery_ImageWrapper.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    orderManagProductsGallery_ImageWrapper.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderManagProductsGallery_ImageWrapper.Animate = false; // default value
    orderManagProductsGallery_ImageWrapper.ColumnGap = null; // default value
    orderManagProductsGallery_ImageWrapper.ColumnSpan = null; // default value
    orderManagProductsGallery_ImageWrapper.CustomStyle = null; // default value
    orderManagProductsGallery_ImageWrapper.FlexBasis = null; // default value
    orderManagProductsGallery_ImageWrapper.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    orderManagProductsGallery_ImageWrapper.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderManagProductsGallery_ImageWrapper.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    orderManagProductsGallery_ImageWrapper.Gap = null;
    orderManagProductsGallery_ImageWrapper.GridColumns = null; // default value
    orderManagProductsGallery_ImageWrapper.GridRows = null; // default value
    orderManagProductsGallery_ImageWrapper.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    orderManagProductsGallery_ImageWrapper.RowGap = null; // default value
    orderManagProductsGallery_ImageWrapper.RowSpan = null; // default value
    orderManagProductsGallery_ImageWrapper.SetStyle(null); // default value
    orderManagProductsGallery_ImageWrapper.SetVisible("True"); // default value
    orderManagProductsGallery_ImageWrapper.VisibleWasExpression = false; // default value
    orderManagProductsGallery_ImageWrapper.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    orderManagProductsGallery_ImageWrapper.Width = "(fill parent)"; // default value
    
    var link = orderManagProductsGallery_ImageWrapper.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
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
    
    var image = link.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image.ColumnSpan = null; // default value
    image.CustomStyle = "height: 200px;";
    var emptyImage = eSpace.Images.Named("EmptyImage");
    image.DefaultImage = emptyImage;
    image.FlexBasis = null; // default value
    image.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    image.Image = null; // default value
    image.SetImageContent("GetProductsByProductName.List.Current.ProductPicture.File");
    image.RowSpan = null; // default value
    image.SetStyle("\"img-cover border-radius-top-soft\"");
    image.Type = ServiceStudio.Plugin.NRWidgets.Enumerations.Type.Binary;
    image.SetUrl(null); // default value
    image.Width = "(fill parent)";
    
    var extendedProperty = image.CreateExtendedProperty();
    extendedProperty.Property = "alt";
    extendedProperty.SetValue("GetProductsByProductName.List.Current.Product.Name + \" image\"");
    var builtinEvent4 = link.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = productDetail;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent4.SetArgumentValue(productId, "GetProductsByProductName.List.Current.Product.Id");
    
    var orderManagProductsGallery_ProductInfo = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OrderManagProductsGallery_ProductInfo");
    orderManagProductsGallery_ProductInfo.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    orderManagProductsGallery_ProductInfo.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    orderManagProductsGallery_ProductInfo.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderManagProductsGallery_ProductInfo.Animate = false; // default value
    orderManagProductsGallery_ProductInfo.ColumnGap = null; // default value
    orderManagProductsGallery_ProductInfo.ColumnSpan = null; // default value
    orderManagProductsGallery_ProductInfo.CustomStyle = null; // default value
    orderManagProductsGallery_ProductInfo.FlexBasis = null; // default value
    orderManagProductsGallery_ProductInfo.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    orderManagProductsGallery_ProductInfo.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderManagProductsGallery_ProductInfo.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    orderManagProductsGallery_ProductInfo.Gap = null;
    orderManagProductsGallery_ProductInfo.GridColumns = null; // default value
    orderManagProductsGallery_ProductInfo.GridRows = null; // default value
    orderManagProductsGallery_ProductInfo.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    orderManagProductsGallery_ProductInfo.RowGap = null; // default value
    orderManagProductsGallery_ProductInfo.RowSpan = null; // default value
    orderManagProductsGallery_ProductInfo.SetStyle("\"padding-base\"");
    orderManagProductsGallery_ProductInfo.SetVisible("True"); // default value
    orderManagProductsGallery_ProductInfo.VisibleWasExpression = false; // default value
    orderManagProductsGallery_ProductInfo.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    orderManagProductsGallery_ProductInfo.Width = "(fill parent)"; // default value
    
    var orderManagProductsGallery_ProductTitle = orderManagProductsGallery_ProductInfo.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OrderManagProductsGallery_ProductTitle");
    orderManagProductsGallery_ProductTitle.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    orderManagProductsGallery_ProductTitle.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    orderManagProductsGallery_ProductTitle.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderManagProductsGallery_ProductTitle.Animate = false; // default value
    orderManagProductsGallery_ProductTitle.ColumnGap = null; // default value
    orderManagProductsGallery_ProductTitle.ColumnSpan = null; // default value
    orderManagProductsGallery_ProductTitle.CustomStyle = null; // default value
    orderManagProductsGallery_ProductTitle.FlexBasis = null; // default value
    orderManagProductsGallery_ProductTitle.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    orderManagProductsGallery_ProductTitle.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderManagProductsGallery_ProductTitle.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    orderManagProductsGallery_ProductTitle.Gap = null;
    orderManagProductsGallery_ProductTitle.GridColumns = null; // default value
    orderManagProductsGallery_ProductTitle.GridRows = null; // default value
    orderManagProductsGallery_ProductTitle.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    orderManagProductsGallery_ProductTitle.RowGap = null; // default value
    orderManagProductsGallery_ProductTitle.RowSpan = null; // default value
    orderManagProductsGallery_ProductTitle.SetStyle("\"heading6 margin-bottom-s\"");
    orderManagProductsGallery_ProductTitle.SetVisible("True"); // default value
    orderManagProductsGallery_ProductTitle.VisibleWasExpression = false; // default value
    orderManagProductsGallery_ProductTitle.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    orderManagProductsGallery_ProductTitle.Width = "(fill parent)"; // default value
    
    var expression3 = orderManagProductsGallery_ProductTitle.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = "Black and silver headphones";
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("GetProductsByProductName.List.Current.Product.Name");
    expression3.Width = ""; // default value
    
    var orderManagProductsGallery_ProductReference = orderManagProductsGallery_ProductInfo.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OrderManagProductsGallery_ProductReference");
    orderManagProductsGallery_ProductReference.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    orderManagProductsGallery_ProductReference.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    orderManagProductsGallery_ProductReference.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderManagProductsGallery_ProductReference.Animate = false; // default value
    orderManagProductsGallery_ProductReference.ColumnGap = null; // default value
    orderManagProductsGallery_ProductReference.ColumnSpan = null; // default value
    orderManagProductsGallery_ProductReference.CustomStyle = null; // default value
    orderManagProductsGallery_ProductReference.FlexBasis = null; // default value
    orderManagProductsGallery_ProductReference.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    orderManagProductsGallery_ProductReference.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderManagProductsGallery_ProductReference.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    orderManagProductsGallery_ProductReference.Gap = null;
    orderManagProductsGallery_ProductReference.GridColumns = null; // default value
    orderManagProductsGallery_ProductReference.GridRows = null; // default value
    orderManagProductsGallery_ProductReference.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    orderManagProductsGallery_ProductReference.RowGap = null; // default value
    orderManagProductsGallery_ProductReference.RowSpan = null; // default value
    orderManagProductsGallery_ProductReference.SetStyle("\"font-size-base text-neutral-7 margin-bottom-base\"");
    orderManagProductsGallery_ProductReference.SetVisible("True"); // default value
    orderManagProductsGallery_ProductReference.VisibleWasExpression = false; // default value
    orderManagProductsGallery_ProductReference.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    orderManagProductsGallery_ProductReference.Width = "(fill parent)"; // default value
    
    var expression4 = orderManagProductsGallery_ProductReference.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = "REF 113829";
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("\"REF \" + GetProductsByProductName.List.Current.Product.Reference");
    expression4.Width = ""; // default value
    
    var orderManagProductsGallery_ProductStockInfo = orderManagProductsGallery_ProductInfo.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OrderManagProductsGallery_ProductStockInfo");
    orderManagProductsGallery_ProductStockInfo.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    orderManagProductsGallery_ProductStockInfo.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    orderManagProductsGallery_ProductStockInfo.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderManagProductsGallery_ProductStockInfo.Animate = false; // default value
    orderManagProductsGallery_ProductStockInfo.ColumnGap = null; // default value
    orderManagProductsGallery_ProductStockInfo.ColumnSpan = null; // default value
    orderManagProductsGallery_ProductStockInfo.CustomStyle = null; // default value
    orderManagProductsGallery_ProductStockInfo.FlexBasis = null; // default value
    orderManagProductsGallery_ProductStockInfo.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    orderManagProductsGallery_ProductStockInfo.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderManagProductsGallery_ProductStockInfo.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    orderManagProductsGallery_ProductStockInfo.Gap = null;
    orderManagProductsGallery_ProductStockInfo.GridColumns = null; // default value
    orderManagProductsGallery_ProductStockInfo.GridRows = null; // default value
    orderManagProductsGallery_ProductStockInfo.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    orderManagProductsGallery_ProductStockInfo.RowGap = null; // default value
    orderManagProductsGallery_ProductStockInfo.RowSpan = null; // default value
    orderManagProductsGallery_ProductStockInfo.SetStyle(null); // default value
    orderManagProductsGallery_ProductStockInfo.SetVisible("True"); // default value
    orderManagProductsGallery_ProductStockInfo.VisibleWasExpression = false; // default value
    orderManagProductsGallery_ProductStockInfo.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    orderManagProductsGallery_ProductStockInfo.Width = "(fill parent)"; // default value
    
    var tagInstance = orderManagProductsGallery_ProductStockInfo.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    tagInstance.CustomStyle = null; // default value
    tagInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var tag = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Tag");
    tagInstance.SourceBlock = tag;
    var isLight = tag.InputParameters.Named("IsLight");
    tagInstance.SetArgumentValue(isLight, "True");
    var shape = tag.InputParameters.Named("Shape");
    tagInstance.SetArgumentValue(shape, null);
    var color = tag.InputParameters.Named("Color");
    tagInstance.SetArgumentValue(color, "If(GetProductsByProductName.List.Current.Product.Stock > 0,\n    If(GetProductsByProductName.List.Current.Product.Stock < 5,\n        Entities.Color.Yellow, Entities.Color.Green\n    ),\n    Entities.Color.Red\n)");
    var extendedClass5 = tag.InputParameters.Named("ExtendedClass");
    tagInstance.SetArgumentValue(extendedClass5, null); // default value
    var size = tag.InputParameters.Named("Size");
    tagInstance.SetArgumentValue(size, "Entities.Size.Small");
    
    var placeholderContentWidget9 = tagInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Tag");
    var expression5 = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = "4 in stock";
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle("If(GetProductsByProductName.List.Current.Product.Stock > 0,\n    If(GetProductsByProductName.List.Current.Product.Stock < 5,\n        \"text-yellow-dark\", \"text-green-dark\"\n    ),\n    \"text-error\"\n)");
    expression5.SetValue("If(GetProductsByProductName.List.Current.Product.Stock > 0, \n    GetProductsByProductName.List.Current.Product.Stock + \" in stock\",\n    \"Out of stock\"\n)");
    expression5.Width = ""; // default value
    var builtinEvent5 = orderManagProductsGallery_List.OnScrollEnding;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = null; // default value
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty2 = orderManagProductsGallery_List.CreateExtendedProperty();
    extendedProperty2.Property = "disable-virtualization";
    extendedProperty2.SetValue("True");
    
    var if_MoreProductsToLoad = hasProducts.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("If_MoreProductsToLoad");
    if_MoreProductsToLoad.Animate = false; // default value
    if_MoreProductsToLoad.SetCondition("GetProductsByProductName.List.Length < GetProductsByProductName.Count");
    if_MoreProductsToLoad.CustomStyle = null; // default value
    if_MoreProductsToLoad.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var container5 = if_MoreProductsToLoad.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container5.Animate = false; // default value
    container5.ColumnGap = null; // default value
    container5.ColumnSpan = null; // default value
    container5.CustomStyle = null; // default value
    container5.FlexBasis = null; // default value
    container5.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container5.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container5.Gap = null;
    container5.GridColumns = null; // default value
    container5.GridRows = null; // default value
    container5.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container5.RowGap = null; // default value
    container5.RowSpan = null; // default value
    container5.SetStyle("\"text-align-center margin-top-l\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var btn_LoadMore = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>("Btn_LoadMore");
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    btn_LoadMore.Widgets.ToList().ForEach(x => x.Delete());
    btn_LoadMore.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    btn_LoadMore.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    btn_LoadMore.ColumnGap = null; // default value
    btn_LoadMore.ColumnSpan = null; // default value
    btn_LoadMore.SetConfirmationMessage(null); // default value
    btn_LoadMore.CustomStyle = null; // default value
    btn_LoadMore.SetEnabled("True");
    btn_LoadMore.FlexBasis = null; // default value
    btn_LoadMore.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    btn_LoadMore.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    btn_LoadMore.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    btn_LoadMore.Gap = null;
    btn_LoadMore.GridColumns = null; // default value
    btn_LoadMore.GridRows = null; // default value
    btn_LoadMore.IsDefault = false; // default value
    btn_LoadMore.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    btn_LoadMore.RowGap = null; // default value
    btn_LoadMore.RowSpan = null; // default value
    btn_LoadMore.SetStyle("\"btn btn-load-more\"");
    btn_LoadMore.SetVisible("True"); // default value
    btn_LoadMore.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    btn_LoadMore.Width = ""; // default value
    
    var icon = btn_LoadMore.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon.ColumnSpan = null; // default value
    icon.CustomStyle = null; // default value
    icon.FlexBasis = null; // default value
    icon.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon.Icon = "chevron-down";
    icon.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon.RowSpan = null; // default value
    icon.SetStyle("\"icon margin-right-s\"");
    icon.SetVisible("True"); // default value
    icon.Weight = null; // default value
    
    var textWidget3 = btn_LoadMore.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Load more";
    var builtinEvent6 = btn_LoadMore.OnClick;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = loadMoreOnClick;
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var orderManagProductsGallery_BlankSlate = hasProducts.FalseBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("OrderManagProductsGallery_BlankSlate");
    orderManagProductsGallery_BlankSlate.CustomStyle = null; // default value
    orderManagProductsGallery_BlankSlate.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var blankSlate = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("BlankSlate");
    orderManagProductsGallery_BlankSlate.SourceBlock = blankSlate;
    // delete default content in Icon placeholder
    var placeholderContentWidget10 = orderManagProductsGallery_BlankSlate.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    placeholderContentWidget10.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass6 = blankSlate.InputParameters.Named("ExtendedClass");
    orderManagProductsGallery_BlankSlate.SetArgumentValue(extendedClass6, "\"card\"");
    var fullHeight = blankSlate.InputParameters.Named("FullHeight");
    orderManagProductsGallery_BlankSlate.SetArgumentValue(fullHeight, "False");
    
    var image2 = placeholderContentWidget10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
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
    
    var placeholderContentWidget11 = orderManagProductsGallery_BlankSlate.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var textWidget4 = placeholderContentWidget11.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "There are no products to display.";
    
    var fetchingRecords = hasLoadedProducts.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("FetchingRecords");
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
    
    var wasAccelerator = orderManagProductsGallery_Wrapper.CreateMetadata<OutSystems.Model.IBooleanMetadata>("WasAccelerator", null);
    wasAccelerator.Category = null; // default value
    wasAccelerator.DiscardOnDuplicate = false; // default value
    wasAccelerator.Hidden = true; // default value
    wasAccelerator.Mandatory = false; // default value
    wasAccelerator.ReadOnly = false; // default value
    wasAccelerator.Value = true;
    
    var acceleratorName = orderManagProductsGallery_Wrapper.CreateMetadata<OutSystems.Model.ITextMetadata>("AcceleratorName", null);
    acceleratorName.Category = null; // default value
    acceleratorName.DiscardOnDuplicate = false; // default value
    acceleratorName.Hidden = true; // default value
    acceleratorName.Mandatory = false; // default value
    acceleratorName.ReadOnly = false; // default value
    acceleratorName.Value = "ProductGallery";
    
    var placeholderContentWidget12 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Footer");
    var pageFooter = placeholderContentWidget12.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("PageFooter");
    pageFooter.CustomStyle = null; // default value
    pageFooter.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var footer = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Footer");
    pageFooter.SourceBlock = footer;
}

```