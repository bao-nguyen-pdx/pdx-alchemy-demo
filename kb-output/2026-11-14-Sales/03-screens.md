# 03 — Screens

Source: `getSerializedObject › UIFlows`. Roles = who may open the screen.

## MainFlow › AccountManager_SetupQuotas  (`Sales`)
Title: Sales - Quarter Quotas
- **Roles:** SalesManager
- **Screen actions:** SaveQuota
- **Server calls:** CreateOrUpdateQuarterQuota, Feedback_Message
- **Navigates to:** Dashboard

## MainFlow › Dashboard  (`Sales`)
Title: Sales - Dashboard
- **Roles:** AccountManager, SalesManager
- **Server calls:** CheckSalesManagerRole, DashboardForecast, DashboardQuarterStatus, GetCurrentAccountManager
- **Navigates to:** Dashboard_SalesManager
- **UI conditions (2):** `visible: OpportunityTable.List.Current.Opportunity.Id <> NullIdentifier()` · `condition: GetOpportunities.Count > OpportunityTable.LineCount`

## MainFlow › Dashboard_SalesManager  (`Sales`)
Title: Sales - Dashboard
- **Roles:** SalesManager
- **Server calls:** DashboardByAccountManager, DashboardForecast, DashboardQuarterStatus

## MainFlow › Opportunities_ListClosed  (`Sales`)
Title: Sales - Closed Opportunities
- **Roles:** AccountManager, SalesManager
- **Screen actions:** ExportToExcel, RefreshOpportunityTable
- **Server calls:** Feedback_Message, GetCurrentAccountManager, List_Navigation_ResetStartIndex
- **UI conditions (3):** `visible: OpportunityTable.List.Current.Opportunity.Id <> NullIdentifier()` · `visible: OpportunityTable.List.Current.Company.Id <> NullIdentifier()` · `condition: OpportunityTable.List.Current.AccountManager.UserId <>NullIdentifier() and GetCurrentAccountManager.AccountManager = NullIdentif…`

## MainFlow › Opportunity_Edit  (`Sales`)
Title: "Sales - " + If(GetOpportunityById.List.Current.Opportunity.Id = NullIdentifier(), 
   "New Opportunity",
   "Edit '" + GetOpportunityById.List.Current.Opportunity.Name + "'")
- **Roles:** AccountManager, SalesManager
- **Inputs:** OpportunityId: Opportunity Identifier, CompanyId: Company Identifier, CancelURL: Text
- **Screen actions:** AutoCompleteCompany, DeleteOpportunity, Save
- **Server calls:** Company_CreateOrUpdate, CreateAccountManager, CreateOrUpdateOpportunity, DeleteOpportunity, Feedback_Message, Input_AutoComplete_GetIdentifier, Input_AutoComplete_ShowList, Input_FocusFirstInvalid, ListAppend, OpportunityHistoryLog
- **Navigates to:** ExternalURL, Opportunity_Show
- **UI conditions (2):** `condition: GetOpportunityById.List.Current.Opportunity.Id = NullIdentifier()` · `condition: OpportunityId <> NullIdentifier()`

## MainFlow › Opportunity_List  (`Sales`)
Title: Sales - Opportunities
- **Roles:** AccountManager, SalesManager
- **Inputs:** AccountManager: User Identifier
- **Screen actions:** ExportToExcel, RefreshOpportunityTable
- **Server calls:** CheckSalesManagerRole, Feedback_Message, List_Navigation_ResetStartIndex
- **UI conditions (5):** `visible: CheckSalesManagerRole.HasRole` · `visible: OpportunityTable.List.Current.Opportunity.Id <> NullIdentifier()` · `visible: OpportunityTable.List.Current.Company.Id <> NullIdentifier()` · `condition: OpportunityTable.List.Current.AccountManager.UserId <>NullIdentifier() and Session.Opportunity_List_AccountManager = NullIdentif…` · `condition: OpportunityTable.List.Current.Opportunity.CloseDate<= CurrDate()`

## MainFlow › Opportunity_Show  (`Sales`)
Title: "Sales - " + GetOpportunityById.List.Current.Opportunity.Name
- **Roles:** AccountManager, SalesManager
- **Inputs:** OpportunityId: Opportunity Identifier
- **Screen actions:** OnChangeState, SwitchChangeStage
- **Server calls:** CreateOrUpdateOpportunity, Feedback_Message, GetOpportunity, OpportunityHistoryLog, RecentItem_Add, RecentItem_Remove
- **Navigates to:** Opportunity_List
- **UI conditions (4):** `visible: OpportunityShow.Record.Company.Id <> NullIdentifier()` · `condition: OpportunityShow.Record.Opportunity.CloseDate<=CurrDate()` · `condition: not ChangeStage` · `condition: OpportunityShow.Record.AccountManager.UserId <> NullIdentifier()`

## ContactsAndCompanies › Company_Edit  (`Sales`)
Title: "Sales - " + If(GetCompanyById.List.Current.Company.Id = NullIdentifier(), 
   "New Company",
   "Edit '" + GetCompanyById.List.Current.Company.CompanyName + "'")
