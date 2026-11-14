```csharp
(OutSystems.Model.UI.Mobile.IMobileFlow order) => {
    
    /*** creating IMobileScreen 'OrderDetail' ***/
    var orderDetail = order.CreateScreen("OrderDetail");
    orderDetail.GetESpace().Roles.ToList().ForEach(x => x.Delete());
    var eSpace = order.GetESpace();
    var orderManagement_CS = eSpace.References.Named("OrderManagement_CS");
    var orderRepresentative = orderManagement_CS.Roles.Named("OrderRepresentative");
    orderDetail.Roles.Add(orderRepresentative);
    var orderManager = orderManagement_CS.Roles.Named("OrderManager");
    orderDetail.Roles.Add(orderManager);
    orderDetail.AnonymousAccess = false; // default value
    orderDetail.CustomURL = false; // default value
    orderDetail.Description = null; // default value
    orderDetail.HorizontalPosition = 2057;
    orderDetail.Icon = null; // default value
    orderDetail.IsDisabled = false; // default value
    orderDetail.PageName = null; // default value
    orderDetail.Public = false; // default value
    orderDetail.Shareable = false; // default value
    orderDetail.StyleSheet = null; // default value
    orderDetail.SetTitle("\"Order Management - Order Detail\"");
    orderDetail.URLStructure = OutSystems.Model.Enumerations.URLStructure.QueryString; // default value
    orderDetail.VerticalPosition = 914;
    
    var getAccounts = orderDetail.CreateScreenAggregate(false, "GetAccounts");
    getAccounts.CacheInMinutes = null; // default value
    getAccounts.Description = null; // default value
    getAccounts.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getAccounts.SetMaxRecords("500");
    getAccounts.SetStartIndex(null); // default value
    getAccounts.Timeout = null; // default value
    var account = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Account");
    var addSourceOperation = getAccounts.AsDatabaseAggregate.CreateSource(account);
    
    
    var getOrder = orderDetail.CreateScreenAggregate(false, "GetOrder");
    getOrder.CacheInMinutes = null; // default value
    getOrder.Description = null; // default value
    getOrder.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getOrder.SetMaxRecords("1");
    getOrder.SetStartIndex(null); // default value
    getOrder.Timeout = null; // default value
    var order3 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Order");
    var order2 = getOrder.AsDatabaseAggregate.CreateSource(order3);
    var account2 = getOrder.AsDatabaseAggregate.CreateSource(account);
    var opportunity2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Opportunity");
    var opportunity = getOrder.AsDatabaseAggregate.CreateSource(opportunity2);
    var _System_ = eSpace.References.Named("(System)");
    var user2 = _System_.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("User");
    var user = getOrder.AsDatabaseAggregate.CreateSource(user2);
    var join = getOrder.AsDatabaseAggregate.CreateJoin();
    join.LeftSource = account2;
    join.RightSource = order2;
    join.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join.SetCondition("Order.AccountId = Account.Id");
    var join2 = getOrder.AsDatabaseAggregate.CreateJoin();
    join2.LeftSource = user;
    join2.RightSource = order2;
    join2.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join2.SetCondition("Order.CreatedBy = User.Id");
    var join3 = getOrder.AsDatabaseAggregate.CreateJoin();
    join3.LeftSource = order2;
    join3.RightSource = opportunity;
    join3.JoinType = OutSystems.Model.Enumerations.JoinType.Left;
    join3.SetCondition("Order.OpportunityId = Opportunity.Id");
    getOrder.AsDatabaseAggregate.CreateFilter("Order.Id = OrderId");
    
    
    var getOrderItemsByOrderId = orderDetail.CreateScreenAggregate(false, "GetOrderItemsByOrderId");
    getOrderItemsByOrderId.CacheInMinutes = null; // default value
    getOrderItemsByOrderId.Description = null; // default value
    getOrderItemsByOrderId.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getOrderItemsByOrderId.SetMaxRecords("50"); // default value
    getOrderItemsByOrderId.SetStartIndex(null); // default value
    getOrderItemsByOrderId.Timeout = null; // default value
    var orderItem2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("OrderItem");
    var orderItem = getOrderItemsByOrderId.AsDatabaseAggregate.CreateSource(orderItem2);
    var product2 = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IServerEntitySignature>().Named("Product");
    var product = getOrderItemsByOrderId.AsDatabaseAggregate.CreateSource(product2);
    var join4 = getOrderItemsByOrderId.AsDatabaseAggregate.CreateJoin();
    join4.LeftSource = product;
    join4.RightSource = orderItem;
    join4.JoinType = OutSystems.Model.Enumerations.JoinType.Inner;
    join4.SetCondition("OrderItem.ProductId = Product.Id");
    getOrderItemsByOrderId.AsDatabaseAggregate.CreateFilter("OrderItem.OrderId = OrderId");
    getOrderItemsByOrderId.AsDatabaseAggregate.CreateFilter("OrderItem.IsActive = True ");
    
    
    var getProducts = orderDetail.CreateScreenAggregate(false, "GetProducts");
    getProducts.CacheInMinutes = null; // default value
    getProducts.Description = null; // default value
    getProducts.Fetch = OutSystems.Model.Enumerations.DataSourceFetch.AtStart; // default value
    getProducts.SetMaxRecords("500");
    getProducts.SetStartIndex(null); // default value
    getProducts.Timeout = null; // default value
    var product3 = getProducts.AsDatabaseAggregate.CreateSource(product2);
    getProducts.AsDatabaseAggregate.CreateFilter("Product.IsActive = True ");
    getProducts.AsDatabaseAggregate.CreateSort().SetAttribute("Product.Name");
    
    
    var orderId = orderDetail.CreateInputParameter("OrderId");
    orderId.DataType = order3.IdentifierType;
    orderId.Description = null; // default value
    orderId.IsMandatory = true; // default value
    
    var accountId = orderDetail.CreateInputParameter("AccountId");
    accountId.DataType = account.IdentifierType;
    accountId.SetDefaultValue(null); // default value
    accountId.Description = null; // default value
    accountId.IsMandatory = false;
    
    var opportunityId = orderDetail.CreateInputParameter("OpportunityId");
    opportunityId.DataType = opportunity2.IdentifierType;
    opportunityId.SetDefaultValue(null); // default value
    opportunityId.Description = null; // default value
    opportunityId.IsMandatory = false;
    
    var isEditing = orderDetail.CreateLocalVariable("IsEditing");
    isEditing.DataType = eSpace.BooleanType;
    isEditing.SetDefaultValue("False");
    isEditing.Description = null; // default value
    
    var isAddingProduct = orderDetail.CreateLocalVariable("IsAddingProduct");
    isAddingProduct.DataType = eSpace.BooleanType;
    isAddingProduct.SetDefaultValue("False");
    isAddingProduct.Description = null; // default value
    
    var orderDetailsEdit = orderDetail.CreateLocalVariable("OrderDetailsEdit");
    orderDetailsEdit.DataType = eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("Inventory", eSpace.IntegerType), Tuple.Create<string, ITypeSignature>("OrderItem", orderItem2), Tuple.Create<string, ITypeSignature>("Product", product2));
    orderDetailsEdit.Description = null; // default value
    
    var isDeleteOrder = orderDetail.CreateLocalVariable("IsDeleteOrder");
    isDeleteOrder.DataType = eSpace.BooleanType;
    isDeleteOrder.SetDefaultValue("False");
    isDeleteOrder.Description = null; // default value
    orderDetail.OnDestroy.Destination = null; // default value
    orderDetail.OnInitialize.Destination = null; // default value
    orderDetail.OnReady.Destination = null; // default value
    orderDetail.OnRender.Destination = null; // default value
    
    var onAfterFetch_GetOrder = orderDetail.CreateScreenAction("OnAfterFetch_GetOrder");
    onAfterFetch_GetOrder.Description = null; // default value
    
    var startNode = onAfterFetch_GetOrder.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode.IsDisabled = false; // default value
    
    var ifNode = onAfterFetch_GetOrder.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode.SetCondition("GetOrder.List.Empty = False");
    ifNode.HorizontalPosition = 3199;
    ifNode.IsDisabled = false; // default value
    ifNode.Label = "Existing Order?";
    ifNode.VerticalPosition = 2285;
    startNode.Target = ifNode;
    
    var order_GenerateCode = onAfterFetch_GetOrder.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Order_GenerateCode");
    var order_GenerateCode2 = orderManagement_CS.ServerActions.Named("Order_GenerateCode");
    order_GenerateCode.Action = order_GenerateCode2;
    order_GenerateCode.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    order_GenerateCode.HorizontalPosition = 3192;
    order_GenerateCode.IsDisabled = false; // default value
    order_GenerateCode.ServerRequestTimeout = null; // default value
    order_GenerateCode.VerticalPosition = 3648;
    ifNode.FalseTarget = order_GenerateCode;
    
    var endNode = onAfterFetch_GetOrder.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode, 1372);
    endNode.IsDisabled = false; // default value
    ifNode.TrueTarget = endNode;
    
    var assignNode = onAfterFetch_GetOrder.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode.HorizontalPosition = 3199;
    assignNode.IsDisabled = false; // default value
    assignNode.Label = "GetOrder";
    assignNode.VerticalPosition = 5016;
    
    var assignment = assignNode.CreateAssignment("GetOrder.List.Current.Order.OrderStatusId", "Entities.OrderStatus.Draft");
    
    var assignment2 = assignNode.CreateAssignment("GetOrder.List.Current.Order.OpportunityId", "OpportunityId");
    
    var assignment3 = assignNode.CreateAssignment("GetOrder.List.Current.Order.RequiredOn", "AddMonths(CurrDateTime(), 1)");
    
    var assignment4 = assignNode.CreateAssignment("GetOrder.List.Current.Order.AccountId", "AccountId");
    
    var assignment5 = assignNode.CreateAssignment("GetOrder.List.Current.Order.Code", "Order_GenerateCode.Code");
    order_GenerateCode.Target = assignNode;
    
    var assignNode2 = onAfterFetch_GetOrder.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode2.HorizontalPosition = 3200;
    assignNode2.IsDisabled = false; // default value
    assignNode2.Label = null; // default value
    assignNode2.VerticalPosition = 6384;
    
    var assignment6 = assignNode2.CreateAssignment("IsEditing", "True");
    assignNode.Target = assignNode2;
    
    var endNode2 = onAfterFetch_GetOrder.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode2.HorizontalPosition = 3199;
    endNode2.IsDisabled = false; // default value
    endNode2.VerticalPosition = 7752;
    assignNode2.Target = endNode2;
    
    var order_Delete = orderDetail.CreateScreenAction("Order_Delete");
    order_Delete.Description = null; // default value
    
    var startNode2 = order_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode2.IsDisabled = false; // default value
    
    var assignNode3 = order_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode3.HorizontalPosition = 3192;
    assignNode3.IsDisabled = false; // default value
    assignNode3.Label = "IsActive = False";
    assignNode3.VerticalPosition = 2280;
    
    var assignment7 = assignNode3.CreateAssignment("GetOrder.List.Current.Order.IsActive", "False");
    startNode2.Target = assignNode3;
    
    var order_Save = order_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Order_Save");
    var order_Save2 = orderManagement_CS.ServerActions.Named("Order_Save");
    order_Save.Action = order_Save2;
    order_Save.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    order_Save.HorizontalPosition = 3200;
    order_Save.IsDisabled = false; // default value
    order_Save.ServerRequestTimeout = null; // default value
    order_Save.VerticalPosition = 3657;
    var orderRecord = order_Save2.InputParameters.Named("OrderRecord");
    order_Save.SetArgumentValue(orderRecord, "GetOrder.List.Current.Order");
    var invoker = order_Save2.InputParameters.Named("Invoker");
    order_Save.SetArgumentValue(invoker, "GetUserId()");
    assignNode3.Target = order_Save;
    
    var updateStockOnOrderDelete = order_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("UpdateStockOnOrderDelete");
    var updateStockOnOrderDelete2 = eSpace.ClientActions.Named("UpdateStockOnOrderDelete");
    updateStockOnOrderDelete.Action = updateStockOnOrderDelete2;
    updateStockOnOrderDelete.HorizontalPosition = 3192;
    updateStockOnOrderDelete.IsDisabled = false; // default value
    updateStockOnOrderDelete.VerticalPosition = 5257;
    var orderId2 = updateStockOnOrderDelete2.InputParameters.Named("OrderId");
    updateStockOnOrderDelete.SetArgumentValue(orderId2, "GetOrder.List.Current.Order.Id");
    order_Save.Target = updateStockOnOrderDelete;
    
    var messageNode = order_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>();
    messageNode.HorizontalPosition = 3199;
    messageNode.IsDisabled = false; // default value
    messageNode.Label = null; // default value
    messageNode.SetMessage("\"Order deleted succefully\"");
    messageNode.Type = OutSystems.Model.Enumerations.MessageType.Success;
    messageNode.VerticalPosition = 6628;
    updateStockOnOrderDelete.Target = messageNode;
    
    var destinationNode = order_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ConnectedBelow(messageNode, 1829);
    var orders = order.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("Orders");
    destinationNode.Destination = orders;
    destinationNode.IsDisabled = false; // default value
    destinationNode.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var order_Save3 = orderDetail.CreateScreenAction("Order_Save");
    order_Save3.Description = null; // default value
    
    var startNode3 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode3.IsDisabled = false; // default value
    
    var ifNode2 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode2.SetCondition("GetOrder.List.Current.Order.AccountId <> NullIdentifier()");
    ifNode2.HorizontalPosition = 3200;
    ifNode2.IsDisabled = false; // default value
    ifNode2.Label = "Account filled?";
    ifNode2.VerticalPosition = 2286;
    startNode3.Target = ifNode2;
    
    var assignNode4 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode4.HorizontalPosition = 5016;
    assignNode4.IsDisabled = false; // default value
    assignNode4.Label = "Set Account invalid";
    assignNode4.VerticalPosition = 2280;
    
    var assignment8 = assignNode4.CreateAssignment("Account.Valid", "False");
    
    var assignment9 = assignNode4.CreateAssignment("Account.ValidationMessage", "\"Account must be filled\"");
    ifNode2.FalseTarget = assignNode4;
    
    var assignNode5 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode5.HorizontalPosition = 3192;
    assignNode5.IsDisabled = false; // default value
    assignNode5.Label = "Set Account valid";
    assignNode5.VerticalPosition = 3876;
    
    var assignment10 = assignNode5.CreateAssignment("Account.Valid", "True");
    ifNode2.TrueTarget = assignNode5;
    
    var ifNode3 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode3.SetCondition("FormOrder.Valid and\nFormOrderAddress.Valid");
    ifNode3.HorizontalPosition = 3200;
    ifNode3.IsDisabled = false; // default value
    ifNode3.Label = "Valid form?";
    ifNode3.VerticalPosition = 5472;
    assignNode4.Target = ifNode3;
    assignNode5.Target = ifNode3;
    
    var endNode3 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ToTheRightOf(ifNode3, 1829);
    endNode3.IsDisabled = false; // default value
    ifNode3.FalseTarget = endNode3;
    
    var order_Save4 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Order_Save").Below(ifNode3, 1596);
    order_Save4.Action = order_Save2;
    order_Save4.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    order_Save4.IsDisabled = false; // default value
    order_Save4.ServerRequestTimeout = null; // default value
    order_Save4.SetArgumentValue(invoker, "GetUserId()");
    order_Save4.SetArgumentValue(orderRecord, "GetOrder.List.Current.Order");
    ifNode3.TrueTarget = order_Save4;
    
    var assignNode6 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(order_Save4, 1368);
    assignNode6.IsDisabled = false; // default value
    assignNode6.Label = null; // default value
    
    var assignment11 = assignNode6.CreateAssignment("OrderId", "Order_Save.OrderId");
    
    var orderToggleEditing = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("OrderToggleEditing").ConnectedBelow(assignNode6, 1368);
    orderToggleEditing.IsDisabled = false; // default value
    
    var orderToggleEditing2 = orderDetail.CreateScreenAction("OrderToggleEditing");
    orderToggleEditing2.Description = null; // default value
    
    var startNode4 = orderToggleEditing2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode4.IsDisabled = false; // default value
    
    var assignNode7 = orderToggleEditing2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode7.HorizontalPosition = 3192;
    assignNode7.IsDisabled = false; // default value
    assignNode7.Label = null; // default value
    assignNode7.VerticalPosition = 2971;
    
    var assignment12 = assignNode7.CreateAssignment("IsEditing", "not IsEditing");
    
    var endNode4 = orderToggleEditing2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode4.HorizontalPosition = 3196;
    endNode4.IsDisabled = false; // default value
    endNode4.VerticalPosition = 5120;
    orderToggleEditing.Action = orderToggleEditing2;
    
    var refreshDataNode = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(orderToggleEditing, 1368);
    refreshDataNode.DataSource = getOrder;
    refreshDataNode.IsDisabled = false; // default value
    refreshDataNode.SetMaxRecords("1");
    refreshDataNode.SetStartIndex(null); // default value
    startNode4.Target = assignNode7;
    assignNode7.Target = endNode4;
    
    var messageNode2 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(refreshDataNode, 1596);
    messageNode2.IsDisabled = false; // default value
    messageNode2.Label = null; // default value
    messageNode2.SetMessage("\"The order was successfully saved.\"");
    messageNode2.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var endNode5 = order_Save3.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(messageNode2, 1368);
    endNode5.IsDisabled = false; // default value
    
    var order_SetStatus = orderDetail.CreateScreenAction("Order_SetStatus");
    order_SetStatus.Description = null; // default value
    
    var orderStatusId = order_SetStatus.CreateInputParameter("OrderStatusId");
    var orderStatus = orderManagement_CS.Entities.OfType<OutSystems.Model.Data.IStaticEntitySignature>().Named("OrderStatus");
    orderStatusId.DataType = orderStatus.IdentifierType;
    orderStatusId.Description = null; // default value
    orderStatusId.IsMandatory = true; // default value
    
    var startNode5 = order_SetStatus.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode5.HorizontalPosition = 3199;
    startNode5.IsDisabled = false; // default value
    startNode5.VerticalPosition = 914;
    
    var assignNode8 = order_SetStatus.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode5, 1372);
    assignNode8.IsDisabled = false; // default value
    assignNode8.Label = "OrderStatusId";
    
    var assignment13 = assignNode8.CreateAssignment("GetOrder.List.Current.Order.OrderStatusId", "OrderStatusId");
    
    var order_Save5 = order_SetStatus.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Order_Save").ConnectedBelow(assignNode8, 1372);
    order_Save5.Action = order_Save2;
    order_Save5.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    order_Save5.IsDisabled = false; // default value
    order_Save5.ServerRequestTimeout = null; // default value
    order_Save5.SetArgumentValue(orderRecord, "GetOrder.List.Current.Order");
    order_Save5.SetArgumentValue(invoker, "GetUserId()");
    
    var messageNode3 = order_SetStatus.CreateNode<OutSystems.Model.Logic.Nodes.IMessageNode>().ConnectedBelow(order_Save5, 1358);
    messageNode3.IsDisabled = false; // default value
    messageNode3.Label = null; // default value
    messageNode3.SetMessage("\"The order status was changed.\"");
    messageNode3.Type = OutSystems.Model.Enumerations.MessageType.Success;
    
    var destinationNode2 = order_SetStatus.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>();
    destinationNode2.Destination = orderDetail;
    destinationNode2.HorizontalPosition = 3192;
    destinationNode2.IsDisabled = false; // default value
    destinationNode2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    destinationNode2.VerticalPosition = 6384;
    destinationNode2.SetArgumentValue(opportunityId, "OpportunityId");
    destinationNode2.SetArgumentValue(orderId, "OrderId");
    destinationNode2.SetArgumentValue(accountId, "AccountId");
    messageNode3.Target = destinationNode2;
    
    var orderCancelEditing = orderDetail.CreateScreenAction("OrderCancelEditing");
    orderCancelEditing.Description = null; // default value
    
    var startNode6 = orderCancelEditing.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode6.IsDisabled = false; // default value
    
    var ifNode4 = orderCancelEditing.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode4.SetCondition("OrderId = NullIdentifier()");
    ifNode4.HorizontalPosition = 3192;
    ifNode4.IsDisabled = false; // default value
    ifNode4.Label = "New Order?";
    ifNode4.VerticalPosition = 2280;
    startNode6.Target = ifNode4;
    
    var orderToggleEditing3 = orderCancelEditing.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("OrderToggleEditing").Below(ifNode4, 1368);
    orderToggleEditing3.Action = orderToggleEditing2;
    orderToggleEditing3.IsDisabled = false; // default value
    ifNode4.FalseTarget = orderToggleEditing3;
    
    var destinationNode3 = orderCancelEditing.CreateNode<OutSystems.Model.Logic.Nodes.IDestinationNode>().ToTheRightOf(ifNode4, 1836);
    destinationNode3.Destination = eSpace.PreviousScreen;
    destinationNode3.IsDisabled = false; // default value
    destinationNode3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromLeft;
    ifNode4.TrueTarget = destinationNode3;
    
    var refreshDataNode2 = orderCancelEditing.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(orderToggleEditing3, 1368);
    refreshDataNode2.DataSource = getOrder;
    refreshDataNode2.IsDisabled = false; // default value
    refreshDataNode2.SetMaxRecords("1");
    refreshDataNode2.SetStartIndex(null); // default value
    
    var endNode6 = orderCancelEditing.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode6.HorizontalPosition = 3196;
    endNode6.IsDisabled = false; // default value
    endNode6.VerticalPosition = 6384;
    refreshDataNode2.Target = endNode6;
    
    var orderItem_OnChange = orderDetail.CreateScreenAction("OrderItem_OnChange");
    orderItem_OnChange.Description = null; // default value
    
    var selectedOptionList = orderItem_OnChange.CreateInputParameter("SelectedOptionList");
    var outSystemsUI = eSpace.References.Named("OutSystemsUI");
    var dropdownOption = outSystemsUI.Structures.Named("DropdownOption");
    selectedOptionList.DataType = eSpace.GetOrCreateListType(dropdownOption);
    selectedOptionList.Description = null; // default value
    selectedOptionList.IsMandatory = false;
    
    var startNode7 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode7.HorizontalPosition = 2743;
    startNode7.IsDisabled = false; // default value
    startNode7.VerticalPosition = 1143;
    
    var assignNode9 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(startNode7, 2057);
    assignNode9.IsDisabled = false; // default value
    assignNode9.Label = "OrderItem.ProductId";
    
    var assignment14 = assignNode9.CreateAssignment(" OrderDetailsEdit.OrderItem.ProductId", "IntegerToIdentifier(TextToInteger(SelectedOptionList.Current.Value))");
    
    var assignment15 = assignNode9.CreateAssignment("OrderDetailsEdit.Product.Id", " OrderDetailsEdit.OrderItem.ProductId");
    
    var product_GetAmountAndStock = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("Product_GetAmountAndStock").ConnectedBelow(assignNode9, 1829);
    var product_GetAmountAndStock2 = eSpace.ServerActions.Named("Product_GetAmountAndStock");
    product_GetAmountAndStock.Action = product_GetAmountAndStock2;
    product_GetAmountAndStock.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    product_GetAmountAndStock.IsDisabled = false; // default value
    product_GetAmountAndStock.ServerRequestTimeout = null; // default value
    var productId = product_GetAmountAndStock2.InputParameters.Named("ProductId");
    product_GetAmountAndStock.SetArgumentValue(productId, "OrderDetailsEdit.OrderItem.ProductId");
    
    var ifNode5 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ConnectedBelow(product_GetAmountAndStock, 1600);
    ifNode5.SetCondition("Product_GetAmountAndStock.Stock >= (OrderDetailsEdit.OrderItem.Quantity - OrderDetailsEdit.Inventory)\n");
    ifNode5.IsDisabled = false; // default value
    ifNode5.Label = "Stock Available?";
    
    var ifNode6 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>().ToTheRightOf(ifNode5, 1828);
    ifNode6.SetCondition("Product_GetAmountAndStock.Stock + OrderDetailsEdit.Inventory = 0 ");
    ifNode6.IsDisabled = false; // default value
    ifNode6.Label = null; // default value
    ifNode5.FalseTarget = ifNode6;
    
    var assignNode10 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode5, 1600);
    assignNode10.IsDisabled = false; // default value
    assignNode10.Label = "Set stock available";
    
    var assignment16 = assignNode10.CreateAssignment("Input_Quantity.Valid", "True");
    ifNode5.TrueTarget = assignNode10;
    
    var assignNode11 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode6, 2058);
    assignNode11.IsDisabled = false; // default value
    assignNode11.Label = "Set stock unavailable";
    
    var assignment17 = assignNode11.CreateAssignment("Input_Quantity.Valid", "False");
    
    var assignment18 = assignNode11.CreateAssignment("Input_Quantity.ValidationMessage", "\"Only \" + (Product_GetAmountAndStock.Stock + OrderDetailsEdit.Inventory)+ \" in stock\"");
    ifNode6.FalseTarget = assignNode11;
    
    var assignNode12 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode12.HorizontalPosition = 6629;
    assignNode12.IsDisabled = false; // default value
    assignNode12.Label = "Set out of stock";
    assignNode12.VerticalPosition = 8229;
    
    var assignment19 = assignNode12.CreateAssignment("Input_Quantity.Valid", "False");
    
    var assignment20 = assignNode12.CreateAssignment("Input_Quantity.ValidationMessage", "\"Out of stock\"");
    ifNode6.TrueTarget = assignNode12;
    
    var assignNode13 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ConnectedBelow(assignNode10, 1371);
    assignNode13.IsDisabled = false; // default value
    assignNode13.Label = "Amount";
    
    var assignment21 = assignNode13.CreateAssignment("OrderDetailsEdit.Product.Price", "Product_GetAmountAndStock.Amount");
    
    var assignment22 = assignNode13.CreateAssignment("OrderDetailsEdit.OrderItem.Amount", "(Product_GetAmountAndStock.Amount * OrderDetailsEdit.OrderItem.Quantity) - (Product_GetAmountAndStock.Amount * OrderDetailsEdit.OrderItem.Quantity * (OrderDetailsEdit.OrderItem.Discount / 100))");
    
    var endNode7 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedToTheRightOf(assignNode11, 1600);
    endNode7.IsDisabled = false; // default value
    assignNode12.Target = endNode7;
    
    var endNode8 = orderItem_OnChange.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode13, 1371);
    endNode8.IsDisabled = false; // default value
    
    var product_Delete = orderDetail.CreateScreenAction("Product_Delete");
    product_Delete.Description = null; // default value
    
    var startNode8 = product_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode8.IsDisabled = false; // default value
    
    var assignNode14 = product_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode14.HorizontalPosition = 3192;
    assignNode14.IsDisabled = false; // default value
    assignNode14.Label = "IsActive";
    assignNode14.VerticalPosition = 2052;
    
    var assignment23 = assignNode14.CreateAssignment("GetOrderItemsByOrderId.List.Current.OrderItem.IsActive", "False");
    startNode8.Target = assignNode14;
    
    var orderItem_Save = product_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("OrderItem_Save").ConnectedBelow(assignNode14, 1368);
    var orderItem_Save2 = orderManagement_CS.ServerActions.Named("OrderItem_Save");
    orderItem_Save.Action = orderItem_Save2;
    orderItem_Save.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    orderItem_Save.IsDisabled = false; // default value
    orderItem_Save.ServerRequestTimeout = null; // default value
    var orderItemRecord = orderItem_Save2.InputParameters.Named("OrderItemRecord");
    orderItem_Save.SetArgumentValue(orderItemRecord, "GetOrderItemsByOrderId.List.Current.OrderItem");
    var invoker2 = orderItem_Save2.InputParameters.Named("Invoker");
    orderItem_Save.SetArgumentValue(invoker2, "GetUserId()");
    
    var updateOrderAmount = product_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("UpdateOrderAmount");
    var updateOrderAmount2 = eSpace.ClientActions.Named("UpdateOrderAmount");
    updateOrderAmount.Action = updateOrderAmount2;
    updateOrderAmount.HorizontalPosition = 3199;
    updateOrderAmount.IsDisabled = false; // default value
    updateOrderAmount.VerticalPosition = 4797;
    var orderId3 = updateOrderAmount2.InputParameters.Named("OrderId");
    updateOrderAmount.SetArgumentValue(orderId3, "GetOrder.List.Current.Order.Id");
    orderItem_Save.Target = updateOrderAmount;
    
    var updateProductStock = product_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("UpdateProductStock");
    var updateProductStock2 = eSpace.ClientActions.Named("UpdateProductStock");
    updateProductStock.Action = updateProductStock2;
    updateProductStock.HorizontalPosition = 3192;
    updateProductStock.IsDisabled = false; // default value
    updateProductStock.VerticalPosition = 6612;
    var productId2 = updateProductStock2.InputParameters.Named("ProductId");
    updateProductStock.SetArgumentValue(productId2, "GetOrderItemsByOrderId.List.Current.Product.Id");
    var quantity = updateProductStock2.InputParameters.Named("Quantity");
    updateProductStock.SetArgumentValue(quantity, "GetOrderItemsByOrderId.List.Current.OrderItem.Quantity");
    updateOrderAmount.Target = updateProductStock;
    
    var refreshDataNode3 = product_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>();
    refreshDataNode3.DataSource = getOrder;
    refreshDataNode3.HorizontalPosition = 3200;
    refreshDataNode3.IsDisabled = false; // default value
    refreshDataNode3.SetMaxRecords("1");
    refreshDataNode3.SetStartIndex(null); // default value
    refreshDataNode3.VerticalPosition = 8436;
    updateProductStock.Target = refreshDataNode3;
    
    var refreshDataNode4 = product_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(refreshDataNode3, 1596);
    refreshDataNode4.DataSource = getOrderItemsByOrderId;
    refreshDataNode4.IsDisabled = false; // default value
    refreshDataNode4.SetMaxRecords("50");
    refreshDataNode4.SetStartIndex(null); // default value
    
    var endNode9 = product_Delete.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(refreshDataNode4, 1368);
    endNode9.IsDisabled = false; // default value
    
    var product_Save = orderDetail.CreateScreenAction("Product_Save");
    product_Save.Description = null; // default value
    
    var startNode9 = product_Save.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode9.HorizontalPosition = 3424;
    startNode9.IsDisabled = false; // default value
    startNode9.VerticalPosition = 800;
    
    var ifNode7 = product_Save.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode7.SetCondition("not FormProduct.Valid");
    ifNode7.HorizontalPosition = 3420;
    ifNode7.IsDisabled = false; // default value
    ifNode7.Label = "Invalid form?";
    ifNode7.VerticalPosition = 2280;
    startNode9.Target = ifNode7;
    
    var assignNode15 = product_Save.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode7, 1596);
    assignNode15.IsDisabled = false; // default value
    assignNode15.Label = "OrderId";
    
    var assignment24 = assignNode15.CreateAssignment("OrderDetailsEdit.OrderItem.OrderId", "OrderId");
    ifNode7.FalseTarget = assignNode15;
    
    var endNode10 = product_Save.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>();
    endNode10.HorizontalPosition = 5244;
    endNode10.IsDisabled = false; // default value
    endNode10.VerticalPosition = 2285;
    ifNode7.TrueTarget = endNode10;
    
    var saveOrderItemandUpdateStock = product_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteServerActionNode>("SaveOrderItemandUpdateStock");
    var saveOrderItemandUpdateStock2 = eSpace.ServerActions.Named("SaveOrderItemandUpdateStock");
    saveOrderItemandUpdateStock.Action = saveOrderItemandUpdateStock2;
    saveOrderItemandUpdateStock.AnimationEffect = OutSystems.Model.Enumerations.ExecuteActionAnimationEffectType.None; // default value
    saveOrderItemandUpdateStock.HorizontalPosition = 3429;
    saveOrderItemandUpdateStock.IsDisabled = false; // default value
    saveOrderItemandUpdateStock.ServerRequestTimeout = null; // default value
    saveOrderItemandUpdateStock.VerticalPosition = 5714;
    var productId3 = saveOrderItemandUpdateStock2.InputParameters.Named("ProductId");
    saveOrderItemandUpdateStock.SetArgumentValue(productId3, "OrderDetailsEdit.OrderItem.ProductId");
    var orderItemRecord2 = saveOrderItemandUpdateStock2.InputParameters.Named("OrderItemRecord");
    saveOrderItemandUpdateStock.SetArgumentValue(orderItemRecord2, "OrderDetailsEdit.OrderItem");
    var quantity2 = saveOrderItemandUpdateStock2.InputParameters.Named("Quantity");
    saveOrderItemandUpdateStock.SetArgumentValue(quantity2, "OrderDetailsEdit.OrderItem.Quantity");
    assignNode15.Target = saveOrderItemandUpdateStock;
    
    var refreshDataNode5 = product_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(saveOrderItemandUpdateStock, 1600);
    refreshDataNode5.DataSource = getOrder;
    refreshDataNode5.IsDisabled = false; // default value
    refreshDataNode5.SetMaxRecords("1");
    refreshDataNode5.SetStartIndex(null); // default value
    
    var refreshDataNode6 = product_Save.CreateNode<OutSystems.Model.Logic.Nodes.IRefreshDataNode>().ConnectedBelow(refreshDataNode5, 1600);
    refreshDataNode6.DataSource = getOrderItemsByOrderId;
    refreshDataNode6.IsDisabled = false; // default value
    refreshDataNode6.SetMaxRecords("50");
    refreshDataNode6.SetStartIndex(null); // default value
    
    var product_ToggleEdit = product_Save.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("Product_ToggleEdit").ConnectedBelow(refreshDataNode6, 1372);
    product_ToggleEdit.IsDisabled = false; // default value
    
    var product_ToggleEdit2 = orderDetail.CreateScreenAction("Product_ToggleEdit");
    product_ToggleEdit2.Description = null; // default value
    
    var toggleOnly = product_ToggleEdit2.CreateInputParameter("ToggleOnly");
    toggleOnly.DataType = eSpace.BooleanType;
    toggleOnly.Description = null; // default value
    toggleOnly.IsMandatory = true; // default value
    
    var orderDetailsEmpty = product_ToggleEdit2.CreateLocalVariable("OrderDetailsEmpty");
    var decimal = eSpace.Structures.Named("Decimal");
    orderDetailsEmpty.DataType = eSpace.GetOrCreateAnonymousStructure(Tuple.Create<string, ITypeSignature>("Decimal", decimal), Tuple.Create<string, ITypeSignature>("OrderItem", orderItem2), Tuple.Create<string, ITypeSignature>("Product", product2));
    orderDetailsEmpty.Description = null; // default value
    
    var startNode10 = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode10.IsDisabled = false; // default value
    
    var assignNode16 = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode16.HorizontalPosition = 3200;
    assignNode16.IsDisabled = false; // default value
    assignNode16.Label = null; // default value
    assignNode16.VerticalPosition = 2286;
    
    var assignment25 = assignNode16.CreateAssignment("IsAddingProduct", "not IsAddingProduct");
    
    var ifNode8 = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode8.SetCondition("IsAddingProduct");
    ifNode8.HorizontalPosition = 3192;
    ifNode8.IsDisabled = false; // default value
    ifNode8.Label = null; // default value
    ifNode8.VerticalPosition = 3648;
    
    var sidebarClose = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("SidebarClose");
    var sidebarClose2 = outSystemsUI.ClientActions.Named("SidebarClose");
    sidebarClose.Action = sidebarClose2;
    sidebarClose.HorizontalPosition = 1600;
    sidebarClose.IsDisabled = false; // default value
    sidebarClose.VerticalPosition = 3657;
    var widgetId = sidebarClose2.InputParameters.Named("WidgetId");
    sidebarClose.SetArgumentValue(widgetId, "Popup_EditingProduct.Id");
    
    var sidebarOpen = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IExecuteClientActionNode>("SidebarOpen");
    var sidebarOpen2 = outSystemsUI.ClientActions.Named("SidebarOpen");
    sidebarOpen.Action = sidebarOpen2;
    sidebarOpen.HorizontalPosition = 5257;
    sidebarOpen.IsDisabled = false; // default value
    sidebarOpen.VerticalPosition = 3657;
    var widgetId2 = sidebarOpen2.InputParameters.Named("WidgetId");
    sidebarOpen.SetArgumentValue(widgetId2, "Popup_EditingProduct.Id");
    
    var ifNode9 = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IIfNode>();
    ifNode9.SetCondition("ToggleOnly");
    ifNode9.HorizontalPosition = 3200;
    ifNode9.IsDisabled = false; // default value
    ifNode9.Label = "Just toggle?";
    ifNode9.VerticalPosition = 5257;
    
    var assignNode17 = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().ToTheRightOf(ifNode9, 2057);
    assignNode17.IsDisabled = false; // default value
    assignNode17.Label = "Empty Product";
    
    var assignment26 = assignNode17.CreateAssignment("OrderDetailsEdit", "OrderDetailsEmpty mapTo { Inventory: OrderItem.Quantity, OrderItem: OrderItem, Product: Product }");
    
    var assignment27 = assignNode17.CreateAssignment("OrderDetailsEdit.OrderItem.Quantity", "0");
    
    var endNode11 = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedToTheRightOf(assignNode17, 1372);
    endNode11.IsDisabled = false; // default value
    
    var assignNode18 = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>().Below(ifNode9, 1372);
    assignNode18.IsDisabled = false; // default value
    assignNode18.Label = "Current Product";
    
    var assignment28 = assignNode18.CreateAssignment("OrderDetailsEdit", "GetOrderItemsByOrderId.List.Current mapTo { Inventory: OrderItem.Quantity, OrderItem: OrderItem, Product: Product }");
    
    var endNode12 = product_ToggleEdit2.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode18, 1371);
    endNode12.IsDisabled = false; // default value
    product_ToggleEdit.Action = product_ToggleEdit2;
    
    var endNode13 = product_Save.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(product_ToggleEdit, 1371);
    endNode13.IsDisabled = false; // default value
    product_ToggleEdit.SetArgumentValue(toggleOnly, "True");
    startNode10.Target = assignNode16;
    assignNode16.Target = ifNode8;
    ifNode8.FalseTarget = sidebarClose;
    ifNode8.TrueTarget = sidebarOpen;
    sidebarClose.Target = ifNode9;
    sidebarOpen.Target = ifNode9;
    ifNode9.FalseTarget = assignNode18;
    ifNode9.TrueTarget = assignNode17;
    
    var toggleDeleteOrderPopup = orderDetail.CreateScreenAction("ToggleDeleteOrderPopup");
    toggleDeleteOrderPopup.Description = null; // default value
    
    var startNode11 = toggleDeleteOrderPopup.CreateNode<OutSystems.Model.Logic.Nodes.IStartNode>();
    startNode11.IsDisabled = false; // default value
    
    var assignNode19 = toggleDeleteOrderPopup.CreateNode<OutSystems.Model.Logic.Nodes.IAssignNode>();
    assignNode19.HorizontalPosition = 3200;
    assignNode19.IsDisabled = false; // default value
    assignNode19.Label = null; // default value
    assignNode19.VerticalPosition = 2971;
    
    var assignment29 = assignNode19.CreateAssignment("IsDeleteOrder", "not IsDeleteOrder");
    startNode11.Target = assignNode19;
    
    var endNode14 = toggleDeleteOrderPopup.CreateNode<OutSystems.Model.Logic.Nodes.IEndNode>().ConnectedBelow(assignNode19, 1829);
    endNode14.IsDisabled = false; // default value
    
    var layoutTopInstance = orderDetail.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
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
    menuInstance.SetArgumentValue(activeMenu, "Entities.MenuItem.Orders");
    
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
    builtinEvent.Destination = orders;
    builtinEvent.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.SlideFromLeft;
    
    var placeholderContentWidget4 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Title");
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
    container.SetStyle(null); // default value
    container.SetVisible("True"); // default value
    container.VisibleWasExpression = false; // default value
    container.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container.Width = "100%";
    
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
    container2.SetStyle(null); // default value
    container2.SetVisible("True"); // default value
    container2.VisibleWasExpression = false; // default value
    container2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container2.Width = "";
    
    var alignCenterInstance = container2.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    alignCenterInstance.CustomStyle = null; // default value
    alignCenterInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var utilities = outSystemsUI.MobileFlows.Named("Utilities");
    var alignCenter = utilities.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("AlignCenter");
    alignCenterInstance.SourceBlock = alignCenter;
    var isHorizontal = alignCenter.InputParameters.Named("IsHorizontal");
    alignCenterInstance.SetArgumentValue(isHorizontal, null);
    var extendedClass = alignCenter.InputParameters.Named("ExtendedClass");
    alignCenterInstance.SetArgumentValue(extendedClass, null); // default value
    
    var placeholderContentWidget5 = alignCenterInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var expression = placeholderContentWidget5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression.ColumnSpan = null; // default value
    expression.CustomStyle = null; // default value
    expression.Example = "Order ";
    expression.FlexBasis = null; // default value
    expression.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression.RowSpan = null; // default value
    expression.SetStyle(null); // default value
    expression.SetValue("If(GetOrder.List.Empty, \"New \", \"\") + \n\"Order \"");
    expression.Width = "(fill parent)";
    
    var ifWidget = placeholderContentWidget5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget.Animate = false; // default value
    ifWidget.SetCondition("GetOrder.IsDataFetched");
    ifWidget.CustomStyle = null; // default value
    ifWidget.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var expression2 = ifWidget.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression2.ColumnSpan = null; // default value
    expression2.CustomStyle = null; // default value
    expression2.Example = null; // default value
    expression2.FlexBasis = null; // default value
    expression2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression2.RowSpan = null; // default value
    expression2.SetStyle(null); // default value
    expression2.SetValue("GetOrder.List.Current.Order.Code");
    expression2.Width = ""; // default value
    
    var orderStatusInstance = ifWidget.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    orderStatusInstance.CustomStyle = null; // default value
    orderStatusInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var orderStatus2 = order.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlock>().Named("OrderStatus");
    orderStatusInstance.SourceBlock = orderStatus2;
    var orderStatusId2 = orderStatus2.InputParameters.Named("OrderStatusId");
    orderStatusInstance.SetArgumentValue(orderStatusId2, "GetOrder.List.Current.Order.OrderStatusId");
    
    var placeholderContentWidget6 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Actions");
    var ifWidget2 = placeholderContentWidget6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget2.Animate = false; // default value
    ifWidget2.SetCondition("GetOrder.List.Empty = False ");
    ifWidget2.CustomStyle = null; // default value
    ifWidget2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var ifWidget3 = ifWidget2.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget3.Animate = false; // default value
    ifWidget3.SetCondition("Client.IsOrderManager\nand not GetOrder.List.Empty \nand GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Draft");
    ifWidget3.CustomStyle = null; // default value
    ifWidget3.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var button = ifWidget3.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
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
    button.SetStyle("\"btn\""); // default value
    button.SetVisible("True"); // default value
    button.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button.Width = ""; // default value
    
    var textWidget2 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button.Widgets.First();
    textWidget2.CustomStyle = null; // default value
    textWidget2.Name = null; // default value
    textWidget2.SetStyleClasses(null); // default value
    textWidget2.Text = "Delete order";
    var builtinEvent2 = button.OnClick;
    builtinEvent2.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent2.Destination = toggleDeleteOrderPopup;
    builtinEvent2.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var popup = ifWidget3.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IPopup>();
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
    popup.SetShowPopup("IsDeleteOrder");
    popup.SetStyle("\"popup-dialog\""); // default value
    popup.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var popupTitle = popup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupTitle");
    popupTitle.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    popupTitle.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    popupTitle.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    popupTitle.Animate = false; // default value
    popupTitle.ColumnGap = null; // default value
    popupTitle.ColumnSpan = null; // default value
    popupTitle.CustomStyle = null; // default value
    popupTitle.FlexBasis = null; // default value
    popupTitle.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    popupTitle.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    popupTitle.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    popupTitle.Gap = null;
    popupTitle.GridColumns = null; // default value
    popupTitle.GridRows = null; // default value
    popupTitle.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    popupTitle.RowGap = null; // default value
    popupTitle.RowSpan = null; // default value
    popupTitle.SetStyle("\"heading3 margin-bottom-m\"");
    popupTitle.SetVisible("True"); // default value
    popupTitle.VisibleWasExpression = false; // default value
    popupTitle.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    popupTitle.Width = "(fill parent)"; // default value
    
    var textWidget3 = popupTitle.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget3.CustomStyle = null; // default value
    textWidget3.SetStyleClasses(null); // default value
    textWidget3.Text = "Delete this order?";
    
    var popupMessage = popup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("PopupMessage");
    popupMessage.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    popupMessage.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    popupMessage.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    popupMessage.Animate = false; // default value
    popupMessage.ColumnGap = null; // default value
    popupMessage.ColumnSpan = null; // default value
    popupMessage.CustomStyle = null; // default value
    popupMessage.FlexBasis = null; // default value
    popupMessage.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    popupMessage.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    popupMessage.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    popupMessage.Gap = null;
    popupMessage.GridColumns = null; // default value
    popupMessage.GridRows = null; // default value
    popupMessage.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    popupMessage.RowGap = null; // default value
    popupMessage.RowSpan = null; // default value
    popupMessage.SetStyle("\"margin-bottom-m\"");
    popupMessage.SetVisible("True"); // default value
    popupMessage.VisibleWasExpression = false; // default value
    popupMessage.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    popupMessage.Width = "(fill parent)"; // default value
    
    var textWidget4 = popupMessage.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget4.CustomStyle = null; // default value
    textWidget4.SetStyleClasses(null); // default value
    textWidget4.Text = "Once deleted, all changes will be lost.";
    
    var container3 = popup.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container3.SetStyle("\"text-align-right\"");
    container3.SetVisible("True"); // default value
    container3.VisibleWasExpression = false; // default value
    container3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container3.Width = "(fill parent)"; // default value
    
    var link2 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
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
    link2.SetStyle("\"margin-right-s\"");
    link2.SetVisible("True"); // default value
    link2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link2.Width = ""; // default value
    
    var textWidget5 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)link2.Widgets.First();
    textWidget5.CustomStyle = null; // default value
    textWidget5.Name = null; // default value
    textWidget5.SetStyleClasses(null); // default value
    textWidget5.Text = "Cancel";
    var builtinEvent3 = link2.OnClick;
    builtinEvent3.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent3.Destination = toggleDeleteOrderPopup;
    builtinEvent3.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var button2 = container3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    button2.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button2.ColumnGap = null; // default value
    button2.ColumnSpan = null; // default value
    button2.SetConfirmationMessage(null); // default value
    button2.CustomStyle = null; // default value
    button2.SetEnabled("True");
    button2.FlexBasis = null; // default value
    button2.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button2.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button2.Gap = null;
    button2.GridColumns = null; // default value
    button2.GridRows = null; // default value
    button2.IsDefault = false; // default value
    button2.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button2.RowGap = null; // default value
    button2.RowSpan = null; // default value
    button2.SetStyle("\"btn btn-primary\"");
    button2.SetVisible("True"); // default value
    button2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button2.Width = ""; // default value
    
    var textWidget6 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button2.Widgets.First();
    textWidget6.CustomStyle = null; // default value
    textWidget6.Name = null; // default value
    textWidget6.SetStyleClasses(null); // default value
    textWidget6.Text = "Delete order";
    var builtinEvent4 = button2.OnClick;
    builtinEvent4.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent4.Destination = order_Delete;
    builtinEvent4.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var placeholderContentWidget7 = layoutTopInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "MainContent");
    var mainContent = placeholderContentWidget7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>("MainContent");
    mainContent.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    mainContent.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    mainContent.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    mainContent.Animate = false; // default value
    mainContent.ColumnGap = null; // default value
    mainContent.ColumnSpan = null; // default value
    mainContent.CustomStyle = null; // default value
    mainContent.FlexBasis = null; // default value
    mainContent.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    mainContent.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    mainContent.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    mainContent.Gap = null;
    mainContent.GridColumns = null; // default value
    mainContent.GridRows = null; // default value
    mainContent.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    mainContent.RowGap = null; // default value
    mainContent.RowSpan = null; // default value
    mainContent.SetStyle(null); // default value
    mainContent.SetVisible("True"); // default value
    mainContent.VisibleWasExpression = false; // default value
    mainContent.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    mainContent.Width = "(fill parent)"; // default value
    
    var container4 = mainContent.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container4.SetStyle("\"wizard-container\"");
    container4.SetVisible("True"); // default value
    container4.VisibleWasExpression = false; // default value
    container4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container4.Width = "(fill parent)"; // default value
    
    var dEPRECATED_WizardV2Instance = container4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_WizardV2Instance.CustomStyle = null; // default value
    dEPRECATED_WizardV2Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var navigation = outSystemsUI.MobileFlows.Named("Navigation");
    var dEPRECATED_WizardV2 = navigation.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DEPRECATED_WizardV2");
    dEPRECATED_WizardV2Instance.SourceBlock = dEPRECATED_WizardV2;
    // delete default content in Content placeholder
    var placeholderContentWidget8 = dEPRECATED_WizardV2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    placeholderContentWidget8.Widgets.ToList().ForEach(w => w.Delete());
    var extendedClass2 = dEPRECATED_WizardV2.InputParameters.Named("ExtendedClass");
    dEPRECATED_WizardV2Instance.SetArgumentValue(extendedClass2, null); // default value
    var isVertical = dEPRECATED_WizardV2.InputParameters.Named("IsVertical");
    dEPRECATED_WizardV2Instance.SetArgumentValue(isVertical, null);
    
    var dEPRECATED_WizardItemV2Instance = placeholderContentWidget8.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_WizardItemV2Instance.CustomStyle = null; // default value
    dEPRECATED_WizardItemV2Instance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dEPRECATED_WizardItemV2 = navigation.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DEPRECATED_WizardItemV2");
    dEPRECATED_WizardItemV2Instance.SourceBlock = dEPRECATED_WizardItemV2;
    var extendedClass3 = dEPRECATED_WizardItemV2.InputParameters.Named("ExtendedClass");
    dEPRECATED_WizardItemV2Instance.SetArgumentValue(extendedClass3, null); // default value
    var status = dEPRECATED_WizardItemV2.InputParameters.Named("Status");
    dEPRECATED_WizardItemV2Instance.SetArgumentValue(status, "If(GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Draft, \n    Entities.Steps.Active, \n    Entities.Steps.Past)");
    var useTopLabel = dEPRECATED_WizardItemV2.InputParameters.Named("UseTopLabel");
    dEPRECATED_WizardItemV2Instance.SetArgumentValue(useTopLabel, null);
    var eventHandler = dEPRECATED_WizardItemV2Instance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnClick");
    eventHandler.Handler = null; // default value
    
    var placeholderContentWidget9 = dEPRECATED_WizardItemV2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    var icon2 = placeholderContentWidget9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon2.ColumnSpan = null; // default value
    icon2.CustomStyle = null; // default value
    icon2.FlexBasis = null; // default value
    icon2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon2.Icon = "flag";
    icon2.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    icon2.RowSpan = null; // default value
    icon2.SetStyle("\"icon-draft\"");
    icon2.SetVisible("True"); // default value
    icon2.Weight = null; // default value
    
    var placeholderContentWidget10 = dEPRECATED_WizardItemV2Instance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Label");
    var textWidget7 = placeholderContentWidget10.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget7.CustomStyle = null; // default value
    textWidget7.SetStyleClasses(null); // default value
    textWidget7.Text = "Draft";
    
    var dEPRECATED_WizardItemV2Instance2 = placeholderContentWidget8.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_WizardItemV2Instance2.CustomStyle = null; // default value
    dEPRECATED_WizardItemV2Instance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    dEPRECATED_WizardItemV2Instance2.SourceBlock = dEPRECATED_WizardItemV2;
    dEPRECATED_WizardItemV2Instance2.SetArgumentValue(status, "If(GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Submitted, \n    Entities.Steps.Active,\n    If(GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Closed, \n        Entities.Steps.Past, \n        Entities.Steps.Next))");
    dEPRECATED_WizardItemV2Instance2.SetArgumentValue(extendedClass3, null); // default value
    dEPRECATED_WizardItemV2Instance2.SetArgumentValue(useTopLabel, null);
    var eventHandler2 = dEPRECATED_WizardItemV2Instance2.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnClick");
    eventHandler2.Handler = null; // default value
    
    var placeholderContentWidget11 = dEPRECATED_WizardItemV2Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    var icon3 = placeholderContentWidget11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon3.ColumnSpan = null; // default value
    icon3.CustomStyle = null; // default value
    icon3.FlexBasis = null; // default value
    icon3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon3.Icon = "flag";
    icon3.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    icon3.RowSpan = null; // default value
    icon3.SetStyle("\"icon-submitted\"");
    icon3.SetVisible("True"); // default value
    icon3.Weight = null; // default value
    
    var placeholderContentWidget12 = dEPRECATED_WizardItemV2Instance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Label");
    var textWidget8 = placeholderContentWidget12.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget8.CustomStyle = null; // default value
    textWidget8.SetStyleClasses(null); // default value
    textWidget8.Text = "Submitted";
    
    var dEPRECATED_WizardItemV2Instance3 = placeholderContentWidget8.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dEPRECATED_WizardItemV2Instance3.CustomStyle = null; // default value
    dEPRECATED_WizardItemV2Instance3.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    dEPRECATED_WizardItemV2Instance3.SourceBlock = dEPRECATED_WizardItemV2;
    dEPRECATED_WizardItemV2Instance3.SetArgumentValue(status, "If(GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Closed, Entities.Steps.Past, Entities.Steps.Next)");
    dEPRECATED_WizardItemV2Instance3.SetArgumentValue(extendedClass3, null); // default value
    dEPRECATED_WizardItemV2Instance3.SetArgumentValue(useTopLabel, null);
    var eventHandler3 = dEPRECATED_WizardItemV2Instance3.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnClick");
    eventHandler3.Handler = null; // default value
    
    var placeholderContentWidget13 = dEPRECATED_WizardItemV2Instance3.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    var icon4 = placeholderContentWidget13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon4.ColumnSpan = null; // default value
    icon4.CustomStyle = null; // default value
    icon4.FlexBasis = null; // default value
    icon4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon4.Icon = "flag";
    icon4.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.Twotimes; // default value
    icon4.RowSpan = null; // default value
    icon4.SetStyle("\"icon-closed\"");
    icon4.SetVisible("True"); // default value
    icon4.Weight = null; // default value
    
    var placeholderContentWidget14 = dEPRECATED_WizardItemV2Instance3.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Label");
    var textWidget9 = placeholderContentWidget14.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget9.CustomStyle = null; // default value
    textWidget9.SetStyleClasses(null); // default value
    textWidget9.Text = "Approved & Closed";
    
    var container5 = mainContent.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container5.SetStyle("\"margin-top-xl\"");
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
    container6.CustomStyle = "font-weight: bold; text-align: right;";
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
    
    var isEditable = container6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsEditable");
    isEditable.Animate = false; // default value
    isEditable.SetCondition("OrderId = NullIdentifier() or\nGetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Draft or\nGetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Rejected");
    isEditable.CustomStyle = null; // default value
    isEditable.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var editingOrder = isEditable.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("EditingOrder");
    editingOrder.Animate = false; // default value
    editingOrder.SetCondition("IsEditing");
    editingOrder.CustomStyle = null; // default value
    editingOrder.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var link3 = editingOrder.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
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
    
    var icon5 = link3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon5.ColumnSpan = null; // default value
    icon5.CustomStyle = null; // default value
    icon5.FlexBasis = null; // default value
    icon5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon5.Icon = "save";
    icon5.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon5.RowSpan = null; // default value
    icon5.SetStyle("\"icon\""); // default value
    icon5.SetVisible("True"); // default value
    icon5.Weight = null; // default value
    
    var textWidget10 = link3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget10.CustomStyle = "margin-left: 10px;";
    textWidget10.SetStyleClasses(null); // default value
    textWidget10.Text = "Save";
    var builtinEvent5 = link3.OnClick;
    builtinEvent5.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent5.Destination = order_Save3;
    builtinEvent5.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var link4 = editingOrder.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
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
    
    var textWidget11 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)link4.Widgets.First();
    textWidget11.CustomStyle = "margin-left: 10px;";
    textWidget11.Name = null; // default value
    textWidget11.SetStyleClasses(null); // default value
    textWidget11.Text = "Cancel";
    var builtinEvent6 = link4.OnClick;
    builtinEvent6.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent6.Destination = orderCancelEditing;
    builtinEvent6.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var link5 = editingOrder.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link5.Widgets.ToList().ForEach(x => x.Delete());
    link5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link5.ColumnGap = null; // default value
    link5.ColumnSpan = null; // default value
    link5.SetConfirmationMessage(null); // default value
    link5.CustomStyle = null; // default value
    link5.SetEnabled("True");
    link5.FlexBasis = null; // default value
    link5.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link5.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link5.Gap = null;
    link5.GridColumns = null; // default value
    link5.GridRows = null; // default value
    link5.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link5.RowGap = null; // default value
    link5.RowSpan = null; // default value
    link5.SetStyle(null); // default value
    link5.SetVisible("True"); // default value
    link5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link5.Width = ""; // default value
    
    var icon6 = link5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon6.ColumnSpan = null; // default value
    icon6.CustomStyle = null; // default value
    icon6.FlexBasis = null; // default value
    icon6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon6.Icon = "pencil";
    icon6.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon6.RowSpan = null; // default value
    icon6.SetStyle("\"icon\""); // default value
    icon6.SetVisible("True"); // default value
    icon6.Weight = null; // default value
    
    var textWidget12 = link5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget12.CustomStyle = "margin-left: 10px;";
    textWidget12.SetStyleClasses(null); // default value
    textWidget12.Text = "Edit";
    var builtinEvent7 = link5.OnClick;
    builtinEvent7.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent7.Destination = orderToggleEditing2;
    builtinEvent7.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var columnsSmallLeftInstance = container5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columnsSmallLeftInstance.CustomStyle = null; // default value
    columnsSmallLeftInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var adaptive = outSystemsUI.MobileFlows.Named("Adaptive");
    var columnsSmallLeft = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ColumnsSmallLeft");
    columnsSmallLeftInstance.SourceBlock = columnsSmallLeft;
    var phoneBehavior = columnsSmallLeft.InputParameters.Named("PhoneBehavior");
    columnsSmallLeftInstance.SetArgumentValue(phoneBehavior, "Entities.BreakColumns.All");
    var gutterSize = columnsSmallLeft.InputParameters.Named("GutterSize");
    columnsSmallLeftInstance.SetArgumentValue(gutterSize, "Entities.GutterSize.XXLarge");
    var extendedClass4 = columnsSmallLeft.InputParameters.Named("ExtendedClass");
    columnsSmallLeftInstance.SetArgumentValue(extendedClass4, "\"order-columns\"");
    var tabletBehavior = columnsSmallLeft.InputParameters.Named("TabletBehavior");
    columnsSmallLeftInstance.SetArgumentValue(tabletBehavior, "Entities.BreakColumns.All");
    
    var placeholderContentWidget15 = columnsSmallLeftInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column1");
    var formOrder = placeholderContentWidget15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IForm>("FormOrder");
    formOrder.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    formOrder.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    formOrder.ColumnGap = null; // default value
    formOrder.ColumnSpan = null; // default value
    formOrder.CustomStyle = null; // default value
    formOrder.FlexBasis = null; // default value
    formOrder.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    formOrder.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    formOrder.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    formOrder.Gap = null;
    formOrder.GridColumns = null; // default value
    formOrder.GridRows = null; // default value
    formOrder.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    formOrder.RowGap = null; // default value
    formOrder.RowSpan = null; // default value
    formOrder.SetStyle("\"form split-form\"");
    formOrder.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    formOrder.Width = "(fill parent)"; // default value
    
    var container7 = formOrder.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container7.SetStyle(null); // default value
    container7.SetVisible("True"); // default value
    container7.VisibleWasExpression = false; // default value
    container7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container7.Width = "8 col";
    
    var textWidget13 = container7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget13.CustomStyle = null; // default value
    textWidget13.SetStyleClasses("\"bold\"");
    textWidget13.Text = "Details";
    
    var container8 = formOrder.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container8.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container8.Animate = false; // default value
    container8.ColumnGap = null; // default value
    container8.ColumnSpan = null; // default value
    container8.CustomStyle = "text-align: right;";
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
    container8.Width = "4 col";
    
    var container9 = formOrder.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var label = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    
    var textWidget14 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label.Widgets.First();
    textWidget14.CustomStyle = null; // default value
    textWidget14.Name = null; // default value
    textWidget14.SetStyleClasses(null); // default value
    textWidget14.Text = "Code";
    
    var container10 = container9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container10.SetStyle(null); // default value
    container10.SetVisible("True"); // default value
    container10.VisibleWasExpression = false; // default value
    container10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container10.Width = "(fill parent)"; // default value
    
    var expression3 = container10.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression3.ColumnSpan = null; // default value
    expression3.CustomStyle = null; // default value
    expression3.Example = null; // default value
    expression3.FlexBasis = null; // default value
    expression3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression3.RowSpan = null; // default value
    expression3.SetStyle(null); // default value
    expression3.SetValue("GetOrder.List.Current.Order.Code");
    expression3.Width = ""; // default value
    
    var container11 = formOrder.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var label2 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    
    var textWidget15 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label2.Widgets.First();
    textWidget15.CustomStyle = null; // default value
    textWidget15.Name = null; // default value
    textWidget15.SetStyleClasses(null); // default value
    textWidget15.Text = "Account";
    
    var container12 = container11.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container12.SetStyle(null); // default value
    container12.SetVisible("True"); // default value
    container12.VisibleWasExpression = false; // default value
    container12.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container12.Width = "(fill parent)"; // default value
    
    var ifWidget4 = container12.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget4.Animate = false; // default value
    ifWidget4.SetCondition("IsEditing");
    ifWidget4.CustomStyle = null; // default value
    ifWidget4.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container13 = ifWidget4.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var account3 = container13.CreateWidget<ServiceStudio.Plugin.NRWidgets.IDropdown>("Account");
    account3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    account3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    account3.ColumnGap = null; // default value
    account3.ColumnSpan = null; // default value
    account3.CustomStyle = null; // default value
    account3.DropdownMode = ServiceStudio.Plugin.NRWidgets.Enumerations.DropdownMode.Custom; // default value
    account3.SetEmptyValue("\"-\"");
    account3.SetEnabled("True");
    account3.FlexBasis = null; // default value
    account3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    account3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    account3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    account3.Gap = null;
    account3.GridColumns = null; // default value
    account3.GridRows = null; // default value
    account3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    account3.SetLabels(null); // default value
    account3.SetList("GetAccounts.List");
    account3.SetMandatory("False");
    account3.RowGap = null; // default value
    account3.RowSpan = null; // default value
    account3.SetStyle("\"dropdown\""); // default value
    account3.SetValues("Account.Id");
    account3.SetVariable("GetOrder.List.Current.Order.AccountId");
    account3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    account3.Width = "(fill parent)";
    
    var expression4 = account3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression4.ColumnSpan = null; // default value
    expression4.CustomStyle = null; // default value
    expression4.Example = null; // default value
    expression4.FlexBasis = null; // default value
    expression4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression4.RowSpan = null; // default value
    expression4.SetStyle(null); // default value
    expression4.SetValue("GetAccounts.List.Current.Account.Company");
    expression4.Width = ""; // default value
    var builtinEvent8 = account3.OnChange;
    builtinEvent8.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent8.Destination = null; // default value
    builtinEvent8.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Fade;
    
    var link6 = ifWidget4.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link6.Widgets.ToList().ForEach(x => x.Delete());
    link6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link6.ColumnGap = null; // default value
    link6.ColumnSpan = null; // default value
    link6.SetConfirmationMessage(null); // default value
    link6.CustomStyle = null; // default value
    link6.SetEnabled("True");
    link6.FlexBasis = null; // default value
    link6.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link6.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link6.Gap = null;
    link6.GridColumns = null; // default value
    link6.GridRows = null; // default value
    link6.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link6.RowGap = null; // default value
    link6.RowSpan = null; // default value
    link6.SetStyle(null); // default value
    link6.SetVisible("True"); // default value
    link6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link6.Width = ""; // default value
    
    var expression5 = link6.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression5.ColumnSpan = null; // default value
    expression5.CustomStyle = null; // default value
    expression5.Example = null; // default value
    expression5.FlexBasis = null; // default value
    expression5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression5.RowSpan = null; // default value
    expression5.SetStyle(null); // default value
    expression5.SetValue("GetOrder.List.Current.Account.Company");
    expression5.Width = ""; // default value
    var builtinEvent9 = link6.OnClick;
    builtinEvent9.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    var account4 = eSpace.MobileFlows.Named("Account");
    var accountShow = account4.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileScreen>().Named("AccountShow");
    builtinEvent9.Destination = accountShow;
    builtinEvent9.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var accountId2 = accountShow.InputParameters.Named("AccountId");
    builtinEvent9.SetArgumentValue(accountId2, "GetOrder.List.Current.Order.AccountId");
    
    var ifWidget5 = formOrder.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget5.Animate = false; // default value
    ifWidget5.SetCondition("GetOrder.List.Current.Order.OpportunityId <> NullIdentifier()");
    ifWidget5.CustomStyle = null; // default value
    ifWidget5.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var container14 = ifWidget5.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var label3 = container14.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    
    var textWidget16 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label3.Widgets.First();
    textWidget16.CustomStyle = null; // default value
    textWidget16.Name = null; // default value
    textWidget16.SetStyleClasses(null); // default value
    textWidget16.Text = "Opportunity";
    
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
    container15.SetStyle(null); // default value
    container15.SetVisible("True"); // default value
    container15.VisibleWasExpression = false; // default value
    container15.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container15.Width = "(fill parent)"; // default value
    
    var expression6 = container15.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression6.ColumnSpan = null; // default value
    expression6.CustomStyle = null; // default value
    expression6.Example = null; // default value
    expression6.FlexBasis = null; // default value
    expression6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression6.RowSpan = null; // default value
    expression6.SetStyle(null); // default value
    expression6.SetValue("GetOrder.List.Current.Opportunity.Name");
    expression6.Width = ""; // default value
    
    var container16 = formOrder.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var label4 = container16.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    
    var textWidget17 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label4.Widgets.First();
    textWidget17.CustomStyle = null; // default value
    textWidget17.Name = null; // default value
    textWidget17.SetStyleClasses(null); // default value
    textWidget17.Text = "Order Date";
    
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
    
    var isEditing6 = container17.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsEditing6");
    isEditing6.Animate = false; // default value
    isEditing6.SetCondition("IsEditing");
    isEditing6.CustomStyle = null; // default value
    isEditing6.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var input_ExpectedClosedOn = isEditing6.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_ExpectedClosedOn");
    input_ExpectedClosedOn.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_ExpectedClosedOn.ColumnSpan = null; // default value
    input_ExpectedClosedOn.CustomStyle = "margin-bottom: 0px;";
    input_ExpectedClosedOn.SetEnabled("True");
    input_ExpectedClosedOn.FlexBasis = null; // default value
    input_ExpectedClosedOn.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_ExpectedClosedOn.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Date;
    input_ExpectedClosedOn.SetMandatory("False");
    input_ExpectedClosedOn.MaxLength = null;
    input_ExpectedClosedOn.SetPrompt(null); // default value
    input_ExpectedClosedOn.RowSpan = null; // default value
    input_ExpectedClosedOn.SetStyle("\"form-control\""); // default value
    input_ExpectedClosedOn.SetVariable("GetOrder.List.Current.Order.ExpectedClosedOn");
    input_ExpectedClosedOn.Width = "(fill parent)"; // default value
    var builtinEvent10 = input_ExpectedClosedOn.OnChange;
    builtinEvent10.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent10.Destination = null; // default value
    builtinEvent10.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container18 = isEditing6.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var expression7 = container18.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression7.ColumnSpan = null; // default value
    expression7.CustomStyle = null; // default value
    expression7.Example = "2022-1-30";
    expression7.FlexBasis = null; // default value
    expression7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression7.RowSpan = null; // default value
    expression7.SetStyle(null); // default value
    expression7.SetValue("If(GetOrder.List.Current.Order.ExpectedClosedOn <> NullDate(), GetOrder.List.Current.Order.ExpectedClosedOn, \"-\")");
    expression7.Width = ""; // default value
    
    var container19 = formOrder.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container19.SetStyle("\"margin-top-base\"");
    container19.SetVisible("True"); // default value
    container19.VisibleWasExpression = false; // default value
    container19.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container19.Width = "(fill parent)"; // default value
    
    var label5 = container19.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    label5.Width = "(fill parent)"; // default value
    
    var textWidget18 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label5.Widgets.First();
    textWidget18.CustomStyle = null; // default value
    textWidget18.Name = null; // default value
    textWidget18.SetStyleClasses(null); // default value
    textWidget18.Text = "Required On";
    
    var container20 = container19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container20.SetStyle(null); // default value
    container20.SetVisible("True"); // default value
    container20.VisibleWasExpression = false; // default value
    container20.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container20.Width = "(fill parent)"; // default value
    
    var isEditing5 = container20.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("IsEditing5");
    isEditing5.Animate = false; // default value
    isEditing5.SetCondition("IsEditing");
    isEditing5.CustomStyle = null; // default value
    isEditing5.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var input_RequiredDate = isEditing5.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_RequiredDate");
    input_RequiredDate.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_RequiredDate.ColumnSpan = null; // default value
    input_RequiredDate.CustomStyle = "margin-bottom: 0px;";
    input_RequiredDate.SetEnabled("True");
    input_RequiredDate.FlexBasis = null; // default value
    input_RequiredDate.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_RequiredDate.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Datetime;
    input_RequiredDate.SetMandatory("False");
    input_RequiredDate.MaxLength = null;
    input_RequiredDate.SetPrompt(null); // default value
    input_RequiredDate.RowSpan = null; // default value
    input_RequiredDate.SetStyle("\"form-control\""); // default value
    input_RequiredDate.SetVariable("GetOrder.List.Current.Order.RequiredOn");
    input_RequiredDate.Width = "(fill parent)"; // default value
    var builtinEvent11 = input_RequiredDate.OnChange;
    builtinEvent11.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent11.Destination = null; // default value
    builtinEvent11.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var container21 = isEditing5.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    container21.SetStyle(null); // default value
    container21.SetVisible("True"); // default value
    container21.VisibleWasExpression = false; // default value
    container21.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container21.Width = "(fill parent)"; // default value
    
    var expression8 = container21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression8.ColumnSpan = null; // default value
    expression8.CustomStyle = null; // default value
    expression8.Example = null; // default value
    expression8.FlexBasis = null; // default value
    expression8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression8.RowSpan = null; // default value
    expression8.SetStyle(null); // default value
    expression8.SetValue("GetOrder.List.Current.Order.RequiredOn");
    expression8.Width = ""; // default value
    
    var container22 = formOrder.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
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
    
    var orderExists3 = container22.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("OrderExists3");
    orderExists3.Animate = false; // default value
    orderExists3.SetCondition("GetOrder.List.Current.Order.ShippedOn <> NullDate() and\nGetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Closed");
    orderExists3.CustomStyle = null; // default value
    orderExists3.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var label6 = orderExists3.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILabel>();
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
    
    var textWidget19 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)label6.Widgets.First();
    textWidget19.CustomStyle = null; // default value
    textWidget19.Name = null; // default value
    textWidget19.SetStyleClasses(null); // default value
    textWidget19.Text = "Shipped On";
    
    var container23 = orderExists3.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container23.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container23.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container23.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container23.Animate = false; // default value
    container23.ColumnGap = null; // default value
    container23.ColumnSpan = null; // default value
    container23.CustomStyle = null; // default value
    container23.FlexBasis = null; // default value
    container23.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container23.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container23.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container23.Gap = null;
    container23.GridColumns = null; // default value
    container23.GridRows = null; // default value
    container23.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container23.RowGap = null; // default value
    container23.RowSpan = null; // default value
    container23.SetStyle(null); // default value
    container23.SetVisible("True"); // default value
    container23.VisibleWasExpression = false; // default value
    container23.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container23.Width = "(fill parent)"; // default value
    
    var expression9 = container23.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression9.ColumnSpan = null; // default value
    expression9.CustomStyle = null; // default value
    expression9.Example = null; // default value
    expression9.FlexBasis = null; // default value
    expression9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression9.RowSpan = null; // default value
    expression9.SetStyle(null); // default value
    expression9.SetValue("GetOrder.List.Current.Order.ShippedOn");
    expression9.Width = ""; // default value
    
    var placeholderContentWidget16 = columnsSmallLeftInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var formOrderAddress = placeholderContentWidget16.CreateWidget<ServiceStudio.Plugin.NRWidgets.IForm>("FormOrderAddress");
    formOrderAddress.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    formOrderAddress.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    formOrderAddress.ColumnGap = null; // default value
    formOrderAddress.ColumnSpan = null; // default value
    formOrderAddress.CustomStyle = null; // default value
    formOrderAddress.FlexBasis = null; // default value
    formOrderAddress.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    formOrderAddress.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    formOrderAddress.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    formOrderAddress.Gap = null;
    formOrderAddress.GridColumns = null; // default value
    formOrderAddress.GridRows = null; // default value
    formOrderAddress.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    formOrderAddress.RowGap = null; // default value
    formOrderAddress.RowSpan = null; // default value
    formOrderAddress.SetStyle("\"form\"");
    formOrderAddress.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    formOrderAddress.Width = "(fill parent)"; // default value
    
    var container24 = formOrderAddress.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container24.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container24.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container24.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container24.Animate = false; // default value
    container24.ColumnGap = null; // default value
    container24.ColumnSpan = null; // default value
    container24.CustomStyle = null; // default value
    container24.FlexBasis = null; // default value
    container24.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container24.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container24.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container24.Gap = null;
    container24.GridColumns = null; // default value
    container24.GridRows = null; // default value
    container24.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container24.RowGap = null; // default value
    container24.RowSpan = null; // default value
    container24.SetStyle(null); // default value
    container24.SetVisible("True"); // default value
    container24.VisibleWasExpression = false; // default value
    container24.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container24.Width = "(fill parent)"; // default value
    
    var orderExists2 = container24.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("OrderExists2");
    orderExists2.Animate = false; // default value
    orderExists2.SetCondition("not GetOrder.List.Empty");
    orderExists2.CustomStyle = null; // default value
    orderExists2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var container25 = orderExists2.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container25.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container25.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container25.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container25.Animate = false; // default value
    container25.ColumnGap = null; // default value
    container25.ColumnSpan = null; // default value
    container25.CustomStyle = null; // default value
    container25.FlexBasis = null; // default value
    container25.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container25.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container25.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container25.Gap = null;
    container25.GridColumns = null; // default value
    container25.GridRows = null; // default value
    container25.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container25.RowGap = null; // default value
    container25.RowSpan = null; // default value
    container25.SetStyle("\"margin-bottom-xl\"");
    container25.SetVisible("True"); // default value
    container25.VisibleWasExpression = false; // default value
    container25.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container25.Width = "(fill parent)"; // default value
    
    var container26 = container25.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container26.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container26.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container26.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container26.Animate = false; // default value
    container26.ColumnGap = null; // default value
    container26.ColumnSpan = null; // default value
    container26.CustomStyle = null; // default value
    container26.FlexBasis = null; // default value
    container26.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container26.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container26.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container26.Gap = null;
    container26.GridColumns = null; // default value
    container26.GridRows = null; // default value
    container26.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container26.RowGap = null; // default value
    container26.RowSpan = null; // default value
    container26.SetStyle(null); // default value
    container26.SetVisible("True"); // default value
    container26.VisibleWasExpression = false; // default value
    container26.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container26.Width = "8 col";
    
    var textWidget20 = container26.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget20.CustomStyle = null; // default value
    textWidget20.SetStyleClasses("\"bold\"");
    textWidget20.Text = "Products";
    
    var tableRecords = container25.CreateWidget<ServiceStudio.Plugin.NRWidgets.ITableRecords>();
    tableRecords.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    tableRecords.ColumnSpan = null; // default value
    tableRecords.CustomStyle = null; // default value
    tableRecords.ExpandedInWebEditor = true; // default value
    tableRecords.FlexBasis = null; // default value
    tableRecords.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    tableRecords.RowSpan = null; // default value
    tableRecords.ShowHeader = true; // default value
    tableRecords.SetSource("GetOrderItemsByOrderId.List");
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
    headerCell.SetSortAttribute(null); // default value
    headerCell.SetSource("GetOrderItemsByOrderId.List");
    headerCell.SetStyle(null); // default value
    headerCell.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget21 = headerCell.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget21.CustomStyle = null; // default value
    textWidget21.SetStyleClasses(null); // default value
    textWidget21.Text = "Product";
    
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
    headerCell2.SetSortAttribute(null); // default value
    headerCell2.SetSource("GetOrderItemsByOrderId.List");
    headerCell2.SetStyle(null); // default value
    headerCell2.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var container27 = headerCell2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container27.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container27.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container27.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container27.Animate = false; // default value
    container27.ColumnGap = null; // default value
    container27.ColumnSpan = null; // default value
    container27.CustomStyle = "text-align: right;";
    container27.FlexBasis = null; // default value
    container27.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container27.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container27.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container27.Gap = null;
    container27.GridColumns = null; // default value
    container27.GridRows = null; // default value
    container27.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container27.RowGap = null; // default value
    container27.RowSpan = null; // default value
    container27.SetStyle(null); // default value
    container27.SetVisible("True"); // default value
    container27.VisibleWasExpression = false; // default value
    container27.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container27.Width = "(fill parent)"; // default value
    
    var textWidget22 = container27.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget22.CustomStyle = null; // default value
    textWidget22.SetStyleClasses(null); // default value
    textWidget22.Text = "Unit Price";
    
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
    headerCell3.SetSortAttribute(null); // default value
    headerCell3.SetSource("GetOrderItemsByOrderId.List");
    headerCell3.SetStyle(null); // default value
    headerCell3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget23 = headerCell3.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget23.CustomStyle = null; // default value
    textWidget23.SetStyleClasses(null); // default value
    textWidget23.Text = "Units";
    
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
    headerCell4.SetSortAttribute(null); // default value
    headerCell4.SetSource("GetOrderItemsByOrderId.List");
    headerCell4.SetStyle(null); // default value
    headerCell4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var textWidget24 = headerCell4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget24.CustomStyle = null; // default value
    textWidget24.SetStyleClasses(null); // default value
    textWidget24.Text = "Discount";
    
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
    headerCell5.SetSortAttribute(null); // default value
    headerCell5.SetSource("GetOrderItemsByOrderId.List");
    headerCell5.SetStyle(null); // default value
    headerCell5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
    var container28 = headerCell5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container28.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container28.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container28.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container28.Animate = false; // default value
    container28.ColumnGap = null; // default value
    container28.ColumnSpan = null; // default value
    container28.CustomStyle = "text-align: right;";
    container28.FlexBasis = null; // default value
    container28.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container28.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container28.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container28.Gap = null;
    container28.GridColumns = null; // default value
    container28.GridRows = null; // default value
    container28.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container28.RowGap = null; // default value
    container28.RowSpan = null; // default value
    container28.SetStyle(null); // default value
    container28.SetVisible("True"); // default value
    container28.VisibleWasExpression = false; // default value
    container28.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container28.Width = "(fill parent)"; // default value
    
    var textWidget25 = container28.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget25.CustomStyle = null; // default value
    textWidget25.SetStyleClasses(null); // default value
    textWidget25.Text = "Total";
    
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
    headerCell6.SetSortAttribute(null); // default value
    headerCell6.SetSource("GetOrderItemsByOrderId.List");
    headerCell6.SetStyle(null); // default value
    headerCell6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    
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
    
    var ifWidget6 = rowCell.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>();
    ifWidget6.Animate = false; // default value
    ifWidget6.SetCondition("GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Draft or\nGetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Rejected\n");
    ifWidget6.CustomStyle = null; // default value
    ifWidget6.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var link7 = ifWidget6.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link7.Widgets.ToList().ForEach(x => x.Delete());
    link7.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link7.ColumnGap = null; // default value
    link7.ColumnSpan = null; // default value
    link7.SetConfirmationMessage(null); // default value
    link7.CustomStyle = null; // default value
    link7.SetEnabled("True");
    link7.FlexBasis = null; // default value
    link7.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link7.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link7.Gap = null;
    link7.GridColumns = null; // default value
    link7.GridRows = null; // default value
    link7.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link7.RowGap = null; // default value
    link7.RowSpan = null; // default value
    link7.SetStyle(null); // default value
    link7.SetVisible("True"); // default value
    link7.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link7.Width = ""; // default value
    
    var expression10 = link7.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression10.ColumnSpan = null; // default value
    expression10.CustomStyle = null; // default value
    expression10.Example = null; // default value
    expression10.FlexBasis = null; // default value
    expression10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression10.RowSpan = null; // default value
    expression10.SetStyle("\"text-ellipsis text-white-space-break\"");
    expression10.SetValue("GetOrderItemsByOrderId.List.Current.Product.Name");
    expression10.Width = ""; // default value
    var builtinEvent12 = link7.OnClick;
    builtinEvent12.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent12.Destination = product_ToggleEdit2;
    builtinEvent12.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent12.SetArgumentValue(toggleOnly, "False");
    
    var expression11 = ifWidget6.FalseBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression11.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression11.ColumnSpan = null; // default value
    expression11.CustomStyle = null; // default value
    expression11.Example = null; // default value
    expression11.FlexBasis = null; // default value
    expression11.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression11.RowSpan = null; // default value
    expression11.SetStyle("\"text-ellipsis text-white-space-break\"");
    expression11.SetValue("GetOrderItemsByOrderId.List.Current.Product.Name");
    expression11.Width = ""; // default value
    
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
    
    var container29 = rowCell2.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container29.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Right;
    container29.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container29.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container29.Animate = false; // default value
    container29.ColumnGap = null; // default value
    container29.ColumnSpan = null; // default value
    container29.CustomStyle = null; // default value
    container29.FlexBasis = null; // default value
    container29.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container29.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container29.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container29.Gap = null;
    container29.GridColumns = null; // default value
    container29.GridRows = null; // default value
    container29.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container29.RowGap = null; // default value
    container29.RowSpan = null; // default value
    container29.SetStyle(null); // default value
    container29.SetVisible("True"); // default value
    container29.VisibleWasExpression = false; // default value
    container29.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container29.Width = "(fill parent)"; // default value
    
    var expression12 = container29.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression12.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression12.ColumnSpan = null; // default value
    expression12.CustomStyle = null; // default value
    expression12.Example = null; // default value
    expression12.FlexBasis = null; // default value
    expression12.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression12.RowSpan = null; // default value
    expression12.SetStyle("\"text-ellipsis\"");
    expression12.SetValue("FormatCurrency(GetOrderItemsByOrderId.List.Current.Product.Price, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)\n");
    expression12.Width = ""; // default value
    
    var rowCell3 = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell3.ColumnGap = null; // default value
    rowCell3.ColumnSpan = null; // default value
    rowCell3.CustomStyle = "text-align: left;";
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
    
    var expression13 = rowCell3.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression13.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression13.ColumnSpan = null; // default value
    expression13.CustomStyle = null; // default value
    expression13.Example = null; // default value
    expression13.FlexBasis = null; // default value
    expression13.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression13.RowSpan = null; // default value
    expression13.SetStyle(null); // default value
    expression13.SetValue("GetOrderItemsByOrderId.List.Current.OrderItem.Quantity");
    expression13.Width = ""; // default value
    
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
    
    var alignCenterInstance2 = rowCell4.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    alignCenterInstance2.CustomStyle = null; // default value
    alignCenterInstance2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    alignCenterInstance2.SourceBlock = alignCenter;
    alignCenterInstance2.SetArgumentValue(isHorizontal, null);
    alignCenterInstance2.SetArgumentValue(extendedClass, null); // default value
    
    var placeholderContentWidget17 = alignCenterInstance2.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var expression14 = placeholderContentWidget17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression14.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression14.ColumnSpan = null; // default value
    expression14.CustomStyle = null; // default value
    expression14.Example = null; // default value
    expression14.FlexBasis = null; // default value
    expression14.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression14.RowSpan = null; // default value
    expression14.SetStyle(null); // default value
    expression14.SetValue("GetOrderItemsByOrderId.List.Current.OrderItem.Discount");
    expression14.Width = ""; // default value
    
    var expression15 = placeholderContentWidget17.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression15.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression15.ColumnSpan = null; // default value
    expression15.CustomStyle = null; // default value
    expression15.Example = null; // default value
    expression15.FlexBasis = null; // default value
    expression15.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression15.RowSpan = null; // default value
    expression15.SetStyle(null); // default value
    expression15.SetValue("\" %\"");
    expression15.Width = ""; // default value
    
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
    
    var container30 = rowCell5.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container30.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container30.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container30.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container30.Animate = false; // default value
    container30.ColumnGap = null; // default value
    container30.ColumnSpan = null; // default value
    container30.CustomStyle = "text-align: right;";
    container30.FlexBasis = null; // default value
    container30.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container30.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container30.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container30.Gap = null;
    container30.GridColumns = null; // default value
    container30.GridRows = null; // default value
    container30.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container30.RowGap = null; // default value
    container30.RowSpan = null; // default value
    container30.SetStyle(null); // default value
    container30.SetVisible("True"); // default value
    container30.VisibleWasExpression = false; // default value
    container30.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container30.Width = "(fill parent)"; // default value
    
    var expression16 = container30.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression16.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression16.ColumnSpan = null; // default value
    expression16.CustomStyle = null; // default value
    expression16.Example = "$151.2";
    expression16.FlexBasis = null; // default value
    expression16.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression16.RowSpan = null; // default value
    expression16.SetStyle("\"text-ellipsis\"");
    expression16.SetValue("FormatCurrency(GetOrderItemsByOrderId.List.Current.OrderItem.Amount, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
    expression16.Width = ""; // default value
    
    var rowCell6 = tableRecords.Row.CreateWidget<ServiceStudio.Plugin.NRWidgets.IRowCell>();
    rowCell6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    rowCell6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    rowCell6.ColumnGap = null; // default value
    rowCell6.ColumnSpan = null; // default value
    rowCell6.CustomStyle = "width: 70px;";
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
    
    var draft3 = rowCell6.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("Draft3");
    draft3.Animate = false; // default value
    draft3.SetCondition("GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Draft or \nGetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Rejected");
    draft3.CustomStyle = null; // default value
    draft3.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var container31 = draft3.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container31.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container31.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container31.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container31.Animate = false; // default value
    container31.ColumnGap = null; // default value
    container31.ColumnSpan = null; // default value
    container31.CustomStyle = "text-align: right;";
    container31.FlexBasis = null; // default value
    container31.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container31.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container31.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container31.Gap = null;
    container31.GridColumns = null; // default value
    container31.GridRows = null; // default value
    container31.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container31.RowGap = null; // default value
    container31.RowSpan = null; // default value
    container31.SetStyle(null); // default value
    container31.SetVisible("True"); // default value
    container31.VisibleWasExpression = false; // default value
    container31.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container31.Width = "(fill parent)"; // default value
    
    var link8 = container31.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link8.Widgets.ToList().ForEach(x => x.Delete());
    link8.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link8.ColumnGap = null; // default value
    link8.ColumnSpan = null; // default value
    link8.SetConfirmationMessage(null); // default value
    link8.CustomStyle = null; // default value
    link8.SetEnabled("True");
    link8.FlexBasis = null; // default value
    link8.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link8.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link8.Gap = null;
    link8.GridColumns = null; // default value
    link8.GridRows = null; // default value
    link8.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link8.RowGap = null; // default value
    link8.RowSpan = null; // default value
    link8.SetStyle(null); // default value
    link8.SetVisible("True"); // default value
    link8.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link8.Width = ""; // default value
    
    var icon7 = link8.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon7.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon7.ColumnSpan = null; // default value
    icon7.CustomStyle = null; // default value
    icon7.FlexBasis = null; // default value
    icon7.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon7.Icon = "trash";
    icon7.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon7.RowSpan = null; // default value
    icon7.SetStyle("\"icon text-red-dark heading6\"");
    icon7.SetVisible("True"); // default value
    icon7.Weight = null; // default value
    var builtinEvent13 = link8.OnClick;
    builtinEvent13.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent13.Destination = product_Delete;
    builtinEvent13.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var builtinEvent14 = tableRecords.OnSort;
    builtinEvent14.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent14.Destination = null; // default value
    builtinEvent14.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    var clickedColumn = builtinEvent14.InputParameters.Named("ClickedColumn");
    clickedColumn.DataType = eSpace.TextType;
    clickedColumn.Description = null; // default value
    clickedColumn.IsMandatory = true; // default value
    clickedColumn.Name = "ClickedColumn";
    
    var draft2 = container25.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("Draft2");
    draft2.Animate = false; // default value
    draft2.SetCondition("GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Draft or\nGetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Rejected");
    draft2.CustomStyle = null; // default value
    draft2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container32 = draft2.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container32.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container32.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container32.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container32.Animate = false; // default value
    container32.ColumnGap = null; // default value
    container32.ColumnSpan = null; // default value
    container32.CustomStyle = null; // default value
    container32.FlexBasis = null; // default value
    container32.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container32.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container32.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container32.Gap = null;
    container32.GridColumns = null; // default value
    container32.GridRows = null; // default value
    container32.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container32.RowGap = null; // default value
    container32.RowSpan = null; // default value
    container32.SetStyle("\"margin-top-s\"");
    container32.SetVisible("True"); // default value
    container32.VisibleWasExpression = false; // default value
    container32.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container32.Width = "(fill parent)"; // default value
    
    var link9 = container32.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    /*** Deleting Widgets that are created by default when instantiating an ILink ***/
    link9.Widgets.ToList().ForEach(x => x.Delete());
    link9.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link9.ColumnGap = null; // default value
    link9.ColumnSpan = null; // default value
    link9.SetConfirmationMessage(null); // default value
    link9.CustomStyle = null; // default value
    link9.SetEnabled("True");
    link9.FlexBasis = null; // default value
    link9.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link9.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link9.Gap = null;
    link9.GridColumns = null; // default value
    link9.GridRows = null; // default value
    link9.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link9.RowGap = null; // default value
    link9.RowSpan = null; // default value
    link9.SetStyle(null); // default value
    link9.SetVisible("True"); // default value
    link9.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link9.Width = ""; // default value
    
    var icon8 = link9.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon8.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon8.ColumnSpan = null; // default value
    icon8.CustomStyle = null; // default value
    icon8.FlexBasis = null; // default value
    icon8.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon8.Icon = "plus-circle";
    icon8.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon8.RowSpan = null; // default value
    icon8.SetStyle("\"icon\""); // default value
    icon8.SetVisible("True"); // default value
    icon8.Weight = null; // default value
    
    var textWidget26 = link9.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget26.CustomStyle = "margin-left: 10px;";
    textWidget26.SetStyleClasses(null); // default value
    textWidget26.Text = "Add new product";
    var builtinEvent15 = link9.OnClick;
    builtinEvent15.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.ValidateAndContinue;
    builtinEvent15.Destination = product_ToggleEdit2;
    builtinEvent15.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent15.SetArgumentValue(toggleOnly, "True");
    
    var columnsSmallRightInstance = container24.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    columnsSmallRightInstance.CustomStyle = null; // default value
    columnsSmallRightInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var columnsSmallRight = adaptive.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("ColumnsSmallRight");
    columnsSmallRightInstance.SourceBlock = columnsSmallRight;
    var tabletBehavior2 = columnsSmallRight.InputParameters.Named("TabletBehavior");
    columnsSmallRightInstance.SetArgumentValue(tabletBehavior2, null);
    var gutterSize2 = columnsSmallRight.InputParameters.Named("GutterSize");
    columnsSmallRightInstance.SetArgumentValue(gutterSize2, null);
    var extendedClass5 = columnsSmallRight.InputParameters.Named("ExtendedClass");
    columnsSmallRightInstance.SetArgumentValue(extendedClass5, null); // default value
    var phoneBehavior2 = columnsSmallRight.InputParameters.Named("PhoneBehavior");
    columnsSmallRightInstance.SetArgumentValue(phoneBehavior2, null);
    
    var placeholderContentWidget18 = columnsSmallRightInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Column2");
    var orderExists = placeholderContentWidget18.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("OrderExists");
    orderExists.Animate = false; // default value
    orderExists.SetCondition("not GetOrder.List.Empty");
    orderExists.CustomStyle = null; // default value
    orderExists.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var container33 = orderExists.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container33.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container33.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container33.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container33.Animate = false; // default value
    container33.ColumnGap = null; // default value
    container33.ColumnSpan = null; // default value
    container33.CustomStyle = "text-align: right;";
    container33.FlexBasis = null; // default value
    container33.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container33.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container33.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container33.Gap = null;
    container33.GridColumns = null; // default value
    container33.GridRows = null; // default value
    container33.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container33.RowGap = null; // default value
    container33.RowSpan = null; // default value
    container33.SetStyle(null); // default value
    container33.SetVisible("True"); // default value
    container33.VisibleWasExpression = false; // default value
    container33.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container33.Width = "(fill parent)"; // default value
    
    var textWidget27 = container33.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget27.CustomStyle = "color: #868e96; font-weight: bold;";
    textWidget27.SetStyleClasses(null); // default value
    textWidget27.Text = "Total amount";
    
    var container34 = orderExists.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container34.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container34.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container34.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container34.Animate = false; // default value
    container34.ColumnGap = null; // default value
    container34.ColumnSpan = null; // default value
    container34.CustomStyle = "text-align: right;";
    container34.FlexBasis = null; // default value
    container34.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container34.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container34.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container34.Gap = null;
    container34.GridColumns = null; // default value
    container34.GridRows = null; // default value
    container34.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container34.RowGap = null; // default value
    container34.RowSpan = null; // default value
    container34.SetStyle("\"heading2 text-neutral-10 font-bold\"");
    container34.SetVisible("True"); // default value
    container34.VisibleWasExpression = false; // default value
    container34.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container34.Width = "(fill parent)"; // default value
    
    var totalAmount = container34.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>("TotalAmount");
    totalAmount.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    totalAmount.ColumnSpan = null; // default value
    totalAmount.CustomStyle = null; // default value
    totalAmount.Example = "$ 151.2";
    totalAmount.FlexBasis = null; // default value
    totalAmount.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    totalAmount.RowSpan = null; // default value
    totalAmount.SetStyle(null); // default value
    totalAmount.SetValue("FormatCurrency(GetOrder.List.Current.Order.TotalAmount, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
    totalAmount.Width = ""; // default value
    
    var if_IsOrderManager = mainContent.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("If_IsOrderManager");
    if_IsOrderManager.Animate = false; // default value
    if_IsOrderManager.SetCondition("Client.IsOrderManager");
    if_IsOrderManager.CustomStyle = null; // default value
    if_IsOrderManager.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var container35 = if_IsOrderManager.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container35.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container35.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container35.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container35.Animate = false; // default value
    container35.ColumnGap = null; // default value
    container35.ColumnSpan = null; // default value
    container35.CustomStyle = "margin-top: 10px; padding: 0px; text-align: right;";
    container35.FlexBasis = null; // default value
    container35.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container35.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container35.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container35.Gap = null;
    container35.GridColumns = null; // default value
    container35.GridRows = null; // default value
    container35.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container35.RowGap = null; // default value
    container35.RowSpan = null; // default value
    container35.SetStyle(null); // default value
    container35.SetVisible("True"); // default value
    container35.VisibleWasExpression = false; // default value
    container35.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container35.Width = "(fill parent)"; // default value
    
    var draftStatus = container35.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("DraftStatus");
    draftStatus.Animate = false; // default value
    draftStatus.SetCondition("(GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Draft or\nGetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Rejected) and \nnot GetOrderItemsByOrderId.List.Empty");
    draftStatus.CustomStyle = null; // default value
    draftStatus.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowTrueOrPreview;
    
    var button3 = draftStatus.TrueBranch.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    button3.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button3.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button3.ColumnGap = null; // default value
    button3.ColumnSpan = null; // default value
    button3.SetConfirmationMessage(null); // default value
    button3.CustomStyle = null; // default value
    button3.SetEnabled("True");
    button3.FlexBasis = null; // default value
    button3.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button3.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button3.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button3.Gap = null;
    button3.GridColumns = null; // default value
    button3.GridRows = null; // default value
    button3.IsDefault = false; // default value
    button3.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button3.RowGap = null; // default value
    button3.RowSpan = null; // default value
    button3.SetStyle("\"btn background-primary\"");
    button3.SetVisible("True"); // default value
    button3.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button3.Width = ""; // default value
    
    var textWidget28 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button3.Widgets.First();
    textWidget28.CustomStyle = null; // default value
    textWidget28.Name = null; // default value
    textWidget28.SetStyleClasses(null); // default value
    textWidget28.Text = "Submit";
    var builtinEvent16 = button3.OnClick;
    builtinEvent16.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent16.Destination = order_SetStatus;
    builtinEvent16.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent16.SetArgumentValue(orderStatusId, "Entities.OrderStatus.Submitted");
    
    var extendedProperty = button3.CreateExtendedProperty();
    extendedProperty.Property = "title";
    extendedProperty.SetValue("\"Enabled when you add products to the Order\"");
    
    var if_OrderIsSubmitted = container35.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("If_OrderIsSubmitted");
    if_OrderIsSubmitted.Animate = false; // default value
    if_OrderIsSubmitted.SetCondition("GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Submitted");
    if_OrderIsSubmitted.CustomStyle = null; // default value
    if_OrderIsSubmitted.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var alignCenterInstance3 = if_OrderIsSubmitted.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    alignCenterInstance3.CustomStyle = null; // default value
    alignCenterInstance3.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    alignCenterInstance3.SourceBlock = alignCenter;
    alignCenterInstance3.SetArgumentValue(isHorizontal, null);
    alignCenterInstance3.SetArgumentValue(extendedClass, null); // default value
    
    var placeholderContentWidget19 = alignCenterInstance3.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var button4 = placeholderContentWidget19.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    button4.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button4.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button4.ColumnGap = null; // default value
    button4.ColumnSpan = null; // default value
    button4.SetConfirmationMessage(null); // default value
    button4.CustomStyle = null; // default value
    button4.SetEnabled("True");
    button4.FlexBasis = null; // default value
    button4.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button4.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button4.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button4.Gap = null;
    button4.GridColumns = null; // default value
    button4.GridRows = null; // default value
    button4.IsDefault = false; // default value
    button4.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button4.RowGap = null; // default value
    button4.RowSpan = null; // default value
    button4.SetStyle("\"btn background-primary margin-right-m\"");
    button4.SetVisible("GetOrder.List.Current.Order.OrderStatusId = Entities.OrderStatus.Submitted");
    button4.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button4.Width = ""; // default value
    
    var textWidget29 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button4.Widgets.First();
    textWidget29.CustomStyle = null; // default value
    textWidget29.Name = null; // default value
    textWidget29.SetStyleClasses(null); // default value
    textWidget29.Text = "Approve";
    var builtinEvent17 = button4.OnClick;
    builtinEvent17.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent17.Destination = order_SetStatus;
    builtinEvent17.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent17.SetArgumentValue(orderStatusId, "Entities.OrderStatus.Closed");
    
    var textWidget30 = placeholderContentWidget19.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget30.CustomStyle = "margin-left: 5px; margin-right: 5px;";
    textWidget30.SetStyleClasses("\"margin-right-m\"");
    textWidget30.Text = "or";
    
    var link10 = placeholderContentWidget19.CreateWidget<ServiceStudio.Plugin.NRWidgets.ILink>();
    link10.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    link10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    link10.ColumnGap = null; // default value
    link10.ColumnSpan = null; // default value
    link10.SetConfirmationMessage(null); // default value
    link10.CustomStyle = null; // default value
    link10.SetEnabled("True");
    link10.FlexBasis = null; // default value
    link10.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    link10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    link10.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    link10.Gap = null;
    link10.GridColumns = null; // default value
    link10.GridRows = null; // default value
    link10.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    link10.RowGap = null; // default value
    link10.RowSpan = null; // default value
    link10.SetStyle(null); // default value
    link10.SetVisible("True"); // default value
    link10.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    link10.Width = ""; // default value
    
    var textWidget31 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)link10.Widgets.First();
    textWidget31.CustomStyle = null; // default value
    textWidget31.Name = null; // default value
    textWidget31.SetStyleClasses(null); // default value
    textWidget31.Text = "Reject";
    var builtinEvent18 = link10.OnClick;
    builtinEvent18.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent18.Destination = order_SetStatus;
    builtinEvent18.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent18.SetArgumentValue(orderStatusId, "Entities.OrderStatus.Rejected");
    
    var popup_EditingProduct = placeholderContentWidget7.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>("Popup_EditingProduct");
    popup_EditingProduct.CustomStyle = null; // default value
    popup_EditingProduct.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var interaction = outSystemsUI.MobileFlows.Named("Interaction");
    var sidebar = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("Sidebar");
    popup_EditingProduct.SourceBlock = sidebar;
    var hasOverlay = sidebar.InputParameters.Named("HasOverlay");
    popup_EditingProduct.SetArgumentValue(hasOverlay, null);
    var startsOpen = sidebar.InputParameters.Named("StartsOpen");
    popup_EditingProduct.SetArgumentValue(startsOpen, null);
    var width = sidebar.InputParameters.Named("Width");
    popup_EditingProduct.SetArgumentValue(width, null);
    var direction = sidebar.InputParameters.Named("Direction");
    popup_EditingProduct.SetArgumentValue(direction, null);
    var extendedClass6 = sidebar.InputParameters.Named("ExtendedClass");
    popup_EditingProduct.SetArgumentValue(extendedClass6, null); // default value
    var eventHandler4 = popup_EditingProduct.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnToggle");
    eventHandler4.Handler = null; // default value
    var eventHandler5 = popup_EditingProduct.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler5.Handler = null; // default value
    
    var placeholderContentWidget20 = popup_EditingProduct.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Header");
    var container36 = placeholderContentWidget20.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container36.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container36.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container36.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container36.Animate = false; // default value
    container36.ColumnGap = null; // default value
    container36.ColumnSpan = null; // default value
    container36.CustomStyle = "font-size: 24px;";
    container36.FlexBasis = null; // default value
    container36.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container36.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container36.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container36.Gap = null;
    container36.GridColumns = null; // default value
    container36.GridRows = null; // default value
    container36.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container36.RowGap = null; // default value
    container36.RowSpan = null; // default value
    container36.SetStyle(null); // default value
    container36.SetVisible("True"); // default value
    container36.VisibleWasExpression = false; // default value
    container36.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container36.Width = "(fill parent)"; // default value
    
    var icon9 = container36.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon9.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon9.ColumnSpan = null; // default value
    icon9.CustomStyle = null; // default value
    icon9.FlexBasis = null; // default value
    icon9.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon9.Icon = "edit";
    icon9.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon9.RowSpan = null; // default value
    icon9.SetStyle("\"icon\""); // default value
    icon9.SetVisible("True"); // default value
    icon9.Weight = null; // default value
    
    var textWidget32 = container36.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget32.CustomStyle = "font-weight: bold; margin-left: 5px;";
    textWidget32.SetStyleClasses(null); // default value
    textWidget32.Text = "Editing Product";
    
    var placeholderContentWidget21 = popup_EditingProduct.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Content");
    var formProduct = placeholderContentWidget21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IForm>("FormProduct");
    formProduct.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    formProduct.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    formProduct.ColumnGap = null; // default value
    formProduct.ColumnSpan = null; // default value
    formProduct.CustomStyle = null; // default value
    formProduct.FlexBasis = null; // default value
    formProduct.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    formProduct.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    formProduct.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    formProduct.Gap = null;
    formProduct.GridColumns = null; // default value
    formProduct.GridRows = null; // default value
    formProduct.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    formProduct.RowGap = null; // default value
    formProduct.RowSpan = null; // default value
    formProduct.SetStyle("\"form\"");
    formProduct.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    formProduct.Width = "(fill parent)"; // default value
    
    var container37 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container37.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container37.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container37.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container37.Animate = false; // default value
    container37.ColumnGap = null; // default value
    container37.ColumnSpan = null; // default value
    container37.CustomStyle = null; // default value
    container37.FlexBasis = null; // default value
    container37.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container37.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container37.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container37.Gap = null;
    container37.GridColumns = null; // default value
    container37.GridRows = null; // default value
    container37.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container37.RowGap = null; // default value
    container37.RowSpan = null; // default value
    container37.SetStyle(null); // default value
    container37.SetVisible("True"); // default value
    container37.VisibleWasExpression = false; // default value
    container37.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container37.Width = "(fill parent)"; // default value
    
    var textWidget33 = container37.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget33.CustomStyle = null; // default value
    textWidget33.SetStyleClasses(null); // default value
    textWidget33.Text = "Product";
    
    var container38 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container38.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container38.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container38.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container38.Animate = false; // default value
    container38.ColumnGap = null; // default value
    container38.ColumnSpan = null; // default value
    container38.CustomStyle = "margin-top: 5px;";
    container38.FlexBasis = null; // default value
    container38.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container38.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container38.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container38.Gap = null;
    container38.GridColumns = null; // default value
    container38.GridRows = null; // default value
    container38.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container38.RowGap = null; // default value
    container38.RowSpan = null; // default value
    container38.SetStyle(null); // default value
    container38.SetVisible("True"); // default value
    container38.VisibleWasExpression = false; // default value
    container38.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container38.Width = "(fill parent)"; // default value
    
    var dropdownSearchInstance = container38.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    dropdownSearchInstance.CustomStyle = null; // default value
    dropdownSearchInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var dropdownSearch = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("DropdownSearch");
    dropdownSearchInstance.SourceBlock = dropdownSearch;
    var startingSelection = dropdownSearch.InputParameters.Named("StartingSelection");
    dropdownSearchInstance.SetArgumentValue(startingSelection, "[ { Value: OrderDetailsEdit.Product.Id, Label: OrderDetailsEdit.Product.Name } ]");
    var extendedClass7 = dropdownSearch.InputParameters.Named("ExtendedClass");
    dropdownSearchInstance.SetArgumentValue(extendedClass7, null); // default value
    var prompt = dropdownSearch.InputParameters.Named("Prompt");
    dropdownSearchInstance.SetArgumentValue(prompt, "\"- Product -\"");
    var optionsList = dropdownSearch.InputParameters.Named("OptionsList");
    dropdownSearchInstance.SetArgumentValue(optionsList, "GetProducts.List mapTo { Value: Product.Id, Label: Product.Name, Description: Product.Description }");
    var optionalConfigs = dropdownSearch.InputParameters.Named("OptionalConfigs");
    dropdownSearchInstance.SetArgumentValue(optionalConfigs, "{ SearchPrompt: \"Search for a product\" }");
    var eventHandler6 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "OnChanged");
    eventHandler6.Handler = orderItem_OnChange;
    eventHandler6.SetArgumentValue(selectedOptionList, "SelectedOptionList");
    var eventHandler7 = dropdownSearchInstance.EventHandlers.FirstOrDefault(e => e.Event.Name == "Initialized");
    eventHandler7.Handler = null; // default value
    
    var container39 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container39.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container39.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container39.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container39.Animate = false; // default value
    container39.ColumnGap = null; // default value
    container39.ColumnSpan = null; // default value
    container39.CustomStyle = "margin-top: 20px;";
    container39.FlexBasis = null; // default value
    container39.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container39.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container39.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container39.Gap = null;
    container39.GridColumns = null; // default value
    container39.GridRows = null; // default value
    container39.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container39.RowGap = null; // default value
    container39.RowSpan = null; // default value
    container39.SetStyle(null); // default value
    container39.SetVisible("True"); // default value
    container39.VisibleWasExpression = false; // default value
    container39.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container39.Width = "(fill parent)"; // default value
    
    var textWidget34 = container39.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget34.CustomStyle = null; // default value
    textWidget34.SetStyleClasses(null); // default value
    textWidget34.Text = "Unit Price";
    
    var container40 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container40.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container40.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container40.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container40.Animate = false; // default value
    container40.ColumnGap = null; // default value
    container40.ColumnSpan = null; // default value
    container40.CustomStyle = null; // default value
    container40.FlexBasis = null; // default value
    container40.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container40.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container40.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container40.Gap = null;
    container40.GridColumns = null; // default value
    container40.GridRows = null; // default value
    container40.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container40.RowGap = null; // default value
    container40.RowSpan = null; // default value
    container40.SetStyle(null); // default value
    container40.SetVisible("True"); // default value
    container40.VisibleWasExpression = false; // default value
    container40.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container40.Width = "(fill parent)"; // default value
    
    var expression17 = container40.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression17.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression17.ColumnSpan = null; // default value
    expression17.CustomStyle = null; // default value
    expression17.Example = null; // default value
    expression17.FlexBasis = null; // default value
    expression17.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression17.RowSpan = null; // default value
    expression17.SetStyle(null); // default value
    expression17.SetValue("FormatCurrency(OrderDetailsEdit.Product.Price, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
    expression17.Width = ""; // default value
    
    var container41 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container41.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container41.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container41.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container41.Animate = false; // default value
    container41.ColumnGap = null; // default value
    container41.ColumnSpan = null; // default value
    container41.CustomStyle = "margin-top: 20px;";
    container41.FlexBasis = null; // default value
    container41.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container41.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container41.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container41.Gap = null;
    container41.GridColumns = null; // default value
    container41.GridRows = null; // default value
    container41.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container41.RowGap = null; // default value
    container41.RowSpan = null; // default value
    container41.SetStyle(null); // default value
    container41.SetVisible("True"); // default value
    container41.VisibleWasExpression = false; // default value
    container41.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container41.Width = "(fill parent)"; // default value
    
    var textWidget35 = container41.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget35.CustomStyle = null; // default value
    textWidget35.SetStyleClasses(null); // default value
    textWidget35.Text = "Units";
    
    var container42 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container42.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container42.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container42.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container42.Animate = false; // default value
    container42.ColumnGap = null; // default value
    container42.ColumnSpan = null; // default value
    container42.CustomStyle = "margin-top: 5px;";
    container42.FlexBasis = null; // default value
    container42.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container42.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container42.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container42.Gap = null;
    container42.GridColumns = null; // default value
    container42.GridRows = null; // default value
    container42.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container42.RowGap = null; // default value
    container42.RowSpan = null; // default value
    container42.SetStyle(null); // default value
    container42.SetVisible("True"); // default value
    container42.VisibleWasExpression = false; // default value
    container42.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container42.Width = "(fill parent)"; // default value
    
    var input_Quantity = container42.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_Quantity");
    input_Quantity.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_Quantity.ColumnSpan = null; // default value
    input_Quantity.CustomStyle = null; // default value
    input_Quantity.SetEnabled("True");
    input_Quantity.FlexBasis = null; // default value
    input_Quantity.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_Quantity.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Number;
    input_Quantity.SetMandatory("IsAddingProduct");
    input_Quantity.MaxLength = null;
    input_Quantity.SetPrompt(null); // default value
    input_Quantity.RowSpan = null; // default value
    input_Quantity.SetStyle("\"form-control\""); // default value
    input_Quantity.SetVariable("OrderDetailsEdit.OrderItem.Quantity");
    input_Quantity.Width = "6 col";
    var builtinEvent19 = input_Quantity.OnChange;
    builtinEvent19.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent19.Destination = orderItem_OnChange;
    builtinEvent19.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent19.SetArgumentValue(selectedOptionList, null); // default value
    
    var container43 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container43.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container43.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container43.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container43.Animate = false; // default value
    container43.ColumnGap = null; // default value
    container43.ColumnSpan = null; // default value
    container43.CustomStyle = "margin-top: 20px;";
    container43.FlexBasis = null; // default value
    container43.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container43.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container43.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container43.Gap = null;
    container43.GridColumns = null; // default value
    container43.GridRows = null; // default value
    container43.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container43.RowGap = null; // default value
    container43.RowSpan = null; // default value
    container43.SetStyle(null); // default value
    container43.SetVisible("True"); // default value
    container43.VisibleWasExpression = false; // default value
    container43.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container43.Width = "(fill parent)"; // default value
    
    var textWidget36 = container43.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget36.CustomStyle = null; // default value
    textWidget36.SetStyleClasses(null); // default value
    textWidget36.Text = "Discount";
    
    var container44 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container44.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container44.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container44.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container44.Animate = false; // default value
    container44.ColumnGap = null; // default value
    container44.ColumnSpan = null; // default value
    container44.CustomStyle = "margin-bottom: var(--space-m); margin-top: 5px;";
    container44.FlexBasis = null; // default value
    container44.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container44.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container44.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container44.Gap = null;
    container44.GridColumns = null; // default value
    container44.GridRows = null; // default value
    container44.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container44.RowGap = null; // default value
    container44.RowSpan = null; // default value
    container44.SetStyle(null); // default value
    container44.SetVisible("True"); // default value
    container44.VisibleWasExpression = false; // default value
    container44.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container44.Width = "6 col";
    
    var inputWithIconInstance = container44.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IMobileBlockInstanceWidget>();
    inputWithIconInstance.CustomStyle = null; // default value
    inputWithIconInstance.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    var inputWithIcon = interaction.Nodes.OfType<OutSystems.Model.UI.Mobile.IMobileBlockSignature>().Named("InputWithIcon");
    inputWithIconInstance.SourceBlock = inputWithIcon;
    // delete default content in Icon placeholder
    var placeholderContentWidget22 = inputWithIconInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Icon");
    placeholderContentWidget22.Widgets.ToList().ForEach(w => w.Delete());
    // delete default content in Input placeholder
    var placeholderContentWidget23 = inputWithIconInstance.PlaceholdersContent.FirstOrDefault(p => p.Placeholder?.Name == "Input");
    placeholderContentWidget23.Widgets.ToList().ForEach(w => w.Delete());
    var alignIconRight = inputWithIcon.InputParameters.Named("AlignIconRight");
    inputWithIconInstance.SetArgumentValue(alignIconRight, null);
    var extendedClass8 = inputWithIcon.InputParameters.Named("ExtendedClass");
    inputWithIconInstance.SetArgumentValue(extendedClass8, null); // default value
    
    var icon10 = placeholderContentWidget22.CreateWidget<ServiceStudio.Plugin.NRWidgets.IIcon>();
    icon10.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    icon10.ColumnSpan = null; // default value
    icon10.CustomStyle = "margin-top: 25px;";
    icon10.FlexBasis = null; // default value
    icon10.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    icon10.Icon = "percent";
    icon10.IconSize = ServiceStudio.Plugin.NRWidgets.Enumerations.IconSize.FontSize;
    icon10.RowSpan = null; // default value
    icon10.SetStyle("\"icon\""); // default value
    icon10.SetVisible("True"); // default value
    icon10.Weight = null; // default value
    
    var input_Discount2 = placeholderContentWidget23.CreateWidget<ServiceStudio.Plugin.NRWidgets.IInput>("Input_Discount2");
    input_Discount2.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    input_Discount2.ColumnSpan = null; // default value
    input_Discount2.CustomStyle = "margin-bottom: 0px;";
    input_Discount2.SetEnabled("True");
    input_Discount2.FlexBasis = null; // default value
    input_Discount2.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    input_Discount2.InputType = ServiceStudio.Plugin.NRWidgets.Enumerations.InputType.Number;
    input_Discount2.SetMandatory("False");
    input_Discount2.MaxLength = 2;
    input_Discount2.SetPrompt(null); // default value
    input_Discount2.RowSpan = null; // default value
    input_Discount2.SetStyle("\"form-control\""); // default value
    input_Discount2.SetVariable("OrderDetailsEdit.OrderItem.Discount");
    input_Discount2.Width = "6 col";
    var builtinEvent20 = input_Discount2.OnChange;
    builtinEvent20.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent20.Destination = orderItem_OnChange;
    builtinEvent20.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent20.SetArgumentValue(selectedOptionList, null); // default value
    
    var container45 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container45.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container45.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container45.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container45.Animate = false; // default value
    container45.ColumnGap = null; // default value
    container45.ColumnSpan = null; // default value
    container45.CustomStyle = "margin-top: 20px; text-align: left;";
    container45.FlexBasis = null; // default value
    container45.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container45.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container45.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container45.Gap = null;
    container45.GridColumns = null; // default value
    container45.GridRows = null; // default value
    container45.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container45.RowGap = null; // default value
    container45.RowSpan = null; // default value
    container45.SetStyle(null); // default value
    container45.SetVisible("True"); // default value
    container45.VisibleWasExpression = false; // default value
    container45.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container45.Width = "(fill parent)"; // default value
    
    var textWidget37 = container45.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget37.CustomStyle = null; // default value
    textWidget37.SetStyleClasses("\"heading6\"");
    textWidget37.Text = "Total";
    
    var container46 = formProduct.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container46.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container46.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container46.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container46.Animate = false; // default value
    container46.ColumnGap = null; // default value
    container46.ColumnSpan = null; // default value
    container46.CustomStyle = "margin-top: 5px; text-align: left;";
    container46.FlexBasis = null; // default value
    container46.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container46.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container46.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container46.Gap = null;
    container46.GridColumns = null; // default value
    container46.GridRows = null; // default value
    container46.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container46.RowGap = null; // default value
    container46.RowSpan = null; // default value
    container46.SetStyle(null); // default value
    container46.SetVisible("True"); // default value
    container46.VisibleWasExpression = false; // default value
    container46.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container46.Width = "(fill parent)"; // default value
    
    var expression18 = container46.CreateWidget<ServiceStudio.Plugin.NRWidgets.IExpression>();
    expression18.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    expression18.ColumnSpan = null; // default value
    expression18.CustomStyle = null; // default value
    expression18.Example = "$151.2";
    expression18.FlexBasis = null; // default value
    expression18.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    expression18.RowSpan = null; // default value
    expression18.SetStyle("\"heading6\"");
    expression18.SetValue("FormatCurrency(OrderDetailsEdit.OrderItem.Amount, Client.Currency, 2, Client.DecimalSeparator, Client.GroupSeparator)");
    expression18.Width = ""; // default value
    
    var container47 = placeholderContentWidget21.CreateWidget<ServiceStudio.Plugin.NRWidgets.IContainer>();
    container47.Align = ServiceStudio.Plugin.NRWidgets.Enumerations.Align.Default; // default value
    container47.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    container47.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    container47.Animate = false; // default value
    container47.ColumnGap = null; // default value
    container47.ColumnSpan = null; // default value
    container47.CustomStyle = "margin-top: 40px; text-align: right;";
    container47.FlexBasis = null; // default value
    container47.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    container47.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    container47.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    container47.Gap = null;
    container47.GridColumns = null; // default value
    container47.GridRows = null; // default value
    container47.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    container47.RowGap = null; // default value
    container47.RowSpan = null; // default value
    container47.SetStyle(null); // default value
    container47.SetVisible("True"); // default value
    container47.VisibleWasExpression = false; // default value
    container47.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    container47.Width = "(fill parent)"; // default value
    
    var button5 = container47.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    /*** Deleting Widgets that are created by default when instantiating an IButton ***/
    button5.Widgets.ToList().ForEach(x => x.Delete());
    button5.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button5.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button5.ColumnGap = null; // default value
    button5.ColumnSpan = null; // default value
    button5.SetConfirmationMessage(null); // default value
    button5.CustomStyle = null; // default value
    button5.SetEnabled("True");
    button5.FlexBasis = null; // default value
    button5.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button5.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button5.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button5.Gap = null;
    button5.GridColumns = null; // default value
    button5.GridRows = null; // default value
    button5.IsDefault = false; // default value
    button5.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button5.RowGap = null; // default value
    button5.RowSpan = null; // default value
    button5.SetStyle("\"btn background-primary\"");
    button5.SetVisible("True"); // default value
    button5.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button5.Width = ""; // default value
    
    var newProduct2 = button5.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.IIfWidget>("NewProduct2");
    newProduct2.Animate = false; // default value
    newProduct2.SetCondition("OrderDetailsEdit.OrderItem.Id = NullIdentifier()");
    newProduct2.CustomStyle = null; // default value
    newProduct2.DesignMode = OutSystems.Model.Enumerations.DesignMode.ShowAll; // default value
    
    var textWidget38 = newProduct2.TrueBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget38.CustomStyle = null; // default value
    textWidget38.SetStyleClasses(null); // default value
    textWidget38.Text = "Add";
    
    var textWidget39 = newProduct2.FalseBranch.CreateWidget<OutSystems.Model.UI.Mobile.Widgets.ITextWidget>();
    textWidget39.CustomStyle = null; // default value
    textWidget39.SetStyleClasses(null); // default value
    textWidget39.Text = "Save";
    var builtinEvent21 = button5.OnClick;
    builtinEvent21.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent21.Destination = product_Save;
    builtinEvent21.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    
    var button6 = container47.CreateWidget<ServiceStudio.Plugin.NRWidgets.IButton>();
    button6.AlignItems = OutSystems.Model.Enumerations.AlignItems.Stretch; // default value
    button6.AlignSelf = OutSystems.Model.Enumerations.AlignSelf.Auto; // default value
    button6.ColumnGap = null; // default value
    button6.ColumnSpan = null; // default value
    button6.SetConfirmationMessage(null); // default value
    button6.CustomStyle = null; // default value
    button6.SetEnabled("True");
    button6.FlexBasis = null; // default value
    button6.FlexDirection = OutSystems.Model.Enumerations.FlexDirection.Row; // default value
    button6.Flexible = OutSystems.Model.Enumerations.Flexible.Shrink; // default value
    button6.FlexWrap = OutSystems.Model.Enumerations.FlexWrap.NoWrap; // default value
    button6.Gap = null;
    button6.GridColumns = null; // default value
    button6.GridRows = null; // default value
    button6.IsDefault = false; // default value
    button6.JustifyContent = OutSystems.Model.Enumerations.JustifyContent.Start; // default value
    button6.RowGap = null; // default value
    button6.RowSpan = null; // default value
    button6.SetStyle("\"btn\""); // default value
    button6.SetVisible("True"); // default value
    button6.WidgetGridType = OutSystems.Model.Enumerations.WidgetGridType.None; // default value
    button6.Width = ""; // default value
    
    var textWidget40 = (OutSystems.Model.UI.Mobile.Widgets.ITextWidget)button6.Widgets.First();
    textWidget40.CustomStyle = null; // default value
    textWidget40.Name = null; // default value
    textWidget40.SetStyleClasses(null); // default value
    textWidget40.Text = "Cancel";
    var builtinEvent22 = button6.OnClick;
    builtinEvent22.BuiltInValidations = OutSystems.Model.Enumerations.ValidationBehavior.None; // default value
    builtinEvent22.Destination = product_ToggleEdit2;
    builtinEvent22.Transition = OutSystems.Model.Enumerations.TransitionTypeWithInheritance.Inherited; // default value
    builtinEvent22.SetArgumentValue(toggleOnly, "True");
}

```