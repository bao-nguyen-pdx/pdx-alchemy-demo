```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow account) => {
    
    /*** creating IMobileScreen 'AccountEdit' ***/
    var accountEdit = account.CreateScreen("AccountEdit");
    accountEdit.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = account.GetESpace();
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    accountEdit.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    accountEdit.Roles.Add(orderManager);
    accountEdit.AnonymousAccess = false; // default value
    accountEdit.CustomURL = false; // default value
    accountEdit.Description = null; // default value
    accountEdit.HorizontalPosition = 2285;
    accountEdit.Icon = null; // default value
    accountEdit.IsDisabled = false; // default value
    accountEdit.PageName = null; // default value
    accountEdit.Public = false; // default value
    accountEdit.Shareable = false; // default value
    accountEdit.StyleSheet = null; // default value
    accountEdit.SetTitle("\"Order Management - Account Detail\"");
    accountEdit.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    accountEdit.VerticalPosition = 3428;
    
    var getAccount = accountEdit.CreateScreenAggregate(false, "GetAccount");
    getAccount.CacheInMinutes = null; // default value
    getAccount.Description = null; // default value
    getAccount.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getAccount.SetMaxRecords("1");
    getAccount.SetStartIndex(null); // default value
    getAccount.Timeout = null; // default value
    var country2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Country");
    var country = getAccount.AsDatabaseAggregate.CreateSource(country2);
    var account3 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Account");
    var account2 = getAccount.AsDatabaseAggregate.CreateSource(account3);
    var join = getAccount.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = account2;
    join.RightSource = country;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join.SetCondition("Account.CountryId = Country.Id");
    getAccount.AsDatabaseAggregate.CreateFilter("Account.Id = AccountId");
    
    
    var getCountry = accountEdit.CreateScreenAggregate(false, "GetCountry");
    getCountry.CacheInMinutes = null; // default value
    getCountry.Description = null; // default value
    getCountry.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getCountry.SetMaxRecords("500");
    getCountry.SetStartIndex(null); // default value
    getCountry.Timeout = null; // default value
    var country3 = getCountry.AsDatabaseAggregate.CreateSource(country2);
    getCountry.AsDatabaseAggregate.CreateSort().SetAttribute("Country.Name");
    
    
    var accountId = accountEdit.CreateInputParameter("AccountId");
    accountId.DataType = account3.IdentifierType;
    accountId.Description = null; // default value
    accountId.IsMandatory = true; // default value
    accountEdit.OnDestroy.Destination = null; // default value
    accountEdit.OnReady.Destination = null; // default value
    accountEdit.OnRender.Destination = null; // default value
    
    var onAfterFetch_GetAccount = accountEdit.CreateScreenAction("OnAfterFetch_GetAccount");
    onAfterFetch_GetAccount.Description = null; // default value
    
    var startNode = onAfterFetch_GetAccount.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var onInitialize = accountEdit.CreateScreenAction("OnInitialize");
    onInitialize.Description = null; // default value
    
    var startNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var ifNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode.SetCondition("AccountId <> NullIdentifier()");
    ifNode.HorizontalPosition = 3192;
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Account already exists?";
    ifNode.VerticalPosition = 2508;
    
    var endNode = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1368);
    endNode.IsDisabled = false; // default value
    
    var endNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3196;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 5120;
    accountEdit.OnInitialize.Destination = onInitialize;
    
    var ifNode2 = onAfterFetch_GetAccount.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode2.SetCondition("GetAccount.List.Empty = False ");
    ifNode2.HorizontalPosition = 3192;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Account already exists?";
    ifNode2.VerticalPosition = 2280;
    startNode.Target = ifNode2;
    startNode2.Target = ifNode;
    ifNode.FalseTarget = endNode2;
    ifNode.TrueTarget = endNode;
    
    var account_GenerateCode = onAfterFetch_GetAccount.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Account_GenerateCode").Below(ifNode2, 1368);
    var account_GenerateCode2 = orderManagement_CS.ServerActions.Named("Account_GenerateCode");
    account_GenerateCode.Action = account_GenerateCode2;
    account_GenerateCode.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    account_GenerateCode.IsDisabled = false; // default value
    account_GenerateCode.ServerRequestTimeout = null; // default value
    ifNode2.FalseTarget = account_GenerateCode;
    
    var endNode3 = onAfterFetch_GetAccount.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode2, 1368);
    endNode3.IsDisabled = false; // default value
    ifNode2.TrueTarget = endNode3;
    
    var assignNode = onAfterFetch_GetAccount.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(account_GenerateCode, 1368);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "Code";
    
    var assignment = assignNode.CreateAssignment("GetAccount.List.Current.Account.Code", "Account_GenerateCode.Code");
    
    var endNode4 = onAfterFetch_GetAccount.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode4.HorizontalPosition = 3196;
    endNode4.IsDisabled = false; // default value
    endNode4.VerticalPosition = 6384;
    assignNode.Target = endNode4;
    
    var onChange_Country = accountEdit.CreateScreenAction("OnChange_Country");
    onChange_Country.Description = null; // default value
    
    var selectedOptionList = onChange_Country.CreateInputParameter("SelectedOptionList");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var dropdownOption = outSystemsUI.Structures.Named("DropdownOption");
    selectedOptionList.DataType = eSpace.GetOrCreateListType(dropdownOption);
    selectedOptionList.Description = null; // default value
    selectedOptionList.IsMandatory = true; // default value
    
    var startNode3 = onChange_Country.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.IsDisabled = false; // default value
    
    var assignNode2 = onChange_Country.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode2.HorizontalPosition = 3192;
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = "Selected country";
    assignNode2.VerticalPosition = 2736;
    
    var assignment2 = assignNode2.CreateAssignment("GetAccount.List.Current.Country.Id", "IntegerToIdentifier(TextToInteger(SelectedOptionList.Current.Value))");
    
    var assignment3 = assignNode2.CreateAssignment("GetAccount.List.Current.Country.Name", "SelectedOptionList.Current.Label");
    
    var assignment4 = assignNode2.CreateAssignment("GetAccount.List.Current.Account.CountryId", "IntegerToIdentifier(TextToInteger(SelectedOptionList.Current.Value))");
    startNode3.Target = assignNode2;
    
    var endNode5 = onChange_Country.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode5.HorizontalPosition = 3196;
    endNode5.IsDisabled = false; // default value
    endNode5.VerticalPosition = 5120;
    assignNode2.Target = endNode5;
    
    var onSave = accountEdit.CreateScreenAction("OnSave");
    onSave.Description = null; // default value
    
    var allExceptions = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = true; // default value
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 7545;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 1143;
    
    var messageNode = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>();
    messageNode.HorizontalPosition = 7524;
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("AllExceptions.ExceptionMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    messageNode.VerticalPosition = 2508;
    allExceptions.Target = messageNode;
    
    var endNode6 = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode6.HorizontalPosition = 7545;
    endNode6.IsDisabled = false; // default value
    endNode6.VerticalPosition = 3876;
    messageNode.Target = endNode6;
    
    var startNode4 = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.HorizontalPosition = 2056;
    startNode4.IsDisabled = false; // default value
    startNode4.VerticalPosition = 1256;
    
    var ifNode3 = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode3.SetCondition("Form1.Valid");
    ifNode3.HorizontalPosition = 2031;
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Valid form?";
    ifNode3.VerticalPosition = 2521;
    startNode4.Target = ifNode3;
    
    var input_FocusFirstInvalid = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Input_FocusFirstInvalid");
    var richWidgets = eSpace.References.Named("RichWidgets");
    var input_FocusFirstInvalid2 = richWidgets.ServerActions.Named("Input_FocusFirstInvalid");
    input_FocusFirstInvalid.Action = input_FocusFirstInvalid2;
    input_FocusFirstInvalid.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    input_FocusFirstInvalid.HorizontalPosition = 3623;
    input_FocusFirstInvalid.IsDisabled = false; // default value
    input_FocusFirstInvalid.ServerRequestTimeout = null; // default value
    input_FocusFirstInvalid.VerticalPosition = 2481;
    ifNode3.FalseTarget = input_FocusFirstInvalid;
    
    var account_Save = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Account_Save");
    var account_Save2 = orderManagement_CS.ServerActions.Named("Account_Save");
    account_Save.Action = account_Save2;
    account_Save.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    account_Save.HorizontalPosition = 2057;
    account_Save.IsDisabled = false; // default value
    account_Save.ServerRequestTimeout = null; // default value
    account_Save.VerticalPosition = 4118;
    var invoker = account_Save2.InputParameters.Named("Invoker");
    account_Save.SetArgumentValue(invoker, "GetUserId()");
    var accountRecord = account_Save2.InputParameters.Named("AccountRecord");
    account_Save.SetArgumentValue(accountRecord, "GetAccount.List.Current.Account");
    ifNode3.TrueTarget = account_Save;
    
    var endNode7 = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode7.HorizontalPosition = 4919;
    endNode7.IsDisabled = false; // default value
    endNode7.VerticalPosition = 2521;
    input_FocusFirstInvalid.Target = endNode7;
    
    var messageNode2 = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(account_Save, 1582);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = "Account Created/Updated";
    messageNode2.SetMessage("\"Account \'\" + GetAccount.List.Current.Account.Company + \"\' was successfully \" + If(GetAccount.List.Current.Account.Id = NullIdentifier(), \"created.\", \"updated.\")");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var destinationNode = onSave.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ConnectedBelow(messageNode2, 1596);
    var accountShow = account.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("AccountShow");
    destinationNode.Destination = accountShow;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    var accountId2 = accountShow.InputParameters.Named("AccountId");
    destinationNode.SetArgumentValue(accountId2, "Account_Save.AccountId");
    
    var layoutTopInstance = accountEdit.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    menuInstance.SetArgumentValue(activeMenu, "Entities.MenuItem.Accounts");
    
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
    textWidget.Text = "Back ";
    var builtinEvent = link.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var accounts = account.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Accounts");
    builtinEvent.Destination = accounts;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromLeft;
    
    var placeholderContentWidget4 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
    var newAccount = placeholderContentWidget4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("NewAccount");
    newAccount.Animate = false; // default value
    newAccount.SetCondition("GetAccount.List.Empty");
    newAccount.CustomStyle = null; // default value
    newAccount.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var textWidget2 = newAccount.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget2.CustomStyle = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "New Account";
    
    var expression = newAccount.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetAccount.List.Current.Account.Company");
    expression.Width = ""; // default value
    
    var placeholderContentWidget5 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Actions");
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
    container.Width = "auto";
    
    var link2 = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link2.Widgets.ToList().ForEach(x => x.Delete());
    link2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link2.ColumnGap = null; // default value
    link2.ColumnSpan = null; // default value
    link2.SetConfirmationMessage(null); // default value
    link2.CustomStyle = "font-weight: bold;";
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
    
    var container2 = link2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container2.SetStyle(null); // default value
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "(fill parent)"; // default value
    
    var icon2 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon2.ColumnSpan = null; // default value
    icon2.CustomStyle = null; // default value
    icon2.FlexBasis = null; // default value
    icon2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon2.Icon = "save";
    icon2.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon2.RowSpan = null; // default value
    icon2.SetStyle("\"icon\""); // default value
    icon2.SetVisible("True"); // default value
    icon2.Weight = null; // default value
    
    var textWidget3 = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = "margin-left: 5px;";
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Save";
    var builtinEvent2 = link2.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent2.Destination = onSave;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container3 = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container3.SetStyle(null); // default value
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "auto";
    
    var link3 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link3.Widgets.ToList().ForEach(x => x.Delete());
    link3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link3.ColumnGap = null; // default value
    link3.ColumnSpan = null; // default value
    link3.SetConfirmationMessage(null); // default value
    link3.CustomStyle = "font-weight: bold;";
    link3.SetEnabled("True");
    link3.FlexBasis = null; // default value
    link3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link3.Gap = null;
    link3.GridColumns = null; // default value
    link3.GridRows = null; // default value
    link3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link3.RowGap = null; // default value
    link3.RowSpan = null; // default value
    link3.SetStyle(null); // default value
    link3.SetVisible("True"); // default value
    link3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link3.Width = ""; // default value
    
    var container4 = link3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var textWidget4 = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = "margin-left: 5px;";
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Cancel";
    var builtinEvent3 = link3.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent3.Destination = eSpace.PreviousScreen;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var placeholderContentWidget6 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
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
    container5.SetStyle(null); // default value
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var columns2Instance = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance.CustomStyle = null; // default value
    columns2Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var adaptive = outSystemsUI.MobileFlows.Named("Adaptive");
    var columns2 = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Columns2");
    columns2Instance.SourceBlock = columns2;
    var tabletBehavior = columns2.InputParameters.Named("TabletBehavior");
    columns2Instance.SetArgumentValue(tabletBehavior, "Entities.BreakColumns.All");
    var extendedClass = columns2.InputParameters.Named("ExtendedClass");
    columns2Instance.SetArgumentValue(extendedClass, null); // default value
    var gutterSize = columns2.InputParameters.Named("GutterSize");
    columns2Instance.SetArgumentValue(gutterSize, null);
    var phoneBehavior = columns2.InputParameters.Named("PhoneBehavior");
    columns2Instance.SetArgumentValue(phoneBehavior, "Entities.BreakColumns.All");
    
    var placeholderContentWidget7 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var form1 = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IForm>("Form1");
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
    form1.SetStyle("\"form split-form\"");
    form1.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    form1.Width = "(fill parent)"; // default value
    
    var container6 = form1.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container6.SetStyle("\"font-bold\"");
    container6.SetVisible("True"); // default value
    container6.VisibleWasExpression = false; // default value
    container6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container6.Width = "(fill parent)"; // default value
    
    var textWidget5 = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget5.CustomStyle = "font-size: 16px;";
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Account details";
    
    var container7 = form1.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var columns2Instance2 = container7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance2.CustomStyle = null; // default value
    columns2Instance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    columns2Instance2.SourceBlock = columns2;
    columns2Instance2.SetArgumentValue(gutterSize, null);
    columns2Instance2.SetArgumentValue(extendedClass, null); // default value
    columns2Instance2.SetArgumentValue(tabletBehavior, null);
    columns2Instance2.SetArgumentValue(phoneBehavior, null);
    
    var placeholderContentWidget8 = columns2Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var container8 = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var label = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label.ColumnGap = null; // default value
    label.ColumnSpan = null; // default value
    label.CustomStyle = "margin-bottom: 0px;";
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
    
    var textWidget6 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label.Widgets.First();
    textWidget6.CustomStyle = null; // default value
    textWidget6.Name = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Company Name";
    
    var input_CompanyName = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_CompanyName");
    input_CompanyName.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_CompanyName.ColumnSpan = null; // default value
    input_CompanyName.CustomStyle = "margin-bottom: 0px;";
    input_CompanyName.SetEnabled("True");
    input_CompanyName.FlexBasis = null; // default value
    input_CompanyName.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_CompanyName.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    input_CompanyName.SetMandatory("True");
    input_CompanyName.MaxLength = 100;
    input_CompanyName.SetPrompt(null); // default value
    input_CompanyName.RowSpan = null; // default value
    input_CompanyName.SetStyle("\"form-control\""); // default value
    input_CompanyName.SetVariable("GetAccount.List.Current.Account.Company");
    input_CompanyName.Width = "(fill parent)"; // default value
    var builtinEvent4 = input_CompanyName.OnChange;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = null; // default value
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty = input_CompanyName.CreateExtendedProperty();
    extendedProperty.Property = "tabindex";
    extendedProperty.SetValue("1");
    
    var container9 = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container9.SetStyle("\"margin-top-base\"");
    container9.SetVisible("True"); // default value
    container9.VisibleWasExpression = false; // default value
    container9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container9.Width = "(fill parent)"; // default value
    
    var label2 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label2.ColumnGap = null; // default value
    label2.ColumnSpan = null; // default value
    label2.CustomStyle = "margin-bottom: 0px;";
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
    
    var textWidget7 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label2.Widgets.First();
    textWidget7.CustomStyle = null; // default value
    textWidget7.Name = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Address";
    
    var input_Address = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_Address");
    input_Address.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_Address.ColumnSpan = null; // default value
    input_Address.CustomStyle = "margin-bottom: 0px;";
    input_Address.SetEnabled("True");
    input_Address.FlexBasis = null; // default value
    input_Address.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_Address.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    input_Address.SetMandatory("False");
    input_Address.MaxLength = 250;
    input_Address.SetPrompt(null); // default value
    input_Address.RowSpan = null; // default value
    input_Address.SetStyle("\"form-control\""); // default value
    input_Address.SetVariable("GetAccount.List.Current.Account.Address");
    input_Address.Width = "(fill parent)"; // default value
    var builtinEvent5 = input_Address.OnChange;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = null; // default value
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty2 = input_Address.CreateExtendedProperty();
    extendedProperty2.Property = "tabindex";
    extendedProperty2.SetValue("2");
    
    var container10 = placeholderContentWidget8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container10.SetStyle("\"margin-top-base\"");
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var label3 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label3.ColumnGap = null; // default value
    label3.ColumnSpan = null; // default value
    label3.CustomStyle = "margin-bottom: 0px;";
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
    
    var textWidget8 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label3.Widgets.First();
    textWidget8.CustomStyle = null; // default value
    textWidget8.Name = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Country";
    
    var dropdownSearchInstance = container10.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dropdownSearchInstance.CustomStyle = null; // default value
    dropdownSearchInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var interaction = outSystemsUI.MobileFlows.Named("Interaction");
    var dropdownSearch = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DropdownSearch");
    dropdownSearchInstance.SourceBlock = dropdownSearch;
    var optionalConfigs = dropdownSearch.InputParameters.Named("OptionalConfigs");
    dropdownSearchInstance.SetArgumentValue(optionalConfigs, "{ SearchPrompt: \"Search\" }");
    var optionsList = dropdownSearch.InputParameters.Named("OptionsList");
    dropdownSearchInstance.SetArgumentValue(optionsList, "GetCountry.List mapTo { Value: Country.Id, Label: Country.Name }");
    var extendedClass2 = dropdownSearch.InputParameters.Named("ExtendedClass");
    dropdownSearchInstance.SetArgumentValue(extendedClass2, null); // default value
    var startingSelection = dropdownSearch.InputParameters.Named("StartingSelection");
    dropdownSearchInstance.SetArgumentValue(startingSelection, "[ { Value: GetAccount.List.Current.Country.Id, Label: GetAccount.List.Current.Country.Name } ]");
    var prompt = dropdownSearch.InputParameters.Named("Prompt");
    dropdownSearchInstance.SetArgumentValue(prompt, "\"Select an item\"");
    var eventHandler = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnChanged");
    eventHandler.Handler = onChange_Country;
    eventHandler.SetArgumentValue(selectedOptionList, "SelectedOptionList");
    var eventHandler2 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler2.Handler = null; // default value
    
    var placeholderContentWidget9 = columns2Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container11 = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var label4 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label4.ColumnGap = null; // default value
    label4.ColumnSpan = null; // default value
    label4.CustomStyle = "margin-bottom: 0px;";
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
    
    var textWidget9 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label4.Widgets.First();
    textWidget9.CustomStyle = null; // default value
    textWidget9.Name = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "Code";
    
    var input_Code = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_Code");
    input_Code.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_Code.ColumnSpan = null; // default value
    input_Code.CustomStyle = "margin-bottom: 0px;";
    input_Code.SetEnabled("False");
    input_Code.FlexBasis = null; // default value
    input_Code.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_Code.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    input_Code.SetMandatory("True");
    input_Code.MaxLength = 10;
    input_Code.SetPrompt(null); // default value
    input_Code.RowSpan = null; // default value
    input_Code.SetStyle("\"form-control\""); // default value
    input_Code.SetVariable("GetAccount.List.Current.Account.Code");
    input_Code.Width = "8 col";
    var builtinEvent6 = input_Code.OnChange;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = null; // default value
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container12 = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var label5 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label5.ColumnGap = null; // default value
    label5.ColumnSpan = null; // default value
    label5.CustomStyle = "margin-bottom: 0px;";
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
    label5.Width = "(fill parent)"; // default value
    
    var textWidget10 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label5.Widgets.First();
    textWidget10.CustomStyle = null; // default value
    textWidget10.Name = null; // default value
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Postal Code";
    
    var input_PostalCode = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_PostalCode");
    input_PostalCode.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_PostalCode.ColumnSpan = null; // default value
    input_PostalCode.CustomStyle = "margin-bottom: 0px;";
    input_PostalCode.SetEnabled("True");
    input_PostalCode.FlexBasis = null; // default value
    input_PostalCode.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_PostalCode.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    input_PostalCode.SetMandatory("False");
    input_PostalCode.MaxLength = 50; // default value
    input_PostalCode.SetPrompt(null); // default value
    input_PostalCode.RowSpan = null; // default value
    input_PostalCode.SetStyle("\"form-control\""); // default value
    input_PostalCode.SetVariable("GetAccount.List.Current.Account.PostalCode");
    input_PostalCode.Width = "(fill parent)"; // default value
    var builtinEvent7 = input_PostalCode.OnChange;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = null; // default value
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty3 = input_PostalCode.CreateExtendedProperty();
    extendedProperty3.Property = "tabindex";
    extendedProperty3.SetValue("3");
    
    var columns2Instance3 = container7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columns2Instance3.CustomStyle = null; // default value
    columns2Instance3.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    columns2Instance3.SourceBlock = columns2;
    columns2Instance3.SetArgumentValue(phoneBehavior, null);
    columns2Instance3.SetArgumentValue(gutterSize, null);
    columns2Instance3.SetArgumentValue(extendedClass, null); // default value
    columns2Instance3.SetArgumentValue(tabletBehavior, null);
    
    var placeholderContentWidget10 = columns2Instance3.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var container13 = placeholderContentWidget10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container13.SetStyle("\"margin-top-base\"");
    container13.SetVisible("True"); // default value
    container13.VisibleWasExpression = false; // default value
    container13.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container13.Width = "(fill parent)"; // default value
    
    var label6 = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label6.ColumnGap = null; // default value
    label6.ColumnSpan = null; // default value
    label6.CustomStyle = "margin-bottom: 0px;";
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
    
    var textWidget11 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label6.Widgets.First();
    textWidget11.CustomStyle = null; // default value
    textWidget11.Name = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "Primary Contact";
    
    var input_ContactName = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_ContactName");
    input_ContactName.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_ContactName.ColumnSpan = null; // default value
    input_ContactName.CustomStyle = "margin-bottom: 0px;";
    input_ContactName.SetEnabled("True");
    input_ContactName.FlexBasis = null; // default value
    input_ContactName.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_ContactName.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    input_ContactName.SetMandatory("True");
    input_ContactName.MaxLength = 100;
    input_ContactName.SetPrompt(null); // default value
    input_ContactName.RowSpan = null; // default value
    input_ContactName.SetStyle("\"form-control\""); // default value
    input_ContactName.SetVariable("GetAccount.List.Current.Account.Contact");
    input_ContactName.Width = "(fill parent)"; // default value
    var builtinEvent8 = input_ContactName.OnChange;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = null; // default value
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty4 = input_ContactName.CreateExtendedProperty();
    extendedProperty4.Property = "tabindex";
    extendedProperty4.SetValue("5");
    
    var container14 = placeholderContentWidget10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container14.SetStyle("\"margin-top-base\"");
    container14.SetVisible("True"); // default value
    container14.VisibleWasExpression = false; // default value
    container14.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container14.Width = "(fill parent)"; // default value
    
    var label7 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label7.ColumnGap = null; // default value
    label7.ColumnSpan = null; // default value
    label7.CustomStyle = "margin-bottom: 0px;";
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
    
    var textWidget12 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label7.Widgets.First();
    textWidget12.CustomStyle = null; // default value
    textWidget12.Name = null; // default value
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "Phone";
    
    var input_Phone = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_Phone");
    input_Phone.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_Phone.ColumnSpan = null; // default value
    input_Phone.CustomStyle = "margin-bottom: 0px;";
    input_Phone.SetEnabled("True");
    input_Phone.FlexBasis = null; // default value
    input_Phone.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_Phone.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Phone;
    input_Phone.SetMandatory("False");
    input_Phone.MaxLength = 20;
    input_Phone.SetPrompt(null); // default value
    input_Phone.RowSpan = null; // default value
    input_Phone.SetStyle("\"form-control\""); // default value
    input_Phone.SetVariable("GetAccount.List.Current.Account.Phone");
    input_Phone.Width = "(fill parent)"; // default value
    var builtinEvent9 = input_Phone.OnChange;
    builtinEvent9.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent9.Destination = null; // default value
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty5 = input_Phone.CreateExtendedProperty();
    extendedProperty5.Property = "tabindex";
    extendedProperty5.SetValue("7");
    
    var placeholderContentWidget11 = columns2Instance3.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container15 = placeholderContentWidget11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container15.SetStyle("\"margin-top-base\"");
    container15.SetVisible("True"); // default value
    container15.VisibleWasExpression = false; // default value
    container15.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container15.Width = "(fill parent)"; // default value
    
    var label8 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
    label8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    label8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    label8.ColumnGap = null; // default value
    label8.ColumnSpan = null; // default value
    label8.CustomStyle = "margin-bottom: 0px;";
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
    
    var textWidget13 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label8.Widgets.First();
    textWidget13.CustomStyle = null; // default value
    textWidget13.Name = null; // default value
    textWidget13.SetStyleClasses(null); // default value
    textWidget13.Text = "Email";
    
    var input_Phone2 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_Phone2");
    input_Phone2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_Phone2.ColumnSpan = null; // default value
    input_Phone2.CustomStyle = "margin-bottom: 0px;";
    input_Phone2.SetEnabled("True");
    input_Phone2.FlexBasis = null; // default value
    input_Phone2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_Phone2.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Email;
    input_Phone2.SetMandatory("False");
    input_Phone2.MaxLength = 250;
    input_Phone2.SetPrompt(null); // default value
    input_Phone2.RowSpan = null; // default value
    input_Phone2.SetStyle("\"form-control\""); // default value
    input_Phone2.SetVariable("GetAccount.List.Current.Account.Email");
    input_Phone2.Width = "(fill parent)"; // default value
    var builtinEvent10 = input_Phone2.OnChange;
    builtinEvent10.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent10.Destination = null; // default value
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var extendedProperty6 = input_Phone2.CreateExtendedProperty();
    extendedProperty6.Property = "tabindex";
    extendedProperty6.SetValue("6");
    
    var footerInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    footerInstance.CustomStyle = null; // default value
    footerInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var footer = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("Footer");
    footerInstance.SourceBlock = footer;
}

```