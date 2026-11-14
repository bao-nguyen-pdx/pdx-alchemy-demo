```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow common) => {
    
    /*** creating IMobileScreen 'Splash' ***/
    var splash = common.CreateScreen("Splash");
    splash.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = common.GetESpace();
    var anonymous = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Anonymous");
    splash.Roles.Add(anonymous);
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    splash.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    splash.Roles.Add(orderManager);
    var registered = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Registered");
    splash.Roles.Add(registered);
    splash.AnonymousAccess = false; // default value
    splash.CustomURL = false; // default value
    splash.Description = null; // default value
    splash.HorizontalPosition = 3885;
    splash.Icon = null; // default value
    splash.IsDisabled = false; // default value
    splash.PageName = null; // default value
    splash.Public = false; // default value
    splash.Shareable = false; // default value
    splash.StyleSheet = "html {\n    -servicestudio-height: 100% !important;\n}\n";
    splash.SetTitle(null); // default value
    splash.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    splash.VerticalPosition = 685;
    
    var percentage = splash.CreateLocalVariable("Percentage");
    percentage.DataType = eSpace.IntegerType;
    percentage.SetDefaultValue(null); // default value
    percentage.Description = null; // default value
    splash.OnDestroy.Destination = null; // default value
    splash.OnInitialize.Destination = null; // default value
    splash.OnReady.Destination = null; // default value
    splash.OnRender.Destination = null; // default value
    
    var navigate = splash.CreateScreenAction("Navigate");
    navigate.Description = null; // default value
    
    var startNode = navigate.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var getURL = navigate.CreateNode<OutSystems.Model.Logic.Nodes.IJavaScriptNode>("GetURL");
    getURL.Description = null; // default value
    getURL.HorizontalPosition = 3192;
    getURL.IsDisabled = false; // default value
    getURL.JavaScript = "$parameters.URL = window.location.pathname;";
    getURL.VerticalPosition = 2508;
    
    var uRL = getURL.CreateOutputParameter("URL");
    uRL.DataType = eSpace.TextType;
    uRL.SetDefaultValue(null); // default value
    uRL.Description = null; // default value
    startNode.Target = getURL;
    
    var destinationNode = navigate.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>();
    var redirectToURL = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IExternalSite>().Named("RedirectToURL");
    destinationNode.Destination = redirectToURL;
    destinationNode.HorizontalPosition = 3199;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    destinationNode.VerticalPosition = 4342;
    var uRL2 = redirectToURL.InputParameters.Named("URL");
    destinationNode.SetArgumentValue(uRL2, "GetURL.URL");
    getURL.Target = destinationNode;
    
    var onComplete = splash.CreateScreenAction("OnComplete");
    onComplete.Description = null; // default value
    
    var startNode2 = onComplete.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.HorizontalPosition = 1141;
    startNode2.IsDisabled = false; // default value
    startNode2.VerticalPosition = 1141;
    
    var exitAnimation = onComplete.CreateNode<OutSystems.Model.Logic.Nodes.IJavaScriptNode>("ExitAnimation").ConnectedToTheRightOf(startNode2, 1829);
    exitAnimation.Description = null; // default value
    exitAnimation.IsDisabled = false; // default value
    exitAnimation.JavaScript = "var splashScreen = document.querySelector(\'.splash-screen\');\n\nif( typeof splashScreen != undefined) {\n    splashScreen.classList.add(\'is--completed\');\n\n    splashScreen.addEventListener(\'animationend\', function handler(event) {\n        $actions.Navigate();   \n    });  \n}";
    
    var endNode = onComplete.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(exitAnimation, 1830);
    endNode.IsDisabled = false; // default value
    
    var onUpgradeProgress = splash.CreateScreenAction("OnUpgradeProgress");
    onUpgradeProgress.Description = null; // default value
    
    var completed = onUpgradeProgress.CreateInputParameter("Completed");
    completed.DataType = eSpace.IntegerType;
    completed.Description = null; // default value
    completed.IsMandatory = true; // default value
    
    var total = onUpgradeProgress.CreateInputParameter("Total");
    total.DataType = eSpace.IntegerType;
    total.Description = null; // default value
    total.IsMandatory = true; // default value
    
    var startNode3 = onUpgradeProgress.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.HorizontalPosition = 1141;
    startNode3.IsDisabled = false; // default value
    startNode3.VerticalPosition = 1141;
    
    var assignNode = onUpgradeProgress.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedToTheRightOf(startNode3, 2058);
    assignNode.IsDisabled = false; // default value
    assignNode.Label = null; // default value
    
    var assignment = assignNode.CreateAssignment("Percentage", "(Completed / Total)");
    
    var endNode2 = onUpgradeProgress.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode, 2058);
    endNode2.IsDisabled = false; // default value
    
    var layoutBlankInstance = splash.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    container.SetStyle("\"splash-screen\"");
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
    container2.SetStyle("\"splash-form\"");
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
    container3.SetStyle("\"splash-logo\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var image = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IImage>();
    image.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    image.ColumnSpan = null; // default value
    image.CustomStyle = "height: 64px;";
    image.DefaultImage = null; // default value
    image.FlexBasis = null; // default value
    image.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    var oM_Logo = eSpace.Images.Named("OM_Logo");
    image.Image = oM_Logo;
    image.SetImageContent(null); // default value
    image.RowSpan = null; // default value
    image.SetStyle("\"display-block\"");
    image.Type = ServiceStudio.Plugin.NRWidgets.Enumerations.Type.Static; // default value
    image.SetUrl(null); // default value
    image.Width = "64px";
    
    var container4 = container2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle("\"splash-loading margin-top-m\"");
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
    container5.SetStyle("\"splash-loading-bar background-primary\"");
    container5.SetVisible("True"); // default value
    container5.VisibleWasExpression = false; // default value
    container5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container5.Width = "(fill parent)"; // default value
    
    var extendedProperty = container5.CreateExtendedProperty();
    extendedProperty.Property = "style";
    extendedProperty.SetValue("\"transform: scaleX(\" + Percentage + \")\"");
    
    var applicationLoadEventsInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    applicationLoadEventsInstance.CustomStyle = null; // default value
    applicationLoadEventsInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var private = outSystemsUI.MobileFlows.Named("Private");
    var applicationLoadEvents = private.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ApplicationLoadEvents");
    applicationLoadEventsInstance.SourceBlock = applicationLoadEvents;
    var minimumDisplayTimeMs = applicationLoadEvents.InputParameters.Named("MinimumDisplayTimeMs");
    applicationLoadEventsInstance.SetArgumentValue(minimumDisplayTimeMs, null);
    var eventHandler = applicationLoadEventsInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnLoadComplete");
    eventHandler.Handler = onComplete;
    var eventHandler2 = applicationLoadEventsInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnUpgradeProgress");
    eventHandler2.Handler = onUpgradeProgress;
    eventHandler2.SetArgumentValue(completed, "Completed");
    eventHandler2.SetArgumentValue(total, "Total");
}

```