- **Roles:** AccountManager, SalesManager
- **Inputs:** CompanyId: Company Identifier, CancelURL: Text
- **Screen actions:** AutoCompleteCountry, DeleteCompany, Save
- **Server calls:** CheckPermissionsForCompany, Company_CreateHistory, Company_CreateOrUpdate, Company_Delete, CreateOrUpdateAccountManager, CreateTask, DeleteAccountManager, Feedback_Message, Input_AutoComplete_GetIdentifier, Input_AutoComplete_ShowList, Input_FocusFirstInvalid, ListAppend
- **Navigates to:** Company_List, Company_Show, ExternalURL
- **UI conditions (2):** `condition: GetCompanyById.List.Current.Company.Id = NullIdentifier()` · `condition: CompanyId <> NullIdentifier()`

## ContactsAndCompanies › Company_FullHistory  (`Sales`)
Title: Sales - " + GetCompanyById.List.Current.Company.CompanyName + "History
- **Roles:** AccountManager, SalesManager
- **Inputs:** CompanyId: Company Identifier, OpportunityStageChanges: Boolean
- **Screen actions:** RefreshList
- **Server calls:** Feedback_Message, RecentItem_Add
- **UI conditions (6):** `condition: CompanyId <>NullIdentifier()` · `visible: GetCompanyById.List.Current.Company.Id <> NullIdentifier()` · `visible: CompanyId = NullIdentifier()` · `enabled: not OpportunityStageChanges` · `visible: OpportunityStageChanges` · `visible: HistoryTable.List.Current.MergedCompanyHistory.OpportunityId= NullIdentifier() and HistoryTable.List.Current.MergedCompanyHistory.…`

## ContactsAndCompanies › Company_List  (`Sales`)
Title: Sales - Companies
- **Roles:** AccountManager, SalesManager
- **Screen actions:** ExportToExcel, RefreshCompanyTable
- **Server calls:** Feedback_Message, GetCurrentAccountManager, List_Navigation_ResetStartIndex
- **UI conditions (5):** `visible: GetCurrentAccountManager.AccountManager = NullIdentifier()` · `visible: CompanyTable.List.Current.Company.Id <> NullIdentifier()` · `condition: CompanyTable.List.Current.AccountManager.UserId <> NullIdentifier() and GetCurrentAccountManager.AccountManager = NullIdentifier…` · `condition: CompanyTable.List.Current.Company.Address<>""` · `condition: CompanyTable.List.Current.Company.City<>""`

## ContactsAndCompanies › Company_Show  (`Sales`)
Title: "Sales - " + GetCompanyById.List.Current.Company.CompanyName
- **Roles:** AccountManager, SalesManager
- **Inputs:** CompanyId: Company Identifier
- **Server calls:** Feedback_Message, RecentItem_Add, RecentItem_Remove
- **Navigates to:** Company_List
- **UI conditions (9):** `condition: CompanyShow.Record.AccountManager.UserId <> NullIdentifier()` · `condition: CompanyShow.Record.Company.Address<>""` · `condition: CompanyShow.Record.Company.City<>""` · `visible: CompanyShow.Record.Company.Address<>""` · `visible: OpportunityTable.List.Current.Opportunity.Id <> NullIdentifier()` · `visible: ContactTable.List.Current.Contact.Id <> NullIdentifier()` · `condition: not GetClosedOpportunities.List.Empty` · `visible: ClosedOpportunityTable.List.Current.Opportunity.Id <> NullIdentifier()` · `condition: ClosedOpportunityTable.List.Current.Opportunity.Id <> NullIdentifier()`

## ContactsAndCompanies › Contact_Edit  (`Sales`)
Title: "Sales - " + If(GetContactById.List.Current.Contact.Id = NullIdentifier(), 
   "New Contact",
   "Edit '" + GetContactById.List.Current.Contact.Name + "'")
- **Roles:** AccountManager, SalesManager
- **Inputs:** ContactId: Contact Identifier, CompanyId: Company Identifier, CancelURL: Text
- **Screen actions:** AutoCompleteCompany, ChangePicture, DeleteContact, Save
- **Server calls:** Company_CreateOrUpdate, Contact_CreateOrUpdate, Contact_CreateOrUpdatePicture, Contact_Delete, CreateAccountManager, Feedback_Message, Input_AutoComplete_GetIdentifier, Input_AutoComplete_ShowList, Input_FocusFirstInvalid, ListAppend, Popup_Upload_GetFile
- **Navigates to:** Contact_Edit, Contact_List, Contact_Show
- **UI conditions (4):** `condition: GetContactById.List.Current.Contact.Id = NullIdentifier()` · `condition: ContactId <> NullIdentifier()` · `condition: ContactEdit.Record.Contact.Id <> NullIdentifier()` · `visible: ContactId = NullIdentifier()`

## ContactsAndCompanies › Contact_Import  (`Sales`)
Title: Sales - Import Contacts
- **Roles:** AccountManager, Anonymous, Registered, SalesManager, UserManager
- **Inputs:** ImportErrors: Text
- **Screen actions:** DownloadSampleFile, ImportContacts
- **Server calls:** Contact_CreateOrUpdate, Feedback_Message
- **Navigates to:** Contact_Import, Contact_List
- **UI conditions (1):** `condition: ImportErrors=""`

