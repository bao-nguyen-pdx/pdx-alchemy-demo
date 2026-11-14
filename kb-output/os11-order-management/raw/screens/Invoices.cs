```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow order) => {
    
    /*** creating IMobileScreen 'Invoices' ***/
    var invoices = order.CreateScreen("Invoices");
    invoices.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = order.GetESpace();
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    invoices.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    invoices.Roles.Add(orderManager);
    invoices.AnonymousAccess = false; // default value
    invoices.CustomURL = false; // default value
    invoices.Description = null; // default value
    invoices.HorizontalPosition = 3428;
    invoices.Icon = null; // default value
    invoices.IsDisabled = false; // default value
    invoices.PageName = null; // default value
    invoices.Public = false; // default value
    invoices.Shareable = false; // default value
    invoices.StyleSheet = null; // default value
    invoices.SetTitle("\"Order Management - Invoices\"");
    invoices.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    invoices.VerticalPosition = 914;
    
    var getOrdersByEmployeeId = invoices.CreateScreenAggregate(false, "GetOrdersByEmployeeId");
    getOrdersByEmployeeId.CacheInMinutes = null; // default value
    getOrdersByEmployeeId.Description = null; // default value
    getOrdersByEmployeeId.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getOrdersByEmployeeId.SetMaxRecords("MaxRecords");
    getOrdersByEmployeeId.SetStartIndex("StartIndex");
    getOrdersByEmployeeId.Timeout = null; // default value
    var order3 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Order");
    var order2 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateSource(order3);
    var account2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Account");
    var account = getOrdersByEmployeeId.AsDatabaseAggregate.CreateSource(account2);
    var orderStatus2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IStaticEntitySignature>().Named("OrderStatus");
    var orderStatus = getOrdersByEmployeeId.AsDatabaseAggregate.CreateSource(orderStatus2);
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getOrdersByEmployeeId.AsDatabaseAggregate.CreateSource(user2);
    var join = getOrdersByEmployeeId.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = account;
    join.RightSource = order2;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("Order.AccountId = Account.Id");
    var join2 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = user;
    join2.RightSource = order2;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join2.SetCondition("Order.CreatedBy = User.Id");
    var join3 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateJoin();
    join3.LeftSource = order2;
    join3.RightSource = orderStatus;
    join3.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join3.SetCondition("Order.OrderStatusId = OrderStatus.Id");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateFilter("Account.Code like \"%\" + SearchKeyword + \"%\" or\nAccount.Company like \"%\" + SearchKeyword + \"%\" or\nOrder.Code like \"%\" + SearchKeyword + \"%\" or \nSearchKeyword = \"\"");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateFilter("(UserDropdownList.Value = \"\") or (UserDropdownList.Value = NullIdentifier()) or (Order.CreatedBy = LongIntegerToIdentifier(TextToLongInteger(UserDropdownList.Value)))");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateFilter("Order.IsActive=True");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateFilter("Order.OrderStatusId=Entities.OrderStatus.Closed");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateSort().SetAttribute("TableSort");
    
    
    var getEmployees = invoices.CreateDataAction("GetEmployees");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getEmployees.OutputParameters.ToList().ForEach(x => x.Delete());
    getEmployees.Description = null; // default value
    getEmployees.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getEmployees.ServerRequestTimeout = null; // default value
    
    var startNode = getEmployees.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var getEmployees2 = getEmployees.CreateNode<OutSystems.Model.Logic.Nodes.ISQLNode>("GetEmployees");
    getEmployees2.CacheInMinutes = null; // default value
    getEmployees2.Description = null; // default value
    getEmployees2.HorizontalPosition = 3192;
    getEmployees2.IsDisabled = false; // default value
    getEmployees2.SetMaxRecords(null); // default value
    getEmployees2.Statement = "SELECT distinct {User}.* \nfrom {User}\nwhere {User}.[Id] in (select {Order}.[CreatedBy] from {Order})\norder by {User}.[Name]";
    getEmployees2.Timeout = null; // default value
    getEmployees2.VerticalPosition = 2742;
    
    var user3 = getEmployees2.CreateOutput(user2, "User");
    startNode.Target = getEmployees2;
    
    var assignNode = getEmployees.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getEmployees2, 1829);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("UserList", "GetEmployees.List mapTo {  }");
    
    var endNode = getEmployees.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 3196;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 6628;
    assignNode.Target = endNode;
    getEmployees.OnAfterFetch.Destination = null; // default value
    
    var userList = getEmployees.CreateOutputParameter("UserList");
    userList.DataType = eSpace.GetOrCreateListType(user2);
    userList.Description = null; // default value
    
    var searchKeyword = invoices.CreateLocalVariable("SearchKeyword");
    searchKeyword.DataType = eSpace.TextType;
    searchKeyword.SetDefaultValue(null); // default value
    searchKeyword.Description = null; // default value
    
    var userDropdownList = invoices.CreateLocalVariable("UserDropdownList");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var dropdownOption = outSystemsUI.Structures.Named("DropdownOption");
    userDropdownList.DataType = dropdownOption;
    userDropdownList.Description = null; // default value
    
    var startIndex = invoices.CreateLocalVariable("StartIndex");
    startIndex.DataType = eSpace.IntegerType;
    startIndex.SetDefaultValue("0");
    startIndex.Description = null; // default value
    
    var maxRecords = invoices.CreateLocalVariable("MaxRecords");
    maxRecords.DataType = eSpace.IntegerType;
    maxRecords.SetDefaultValue("20");
    maxRecords.Description = null; // default value
    
    var tableSort = invoices.CreateLocalVariable("TableSort");
    tableSort.DataType = eSpace.TextType;
    tableSort.SetDefaultValue("\"{Account}.[Company]\"");
    tableSort.Description = null; // default value
    invoices.OnDestroy.Destination = null; // default value
    invoices.OnInitialize.Destination = null; // default value
    invoices.OnReady.Destination = null; // default value
    invoices.OnRender.Destination = null; // default value
    
    var dropdownSearchOnSelect = invoices.CreateScreenAction("DropdownSearchOnSelect");
    dropdownSearchOnSelect.Description = null; // default value
    
    var selectedOptionList = dropdownSearchOnSelect.CreateInputParameter("SelectedOptionList");
    selectedOptionList.DataType = eSpace.GetOrCreateListType(dropdownOption);
    selectedOptionList.Description = null; // default value
    selectedOptionList.IsMandatory = true; // default value
    
    var startNode2 = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var assignNode2 = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode2.HorizontalPosition = 3200;
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.VerticalPosition = 2286;
    
    var assignment2 = assignNode2.CreateAssignment("UserDropdownList", "SelectedOptionList.Current");
    startNode2.Target = assignNode2;
    
    var onRefreshTable = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("OnRefreshTable").ConnectedBelow(assignNode2, 1371);
    onRefreshTable.IsDisabled = false; // default value
    
    var onRefreshTable2 = invoices.CreateScreenAction("OnRefreshTable");
    onRefreshTable2.Description = null; // default value
    
    var resetFilters = onRefreshTable2.CreateInputParameter("ResetFilters");
    resetFilters.DataType = eSpace.BooleanType;
    resetFilters.Description = null; // default value
    resetFilters.IsMandatory = true; // default value
    
    var resetPagination = onRefreshTable2.CreateInputParameter("ResetPagination");
    resetPagination.DataType = eSpace.BooleanType;
    resetPagination.Description = null; // default value
    resetPagination.IsMandatory = true; // default value
    
    var startNode3 = onRefreshTable2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 4564;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 800;
    
    var ifNode = onRefreshTable2.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode.SetCondition("ResetFilters");
    ifNode.HorizontalPosition = 4560;
    ifNode.IsDisabled = false; // default value
    ifNode.Label = null; // default value
    ifNode.VerticalPosition = 2052;
    
    var assignNode3 = onRefreshTable2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 2964);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    
    var assignment3 = assignNode3.CreateAssignment("SearchKeyword", "\"\"");
    
    var assignment4 = assignNode3.CreateAssignment("UserDropdownList.Value", "\"\"");
    
    var assignNode4 = onRefreshTable2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode4.HorizontalPosition = 2280;
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    assignNode4.VerticalPosition = 3876;
    
    var ifNode2 = onRefreshTable2.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1833);
    ifNode2.SetCondition("ResetPagination");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = null; // default value
    ifNode2.TrueTarget = assignNode4;
    
    var refreshDataNode = onRefreshTable2.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().Below(ifNode2, 2043);
    refreshDataNode.DataSource = getOrdersByEmployeeId;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords("MaxRecords");
    refreshDataNode.SetStartIndex("StartIndex");
    
    var endNode2 = onRefreshTable2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 4564;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 8457;
    onRefreshTable.Action = onRefreshTable2;
    
    var endNode3 = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode3.HorizontalPosition = 3196;
    endNode3.IsDisabled = false; // default value
    endNode3.VerticalPosition = 5120;
    onRefreshTable.Target = endNode3;
    onRefreshTable.SetArgumentValue(resetFilters, "False");
    onRefreshTable.SetArgumentValue(resetPagination, "True");
    startNode3.Target = ifNode;
    ifNode.FalseTarget = ifNode2;
    ifNode.TrueTarget = assignNode3;
    assignNode3.Target = ifNode2;
    assignNode4.Target = refreshDataNode;
    ifNode2.FalseTarget = refreshDataNode;
    refreshDataNode.Target = endNode2;
    
    var onNavigate = invoices.CreateScreenAction("OnNavigate");
    onNavigate.Description = null; // default value
    
    var newStartIndex = onNavigate.CreateInputParameter("NewStartIndex");
    newStartIndex.DataType = eSpace.IntegerType;
    newStartIndex.Description = null; // default value
    newStartIndex.IsMandatory = true; // default value
    
    var startNode4 = onNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.IsDisabled = false; // default value
    
    var assignNode5 = onNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode5.HorizontalPosition = 3192;
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    assignNode5.VerticalPosition = 2736;
    
    var assignment5 = assignNode5.CreateAssignment("StartIndex", "NewStartIndex");
    startNode4.Target = assignNode5;
    
    var refreshDataNode2 = onNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode5, 2292);
    refreshDataNode2.DataSource = getOrdersByEmployeeId;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords("MaxRecords");
    refreshDataNode2.SetStartIndex("StartIndex");
    
    var endNode4 = onNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode4.HorizontalPosition = 3199;
    endNode4.IsDisabled = false; // default value
    endNode4.VerticalPosition = 6628;
    refreshDataNode2.Target = endNode4;
    
    var onSort = invoices.CreateScreenAction("OnSort");
    onSort.Description = null; // default value
    
    var sortBy = onSort.CreateInputParameter("SortBy");
    sortBy.DataType = eSpace.TextType;
    sortBy.Description = "Table column by which the aggregate will be sorted.";
    sortBy.IsMandatory = true; // default value
    
    var startNode5 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.IsDisabled = false; // default value
    
    var ifNode3 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode5, 1330);
    ifNode3.SetCondition("TableSort = SortBy");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = null; // default value
    
    var assignNode6 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode3, 1330);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = "TableSort = SortBy";
    
    var assignment6 = assignNode6.CreateAssignment("TableSort", "SortBy");
    ifNode3.FalseTarget = assignNode6;
    
    var assignNode7 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode3, 1772);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = "TableSort += DESC";
    
    var assignment7 = assignNode7.CreateAssignment("TableSort", "SortBy + \" DESC\"");
    ifNode3.TrueTarget = assignNode7;
    
    var assignNode8 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(assignNode6, 1330);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = "StartIndex = 0";
    
    var assignment8 = assignNode8.CreateAssignment("StartIndex", "0");
    assignNode7.Target = assignNode8;
    
    var refreshDataNode3 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode8, 1330);
    refreshDataNode3.DataSource = getOrdersByEmployeeId;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords("MaxRecords");
    refreshDataNode3.SetStartIndex("StartIndex");
    
    var endNode5 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode3, 1330);
    endNode5.IsDisabled = false; // default value
    
    var commentNode = onSort.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode.HorizontalPosition = 6392;
    commentNode.IsDisabled = false; // default value
    commentNode.IsReminder = false; // default value
    commentNode.Text = "This on sort action was automatically generated.";
    commentNode.VerticalPosition = 800;
    
    var commentNode2 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode2.HorizontalPosition = 5856;
    commentNode2.IsDisabled = false; // default value
    commentNode2.IsReminder = false; // default value
    commentNode2.Text = "This brings the pagination back to the first \n page everytime the aggregate is sorted.";
    commentNode2.VerticalPosition = 3460;
    commentNode2.CreateConnector(assignNode8);
    
    var layoutTopHeaderInstance = invoices.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    layoutTopHeaderInstance.SetArgumentValue(hasFixedHeader, null);
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeMenu = menu.InputParameters.Named("ActiveMenu");
    menuInstance.SetArgumentValue(activeMenu, "Entities.MenuItem.Invoices");
    
    var placeholderContentWidget2 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Invoices";
    
    var placeholderContentWidget3 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Controls");
    var container = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    var placeholderContentWidget4 = dEPRECATED_SearchInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Input");
    placeholderContentWidget4.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass = dEPRECATED_Search.InputParameters.Named("ExtendedClass");
    dEPRECATED_SearchInstance.SetArgumentValue(extendedClass, null); // default value
    var eventHandler = dEPRECATED_SearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnCollapse");
    eventHandler.Handler = null; // default value
    
    var input_Order_List_SearchKeyword = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_Order_List_SearchKeyword");
    input_Order_List_SearchKeyword.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_Order_List_SearchKeyword.ColumnSpan = null; // default value
    input_Order_List_SearchKeyword.CustomStyle = "padding: 5px 16px 5px 40px;";
    input_Order_List_SearchKeyword.SetEnabled("True");
    input_Order_List_SearchKeyword.FlexBasis = null; // default value
    input_Order_List_SearchKeyword.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_Order_List_SearchKeyword.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Search;
    input_Order_List_SearchKeyword.SetMandatory("False");
    input_Order_List_SearchKeyword.MaxLength = null;
    input_Order_List_SearchKeyword.SetPrompt("\"Type the order number or account\"");
    input_Order_List_SearchKeyword.RowSpan = null; // default value
    input_Order_List_SearchKeyword.SetStyle("\"form-control search-input\"");
    input_Order_List_SearchKeyword.SetVariable("SearchKeyword");
    input_Order_List_SearchKeyword.Width = "(fill parent)"; // default value
    var builtinEvent = input_Order_List_SearchKeyword.OnChange;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = onRefreshTable2;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent.SetArgumentValue(resetFilters, "False");
    builtinEvent.SetArgumentValue(resetPagination, "True");
    
    var container2 = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle("\"second-search\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "";
    
    var dropdownSearchInstance = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dropdownSearchInstance.CustomStyle = null; // default value
    dropdownSearchInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dropdownSearch = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DropdownSearch");
    dropdownSearchInstance.SourceBlock = dropdownSearch;
    var extendedClass2 = dropdownSearch.InputParameters.Named("ExtendedClass");
    dropdownSearchInstance.SetArgumentValue(extendedClass2, "\"search-dropdown\"");
    var optionsList = dropdownSearch.InputParameters.Named("OptionsList");
    dropdownSearchInstance.SetArgumentValue(optionsList, "GetEmployees.UserList mapTo { Value: Id, Label: Name }");
    var optionalConfigs = dropdownSearch.InputParameters.Named("OptionalConfigs");
    dropdownSearchInstance.SetArgumentValue(optionalConfigs, "{ SearchPrompt: \"Search by Employee\" }");
    var prompt = dropdownSearch.InputParameters.Named("Prompt");
    dropdownSearchInstance.SetArgumentValue(prompt, "\"All Employees\"");
    var startingSelection = dropdownSearch.InputParameters.Named("StartingSelection");
    dropdownSearchInstance.SetArgumentValue(startingSelection, null); // default value
    var eventHandler2 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler2.Handler = null; // default value
    var eventHandler3 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnChanged");
    eventHandler3.Handler = dropdownSearchOnSelect;
    eventHandler3.SetArgumentValue(selectedOptionList, "SelectedOptionList");
    
    var placeholderContentWidget5 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var ifWidget = placeholderContentWidget5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.Animate = false; // default value
    ifWidget.SetCondition("GetOrdersByEmployeeId.List.Empty and GetOrdersByEmployeeId.IsDataFetched");
    ifWidget.CustomStyle = null; // default value
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container3 = ifWidget.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container3.SetStyle("\"margin-top-xl\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var blankSlateInstance = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    blankSlateInstance.CustomStyle = null; // default value
    blankSlateInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var content = outSystemsUI.MobileFlows.Named("Content");
    var blankSlate = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("BlankSlate");
    blankSlateInstance.SourceBlock = blankSlate;
    // delete default content in Icon placeholder
    var placeholderContentWidget6 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    placeholderContentWidget6.Widgets.ToList().ForEach(w => w.Delete());
    var fullHeight = blankSlate.InputParameters.Named("FullHeight");
    blankSlateInstance.SetArgumentValue(fullHeight, null);
    var extendedClass3 = blankSlate.InputParameters.Named("ExtendedClass");
    blankSlateInstance.SetArgumentValue(extendedClass3, null); // default value
    
    var image = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image.ColumnSpan = null; // default value
    image.CustomStyle = null; // default value
    image.DefaultImage = null; // default value
    image.FlexBasis = null; // default value
    image.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    var invoiceIcon = eSpace.Images.Named("InvoiceIcon");
    image.Image = invoiceIcon;
    image.SetImageContent(null); // default value
    image.RowSpan = null; // default value
    image.SetStyle(null); // default value
    image.Type = ServiceStudio.Plugin.NRWidgets.Enumerations.Type.Static; // default value
    image.SetUrl(null); // default value
    image.Width = ""; // default value
    
    var placeholderContentWidget7 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var textWidget2 = placeholderContentWidget7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses("\"text-neutral-8\"");
    textWidget2.Text = "There aren\'t any Invoices to show.";
    
    var orderTable_Wrapper = ifWidget.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OrderTable_Wrapper");
    orderTable_Wrapper.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    orderTable_Wrapper.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    orderTable_Wrapper.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderTable_Wrapper.Animate = false; // default value
    orderTable_Wrapper.ColumnGap = null; // default value
    orderTable_Wrapper.ColumnSpan = null; // default value
    orderTable_Wrapper.CustomStyle = "margin-top: 20px;";
    orderTable_Wrapper.FlexBasis = null; // default value
    orderTable_Wrapper.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    orderTable_Wrapper.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderTable_Wrapper.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    orderTable_Wrapper.Gap = null;
    orderTable_Wrapper.GridColumns = null; // default value
    orderTable_Wrapper.GridRows = null; // default value
    orderTable_Wrapper.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    orderTable_Wrapper.RowGap = null; // default value
    orderTable_Wrapper.RowSpan = null; // default value
    orderTable_Wrapper.SetStyle(null); // default value
    orderTable_Wrapper.SetVisible("True"); // default value
    orderTable_Wrapper.VisibleWasExpression = false; // default value
    orderTable_Wrapper.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    orderTable_Wrapper.Width = "(fill parent)"; // default value
    
    var tableRecords = orderTable_Wrapper.CreateWidget<ServiceStudio.Plugin.NRWidgets.ITableRecords>();
    tableRecords.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    tableRecords.ColumnSpan = null; // default value
    tableRecords.CustomStyle = null; // default value
    tableRecords.ExpandedInWebEditor = true; // default value
    tableRecords.FlexBasis = null; // default value
    tableRecords.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    tableRecords.RowSpan = null; // default value
    tableRecords.ShowHeader = true; // default value
    tableRecords.SetSource("GetOrdersByEmployeeId.List");
    tableRecords.SetStyle("\"table\""); // default value
    tableRecords.SetStyleHeader("\"table-header\""); // default value
    tableRecords.SetStyleRow("\"table-row\""); // default value
    
    var headerCell = tableRecords.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell.ColumnGap = null; // default value
    headerCell.ColumnSpan = null; // default value
    headerCell.CustomStyle = null; // default value
    headerCell.FlexBasis = null; // default value
    headerCell.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    headerCell.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    headerCell.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    headerCell.Gap = null;
    headerCell.GridColumns = null; // default value
    headerCell.GridRows = null; // default value
    headerCell.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    headerCell.RowGap = null; // default value
    headerCell.RowSpan = null; // default value
    headerCell.SetSortAttribute("Order.Code");
    headerCell.SetSource("GetOrdersByEmployeeId.List");
    headerCell.SetStyle(null); // default value
    headerCell.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget3 = headerCell.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Number";
    
    var headerCell2 = tableRecords.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell2.ColumnGap = null; // default value
    headerCell2.ColumnSpan = null; // default value
    headerCell2.CustomStyle = null; // default value
    headerCell2.FlexBasis = null; // default value
    headerCell2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    headerCell2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    headerCell2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    headerCell2.Gap = null;
    headerCell2.GridColumns = null; // default value
    headerCell2.GridRows = null; // default value
    headerCell2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    headerCell2.RowGap = null; // default value
    headerCell2.RowSpan = null; // default value
    headerCell2.SetSortAttribute("Account.Company");
    headerCell2.SetSource("GetOrdersByEmployeeId.List");
    headerCell2.SetStyle(null); // default value
    headerCell2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget4 = headerCell2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Account";
    
    var headerCell3 = tableRecords.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell3.ColumnGap = null; // default value
    headerCell3.ColumnSpan = null; // default value
    headerCell3.CustomStyle = "text-align: right;";
    headerCell3.FlexBasis = null; // default value
    headerCell3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    headerCell3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    headerCell3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    headerCell3.Gap = null;
    headerCell3.GridColumns = null; // default value
    headerCell3.GridRows = null; // default value
    headerCell3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    headerCell3.RowGap = null; // default value
    headerCell3.RowSpan = null; // default value
    headerCell3.SetSortAttribute("Order.TotalAmount");
    headerCell3.SetSource("GetOrdersByEmployeeId.List");
    headerCell3.SetStyle(null); // default value
    headerCell3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget5 = headerCell3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Total Amount";
    
    var headerCell4 = tableRecords.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell4.ColumnGap = null; // default value
    headerCell4.ColumnSpan = null; // default value
    headerCell4.CustomStyle = null; // default value
    headerCell4.FlexBasis = null; // default value
    headerCell4.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    headerCell4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    headerCell4.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    headerCell4.Gap = null;
    headerCell4.GridColumns = null; // default value
    headerCell4.GridRows = null; // default value
    headerCell4.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    headerCell4.RowGap = null; // default value
    headerCell4.RowSpan = null; // default value
    headerCell4.SetSortAttribute("User.Name");
    headerCell4.SetSource("GetOrdersByEmployeeId.List");
    headerCell4.SetStyle(null); // default value
    headerCell4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget6 = headerCell4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Employee";
    
    var headerCell5 = tableRecords.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell5.ColumnGap = null; // default value
    headerCell5.ColumnSpan = null; // default value
    headerCell5.CustomStyle = null; // default value
    headerCell5.FlexBasis = null; // default value
    headerCell5.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    headerCell5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    headerCell5.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    headerCell5.Gap = null;
    headerCell5.GridColumns = null; // default value
    headerCell5.GridRows = null; // default value
    headerCell5.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    headerCell5.RowGap = null; // default value
    headerCell5.RowSpan = null; // default value
    headerCell5.SetSortAttribute("Order.ExpectedClosedOn");
    headerCell5.SetSource("GetOrdersByEmployeeId.List");
    headerCell5.SetStyle(null); // default value
    headerCell5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget7 = headerCell5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Order Date";
    
    var headerCell6 = tableRecords.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell6.ColumnGap = null; // default value
    headerCell6.ColumnSpan = null; // default value
    headerCell6.CustomStyle = null; // default value
    headerCell6.FlexBasis = null; // default value
    headerCell6.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    headerCell6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    headerCell6.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    headerCell6.Gap = null;
    headerCell6.GridColumns = null; // default value
    headerCell6.GridRows = null; // default value
    headerCell6.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    headerCell6.RowGap = null; // default value
    headerCell6.RowSpan = null; // default value
    headerCell6.SetSortAttribute("Order.RequiredOn");
    headerCell6.SetSource("GetOrdersByEmployeeId.List");
    headerCell6.SetStyle(null); // default value
    headerCell6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget8 = headerCell6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.CustomStyle = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Required By";
    
    var rowCell = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell.ColumnGap = null; // default value
    rowCell.ColumnSpan = null; // default value
    rowCell.CustomStyle = null; // default value
    rowCell.FlexBasis = null; // default value
    rowCell.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rowCell.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rowCell.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rowCell.Gap = null;
    rowCell.GridColumns = null; // default value
    rowCell.GridRows = null; // default value
    rowCell.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rowCell.RowGap = null; // default value
    rowCell.RowSpan = null; // default value
    rowCell.SetStyle(null); // default value
    rowCell.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var link = rowCell.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
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
    
    var expression = link.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetOrdersByEmployeeId.List.Current.Order.Code");
    expression.Width = ""; // default value
    var builtinEvent2 = link.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var orderDetail = order.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("OrderDetail");
    builtinEvent2.Destination = orderDetail;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromRight;
    var orderId = orderDetail.InputParameters.Named("OrderId");
    builtinEvent2.SetArgumentValue(orderId, "GetOrdersByEmployeeId.List.Current.Order.Id");
    var accountId = orderDetail.InputParameters.Named("AccountId");
    builtinEvent2.SetArgumentValue(accountId, null); // default value
    var opportunityId = orderDetail.InputParameters.Named("OpportunityId");
    builtinEvent2.SetArgumentValue(opportunityId, null); // default value
    
    var rowCell2 = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell2.ColumnGap = null; // default value
    rowCell2.ColumnSpan = null; // default value
    rowCell2.CustomStyle = null; // default value
    rowCell2.FlexBasis = null; // default value
    rowCell2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rowCell2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rowCell2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rowCell2.Gap = null;
    rowCell2.GridColumns = null; // default value
    rowCell2.GridRows = null; // default value
    rowCell2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rowCell2.RowGap = null; // default value
    rowCell2.RowSpan = null; // default value
    rowCell2.SetStyle(null); // default value
    rowCell2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var expression2 = rowCell2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetOrdersByEmployeeId.List.Current.Account.Company");
    expression2.Width = ""; // default value
    
    var rowCell3 = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell3.ColumnGap = null; // default value
    rowCell3.ColumnSpan = null; // default value
    rowCell3.CustomStyle = null; // default value
    rowCell3.FlexBasis = null; // default value
    rowCell3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rowCell3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rowCell3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rowCell3.Gap = null;
    rowCell3.GridColumns = null; // default value
    rowCell3.GridRows = null; // default value
    rowCell3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rowCell3.RowGap = null; // default value
    rowCell3.RowSpan = null; // default value
    rowCell3.SetStyle(null); // default value
    rowCell3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var container4 = rowCell3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container4.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Right;
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
    container4.SetStyle(null); // default value
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var expression3 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("FormatCurrency(GetOrdersByEmployeeId.List.Current.Order.TotalAmount, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
    expression3.Width = ""; // default value
    
    var rowCell4 = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell4.ColumnGap = null; // default value
    rowCell4.ColumnSpan = null; // default value
    rowCell4.CustomStyle = null; // default value
    rowCell4.FlexBasis = null; // default value
    rowCell4.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rowCell4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rowCell4.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rowCell4.Gap = null;
    rowCell4.GridColumns = null; // default value
    rowCell4.GridRows = null; // default value
    rowCell4.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rowCell4.RowGap = null; // default value
    rowCell4.RowSpan = null; // default value
    rowCell4.SetStyle(null); // default value
    rowCell4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var expression4 = rowCell4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("GetOrdersByEmployeeId.List.Current.User.Name");
    expression4.Width = ""; // default value
    
    var rowCell5 = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell5.ColumnGap = null; // default value
    rowCell5.ColumnSpan = null; // default value
    rowCell5.CustomStyle = "text-align: left;";
    rowCell5.FlexBasis = null; // default value
    rowCell5.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rowCell5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rowCell5.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rowCell5.Gap = null;
    rowCell5.GridColumns = null; // default value
    rowCell5.GridRows = null; // default value
    rowCell5.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rowCell5.RowGap = null; // default value
    rowCell5.RowSpan = null; // default value
    rowCell5.SetStyle(null); // default value
    rowCell5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var expression5 = rowCell5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("FormatDateTime(GetOrdersByEmployeeId.List.Current.Order.ExpectedClosedOn, \"d MMM yyyy\")");
    expression5.Width = ""; // default value
    
    var rowCell6 = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell6.ColumnGap = null; // default value
    rowCell6.ColumnSpan = null; // default value
    rowCell6.CustomStyle = null; // default value
    rowCell6.FlexBasis = null; // default value
    rowCell6.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rowCell6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rowCell6.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rowCell6.Gap = null;
    rowCell6.GridColumns = null; // default value
    rowCell6.GridRows = null; // default value
    rowCell6.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rowCell6.RowGap = null; // default value
    rowCell6.RowSpan = null; // default value
    rowCell6.SetStyle(null); // default value
    rowCell6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var expression6 = rowCell6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("FormatDateTime(GetOrdersByEmployeeId.List.Current.Order.RequiredOn, \"d MMM yyyy\")");
    expression6.Width = ""; // default value
    var builtinEvent3 = tableRecords.OnSort;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = onSort;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var clickedColumn = builtinEvent3.InputParameters.Named("ClickedColumn");
    clickedColumn.DataType = eSpace.TextType;
    clickedColumn.Description = null; // default value
    clickedColumn.IsMandatory = true; // default value
    clickedColumn.Name = "ClickedColumn";
    builtinEvent3.SetArgumentValue(sortBy, "ClickedColumn");
    
    var ifWidget2 = orderTable_Wrapper.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget2.Animate = false; // default value
    ifWidget2.SetCondition("GetOrdersByEmployeeId.IsDataFetched or GetOrdersByEmployeeId.List.Empty");
    ifWidget2.CustomStyle = null; // default value
    ifWidget2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container5 = ifWidget2.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container5.SetStyle("\"table-updating\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var container6 = orderTable_Wrapper.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container6.Animate = false; // default value
    container6.ColumnGap = null; // default value
    container6.ColumnSpan = null; // default value
    container6.CustomStyle = "margin-top: 20px;";
    container6.FlexBasis = null; // default value
    container6.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container6.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container6.Gap = null;
    container6.GridColumns = null; // default value
    container6.GridRows = null; // default value
    container6.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container6.RowGap = null; // default value
    container6.RowSpan = null; // default value
    container6.SetStyle(null); // default value
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var paginationInstance = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    paginationInstance.CustomStyle = null; // default value
    paginationInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var navigation = outSystemsUI.MobileFlows.Named("Navigation");
    var pagination = navigation.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Pagination");
    paginationInstance.SourceBlock = pagination;
    // delete default content in Previous placeholder
    var placeholderContentWidget8 = paginationInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Previous");
    placeholderContentWidget8.Widgets.ToList().ForEach(w => w.Delete());
    // delete default content in Next placeholder
    var placeholderContentWidget9 = paginationInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Next");
    placeholderContentWidget9.Widgets.ToList().ForEach(w => w.Delete());
    var totalCount = pagination.InputParameters.Named("TotalCount");
    paginationInstance.SetArgumentValue(totalCount, "GetOrdersByEmployeeId.Count");
    var startIndex2 = pagination.InputParameters.Named("StartIndex");
    paginationInstance.SetArgumentValue(startIndex2, "StartIndex");
    var showGoToPage = pagination.InputParameters.Named("ShowGoToPage");
    paginationInstance.SetArgumentValue(showGoToPage, null);
    var extendedClass4 = pagination.InputParameters.Named("ExtendedClass");
    paginationInstance.SetArgumentValue(extendedClass4, null); // default value
    var maxRecords2 = pagination.InputParameters.Named("MaxRecords");
    paginationInstance.SetArgumentValue(maxRecords2, "MaxRecords");
    var eventHandler4 = paginationInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnNavigate");
    eventHandler4.Handler = onNavigate;
    eventHandler4.SetArgumentValue(newStartIndex, "NewStartIndex");
    
    var icon = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon.ColumnSpan = null; // default value
    icon.CustomStyle = null; // default value
    icon.FlexBasis = null; // default value
    icon.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon.Icon = "angle-left";
    icon.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon.RowSpan = null; // default value
    icon.SetStyle("\"icon\""); // default value
    icon.SetVisible("True"); // default value
    icon.Weight = null; // default value
    
    var icon2 = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon2.ColumnSpan = null; // default value
    icon2.CustomStyle = null; // default value
    icon2.FlexBasis = null; // default value
    icon2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon2.Icon = "angle-right";
    icon2.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon2.RowSpan = null; // default value
    icon2.SetStyle("\"icon\""); // default value
    icon2.SetVisible("True"); // default value
    icon2.Weight = null; // default value
    
    var placeholderContentWidget10 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Footer");
    var footerInstance = placeholderContentWidget10.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    footerInstance.CustomStyle = null; // default value
    footerInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var footer = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Footer");
    footerInstance.SourceBlock = footer;
}

```