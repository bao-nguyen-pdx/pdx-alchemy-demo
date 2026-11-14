```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow dashboard) => {
    
    /*** creating IMobileScreen 'Dashboard' ***/
    var dashboard2 = dashboard.CreateScreen("Dashboard");
    dashboard2.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = dashboard.GetESpace();
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    dashboard2.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    dashboard2.Roles.Add(orderManager);
    dashboard2.AnonymousAccess = false; // default value
    dashboard2.CustomURL = false; // default value
    dashboard2.Description = "Dashboard";
    dashboard2.HorizontalPosition = 685;
    dashboard2.Icon = null; // default value
    dashboard2.IsDisabled = false; // default value
    dashboard2.PageName = null; // default value
    dashboard2.Public = false; // default value
    dashboard2.Shareable = false; // default value
    dashboard2.StyleSheet = null; // default value
    dashboard2.SetTitle("\"Order Management - Dashboard\"");
    dashboard2.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    dashboard2.VerticalPosition = 685;
    
    var getUser = dashboard2.CreateScreenAggregate(false, "GetUser");
    getUser.CacheInMinutes = null; // default value
    getUser.Description = null; // default value
    getUser.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getUser.SetMaxRecords("1");
    getUser.SetStartIndex(null); // default value
    getUser.Timeout = null; // default value
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getUser.AsDatabaseAggregate.CreateSource(user2);
    getUser.AsDatabaseAggregate.CreateFilter("User.Is_Active = True");
    getUser.AsDatabaseAggregate.CreateFilter("User.Id = GetUserId()");
    
    
    var getDashboardByCategory = dashboard2.CreateDataAction("GetDashboardByCategory");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getDashboardByCategory.OutputParameters.ToList().ForEach(x => x.Delete());
    getDashboardByCategory.Description = null; // default value
    getDashboardByCategory.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getDashboardByCategory.ServerRequestTimeout = null; // default value
    
    var startNode = getDashboardByCategory.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var dashboard_GetByCategory = getDashboardByCategory.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Dashboard_GetByCategory");
    var dashboard_GetByCategory2 = orderManagement_CS.ServerActions.Named("Dashboard_GetByCategory");
    dashboard_GetByCategory.Action = dashboard_GetByCategory2;
    dashboard_GetByCategory.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    dashboard_GetByCategory.HorizontalPosition = 3199;
    dashboard_GetByCategory.IsDisabled = false; // default value
    dashboard_GetByCategory.ServerRequestTimeout = null; // default value
    dashboard_GetByCategory.VerticalPosition = 2514;
    startNode.Target = dashboard_GetByCategory;
    
    var assignNode = getDashboardByCategory.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode.HorizontalPosition = 3192;
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    assignNode.VerticalPosition = 3876;
    
    var assignment = assignNode.CreateAssignment("ChartData", "Dashboard_GetByCategory.ChartData mapTo { Legacy_DataPoint.Label: DataPoint2.Label, Legacy_DataPoint.Value: DataPoint2.Value, Legacy_DataPoint.SeriesName: DataPoint2.DataSeriesName, Legacy_DataPoint.Tooltip: DataPoint2.Tooltip, Legacy_DataPoint.Color: DataPoint2.Color }");
    dashboard_GetByCategory.Target = assignNode;
    
    var endNode = getDashboardByCategory.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 3196;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 5348;
    assignNode.Target = endNode;
    getDashboardByCategory.OnAfterFetch.Destination = null; // default value
    
    var chartData = getDashboardByCategory.CreateOutputParameter("ChartData");
    var outSystemsCharts = eSpace.References.Named("OutSystemsCharts");
    var legacy_DataPoint = outSystemsCharts.Structures.Named("Legacy_DataPoint");
    chartData.DataType = eSpace.GetOrCreateListType(eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("Legacy_DataPoint", legacy_DataPoint)));
    chartData.Description = null; // default value
    
    var getDashboardHistory = dashboard2.CreateDataAction("GetDashboardHistory");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getDashboardHistory.OutputParameters.ToList().ForEach(x => x.Delete());
    getDashboardHistory.Description = null; // default value
    getDashboardHistory.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getDashboardHistory.ServerRequestTimeout = null; // default value
    
    var startNode2 = getDashboardHistory.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var dashboard_GetHistory = getDashboardHistory.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Dashboard_GetHistory");
    var dashboard_GetHistory2 = orderManagement_CS.ServerActions.Named("Dashboard_GetHistory");
    dashboard_GetHistory.Action = dashboard_GetHistory2;
    dashboard_GetHistory.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    dashboard_GetHistory.HorizontalPosition = 3192;
    dashboard_GetHistory.IsDisabled = false; // default value
    dashboard_GetHistory.ServerRequestTimeout = null; // default value
    dashboard_GetHistory.VerticalPosition = 2280;
    startNode2.Target = dashboard_GetHistory;
    
    var assignNode2 = getDashboardHistory.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode2.HorizontalPosition = 3199;
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "ChartData/Goals";
    assignNode2.VerticalPosition = 3667;
    
    var assignment2 = assignNode2.CreateAssignment("HistoryChartData_Out", "Dashboard_GetHistory.ChartData mapTo { Legacy_DataPoint.Label: ToUpper(FormatDateTime(TextToDate(DataPoint2.Label), \"MMM\")), Legacy_DataPoint.Value: DataPoint2.Value, Legacy_DataPoint.SeriesName: DataPoint2.DataSeriesName, Legacy_DataPoint.Tooltip: DataPoint2.Tooltip }");
    dashboard_GetHistory.Target = assignNode2;
    
    var endNode2 = getDashboardHistory.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3196;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 5039;
    assignNode2.Target = endNode2;
    getDashboardHistory.OnAfterFetch.Destination = null; // default value
    
    var historyChartData_Out = getDashboardHistory.CreateOutputParameter("HistoryChartData_Out");
    historyChartData_Out.DataType = eSpace.GetOrCreateListType(eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("Legacy_DataPoint", legacy_DataPoint)));
    historyChartData_Out.Description = null; // default value
    
    var actualTarget = dashboard2.CreateLocalVariable("ActualTarget");
    actualTarget.DataType = eSpace.CurrencyType;
    actualTarget.SetDefaultValue(null); // default value
    actualTarget.Description = null; // default value
    
    var personalYTD = dashboard2.CreateLocalVariable("PersonalYTD");
    personalYTD.DataType = eSpace.CurrencyType;
    personalYTD.SetDefaultValue(null); // default value
    personalYTD.Description = null; // default value
    
    var personalTarget = dashboard2.CreateLocalVariable("PersonalTarget");
    personalTarget.DataType = eSpace.CurrencyType;
    personalTarget.SetDefaultValue(null); // default value
    personalTarget.Description = null; // default value
    
    var teamYTD = dashboard2.CreateLocalVariable("TeamYTD");
    teamYTD.DataType = eSpace.CurrencyType;
    teamYTD.SetDefaultValue(null); // default value
    teamYTD.Description = null; // default value
    
    var teamTarget = dashboard2.CreateLocalVariable("TeamTarget");
    teamTarget.DataType = eSpace.CurrencyType;
    teamTarget.SetDefaultValue(null); // default value
    teamTarget.Description = null; // default value
    dashboard2.OnDestroy.Destination = null; // default value
    dashboard2.OnReady.Destination = null; // default value
    dashboard2.OnRender.Destination = null; // default value
    
    var onInitialize = dashboard2.CreateScreenAction("OnInitialize");
    onInitialize.Description = null; // default value
    
    var startNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.IsDisabled = false; // default value
    dashboard2.OnInitialize.Destination = onInitialize;
    
    var ifNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode.SetCondition("Client.IsOrderManager");
    ifNode.HorizontalPosition = 3192;
    ifNode.IsDisabled = false; // default value
    ifNode.Label = null; // default value
    ifNode.VerticalPosition = 2508;
    startNode3.Target = ifNode;
    
    var assignNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode, 1596);
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "Targets/YTD";
    
    var assignment3 = assignNode3.CreateAssignment("ActualTarget", "Client.TargetValue");
    
    var assignment4 = assignNode3.CreateAssignment("PersonalTarget", "Client.PersonalGoal");
    
    var assignment5 = assignNode3.CreateAssignment("TeamTarget", "ActualTarget * (IntegerToDecimal(Client.TeamGoal) / 100)");
    
    var assignment6 = assignNode3.CreateAssignment("TeamYTD", "TeamTarget * ((Month(CurrDate()) - 0.1) / 12)");
    
    var assignment7 = assignNode3.CreateAssignment("PersonalYTD", "PersonalTarget * ((Month(CurrDate()) - 0.2) / 12)");
    ifNode.FalseTarget = assignNode3;
    
    var destinationNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode, 1824);
    var dashboardManager = dashboard.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("DashboardManager");
    destinationNode.Destination = dashboardManager;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    ifNode.TrueTarget = destinationNode;
    
    var endNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode3.HorizontalPosition = 3199;
    endNode3.IsDisabled = false; // default value
    endNode3.VerticalPosition = 5714;
    assignNode3.Target = endNode3;
    
    var layoutTopHeaderInstance = dashboard2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutTopHeaderInstance.CustomStyle = null; // default value
    layoutTopHeaderInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var common = eSpace.MobileFlows.Named("Common");
    var layoutTopHeader = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutTopHeader");
    layoutTopHeaderInstance.SourceBlock = layoutTopHeader;
    // delete default content in Header placeholder
    var placeholderContentWidget = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    placeholderContentWidget.Widgets.ToList().ForEach(w => w.Delete());
    var hasHeaderImage = layoutTopHeader.InputParameters.Named("HasHeaderImage");
    layoutTopHeaderInstance.SetArgumentValue(hasHeaderImage, "False");
    var hasFixedHeader = layoutTopHeader.InputParameters.Named("HasFixedHeader");
    layoutTopHeaderInstance.SetArgumentValue(hasFixedHeader, "True");
    
    var menuInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    menuInstance.CustomStyle = null; // default value
    menuInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var menu = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Menu");
    menuInstance.SourceBlock = menu;
    var activeMenu = menu.InputParameters.Named("ActiveMenu");
    menuInstance.SetArgumentValue(activeMenu, "Entities.MenuItem.Dashboard");
    
    var placeholderContentWidget2 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Dashboard");
    var container = placeholderContentWidget2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.Width = "(fill parent)"; // default value
    
    var container2 = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container2.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container2.Animate = false; // default value
    container2.ColumnGap = null; // default value
    container2.ColumnSpan = null; // default value
    container2.CustomStyle = "font-size: 18px;";
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
    
    var textWidget = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Welcome, ";
    
    var expression = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = "John Doe";
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle("\"bold\"");
    expression.SetValue("GetUser.List.Current.User.Name");
    expression.Width = ""; // default value
    
    var container3 = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container3.Animate = false; // default value
    container3.ColumnGap = null; // default value
    container3.ColumnSpan = null; // default value
    container3.CustomStyle = "margin-top: 16px;";
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
    container3.SetStyle(null); // default value
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var columns2Instance = container3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance.CustomStyle = null; // default value
    columns2Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var adaptive = outSystemsUI.MobileFlows.Named("Adaptive");
    var columns2 = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Columns2");
    columns2Instance.SourceBlock = columns2;
    var phoneBehavior = columns2.InputParameters.Named("PhoneBehavior");
    columns2Instance.SetArgumentValue(phoneBehavior, "Entities.BreakColumns.All");
    var gutterSize = columns2.InputParameters.Named("GutterSize");
    columns2Instance.SetArgumentValue(gutterSize, null);
    var tabletBehavior = columns2.InputParameters.Named("TabletBehavior");
    columns2Instance.SetArgumentValue(tabletBehavior, null);
    var extendedClass = columns2.InputParameters.Named("ExtendedClass");
    columns2Instance.SetArgumentValue(extendedClass, "\"dashboard-columns\"");
    
    var placeholderContentWidget3 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container4 = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.Width = "(fill parent)"; // default value
    
    var button = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
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
    button.SetStyle("\"btn btn-large text-primary\"");
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
    var order = eSpace.MobileFlows.Named("Order");
    var orderDetail = order.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("OrderDetail");
    builtinEvent.Destination = orderDetail;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromRight;
    var accountId = orderDetail.InputParameters.Named("AccountId");
    builtinEvent.SetArgumentValue(accountId, null); // default value
    var orderId = orderDetail.InputParameters.Named("OrderId");
    builtinEvent.SetArgumentValue(orderId, "NullIdentifier()");
    var opportunityId = orderDetail.InputParameters.Named("OpportunityId");
    builtinEvent.SetArgumentValue(opportunityId, null); // default value
    
    var container5 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container5.SetStyle("\"margin-top-base charts-container\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var container6 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container6.SetStyle("\"only-charts\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "10col";
    
    var columns2Instance2 = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance2.CustomStyle = null; // default value
    columns2Instance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    columns2Instance2.SourceBlock = columns2;
    columns2Instance2.SetArgumentValue(tabletBehavior, "Entities.BreakColumns.All");
    columns2Instance2.SetArgumentValue(phoneBehavior, "Entities.BreakColumns.All");
    columns2Instance2.SetArgumentValue(extendedClass, null); // default value
    columns2Instance2.SetArgumentValue(gutterSize, "Entities.GutterSize.Large");
    
    var placeholderContentWidget4 = columns2Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var container7 = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container7.SetStyle("\"dash-card\"");
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "(fill parent)"; // default value
    
    var container8 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container8.SetStyle(null); // default value
    container8.SetVisible("True"); // default value
    container8.VisibleWasExpression = false; // default value
    container8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container8.Width = "(fill parent)"; // default value
    
    var textWidget3 = container8.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses("\"font-semi-bold\"");
    textWidget3.Text = "Order Sales";
    
    var container9 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container9.Width = "(fill parent)"; // default value
    
    var areaChart_v1Instance = container9.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    areaChart_v1Instance.CustomStyle = null; // default value
    areaChart_v1Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var version1 = outSystemsCharts.MobileFlows.Named("Version1");
    var areaChart_v1 = version1.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("AreaChart_v1");
    areaChart_v1Instance.SourceBlock = areaChart_v1;
    var height = areaChart_v1.InputParameters.Named("Height");
    areaChart_v1Instance.SetArgumentValue(height, null);
    var yAxisFormat = areaChart_v1.InputParameters.Named("YAxisFormat");
    areaChart_v1Instance.SetArgumentValue(yAxisFormat, "YAxisFormat_Init_v1(ValuesPrefix:Client.Currency)");
    var xAxisFormat = areaChart_v1.InputParameters.Named("XAxisFormat");
    areaChart_v1Instance.SetArgumentValue(xAxisFormat, "XAxisFormat_Init_v1(Title:,LabelsRotation:,MinValue:,MaxValue:,ValuesType:)");
    var legendPositionId = areaChart_v1.InputParameters.Named("LegendPositionId");
    areaChart_v1Instance.SetArgumentValue(legendPositionId, null); // default value
    var advancedFormat = areaChart_v1.InputParameters.Named("AdvancedFormat");
    areaChart_v1Instance.SetArgumentValue(advancedFormat, "AdvancedFormat_Init_v1(DataPointFormats:,DataSeriesFormats:,\n        XAxisJSON:\"labels:{\n                        style:{\n                            color: \'#fff\'\n                        },\n                    autoRotationLimit: 0\n                    },\n                    lineWidth:0,\n                    gridLineWidth: 0,\n                    tickWidth: 0,\"\n        ,YAxisJSON:\"labels:{\n                    style:{\n                        color: \'#fff\'\n                    }\n                },\n                lineWidth: 0,\n                gridLineWidth: 0\",\n\"plotOptions:{\n    series:{\n        showInLegend:false,\n            color: {\n                linearGradient: { x1: 0, x2: 0, y1: 0, y2: 1 },\n                stops: [\n                        [0, \'rgba(255,255,255,0.32)\'], // start\n                        [1, \'rgba(255,255,255,0)\'] // end\n                    ]\n            },\n            lineColor: \'#00E6AC\',\n            marker:{\n                fillColor:\'#ffffff\',\n                lineWidth: 2,\n                lineColor:\'#00E6AC\'\n            }\n    }\n}\")");
    var sourceDataPointList = areaChart_v1.InputParameters.Named("SourceDataPointList");
    areaChart_v1Instance.SetArgumentValue(sourceDataPointList, "GetDashboardHistory.HistoryChartData_Out mapTo {  }");
    var stackingType = areaChart_v1.InputParameters.Named("StackingType");
    areaChart_v1Instance.SetArgumentValue(stackingType, null);
    var clickable = areaChart_v1.InputParameters.Named("Clickable");
    areaChart_v1Instance.SetArgumentValue(clickable, null);
    var chartFormat = areaChart_v1.InputParameters.Named("ChartFormat");
    areaChart_v1Instance.SetArgumentValue(chartFormat, null); // default value
    var eventHandler = areaChart_v1Instance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnClick");
    eventHandler.Handler = null; // default value
    
    var extendedProperty = container9.CreateExtendedProperty();
    extendedProperty.Property = "class";
    extendedProperty.SetValue("\"margin-top-s line-chart\"");
    
    var placeholderContentWidget5 = columns2Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container10 = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container10.SetStyle("\"dash-card\"");
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
    container11.SetStyle(null); // default value
    container11.SetVisible("True"); // default value
    container11.VisibleWasExpression = false; // default value
    container11.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container11.Width = "(fill parent)"; // default value
    
    var textWidget4 = container11.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses("\"font-semi-bold\"");
    textWidget4.Text = "Top 5 Categories";
    
    var container12 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container12.SetStyle("\"margin-top-s column-chart\"");
    container12.SetVisible("True"); // default value
    container12.VisibleWasExpression = false; // default value
    container12.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container12.Width = "(fill parent)"; // default value
    
    var columnChart_v1Instance = container12.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columnChart_v1Instance.CustomStyle = null; // default value
    columnChart_v1Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var columnChart_v1 = version1.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ColumnChart_v1");
    columnChart_v1Instance.SourceBlock = columnChart_v1;
    var xAxisFormat2 = columnChart_v1.InputParameters.Named("XAxisFormat");
    columnChart_v1Instance.SetArgumentValue(xAxisFormat2, null); // default value
    var legendPositionId2 = columnChart_v1.InputParameters.Named("LegendPositionId");
    columnChart_v1Instance.SetArgumentValue(legendPositionId2, null); // default value
    var yAxisFormat2 = columnChart_v1.InputParameters.Named("YAxisFormat");
    columnChart_v1Instance.SetArgumentValue(yAxisFormat2, "YAxisFormat_Init_v1(ValuesPrefix:Client.Currency)");
    var stackingType2 = columnChart_v1.InputParameters.Named("StackingType");
    columnChart_v1Instance.SetArgumentValue(stackingType2, null);
    var advancedFormat2 = columnChart_v1.InputParameters.Named("AdvancedFormat");
    columnChart_v1Instance.SetArgumentValue(advancedFormat2, "AdvancedFormat_Init_v1(DataPointFormats:,DataSeriesFormats:,XAxisJSON:\n\"labels:{\n    style:{\n            color: \'#fff\',\n            fontSize: \'10px\'\n    },\n    autoRotationLimit: 0\n    },\n gridLineWidth: 0,\n tickWidth: 0\"\n\n,YAxisJSON:\n\"labels:{\n    style:{\n            color: \'#fff\',\n            fontSize: \'10px\'\n        }\n    },\nlineWidth: 0,\n gridLineWidth: 0\"\n\n,\"plotOptions:{\n    series:{\n        showInLegend:false,\n        borderRadiusTopLeft: 5,\n        borderRadiusTopRight: 5,\n        color: {\n            linearGradient: { x1: 0, x2: 0, y1: 0, y2: 1 },\n            stops: [\n                [0, \'#00E6AC\'], // start\n                [1, \'rgba(0,230,172,0.5)\'] // end\n            ]\n        }\n    }\n}\")");
    var chartFormat2 = columnChart_v1.InputParameters.Named("ChartFormat");
    columnChart_v1Instance.SetArgumentValue(chartFormat2, null); // default value
    var clickable2 = columnChart_v1.InputParameters.Named("Clickable");
    columnChart_v1Instance.SetArgumentValue(clickable2, null);
    var sourceDataPointList2 = columnChart_v1.InputParameters.Named("SourceDataPointList");
    columnChart_v1Instance.SetArgumentValue(sourceDataPointList2, "GetDashboardByCategory.ChartData mapTo {  }");
    var height2 = columnChart_v1.InputParameters.Named("Height");
    columnChart_v1Instance.SetArgumentValue(height2, null);
    var eventHandler2 = columnChart_v1Instance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnClick");
    eventHandler2.Handler = null; // default value
    
    var container13 = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container13.SetStyle("\"goal-container\"");
    container13.SetVisible("True"); // default value
    container13.VisibleWasExpression = false; // default value
    container13.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container13.Width = "2 col";
    
    var container14 = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container14.SetStyle("\"dash-card\"");
    container14.SetVisible("True"); // default value
    container14.VisibleWasExpression = false; // default value
    container14.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container14.Width = "(fill parent)"; // default value
    
    var dashboard_GoalProgressInstance = container14.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dashboard_GoalProgressInstance.CustomStyle = null; // default value
    dashboard_GoalProgressInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dashboard_GoalProgress = dashboard.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Dashboard_GoalProgress");
    dashboard_GoalProgressInstance.SourceBlock = dashboard_GoalProgress;
    var current = dashboard_GoalProgress.InputParameters.Named("Current");
    dashboard_GoalProgressInstance.SetArgumentValue(current, "PersonalYTD");
    var goal = dashboard_GoalProgress.InputParameters.Named("Goal");
    dashboard_GoalProgressInstance.SetArgumentValue(goal, "PersonalTarget");
    var title = dashboard_GoalProgress.InputParameters.Named("Title");
    dashboard_GoalProgressInstance.SetArgumentValue(title, "\"Personal Goal\"");
    
    var container15 = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container15.SetStyle("\"dash-card\"");
    container15.SetVisible("True"); // default value
    container15.VisibleWasExpression = false; // default value
    container15.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container15.Width = "(fill parent)"; // default value
    
    var dashboard_GoalProgressInstance2 = container15.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dashboard_GoalProgressInstance2.CustomStyle = null; // default value
    dashboard_GoalProgressInstance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    dashboard_GoalProgressInstance2.SourceBlock = dashboard_GoalProgress;
    dashboard_GoalProgressInstance2.SetArgumentValue(current, "TeamYTD");
    dashboard_GoalProgressInstance2.SetArgumentValue(goal, "TeamTarget");
    dashboard_GoalProgressInstance2.SetArgumentValue(title, "\"Team Goal\"");
    
    var placeholderContentWidget6 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var container16 = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container16.SetStyle(null); // default value
    container16.SetVisible("True"); // default value
    container16.VisibleWasExpression = false; // default value
    container16.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container16.Width = "(fill parent)"; // default value
    
    var container17 = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container17.SetStyle(null); // default value
    container17.SetVisible("True"); // default value
    container17.VisibleWasExpression = false; // default value
    container17.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container17.Width = "(fill parent)"; // default value
    
    var columnsMediumRightInstance = container17.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columnsMediumRightInstance.CustomStyle = null; // default value
    columnsMediumRightInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var columnsMediumRight = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ColumnsMediumRight");
    columnsMediumRightInstance.SourceBlock = columnsMediumRight;
    var tabletBehavior2 = columnsMediumRight.InputParameters.Named("TabletBehavior");
    columnsMediumRightInstance.SetArgumentValue(tabletBehavior2, "Entities.BreakColumns.Middle");
    var extendedClass2 = columnsMediumRight.InputParameters.Named("ExtendedClass");
    columnsMediumRightInstance.SetArgumentValue(extendedClass2, null); // default value
    var gutterSize2 = columnsMediumRight.InputParameters.Named("GutterSize");
    columnsMediumRightInstance.SetArgumentValue(gutterSize2, "Entities.GutterSize.ExtraLarge");
    var phoneBehavior2 = columnsMediumRight.InputParameters.Named("PhoneBehavior");
    columnsMediumRightInstance.SetArgumentValue(phoneBehavior2, "Entities.BreakColumns.Middle");
    
    var placeholderContentWidget7 = columnsMediumRightInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var container18 = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container18.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
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
    
    var textWidget5 = container18.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses("\"heading3\"");
    textWidget5.Text = "Orders";
    
    var dashboard_OrdersBySalesRepresentativeInstance = placeholderContentWidget7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dashboard_OrdersBySalesRepresentativeInstance.CustomStyle = null; // default value
    dashboard_OrdersBySalesRepresentativeInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dashboard_OrdersBySalesRepresentative = dashboard.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Dashboard_OrdersBySalesRepresentative");
    dashboard_OrdersBySalesRepresentativeInstance.SourceBlock = dashboard_OrdersBySalesRepresentative;
    
    var placeholderContentWidget8 = columnsMediumRightInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container19 = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container19.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container19.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container19.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container19.Animate = false; // default value
    container19.ColumnGap = null; // default value
    container19.ColumnSpan = null; // default value
    container19.CustomStyle = null; // default value
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
    
    var textWidget6 = container19.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses("\"heading3\"");
    textWidget6.Text = "Top performers";
    
    var container20 = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container20.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container20.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container20.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container20.Animate = false; // default value
    container20.ColumnGap = null; // default value
    container20.ColumnSpan = null; // default value
    container20.CustomStyle = "margin-top: 15px;";
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
    container20.SetStyle(null); // default value
    container20.SetVisible("True"); // default value
    container20.VisibleWasExpression = false; // default value
    container20.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container20.Width = "(fill parent)"; // default value
    
    var dashboard_TopPerformersInstance = container20.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dashboard_TopPerformersInstance.CustomStyle = null; // default value
    dashboard_TopPerformersInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dashboard_TopPerformers = dashboard.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Dashboard_TopPerformers");
    dashboard_TopPerformersInstance.SourceBlock = dashboard_TopPerformers;
    
    var container21 = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container21.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container21.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container21.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container21.Animate = false; // default value
    container21.ColumnGap = null; // default value
    container21.ColumnSpan = null; // default value
    container21.CustomStyle = null; // default value
    container21.FlexBasis = null; // default value
    container21.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container21.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container21.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container21.Gap = null;
    container21.GridColumns = null; // default value
    container21.GridRows = null; // default value
    container21.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container21.RowGap = null; // default value
    container21.RowSpan = null; // default value
    container21.SetStyle("\"margin-top-m\"");
    container21.SetVisible("True"); // default value
    container21.VisibleWasExpression = false; // default value
    container21.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container21.Width = "(fill parent)"; // default value
    
    var textWidget7 = container21.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses("\"heading3\"");
    textWidget7.Text = "Top products";
    
    var container22 = container21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container22.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container22.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container22.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container22.Animate = false; // default value
    container22.ColumnGap = null; // default value
    container22.ColumnSpan = null; // default value
    container22.CustomStyle = null; // default value
    container22.FlexBasis = null; // default value
    container22.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container22.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container22.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container22.Gap = null;
    container22.GridColumns = null; // default value
    container22.GridRows = null; // default value
    container22.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container22.RowGap = null; // default value
    container22.RowSpan = null; // default value
    container22.SetStyle("\"margin-top-base\"");
    container22.SetVisible("True"); // default value
    container22.VisibleWasExpression = false; // default value
    container22.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container22.Width = "(fill parent)"; // default value
    
    var dashboard_TopProductsInstance = container22.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dashboard_TopProductsInstance.CustomStyle = null; // default value
    dashboard_TopProductsInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dashboard_TopProducts = dashboard.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Dashboard_TopProducts");
    dashboard_TopProductsInstance.SourceBlock = dashboard_TopProducts;
    
    var placeholderContentWidget9 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Footer");
    var footerInstance = placeholderContentWidget9.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    footerInstance.CustomStyle = null; // default value
    footerInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var footer = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Footer");
    footerInstance.SourceBlock = footer;
    
    var resizeChartInstance = placeholderContentWidget9.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    resizeChartInstance.CustomStyle = null; // default value
    resizeChartInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var utils = eSpace.MobileFlows.Named("Utils");
    var resizeChart = utils.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("ResizeChart");
    resizeChartInstance.SourceBlock = resizeChart;
}

```