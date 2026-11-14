```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow opportunity) => {
    
    /*** creating IMobileScreen 'OpportunityDetail' ***/
    var opportunityDetail = opportunity.CreateScreen("OpportunityDetail");
    opportunityDetail.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = opportunity.GetESpace();
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    opportunityDetail.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    opportunityDetail.Roles.Add(orderManager);
    opportunityDetail.AnonymousAccess = false; // default value
    opportunityDetail.CustomURL = false; // default value
    opportunityDetail.Description = null; // default value
    opportunityDetail.HorizontalPosition = 2742;
    opportunityDetail.Icon = null; // default value
    opportunityDetail.IsDisabled = false; // default value
    opportunityDetail.PageName = null; // default value
    opportunityDetail.Public = false; // default value
    opportunityDetail.Shareable = false; // default value
    opportunityDetail.StyleSheet = null; // default value
    opportunityDetail.SetTitle("\"Order Management - Opportunity Detail\"");
    opportunityDetail.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    opportunityDetail.VerticalPosition = 1142;
    
    var getOpportunity = opportunityDetail.CreateScreenAggregate(false, "GetOpportunity");
    getOpportunity.CacheInMinutes = null; // default value
    getOpportunity.Description = null; // default value
    getOpportunity.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getOpportunity.SetMaxRecords("1");
    getOpportunity.SetStartIndex(null); // default value
    getOpportunity.Timeout = null; // default value
    var opportunity3 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Opportunity");
    var opportunity2 = getOpportunity.AsDatabaseAggregate.CreateSource(opportunity3);
    var account2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Account");
    var account = getOpportunity.AsDatabaseAggregate.CreateSource(account2);
    var opportunityStage2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IStaticEntitySignature>().Named("OpportunityStage");
    var opportunityStage = getOpportunity.AsDatabaseAggregate.CreateSource(opportunityStage2);
    var join = getOpportunity.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = opportunity2;
    join.RightSource = opportunityStage;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("Opportunity.OpportunityStageId = OpportunityStage.Id");
    var join2 = getOpportunity.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = account;
    join2.RightSource = opportunity2;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join2.SetCondition("Opportunity.AccountId = Account.Id");
    getOpportunity.AsDatabaseAggregate.CreateFilter("Opportunity.Id = OpportunityId");
    
    
    var getOrdersByOpportunityId = opportunityDetail.CreateScreenAggregate(false, "GetOrdersByOpportunityId");
    getOrdersByOpportunityId.CacheInMinutes = null; // default value
    getOrdersByOpportunityId.Description = null; // default value
    getOrdersByOpportunityId.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getOrdersByOpportunityId.SetMaxRecords("MaxRecords");
    getOrdersByOpportunityId.SetStartIndex("StartIndex");
    getOrdersByOpportunityId.Timeout = null; // default value
    var account3 = getOrdersByOpportunityId.AsDatabaseAggregate.CreateSource(account2);
    var opportunity4 = getOrdersByOpportunityId.AsDatabaseAggregate.CreateSource(opportunity3);
    var order2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Order");
    var order = getOrdersByOpportunityId.AsDatabaseAggregate.CreateSource(order2);
    var orderStatus2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IStaticEntitySignature>().Named("OrderStatus");
    var orderStatus = getOrdersByOpportunityId.AsDatabaseAggregate.CreateSource(orderStatus2);
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getOrdersByOpportunityId.AsDatabaseAggregate.CreateSource(user2);
    var join3 = getOrdersByOpportunityId.AsDatabaseAggregate.CreateJoin();
    join3.LeftSource = account3;
    join3.RightSource = order;
    join3.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join3.SetCondition("Order.AccountId = Account.Id");
    var join4 = getOrdersByOpportunityId.AsDatabaseAggregate.CreateJoin();
    join4.LeftSource = user;
    join4.RightSource = order;
    join4.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join4.SetCondition("Order.CreatedBy = User.Id");
    var join5 = getOrdersByOpportunityId.AsDatabaseAggregate.CreateJoin();
    join5.LeftSource = order;
    join5.RightSource = orderStatus;
    join5.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join5.SetCondition("Order.OrderStatusId = OrderStatus.Id");
    var join6 = getOrdersByOpportunityId.AsDatabaseAggregate.CreateJoin();
    join6.LeftSource = opportunity4;
    join6.RightSource = order;
    join6.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join6.SetCondition("Order.OpportunityId = Opportunity.Id");
    getOrdersByOpportunityId.AsDatabaseAggregate.CreateFilter("Order.OpportunityId = OpportunityId");
    getOrdersByOpportunityId.AsDatabaseAggregate.CreateSort().SetAttribute("TableSort");
    
    
    var opportunityId = opportunityDetail.CreateInputParameter("OpportunityId");
    opportunityId.DataType = opportunity3.IdentifierType;
    opportunityId.Description = null; // default value
    opportunityId.IsMandatory = true; // default value
    
    var maxRecords = opportunityDetail.CreateLocalVariable("MaxRecords");
    maxRecords.DataType = eSpace.IntegerType;
    maxRecords.SetDefaultValue("10");
    maxRecords.Description = null; // default value
    
    var tableSort = opportunityDetail.CreateLocalVariable("TableSort");
    tableSort.DataType = eSpace.TextType;
    tableSort.SetDefaultValue(null); // default value
    tableSort.Description = null; // default value
    
    var startIndex = opportunityDetail.CreateLocalVariable("StartIndex");
    startIndex.DataType = eSpace.IntegerType;
    startIndex.SetDefaultValue(null); // default value
    startIndex.Description = null; // default value
    opportunityDetail.OnDestroy.Destination = null; // default value
    opportunityDetail.OnInitialize.Destination = null; // default value
    opportunityDetail.OnReady.Destination = null; // default value
    opportunityDetail.OnRender.Destination = null; // default value
    
    var onNavigate = opportunityDetail.CreateScreenAction("OnNavigate");
    onNavigate.Description = null; // default value
    
    var newStartIndex = onNavigate.CreateInputParameter("NewStartIndex");
    newStartIndex.DataType = eSpace.IntegerType;
    newStartIndex.Description = "New Index used for pagination.";
    newStartIndex.IsMandatory = true; // default value
    
    var startNode = onNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var assignNode = onNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode, 1729);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "StartIndex = NewStartIndex";
    
    var assignment = assignNode.CreateAssignment("StartIndex", "NewStartIndex");
    
    var refreshDataNode = onNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode, 1558);
    refreshDataNode.DataSource = getOrdersByOpportunityId;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords("MaxRecords");
    refreshDataNode.SetStartIndex("StartIndex");
    
    var endNode = onNavigate.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode, 1330);
    endNode.IsDisabled = false; // default value
    
    var onSort = opportunityDetail.CreateScreenAction("OnSort");
    onSort.Description = null; // default value
    
    var sortBy = onSort.CreateInputParameter("SortBy");
    sortBy.DataType = eSpace.TextType;
    sortBy.Description = "Table column by which the aggregate will be sorted.";
    sortBy.IsMandatory = true; // default value
    
    var startNode2 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var ifNode = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(startNode2, 1330);
    ifNode.SetCondition("TableSort = SortBy");
    ifNode.IsDisabled = false; // default value
    ifNode.Label = null; // default value
    
    var assignNode2 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1330);
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "TableSort = SortBy";
    
    var assignment2 = assignNode2.CreateAssignment("TableSort", "SortBy");
    ifNode.FalseTarget = assignNode2;
    
    var assignNode3 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode, 1772);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "TableSort += DESC";
    
    var assignment3 = assignNode3.CreateAssignment("TableSort", "SortBy + \" DESC\"");
    ifNode.TrueTarget = assignNode3;
    
    var assignNode4 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(assignNode2, 1330);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "StartIndex = 0";
    
    var assignment4 = assignNode4.CreateAssignment("StartIndex", "0");
    assignNode3.Target = assignNode4;
    
    var refreshDataNode2 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode4, 1330);
    refreshDataNode2.DataSource = getOrdersByOpportunityId;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords("MaxRecords");
    refreshDataNode2.SetStartIndex("StartIndex");
    
    var endNode2 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode2, 1330);
    endNode2.IsDisabled = false; // default value
    
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
    commentNode2.CreateConnector(assignNode4);
    
    var layoutTopInstance = opportunityDetail.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    menuInstance.SetArgumentValue(activeMenu, "Entities.MenuItem.Opportunites");
    
    var placeholderContentWidget3 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Breadcrumbs");
    var link = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link.Widgets.ToList().ForEach(x => x.Delete());
    link.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link.ColumnGap = null; // default value
    link.ColumnSpan = null; // default value
    link.SetConfirmationMessage(null); // default value
    link.CustomStyle = "font-weight: normal;";
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
    
    var icon = link.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
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
    
    var textWidget = link.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses("\"margin-left-base\"");
    textWidget.Text = "Back";
    var builtinEvent = link.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = eSpace.PreviousScreen;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromLeft;
    
    var placeholderContentWidget4 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var expression = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = "Sales Opportunity";
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetOpportunity.List.Current.Opportunity.Name");
    expression.Width = ""; // default value
    
    var placeholderContentWidget5 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var container = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.SetStyle(null); // default value
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "6 col";
    
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
    container2.SetStyle("\"font-bold\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var textWidget2 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = "font-size: 16px;";
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Opportunity Details";
    
    var container3 = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container3.SetStyle("\"margin-top-base\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var form1 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IForm>("Form1");
    form1.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    form1.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    form1.ColumnGap = null; // default value
    form1.ColumnSpan = null; // default value
    form1.CustomStyle = null; // default value
    form1.FlexBasis = null; // default value
    form1.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    form1.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    form1.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    form1.Gap = null;
    form1.GridColumns = null; // default value
    form1.GridRows = null; // default value
    form1.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    form1.RowGap = null; // default value
    form1.RowSpan = null; // default value
    form1.SetStyle("\"form \"");
    form1.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    form1.Width = "(fill parent)"; // default value
    
    var columns2Instance = form1.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance.CustomStyle = null; // default value
    columns2Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var adaptive = outSystemsUI.MobileFlows.Named("Adaptive");
    var columns2 = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Columns2");
    columns2Instance.SourceBlock = columns2;
    var gutterSize = columns2.InputParameters.Named("GutterSize");
    columns2Instance.SetArgumentValue(gutterSize, null);
    var tabletBehavior = columns2.InputParameters.Named("TabletBehavior");
    columns2Instance.SetArgumentValue(tabletBehavior, null);
    var phoneBehavior = columns2.InputParameters.Named("PhoneBehavior");
    columns2Instance.SetArgumentValue(phoneBehavior, null);
    var extendedClass = columns2.InputParameters.Named("ExtendedClass");
    columns2Instance.SetArgumentValue(extendedClass, null); // default value
    
    var placeholderContentWidget6 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var container4 = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.Width = "(fill parent)"; // default value
    
    var label = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label.SetStyle(null); // default value
    label.TargetWidget = null; // default value
    label.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label.Width = "(fill parent)"; // default value
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label.Widgets.First();
    textWidget3.CustomStyle = null; // default value
    textWidget3.Name = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Account";
    
    var expression2 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetOpportunity.List.Current.Account.Company");
    expression2.Width = ""; // default value
    
    var container5 = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container5.SetStyle("\"margin-top-base\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var label2 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label2.SetStyle(null); // default value
    label2.TargetWidget = null; // default value
    label2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label2.Width = "(fill parent)"; // default value
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label2.Widgets.First();
    textWidget4.CustomStyle = null; // default value
    textWidget4.Name = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Description";
    
    var expression3 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("GetOpportunity.List.Current.Opportunity.Description");
    expression3.Width = ""; // default value
    
    var container6 = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container6.SetStyle("\"margin-top-base\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var label3 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label3.SetStyle(null); // default value
    label3.TargetWidget = null; // default value
    label3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label3.Width = "(fill parent)"; // default value
    
    var textWidget5 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label3.Widgets.First();
    textWidget5.CustomStyle = null; // default value
    textWidget5.Name = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Close Date";
    
    var expression4 = container6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("GetOpportunity.List.Current.Opportunity.CloseOn");
    expression4.Width = ""; // default value
    
    var container7 = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container7.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container7.Animate = false; // default value
    container7.ColumnGap = null; // default value
    container7.ColumnSpan = null; // default value
    container7.CustomStyle = null; // default value
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
    container7.SetStyle("\"margin-top-base\"");
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "(fill parent)"; // default value
    
    var label4 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label4.SetStyle(null); // default value
    label4.TargetWidget = null; // default value
    label4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label4.Width = "(fill parent)"; // default value
    
    var textWidget6 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label4.Widgets.First();
    textWidget6.CustomStyle = null; // default value
    textWidget6.Name = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Stage name";
    
    var expression5 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("GetOpportunity.List.Current.OpportunityStage.Label");
    expression5.Width = ""; // default value
    
    var container8 = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container8.Animate = false; // default value
    container8.ColumnGap = null; // default value
    container8.ColumnSpan = null; // default value
    container8.CustomStyle = null; // default value
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
    container8.SetStyle("\"margin-top-base\"");
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var label5 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label5.SetStyle(null); // default value
    label5.TargetWidget = null; // default value
    label5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label5.Width = "";
    
    var textWidget7 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label5.Widgets.First();
    textWidget7.CustomStyle = null; // default value
    textWidget7.Name = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Is Closed";
    
    var container9 = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container9.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
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
    container9.Width = "";
    
    var ifWidget = container9.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.Animate = false; // default value
    ifWidget.SetCondition("GetOpportunity.List.Current.OpportunityStage.Id = Entities.OpportunityStage.ClosedWon\nor GetOpportunity.List.Current.OpportunityStage.Id = Entities.OpportunityStage.ClosedLost");
    ifWidget.CustomStyle = null; // default value
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var icon2 = ifWidget.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon2.ColumnSpan = null; // default value
    icon2.CustomStyle = null; // default value
    icon2.FlexBasis = null; // default value
    icon2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon2.Icon = "check";
    icon2.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon2.RowSpan = null; // default value
    icon2.SetStyle("\"icon text-success\"");
    icon2.SetVisible("True"); // default value
    icon2.Weight = null; // default value
    
    var ifWidget2 = ifWidget.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget2.Animate = false; // default value
    ifWidget2.SetCondition("GetOpportunity.List.Current.OpportunityStage.Id = Entities.OpportunityStage.ClosedWon");
    ifWidget2.CustomStyle = null; // default value
    ifWidget2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var textWidget8 = ifWidget2.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.CustomStyle = null; // default value
    textWidget8.SetStyleClasses("\"margin-left-s\"");
    textWidget8.Text = "Won";
    
    var icon3 = ifWidget.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon3.ColumnSpan = null; // default value
    icon3.CustomStyle = null; // default value
    icon3.FlexBasis = null; // default value
    icon3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon3.Icon = "remove";
    icon3.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon3.RowSpan = null; // default value
    icon3.SetStyle("\"icon text-error\"");
    icon3.SetVisible("True"); // default value
    icon3.Weight = null; // default value
    
    var placeholderContentWidget7 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container10 = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container10.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container10.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container10.Animate = false; // default value
    container10.ColumnGap = null; // default value
    container10.ColumnSpan = null; // default value
    container10.CustomStyle = null; // default value
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
    container10.SetStyle("\"margin-top-xs\"");
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var label6 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label6.SetStyle(null); // default value
    label6.TargetWidget = null; // default value
    label6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label6.Width = "(fill parent)"; // default value
    
    var textWidget9 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label6.Widgets.First();
    textWidget9.CustomStyle = null; // default value
    textWidget9.Name = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "Amount";
    
    var expression6 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("GetOpportunity.List.Current.Opportunity.Amount");
    expression6.Width = ""; // default value
    
    var container11 = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container11.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container11.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container11.Animate = false; // default value
    container11.ColumnGap = null; // default value
    container11.ColumnSpan = null; // default value
    container11.CustomStyle = null; // default value
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
    container11.SetStyle("\"margin-top-base\"");
    container11.SetVisible("True"); // default value
    container11.VisibleWasExpression = false; // default value
    container11.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container11.Width = "(fill parent)"; // default value
    
    var label7 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label7.SetStyle(null); // default value
    label7.TargetWidget = null; // default value
    label7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label7.Width = "(fill parent)"; // default value
    
    var textWidget10 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label7.Widgets.First();
    textWidget10.CustomStyle = null; // default value
    textWidget10.Name = null; // default value
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Expected Revenue";
    
    var expression7 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = null; // default value
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("GetOpportunity.List.Current.Opportunity.ExpectedRevenue");
    expression7.Width = ""; // default value
    
    var container12 = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container12.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container12.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container12.Animate = false; // default value
    container12.ColumnGap = null; // default value
    container12.ColumnSpan = null; // default value
    container12.CustomStyle = null; // default value
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
    container12.SetStyle("\"margin-top-base\"");
    container12.SetVisible("True"); // default value
    container12.VisibleWasExpression = false; // default value
    container12.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container12.Width = "(fill parent)"; // default value
    
    var label8 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label8.SetStyle(null); // default value
    label8.TargetWidget = null; // default value
    label8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label8.Width = "(fill parent)"; // default value
    
    var textWidget11 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label8.Widgets.First();
    textWidget11.CustomStyle = null; // default value
    textWidget11.Name = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "Probability";
    
    var expression8 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("GetOpportunity.List.Current.Opportunity.Probability");
    expression8.Width = ""; // default value
    
    var separatorInstance = placeholderContentWidget5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    separatorInstance.CustomStyle = null; // default value
    separatorInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var utilities = outSystemsUI.MobileFlows.Named("Utilities");
    var separator = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Separator");
    separatorInstance.SourceBlock = separator;
    var color = separator.InputParameters.Named("Color");
    separatorInstance.SetArgumentValue(color, null);
    var isVertical = separator.InputParameters.Named("IsVertical");
    separatorInstance.SetArgumentValue(isVertical, null);
    var extendedClass2 = separator.InputParameters.Named("ExtendedClass");
    separatorInstance.SetArgumentValue(extendedClass2, null); // default value
    var space = separator.InputParameters.Named("Space");
    separatorInstance.SetArgumentValue(space, "Entities.Space.Large");
    
    var container13 = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container13.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container13.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container13.Animate = false; // default value
    container13.ColumnGap = null; // default value
    container13.ColumnSpan = null; // default value
    container13.CustomStyle = null; // default value
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
    
    var container14 = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container14.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container14.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container14.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container14.Animate = false; // default value
    container14.ColumnGap = null; // default value
    container14.ColumnSpan = null; // default value
    container14.CustomStyle = null; // default value
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
    container14.SetStyle("\"margin-bottom-base\"");
    container14.SetVisible("True"); // default value
    container14.VisibleWasExpression = false; // default value
    container14.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container14.Width = "(fill parent)"; // default value
    
    var container15 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container15.SetStyle("\" heading2 \"");
    container15.SetVisible("True"); // default value
    container15.VisibleWasExpression = false; // default value
    container15.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container15.Width = "6 col";
    
    var textWidget12 = container15.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.CustomStyle = null; // default value
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "Orders";
    
    var container16 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container16.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container16.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container16.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container16.Animate = false; // default value
    container16.ColumnGap = null; // default value
    container16.ColumnSpan = null; // default value
    container16.CustomStyle = "text-align: right;";
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
    container16.SetStyle(null); // default value
    container16.SetVisible("True"); // default value
    container16.VisibleWasExpression = false; // default value
    container16.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container16.Width = "6 col";
    
    var button = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
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
    button.Width = ""; // default value
    
    var textWidget13 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button.Widgets.First();
    textWidget13.CustomStyle = null; // default value
    textWidget13.Name = null; // default value
    textWidget13.SetStyleClasses(null); // default value
    textWidget13.Text = "Create New Order for Opportunity";
    var builtinEvent2 = button.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var order3 = eSpace.MobileFlows.Named("Order");
    var orderDetail = order3.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("OrderDetail");
    builtinEvent2.Destination = orderDetail;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromRight;
    var orderId = orderDetail.InputParameters.Named("OrderId");
    builtinEvent2.SetArgumentValue(orderId, "NullIdentifier()");
    var opportunityId2 = orderDetail.InputParameters.Named("OpportunityId");
    builtinEvent2.SetArgumentValue(opportunityId2, "OpportunityId");
    var accountId = orderDetail.InputParameters.Named("AccountId");
    builtinEvent2.SetArgumentValue(accountId, "GetOpportunity.List.Current.Account.Id");
    
    var ifWidget3 = placeholderContentWidget5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget3.Animate = false; // default value
    ifWidget3.SetCondition("GetOrdersByOpportunityId.List.Empty and GetOrdersByOpportunityId.IsDataFetched");
    ifWidget3.CustomStyle = null; // default value
    ifWidget3.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var blankSlateInstance = ifWidget3.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    blankSlateInstance.CustomStyle = null; // default value
    blankSlateInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var content = outSystemsUI.MobileFlows.Named("Content");
    var blankSlate = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("BlankSlate");
    blankSlateInstance.SourceBlock = blankSlate;
    // delete default content in Icon placeholder
    var placeholderContentWidget8 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    placeholderContentWidget8.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass3 = blankSlate.InputParameters.Named("ExtendedClass");
    blankSlateInstance.SetArgumentValue(extendedClass3, null); // default value
    var fullHeight = blankSlate.InputParameters.Named("FullHeight");
    blankSlateInstance.SetArgumentValue(fullHeight, null);
    
    var image = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
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
    
    var placeholderContentWidget9 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var textWidget14 = placeholderContentWidget9.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget14.CustomStyle = null; // default value
    textWidget14.SetStyleClasses("\"text-neutral-8\"");
    textWidget14.Text = "There aren\'t any Orders to show.";
    
    var container17 = ifWidget3.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container17.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container17.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container17.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container17.Animate = false; // default value
    container17.ColumnGap = null; // default value
    container17.ColumnSpan = null; // default value
    container17.CustomStyle = null; // default value
    container17.FlexBasis = null; // default value
    container17.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container17.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container17.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container17.Gap = null;
    container17.GridColumns = null; // default value
    container17.GridRows = null; // default value
    container17.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container17.RowGap = null; // default value
    container17.RowSpan = null; // default value
    container17.SetStyle("\"margin-top-base\"");
    container17.SetVisible("True"); // default value
    container17.VisibleWasExpression = false; // default value
    container17.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container17.Width = "(fill parent)"; // default value
    
    var tableRecords = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.ITableRecords>();
    tableRecords.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    tableRecords.ColumnSpan = null; // default value
    tableRecords.CustomStyle = null; // default value
    tableRecords.ExpandedInWebEditor = true; // default value
    tableRecords.FlexBasis = null; // default value
    tableRecords.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    tableRecords.RowSpan = null; // default value
    tableRecords.ShowHeader = true; // default value
    tableRecords.SetSource("GetOrdersByOpportunityId.List");
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
    headerCell.SetSource("GetOrdersByOpportunityId.List");
    headerCell.SetStyle(null); // default value
    headerCell.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget15 = headerCell.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget15.CustomStyle = null; // default value
    textWidget15.SetStyleClasses(null); // default value
    textWidget15.Text = "Number";
    
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
    headerCell2.SetSortAttribute("Order.OrderStatusId");
    headerCell2.SetSource("GetOrdersByOpportunityId.List");
    headerCell2.SetStyle(null); // default value
    headerCell2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget16 = headerCell2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget16.CustomStyle = null; // default value
    textWidget16.SetStyleClasses(null); // default value
    textWidget16.Text = "Status";
    
    var headerCell3 = tableRecords.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell3.ColumnGap = null; // default value
    headerCell3.ColumnSpan = null; // default value
    headerCell3.CustomStyle = null; // default value
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
    headerCell3.SetSortAttribute("Order.ExpectedClosedOn");
    headerCell3.SetSource("GetOrdersByOpportunityId.List");
    headerCell3.SetStyle(null); // default value
    headerCell3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget17 = headerCell3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget17.CustomStyle = null; // default value
    textWidget17.SetStyleClasses(null); // default value
    textWidget17.Text = "Order Date";
    
    var headerCell4 = tableRecords.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell4.ColumnGap = null; // default value
    headerCell4.ColumnSpan = null; // default value
    headerCell4.CustomStyle = "text-align: right;";
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
    headerCell4.SetSortAttribute("Order.TotalAmount");
    headerCell4.SetSource("GetOrdersByOpportunityId.List");
    headerCell4.SetStyle(null); // default value
    headerCell4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget18 = headerCell4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget18.CustomStyle = null; // default value
    textWidget18.SetStyleClasses(null); // default value
    textWidget18.Text = "Total Amount";
    
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
    
    var link2 = rowCell.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
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
    
    var expression9 = link2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("GetOrdersByOpportunityId.List.Current.Order.Code");
    expression9.Width = ""; // default value
    var builtinEvent3 = link2.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = orderDetail;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromRight;
    builtinEvent3.SetArgumentValue(orderId, "GetOrdersByOpportunityId.List.Current.Order.Id");
    builtinEvent3.SetArgumentValue(accountId, null); // default value
    builtinEvent3.SetArgumentValue(opportunityId2, null); // default value
    
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
    
    var expression10 = rowCell2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression10.ColumnSpan = null; // default value
    expression10.CustomStyle = null; // default value
    expression10.Example = null; // default value
    expression10.FlexBasis = null; // default value
    expression10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression10.RowSpan = null; // default value
    expression10.SetStyle(null); // default value
    expression10.SetValue("GetOrdersByOpportunityId.List.Current.OrderStatus.Label");
    expression10.Width = ""; // default value
    
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
    
    var container18 = rowCell3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container18.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Right;
    container18.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container18.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container18.Animate = false; // default value
    container18.ColumnGap = null; // default value
    container18.ColumnSpan = null; // default value
    container18.CustomStyle = null; // default value
    container18.FlexBasis = null; // default value
    container18.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container18.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container18.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container18.Gap = null;
    container18.GridColumns = null; // default value
    container18.GridRows = null; // default value
    container18.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container18.RowGap = null; // default value
    container18.RowSpan = null; // default value
    container18.SetStyle(null); // default value
    container18.SetVisible("True"); // default value
    container18.VisibleWasExpression = false; // default value
    container18.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container18.Width = "(fill parent)"; // default value
    
    var expression11 = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression11.ColumnSpan = null; // default value
    expression11.CustomStyle = null; // default value
    expression11.Example = null; // default value
    expression11.FlexBasis = null; // default value
    expression11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression11.RowSpan = null; // default value
    expression11.SetStyle(null); // default value
    expression11.SetValue("FormatDateTime(GetOrdersByOpportunityId.List.Current.Order.ExpectedClosedOn, \"d MMM yyyy\")");
    expression11.Width = ""; // default value
    
    var rowCell4 = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell4.ColumnGap = null; // default value
    rowCell4.ColumnSpan = null; // default value
    rowCell4.CustomStyle = "text-align: right;";
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
    
    var container19 = rowCell4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container19.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container19.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container19.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container19.Animate = false; // default value
    container19.ColumnGap = null; // default value
    container19.ColumnSpan = null; // default value
    container19.CustomStyle = "text-align: right;";
    container19.FlexBasis = null; // default value
    container19.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container19.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container19.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container19.Gap = null;
    container19.GridColumns = null; // default value
    container19.GridRows = null; // default value
    container19.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container19.RowGap = null; // default value
    container19.RowSpan = null; // default value
    container19.SetStyle(null); // default value
    container19.SetVisible("True"); // default value
    container19.VisibleWasExpression = false; // default value
    container19.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container19.Width = "(fill parent)"; // default value
    
    var expression12 = container19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression12.ColumnSpan = null; // default value
    expression12.CustomStyle = null; // default value
    expression12.Example = null; // default value
    expression12.FlexBasis = null; // default value
    expression12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression12.RowSpan = null; // default value
    expression12.SetStyle(null); // default value
    expression12.SetValue("FormatCurrency(GetOrdersByOpportunityId.List.Current.Order.TotalAmount, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
    expression12.Width = ""; // default value
    var builtinEvent4 = tableRecords.OnSort;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = onSort;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var clickedColumn = builtinEvent4.InputParameters.Named("ClickedColumn");
    clickedColumn.DataType = eSpace.TextType;
    clickedColumn.Description = null; // default value
    clickedColumn.IsMandatory = true; // default value
    clickedColumn.Name = "ClickedColumn";
    builtinEvent4.SetArgumentValue(sortBy, "ClickedColumn");
    
    var paginationInstance = container17.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    paginationInstance.CustomStyle = null; // default value
    paginationInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var navigation = outSystemsUI.MobileFlows.Named("Navigation");
    var pagination = navigation.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Pagination");
    paginationInstance.SourceBlock = pagination;
    // delete default content in Previous placeholder
    var placeholderContentWidget10 = paginationInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Previous");
    placeholderContentWidget10.Widgets.ToList().ForEach(w => w.Delete());
    // delete default content in Next placeholder
    var placeholderContentWidget11 = paginationInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Next");
    placeholderContentWidget11.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass4 = pagination.InputParameters.Named("ExtendedClass");
    paginationInstance.SetArgumentValue(extendedClass4, null); // default value
    var startIndex2 = pagination.InputParameters.Named("StartIndex");
    paginationInstance.SetArgumentValue(startIndex2, "StartIndex");
    var totalCount = pagination.InputParameters.Named("TotalCount");
    paginationInstance.SetArgumentValue(totalCount, "GetOrdersByOpportunityId.Count");
    var showGoToPage = pagination.InputParameters.Named("ShowGoToPage");
    paginationInstance.SetArgumentValue(showGoToPage, null);
    var maxRecords2 = pagination.InputParameters.Named("MaxRecords");
    paginationInstance.SetArgumentValue(maxRecords2, "MaxRecords");
    var eventHandler = paginationInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnNavigate");
    eventHandler.Handler = onNavigate;
    eventHandler.SetArgumentValue(newStartIndex, "NewStartIndex");
    
    var icon4 = placeholderContentWidget10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon4.ColumnSpan = null; // default value
    icon4.CustomStyle = null; // default value
    icon4.FlexBasis = null; // default value
    icon4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon4.Icon = "angle-left";
    icon4.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon4.RowSpan = null; // default value
    icon4.SetStyle("\"icon\""); // default value
    icon4.SetVisible("True"); // default value
    icon4.Weight = null; // default value
    
    var icon5 = placeholderContentWidget11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon5.ColumnSpan = null; // default value
    icon5.CustomStyle = null; // default value
    icon5.FlexBasis = null; // default value
    icon5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon5.Icon = "angle-right";
    icon5.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon5.RowSpan = null; // default value
    icon5.SetStyle("\"icon\""); // default value
    icon5.SetVisible("True"); // default value
    icon5.Weight = null; // default value
    
    var ifWidget4 = container17.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget4.Animate = false; // default value
    ifWidget4.SetCondition("GetOrdersByOpportunityId.IsDataFetched or GetOrdersByOpportunityId.List.Empty");
    ifWidget4.CustomStyle = null; // default value
    ifWidget4.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container20 = ifWidget4.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container20.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container20.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container20.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container20.Animate = false; // default value
    container20.ColumnGap = null; // default value
    container20.ColumnSpan = null; // default value
    container20.CustomStyle = null; // default value
    container20.FlexBasis = null; // default value
    container20.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container20.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container20.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container20.Gap = null;
    container20.GridColumns = null; // default value
    container20.GridRows = null; // default value
    container20.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container20.RowGap = null; // default value
    container20.RowSpan = null; // default value
    container20.SetStyle("\"table-updating\"");
    container20.SetVisible("True"); // default value
    container20.VisibleWasExpression = false; // default value
    container20.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container20.Width = "(fill parent)"; // default value
    
    var footerInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    footerInstance.CustomStyle = null; // default value
    footerInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var footer = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Footer");
    footerInstance.SourceBlock = footer;
}

```