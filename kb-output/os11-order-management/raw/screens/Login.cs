```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow common) => {
    
    /*** creating IMobileScreen 'Login' ***/
    var login = common.CreateScreen("Login");
    login.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = common.GetESpace();
    var anonymous = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Anonymous");
    login.Roles.Add(anonymous);
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    login.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    login.Roles.Add(orderManager);
    var registered = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Registered");
    login.Roles.Add(registered);
    login.AnonymousAccess = false; // default value
    login.CustomURL = false; // default value
    login.Description = "Screen where users login";
    login.HorizontalPosition = 685;
    login.Icon = null; // default value
    login.IsDisabled = false; // default value
    login.PageName = null; // default value
    login.Public = false; // default value
    login.Shareable = false; // default value
    login.StyleSheet = "html{\n    overflow: auto;\n}\n\nbody,\nbody .content{\n    background: linear-gradient(227.66deg, #C6CAE4 0%, #C8CCE5 15%, #E6E8F3 30%, #F8F9FC 40%, transparent 50%) no-repeat right;\n    background-size: 100vh 100vh;\n}\n\n";
    login.SetTitle("\"Login\"");
    login.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    login.VerticalPosition = 686;
    
    var getSampleUsers = login.CreateDataAction("GetSampleUsers");
    /*** Deleting OutputParameters that are created by default when instantiating an IDataAction ***/
    getSampleUsers.OutputParameters.ToList().ForEach(x => x.Delete());
    getSampleUsers.Description = null; // default value
    getSampleUsers.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getSampleUsers.ServerRequestTimeout = null; // default value
    
    var startNode = getSampleUsers.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.HorizontalPosition = 2742;
    startNode.IsDisabled = false; // default value
    startNode.VerticalPosition = 457;
    
    var sampleUsers_Get = getSampleUsers.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("SampleUsers_Get");
    var sampleUsers_Get2 = eSpace.ServerActions.Named("SampleUsers_Get");
    sampleUsers_Get.Action = sampleUsers_Get2;
    sampleUsers_Get.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    sampleUsers_Get.HorizontalPosition = 2736;
    sampleUsers_Get.IsDisabled = false; // default value
    sampleUsers_Get.ServerRequestTimeout = null; // default value
    sampleUsers_Get.VerticalPosition = 2057;
    startNode.Target = sampleUsers_Get;
    
    var assignNode = getSampleUsers.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(sampleUsers_Get, 1591);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("UserList", "SampleUsers_Get.UserList");
    
    var endNode = getSampleUsers.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode.HorizontalPosition = 2742;
    endNode.IsDisabled = false; // default value
    endNode.VerticalPosition = 5472;
    assignNode.Target = endNode;
    getSampleUsers.OnAfterFetch.Destination = null; // default value
    
    var userList = getSampleUsers.CreateOutputParameter("UserList");
    var _System_ = eSpace.References.Named("(System)");
    var user = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    userList.DataType = eSpace.GetOrCreateListType(eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("Role", eSpace.TextType), Tuple.Create<string, ITypeSignature>("User", user)));
    userList.Description = null; // default value
    
    var usernameVal = login.CreateLocalVariable("UsernameVal");
    usernameVal.DataType = eSpace.TextType;
    usernameVal.SetDefaultValue(null); // default value
    usernameVal.Description = null; // default value
    
    var passwordVal = login.CreateLocalVariable("PasswordVal");
    passwordVal.DataType = eSpace.TextType;
    passwordVal.SetDefaultValue(null); // default value
    passwordVal.Description = null; // default value
    
    var isExecuting = login.CreateLocalVariable("IsExecuting");
    isExecuting.DataType = eSpace.BooleanType;
    isExecuting.SetDefaultValue("False");
    isExecuting.Description = null; // default value
    
    var remember = login.CreateLocalVariable("Remember");
    remember.DataType = eSpace.BooleanType;
    remember.SetDefaultValue(null); // default value
    remember.Description = null; // default value
    
    var isSampleUsersPopupVisible = login.CreateLocalVariable("IsSampleUsersPopupVisible");
    isSampleUsersPopupVisible.DataType = eSpace.BooleanType;
    isSampleUsersPopupVisible.SetDefaultValue("False");
    isSampleUsersPopupVisible.Description = null; // default value
    login.OnDestroy.Destination = null; // default value
    login.OnRender.Destination = null; // default value
    
    var closePopup = login.CreateScreenAction("ClosePopup");
    closePopup.Description = null; // default value
    
    var startNode2 = closePopup.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var onInitialize = login.CreateScreenAction("OnInitialize");
    onInitialize.Description = null; // default value
    
    var startNode3 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.IsDisabled = false; // default value
    
    var assignNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode2.HorizontalPosition = 3192;
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.VerticalPosition = 2964;
    
    var assignment2 = assignNode2.CreateAssignment("IsExecuting", "False");
    
    var endNode2 = onInitialize.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3196;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 5120;
    login.OnInitialize.Destination = onInitialize;
    
    var onReady = login.CreateScreenAction("OnReady");
    onReady.Description = null; // default value
    
    var startNode4 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.IsDisabled = false; // default value
    
    var enterOnClick = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IJavaScriptNode>("EnterOnClick");
    enterOnClick.Description = null; // default value
    enterOnClick.HorizontalPosition = 3192;
    enterOnClick.IsDisabled = false; // default value
    enterOnClick.JavaScript = "document.addEventListener(\'keypress\', function enterLogin(e){\r\n    if(e.which == 13) {\r\n        $actions.Login();\r\n        document.removeEventListener(\'keypress\', enterLogin);\r\n    }\r\n});\r\n\r\n ";
    enterOnClick.VerticalPosition = 2964;
    
    var endNode3 = onReady.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode3.HorizontalPosition = 3196;
    endNode3.IsDisabled = false; // default value
    endNode3.VerticalPosition = 5120;
    login.OnReady.Destination = onReady;
    
    var assignNode3 = closePopup.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode3.HorizontalPosition = 3192;
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = null; // default value
    assignNode3.VerticalPosition = 2736;
    
    var assignment3 = assignNode3.CreateAssignment("IsSampleUsersPopupVisible", "False");
    startNode2.Target = assignNode3;
    startNode3.Target = assignNode2;
    assignNode2.Target = endNode2;
    startNode4.Target = enterOnClick;
    enterOnClick.Target = endNode3;
    
    var endNode4 = closePopup.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode4.HorizontalPosition = 3196;
    endNode4.IsDisabled = false; // default value
    endNode4.VerticalPosition = 5120;
    assignNode3.Target = endNode4;
    
    var forgotPassword = login.CreateScreenAction("ForgotPassword");
    forgotPassword.Description = null; // default value
    
    var startNode5 = forgotPassword.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.IsDisabled = false; // default value
    
    var endNode5 = forgotPassword.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(startNode5, 4320);
    endNode5.IsDisabled = false; // default value
    
    var login2 = login.CreateScreenAction("Login");
    login2.Description = null; // default value
    
    var startNode6 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.HorizontalPosition = 2514;
    startNode6.IsDisabled = false; // default value
    startNode6.VerticalPosition = 914;
    
    var assignNode4 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode6, 1371);
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = null; // default value
    
    var assignment4 = assignNode4.CreateAssignment("IsExecuting", "True");
    
    var doLogin = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("DoLogin").ConnectedBelow(assignNode4, 1372);
    var doLogin2 = eSpace.ServerActions.Named("DoLogin");
    doLogin.Action = doLogin2;
    doLogin.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    doLogin.IsDisabled = false; // default value
    doLogin.ServerRequestTimeout = null; // default value
    var password = doLogin2.InputParameters.Named("Password");
    doLogin.SetArgumentValue(password, "PasswordVal");
    var username = doLogin2.InputParameters.Named("Username");
    doLogin.SetArgumentValue(username, "UsernameVal");
    
    var initClientVars = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("InitClientVars");
    var initClientVars2 = eSpace.ClientActions.Named("InitClientVars");
    initClientVars.Action = initClientVars2;
    initClientVars.HorizontalPosition = 2508;
    initClientVars.IsDisabled = false; // default value
    initClientVars.VerticalPosition = 5016;
    doLogin.Target = initClientVars;
    
    var destinationNode = login2.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>();
    var redirectToURL = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IExternalSite>().Named("RedirectToURL");
    destinationNode.Destination = redirectToURL;
    destinationNode.HorizontalPosition = 2514;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    destinationNode.VerticalPosition = 6402;
    var uRL = redirectToURL.InputParameters.Named("URL");
    destinationNode.SetArgumentValue(uRL, "\"/\" + GetEntryEspaceName() + \"/\"\n\n");
    initClientVars.Target = destinationNode;
    
    var allExceptions = login2.CreateNode<OutSystems.Model.Logic.Nodes.IExceptionHandlerNode>("AllExceptions");
    allExceptions.AbortTransaction = true; // default value
    allExceptions.Exception = eSpace.AllExceptions;
    allExceptions.HorizontalPosition = 10285;
    allExceptions.IsDisabled = false; // default value
    allExceptions.LogError = true; // default value
    allExceptions.VerticalPosition = 914;
    
    var assignNode5 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(allExceptions, 1371);
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = null; // default value
    
    var assignment5 = assignNode5.CreateAssignment("IsExecuting", "False");
    
    var messageNode = login2.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(assignNode5, 1600);
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("AllExceptions.ExceptionMessage");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Error;
    
    var endNode6 = login2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode, 1372);
    endNode6.IsDisabled = false; // default value
    
    var loginWithSampleUser = login.CreateScreenAction("LoginWithSampleUser");
    loginWithSampleUser.Description = null; // default value
    
    var userId = loginWithSampleUser.CreateInputParameter("UserId");
    userId.DataType = user.IdentifierType;
    userId.Description = null; // default value
    userId.IsMandatory = true; // default value
    
    var startNode7 = loginWithSampleUser.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.IsDisabled = false; // default value
    
    var assignNode6 = loginWithSampleUser.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode6.HorizontalPosition = 3192;
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    assignNode6.VerticalPosition = 2508;
    
    var assignment6 = assignNode6.CreateAssignment("IsSampleUsersPopupVisible", "False");
    startNode7.Target = assignNode6;
    
    var login_SampleUser = loginWithSampleUser.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Login_SampleUser");
    var login_SampleUser2 = eSpace.ServerActions.Named("Login_SampleUser");
    login_SampleUser.Action = login_SampleUser2;
    login_SampleUser.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    login_SampleUser.HorizontalPosition = 3199;
    login_SampleUser.IsDisabled = false; // default value
    login_SampleUser.ServerRequestTimeout = null; // default value
    login_SampleUser.VerticalPosition = 3881;
    var userId2 = login_SampleUser2.InputParameters.Named("UserId");
    login_SampleUser.SetArgumentValue(userId2, "UserId");
    assignNode6.Target = login_SampleUser;
    
    var initClientVars3 = loginWithSampleUser.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("InitClientVars");
    initClientVars3.Action = initClientVars2;
    initClientVars3.HorizontalPosition = 3192;
    initClientVars3.IsDisabled = false; // default value
    initClientVars3.VerticalPosition = 5256;
    login_SampleUser.Target = initClientVars3;
    
    var destinationNode2 = loginWithSampleUser.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ConnectedBelow(initClientVars3, 1480);
    var dashboard = eSpace.MobileFlows.Named("Dashboard");
    var dashboard2 = dashboard.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Dashboard");
    destinationNode2.Destination = dashboard2;
    destinationNode2.IsDisabled = false; // default value
    destinationNode2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var openPopup = login.CreateScreenAction("OpenPopup");
    openPopup.Description = null; // default value
    
    var startNode8 = openPopup.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.IsDisabled = false; // default value
    
    var assignNode7 = openPopup.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode7.HorizontalPosition = 3192;
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    assignNode7.VerticalPosition = 2736;
    
    var assignment7 = assignNode7.CreateAssignment("IsSampleUsersPopupVisible", "True");
    startNode8.Target = assignNode7;
    
    var endNode7 = openPopup.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode7.HorizontalPosition = 3196;
    endNode7.IsDisabled = false; // default value
    endNode7.VerticalPosition = 5120;
    assignNode7.Target = endNode7;
    
    var layoutBlankInstance = login.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutBlankInstance.CustomStyle = null; // default value
    layoutBlankInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layoutBlank = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutBlank");
    layoutBlankInstance.SourceBlock = layoutBlank;
    
    var placeholderContentWidget = layoutBlankInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var container = placeholderContentWidget.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container.SetStyle("\"login-screen\"");
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
    container2.SetStyle("\"login-container\"");
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
    container3.CustomStyle = "padding: 20px; text-align: left;";
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
    container3.SetStyle("\"login-form full-height\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "7 col";
    
    var container4 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle("\"login-logo\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var container5 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container5.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container5.Animate = false; // default value
    container5.ColumnGap = null; // default value
    container5.ColumnSpan = null; // default value
    container5.CustomStyle = "text-align: center;";
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
    
    var image = container5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image.ColumnSpan = null; // default value
    image.CustomStyle = null; // default value
    image.DefaultImage = null; // default value
    image.FlexBasis = null; // default value
    image.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    var oM_Logo = eSpace.Images.Named("OM_Logo");
    image.Image = oM_Logo;
    image.SetImageContent(null); // default value
    image.RowSpan = null; // default value
    image.SetStyle(null); // default value
    image.Type = ServiceStudio.Plugin.NRWidgets.Enumerations.Type.Static; // default value
    image.SetUrl(null); // default value
    image.Width = ""; // default value
    
    var container6 = container4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container6.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container6.Animate = false; // default value
    container6.ColumnGap = null; // default value
    container6.ColumnSpan = null; // default value
    container6.CustomStyle = "text-align: center;";
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
    
    var textWidget = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses("\"heading5 \"");
    textWidget.Text = "Order Management";
    
    var container7 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container7.SetStyle("\"login-inputs margin-top-m\"");
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
    
    var label = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label.Widgets.First();
    textWidget2.CustomStyle = null; // default value
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Username";
    
    var input_UsernameVal = container8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_UsernameVal");
    input_UsernameVal.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_UsernameVal.ColumnSpan = null; // default value
    input_UsernameVal.CustomStyle = null; // default value
    input_UsernameVal.SetEnabled("True");
    input_UsernameVal.FlexBasis = null; // default value
    input_UsernameVal.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_UsernameVal.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Text; // default value
    input_UsernameVal.SetMandatory("True");
    input_UsernameVal.MaxLength = 250;
    input_UsernameVal.SetPrompt(null); // default value
    input_UsernameVal.RowSpan = null; // default value
    input_UsernameVal.SetStyle("\"form-control\""); // default value
    input_UsernameVal.SetVariable("UsernameVal");
    input_UsernameVal.Width = "(fill parent)"; // default value
    var builtinEvent = input_UsernameVal.OnChange;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent.Destination = null; // default value
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
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
    
    var textWidget3 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label2.Widgets.First();
    textWidget3.CustomStyle = null; // default value
    textWidget3.Name = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Password";
    
    var input_PasswordVal = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_PasswordVal");
    input_PasswordVal.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_PasswordVal.ColumnSpan = null; // default value
    input_PasswordVal.CustomStyle = null; // default value
    input_PasswordVal.SetEnabled("True");
    input_PasswordVal.FlexBasis = null; // default value
    input_PasswordVal.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_PasswordVal.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Password;
    input_PasswordVal.SetMandatory("True");
    input_PasswordVal.MaxLength = null;
    input_PasswordVal.SetPrompt(null); // default value
    input_PasswordVal.RowSpan = null; // default value
    input_PasswordVal.SetStyle("\"form-control login-password\"");
    input_PasswordVal.SetVariable("PasswordVal");
    input_PasswordVal.Width = "(fill parent)"; // default value
    var builtinEvent2 = input_PasswordVal.OnChange;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = null; // default value
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container10 = container7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container10.SetStyle("\"margin-top-l\"");
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var columns2Instance = container10.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    
    var placeholderContentWidget2 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var alignCenterInstance = placeholderContentWidget2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    alignCenterInstance.CustomStyle = null; // default value
    alignCenterInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var utilities = outSystemsUI.MobileFlows.Named("Utilities");
    var alignCenter = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("AlignCenter");
    alignCenterInstance.SourceBlock = alignCenter;
    var extendedClass2 = alignCenter.InputParameters.Named("ExtendedClass");
    alignCenterInstance.SetArgumentValue(extendedClass2, null); // default value
    var isHorizontal = alignCenter.InputParameters.Named("IsHorizontal");
    alignCenterInstance.SetArgumentValue(isHorizontal, null);
    
    var placeholderContentWidget3 = alignCenterInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var checkbox1 = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.ICheckbox>("Checkbox1");
    checkbox1.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    checkbox1.ColumnSpan = null; // default value
    checkbox1.CustomStyle = null; // default value
    checkbox1.SetEnabled("True");
    checkbox1.FlexBasis = null; // default value
    checkbox1.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    checkbox1.RowSpan = null; // default value
    checkbox1.SetStyle("\"checkbox\""); // default value
    checkbox1.SetVariable("Remember");
    var builtinEvent3 = checkbox1.OnChange;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = null; // default value
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var label3 = placeholderContentWidget3.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label3.SetStyle("\"font-size-s margin-left-s\"");
    label3.TargetWidget = checkbox1;
    label3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    label3.Width = "(fill parent)"; // default value
    
    var textWidget4 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label3.Widgets.First();
    textWidget4.CustomStyle = null; // default value
    textWidget4.Name = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Remember me";
    
    var placeholderContentWidget4 = columns2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var container11 = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container11.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container11.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container11.Animate = false; // default value
    container11.ColumnGap = null; // default value
    container11.ColumnSpan = null; // default value
    container11.CustomStyle = "text-align: right;";
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
    
    var link = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
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
    
    var textWidget5 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)link.Widgets.First();
    textWidget5.CustomStyle = null; // default value
    textWidget5.Name = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Forgot password?";
    var builtinEvent4 = link.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = forgotPassword;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container12 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container12.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container12.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container12.Animate = false; // default value
    container12.ColumnGap = null; // default value
    container12.ColumnSpan = null; // default value
    container12.CustomStyle = "text-align: center;";
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
    container12.SetStyle("\"login-button margin-top-l\"");
    container12.SetVisible("True"); // default value
    container12.VisibleWasExpression = false; // default value
    container12.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container12.Width = "(fill parent)"; // default value
    
    var button = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    button.Widgets.ToList().ForEach(x => x.Delete());
    button.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button.ColumnGap = null; // default value
    button.ColumnSpan = null; // default value
    button.SetConfirmationMessage(null); // default value
    button.CustomStyle = null; // default value
    button.SetEnabled("not IsExecuting");
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
    button.Width = "7 col";
    
    var ifWidget = button.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.Animate = false; // default value
    ifWidget.SetCondition("not IsExecuting");
    ifWidget.CustomStyle = null; // default value
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var textWidget6 = ifWidget.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget6.CustomStyle = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Login";
    
    var icon = ifWidget.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon.ColumnSpan = null; // default value
    icon.CustomStyle = null; // default value
    icon.FlexBasis = null; // default value
    icon.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon.Icon = "spinner";
    icon.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon.RowSpan = null; // default value
    icon.SetStyle("\"icon fa-spin\"");
    icon.SetVisible("True"); // default value
    icon.Weight = null; // default value
    var builtinEvent5 = button.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent5.Destination = login2;
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container13 = container12.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container13.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container13.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container13.Animate = false; // default value
    container13.ColumnGap = null; // default value
    container13.ColumnSpan = null; // default value
    container13.CustomStyle = "text-align: center;";
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
    container13.SetStyle("\"margin-top-s\"");
    container13.SetVisible("True"); // default value
    container13.VisibleWasExpression = false; // default value
    container13.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container13.Width = "(fill parent)"; // default value
    
    var link2 = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
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
    link2.SetStyle(" \"font-bold\"");
    link2.SetVisible("True"); // default value
    link2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link2.Width = ""; // default value
    
    var textWidget7 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)link2.Widgets.First();
    textWidget7.CustomStyle = null; // default value
    textWidget7.Name = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Sample Users";
    var builtinEvent6 = link2.OnClick;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent6.Destination = openPopup;
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var loginImage = container.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("LoginImage");
    loginImage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    loginImage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    loginImage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    loginImage.Animate = false; // default value
    loginImage.ColumnGap = null; // default value
    loginImage.ColumnSpan = null; // default value
    loginImage.CustomStyle = null; // default value
    loginImage.FlexBasis = null; // default value
    loginImage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    loginImage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    loginImage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    loginImage.Gap = null;
    loginImage.GridColumns = null; // default value
    loginImage.GridRows = null; // default value
    loginImage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    loginImage.RowGap = null; // default value
    loginImage.RowSpan = null; // default value
    loginImage.SetStyle("\"login-image\"");
    loginImage.SetVisible("True"); // default value
    loginImage.VisibleWasExpression = false; // default value
    loginImage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    loginImage.Width = "";
    
    var popup = login.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>();
    popup.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    popup.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    popup.ColumnGap = null; // default value
    popup.ColumnSpan = null; // default value
    popup.CustomStyle = null; // default value
    popup.ExpandedInWebEditor = false;
    popup.FlexBasis = null; // default value
    popup.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    popup.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    popup.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    popup.Gap = null;
    popup.GridColumns = null; // default value
    popup.GridRows = null; // default value
    popup.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    popup.RowGap = null; // default value
    popup.RowSpan = null; // default value
    popup.SetShowPopup("IsSampleUsersPopupVisible");
    popup.SetStyle("\"popup-dialog sampleusers-dialog\"");
    popup.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var container14 = popup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container14.SetStyle("\"heading4\"");
    container14.SetVisible("True"); // default value
    container14.VisibleWasExpression = false; // default value
    container14.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container14.Width = "11 col";
    
    var textWidget8 = container14.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.CustomStyle = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Sample Users";
    
    var container15 = popup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container15.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container15.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container15.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container15.Animate = false; // default value
    container15.ColumnGap = null; // default value
    container15.ColumnSpan = null; // default value
    container15.CustomStyle = "text-align: right;";
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
    container15.Width = "1 col";
    
    var link3 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link3.Widgets.ToList().ForEach(x => x.Delete());
    link3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link3.ColumnGap = null; // default value
    link3.ColumnSpan = null; // default value
    link3.SetConfirmationMessage(null); // default value
    link3.CustomStyle = null; // default value
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
    
    var icon2 = link3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon2.ColumnSpan = null; // default value
    icon2.CustomStyle = null; // default value
    icon2.FlexBasis = null; // default value
    icon2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon2.Icon = "remove";
    icon2.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    icon2.RowSpan = null; // default value
    icon2.SetStyle("\"icon font-size-base\"");
    icon2.SetVisible("True"); // default value
    icon2.Weight = null; // default value
    var builtinEvent7 = link3.OnClick;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = closePopup;
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var container16 = popup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container16.SetStyle("\"margin-top-base\"");
    container16.SetVisible("True"); // default value
    container16.VisibleWasExpression = false; // default value
    container16.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container16.Width = "(fill parent)"; // default value
    
    var list = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IList>();
    list.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    list.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    list.AnimateItems = true; // default value
    list.ColumnGap = null; // default value
    list.ColumnSpan = null; // default value
    list.CustomStyle = null; // default value
    list.ExpandedInWebEditor = false;
    list.FlexBasis = null; // default value
    list.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    list.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    list.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    list.Gap = null;
    list.GridColumns = null; // default value
    list.GridRows = null; // default value
    list.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    list.Mode = ServiceStudio.Plugin.NRWidgets.Enumerations.Mode.Default; // default value
    list.RowGap = null; // default value
    list.RowSpan = null; // default value
    list.SetSource("GetSampleUsers.UserList");
    list.SetStyle("\"list list-group\""); // default value
    list.Tag = "div"; // default value
    list.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    list.Width = "(fill parent)"; // default value
    
    var container17 = list.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container17.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container17.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container17.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container17.Animate = false; // default value
    container17.ColumnGap = null; // default value
    container17.ColumnSpan = null; // default value
    container17.CustomStyle = "padding: 0px;";
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
    
    var container18 = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container18.SetStyle("\"font-bold\"");
    container18.SetVisible("True"); // default value
    container18.VisibleWasExpression = false; // default value
    container18.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container18.Width = "(fill parent)"; // default value
    
    var link4 = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link4.Widgets.ToList().ForEach(x => x.Delete());
    link4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link4.ColumnGap = null; // default value
    link4.ColumnSpan = null; // default value
    link4.SetConfirmationMessage(null); // default value
    link4.CustomStyle = null; // default value
    link4.SetEnabled("True");
    link4.FlexBasis = null; // default value
    link4.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link4.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link4.Gap = null;
    link4.GridColumns = null; // default value
    link4.GridRows = null; // default value
    link4.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link4.RowGap = null; // default value
    link4.RowSpan = null; // default value
    link4.SetStyle(null); // default value
    link4.SetVisible("True"); // default value
    link4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link4.Width = ""; // default value
    
    var expression = link4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = null; // default value
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("GetSampleUsers.UserList.Current.User.Name");
    expression.Width = ""; // default value
    var builtinEvent8 = link4.OnClick;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = loginWithSampleUser;
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    builtinEvent8.SetArgumentValue(userId, "GetSampleUsers.UserList.Current.User.Id");
    
    var container19 = container17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container19.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container19.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container19.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container19.Animate = false; // default value
    container19.ColumnGap = null; // default value
    container19.ColumnSpan = null; // default value
    container19.CustomStyle = "margin-top: 5px;";
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
    
    var expression2 = container19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("\"(\" + GetSampleUsers.UserList.Current.Role + \")\"");
    expression2.Width = ""; // default value
    
    var separatorInstance = container17.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    separatorInstance.CustomStyle = null; // default value
    separatorInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var separator = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Separator");
    separatorInstance.SourceBlock = separator;
    var space = separator.InputParameters.Named("Space");
    separatorInstance.SetArgumentValue(space, "Entities.Space.Small");
    var color = separator.InputParameters.Named("Color");
    separatorInstance.SetArgumentValue(color, null);
    var extendedClass3 = separator.InputParameters.Named("ExtendedClass");
    separatorInstance.SetArgumentValue(extendedClass3, null); // default value
    var isVertical = separator.InputParameters.Named("IsVertical");
    separatorInstance.SetArgumentValue(isVertical, null);
    var builtinEvent9 = list.OnScrollEnding;
    builtinEvent9.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent9.Destination = null; // default value
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
}

```