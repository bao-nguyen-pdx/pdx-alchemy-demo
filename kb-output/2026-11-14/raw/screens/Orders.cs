```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow order) => {
    
    /*** creating IMobileScreen 'Orders' ***/
    var orders = order.CreateScreen("Orders");
    orders.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = order.GetESpace();
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    orders.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    orders.Roles.Add(orderManager);
    orders.AnonymousAccess = false; // default value
    orders.CustomURL = false; // default value
    orders.Description = null; // default value
    orders.HorizontalPosition = 685;
    orders.Icon = null; // default value
    orders.IsDisabled = false; // default value
    orders.PageName = null; // default value
    orders.Public = false; // default value
    orders.Shareable = false; // default value
    orders.StyleSheet = null; // default value
    orders.SetTitle("\"Order Management - Orders\"");
    orders.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    orders.VerticalPosition = 1000; // default value
    
    var getMinMax = orders.CreateScreenAggregate(false, "GetMinMax");
    getMinMax.CacheInMinutes = null; // default value
    getMinMax.Description = null; // default value
    getMinMax.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getMinMax.SetMaxRecords("50"); // default value
    getMinMax.SetStartIndex(null); // default value
    getMinMax.Timeout = null; // default value
    var order3 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Order");
    var order2 = getMinMax.AsDatabaseAggregate.CreateSource(order3);
    var account2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Account");
    var account = getMinMax.AsDatabaseAggregate.CreateSource(account2);
    var orderStatus2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IStaticEntitySignature>().Named("OrderStatus");
    var orderStatus = getMinMax.AsDatabaseAggregate.CreateSource(orderStatus2);
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getMinMax.AsDatabaseAggregate.CreateSource(user2);
    var join = getMinMax.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = account;
    join.RightSource = order2;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("Order.AccountId = Account.Id");
    var join2 = getMinMax.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = user;
    join2.RightSource = order2;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join2.SetCondition("Order.CreatedBy = User.Id");
    var join3 = getMinMax.AsDatabaseAggregate.CreateJoin();
    join3.LeftSource = order2;
    join3.RightSource = orderStatus;
    join3.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join3.SetCondition("Order.OrderStatusId = OrderStatus.Id");
    getMinMax.AsDatabaseAggregate.CreateFilter("Account.Code like \"%\" + SearchKeyword + \"%\" or\nAccount.Company like \"%\" + SearchKeyword + \"%\" or\nOrder.Code like \"%\" + SearchKeyword + \"%\"");
    getMinMax.AsDatabaseAggregate.CreateFilter("(SearchByEmployee.Value = \"\") or (Order.CreatedBy = LongIntegerToIdentifier(TextToLongInteger(SearchByEmployee.Value)))");
    getMinMax.AsDatabaseAggregate.CreateFilter("Order.IsActive=True");
    getMinMax.AsDatabaseAggregate.CreateFilter("(Order.OrderStatusId = SearchByStatusId or SearchByStatusId = NullIdentifier() ) ");
    var totalAmountMax = getMinMax.AsDatabaseAggregate.CreateAggregatedAttribute("TotalAmountMax");
    totalAmountMax.SetAttribute("Order.TotalAmount");
    totalAmountMax.AggregationType = AggregationType.Max;
    var totalAmountMin = getMinMax.AsDatabaseAggregate.CreateAggregatedAttribute("TotalAmountMin");
    totalAmountMin.SetAttribute("Order.TotalAmount");
    totalAmountMin.AggregationType = AggregationType.Min;
    
    
    var getOrdersByEmployeeId = orders.CreateScreenAggregate(false, "GetOrdersByEmployeeId");
    getOrdersByEmployeeId.CacheInMinutes = null; // default value
    getOrdersByEmployeeId.Description = null; // default value
    getOrdersByEmployeeId.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.OnDemand;
    getOrdersByEmployeeId.SetMaxRecords("MaxRecords");
    getOrdersByEmployeeId.SetStartIndex("StartIndex");
    getOrdersByEmployeeId.Timeout = null; // default value
    var order4 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateSource(order3);
    var account3 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateSource(account2);
    var orderStatus3 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateSource(orderStatus2);
    var user3 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateSource(user2);
    var join4 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateJoin();
    join4.LeftSource = account3;
    join4.RightSource = order4;
    join4.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join4.SetCondition("Order.AccountId = Account.Id");
    var join5 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateJoin();
    join5.LeftSource = user3;
    join5.RightSource = order4;
    join5.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join5.SetCondition("Order.CreatedBy = User.Id");
    var join6 = getOrdersByEmployeeId.AsDatabaseAggregate.CreateJoin();
    join6.LeftSource = order4;
    join6.RightSource = orderStatus3;
    join6.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join6.SetCondition("Order.OrderStatusId = OrderStatus.Id");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateFilter("Account.Code like \"%\" + SearchKeyword + \"%\" or\nAccount.Company like \"%\" + SearchKeyword + \"%\" or\nOrder.Code like \"%\" + SearchKeyword + \"%\"");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateFilter("(SearchByEmployee.Value = \"\") or (SearchByEmployee.Value = NullIdentifier()) or (Order.CreatedBy = LongIntegerToIdentifier(TextToLongInteger(SearchByEmployee.Value)))");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateFilter("Order.IsActive=True");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateFilter("(Order.OrderStatusId = SearchByStatusId or SearchByStatusId = NullIdentifier() ) ");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateFilter("Order.TotalAmount >= Min and Order.TotalAmount <= Max");
    getOrdersByEmployeeId.AsDatabaseAggregate.CreateSort().SetAttribute("TableSort");
    
    
    var getEmployees = orders.CreateDataAction("GetEmployees");
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
    getEmployees2.Statement = "SELECT DISTINCT {User}.* \n\nFROM {User}\n\nWHERE {User}.[Id] \nIN (\n    SELECT {Order}.[CreatedBy] FROM {Order}\n)\n\nORDER BY {User}.[Name]";
    getEmployees2.Timeout = null; // default value
    getEmployees2.VerticalPosition = 2508;
    
    var user4 = getEmployees2.CreateOutput(user2, "User");
    startNode.Target = getEmployees2;
    
    var assignNode = getEmployees.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getEmployees2, 2291);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("UserList", "GetEmployees.List mapTo {  }");
    
    var endNode = getEmployees.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 3196;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 6857;
    assignNode.Target = endNode;
    getEmployees.OnAfterFetch.Destination = null; // default value
    
    var userList = getEmployees.CreateOutputParameter("UserList");
    userList.DataType = eSpace.GetOrCreateListType(user2);
    userList.Description = null; // default value
    
    var min = orders.CreateLocalVariable("Min");
    min.DataType = eSpace.IntegerType;
    min.SetDefaultValue(null); // default value
    min.Description = null; // default value
    
    var max = orders.CreateLocalVariable("Max");
    max.DataType = eSpace.IntegerType;
    max.SetDefaultValue(null); // default value
    max.Description = null; // default value
    
    var searchKeyword = orders.CreateLocalVariable("SearchKeyword");
    searchKeyword.DataType = eSpace.TextType;
    searchKeyword.SetDefaultValue(null); // default value
    searchKeyword.Description = null; // default value
    
    var searchByEmployee = orders.CreateLocalVariable("SearchByEmployee");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var dropdownOption = outSystemsUI.Structures.Named("DropdownOption");
    searchByEmployee.DataType = dropdownOption;
    searchByEmployee.Description = null; // default value
    
    var searchByStatusId = orders.CreateLocalVariable("SearchByStatusId");
    searchByStatusId.DataType = orderStatus2.IdentifierType;
    searchByStatusId.SetDefaultValue(null); // default value
    searchByStatusId.Description = null; // default value
    
    var startIndex = orders.CreateLocalVariable("StartIndex");
    startIndex.DataType = eSpace.IntegerType;
    startIndex.SetDefaultValue("0");
    startIndex.Description = null; // default value
    
    var maxRecords = orders.CreateLocalVariable("MaxRecords");
    maxRecords.DataType = eSpace.IntegerType;
    maxRecords.SetDefaultValue("20");
    maxRecords.Description = null; // default value
    
    var maxRange = orders.CreateLocalVariable("MaxRange");
    maxRange.DataType = eSpace.IntegerType;
    maxRange.SetDefaultValue("20000");
    maxRange.Description = null; // default value
    
    var minRange = orders.CreateLocalVariable("MinRange");
    minRange.DataType = eSpace.IntegerType;
    minRange.SetDefaultValue("0");
    minRange.Description = null; // default value
    
    var tableSort = orders.CreateLocalVariable("TableSort");
    tableSort.DataType = eSpace.TextType;
    tableSort.SetDefaultValue(null); // default value
    tableSort.Description = null; // default value
    
    var isOrdersDataFetched = orders.CreateLocalVariable("IsOrdersDataFetched");
    isOrdersDataFetched.DataType = eSpace.BooleanType;
    isOrdersDataFetched.SetDefaultValue("False");
    isOrdersDataFetched.Description = null; // default value
    orders.OnDestroy.Destination = null; // default value
    orders.OnInitialize.Destination = null; // default value
    orders.OnReady.Destination = null; // default value
    orders.OnRender.Destination = null; // default value
    
    var dropdownSearchOnSelect = orders.CreateScreenAction("DropdownSearchOnSelect");
    dropdownSearchOnSelect.Description = null; // default value
    
    var selectedOptionList = dropdownSearchOnSelect.CreateInputParameter("SelectedOptionList");
    selectedOptionList.DataType = eSpace.GetOrCreateListType(dropdownOption);
    selectedOptionList.Description = null; // default value
    selectedOptionList.IsMandatory = true; // default value
    
    var startNode2 = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 2971;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 914;
    
    var assignNode2 = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode2, 1372);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    
    var assignment2 = assignNode2.CreateAssignment("SearchByEmployee", "SelectedOptionList.Current");
    
    var refreshOrderTable = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("RefreshOrderTable").ConnectedBelow(assignNode2, 1371);
    refreshOrderTable.IsDisabled = false; // default value
    
    var refreshOrderTable2 = orders.CreateScreenAction("RefreshOrderTable");
    refreshOrderTable2.Description = null; // default value
    
    var resetFilters = refreshOrderTable2.CreateInputParameter("ResetFilters");
    resetFilters.DataType = eSpace.BooleanType;
    resetFilters.Description = null; // default value
    resetFilters.IsMandatory = true; // default value
    
    var resetPagination = refreshOrderTable2.CreateInputParameter("ResetPagination");
    resetPagination.DataType = eSpace.BooleanType;
    resetPagination.Description = null; // default value
    resetPagination.IsMandatory = true; // default value
    
    var startNode3 = refreshOrderTable2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.IsDisabled = false; // default value
    
    var ifNode = refreshOrderTable2.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode.SetCondition("ResetFilters");
    ifNode.HorizontalPosition = 3192;
    ifNode.IsDisabled = false; // default value
    ifNode.Label = null; // default value
    ifNode.VerticalPosition = 2287;
    
    var getCeilingandFloor = refreshOrderTable2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetCeilingandFloor").ToTheRightOf(ifNode, 1610);
    var getCeilingandFloor2 = eSpace.ServerActions.Named("GetCeilingandFloor");
    getCeilingandFloor.Action = getCeilingandFloor2;
    getCeilingandFloor.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getCeilingandFloor.IsDisabled = false; // default value
    getCeilingandFloor.ServerRequestTimeout = null; // default value
    var totalAmountCacheMin = getCeilingandFloor2.InputParameters.Named("TotalAmountCacheMin");
    getCeilingandFloor.SetArgumentValue(totalAmountCacheMin, "GetMinMax.List.Current.TotalAmountMin");
    var totalAmountCacheMax = getCeilingandFloor2.InputParameters.Named("TotalAmountCacheMax");
    getCeilingandFloor.SetArgumentValue(totalAmountCacheMax, "GetMinMax.List.Current.TotalAmountMax");
    
    var assignNode3 = refreshOrderTable2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode3.HorizontalPosition = 1368;
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Set Pagination";
    assignNode3.VerticalPosition = 3876;
    
    var assignment3 = assignNode3.CreateAssignment("StartIndex", "0");
    
    var assignment4 = assignNode3.CreateAssignment("MaxRecords", "10");
    
    var ifNode2 = refreshOrderTable2.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().Below(ifNode, 1602);
    ifNode2.SetCondition("ResetPagination");
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = null; // default value
    ifNode2.TrueTarget = assignNode3;
    
    var assignNode4 = refreshOrderTable2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getCeilingandFloor, 1602);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Set Filters";
    assignNode4.Target = ifNode2;
    
    var assignment5 = assignNode4.CreateAssignment("SearchKeyword", "\"\"");
    
    var assignment6 = assignNode4.CreateAssignment("SearchByEmployee.Value", "\"\"");
    
    var assignment7 = assignNode4.CreateAssignment("SearchByStatusId", "NullIdentifier()");
    
    var assignment8 = assignNode4.CreateAssignment("Min", "GetCeilingandFloor.Min");
    
    var assignment9 = assignNode4.CreateAssignment("Max", "GetCeilingandFloor.Max");
    
    var refreshDataNode = refreshOrderTable2.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().Below(ifNode2, 1829);
    refreshDataNode.DataSource = getOrdersByEmployeeId;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords("MaxRecords");
    refreshDataNode.SetStartIndex("StartIndex");
    
    var endNode2 = refreshOrderTable2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3199;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 7091;
    refreshOrderTable.Action = refreshOrderTable2;
    
    var endNode3 = dropdownSearchOnSelect.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshOrderTable, 1600);
    endNode3.IsDisabled = false; // default value
    refreshOrderTable.SetArgumentValue(resetFilters, "False");
    refreshOrderTable.SetArgumentValue(resetPagination, "True");
    startNode3.Target = ifNode;
    ifNode.FalseTarget = ifNode2;
    ifNode.TrueTarget = getCeilingandFloor;
    assignNode3.Target = refreshDataNode;
    ifNode2.FalseTarget = refreshDataNode;
    refreshDataNode.Target = endNode2;
    
    var getAccountsWithOrdersOnAfterFetch = orders.CreateScreenAction("GetAccountsWithOrdersOnAfterFetch");
    getAccountsWithOrdersOnAfterFetch.Description = null; // default value
    
    var startNode4 = getAccountsWithOrdersOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.IsDisabled = false; // default value
    
    var getCeilingandFloor3 = getAccountsWithOrdersOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("GetCeilingandFloor");
    getCeilingandFloor3.Action = getCeilingandFloor2;
    getCeilingandFloor3.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    getCeilingandFloor3.HorizontalPosition = 3192;
    getCeilingandFloor3.IsDisabled = false; // default value
    getCeilingandFloor3.ServerRequestTimeout = null; // default value
    getCeilingandFloor3.VerticalPosition = 2971;
    getCeilingandFloor3.SetArgumentValue(totalAmountCacheMax, "GetMinMax.List.Current.TotalAmountMax");
    getCeilingandFloor3.SetArgumentValue(totalAmountCacheMin, "GetMinMax.List.Current.TotalAmountMin");
    startNode4.Target = getCeilingandFloor3;
    
    var assignNode5 = getAccountsWithOrdersOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(getCeilingandFloor3, 1828);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment10 = assignNode5.CreateAssignment("Min", "GetCeilingandFloor.Min");
    
    var assignment11 = assignNode5.CreateAssignment("Max", "GetCeilingandFloor.Max");
    
    var assignment12 = assignNode5.CreateAssignment("MinRange", "GetCeilingandFloor.Min");
    
    var assignment13 = assignNode5.CreateAssignment("MaxRange", "GetCeilingandFloor.Max");
    
    var refreshDataNode2 = getAccountsWithOrdersOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode5, 1829);
    refreshDataNode2.DataSource = getOrdersByEmployeeId;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords("MaxRecords");
    refreshDataNode2.SetStartIndex("StartIndex");
    
    var endNode4 = getAccountsWithOrdersOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode4.HorizontalPosition = 3199;
    endNode4.IsDisabled = false; // default value
    endNode4.VerticalPosition = 8457;
    refreshDataNode2.Target = endNode4;
    
    var getOrdersByEmployeeIdOnAfterFetch = orders.CreateScreenAction("GetOrdersByEmployeeIdOnAfterFetch");
    getOrdersByEmployeeIdOnAfterFetch.Description = null; // default value
    
    var startNode5 = getOrdersByEmployeeIdOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.IsDisabled = false; // default value
    
    var assignNode6 = getOrdersByEmployeeIdOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode6.HorizontalPosition = 3192;
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    assignNode6.VerticalPosition = 2736;
    
    var assignment14 = assignNode6.CreateAssignment("IsOrdersDataFetched", "True");
    startNode5.Target = assignNode6;
    
    var endNode5 = getOrdersByEmployeeIdOnAfterFetch.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode5.HorizontalPosition = 3196;
    endNode5.IsDisabled = false; // default value
    endNode5.VerticalPosition = 5120;
    assignNode6.Target = endNode5;
    
    var orderTableOnSort = orders.CreateScreenAction("OrderTableOnSort");
    orderTableOnSort.Description = null; // default value
    
    var sortBy = orderTableOnSort.CreateInputParameter("SortBy");
    sortBy.DataType = eSpace.TextType;
    sortBy.Description = "Table column by which the aggregate will be sorted.";
    sortBy.IsMandatory = true; // default value
    
    var startNode6 = orderTableOnSort.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.IsDisabled = false; // default value
    
    var ifNode3 = orderTableOnSort.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode6, 1330);
    ifNode3.SetCondition("TableSort = SortBy");
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = null; // default value
    
    var assignNode7 = orderTableOnSort.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode3, 1330);
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = "TableSort = SortBy";
    
    var assignment15 = assignNode7.CreateAssignment("TableSort", "SortBy");
    ifNode3.FalseTarget = assignNode7;
    
    var assignNode8 = orderTableOnSort.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode3, 1772);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = "TableSort += DESC";
    
    var assignment16 = assignNode8.CreateAssignment("TableSort", "SortBy + \" DESC\"");
    ifNode3.TrueTarget = assignNode8;
    
    var assignNode9 = orderTableOnSort.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(assignNode7, 1330);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = "StartIndex = 0";
    
    var assignment17 = assignNode9.CreateAssignment("StartIndex", "0");
    assignNode8.Target = assignNode9;
    
    var refreshDataNode3 = orderTableOnSort.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode9, 1330);
    refreshDataNode3.DataSource = getOrdersByEmployeeId;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords("MaxRecords");
    refreshDataNode3.SetStartIndex("StartIndex");
    
    var endNode6 = orderTableOnSort.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode3, 1330);
    endNode6.IsDisabled = false; // default value
    
    var commentNode = orderTableOnSort.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode.HorizontalPosition = 5856;
    commentNode.IsDisabled = false; // default value
    commentNode.IsReminder = false; // default value
    commentNode.Text = "This brings the pagination back to the first \n page everytime the aggregate is sorted.";
    commentNode.VerticalPosition = 3460;
    commentNode.CreateConnector(assignNode9);
    
    var paginationOnNavigate = orders.CreateScreenAction("PaginationOnNavigate");
    paginationOnNavigate.Description = null; // default value
    
    var newStartIndex = paginationOnNavigate.CreateInputParameter("NewStartIndex");
    newStartIndex.DataType = eSpace.IntegerType;
    newStartIndex.Description = null; // default value
    newStartIndex.IsMandatory = true; // default value
    
    var startNode7 = paginationOnNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.IsDisabled = false; // default value
    
    var assignNode10 = paginationOnNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode10.HorizontalPosition = 3192;
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = null; // default value
    assignNode10.VerticalPosition = 2280;
    
    var assignment18 = assignNode10.CreateAssignment("StartIndex", "NewStartIndex");
    startNode7.Target = assignNode10;
    
    var refreshDataNode4 = paginationOnNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode10, 1368);
    refreshDataNode4.DataSource = getOrdersByEmployeeId;
    refreshDataNode4.IsDisabled = false; // default value
    refreshDataNode4.SetMaxRecords("MaxRecords");
    refreshDataNode4.SetStartIndex("StartIndex");
    
    var endNode7 = paginationOnNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode7.HorizontalPosition = 3196;
    endNode7.IsDisabled = false; // default value
    endNode7.VerticalPosition = 5120;
    refreshDataNode4.Target = endNode7;
    
    var rangeSliderIntervalOnChange = orders.CreateScreenAction("RangeSliderIntervalOnChange");
    rangeSliderIntervalOnChange.Description = null; // default value
    
    var intervalStart = rangeSliderIntervalOnChange.CreateInputParameter("IntervalStart");
    intervalStart.DataType = eSpace.IntegerType;
    intervalStart.Description = null; // default value
    intervalStart.IsMandatory = true; // default value
    
    var intervalEnd = rangeSliderIntervalOnChange.CreateInputParameter("IntervalEnd");
    intervalEnd.DataType = eSpace.IntegerType;
    intervalEnd.Description = null; // default value
    intervalEnd.IsMandatory = true; // default value
    
    var startNode8 = rangeSliderIntervalOnChange.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.IsDisabled = false; // default value
    
    var assignNode11 = rangeSliderIntervalOnChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode11.HorizontalPosition = 3192;
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = "Set Vars";
    assignNode11.VerticalPosition = 2280;
    
    var assignment19 = assignNode11.CreateAssignment("Min", "IntervalStart");
    
    var assignment20 = assignNode11.CreateAssignment("Max", "IntervalEnd");
    startNode8.Target = assignNode11;
    
    var refreshDataNode5 = rangeSliderIntervalOnChange.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode11, 1608);
    refreshDataNode5.DataSource = getOrdersByEmployeeId;
    refreshDataNode5.IsDisabled = false; // default value
    refreshDataNode5.SetMaxRecords("MaxRecords");
    refreshDataNode5.SetStartIndex("StartIndex");
    
    var endNode8 = rangeSliderIntervalOnChange.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode8.HorizontalPosition = 3199;
    endNode8.IsDisabled = false; // default value
    endNode8.VerticalPosition = 5257;
    refreshDataNode5.Target = endNode8;
    
    var tabsOnTabChange = orders.CreateScreenAction("TabsOnTabChange");
    tabsOnTabChange.Description = null; // default value
    
    var activeTab = tabsOnTabChange.CreateInputParameter("ActiveTab");
    activeTab.DataType = eSpace.IntegerType;
    activeTab.Description = null; // default value
    activeTab.IsMandatory = true; // default value
    
    var startNode9 = tabsOnTabChange.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.IsDisabled = false; // default value
    
    var switchNode = tabsOnTabChange.CreateNode<OutSystems.Model.Logic.Nodes.ISwitchNode>();
    switchNode.HorizontalPosition = 3192;
    switchNode.IsDisabled = false; // default value
    switchNode.Label = null; // default value
    switchNode.VerticalPosition = 2280;
    
    var switchCondition = switchNode.CreateCondition();
    switchCondition.Label = null; // default value
    switchCondition.SetValue("ActiveTab = 0");
    
    var switchCondition2 = switchNode.CreateCondition();
    switchCondition2.Label = null; // default value
    switchCondition2.SetValue("ActiveTab = 1");
    
    var switchCondition3 = switchNode.CreateCondition();
    switchCondition3.Label = null; // default value
    switchCondition3.SetValue("ActiveTab = 2");
    
    var switchCondition4 = switchNode.CreateCondition();
    switchCondition4.Label = null; // default value
    switchCondition4.SetValue("ActiveTab = 3");
    
    var switchCondition5 = switchNode.CreateCondition();
    switchCondition5.Label = null; // default value
    switchCondition5.Target = null; // default value
    switchCondition5.SetValue("ActiveTab = 3");
    
    var switchCondition6 = switchNode.CreateCondition();
    switchCondition6.Label = null; // default value
    switchCondition6.Target = null; // default value
    switchCondition6.SetValue("ActiveTab = 5");
    startNode9.Target = switchNode;
    
    var refreshOrderTable3 = tabsOnTabChange.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("RefreshOrderTable");
    refreshOrderTable3.Action = refreshOrderTable2;
    refreshOrderTable3.HorizontalPosition = 3199;
    refreshOrderTable3.IsDisabled = false; // default value
    refreshOrderTable3.VerticalPosition = 6854;
    refreshOrderTable3.SetArgumentValue(resetPagination, "True");
    refreshOrderTable3.SetArgumentValue(resetFilters, "False");
    switchNode.OtherwiseTarget = refreshOrderTable3;
    
    var assignNode12 = tabsOnTabChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode12.HorizontalPosition = 3886;
    assignNode12.IsDisabled = false; // default value
    assignNode12.Label = "All";
    assignNode12.Target = refreshOrderTable3;
    assignNode12.VerticalPosition = 4570;
    
    var assignment21 = assignNode12.CreateAssignment("SearchByStatusId", "NullIdentifier()");
    switchCondition.Target = assignNode12;
    
    var assignNode13 = tabsOnTabChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode13.HorizontalPosition = 4801;
    assignNode13.IsDisabled = false; // default value
    assignNode13.Label = "Draft";
    assignNode13.Target = refreshOrderTable3;
    assignNode13.VerticalPosition = 4570;
    
    var assignment22 = assignNode13.CreateAssignment("SearchByStatusId", "Entities.OrderStatus.Draft");
    switchCondition2.Target = assignNode13;
    
    var assignNode14 = tabsOnTabChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode14.HorizontalPosition = 5717;
    assignNode14.IsDisabled = false; // default value
    assignNode14.Label = "Submitted";
    assignNode14.Target = refreshOrderTable3;
    assignNode14.VerticalPosition = 4570;
    
    var assignment23 = assignNode14.CreateAssignment("SearchByStatusId", "Entities.OrderStatus.Submitted");
    switchCondition3.Target = assignNode14;
    
    var assignNode15 = tabsOnTabChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode15.HorizontalPosition = 6860;
    assignNode15.IsDisabled = false; // default value
    assignNode15.Label = "Closed";
    assignNode15.Target = refreshOrderTable3;
    assignNode15.VerticalPosition = 4570;
    
    var assignment24 = assignNode15.CreateAssignment("SearchByStatusId", "Entities.OrderStatus.Closed");
    switchCondition4.Target = assignNode15;
    
    var endNode9 = tabsOnTabChange.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshOrderTable3, 1379);
    endNode9.IsDisabled = false; // default value
    
    var layoutTopHeaderInstance = orders.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopHeaderInstance.CustomStyle = null; // default value
    layoutTopHeaderInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var layoutTopHeader = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopHeader");
    layoutTopHeaderInstance.SourceBlock = layoutTopHeader;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var hasFixedHeader = layoutTopHeader.InputParameters.Named("HasFixedHeader");
    layoutTopHeaderInstance.SetArgumentValue(hasFixedHeader, null);
    var hasHeaderImage = layoutTopHeader.InputParameters.Named("HasHeaderImage");
    layoutTopHeaderInstance.SetArgumentValue(hasHeaderImage, null);
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeMenu = menu.InputParameters.Named("ActiveMenu");
    menuInstance.SetArgumentValue(activeMenu, "Entities.MenuItem.Orders");
    
    var placeholderContentWidget2 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Orders";
    
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
    button.SetStyle("\"btn btn-large \"");
    button.SetVisible("True"); // default value
    button.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button.Width = ""; // default value
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button.Widgets.First();
    textWidget2.CustomStyle = null; // default value
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "New Order";
    var builtinEvent = button.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var orderDetail = order.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("OrderDetail");
    builtinEvent.Destination = orderDetail;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromRight;
    var opportunityId = orderDetail.InputParameters.Named("OpportunityId");
    builtinEvent.SetArgumentValue(opportunityId, null); // default value
    var orderId = orderDetail.InputParameters.Named("OrderId");
    builtinEvent.SetArgumentValue(orderId, "NullIdentifier()");
    var accountId = orderDetail.InputParameters.Named("AccountId");
    builtinEvent.SetArgumentValue(accountId, null); // default value
    
    var placeholderContentWidget4 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Controls");
    var filters_Wrapper = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Filters_Wrapper");
    filters_Wrapper.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    filters_Wrapper.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    filters_Wrapper.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    filters_Wrapper.Animate = false; // default value
    filters_Wrapper.ColumnGap = null; // default value
    filters_Wrapper.ColumnSpan = null; // default value
    filters_Wrapper.CustomStyle = "text-align: center;";
    filters_Wrapper.FlexBasis = null; // default value
    filters_Wrapper.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    filters_Wrapper.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    filters_Wrapper.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    filters_Wrapper.Gap = null;
    filters_Wrapper.GridColumns = null; // default value
    filters_Wrapper.GridRows = null; // default value
    filters_Wrapper.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    filters_Wrapper.RowGap = null; // default value
    filters_Wrapper.RowSpan = null; // default value
    filters_Wrapper.SetStyle(null); // default value
    filters_Wrapper.SetVisible("True"); // default value
    filters_Wrapper.VisibleWasExpression = false; // default value
    filters_Wrapper.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    filters_Wrapper.Width = "(fill parent)"; // default value
    
    var columns3Instance = filters_Wrapper.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns3Instance.CustomStyle = null; // default value
    columns3Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var adaptive = outSystemsUI.MobileFlows.Named("Adaptive");
    var columns3 = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Columns3");
    columns3Instance.SourceBlock = columns3;
    var gutterSize = columns3.InputParameters.Named("GutterSize");
    columns3Instance.SetArgumentValue(gutterSize, "Entities.GutterSize.Large");
    var phoneBehavior = columns3.InputParameters.Named("PhoneBehavior");
    columns3Instance.SetArgumentValue(phoneBehavior, "Entities.BreakColumns.All");
    var tabletBehavior = columns3.InputParameters.Named("TabletBehavior");
    columns3Instance.SetArgumentValue(tabletBehavior, "Entities.BreakColumns.Last");
    var extendedClass = columns3.InputParameters.Named("ExtendedClass");
    columns3Instance.SetArgumentValue(extendedClass, null); // default value
    
    var placeholderContentWidget5 = columns3Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var dEPRECATED_SearchInstance = placeholderContentWidget5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_SearchInstance.CustomStyle = null; // default value
    dEPRECATED_SearchInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var interaction = outSystemsUI.MobileFlows.Named("Interaction");
    var dEPRECATED_Search = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DEPRECATED_Search");
    dEPRECATED_SearchInstance.SourceBlock = dEPRECATED_Search;
    // delete default content in Input placeholder
    var placeholderContentWidget6 = dEPRECATED_SearchInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Input");
    placeholderContentWidget6.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass2 = dEPRECATED_Search.InputParameters.Named("ExtendedClass");
    dEPRECATED_SearchInstance.SetArgumentValue(extendedClass2, null); // default value
    var eventHandler = dEPRECATED_SearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnCollapse");
    eventHandler.Handler = null; // default value
    
    var searchInput = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("SearchInput");
    searchInput.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    searchInput.ColumnSpan = null; // default value
    searchInput.CustomStyle = "padding: 5px 16px 5px 40px;";
    searchInput.SetEnabled("True");
    searchInput.FlexBasis = null; // default value
    searchInput.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    searchInput.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Search;
    searchInput.SetMandatory("False");
    searchInput.MaxLength = 100;
    searchInput.SetPrompt("\"Type order number or company name\"");
    searchInput.RowSpan = null; // default value
    searchInput.SetStyle("\"form-control search-input\"");
    searchInput.SetVariable("SearchKeyword");
    searchInput.Width = "(fill parent)"; // default value
    var builtinEvent2 = searchInput.OnChange;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = refreshOrderTable2;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent2.SetArgumentValue(resetFilters, "False");
    builtinEvent2.SetArgumentValue(resetPagination, "True");
    
    var placeholderContentWidget7 = columns3Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container.Animate = false; // default value
    container.ColumnGap = null; // default value
    container.ColumnSpan = null; // default value
    container.CustomStyle = "text-align: left;";
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
    container.SetStyle(null); // default value
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "(fill parent)"; // default value
    
    var dropdownSearchInstance = container.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dropdownSearchInstance.CustomStyle = null; // default value
    dropdownSearchInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dropdownSearch = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DropdownSearch");
    dropdownSearchInstance.SourceBlock = dropdownSearch;
    var startingSelection = dropdownSearch.InputParameters.Named("StartingSelection");
    dropdownSearchInstance.SetArgumentValue(startingSelection, null); // default value
    var optionalConfigs = dropdownSearch.InputParameters.Named("OptionalConfigs");
    dropdownSearchInstance.SetArgumentValue(optionalConfigs, "{ SearchPrompt: \"Search by Employee\" }");
    var extendedClass3 = dropdownSearch.InputParameters.Named("ExtendedClass");
    dropdownSearchInstance.SetArgumentValue(extendedClass3, "\"search-dropdown\"");
    var optionsList = dropdownSearch.InputParameters.Named("OptionsList");
    dropdownSearchInstance.SetArgumentValue(optionsList, "GetEmployees.UserList mapTo { Value: Id, Label: Name }");
    var prompt = dropdownSearch.InputParameters.Named("Prompt");
    dropdownSearchInstance.SetArgumentValue(prompt, "\"All Employees\"");
    var eventHandler2 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnChanged");
    eventHandler2.Handler = dropdownSearchOnSelect;
    eventHandler2.SetArgumentValue(selectedOptionList, "SelectedOptionList");
    var eventHandler3 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler3.Handler = null; // default value
    
    var placeholderContentWidget8 = columns3Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column3");
    var container2 = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container2.Animate = false; // default value
    container2.ColumnGap = null; // default value
    container2.ColumnSpan = null; // default value
    container2.CustomStyle = "text-align: left;";
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
    container2.SetStyle(null); // default value
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var container3 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container3.Animate = false; // default value
    container3.ColumnGap = null; // default value
    container3.ColumnSpan = null; // default value
    container3.CustomStyle = "font-size: 10px; height: 20px; margin-top: -10px; padding: 0px 0px 20px 0px; text-align: left;";
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
    container3.SetStyle("\"slide-interval-labels\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var alignCenterInstance = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    alignCenterInstance.CustomStyle = null; // default value
    alignCenterInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var utilities = outSystemsUI.MobileFlows.Named("Utilities");
    var alignCenter = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("AlignCenter");
    alignCenterInstance.SourceBlock = alignCenter;
    var extendedClass4 = alignCenter.InputParameters.Named("ExtendedClass");
    alignCenterInstance.SetArgumentValue(extendedClass4, null); // default value
    var isHorizontal = alignCenter.InputParameters.Named("IsHorizontal");
    alignCenterInstance.SetArgumentValue(isHorizontal, null);
    
    var placeholderContentWidget9 = alignCenterInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var container4 = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle(null); // default value
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "";
    
    var textWidget3 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Amount";
    
    var container5 = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container5.Animate = false; // default value
    container5.ColumnGap = null; // default value
    container5.ColumnSpan = null; // default value
    container5.CustomStyle = "margin-right: 5px;";
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
    container5.SetStyle(null); // default value
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "";
    
    var expression = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = "0";
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("FormatCurrency(Min, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
    expression.Width = ""; // default value
    
    var container6 = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container6.Animate = false; // default value
    container6.ColumnGap = null; // default value
    container6.ColumnSpan = null; // default value
    container6.CustomStyle = null; // default value
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
    container6.Width = "3 col";
    
    var separatorInstance = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    separatorInstance.CustomStyle = null; // default value
    separatorInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var separator = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Separator");
    separatorInstance.SourceBlock = separator;
    var extendedClass5 = separator.InputParameters.Named("ExtendedClass");
    separatorInstance.SetArgumentValue(extendedClass5, null); // default value
    var color = separator.InputParameters.Named("Color");
    separatorInstance.SetArgumentValue(color, null);
    var space = separator.InputParameters.Named("Space");
    separatorInstance.SetArgumentValue(space, null);
    var isVertical = separator.InputParameters.Named("IsVertical");
    separatorInstance.SetArgumentValue(isVertical, null);
    
    var container7 = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container7.Animate = false; // default value
    container7.ColumnGap = null; // default value
    container7.ColumnSpan = null; // default value
    container7.CustomStyle = "margin-right: 5px;";
    container7.FlexBasis = null; // default value
    container7.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container7.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container7.Gap = null;
    container7.GridColumns = null; // default value
    container7.GridRows = null; // default value
    container7.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container7.RowGap = null; // default value
    container7.RowSpan = null; // default value
    container7.SetStyle(null); // default value
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "";
    
    var expression2 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = "20000";
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("FormatCurrency(Max,Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
    expression2.Width = ""; // default value
    
    var container8 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container8.Animate = false; // default value
    container8.ColumnGap = null; // default value
    container8.ColumnSpan = null; // default value
    container8.CustomStyle = "height: 20px; margin-top: -10px;";
    container8.FlexBasis = null; // default value
    container8.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container8.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container8.Gap = null;
    container8.GridColumns = null; // default value
    container8.GridRows = null; // default value
    container8.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container8.RowGap = null; // default value
    container8.RowSpan = null; // default value
    container8.SetStyle(null); // default value
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var iF_AmountIsFetched = container8.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IF_AmountIsFetched");
    iF_AmountIsFetched.Animate = false; // default value
    iF_AmountIsFetched.SetCondition("IsOrdersDataFetched");
    iF_AmountIsFetched.CustomStyle = null; // default value
    iF_AmountIsFetched.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var rangeSliderIntervalInstance = iF_AmountIsFetched.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    rangeSliderIntervalInstance.CustomStyle = null; // default value
    rangeSliderIntervalInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var rangeSliderInterval = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("RangeSliderInterval");
    rangeSliderIntervalInstance.SourceBlock = rangeSliderInterval;
    var maxValue = rangeSliderInterval.InputParameters.Named("MaxValue");
    rangeSliderIntervalInstance.SetArgumentValue(maxValue, "MaxRange");
    var orientation = rangeSliderInterval.InputParameters.Named("Orientation");
    rangeSliderIntervalInstance.SetArgumentValue(orientation, null);
    var startingValueTo = rangeSliderInterval.InputParameters.Named("StartingValueTo");
    rangeSliderIntervalInstance.SetArgumentValue(startingValueTo, "MaxRange");
    var optionalConfigs2 = rangeSliderInterval.InputParameters.Named("OptionalConfigs");
    rangeSliderIntervalInstance.SetArgumentValue(optionalConfigs2, "{ Step: 100, ShowTickMarks: False, TickMarksInterval: 1 }");
    var extendedClass6 = rangeSliderInterval.InputParameters.Named("ExtendedClass");
    rangeSliderIntervalInstance.SetArgumentValue(extendedClass6, "\"amount-slider\"");
    var minValue = rangeSliderInterval.InputParameters.Named("MinValue");
    rangeSliderIntervalInstance.SetArgumentValue(minValue, "MinRange");
    var startingValueFrom = rangeSliderInterval.InputParameters.Named("StartingValueFrom");
    rangeSliderIntervalInstance.SetArgumentValue(startingValueFrom, "MinRange");
    var size = rangeSliderInterval.InputParameters.Named("Size");
    rangeSliderIntervalInstance.SetArgumentValue(size, null);
    var eventHandler4 = rangeSliderIntervalInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler4.Handler = null; // default value
    var eventHandler5 = rangeSliderIntervalInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnValueChange");
    eventHandler5.Handler = rangeSliderIntervalOnChange;
    eventHandler5.SetArgumentValue(intervalEnd, "IntervalEnd");
    eventHandler5.SetArgumentValue(intervalStart, "IntervalStart");
    
    var rangeSliderIntervalInstance2 = iF_AmountIsFetched.FalseBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    rangeSliderIntervalInstance2.CustomStyle = null; // default value
    rangeSliderIntervalInstance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    rangeSliderIntervalInstance2.SourceBlock = rangeSliderInterval;
    rangeSliderIntervalInstance2.SetArgumentValue(startingValueTo, "MaxRange");
    rangeSliderIntervalInstance2.SetArgumentValue(extendedClass6, "\"amount-slider\"");
    rangeSliderIntervalInstance2.SetArgumentValue(maxValue, "MaxRange");
    rangeSliderIntervalInstance2.SetArgumentValue(orientation, null);
    rangeSliderIntervalInstance2.SetArgumentValue(size, null);
    rangeSliderIntervalInstance2.SetArgumentValue(startingValueFrom, "MinRange");
    rangeSliderIntervalInstance2.SetArgumentValue(minValue, "MinRange");
    rangeSliderIntervalInstance2.SetArgumentValue(optionalConfigs2, "{ Step: 100, ShowTickMarks: False, TickMarksInterval: 1, IsDisabled: True }");
    var eventHandler6 = rangeSliderIntervalInstance2.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnValueChange");
    eventHandler6.Handler = rangeSliderIntervalOnChange;
    eventHandler6.SetArgumentValue(intervalEnd, "IntervalEnd");
    eventHandler6.SetArgumentValue(intervalStart, "IntervalStart");
    var eventHandler7 = rangeSliderIntervalInstance2.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler7.Handler = null; // default value
    
    var extendedProperty = container2.CreateExtendedProperty();
    extendedProperty.Property = "style";
    extendedProperty.SetValue("\"position:relative;\"");
    
    var placeholderContentWidget10 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var dEPRECATED_TabsV2Instance = placeholderContentWidget10.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_TabsV2Instance.CustomStyle = null; // default value
    dEPRECATED_TabsV2Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var navigation = outSystemsUI.MobileFlows.Named("Navigation");
    var dEPRECATED_TabsV2 = navigation.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DEPRECATED_TabsV2");
    dEPRECATED_TabsV2Instance.SourceBlock = dEPRECATED_TabsV2;
    // delete default content in Header placeholder
    var placeholderContentWidget11 = dEPRECATED_TabsV2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget11.Widgets.ToList().ForEach(w => w.Delete());
    // delete default content in Content placeholder
    var placeholderContentWidget12 = dEPRECATED_TabsV2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    placeholderContentWidget12.Widgets.ToList().ForEach(w => w.Delete());
    var isJustified = dEPRECATED_TabsV2.InputParameters.Named("IsJustified");
    dEPRECATED_TabsV2Instance.SetArgumentValue(isJustified, null); // default value
    var extendedClass7 = dEPRECATED_TabsV2.InputParameters.Named("ExtendedClass");
    dEPRECATED_TabsV2Instance.SetArgumentValue(extendedClass7, null); // default value
    var isRight = dEPRECATED_TabsV2.InputParameters.Named("IsRight");
    dEPRECATED_TabsV2Instance.SetArgumentValue(isRight, null);
    var startingTab = dEPRECATED_TabsV2.InputParameters.Named("StartingTab");
    dEPRECATED_TabsV2Instance.SetArgumentValue(startingTab, null);
    var isVertical2 = dEPRECATED_TabsV2.InputParameters.Named("IsVertical");
    dEPRECATED_TabsV2Instance.SetArgumentValue(isVertical2, null);
    var eventHandler8 = dEPRECATED_TabsV2Instance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnTabChange");
    eventHandler8.Handler = tabsOnTabChange;
    eventHandler8.SetArgumentValue(activeTab, "ActiveTab");
    
    var dEPRECATED_TabsHeaderItemInstance = placeholderContentWidget11.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_TabsHeaderItemInstance.CustomStyle = null; // default value
    dEPRECATED_TabsHeaderItemInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dEPRECATED_TabsHeaderItem = navigation.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DEPRECATED_TabsHeaderItem");
    dEPRECATED_TabsHeaderItemInstance.SourceBlock = dEPRECATED_TabsHeaderItem;
    var extendedClass8 = dEPRECATED_TabsHeaderItem.InputParameters.Named("ExtendedClass");
    dEPRECATED_TabsHeaderItemInstance.SetArgumentValue(extendedClass8, null); // default value
    
    var placeholderContentWidget13 = dEPRECATED_TabsHeaderItemInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget4 = placeholderContentWidget13.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "All";
    
    var dEPRECATED_TabsHeaderItemInstance2 = placeholderContentWidget11.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_TabsHeaderItemInstance2.CustomStyle = null; // default value
    dEPRECATED_TabsHeaderItemInstance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    dEPRECATED_TabsHeaderItemInstance2.SourceBlock = dEPRECATED_TabsHeaderItem;
    dEPRECATED_TabsHeaderItemInstance2.SetArgumentValue(extendedClass8, null); // default value
    
    var placeholderContentWidget14 = dEPRECATED_TabsHeaderItemInstance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget5 = placeholderContentWidget14.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Draft";
    
    var dEPRECATED_TabsHeaderItemInstance3 = placeholderContentWidget11.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_TabsHeaderItemInstance3.CustomStyle = null; // default value
    dEPRECATED_TabsHeaderItemInstance3.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    dEPRECATED_TabsHeaderItemInstance3.SourceBlock = dEPRECATED_TabsHeaderItem;
    dEPRECATED_TabsHeaderItemInstance3.SetArgumentValue(extendedClass8, null); // default value
    
    var placeholderContentWidget15 = dEPRECATED_TabsHeaderItemInstance3.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget6 = placeholderContentWidget15.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Submitted";
    
    var dEPRECATED_TabsHeaderItemInstance4 = placeholderContentWidget11.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_TabsHeaderItemInstance4.CustomStyle = null; // default value
    dEPRECATED_TabsHeaderItemInstance4.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    dEPRECATED_TabsHeaderItemInstance4.SourceBlock = dEPRECATED_TabsHeaderItem;
    dEPRECATED_TabsHeaderItemInstance4.SetArgumentValue(extendedClass8, null); // default value
    
    var placeholderContentWidget16 = dEPRECATED_TabsHeaderItemInstance4.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget7 = placeholderContentWidget16.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Approved & Closed";
    
    var dEPRECATED_TabsHeaderItemInstance5 = placeholderContentWidget11.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_TabsHeaderItemInstance5.CustomStyle = null; // default value
    dEPRECATED_TabsHeaderItemInstance5.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    dEPRECATED_TabsHeaderItemInstance5.SourceBlock = dEPRECATED_TabsHeaderItem;
    dEPRECATED_TabsHeaderItemInstance5.SetArgumentValue(extendedClass8, null); // default value
    
    var dEPRECATED_TabsContentItemInstance = placeholderContentWidget12.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_TabsContentItemInstance.CustomStyle = null; // default value
    dEPRECATED_TabsContentItemInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dEPRECATED_TabsContentItem = navigation.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DEPRECATED_TabsContentItem");
    dEPRECATED_TabsContentItemInstance.SourceBlock = dEPRECATED_TabsContentItem;
    var extendedClass9 = dEPRECATED_TabsContentItem.InputParameters.Named("ExtendedClass");
    dEPRECATED_TabsContentItemInstance.SetArgumentValue(extendedClass9, null); // default value
    
    var placeholderContentWidget17 = dEPRECATED_TabsContentItemInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var ifWidget = placeholderContentWidget17.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.Animate = false; // default value
    ifWidget.SetCondition("IsOrdersDataFetched");
    ifWidget.CustomStyle = null; // default value
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var notEmpty = ifWidget.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("NotEmpty");
    notEmpty.Animate = false; // default value
    notEmpty.SetCondition("not GetOrdersByEmployeeId.List.Empty");
    notEmpty.CustomStyle = null; // default value
    notEmpty.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var orderTable_Wrapper = notEmpty.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("OrderTable_Wrapper");
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
    
    var orderTable = orderTable_Wrapper.CreateWidget<ServiceStudio.Plugin.NRWidgets.ITableRecords>("OrderTable");
    orderTable.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    orderTable.ColumnSpan = null; // default value
    orderTable.CustomStyle = null; // default value
    orderTable.ExpandedInWebEditor = true; // default value
    orderTable.FlexBasis = null; // default value
    orderTable.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    orderTable.RowSpan = null; // default value
    orderTable.ShowHeader = true; // default value
    orderTable.SetSource("GetOrdersByEmployeeId.List");
    orderTable.SetStyle("\"table\""); // default value
    orderTable.SetStyleHeader("\"table-header\""); // default value
    orderTable.SetStyleRow("\"table-row\""); // default value
    
    var headerCell = orderTable.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
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
    
    var textWidget8 = headerCell.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.CustomStyle = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Number";
    
    var headerCell2 = orderTable.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
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
    
    var textWidget9 = headerCell2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.CustomStyle = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "Company";
    
    var headerCell3 = orderTable.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
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
    
    var textWidget10 = headerCell3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget10.CustomStyle = null; // default value
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Total Amount";
    
    var headerCell4 = orderTable.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
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
    headerCell4.SetSortAttribute("Order.ExpectedClosedOn");
    headerCell4.SetSource("GetOrdersByEmployeeId.List");
    headerCell4.SetStyle(null); // default value
    headerCell4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget11 = headerCell4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget11.CustomStyle = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "Order Date";
    
    var headerCell5 = orderTable.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
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
    headerCell5.SetSortAttribute("Order.RequiredOn");
    headerCell5.SetSource("GetOrdersByEmployeeId.List");
    headerCell5.SetStyle(null); // default value
    headerCell5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget12 = headerCell5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.CustomStyle = null; // default value
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "Required On";
    
    var headerCell6 = orderTable.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
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
    headerCell6.SetSortAttribute("User.Name");
    headerCell6.SetSource("GetOrdersByEmployeeId.List");
    headerCell6.SetStyle(null); // default value
    headerCell6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget13 = headerCell6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget13.CustomStyle = null; // default value
    textWidget13.SetStyleClasses(null); // default value
    textWidget13.Text = "Created by";
    
    var headerCell7 = orderTable.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell7.ColumnGap = null; // default value
    headerCell7.ColumnSpan = null; // default value
    headerCell7.CustomStyle = null; // default value
    headerCell7.FlexBasis = null; // default value
    headerCell7.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    headerCell7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    headerCell7.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    headerCell7.Gap = null;
    headerCell7.GridColumns = null; // default value
    headerCell7.GridRows = null; // default value
    headerCell7.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    headerCell7.RowGap = null; // default value
    headerCell7.RowSpan = null; // default value
    headerCell7.SetSortAttribute("OrderStatus.Label");
    headerCell7.SetSource("GetOrdersByEmployeeId.List");
    headerCell7.SetStyle(null); // default value
    headerCell7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget14 = headerCell7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget14.CustomStyle = null; // default value
    textWidget14.SetStyleClasses(null); // default value
    textWidget14.Text = "State";
    
    var rowCell = orderTable.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
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
    link.SetVisible("GetOrdersByEmployeeId.List.Current.Order.Id<> NullIdentifier()");
    link.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link.Width = ""; // default value
    
    var expression3 = link.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("GetOrdersByEmployeeId.List.Current.Order.Code");
    expression3.Width = ""; // default value
    var builtinEvent3 = link.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = orderDetail;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromRight;
    builtinEvent3.SetArgumentValue(accountId, null); // default value
    builtinEvent3.SetArgumentValue(opportunityId, null); // default value
    builtinEvent3.SetArgumentValue(orderId, "GetOrdersByEmployeeId.List.Current.Order.Id");
    
    var rowCell2 = orderTable.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
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
    
    var expression4 = rowCell2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("GetOrdersByEmployeeId.List.Current.Account.Company");
    expression4.Width = ""; // default value
    
    var rowCell3 = orderTable.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
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
    
    var container9 = rowCell3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Right;
    container9.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container9.Animate = false; // default value
    container9.ColumnGap = null; // default value
    container9.ColumnSpan = null; // default value
    container9.CustomStyle = null; // default value
    container9.FlexBasis = null; // default value
    container9.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container9.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container9.Gap = null;
    container9.GridColumns = null; // default value
    container9.GridRows = null; // default value
    container9.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container9.RowGap = null; // default value
    container9.RowSpan = null; // default value
    container9.SetStyle(null); // default value
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var expression5 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("FormatCurrency(GetOrdersByEmployeeId.List.Current.Order.TotalAmount, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
    expression5.Width = ""; // default value
    
    var rowCell4 = orderTable.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell4.ColumnGap = null; // default value
    rowCell4.ColumnSpan = null; // default value
    rowCell4.CustomStyle = "text-align: left;";
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
    
    var container10 = rowCell4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Right;
    container10.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container10.Animate = false; // default value
    container10.ColumnGap = null; // default value
    container10.ColumnSpan = null; // default value
    container10.CustomStyle = "text-align: left;";
    container10.FlexBasis = null; // default value
    container10.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container10.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container10.Gap = null;
    container10.GridColumns = null; // default value
    container10.GridRows = null; // default value
    container10.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container10.RowGap = null; // default value
    container10.RowSpan = null; // default value
    container10.SetStyle(null); // default value
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var container11 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container11.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container11.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container11.Animate = false; // default value
    container11.ColumnGap = null; // default value
    container11.ColumnSpan = null; // default value
    container11.CustomStyle = "text-align: left;";
    container11.FlexBasis = null; // default value
    container11.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container11.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container11.Gap = null;
    container11.GridColumns = null; // default value
    container11.GridRows = null; // default value
    container11.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container11.RowGap = null; // default value
    container11.RowSpan = null; // default value
    container11.SetStyle(null); // default value
    container11.SetVisible("True"); // default value
    container11.VisibleWasExpression = false; // default value
    container11.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container11.Width = "(fill parent)"; // default value
    
    var expression6 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = "1 Jan 2022";
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("If(GetOrdersByEmployeeId.List.Current.Order.ExpectedClosedOn <> NullDate(),\n    FormatDateTime(GetOrdersByEmployeeId.List.Current.Order.ExpectedClosedOn, \"d MMM yyyy\"),\n    \"-\"\n)");
    expression6.Width = ""; // default value
    
    var rowCell5 = orderTable.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell5.ColumnGap = null; // default value
    rowCell5.ColumnSpan = null; // default value
    rowCell5.CustomStyle = null; // default value
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
    
    var container12 = rowCell5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container12.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Right;
    container12.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container12.Animate = false; // default value
    container12.ColumnGap = null; // default value
    container12.ColumnSpan = null; // default value
    container12.CustomStyle = "text-align: left;";
    container12.FlexBasis = null; // default value
    container12.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container12.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container12.Gap = null;
    container12.GridColumns = null; // default value
    container12.GridRows = null; // default value
    container12.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container12.RowGap = null; // default value
    container12.RowSpan = null; // default value
    container12.SetStyle(null); // default value
    container12.SetVisible("True"); // default value
    container12.VisibleWasExpression = false; // default value
    container12.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container12.Width = "(fill parent)"; // default value
    
    var container13 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container13.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container13.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container13.Animate = false; // default value
    container13.ColumnGap = null; // default value
    container13.ColumnSpan = null; // default value
    container13.CustomStyle = "text-align: left;";
    container13.FlexBasis = null; // default value
    container13.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container13.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container13.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container13.Gap = null;
    container13.GridColumns = null; // default value
    container13.GridRows = null; // default value
    container13.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container13.RowGap = null; // default value
    container13.RowSpan = null; // default value
    container13.SetStyle(null); // default value
    container13.SetVisible("True"); // default value
    container13.VisibleWasExpression = false; // default value
    container13.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container13.Width = "(fill parent)"; // default value
    
    var expression7 = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("FormatDateTime(GetOrdersByEmployeeId.List.Current.Order.RequiredOn, \"d MMM yyyy\")");
    expression7.Width = ""; // default value
    
    var rowCell6 = orderTable.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
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
    
    var expression8 = rowCell6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("GetOrdersByEmployeeId.List.Current.User.Name");
    expression8.Width = ""; // default value
    
    var rowCell7 = orderTable.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell7.ColumnGap = null; // default value
    rowCell7.ColumnSpan = null; // default value
    rowCell7.CustomStyle = "text-align: left;";
    rowCell7.FlexBasis = null; // default value
    rowCell7.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    rowCell7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    rowCell7.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    rowCell7.Gap = null;
    rowCell7.GridColumns = null; // default value
    rowCell7.GridRows = null; // default value
    rowCell7.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    rowCell7.RowGap = null; // default value
    rowCell7.RowSpan = null; // default value
    rowCell7.SetStyle(null); // default value
    rowCell7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var container14 = rowCell7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container14.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container14.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container14.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container14.Animate = false; // default value
    container14.ColumnGap = null; // default value
    container14.ColumnSpan = null; // default value
    container14.CustomStyle = "margin-right: 0px; text-align: center;";
    container14.FlexBasis = null; // default value
    container14.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container14.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container14.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container14.Gap = null;
    container14.GridColumns = null; // default value
    container14.GridRows = null; // default value
    container14.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container14.RowGap = null; // default value
    container14.RowSpan = null; // default value
    container14.SetStyle("\"label-status Label background-red-dark\"");
    container14.SetVisible("True"); // default value
    container14.VisibleWasExpression = false; // default value
    container14.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container14.Width = "(fill parent)"; // default value
    
    var expression9 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("GetOrdersByEmployeeId.List.Current.OrderStatus.Label");
    expression9.Width = ""; // default value
    
    var extendedProperty2 = container14.CreateExtendedProperty();
    extendedProperty2.Property = "style";
    extendedProperty2.SetValue("\"background-color: \" + GetOrdersByEmployeeId.List.Current.OrderStatus.ColorRGB");
    var builtinEvent4 = orderTable.OnSort;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = orderTableOnSort;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var clickedColumn = builtinEvent4.InputParameters.Named("ClickedColumn");
    clickedColumn.DataType = eSpace.TextType;
    clickedColumn.Description = null; // default value
    clickedColumn.IsMandatory = true; // default value
    clickedColumn.Name = "ClickedColumn";
    builtinEvent4.SetArgumentValue(sortBy, "ClickedColumn");
    
    var container15 = orderTable_Wrapper.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container15.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container15.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container15.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container15.Animate = false; // default value
    container15.ColumnGap = null; // default value
    container15.ColumnSpan = null; // default value
    container15.CustomStyle = null; // default value
    container15.FlexBasis = null; // default value
    container15.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container15.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container15.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container15.Gap = null;
    container15.GridColumns = null; // default value
    container15.GridRows = null; // default value
    container15.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container15.RowGap = null; // default value
    container15.RowSpan = null; // default value
    container15.SetStyle(null); // default value
    container15.SetVisible("True"); // default value
    container15.VisibleWasExpression = false; // default value
    container15.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container15.Width = "(fill parent)"; // default value
    
    var paginationInstance = container15.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    paginationInstance.CustomStyle = null; // default value
    paginationInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var pagination = navigation.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Pagination");
    paginationInstance.SourceBlock = pagination;
    // delete default content in Previous placeholder
    var placeholderContentWidget18 = paginationInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Previous");
    placeholderContentWidget18.Widgets.ToList().ForEach(w => w.Delete());
    // delete default content in Next placeholder
    var placeholderContentWidget19 = paginationInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Next");
    placeholderContentWidget19.Widgets.ToList().ForEach(w => w.Delete());
    var totalCount = pagination.InputParameters.Named("TotalCount");
    paginationInstance.SetArgumentValue(totalCount, "GetOrdersByEmployeeId.Count");
    var startIndex2 = pagination.InputParameters.Named("StartIndex");
    paginationInstance.SetArgumentValue(startIndex2, "StartIndex");
    var maxRecords2 = pagination.InputParameters.Named("MaxRecords");
    paginationInstance.SetArgumentValue(maxRecords2, "MaxRecords");
    var showGoToPage = pagination.InputParameters.Named("ShowGoToPage");
    paginationInstance.SetArgumentValue(showGoToPage, null);
    var extendedClass10 = pagination.InputParameters.Named("ExtendedClass");
    paginationInstance.SetArgumentValue(extendedClass10, null); // default value
    var eventHandler9 = paginationInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnNavigate");
    eventHandler9.Handler = paginationOnNavigate;
    eventHandler9.SetArgumentValue(newStartIndex, "NewStartIndex");
    
    var icon = placeholderContentWidget18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
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
    
    var icon2 = placeholderContentWidget19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
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
    
    var container16 = notEmpty.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container16.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container16.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container16.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container16.Animate = false; // default value
    container16.ColumnGap = null; // default value
    container16.ColumnSpan = null; // default value
    container16.CustomStyle = null; // default value
    container16.FlexBasis = null; // default value
    container16.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container16.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container16.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container16.Gap = null;
    container16.GridColumns = null; // default value
    container16.GridRows = null; // default value
    container16.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container16.RowGap = null; // default value
    container16.RowSpan = null; // default value
    container16.SetStyle("\"margin-top-xl\"");
    container16.SetVisible("True"); // default value
    container16.VisibleWasExpression = false; // default value
    container16.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container16.Width = "(fill parent)"; // default value
    
    var blankSlateInstance = container16.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    blankSlateInstance.CustomStyle = null; // default value
    blankSlateInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var content = outSystemsUI.MobileFlows.Named("Content");
    var blankSlate = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("BlankSlate");
    blankSlateInstance.SourceBlock = blankSlate;
    // delete default content in Icon placeholder
    var placeholderContentWidget20 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    placeholderContentWidget20.Widgets.ToList().ForEach(w => w.Delete());
    var fullHeight = blankSlate.InputParameters.Named("FullHeight");
    blankSlateInstance.SetArgumentValue(fullHeight, null);
    var extendedClass11 = blankSlate.InputParameters.Named("ExtendedClass");
    blankSlateInstance.SetArgumentValue(extendedClass11, null); // default value
    
    var image = placeholderContentWidget20.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image.ColumnSpan = null; // default value
    image.CustomStyle = null; // default value
    image.DefaultImage = null; // default value
    image.FlexBasis = null; // default value
    image.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    var ordersIcon = eSpace.Images.Named("OrdersIcon");
    image.Image = ordersIcon;
    image.SetImageContent(null); // default value
    image.RowSpan = null; // default value
    image.SetStyle(null); // default value
    image.Type = ServiceStudio.Plugin.NRWidgets.Enumerations.Type.Static; // default value
    image.SetUrl(null); // default value
    image.Width = ""; // default value
    
    var placeholderContentWidget21 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var textWidget15 = placeholderContentWidget21.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget15.CustomStyle = null; // default value
    textWidget15.SetStyleClasses("\"text-neutral-8\"");
    textWidget15.Text = "There aren\'t any Orders to show.";
    
    var placeholderContentWidget22 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Footer");
    var footerInstance = placeholderContentWidget22.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    footerInstance.CustomStyle = null; // default value
    footerInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var footer = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Footer");
    footerInstance.SourceBlock = footer;
}

```