## ContactsAndCompanies › Contact_List  (`Sales`)
Title: Sales - Contacts
- **Roles:** AccountManager, SalesManager
- **Screen actions:** ExportToExcel, OnNotifyRefreshContactTable
- **Server calls:** Feedback_Message, List_Navigation_ResetStartIndex
- **UI conditions (3):** `visible: ContactTable.List.Current.Contact.Id <> NullIdentifier()` · `condition: ContactTable.List.Current.Contact.CompanyId <> NullIdentifier()` · `visible: ContactTable.List.Current.Company.Id <> NullIdentifier()`

## ContactsAndCompanies › Contact_Show  (`Sales`)
Title: "Sales - " + GetContactById.List.Current.Contact.Name
- **Roles:** AccountManager, SalesManager
- **Inputs:** ContactId: Contact Identifier
- **Server calls:** Feedback_Message, RecentItem_Add, RecentItem_Remove
- **Navigates to:** Contact_List
- **UI conditions (2):** `condition: not GetOpportunitiesByCompanyId.List.Empty` · `visible: OpportunityTable.List.Current.Opportunity.Id <> NullIdentifier()`

## Common › InternalError  (`Sales`)
Title: Internal Error
- **Roles:** AccountManager, Anonymous, Registered, SalesManager, UserManager
- **Inputs:** ExceptionMessage: Text
- **UI conditions (1):** `condition: GetPersonalAreaName() = ""`

## Common › InvalidPermissions  (`Sales`)
Title: Invalid Permissions
- **Roles:** AccountManager, Anonymous, Registered, SalesManager, UserManager

## Common › Login  (`Sales`)
Title: Login
- **Roles:** AccountManager, Anonymous, Registered, SalesManager, UserManager
- **Inputs:** OriginalURL: Text
- **Screen actions:** ForgotPassword, Login
- **Server calls:** AddFaviconTag, Feedback_Message, User_Login
- **Navigates to:** ExternalURL

## Common › MyInfo  (`Sales`)
Title: My Info
- **Roles:** AccountManager, Registered, SalesManager, UserManager
- **Screen actions:** ToggleChangePassword
- **Server calls:** Feedback_Message
- **UI conditions (1):** `condition: not ShowChangePassword`

## Common › NoPermission  (`Sales`)
- **Roles:** AccountManager, Anonymous, Registered, SalesManager, UserManager
- **Server calls:** User_GetUnifiedLoginUrl
- **Navigates to:** ExternalURL, InvalidPermissions, Login

## Common › SearchResults  (`Sales`)
Title: "Sales - Search for " + Keyword
- **Roles:** AccountManager, Registered, SalesManager, UserManager
- **Inputs:** Keyword: Text
- **UI conditions (17):** `condition: not GetPendingOpportunities.List.Empty` · `visible: OpportunityTable.List.Current.Opportunity.Id <> NullIdentifier()` · `visible: OpportunityTable.List.Current.Company.Id <> NullIdentifier()` · `condition: OpportunityTable.List.Current.AccountManager.UserId <>NullIdentifier() and Session.Opportunity_List_AccountManager = NullIdentif…` · `condition: OpportunityTable.List.Current.Opportunity.CloseDate<= CurrDate()` · `condition: not GetContacts.List.Empty` · `visible: ContactTable.List.Current.Contact.Id <> NullIdentifier()` · `condition: ContactTable.List.Current.Contact.CompanyId <> NullIdentifier()` · `visible: ContactTable.List.Current.Company.Id <> NullIdentifier()` · `condition: not GetCompaniesByAccountManager.List.Empty` · `visible: CompanyTable.List.Current.Company.Id <> NullIdentifier()` · `condition: CompanyTable.List.Current.Company.Address<>""` · `condition: CompanyTable.List.Current.Company.City<>""` · `visible: OpportunityTable2.List.Current.Opportunity.Id <> NullIdentifier()` · `visible: OpportunityTable2.List.Current.Company.Id <> NullIdentifier()` · `condition: OpportunityTable2.List.Current.AccountManager.UserId <>NullIdentifier() and Session.Opportunity_List_AccountManager = NullIdenti…` · `condition: OpportunityTable2.List.Current.Opportunity.CloseDate<= CurrDate()`

## MainFlow › Configuration  (`SalesSampleData`)
Title: Sales - Configuration
- **Roles:** UserManager
- **Screen actions:** DeleteAllData, DownloadTemplateCompanies, DownloadTemplateContacts, DownloadTemplateOpportunities, ReloadSampleData, UploadCompanies, UploadContacts, UploadOpportunities
- **Server calls:** Company_ImportFromExcel, Contact_ImportFromExcel, Feedback_Message, Logout, Opportunity_ImportFromExcel, SampleData_Bootstrap, SampleData_Delete, User_Login
- **Navigates to:** Login
- **UI conditions (2):** `enabled: GetOpportunities.List.Empty` · `condition: IsSampleUser`

