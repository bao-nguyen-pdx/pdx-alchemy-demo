```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow common) => {
    
    /*** creating IMobileScreen 'InvalidPermissions' ***/
    var invalidPermissions = common.CreateScreen("InvalidPermissions");
    invalidPermissions.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = common.GetESpace();
    var anonymous = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Anonymous");
    invalidPermissions.Roles.Add(anonymous);
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    invalidPermissions.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    invalidPermissions.Roles.Add(orderManager);
    var registered = eSpace.SystemRoles.OfType<OutSystems.Model.Logic.ISystemRole>().Named("Registered");
    invalidPermissions.Roles.Add(registered);
    invalidPermissions.AnonymousAccess = false; // default value
    invalidPermissions.CustomURL = false; // default value
    invalidPermissions.Description = null; // default value
    invalidPermissions.HorizontalPosition = 2285;
    invalidPermissions.Icon = null; // default value
    invalidPermissions.IsDisabled = false; // default value
    invalidPermissions.PageName = null; // default value
    invalidPermissions.Public = false; // default value
    invalidPermissions.Shareable = false; // default value
    invalidPermissions.StyleSheet = null; // default value
    invalidPermissions.SetTitle("\"Invalid Permissions\"");
    invalidPermissions.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    invalidPermissions.VerticalPosition = 685;
    invalidPermissions.OnDestroy.Destination = null; // default value
    invalidPermissions.OnInitialize.Destination = null; // default value
    invalidPermissions.OnReady.Destination = null; // default value
    invalidPermissions.OnRender.Destination = null; // default value
    
    var layoutBlankInstance = invalidPermissions.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    layoutBlankInstance.CustomStyle = null; // default value
    layoutBlankInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var layoutBlank = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("LayoutBlank");
    layoutBlankInstance.SourceBlock = layoutBlank;
    
    var placeholderContentWidget = layoutBlankInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var blankSlateInstance = placeholderContentWidget.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    blankSlateInstance.CustomStyle = null; // default value
    blankSlateInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var content = outSystemsUI.MobileFlows.Named("Content");
    var blankSlate = content.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("BlankSlate");
    blankSlateInstance.SourceBlock = blankSlate;
    // delete default content in Icon placeholder
    var placeholderContentWidget2 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    placeholderContentWidget2.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass = blankSlate.InputParameters.Named("ExtendedClass");
    blankSlateInstance.SetArgumentValue(extendedClass, null); // default value
    var fullHeight = blankSlate.InputParameters.Named("FullHeight");
    blankSlateInstance.SetArgumentValue(fullHeight, "True");
    
    var icon1 = placeholderContentWidget2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>("Icon1");
    icon1.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon1.ColumnSpan = null; // default value
    icon1.CustomStyle = null; // default value
    icon1.FlexBasis = null; // default value
    icon1.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon1.Icon = "warning";
    icon1.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon1.RowSpan = null; // default value
    icon1.SetStyle("\"icon\""); // default value
    icon1.SetVisible("True"); // default value
    icon1.Weight = null; // default value
    
    var placeholderContentWidget3 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var textWidget = placeholderContentWidget3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget.CustomStyle = null; // default value
    textWidget.SetStyleClasses(null); // default value
    textWidget.Text = "You don\'t have permissions to view the required screen.";
    
    var placeholderContentWidget4 = blankSlateInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Actions");
    var button = placeholderContentWidget4.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
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
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button.Widgets.First();
    textWidget2.CustomStyle = null; // default value
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Login";
    var builtinEvent = button.OnClick;
    builtinEvent.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var login = common.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Login");
    builtinEvent.Destination = login;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
}

```