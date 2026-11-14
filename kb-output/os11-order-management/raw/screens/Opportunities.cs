```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow opportunity) => {
    
    /*** creating IMobileScreen 'Opportunities' ***/
    var opportunities = opportunity.CreateScreen("Opportunities");
    opportunities.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = opportunity.GetESpace();
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    opportunities.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    opportunities.Roles.Add(orderManager);
    opportunities.AnonymousAccess = false; // default value
    opportunities.CustomURL = false; // default value
    opportunities.Description = null; // default value
    opportunities.HorizontalPosition = 1142;
    opportunities.Icon = null; // default value
    opportunities.IsDisabled = false; // default value
    opportunities.PageName = null; // default value
    opportunities.Public = false; // default value
    opportunities.Shareable = false; // default value
    opportunities.StyleSheet = null; // default value
    opportunities.SetTitle("\"Order Management - Opportunities\"");
    opportunities.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    opportunities.VerticalPosition = 1142;
    
    var getOpportunities = opportunities.CreateScreenAggregate(false, "GetOpportunities");
    getOpportunities.CacheInMinutes = null; // default value
    getOpportunities.Description = null; // default value
    getOpportunities.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getOpportunities.SetMaxRecords("50"); // default value
    getOpportunities.SetStartIndex(null); // default value
    getOpportunities.Timeout = null; // default value
    var opportunity3 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Opportunity");
    var opportunity2 = getOpportunities.AsDatabaseAggregate.CreateSource(opportunity3);
    var account2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Account");
    var account = getOpportunities.AsDatabaseAggregate.CreateSource(account2);
    var opportunityStage2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IStaticEntitySignature>().Named("OpportunityStage");
    var opportunityStage = getOpportunities.AsDatabaseAggregate.CreateSource(opportunityStage2);
    var join = getOpportunities.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = opportunity2;
    join.RightSource = opportunityStage;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("Opportunity.OpportunityStageId = OpportunityStage.Id");
    var join2 = getOpportunities.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = opportunity2;
    join2.RightSource = account;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join2.SetCondition("Opportunity.AccountId = Account.Id");
    getOpportunities.AsDatabaseAggregate.CreateFilter("Opportunity.IsActive = True");
    getOpportunities.AsDatabaseAggregate.CreateFilter("Opportunity.OpportunityStageId = Entities.OpportunityStage.Prospecting");
    getOpportunities.AsDatabaseAggregate.CreateFilter("Opportunity.Description like \"%\" + SearchKeyword + \"%\" \nor Account.Company like \"%\" + SearchKeyword + \"%\" \nor Opportunity.Name like \"%\" + SearchKeyword + \"%\" \nor OpportunityStage.Label like \"%\" + SearchKeyword + \"%\"\nor SearchKeyword = \"\"");
    getOpportunities.AsDatabaseAggregate.CreateSort().SetAttribute("TableSort");
    
    
    var searchKeyword = opportunities.CreateLocalVariable("SearchKeyword");
    searchKeyword.DataType = eSpace.TextType;
    searchKeyword.SetDefaultValue(null); // default value
    searchKeyword.Description = null; // default value
    
    var tableSort = opportunities.CreateLocalVariable("TableSort");
    tableSort.DataType = eSpace.TextType;
    tableSort.SetDefaultValue("\"{Account}.[Company]\"");
    tableSort.Description = null; // default value
    
    var startIndex = opportunities.CreateLocalVariable("StartIndex");
    startIndex.DataType = eSpace.IntegerType;
    startIndex.SetDefaultValue("0");
    startIndex.Description = null; // default value
    
    var maxRecords = opportunities.CreateLocalVariable("MaxRecords");
    maxRecords.DataType = eSpace.IntegerType;
    maxRecords.SetDefaultValue("20");
    maxRecords.Description = null; // default value
    opportunities.OnDestroy.Destination = null; // default value
    opportunities.OnInitialize.Destination = null; // default value
    opportunities.OnReady.Destination = null; // default value
    opportunities.OnRender.Destination = null; // default value
    
    var onChange_Filters = opportunities.CreateScreenAction("OnChange_Filters");
    onChange_Filters.Description = null; // default value
    
    var newStartIndex = onChange_Filters.CreateInputParameter("NewStartIndex");
    newStartIndex.DataType = eSpace.IntegerType;
    newStartIndex.Description = null; // default value
    newStartIndex.IsMandatory = true; // default value
    
    var startNode = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 1600;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 800;
    
    var assignNode = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode.HorizontalPosition = 1596;
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    assignNode.VerticalPosition = 2285;
    
    var assignment = assignNode.CreateAssignment("StartIndex", "NewStartIndex");
    startNode.Target = assignNode;
    
    var refreshDataNode = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode, 1591);
    refreshDataNode.DataSource = getOpportunities;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords("50");
    refreshDataNode.SetStartIndex(null); // default value
    
    var endNode = onChange_Filters.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 1600;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 5257;
    refreshDataNode.Target = endNode;
    
    var onSort = opportunities.CreateScreenAction("OnSort");
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
    
    var refreshDataNode2 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(assignNode4, 1558);
    refreshDataNode2.DataSource = getOpportunities;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords("50");
    refreshDataNode2.SetStartIndex(null); // default value
    
    var endNode2 = onSort.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode2, 1330);
    endNode2.IsDisabled = false; // default value
    
    var commentNode = onSort.CreateNode<OutSystems.Model.Logic.Nodes.ICommentNode>();
    commentNode.HorizontalPosition = 5856;
    commentNode.IsDisabled = false; // default value
    commentNode.IsReminder = false; // default value
    commentNode.Text = "This brings the pagination back to the first \n page everytime the aggregate is sorted.";
    commentNode.VerticalPosition = 3460;
    commentNode.CreateConnector(assignNode4);
    
    var layoutTopHeaderInstance = opportunities.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    menuInstance.SetArgumentValue(activeMenu, "Entities.MenuItem.Opportunites");
    
    var placeholderContentWidget2 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var textWidget = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "Open Opportunities";
    
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
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
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
    
    var input_TextVar = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_TextVar");
    input_TextVar.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_TextVar.ColumnSpan = null; // default value
    input_TextVar.CustomStyle = "padding: 5px 16px 5px 40px;";
    input_TextVar.SetEnabled("True");
    input_TextVar.FlexBasis = null; // default value
    input_TextVar.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_TextVar.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Search;
    input_TextVar.SetMandatory("False");
    input_TextVar.MaxLength = 50; // default value
    input_TextVar.SetPrompt("\"Type company or opportunity name\"");
    input_TextVar.RowSpan = null; // default value
    input_TextVar.SetStyle("\"form-control search-input\"");
    input_TextVar.SetVariable("SearchKeyword");
    input_TextVar.Width = "(fill parent)"; // default value
    var builtinEvent = input_TextVar.OnChange;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = onChange_Filters;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent.SetArgumentValue(newStartIndex, "0");
    
    var placeholderContentWidget5 = layoutTopHeaderInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var ifWidget = placeholderContentWidget5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.Animate = false; // default value
    ifWidget.SetCondition("GetOpportunities.List.Empty and GetOpportunities.IsDataFetched");
    ifWidget.CustomStyle = null; // default value
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var blankSlateInstance = ifWidget.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    var extendedClass2 = blankSlate.InputParameters.Named("ExtendedClass");
    blankSlateInstance.SetArgumentValue(extendedClass2, null); // default value
    
    var image = placeholderContentWidget6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image.ColumnSpan = null; // default value
    image.CustomStyle = null; // default value
    image.DefaultImage = null; // default value
    image.FlexBasis = null; // default value
    image.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    var opportunitiesIcon = eSpace.Images.Named("OpportunitiesIcon");
    image.Image = opportunitiesIcon;
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
    textWidget2.Text = "There aren\'t any Opportunities to show.";
    
    var table_Wrapper = ifWidget.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("Table_Wrapper");
    table_Wrapper.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    table_Wrapper.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    table_Wrapper.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    table_Wrapper.Animate = false; // default value
    table_Wrapper.ColumnGap = null; // default value
    table_Wrapper.ColumnSpan = null; // default value
    table_Wrapper.CustomStyle = null; // default value
    table_Wrapper.FlexBasis = null; // default value
    table_Wrapper.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    table_Wrapper.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    table_Wrapper.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    table_Wrapper.Gap = null;
    table_Wrapper.GridColumns = null; // default value
    table_Wrapper.GridRows = null; // default value
    table_Wrapper.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    table_Wrapper.RowGap = null; // default value
    table_Wrapper.RowSpan = null; // default value
    table_Wrapper.SetStyle(null); // default value
    table_Wrapper.SetVisible("True"); // default value
    table_Wrapper.VisibleWasExpression = false; // default value
    table_Wrapper.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    table_Wrapper.Width = "(fill parent)"; // default value
    
    var container2 = table_Wrapper.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle("\"margin-top-m\"");
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var tableRecords = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.ITableRecords>();
    tableRecords.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    tableRecords.ColumnSpan = null; // default value
    tableRecords.CustomStyle = null; // default value
    tableRecords.ExpandedInWebEditor = true; // default value
    tableRecords.FlexBasis = null; // default value
    tableRecords.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    tableRecords.RowSpan = null; // default value
    tableRecords.ShowHeader = true; // default value
    tableRecords.SetSource("GetOpportunities.List");
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
    headerCell.SetSortAttribute("Opportunity.Name");
    headerCell.SetSource("GetOpportunities.List");
    headerCell.SetStyle(null); // default value
    headerCell.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget3 = headerCell.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Opportunity";
    
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
    headerCell2.SetSortAttribute("Opportunity.Name");
    headerCell2.SetSource("GetOpportunities.List");
    headerCell2.SetStyle(null); // default value
    headerCell2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget4 = headerCell2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Company";
    
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
    headerCell3.SetSortAttribute("Opportunity.Amount");
    headerCell3.SetSource("GetOpportunities.List");
    headerCell3.SetStyle(null); // default value
    headerCell3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget5 = headerCell3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Amount";
    
    var headerCell4 = tableRecords.HeaderRow.CreateWidget<ServiceStudio.Plugin.NRWidgets.IHeaderCell>();
    headerCell4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    headerCell4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    headerCell4.ColumnGap = null; // default value
    headerCell4.ColumnSpan = null; // default value
    headerCell4.CustomStyle = "text-align: right";
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
    headerCell4.SetSortAttribute("Opportunity.Probability");
    headerCell4.SetSource("GetOpportunities.List");
    headerCell4.SetStyle(null); // default value
    headerCell4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget6 = headerCell4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Probability";
    
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
    headerCell5.SetSortAttribute("Opportunity.CloseOn");
    headerCell5.SetSource("GetOpportunities.List");
    headerCell5.SetStyle(null); // default value
    headerCell5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget7 = headerCell5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Close Date";
    
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
    expression.SetValue("GetOpportunities.List.Current.Opportunity.Name");
    expression.Width = ""; // default value
    var builtinEvent2 = link.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var opportunityDetail = opportunity.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("OpportunityDetail");
    builtinEvent2.Destination = opportunityDetail;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromRight;
    var opportunityId = opportunityDetail.InputParameters.Named("OpportunityId");
    builtinEvent2.SetArgumentValue(opportunityId, "GetOpportunities.List.Current.Opportunity.Id");
    
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
    expression2.SetValue("GetOpportunities.List.Current.Account.Company");
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
    
    var container3 = rowCell3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container3.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Right;
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
    container3.SetStyle(null); // default value
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var expression3 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("FormatCurrency(GetOpportunities.List.Current.Opportunity.Amount, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
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
    
    var container4 = rowCell4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression4 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("FormatDecimal(GetOpportunities.List.Current.Opportunity.Probability, 2, \".\", \",\")");
    expression4.Width = ""; // default value
    
    var rowCell5 = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
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
    
    var expression5 = rowCell5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("FormatDateTime(GetOpportunities.List.Current.Opportunity.CloseOn, \"d MMM yyyy\")");
    expression5.Width = ""; // default value
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
    
    var container5 = table_Wrapper.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container5.Animate = false; // default value
    container5.ColumnGap = null; // default value
    container5.ColumnSpan = null; // default value
    container5.CustomStyle = "margin-top: 20px;";
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
    container5.Width = "(fill parent)"; // default value
    
    var paginationInstance = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    var maxRecords2 = pagination.InputParameters.Named("MaxRecords");
    paginationInstance.SetArgumentValue(maxRecords2, "MaxRecords");
    var startIndex2 = pagination.InputParameters.Named("StartIndex");
    paginationInstance.SetArgumentValue(startIndex2, "StartIndex");
    var showGoToPage = pagination.InputParameters.Named("ShowGoToPage");
    paginationInstance.SetArgumentValue(showGoToPage, null);
    var totalCount = pagination.InputParameters.Named("TotalCount");
    paginationInstance.SetArgumentValue(totalCount, "GetOpportunities.Count");
    var extendedClass3 = pagination.InputParameters.Named("ExtendedClass");
    paginationInstance.SetArgumentValue(extendedClass3, null); // default value
    var eventHandler2 = paginationInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnNavigate");
    eventHandler2.Handler = onChange_Filters;
    eventHandler2.SetArgumentValue(newStartIndex, "NewStartIndex");
    
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