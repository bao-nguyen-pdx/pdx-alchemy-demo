# 04 — Logic

Every action rendered as pseudo-code from its flow graph (`getSerializedObject`).
Validation-rule candidates (IF → RAISE / error message) are listed first.

## Validation-rule candidates

| Where | Condition | Message | Raises |
|---|---|---|---|
| Sales › Opportunity_Edit › Save | `OpportunityEdit.Record.Opportunity.CloseDate<CurrDate() and OpportunityEdit.Record.Opportunity.Stage<> Entities.OpportunityStage.Won and OpportunityEdit.Record…` | You can not set dates in the past. | input validation (OPPORTUNITY_CloseDate) |
| Sales › Opportunity_Edit › Preparation | `NOT (CheckPermissionsForCompany(GetOpportunityById.List.Current.Opportunity.CompanyId))` | You do not have permissions for the selected company. | NoPermissionForSelectedCompany |
| Sales › Opportunity_Show › Preparation | `NOT (CheckPermissionsForCompany(GetOpportunityById.List.Current.Opportunity.CompanyId))` | You do not have permissions for the selected company. | NoPermissionForSelectedCompany |
| Sales › Company_Edit › Save | `Country_Name_GetIdentifier.Identifier = -1` | Please select a valid Country | input validation (Country_Name) |
| Sales › Company_Edit › Save | `NOT (CompanyEdit.Record.Company.Website = "" or Index(ToLower(CompanyEdit.Record.Company.Website),"http://")=0)` | Website need to start with http:// | input validation (COMPANY_Website) |
| Sales › Company_Edit › Save | `NOT (CompanyEdit.Record.Company.Email = "" or EmailAddressValidate(CompanyEdit.Record.Company.Email))` | Invalid Email Address | input validation (COMPANY_Email) |
| Sales › Company_Edit › Save | `NOT (GetRepeatedCompanyNames.List.Empty)` | There is already one Company with the same Company Name | input validation (COMPANY_CompanyName) |
| Sales › Company_Edit › Preparation | `NOT (CheckPermissionsForCompany(CompanyId))` | You do not have permissions for the selected company. | NoPermissionForSelectedCompany |
| Sales › Company_FullHistory › Preparation | `NOT (CheckPermissionsForCompany(CompanyId))` | You do not have permissions for the selected company. | NoPermissionForSelectedCompany |
| Sales › Company_Show › Preparation | `NOT (CheckPermissionsForCompany(CompanyId))` | You do not have permissions for the selected company. | NoPermissionForSelectedCompany |
| Sales › Contact_Edit › Save | `NOT (ContactEdit.Record.Contact.Email = "" or EmailAddressValidate(ContactEdit.Record.Contact.Email))` | Invalid Email Address | input validation (CONTACT_Email) |
| Sales › Contact_Edit › Preparation | `NOT (CheckPermissionsForCompany(GetContactById.List.Current.Contact.CompanyId))` | You do not have permissions for the selected company. | NoPermissionForSelectedCompany |
| Sales › Contact_Show › Preparation | `NOT (CheckPermissionsForCompany(GetContactById.List.Current.Contact.CompanyId))` | You do not have permissions for the selected company. | NoPermissionForSelectedCompany |
| Sales › NoteTaskHistory › AddTask | `TaskEdit.Record.Task.DueDate<CurrDate()` | You can't add tasks in the past. | input validation (Task_DueDate) |

## Actions

### CustomersSampleData › Company_GetIdFromName  _(server)_
In: CompanyName: Text · Out: CompanyId: Company Identifier
```text
QUERY GetCompanyFromName from Company; where Company.CompanyName = CompanyName
CompanyId = GetCompanyFromName.List.Current.Company.Id
END
```

### CustomersSampleData › Company_ImportFromExcel  _(server)_
In: Excel: Binary Data · Out: —
```text
EXCEL→LIST CompaniesExcel from Excel
FOR EACH CompaniesExcel:
  QUERY GetCountryByName from Country; where Country.Name like CompaniesExcel.Current.Excel_Company.Country; max 1
  CompanyRecord.Company.CompanyName = CompaniesExcel.Current.Excel_Company.CompanyName; CompanyRecord.Company.CompanyType = CompanyType_GetIdFromLabel(CompaniesExcel.Current.Excel_Company.CompanyType); CompanyRecord.Company.Phone = CompaniesExcel.Current.Excel_Company.Phone; CompanyRecord.Company.Email = CompaniesExcel.Current.Excel_Company.Email; CompanyRecord.Company.Fax = CompaniesExcel.Current.Excel_Company.Fax; CompanyRecord.Company.Website = CompaniesExcel.Current.Excel_Company.Website; CompanyRecord.Company.Address = CompaniesExcel.Current.Excel_Company.Address; CompanyRecord.Company.PostalCode = CompaniesExcel.Current.Excel_Company.PostalCode; CompanyRecord.Company.City = CompaniesExcel.Current.Excel_Company.City; CompanyRecord.Company.CountryId = GetCountryByName.List.Current.Country.Id
  CALL server Company_Create(CompanyRecord)
  ↪ continue at «ForEach»
END
```

### CustomersSampleData › CompanyType_GetIdFromLabel  _(server)_
In: CompanyTypeLabel: Text · Out: CompanyTypeId: CompanyType Identifier
```text
QUERY GetCompanyTypeFomLabel from CompanyType; where CompanyType.Label = CompanyTypeLabel
CompanyTypeId = GetCompanyTypeFomLabel.List.Current.CompanyType.Id
END
```

### CustomersSampleData › Contact_GetIdFromName  _(server)_
In: ContactName: Text · Out: ContactId: Contact Identifier
```text
QUERY GetContactsByName from Contact; where Contact.Name = ContactName
ContactId = GetContactsByName.List.Current.Contact.Id
END
```

### CustomersSampleData › Contact_ImportFromExcel  _(server)_
In: Excel: Binary Data · Out: —
```text
EXCEL→LIST ContactsExcel from Excel
FOR EACH ContactsExcel:
  ContactRecord.Contact.Name = ContactsExcel.Current.Excel_Contacts.Name; ContactRecord.Contact.JobTitle = ContactsExcel.Current.Excel_Contacts.JobTitle; ContactRecord.Contact.Phone = ContactsExcel.Current.Excel_Contacts.Phone; ContactRecord.Contact.Mobile = ContactsExcel.Current.Excel_Contacts.Mobile; ContactRecord.Contact.Email = ToLower(ContactsExcel.Current.Excel_Contacts.Email); ContactRecord.Contact.CompanyId = Company_GetIdFromName(ContactsExcel.Current.Excel_Contacts.Company); ContactRecord.Contact.SecondaryEmail = ContactsExcel.Current.Excel_Contacts.SecondaryEmail
  CALL server Contact_Create(ContactRecord)
  ↪ continue at «ForEach»
END
```

### CustomersSampleData › DownloadSampleCompanies  _(server)_
In: — · Out: BinaryData: Binary Data
```text
BinaryData = Resources.SampleCompanies_xlsx.Content
END
```

### CustomersSampleData › DownloadSampleContacts  _(server)_
In: — · Out: BinaryData: Binary Data
```text
BinaryData = Resources.SampleContacts_xlsx.Content
END
```

### CustomersSampleData › SampleData_Bootstrap  _(server)_
```text
IF SampleData_BootstrapDone() = True   «Bootstrap Done?»
  THEN:
    END
  ELSE:
    CALL server GetRoleForUpdate(Roles.UserManager)
    CALL server Company_ImportFromExcel(Resources.SampleCompanies_xlsx.Content)
    CALL server Contact_ImportFromExcel(Resources.SampleContacts_xlsx.Content)
    END
```

### CustomersSampleData › SampleData_BootstrapDone  _(server)_
In: — · Out: BootstrapDone: Boolean
```text
QUERY GetCompanies 
IF not GetCompanies.List.Empty   «Has Companies?»
  THEN:
    END
  ELSE:
    BootstrapDone = False
    END
```

### CustomersSampleData › SampleData_Delete  _(server)_
```text
CALL server Contact_DeleteAllContacts()
CALL server Company_DeleteAllCompanies()
END
```

### Sales › CheckPermissionsForCompany  _(server)_
In: CompanyId: Company Identifier · Out: HasPermissions: Boolean
```text
CALL server CheckSalesManagerRole()
IF CheckSalesManagerRole.HasRole   «SalesManager or Admin»
  THEN:
    HasPermissions = True
    END
  ELSE:
    QUERY GetCompanyById from AccountManager, Company; where Company.Id = CompanyId
    HasPermissions = GetCompanyById.List.Current.AccountManager.UserId = GetUserId()
    END
```

### Sales › CheckSuperUserRole  _(server)_
In: UserId: User Identifier · Out: —
```text
IF UserId = NullIdentifier()
  THEN:
    UserId = GetUserId()
    QUERY GetSuperUserRole from Role; where Role.Name = "SuperUser"
    Session.SuperUserRole = CheckRole(GetSuperUserRole.List.Current.Role.Id,UserId)
    END
  ELSE:
    ↪ continue at «GetSuperUserRole»
```

### Sales › DashboardByAccountManager  _(server)_
In: — · Out: DataPoint: DataPoint Record, DataPointList: DataPoint Record List
```text
QUERY GetAccountManagers from User; where CheckRole(Roles.AccountManager, User.Id)=True AND User.Username <> "admin"; max 25
CALL server GetOpportunityStage(Entities.OpportunityStage.Won)
FOR EACH GetAccountManagers.List:
  QUERY GetQuarterQuotasByUserId from QuarterQuota; where QuarterQuota.UserId = GetAccountManagers.List.Current.User.Id
  DataPoint.DataPoint.Label = GetAccountManagers.List.Current.User.Name; DataPoint.DataPoint.Value = GetQuarterQuotasByUserId.List.Current.QuarterQuota.QuarterQuota; DataPoint.DataPoint.DataSeriesName = "Quota"
  CALL server ListAppend(DataPoint, DataPointList)
  SQL GetSumForCurrentQuarter (see SQL below)
  DataPoint.DataPoint.Value = GetSumForCurrentQuarter.List.Current.Decimal.Value; DataPoint.DataPoint.DataSeriesName = "Won"
  CALL server ListAppend(DataPoint, DataPointList)
  ↪ continue at «ForEach»
END
```
SQL `GetSumForCurrentQuarter`:
```sql
SELECT Sum({Opportunity}.[Amount])
FROM {Opportunity} 
    inner join {Company} on {Opportunity}.[CompanyId] = {Company}.[Id]
    left join {AccountManager} on {AccountManager}.[CompanyId] = {Company}.[Id]
WHERE {Opportunity}.[Stage] = @OpportunityStageId 
  and {AccountManager}.[UserId] = @UserId 
  and {Opportunity}.[CloseDate] >=@StartQuarter and {Opportunity}.[CloseDate]<=@EndQuarter
```

### Sales › DashboardForecast  _(server)_
In: — · Out: DataPoint: DataPoint Record, DataPointList: DataPoint Record List
```text
StartDate = NewDate(Year(CurrDate()),Month(CurrDate()),1)
IF DateToDateTime(StartDate) > AddMonths(CurrDate(),6)   «>6 months»
  THEN:
    END
  ELSE:
    SQL GetForecast (see SQL below)
    DataPoint.DataPoint.Label = Substr(Date_MonthName(Date:StartDate),0,3); DataPoint.DataPoint.Value = GetForecast.List.Current.Decimal.Value
    CALL server ListAppend(DataPoint, DataPointList)
    StartDate = AddMonths(StartDate,1)
    ↪ continue at «>6 months»
```
SQL `GetForecast`:
```sql
SELECT Sum({Opportunity}.[Amount] * {OpportunityStage}.[Percentage] ) 
FROM {Opportunity} inner join {OpportunityStage} 
    on {Opportunity}.[Stage] = {OpportunityStage}.[Id]
    inner join {Company} 
        on {Opportunity}.[CompanyId] = {Company}.[Id]
    left join {AccountManager} on {AccountManager}.[CompanyId] = {Company}.[Id] 
WHERE ({AccountManager}.[UserId] = @AccountManager or @AccountManager = 0)
and ({Opportunity}.[CloseDate]) > @StartDate and  ({Opportunity}.[CloseDate]) < @EndDate
```

### Sales › DashboardQuarterStatus  _(server)_
In: — · Out: QuotaStatus: Text, DataPoint: DataPoint Record, DataPointList: DataPoint Record List
```text
SQL GetSumOfQuotas (see SQL below)
DataPoint.DataPoint.Value = GetSumOfQuotas.List.Current.Decimal.Value; DataPoint.DataPoint.Label = "Quota"
CALL server ListAppend(DataPoint, DataPointList)
QUERY GetStagesGE70 from OpportunityStage; where OpportunityStage.Percentage>=0.7
FOR EACH GetStagesGE70.List:
  SQL GetSumByStageForCurrentQuarter (see SQL below)
  DataPoint.DataPoint.Value = GetSumByStageForCurrentQuarter.List.Current.Decimal.Value; DataPoint.DataPoint.Label = FormatPercent(GetStagesGE70.List.Current.OpportunityStage.Percentage,0,",")
  CALL server ListAppend(DataPoint, DataPointList)
  ↪ continue at «ForEach»
SQL GetSumWon (see SQL below)
QuotaStatus = If(GetSumOfQuotas.List.Current.Decimal.Value>0, Round(GetSumWon.List.Current.Decimal.Value/GetSumOfQuotas.List.Current.…
END
```
SQL `GetSumOfQuotas`:
```sql
SELECT sum({QuarterQuota}.[QuarterQuota])
FROM {QuarterQuota} 
WHERE ({QuarterQuota}.[UserId] = @AccountManager or @AccountManager = 0)
```
SQL `GetSumByStageForCurrentQuarter`:
```sql
SELECT Sum({Opportunity}.[Amount])
FROM {Opportunity}
    inner join {Company} 
        on {Opportunity}.[CompanyId] = {Company}.[Id]
    left join {AccountManager} on {AccountManager}.[CompanyId] = {Company}.[Id] 
WHERE {Opportunity}.[Stage] = @OpportunityStageId 
  and ({AccountManager}.[UserId] = @AccountManager or @AccountManager = 0)
  and {Opportunity}.[CloseDate] >=@StartQuarter and {Opportunity}.[CloseDate]<=@EndQuarter
```
SQL `GetSumWon`:
```sql
SELECT Sum({Opportunity}.[Amount])
FROM {Opportunity} inner join {Company} on {Opportunity}.[CompanyId] = {Company}.[Id]
left join {AccountManager} on {AccountManager}.[CompanyId] = {Company}.[Id]
WHERE {Opportunity}.[Stage] = @OpportunityStageId 
and ({AccountManager}.[UserId] = @AccountManager or @AccountManager = 0)
and {Opportunity}.[CloseDate] >=@StartQuarter and {Opportunity}.[CloseDate]<=@EndQuarter
```

### Sales › FormatAddress  _(server)_
In: Address: Text, PostalCode: Text, City: Text, Country: Text · Out: CompleteAddress: Text
```text
IF Address <> ""
  THEN:
    CompleteAddress = CompleteAddress + Address
    IF PostalCode <> ""
      THEN:
        CompleteAddress = If(CompleteAddress<>"", CompleteAddress + ", ", "") + PostalCode
        IF City <> ""
          THEN:
            CompleteAddress = If(CompleteAddress<>"", CompleteAddress + ", ", "") + City
            IF Country <> ""
              THEN:
                CompleteAddress = If(CompleteAddress<>"", CompleteAddress + ", ", "") + Country
                END
              ELSE:
                ↪ continue at «End»
          ELSE:
            ↪ continue at «If»
      ELSE:
        ↪ continue at «If»
  ELSE:
    ↪ continue at «If»
```

### Sales › FormatDate  _(server)_
In: Date: Date · Out: Formatted: Text
```text
IF CurrDate() = Date
  THEN:
    Formatted = "today"
    END
  ELSE:
    IF AddDays(CurrDate(),1) = Date
      THEN:
        Formatted = "tomorrow"
        END
      ELSE:
        IF Date=NullDate()
          THEN:
            END
          ELSE:
            IF Date < CurrDate()
              THEN:
                Formatted = Date_PrettyFormat(Date)
                END
              ELSE:
                Formatted = Day(Date) + " " + Substr(FormatDateTime(Date, "dd-MMMM"),Index(FormatDateTime(Date, "dd-MMMM"),"-")+1,3)
                IF Year(CurrDate()) <> Year(Date)
                  THEN:
                    Formatted = Formatted + " " + Year(Date)
                    END
                  ELSE:
                    ↪ continue at «End»
```

### Sales › GetCurrentAccountManager  _(server)_
In: — · Out: AccountManager: User Identifier
```text
CALL server CheckSalesManagerRole()
IF CheckSalesManagerRole.HasRole   «Has Permissions»
  THEN:
    AccountManager = NullIdentifier()
    END
  ELSE:
    AccountManager = GetUserId()
    END
```

### Sales › GetQuarterEnd  _(server)_
In: Date: Date · Out: QuarterEnd: Date
```text
QuarterEnd = AddDays( AddMonths(Date, 3-(Mod(Month(Date)-1,3))), -Day(AddMonths(Date, 3-(Mod(Month(Date)-1,3)))) )
END
```

### Sales › NoteOrTaskHistory  _(server)_
In: CompanyId: Company Identifier, ContactId: Contact Identifier, OpportunityId: Opportunity Identifier, Description: Text · Out: —
```text
SWITCH
  CASE ContactId<>NullIdentifier():
    CALL server Contact_CreateHistory(ContactId, Description)
    END
  CASE OpportunityId<>NullIdentifier():
    OpportunityHistory.OpportunityHistory.OpportunityId = OpportunityId; OpportunityHistory.OpportunityHistory.Description = Description; OpportunityHistory.OpportunityHistory.UserId = GetUserId(); OpportunityHistory.OpportunityHistory.Instant = CurrDateTime()
    CALL server CreateOpportunityHistory(OpportunityHistory)
    ↪ continue at «End»
  OTHERWISE:
    CALL server Company_CreateHistory(CompanyId, Description)
    ↪ continue at «End»
```

### Sales › Opportunity_Create  _(server)_
In: OpportunityRecord: Opportunity Record · Out: OpportunityId: Opportunity Identifier
```text
CALL server CreateOpportunity(OpportunityRecord)
CALL server OpportunityHistoryLog(OpportunityRecord, EmptyOpportunityRecord)
OpportunityId = CreateOpportunity.Id
END
```

### Sales › OpportunityHistoryLog  _(server)_
In: PreviousRecord: Opportunity Record, NewRecord: Opportunity Record · Out: —
```text
IF PreviousRecord.Opportunity.Id = NullIdentifier()   «new»
  THEN:
    Description = "created opportunity"
    History.OpportunityHistory.Instant = CurrDateTime(); History.OpportunityHistory.Description = Description; History.OpportunityHistory.UserId = GetUserId(); History.OpportunityHistory.OpportunityId = NewRecord.Opportunity.Id
    CALL server CreateOpportunityHistory(History)
    END
  ELSE:
    Description = "changed opportunity"
    IF PreviousRecord.Opportunity.Name <>NewRecord.Opportunity.Name   «<> Name»
      THEN:
        Description = Description + " Name: '" + PreviousRecord.Opportunity.Name + "' to '" + NewRecord.Opportunity.Name + "'"
        IF PreviousRecord.Opportunity.Description <>NewRecord.Opportunity.Description   «<> Description»
          THEN:
            Description = Description + " Description: '" + PreviousRecord.Opportunity.Description + "' to '" + NewRecord.Opportunity.Description…
            IF PreviousRecord.Opportunity.CloseDate <>NewRecord.Opportunity.CloseDate   «<> CloseDate»
              THEN:
                Description = Description + " Close Date: " + FormatDate(PreviousRecord.Opportunity.CloseDate) + " to " + FormatDate(NewRecord.Opport…
                IF PreviousRecord.Opportunity.Amount <>NewRecord.Opportunity.Amount   «<> Amount»
                  THEN:
                    Description = Description + " Amount: " + FormatCurrency(PreviousRecord.Opportunity.Amount, Site.Currency, 0, Site.DecimalSeparator, …
                    IF PreviousRecord.Opportunity.Stage <>NewRecord.Opportunity.Stage   «<> Stage»
                      THEN:
                        QUERY PreviousStage from OpportunityStage; where OpportunityStage.Id = PreviousRecord.Opportunity.Stage
                        QUERY NewStage from OpportunityStage; where OpportunityStage.Id = NewRecord.Opportunity.Stage
                        Description = Description + " Stage: " + PreviousStage.List.Current.OpportunityStage.Label + " to " +NewStage.List.Current.Opportunit…
                        IF PreviousRecord.Opportunity.CompanyId <>NewRecord.Opportunity.CompanyId   «<> Company»
                          THEN:
                            QUERY PreviousCompany from Company; where Company.Id = PreviousRecord.Opportunity.CompanyId
                            QUERY NewCompany from Company; where Company.Id = NewRecord.Opportunity.CompanyId
                            Description = Description + " Stage: '" + PreviousCompany.List.Current.Company.CompanyName+ "' to '" + NewCompany.List.Current.Compan…
                            ↪ continue at «AddRecord»
                          ELSE:
                            ↪ continue at «AddRecord»
                      ELSE:
                        ↪ continue at «<> Company»
                  ELSE:
                    ↪ continue at «<> Stage»
              ELSE:
                ↪ continue at «<> Amount»
          ELSE:
            ↪ continue at «<> CloseDate»
      ELSE:
        ↪ continue at «<> Description»
```

### Sales › AccountManager_SetupQuotas › SaveQuota  _(screen)_
```text
FOR EACH AccountManagerTableRecords.List:
  AccountManagerTableRecords.List.Current.QuarterQuota.UserId = AccountManagerTableRecords.List.Current.User.Id
  CALL server CreateOrUpdateQuarterQuota(AccountManagerTableRecords.List.Current.QuarterQuota)
  ↪ continue at «For Each AccountManager»
CALL server Feedback_Message("Quarter Quotas saved.", Entities.MessageType.Success)
GO TO Dashboard
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › AccountManager_SetupQuotas › Preparation  _(preparation)_
```text
QUERY GetAccountManagerQuotas from QuarterQuota, User; where User.Is_Active = True AND CheckRole(Roles.AccountManager, User.Id)=True AND User.Username <> "admin"
END
```

### Sales › Dashboard › Preparation  _(preparation)_
```text
CALL server CheckSalesManagerRole()
IF CheckSalesManagerRole.HasRole
  THEN:
    GO TO Dashboard_SalesManager
  ELSE:
    CALL server DashboardQuarterStatus()
    CALL server GetCurrentAccountManager()
    QUERY GetOpportunities from AccountManager, Company, Opportunity, OpportunityStage; where (GetCurrentAccountManager.AccountManager = NullIdentifier()) or (AccountManager.UserId = GetCurrent… AND Opportunity.Stage <> Entities.OpportunityStage.Won and Opportunity.Stage <> Entities.OpportunitySta…
    CALL server DashboardForecast()
    END
```

### Sales › Dashboard_SalesManager › Preparation  _(preparation)_
```text
CALL server DashboardQuarterStatus()
CALL server DashboardByAccountManager()
CALL server DashboardForecast()
END
```

### Sales › Opportunities_ListClosed › ExportToExcel  _(screen)_
```text
GetOpportunities_SearchKeyword_IsEmpty = Session.Opportunity_List_SearchKeyword = ""
REFRESH GetOpportunities
RECORDLISTTOEXCEL RecordListToExcel
DOWNLOAD
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Opportunities_ListClosed › RefreshOpportunityTable  _(screen)_
In: ResetPagination: Boolean · Out: —
```text
IF ResetPagination
  THEN:
    CALL server List_Navigation_ResetStartIndex(OpportunityTable.Id)
    NumberOfRows = 5
    GetOpportunities_SearchKeyword_IsEmpty = Session.Opportunity_List_SearchKeyword = ""
    REFRESH GetOpportunities
    AJAXREFRESH
    END
  ELSE:
    ↪ continue at «Created by the upgrade of 'GetOpportunities' to an Aggregate.»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Opportunities_ListClosed › Preparation  _(preparation)_
```text
CALL server GetCurrentAccountManager()
GetOpportunities_SearchKeyword_IsEmpty = Session.Opportunity_List_SearchKeyword = ""
QUERY GetOpportunities from AccountManager, Company, Opportunity, OpportunityStage, User; where (GetOpportunities_SearchKeyword_IsEmpty = True) or (Opportunity.Name like "%" + Session.Opportunity… AND Opportunity.Stage = Entities.OpportunityStage.Won or Opportunity.Stage= Entities.OpportunityStage.L… AND (GetCurrentAccountManager.AccountManager = NullIdentifier()) or (AccountManager.UserId = GetCurrent…
END
```

### Sales › Opportunity_Edit › AutoCompleteCompany  _(screen)_
```text
GetCompany_SearchKeyword = NotifyGetMessage(); GetCompany_AccountManagerId = GetCurrentAccountManager(); GetCompany_SearchKeyword_IsEmpty = GetCompany_SearchKeyword = ""
QUERY GetCompany from AccountManager, Company; where (GetCompany_SearchKeyword_IsEmpty = True) or (Company.CompanyName like GetCompany_SearchKeyword) AND (GetCompany_AccountManagerId = NullIdentifier()) or (AccountManager.UserId = GetCompany_AccountMana…
FOR EACH GetCompany.List:
  AutoCompleteSuggestion.Input_AutoComplete_ListEntry.Label = GetCompany.List.Current.Company.CompanyName; AutoCompleteSuggestion.Input_AutoComplete_ListEntry.Identifier = GetCompany.List.Current.Company.Id
  CALL server ListAppend(AutoCompleteSuggestion, AutoCompleteSuggestionList)
  ↪ continue at «ForEach»
CALL server Input_AutoComplete_ShowList(COMPANY_CompanyName.Id, AutoCompleteSuggestionList)
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Opportunity_Edit › DeleteOpportunity  _(screen)_
```text
CALL server DeleteOpportunity(OpportunityId)
CALL server Feedback_Message("The opportunity was successfully deleted.", Entities.MessageType.Success)
GO TO ExternalURL
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Opportunity_Edit › Save  _(screen)_
```text
CALL server Input_AutoComplete_GetIdentifier(COMPANY_CompanyName.Id, OpportunityEdit.Record.Opportunity.CompanyId)
IF COMPANY_CompanyName_GetIdentifier.Identifier = -1   «Invalid CompanyId?»
  THEN:
    NewCompany.Company.CompanyName = OpportunityEdit.Record.Company.CompanyName
    CALL server Company_CreateOrUpdate(NewCompany)
    NewAccountManager.AccountManager.CompanyId = Company_Create.Id; NewAccountManager.AccountManager.UserId = GetUserId()
    CALL server CreateAccountManager(NewAccountManager)
    OpportunityEdit.Record.Opportunity.CompanyId = Company_Create.Id
    IF OpportunityEdit.Record.Opportunity.CloseDate<CurrDate() and OpportunityEdit.Record.Opportunity.Stage<> Entities.OpportunityStage.Won and OpportunityEdit.Record.Opportunity.Stage<> Entities.Opportunit…   «Past CloseDate and <>Won and <>Lost»
      THEN:
        OPPORTUNITY_CloseDate.Valid = False; OPPORTUNITY_CloseDate.ValidationMessage = "You can not set dates in the past."
        IF OpportunityEdit.Valid   «Valid»
          THEN:
            OpportunityEdit.Record.Opportunity.LastModified = CurrDateTime()
            CALL server CreateOrUpdateOpportunity(OpportunityEdit.Record.Opportunity)
            CALL server OpportunityHistoryLog(OpportunityEdit.Record.Opportunity, GetOpportunityById.List.Current.Opportunity)
            CALL server Feedback_Message("Opportunity '" + OpportunityEdit.Record.Opportunity.Name +…, Entities.MessageType.Success)
            GO TO Opportunity_Show
          ELSE:
            AJAXREFRESH
            CALL server Input_FocusFirstInvalid()
            END
      ELSE:
        ↪ continue at «Valid»
  ELSE:
    OpportunityEdit.Record.Opportunity.CompanyId = IntegerToIdentifier(TextToInteger(COMPANY_CompanyName_GetIdentifier.Identifier))
    ↪ continue at «Past CloseDate and <>Won and <>Lost»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Opportunity_Edit › Preparation  _(preparation)_
```text
QUERY GetOpportunityById from Company, Opportunity; where Opportunity.Id = OpportunityId; max 1
IF not GetOpportunityById.List.Empty   «existing opportunity»
  THEN:
    IF CheckPermissionsForCompany(GetOpportunityById.List.Current.Opportunity.CompanyId)   «Admin or AccountManager»
      THEN:
        IF CompanyId <> NullIdentifier()
          THEN:
            QUERY GetCompanyById from Company; where Company.Id = CompanyId
            GetOpportunityById.List.Current.Company.CompanyName = GetCompanyById.List.Current.Company.CompanyName; GetOpportunityById.List.Current.Opportunity.CompanyId = GetCompanyById.List.Current.Company.Id
            END
          ELSE:
            ↪ continue at «End»
      ELSE:
        RAISE NoPermissionForSelectedCompany: "You do not have permissions for the selected company."
  ELSE:
    ↪ continue at «If»
```

### Sales › Opportunity_List › ExportToExcel  _(screen)_
```text
GetOpportunities_SearchKeyword_IsEmpty = Session.Opportunity_List_SearchKeyword = ""
REFRESH GetOpportunities
RECORDLISTTOEXCEL RecordListToExcel
DOWNLOAD
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Opportunity_List › RefreshOpportunityTable  _(screen)_
In: ResetPagination: Boolean · Out: —
```text
SWITCH
  CASE Session.Opportunity_List_Time = "Month":
    UntilDate = AddDays(AddMonths(CurrDate(),1), -Day(AddMonths(CurrDate(),1))); Order = "{OpportunityStage}.[Label] DESC, {Opportunity}.[Amount] Desc"
    IF ResetPagination
      THEN:
        NumberOfRows = 5
        CALL server List_Navigation_ResetStartIndex(OpportunityTable_Wrapper.Id)
        GetOpportunities_SearchKeyword_IsEmpty = Session.Opportunity_List_SearchKeyword = ""
        REFRESH GetOpportunities
        AJAXREFRESH
        END
      ELSE:
        ↪ continue at «Created by the upgrade of 'GetOpportunities' to an Aggregate.»
  CASE Session.Opportunity_List_Time = "Quarter":
    UntilDate = GetQuarterEnd(CurrDate()); Order = "{OpportunityStage}.[Label] DESC, {Opportunity}.[Amount] Desc"
    ↪ continue at «If»
  OTHERWISE:
    UntilDate = NullDate(); Order = "{Opportunity}.[CloseDate] ASC"
    ↪ continue at «If»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Opportunity_List › Preparation  _(preparation)_
```text
CALL server CheckSalesManagerRole()
IF not CheckSalesManagerRole.HasRole   «User is only AccountManager»
  THEN:
    Session.Opportunity_List_AccountManager = GetUserId()
    SWITCH
      CASE Session.Opportunity_List_Time = "Month":
        UntilDate = AddDays(AddMonths(CurrDate(),1), -Day(AddMonths(CurrDate(),1))); Order = "{OpportunityStage}.[Label] DESC, {Opportunity}.[Amount] Desc"
        GetOpportunities_SearchKeyword_IsEmpty = Session.Opportunity_List_SearchKeyword = ""
        QUERY GetOpportunities from AccountManager, Company, Opportunity, OpportunityStage, User; where (GetOpportunities_SearchKeyword_IsEmpty = True) or (Opportunity.Name like "%" + Session.Opportunity… AND Opportunity.Stage<> Entities.OpportunityStage.Won and Opportunity.Stage<> Entities.OpportunityStage… AND (UntilDate = NullDate()) or (Opportunity.CloseDate <= UntilDate and Opportunity.CloseDate <> NullDa… AND (Session.Opportunity_List_AccountManager = NullIdentifier()) or (AccountManager.UserId = Session.Op…
        QUERY GetAccountManagers from User; where User.Is_Active = True AND CheckRole(Roles.AccountManager, User.Id)=True AND User.Username <> "admin"
        END
      CASE Session.Opportunity_List_Time = "Quarter":
        UntilDate = GetQuarterEnd(CurrDate()); Order = "{OpportunityStage}.[Label] DESC, {Opportunity}.[Amount] Desc"
        ↪ continue at «Created by the upgrade of 'GetOpportunities' to an Aggregate.»
      OTHERWISE:
        UntilDate = NullDate(); Order = "{Opportunity}.[CloseDate] ASC"
        ↪ continue at «Created by the upgrade of 'GetOpportunities' to an Aggregate.»
  ELSE:
    IF AccountManager <> NullIdentifier()   «Has AccountManager Input Parameter»
      THEN:
        Session.Opportunity_List_AccountManager = AccountManager
        ↪ continue at «Switch»
      ELSE:
        ↪ continue at «Switch»
```

### Sales › Opportunity_Show › OnChangeState  _(screen)_
```text
CALL server GetOpportunity(OpportunityId)
CALL server GetOpportunity(OpportunityId)
GetOpportunity2.Record.Opportunity.LastModified = CurrDateTime(); GetOpportunity2.Record.Opportunity.Stage = StageId
CALL server CreateOrUpdateOpportunity(GetOpportunity2.Record)
CALL server OpportunityHistoryLog(GetOpportunity2.Record, GetOpportunity.Record)
CALL server Feedback_Message("Opportunity '" + GetOpportunity.Record.Opportunity.Name + …, Entities.MessageType.Success)
ChangeStage = not ChangeStage
REFRESH GetOpportunityById
AJAXREFRESH
AJAXREFRESH
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Opportunity_Show › SwitchChangeStage  _(screen)_
```text
ChangeStage = not ChangeStage
AJAXREFRESH
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Opportunity_Show › Preparation  _(preparation)_
```text
QUERY GetOpportunityById from AccountManager, Company, Opportunity, OpportunityStage, User; where Opportunity.Id = OpportunityId; max 1
StageId = GetOpportunityById.List.Current.Opportunity.Stage
IF CheckPermissionsForCompany(GetOpportunityById.List.Current.Opportunity.CompanyId)   «Admin or AccountManager»
  THEN:
    IF not GetOpportunityById.List.Empty   «Exists?»
      THEN:
        CALL server RecentItem_Add(GetOpportunityById.List.Current.Opportunity.Name, "Opportunity")
        QUERY GetContactsByCompanyId from Contact; where Contact.CompanyId = GetOpportunityById.List.Current.Opportunity.CompanyId
        END
      ELSE:
        CALL server RecentItem_Remove()
        CALL server Feedback_Message("Opportunity has been deleted.", Entities.MessageType.Error)
        GO TO Opportunity_List
  ELSE:
    RAISE NoPermissionForSelectedCompany: "You do not have permissions for the selected company."
```

### Sales › Company_Edit › AutoCompleteCountry  _(screen)_
```text
GetCountry_SearchKeyword = NotifyGetMessage(); GetCountry_SearchKeyword_IsEmpty = GetCountry_SearchKeyword = ""
QUERY GetCountry from Country; where (GetCountry_SearchKeyword_IsEmpty = True) or (Country.Name like GetCountry_SearchKeyword)
FOR EACH GetCountry.List:
  AutoCompleteSuggestion.Input_AutoComplete_ListEntry.Label = GetCountry.List.Current.Country.Name; AutoCompleteSuggestion.Input_AutoComplete_ListEntry.Identifier = GetCountry.List.Current.Country.Id
  CALL server ListAppend(AutoCompleteSuggestion, AutoCompleteSuggestionList)
  ↪ continue at «ForEach»
CALL server Input_AutoComplete_ShowList(Country_Name.Id, AutoCompleteSuggestionList)
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Company_Edit › DeleteCompany  _(screen)_
```text
CALL server Company_Delete(CompanyId)
CALL server Feedback_Message("The company was successfully deleted.", Entities.MessageType.Success)
GO TO ExternalURL
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Company_Edit › Save  _(screen)_
```text
CALL server Input_AutoComplete_GetIdentifier(Country_Name.Id, CompanyEdit.Record.Company.CountryId)
IF Country_Name_GetIdentifier.Identifier = -1   «Invalid CountryId?»
  THEN:
    Country_Name.Valid = False; Country_Name.ValidationMessage = "Please select a valid Country"
    IF CompanyEdit.Record.Company.Website = "" or Index(ToLower(CompanyEdit.Record.Company.Website),"http://")=0   «Valid Website»
      THEN:
        IF CompanyEdit.Record.Company.Email = "" or EmailAddressValidate(CompanyEdit.Record.Company.Email)   «Valid Email»
          THEN:
            QUERY GetRepeatedCompanyNames from Company; where Company.Id <> CompanyEdit.Record.Company.Id AND Company.CompanyName = CompanyEdit.Record.Company.CompanyName
            IF GetRepeatedCompanyNames.List.Empty   «Unique Company Name»
              THEN:
                IF CompanyEdit.Valid   «Valid»
                  THEN:
                    CALL server Company_CreateOrUpdate(CompanyEdit.Record.Company)
                    IF CompanyEdit.Record.AccountManager.UserId<>NullIdentifier()
                      THEN:
                        CompanyEdit.Record.AccountManager.CompanyId = CreateOrUpdateCOMPANY.Id
                        CALL server CreateOrUpdateAccountManager(CompanyEdit.Record.AccountManager)
                        CALL server Feedback_Message("Company '" + CompanyEdit.Record.Company.CompanyName + "' w…, Entities.MessageType.Success)
                        IF GetCompanyById.List.Current.AccountManager.UserId <> CompanyEdit.Record.AccountManager.UserId and CompanyId<>NullIdentifier()   «Changed AccountManager?»
                          THEN:
                            CALL server Company_CreateHistory(CreateOrUpdateCOMPANY.Id, If(GetCompanyById.List.Current.AccountManager.UserId = Null…)
                            IF CompanyEdit.Record.AccountManager.UserId <> GetUserId() and CompanyEdit.Record.AccountManager.UserId <> NullIdentifier()   «Notify AccountManager?»
                              THEN:
                                Task.Task.Name = "Followup"; Task.Task.UserId = CompanyEdit.Record.AccountManager.UserId; Task.Task.DueDate = CurrDate(); Task.Task.CompanyId = CreateOrUpdateCOMPANY.Id
                                CALL server CreateTask(Task)
                                CALL server CheckPermissionsForCompany(CreateOrUpdateCOMPANY.Id)
                                IF CheckPermissionsForCompany.HasPermissions   «Can view Company?»
                                  THEN:
                                    GO TO Company_Show
                                  ELSE:
                                    GO TO Company_List
                              ELSE:
                                ↪ continue at «CheckPermissionsForCompany»
                          ELSE:
                            ↪ continue at «CheckPermissionsForCompany»
                      ELSE:
                        CALL server DeleteAccountManager(CreateOrUpdateCOMPANY.Id)
                        ↪ continue at «Feedback_Message»
                  ELSE:
                    AJAXREFRESH
                    CALL server Input_FocusFirstInvalid()
                    END
              ELSE:
                COMPANY_CompanyName.Valid = False; COMPANY_CompanyName.ValidationMessage = "There is already one Company with the same Company Name"
                ↪ continue at «Valid»
          ELSE:
            COMPANY_Email.Valid = False; COMPANY_Email.ValidationMessage = "Invalid Email Address"
            ↪ continue at «GetRepeatedCompanyNames»
      ELSE:
        COMPANY_Website.Valid = False; COMPANY_Website.ValidationMessage = "Website need to start with http://"
        ↪ continue at «Valid Email»
  ELSE:
    CompanyEdit.Record.Company.CountryId = IntegerToIdentifier(TextToInteger(Country_Name_GetIdentifier.Identifier))
    ↪ continue at «Valid Website»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Company_Edit › Preparation  _(preparation)_
```text
QUERY GetAccountManagers from User; where User.Is_Active = True AND CheckRole(Roles.AccountManager, User.Id)=True AND User.Username <> "admin"
QUERY GetCompanyById from AccountManager, Company, Country; where Company.Id = CompanyId; max 1
IF CompanyId = NullIdentifier()   «new»
  THEN:
    GetCompanyById.List.Current.AccountManager.UserId = GetUserId()
    END
  ELSE:
    IF CheckPermissionsForCompany(CompanyId)
      THEN:
        ↪ continue at «End»
      ELSE:
        RAISE NoPermissionForSelectedCompany: "You do not have permissions for the selected company."
```

### Sales › Company_FullHistory › RefreshList  _(screen)_
```text
REFRESH GetHistoriesByCompanyId
AJAXREFRESH
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Company_FullHistory › Preparation  _(preparation)_
```text
QUERY GetCompanyById from AccountManager, Company, User; where Company.Id = CompanyId; max 1
IF CompanyId <> NullIdentifier()
  THEN:
    IF CheckPermissionsForCompany(CompanyId)   «Admin or AccountManager»
      THEN:
        CALL server RecentItem_Add(If(CompanyId = NullIdentifier(), "", GetCompanyById.List.Cu…, "History")
        SQL GetHistoriesByCompanyId (see SQL below)
        END
      ELSE:
        RAISE NoPermissionForSelectedCompany: "You do not have permissions for the selected company."
  ELSE:
    ↪ continue at «RecentItem_Add»
```
SQL `GetHistoriesByCompanyId`:
```sql
SELECT {CompanyHistory}.Id, {Company}.[Id],0,0,{CompanyHistory}.[UserId],{CompanyHistory}.[Instant], 
        {CompanyHistory}.[Description], {User}.[Name], {Company}.[CompanyName], '', ''
     FROM {CompanyHistory} 
            INNER JOIN {Company} on {Company}.[Id] = {CompanyHistory}.[CompanyId]
            INNER JOIN {User} on {CompanyHistory}.[UserId] = {User}.[Id]
            LEFT JOIN {AccountManager} ON {AccountManager}.[CompanyId] = {Company}.[Id]
    WHERE ({Company}.[Id] = @CompanyId or @CompanyId = 0) 
        and ({AccountManager}.[UserId] = @AccountManager or @AccountManager = 0)
        and @Keyword = ''

UNION
SELECT  {ContactHistory}.[Id], {Company}.[Id], {Contact}.[Id],0,{ContactHistory}.[UserId], {ContactHistory}.[Instant], 
        {ContactHistory}.[Description], {User}.[Name], {Company}.[CompanyName], {Contact}.[Name], ''
    FROM {ContactHistory} 
        INNER JOIN {Contact} on {ContactHistory}.[ContactId] = {Contact}.[Id]
        INNER JOIN {Company} on {Contact}.[CompanyId] = {Company}.[Id]
        LEFT JOIN {User} on {ContactHistory}.[UserId] = {User}.[Id]
        LEFT JOIN {AccountManager} ON {AccountManager}.[CompanyId] = {Company}.[Id]
    WHERE ({Company}.[Id] = @CompanyId or @CompanyId = 0) 
        and ({AccountManager}.[UserId] = @AccountManager or @AccountManager = 0)
        and @Keyword = ''

UNION
SELECT {OpportunityHistory}.Id, {Company}.[Id], 0, {OpportunityHistory}.[OpportunityId], {OpportunityHistory}.[UserId], {OpportunityHistory}.[Instant], 
        {OpportunityHistory}.[Description], {User}.[Name], {Company}.[CompanyName], '', {Opportunity}.[Name]
    FROM {OpportunityHistory}
        INNER JOIN {Opportunity} on {OpportunityHistory}.[OpportunityId] = {Opportunity}.[Id]
        INNER JOIN {Company} on {Opportunity}.[CompanyId] = {Company}.[Id]
        INNER JOIN {User} on {OpportunityHistory}.[UserId] = {User}.[Id]
        LEFT JOIN {AccountManager} ON {AccountManager}.[CompanyId] = {Company}.[Id]
    WHERE ({Company}.[Id] = @CompanyId or @CompanyId = 0) 
        and ({AccountManager}.[UserId] = @AccountManager or @AccountManager = 0)
        and (@Keyword = '' or {OpportunityHistory}.[Description] like @KeywordSearch)

ORDER BY [Instant] DESC
```

### Sales › Company_List › ExportToExcel  _(screen)_
```text
REFRESH GetCompanies
RECORDLISTTOEXCEL RecordListToExcel
DOWNLOAD
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Company_List › RefreshCompanyTable  _(screen)_
In: ResetFilters: Boolean, ResetPagination: Boolean · Out: —
```text
IF ResetFilters
  THEN:
    Session.Company_List_SearchKeyword = ""; NumberOfRows = 10
    AJAXREFRESH
    IF ResetPagination
      THEN:
        CALL server List_Navigation_ResetStartIndex(CompanyTable.Id)
        NumberOfRows = 10
        REFRESH GetCompanies
        AJAXREFRESH
        END
      ELSE:
        ↪ continue at «RefreshData»
  ELSE:
    ↪ continue at «If»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Company_List › Preparation  _(preparation)_
```text
CALL server GetCurrentAccountManager()
IF GetCurrentAccountManager.AccountManager <> NullIdentifier()   «Is Account Manager»
  THEN:
    Session.Opportunity_List_AccountManager = GetCurrentAccountManager.AccountManager
    QUERY GetCompanies from AccountManager, Company, Country, User; where Company.CompanyName like "%" + Session.Company_List_SearchKeyword + "%" AND (Session.Opportunity_List_AccountManager = NullIdentifier()) or (AccountManager.UserId = Session.Op…
    QUERY GetAccountManagers from User; where User.Is_Active = True AND CheckRole(Roles.AccountManager, User.Id) = True AND User.Username <> "admin"
    END
  ELSE:
    ↪ continue at «GetCompanies»
```

### Sales › Company_Show › Preparation  _(preparation)_
```text
IF CheckPermissionsForCompany(CompanyId)   «Admin or AccountManager»
  THEN:
    QUERY GetCompanyById from AccountManager, Company, Country, User; where Company.Id = CompanyId; max 1
    IF not GetCompanyById.List.Empty   «Exists?»
      THEN:
        CALL server RecentItem_Add(GetCompanyById.List.Current.Company.CompanyName, "Company")
        QUERY GetContactsByCompanyId from Contact, ContactPicture; where Contact.CompanyId = CompanyId
        QUERY GetOpportunitiesByCompanyId from Opportunity, OpportunityStage; where Opportunity.CompanyId = CompanyId AND Opportunity.Stage<> Entities.OpportunityStage.Won and Opportunity.Stage<> Entities.OpportunityStage…
        QUERY GetClosedOpportunities from Opportunity, OpportunityStage; where Opportunity.CompanyId = CompanyId AND Opportunity.Stage = Entities.OpportunityStage.Won or Opportunity.Stage = Entities.OpportunityStage.…
        END
      ELSE:
        CALL server RecentItem_Remove()
        CALL server Feedback_Message("Company has been deleted.", Entities.MessageType.Error)
        GO TO Company_List
  ELSE:
    RAISE NoPermissionForSelectedCompany: "You do not have permissions for the selected company."
```

### Sales › Contact_Edit › AutoCompleteCompany  _(screen)_
```text
GetCompany_SearchKeyword = NotifyGetMessage(); GetCompany_AccountManagerId = GetCurrentAccountManager(); GetCompany_SearchKeyword_IsEmpty = GetCompany_SearchKeyword = ""
QUERY GetCompany from AccountManager, Company; where (GetCompany_SearchKeyword_IsEmpty = True) or (Company.CompanyName like GetCompany_SearchKeyword) AND (GetCompany_AccountManagerId = NullIdentifier()) or (AccountManager.UserId = GetCompany_AccountMana…
FOR EACH GetCompany.List:
  AutoCompleteSuggestion.Input_AutoComplete_ListEntry.Label = GetCompany.List.Current.Company.CompanyName; AutoCompleteSuggestion.Input_AutoComplete_ListEntry.Identifier = GetCompany.List.Current.Company.Id
  CALL server ListAppend(AutoCompleteSuggestion, AutoCompleteSuggestionList)
  ↪ continue at «ForEach»
CALL server Input_AutoComplete_ShowList(COMPANY_CompanyName.Id, AutoCompleteSuggestionList)
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Contact_Edit › ChangePicture  _(screen)_
```text
CALL server Popup_Upload_GetFile(TextToInteger(NotifyGetMessage()))
CALL server Contact_CreateOrUpdatePicture(ContactId, Popup_Upload_GetFile.File.UploadedFile.Content, Popup_Upload_GetFile.File.UploadedFile.Filename, Popup_Upload_GetFile.File.UploadedFile.Filetype)
ContactEdit.Record.ContactPicture = Record
AJAXREFRESH
CALL server Feedback_Message("The picture was successfully changed.", Entities.MessageType.Success)
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Contact_Edit › DeleteContact  _(screen)_
```text
CALL server Contact_Delete(ContactId)
CALL server Feedback_Message("Contact deleted.", Entities.MessageType.Success)
GO TO Contact_List
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Contact_Edit › Save  _(screen)_
In: AndNew: Boolean · Out: —
```text
CALL server Input_AutoComplete_GetIdentifier(COMPANY_CompanyName.Id, ContactEdit.Record.Contact.CompanyId)
IF COMPANY_CompanyName_GetIdentifier.Identifier = -1   «Invalid CompanyId?»
  THEN:
    NewCompany.Company.CompanyName = ContactEdit.Record.Company.CompanyName
    CALL server Company_CreateOrUpdate(NewCompany)
    NewAccountManager.AccountManager.CompanyId = CreateCompany.Id; NewAccountManager.AccountManager.UserId = GetUserId()
    CALL server CreateAccountManager(NewAccountManager)
    ContactEdit.Record.Contact.CompanyId = CreateCompany.Id
    IF ContactEdit.Record.Contact.Email = "" or EmailAddressValidate(ContactEdit.Record.Contact.Email)   «Valid Email»
      THEN:
        IF ContactEdit.Valid   «Valid»
          THEN:
            CALL server Contact_CreateOrUpdate(ContactEdit.Record.Contact)
            CALL server Feedback_Message("Contact '" + ContactEdit.Record.Contact.Name + "' was succ…, Entities.MessageType.Success)
            IF AndNew
              THEN:
                GO TO Contact_Edit
              ELSE:
                GO TO Contact_Show
          ELSE:
            AJAXREFRESH
            CALL server Input_FocusFirstInvalid()
            END
      ELSE:
        CONTACT_Email.Valid = False; CONTACT_Email.ValidationMessage = "Invalid Email Address"
        ↪ continue at «Valid»
  ELSE:
    ContactEdit.Record.Contact.CompanyId = IntegerToIdentifier(TextToInteger(COMPANY_CompanyName_GetIdentifier.Identifier))
    ↪ continue at «Valid Email»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Contact_Edit › Preparation  _(preparation)_
```text
QUERY GetContactById from Company, Contact, ContactPicture; where Contact.Id = ContactId; max 1
IF not GetContactById.List.Empty   «existing contact»
  THEN:
    IF CheckPermissionsForCompany(GetContactById.List.Current.Contact.CompanyId)   «Admin or AccountManager»
      THEN:
        IF CompanyId <> NullIdentifier()
          THEN:
            QUERY GetCompanyById from Company; where Company.Id = CompanyId
            GetContactById.List.Current.Company.CompanyName = GetCompanyById.List.Current.Company.CompanyName; GetContactById.List.Current.Contact.CompanyId = GetCompanyById.List.Current.Company.Id
            END
          ELSE:
            ↪ continue at «End»
      ELSE:
        RAISE NoPermissionForSelectedCompany: "You do not have permissions for the selected company."
  ELSE:
    ↪ continue at «If»
```

### Sales › Contact_Import › DownloadSampleFile  _(screen)_
```text
DOWNLOAD
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Contact_Import › ImportContacts  _(screen)_
```text
EXCEL→LIST ContactsToImport from ContactXlsxFilename.Content
FOR EACH ContactsToImport:
  QUERY GetContact from Contact; where ContactsToImport.Current.Excel_Contacts.Name = Contact.Name AND ContactsToImport.Current.Excel_Contacts.Email = Contact.Email
  IF GetContact.Count > 0 and Overwrite = False   «Discard?»
    THEN:
      ↪ continue at «ForEach»
    ELSE:
      QUERY GetCompanyByEmail from Company; where Company.Website like "%" + Substr(ContactsToImport.Current.Excel_Contacts.Email,Index(ContactsToImp…
      IF GetCompanyByEmail.Count > 0   «CompanyFound»
        THEN:
          GetContact.List.Current.Contact.Name = ContactsToImport.Current.Excel_Contacts.Name; GetContact.List.Current.Contact.JobTitle = ContactsToImport.Current.Excel_Contacts.JobTitle; GetContact.List.Current.Contact.Phone = ContactsToImport.Current.Excel_Contacts.Phone; GetContact.List.Current.Contact.Email = ContactsToImport.Current.Excel_Contacts.Email; GetContact.List.Current.Contact.CompanyId = GetCompanyByEmail.List.Current.Company.Id
          CALL server Contact_CreateOrUpdate(GetContact.List.Current)
          ↪ continue at «ForEach»
        ELSE:
          ErrorsString = ErrorsString + " - Contact '" + ContactsToImport.Current.Excel_Contacts.Name + "' company's not registered." + NewLine()
          ↪ continue at «ForEach»
IF Length(ErrorsString) <> 0   «Has Errors?»
  THEN:
    GO TO Contact_Import
  ELSE:
    GO TO Contact_List
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Contact_List › ExportToExcel  _(screen)_
```text
GetContacts_AccountManagerId = GetCurrentAccountManager(); GetContacts_SearchKeyword_IsEmpty = Session.Contact_List_SearchKeyword = ""
REFRESH GetContacts
RECORDLISTTOEXCEL RecordListToExcel
DOWNLOAD
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Contact_List › OnNotifyRefreshContactTable  _(screen)_
In: ResetFilters: Boolean, ResetPagination: Boolean · Out: —
```text
IF ResetFilters
  THEN:
    Session.Contact_List_SearchKeyword = ""
    IF ResetPagination
      THEN:
        CALL server List_Navigation_ResetStartIndex(ContactTable.Id)
        REFRESH GetContacts
        AJAXREFRESH
        END
      ELSE:
        ↪ continue at «RefreshData»
  ELSE:
    ↪ continue at «If»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Contact_List › Preparation  _(preparation)_
```text
GetContacts_AccountManagerId = GetCurrentAccountManager(); GetContacts_SearchKeyword_IsEmpty = Session.Contact_List_SearchKeyword = ""
QUERY GetContacts from AccountManager, Company, Contact, ContactPicture; where (GetContacts_SearchKeyword_IsEmpty = True) or (Contact.Name like "%" + Session.Contact_List_SearchK… AND (GetContacts_AccountManagerId = NullIdentifier()) or (AccountManager.UserId = GetContacts_AccountMa…
END
```

### Sales › Contact_Show › Preparation  _(preparation)_
```text
QUERY GetContactById from Company, Contact, ContactPicture; where Contact.Id = ContactId; max 1
IF CheckPermissionsForCompany(GetContactById.List.Current.Contact.CompanyId)   «Admin or AccountManager»
  THEN:
    IF not GetContactById.List.Empty   «Exists?»
      THEN:
        CALL server RecentItem_Add(GetContactById.List.Current.Contact.Name, "Contact")
        QUERY GetOpportunitiesByCompanyId from Opportunity, OpportunityStage; where Opportunity.CompanyId = GetContactById.List.Current.Contact.CompanyId AND Opportunity.Stage<> Entities.OpportunityStage.Won and Opportunity.Stage<> Entities.OpportunityStage…
        IF GetContactById.List.Current.Contact.CompanyId <> NullIdentifier()   «Has Company?»
          THEN:
            QUERY GetOtherContactsByCompanyId from Contact; where Contact.CompanyId = GetContactById.List.Current.Contact.CompanyId AND Contact.Id <> ContactId
            END
          ELSE:
            ↪ continue at «End»
      ELSE:
        CALL server RecentItem_Remove()
        CALL server Feedback_Message("Contact has been deleted.", Entities.MessageType.Error)
        GO TO Contact_List
  ELSE:
    RAISE NoPermissionForSelectedCompany: "You do not have permissions for the selected company."
```

### Sales › Login › ForgotPassword  _(screen)_
```text
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Login › Login  _(screen)_
```text
IF not UserNameInput.Valid or not UserNameInput.Valid   «Not Pass or Not UserName?»
  THEN:
    END
  ELSE:
    CALL server User_Login(Password, RememberLogin, UserName)
    GO TO ExternalURL
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › Login › Preparation  _(preparation)_
```text
CALL server AddFaviconTag("favicon.png")
IF GetUserId() <> NullIdentifier()
  THEN:
    GO TO ExternalURL
  ELSE:
    END
```

### Sales › MyInfo › ToggleChangePassword  _(screen)_
In: IsVisible: Boolean · Out: —
```text
ShowChangePassword = IsVisible
AJAXREFRESH
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoPermission › Preparation  _(preparation)_
```text
IF GetUserId() <> NullIdentifier()
  THEN:
    GO TO InvalidPermissions
  ELSE:
    CALL server User_GetUnifiedLoginUrl(If(GetExceptionURL()="", GetOwnerURLPath(), GetExceptionURL…)
    IF User_GetUnifiedLoginUrl.Url<>""
      THEN:
        GO TO ExternalURL
      ELSE:
        GO TO Login
```

### Sales › SearchResults › Preparation  _(preparation)_
```text
GetPendingOpportunities_UserId = GetCurrentAccountManager()
QUERY GetPendingOpportunities from AccountManager, Company, Opportunity, OpportunityStage, User; where Opportunity.Name like "%" + Keyword + "%" AND Opportunity.Stage <> Entities.OpportunityStage.Won and Opportunity.Stage <> Entities.OpportunitySta… AND (GetPendingOpportunities_UserId = NullIdentifier()) or (AccountManager.UserId = GetPendingOpportuni…
GetContacts_UserId = GetCurrentAccountManager()
QUERY GetContacts from AccountManager, Company, Contact, ContactPicture; where Contact.Name like "%" + Keyword + "%" AND (GetContacts_UserId = NullIdentifier()) or (AccountManager.UserId = GetContacts_UserId)
GetCompaniesByAccountManager_UserId = GetCurrentAccountManager()
QUERY GetCompaniesByAccountManager from AccountManager, Company, Country; where Company.CompanyName like "%" + Keyword + "%" AND (GetCompaniesByAccountManager_UserId = NullIdentifier()) or (AccountManager.UserId = GetCompaniesBy…
GetClosedOpportunities_UserId = GetCurrentAccountManager()
QUERY GetClosedOpportunities from AccountManager, Company, Opportunity, OpportunityStage, User; where Opportunity.Description like "%" + Keyword+ "%" AND Opportunity.Stage = Entities.OpportunityStage.Won and Opportunity.Stage = Entities.OpportunityStage… AND (GetClosedOpportunities_UserId = NullIdentifier()) or (AccountManager.UserId = GetClosedOpportuniti…
END
```

### Sales › EmployeeInfo › Preparation  _(preparation)_
```text
IF UserId <> NullIdentifier()   «Has UserId?»
  THEN:
    CALL server Employee_GetPicture_SA(UserId)
    PictureName = Employee_GetPicture_SA.Filename
    CALL server Employee_GetResponsible_SA(UserId)
    QUERY GetResponsibleById from Employee, JobTitle, User; where Employee.UserId = Employee_GetResponsible_SA.ResponsibleId; max 1
    END
  ELSE:
    END
```

### Sales › NoteTaskHistory › AddNote  _(screen)_
```text
IF Note=""
  THEN:
    CALL server Input_FocusFirstInvalid()
    END
  ELSE:
    CALL server NoteOrTaskHistory(CompanyId, ContactId, "added note “" + Note + "”", OpportunityId)
    Note = ""
    AJAXREFRESH
    IF GetHistoriesByCompanyId.List.Empty   «list was empy»
      THEN:
        REFRESH GetHistoriesByCompanyId
        AJAXREFRESH
        END
      ELSE:
        REFRESH GetHistoriesByCompanyId
        CALL server ListInsert(GetHistoriesByCompanyId.List.Current, HistoryTable.List, 0)
        END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoteTaskHistory › AddTask  _(screen)_
```text
TaskEdit.Record.Task.UserId = GetUserId(); TaskEdit.Record.Task.CompanyId = CompanyId; TaskEdit.Record.Task.OpportunityId = OpportunityId; TaskEdit.Record.Task.ContactId = ContactId
IF TaskEdit.Record.Task.DueDate<CurrDate()   «past date»
  THEN:
    Task_DueDate.Valid = False; Task_DueDate.ValidationMessage = "You can't add tasks in the past."
    IF TaskEdit.Valid
      THEN:
        CALL server CreateOrUpdateTask(TaskEdit.Record)
        TaskEdit.Record = BlankTask
        END
      ELSE:
        END
  ELSE:
    ↪ continue at «If»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoteTaskHistory › Cancel  _(screen)_
```text
TaskEdit.Record = BlankTask
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoteTaskHistory › DeleteHistory  _(screen)_
```text
SWITCH
  CASE HistoryTable.List.Current.MergedCompanyHistory.ContactId <> NullIdentifier():
    CALL server Contact_DeleteHistory(IntegerToIdentifier(HistoryTable.List.Current.MergedCompany…)
    CALL server ListRemove(HistoryTable.List, HistoryTable.List.CurrentRowNumber)
    END
  CASE HistoryTable.List.Current.MergedCompanyHistory.OpportunityId <> NullIdentifier():
    CALL server DeleteOpportunityHistory(IntegerToIdentifier(HistoryTable.List.Current.MergedCompany…)
    ↪ continue at «ListRemove»
  OTHERWISE:
    CALL server Company_DeleteHistory(IntegerToIdentifier(HistoryTable.List.Current.MergedCompany…)
    ↪ continue at «ListRemove»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoteTaskHistory › EditTask  _(screen)_
```text
TaskEdit.Record = TaskTable.List.Current.Task; ShowAddTask = True
AJAXREFRESH
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoteTaskHistory › OnTaskCheck  _(screen)_
```text
CALL server UpdateTask(TaskTable.List.Current.Task)
CALL server NoteOrTaskHistory(CompanyId, TaskTable.List.Current.Task.ContactId, "completed task '" + TaskTable.List.Current.Task.Name + "'", TaskTable.List.Current.Task.OpportunityId)
AJAXREFRESH
IF OpportunityId<>NullIdentifier()
  THEN:
    GO TO Opportunity_Show
  ELSE:
    IF ContactId<>NullIdentifier()
      THEN:
        GO TO 3428,4799
      ELSE:
        GO TO Company_Show
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoteTaskHistory › SetTaskDate  _(screen)_
In: Date: Date · Out: —
```text
TaskEdit.Record.Task.DueDate = Date
AJAXREFRESH
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoteTaskHistory › SetTaskName  _(screen)_
In: TaskName: Text · Out: —
```text
TaskEdit.Record.Task.Name = TaskName
AJAXREFRESH
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoteTaskHistory › SwitchNoteTask  _(screen)_
```text
ShowAddTask = not ShowAddTask
AJAXREFRESH
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › NoteTaskHistory › Preparation  _(preparation)_
```text
QUERY GetOpportunityById from Opportunity; where Opportunity.Id = OpportunityId
QUERY GetContactById from Contact, ContactPicture; where Contact.Id = ContactId
QUERY GetCompany from Company; where Company.Id = CompanyId
SQL GetHistoriesByCompanyId (see SQL below)
QUERY GetTasksByUserId from Company, Contact, Opportunity, Task; where Task.UserId = GetUserId() AND Task.IsCompleted = False AND Task.CompanyId = CompanyId
END
```
SQL `GetHistoriesByCompanyId`:
```sql
SELECT {CompanyHistory}.[Id], {Company}.[Id],0,0,{CompanyHistory}.[UserId],{CompanyHistory}.[Instant], 
        {CompanyHistory}.[Description], {User}.[Name], {Company}.[CompanyName], '', ''
     FROM {CompanyHistory} 
            INNER JOIN {Company} on {Company}.[Id] = {CompanyHistory}.[CompanyId]
            INNER JOIN {User} on {CompanyHistory}.[UserId] = {User}.[Id]
    WHERE {Company}.[Id] = @CompanyId

UNION
SELECT {ContactHistory}.[Id], {Company}.[Id], {Contact}.[Id],0,{ContactHistory}.[UserId], {ContactHistory}.[Instant], 
        {ContactHistory}.[Description], {User}.[Name], {Company}.[CompanyName], {Contact}.[Name], ''
    FROM {ContactHistory} 
        INNER JOIN {Contact} on {ContactHistory}.[ContactId] = {Contact}.[Id]
        INNER JOIN {Company} on {Contact}.[CompanyId] = {Company}.[Id]
        LEFT JOIN {User} on {ContactHistory}.[UserId] = {User}.[Id]
    WHERE {Company}.[Id] = @CompanyId 

UNION
SELECT {OpportunityHistory}.[Id], {Company}.[Id], 0, {OpportunityHistory}.[OpportunityId], {OpportunityHistory}.[UserId], {OpportunityHistory}.[Instant], 
        {OpportunityHistory}.[Description], {User}.[Name], {Company}.[CompanyName], '', {Opportunity}.[Name]
    FROM {OpportunityHistory}
        INNER JOIN {Opportunity} on {OpportunityHistory}.[OpportunityId] = {Opportunity}.[Id]
        INNER JOIN {Company} on {Opportunity}.[CompanyId] = {Company}.[Id]
        INNER JOIN {User} on {OpportunityHistory}.[UserId] = {User}.[Id]
    WHERE {Company}.[Id] = @CompanyId

ORDER BY [Instant] DESC
```

### Sales › QuarterStatus › Preparation  _(preparation)_
```text
CALL server DashboardQuarterStatus()
END
```

### Sales › Layout › Preparation  _(preparation)_
```text
CALL server AddFaviconTag("favicon.png")
END
```

### Sales › LoginInfo › Logout  _(screen)_
```text
CALL server User_Logout()
GO TO Login
```

### Sales › LoginInfo › Preparation  _(preparation)_
```text
QUERY GetEmployee from Employee, EmployeePicture, User; where User.Is_Active = True AND User.Id = GetUserId(); max 1
END
```

### Sales › Menu › Preparation  _(preparation)_
```text
ActiveMenuItemId = ActiveMenuItemId; ActiveSubMenuItemId = ActiveSubMenuItemId
END
```

### Sales › SampleUsersLogin › Login  _(screen)_
In: UserId: User Identifier · Out: —
```text
CALL server Login(False, UserId)
GO TO ExternalURL
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Sales › SampleUsersLogin › Preparation  _(preparation)_
```text
QUERY GetSuperUserRole from Role; where Role.Name = "SuperUser"
QUERY GetSampleSalesManagers from User; where CheckRole(Roles.SalesManager, User.Id) = True AND CheckRole(GetSuperUserRole.List.Current.Role.Id,User.Id) = False AND User.Is_Active = True
QUERY GetSampleAccountManagers from User; where CheckRole(Roles.AccountManager, User.Id) = True AND CheckRole(GetSuperUserRole.List.Current.Role.Id,User.Id) = False AND User.Is_Active = True
END
```

### Sales › InfiniteScroll › ScrollEnd  _(screen)_
```text
CALL server NotifyWidget_Deprecated()
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### Customers › Company_Create  _(server)_
In: Source: Company Record · Out: Id: Company Identifier
```text
CALL server CreateCompany(Source)
Id = CreateCompany.Id
CompanyHistory.CompanyHistory.CompanyId = CreateCompany.Id; CompanyHistory.CompanyHistory.Instant = CurrDateTime(); CompanyHistory.CompanyHistory.Description = "Company created"; CompanyHistory.CompanyHistory.UserId = GetUserId()
CALL server CreateCompanyHistory(CompanyHistory)
END
```

### Customers › Company_CreateHistory  _(server)_
In: CompanyId: Company Identifier, Description: Text · Out: —
```text
CompanyHistory.CompanyHistory.Instant = CurrDateTime(); CompanyHistory.CompanyHistory.UserId = GetUserId(); CompanyHistory.CompanyHistory.Description = Description; CompanyHistory.CompanyHistory.CompanyId = CompanyId
CALL server CreateCompanyHistory(CompanyHistory)
END
```

### Customers › Company_CreateOrUpdate  _(server)_
In: Source: Company Record · Out: Id: Company Identifier
```text
IF Source.Company.Id = NullIdentifier()
  THEN:
    CALL server Company_Create(Source)
    Id = Company_Create.Id
    END
  ELSE:
    CALL server Company_Update(Source)
    Id = Source.Company.Id
    ↪ continue at «End»
```

### Customers › Company_Delete  _(server)_
In: CompanyId: Company Identifier · Out: —
```text
CALL server Company_CreateHistory(CompanyId, "deleted company")
CALL server DeleteCompany(CompanyId)
END
```

### Customers › Company_DeleteAllCompanies  _(server)_
```text
SQL DeleteCompanies (see SQL below)
END
```
SQL `DeleteCompanies`:
```sql
DELETE FROM {Company}
```

### Customers › Company_DeleteHistory  _(server)_
In: CompanyHistoryId: CompanyHistory Identifier · Out: —
```text
CALL server DeleteCompanyHistory(CompanyHistoryId)
END
```

### Customers › Company_New  _(server)_
In: CompanyName: Text, CompanyType: CompanyType Identifier, Phone: Phone Number, Email: Text, Fax: Phone Number, Website: Text, Address: Text, PostalCode: Text, City: Text, Country: Country Identifier · Out: Record: Company Record
```text
Record.Company.CompanyName = CompanyName; Record.Company.CompanyType = CompanyType; Record.Company.Phone = Phone; Record.Company.Email = Email; Record.Company.Fax = Fax; Record.Company.Website = Website; Record.Company.Address = Address; Record.Company.PostalCode = PostalCode; Record.Company.City = City; Record.Company.CountryId = Country
END
```

### Customers › Company_Update  _(server)_
In: Source: Company Record · Out: —
```text
CALL server GetCompany(Source.Company.Id)
CALL server UpdateCompany(Source)
Description = "changed company"
IF GetPrevious.Record.Company.CompanyName <> Source.Company.CompanyName   «<> Name»
  THEN:
    Description = Description + " Name: '" + GetPrevious.Record.Company.CompanyName + "' to '" + Source.Company.CompanyName + "'"
    IF GetPrevious.Record.Company.Website <>Source.Company.Website   «<> Website»
      THEN:
        Description = Description + " Website: '" + GetPrevious.Record.Company.Website + "' to '" + Source.Company.Website + "'"
        IF GetPrevious.Record.Company.Phone <>Source.Company.Phone   «<> Phone»
          THEN:
            Description = Description + " Phone: " + GetPrevious.Record.Company.Phone + " to " + Source.Company.Phone
            IF GetPrevious.Record.Company.Email <> Source.Company.Email   «<> Email»
              THEN:
                Description = Description + " Email: '" +GetPrevious.Record.Company.Email + "' to '" + Source.Company.Email + "'"
                IF GetPrevious.Record.Company.Address+ GetPrevious.Record.Company.PostalCode+ GetPrevious.Record.Company.City+ GetPrevious.Record.Company.CountryId <> Source.Company.Address + Source.Company.PostalCode …   «<> Address»
                  THEN:
                    QUERY GetPreviousCountryById from Country; where Country.Id = GetPrevious.Record.Company.CountryId; max 1
                    QUERY GetNewCountryById from Country; where Country.Id = Source.Company.CountryId; max 1
                    Description = Description + " Address: '" + GetPrevious.Record.Company.Address + " " + GetPrevious.Record.Company.PostalCode+ " " + G…
                    CompanyHistory.CompanyHistory.CompanyId = Source.Company.Id; CompanyHistory.CompanyHistory.Instant = CurrDateTime(); CompanyHistory.CompanyHistory.Description = Description; CompanyHistory.CompanyHistory.UserId = GetUserId()
                    CALL server CreateCompanyHistory(CompanyHistory)
                    END
                  ELSE:
                    ↪ continue at «CompanyHistory»
              ELSE:
                ↪ continue at «<> Address»
          ELSE:
            ↪ continue at «<> Email»
      ELSE:
        ↪ continue at «<> Phone»
  ELSE:
    ↪ continue at «<> Website»
```

### Customers › Contact_Create  _(server)_
In: Source: Contact Record · Out: Id: Contact Identifier
```text
CALL server CreateContact(Source)
ContactHistory.ContactHistory.Instant = CurrDateTime(); ContactHistory.ContactHistory.UserId = GetUserId(); ContactHistory.ContactHistory.ContactId = CreateContact.Id; ContactHistory.ContactHistory.Description = "created contact"; Id = CreateContact.Id
CALL server CreateContactHistory(ContactHistory)
END
```

### Customers › Contact_CreateHistory  _(server)_
In: ContactId: Contact Identifier, Description: Text · Out: —
```text
ContactHistory.ContactHistory.Instant = CurrDateTime(); ContactHistory.ContactHistory.UserId = GetUserId(); ContactHistory.ContactHistory.ContactId = ContactId; ContactHistory.ContactHistory.Description = Description
CALL server CreateContactHistory(ContactHistory)
END
```

### Customers › Contact_CreateOrUpdate  _(server)_
In: Source: Contact Record · Out: Id: Contact Identifier
```text
IF Source.Contact.Id = NullIdentifier()
  THEN:
    CALL server Contact_Create(Source)
    Id = Contact_Create.Id
    END
  ELSE:
    CALL server Contact_Update(Source)
    Id = Source.Contact.Id
    ↪ continue at «End»
```

### Customers › Contact_CreateOrUpdatePicture  _(server)_
In: ContactId: Contact Identifier, Filename: Text, Content: Binary Data, Type: Text · Out: Record: ContactPicture Record
```text
Record.ContactPicture.Id = ContactId; Record.ContactPicture.Picture = Content; Record.ContactPicture.Filename = Filename; Record.ContactPicture.Filetype = Type
CALL server CreateOrUpdateContactPicture(Record)
History.ContactHistory.ContactId = ContactId; History.ContactHistory.Instant = CurrDateTime(); History.ContactHistory.Description = "changed picture"; History.ContactHistory.UserId = GetUserId()
CALL server CreateContactHistory(History)
END
```

### Customers › Contact_Delete  _(server)_
In: ContactId: Contact Identifier · Out: —
```text
CALL server GetContact(ContactId)
CALL server Company_CreateHistory(GetContact.Record.Contact.CompanyId, "deleted contact " + GetContact.Record.Contact.Name)
CALL server Contact_CreateHistory(ContactId, "deleted contact")
CALL server DeleteContact(ContactId)
END
```

### Customers › Contact_DeleteAllContacts  _(server)_
```text
SQL DeleteContacts (see SQL below)
END
```
SQL `DeleteContacts`:
```sql
DELETE FROM {Contact}
```

### Customers › Contact_DeleteHistory  _(server)_
In: ContactHistoryId: ContactHistory Identifier · Out: —
```text
CALL server DeleteContactHistory(ContactHistoryId)
END
```

### Customers › Contact_New  _(server)_
In: Name: Text, JobTitle: Text, Phone: Phone Number, Mobile: Phone Number, Email: Text, CompanyId: Company Identifier, SecondaryEmail: Text · Out: Record: Contact Record
```text
Record.Contact.Name = Name; Record.Contact.JobTitle = JobTitle; Record.Contact.Phone = Phone; Record.Contact.Mobile = Mobile; Record.Contact.Email = Email; Record.Contact.CompanyId = CompanyId; Record.Contact.SecondaryEmail = SecondaryEmail
END
```

### Customers › Contact_Update  _(server)_
In: Source: Contact Record · Out: —
```text
CALL server GetContact(Source.Contact.Id)
CALL server UpdateContact(Source)
Description = "changed contact"
IF GetPrevious.Record.Contact.Name <>Source.Contact.Name   «<> Name»
  THEN:
    Description = Description + " Name: '" + GetPrevious.Record.Contact.Name + "' to '" + Source.Contact.Name + "'"
    IF GetPrevious.Record.Contact.JobTitle <>Source.Contact.JobTitle   «<> JobTitle»
      THEN:
        Description = Description + " Job Title: '" + GetPrevious.Record.Contact.JobTitle + "' to '" + Source.Contact.JobTitle + "'"
        IF GetPrevious.Record.Contact.Phone <>Source.Contact.Phone   «<> Phone»
          THEN:
            Description = Description + " Phone: " + GetPrevious.Record.Contact.Phone + " to " + Source.Contact.Phone
            IF GetPrevious.Record.Contact.Email <> Source.Contact.Email   «<> Email»
              THEN:
                Description = Description + " Email: '" +GetPrevious.Record.Contact.Email + "' to '" + Source.Contact.Email + "'"
                IF GetPrevious.Record.Contact.CompanyId <>Source.Contact.CompanyId   «<> Company»
                  THEN:
                    QUERY PreviousCompany from Company; where Company.Id = GetPrevious.Record.Contact.CompanyId
                    QUERY NewCompany from Company; where Company.Id = Source.Contact.CompanyId
                    Description = Description + " Stage: '" + PreviousCompany.List.Current.Company.CompanyName+ "' to '" + NewCompany.List.Current.Compan…
                    History.ContactHistory.ContactId = Source.Contact.Id; History.ContactHistory.Instant = CurrDateTime(); History.ContactHistory.Description = Description; History.ContactHistory.UserId = GetUserId()
                    CALL server CreateContactHistory(History)
                    END
                  ELSE:
                    ↪ continue at «ContactHistory»
              ELSE:
                ↪ continue at «<> Company»
          ELSE:
            ↪ continue at «<> Email»
      ELSE:
        ↪ continue at «<> Phone»
  ELSE:
    ↪ continue at «<> JobTitle»
```

### Customers › Company_Create_SA  _(service)_
In: Source: Company Record · Out: Id: Company Identifier
```text
CALL server Company_Create(Source)
Id = Company_Create.Id
END
```

### Customers › Company_CreateHistory_SA  _(service)_
In: CompanyId: Company Identifier, Description: Text · Out: —
```text
CALL server Company_CreateHistory(CompanyId, Description)
END
```

### Customers › Company_CreateOrUpdate_SA  _(service)_
In: Source: Company Record · Out: Id: Company Identifier
```text
CALL server Company_CreateOrUpdate(Source)
Id = Company_CreateOrUpdate.Id
END
```

### Customers › Company_Delete_SA  _(service)_
In: CompanyId: Company Identifier · Out: —
```text
QUERY GetCompanyById from Company; where Company.Id = CompanyId; max 1
IF GetCompanyById.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server Company_Delete(CompanyId)
    END
```

### Customers › Company_DeleteHistory_SA  _(service)_
In: CompanyHistoryId: CompanyHistory Identifier · Out: —
```text
QUERY GetCompanyHistoryById from CompanyHistory; where CompanyHistory.Id = CompanyHistoryId; max 1
IF GetCompanyHistoryById.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server Company_DeleteHistory(CompanyHistoryId)
    END
```

### Customers › Company_Update_SA  _(service)_
In: Source: Company Record · Out: —
```text
CALL server Company_Update(Source)
END
```

### Customers › Contact_Create_SA  _(service)_
In: Source: Contact Record · Out: Id: Contact Identifier
```text
CALL server Contact_Create(Source)
Id = Contact_Create.Id
END
```

### Customers › Contact_CreateHistory_SA  _(service)_
In: ContactId: Contact Identifier, Description: Text · Out: —
```text
CALL server Contact_CreateHistory(ContactId, Description)
END
```

### Customers › Contact_CreateOrUpdate_SA  _(service)_
In: Source: Contact Record · Out: Id: Contact Identifier
```text
CALL server Contact_CreateOrUpdate(Source)
Id = Contact_CreateOrUpdate.Id
END
```

### Customers › Contact_Delete_SA  _(service)_
In: ContactId: Contact Identifier · Out: —
```text
QUERY GetContactById from Contact; where Contact.Id = ContactId; max 1
IF GetContactById.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server Contact_Delete(ContactId)
    END
```

### Customers › Contact_DeleteHistory_SA  _(service)_
In: ContactHistoryId: ContactHistory Identifier · Out: —
```text
QUERY GetContactHistoryById from ContactHistory; where ContactHistory.Id = ContactHistoryId; max 1
IF GetContactHistoryById.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server Contact_DeleteHistory(ContactHistoryId)
    END
```

### Customers › Contact_Update_SA  _(service)_
In: Source: Contact Record · Out: —
```text
CALL server Contact_Update(Source)
END
```

### Customers › ContactPicture_CreateOrUpdate_SA  _(service)_
In: ContactId: Contact Identifier, Filename: Text, Content: Binary Data, Type: Text · Out: Record: ContactPicture Record
```text
CALL server Contact_CreateOrUpdatePicture(ContactId, Content, Filename, Type)
Record.ContactPicture = Contact_CreateOrUpdatePicture.Record.ContactPicture
END
```

### Employees › CheckOfficeLocationForValidLocation  _(server)_
In: OfficeLocationId: OfficeLocation Identifier · Out: Result: Boolean
```text
QUERY GetOfficeLocationWithLocationById from Location, OfficeLocation; where OfficeLocation.Id = OfficeLocationId; max 1
Result = not GetOfficeLocationWithLocationById.List.Empty and GetOfficeLocationWithLocationById.List.Current.Location.Id <> Null…
END
```

### Employees › CompanyName_Get  _(server)_
In: — · Out: CompanyName: Text
```text
CompanyName = Site.CompanyName
END
```

### Employees › CreateOfficeLocationWithEntities  _(server)_
In: OfficeLocation: OfficeLocation · Out: OfficeLocationId: OfficeLocation Identifier
```text
CALL server CreateNewLocation(OfficeLocation.Name)
OfficeLocation.LocationId = CreateNewLocation.LocationId
CALL server GetCurrentTimezoneInfoForAddress(OfficeLocation.Address, Site.ServerAPIKeyGoogleMaps, GetTimestampFromDateTime(CurrDateTime()))
OfficeLocation.TimezoneInfoId = GetCurrentTimezoneInfoForAddress.TimezoneInfoId
IF OfficeLocation.TimezoneInfoId = NullIdentifier()   «No timezone info?»
  THEN:
    CALL server LogMessage(EncodeHtml("Error obtaining timezon info for address " + Of…, "Employee Services")
    CALL server OfficeLocation_Create(OfficeLocation)
    OfficeLocationId = OfficeLocation_Create.OfficeLocationd
    END
  ELSE:
    ↪ continue at «OfficeLocation_Create»
```

### Employees › CreateOrUpdateEmployeeLocationFromAddress  _(server)_
In: Address: Text, EmployeeId: Employee Identifier · Out: EmployeeLocationId: EmployeeLocation Identifier
```text
QUERY GetEmployeeLocationForEmployeeId from EmployeeLocation; where EmployeeLocation.EmployeeId = EmployeeId
IF not GetEmployeeLocationForEmployeeId.List.Empty   «Is there an existing EmployeeLocation?»
  THEN:
    CALL server AddressToCoordinates(Address, Site.ServerAPIKeyGoogleMaps)
    CALL server GetCurrentTimezoneInfoForCoordinates(AddressToCoordinates.Latitude, AddressToCoordinates.Longitude, Site.ServerAPIKeyGoogleMaps, GetTimestampFromDateTime(CurrDateTime()))
    EmployeeLocation.EmployeeId = EmployeeId; EmployeeLocation.Address = Address; EmployeeLocation.RetrievedDateTime = CurrDateTime(); EmployeeLocation.Latitude = AddressToCoordinates.Latitude; EmployeeLocation.Longitude = AddressToCoordinates.Longitude; EmployeeLocation.TimezoneInfoId = GetCurrentTimezoneInfoForCoordinates.TimezoneInfoId
    CALL server EmployeeLocation_CreateOrUpdate(EmployeeLocation)
    EmployeeLocationId = EmployeeLocation_CreateOrUpdate.EmployeeLocationId
    END
  ELSE:
    EmployeeLocation = GetEmployeeLocationForEmployeeId.List.Current.EmployeeLocation
    ↪ continue at «AddressToCoordinates»
```

### Employees › Employee_Create  _(server)_
In: Source: Employee Record · Out: EmployeeId: Employee Identifier
```text
CALL server CreateEmployee(Source)
EmployeeId = CreateEmployee.Id
CALL server GrantEmployeeRole(CreateEmployee.Id)
CALL server Employees_MarkUpdate()
END
```

### Employees › Employee_CreateOrUpdate  _(server)_
In: Source: Employee Record · Out: EmployeeId: Employee Identifier
```text
QUERY GetEmployeeByUserId from Employee; where Employee.UserId = Source.Employee.UserId
Source.Employee.UpdatedOn = CurrDateTime()
IF GetEmployeeByUserId.List.Empty
  THEN:
    CALL server Employee_Create(Source)
    EmployeeId = Employee_Create.EmployeeId
    CALL server Employees_MarkUpdate()
    END
  ELSE:
    CALL server Employee_Update(Source)
    EmployeeId = Source.Employee.UserId
    ↪ continue at «Employees_MarkUpdate»
```

### Employees › Employee_Delete  _(server)_
In: EmployeeId: Employee Identifier · Out: —
```text
QUERY GetUserById from User; where User.Is_Active = True AND User.Id = EmployeeId
SQL ClearReportsToIds (see SQL below)
SQL ClearManagerIds (see SQL below)
CALL server DeleteEmployee(EmployeeId)
CALL server RevokeEmployeeRole(EmployeeId)
CALL server Employees_MarkUpdate()
END
```
SQL `ClearReportsToIds`:
```sql
update {Employee} set {Employee}.[ReportsToId]=null where {Employee}.[ReportsToId]=@EmployeeId
```
SQL `ClearManagerIds`:
```sql
update {OrganizationUnit} set {OrganizationUnit}.[ManagerId]=null 
where {OrganizationUnit}.[ManagerId]=@EmployeeId
```

### Employees › Employee_GetByPictureName  _(server)_
In: Filename: Text · Out: Employee: Employee Record
```text
QUERY GetEmployeeByPictureName from Employee, User; where User.Username = Substr(Filename,0,Length(Filename)-4)
Employee = GetEmployeeByPictureName.List.Current.Employee
END
```

### Employees › Employee_GetResponsible  _(server)_
In: EmployeeId: Employee Identifier · Out: ResponsibleId: Employee Identifier
```text
QUERY GetEmployee from Employee; where Employee.UserId = EmployeeId; max 1
IF GetEmployee.List.Current.Employee.ReportsToId<>NullIdentifier()   «Has responsible?»
  THEN:
    ResponsibleId = GetEmployee.List.Current.Employee.ReportsToId
    END
  ELSE:
    OrganizationUnitId = GetEmployee.List.Current.Employee.OrganizationUnitId
    QUERY GetOrganizationUnitById from OrganizationUnit; where OrganizationUnit.Id = OrganizationUnitId
    IF GetOrganizationUnitById.List.Current.OrganizationUnit.ManagerId<>NullIdentifier()   «Has Manager?»
      THEN:
        ResponsibleId = GetOrganizationUnitById.List.Current.OrganizationUnit.ManagerId
        END
      ELSE:
        IF GetOrganizationUnitById.List.Current.OrganizationUnit.ParentOrgUnitId<>NullIdentifier()   «Has Parent unit?»
          THEN:
            OrganizationUnitId = GetOrganizationUnitById.List.Current.OrganizationUnit.ParentOrgUnitId
            ↪ continue at «GetOrganizationUnitById»
          ELSE:
            END
```

### Employees › Employee_New  _(server)_
In: UserId: User Identifier, OrganizationUnitId: OrganizationUnit Identifier, ReportsToId: Employee Identifier, Birthday: Date, Gender: Gender Identifier, Address: Text, JobTitleId: JobTitle Identifier, OfficeLocationId: OfficeLocation Identifier, EmployeeCode: Text, Extension: Text, SkypeUsername: Text, IMUsername: Text, LinkedInUrl: Text, EntryDate: Date, CreatedOn: Date, CreatedBy: User Identifier, EndedOn: Date, EndedBy: User Identifier · Out: Record: Employee Record
```text
Record.Employee.UserId = UserId; Record.Employee.OrganizationUnitId = OrganizationUnitId; Record.Employee.ReportsToId = ReportsToId; Record.Employee.Birthday = Birthday; Record.Employee.Gender = Gender; Record.Employee.Address = Address; Record.Employee.JobTitleId = JobTitleId; Record.Employee.OfficeLocationId = OfficeLocationId; Record.Employee.EmployeeCode = EmployeeCode; Record.Employee.Extension = Extension; Record.Employee.SkypeUsername = SkypeUsername; Record.Employee.IMUsername = IMUsername; Record.Employee.LinkedInUrl = LinkedInUrl; Record.Employee.EntryDate = EntryDate; Record.Employee.CreatedOn = CreatedOn; Record.Employee.CreatedBy = CreatedBy; Record.Employee.EndedOn = EndedOn; Record.Employee.EndedBy = EndedBy
END
```

### Employees › Employee_Update  _(server)_
In: Source: Employee Record · Out: —
```text
Source.Employee.UpdatedOn = CurrDateTime()
CALL server UpdateEmployee(Source)
CALL server Employees_MarkUpdate()
END
```

### Employees › EmployeeCV_Create  _(server)_
In: Source: EmployeeCV · Out: EmployeeCVId: EmployeeCV Identifier
```text
CALL server CreateEmployeeCV(Source)
EmployeeCVId = CreateEmployeeCV.Id
END
```

### Employees › EmployeeCV_CreateOrUpdate  _(server)_
In: Source: EmployeeCV · Out: EmployeeCVId: EmployeeCV Identifier
```text
QUERY GetEmployeeCVById from EmployeeCV; where EmployeeCV.Id = Source.Id
IF GetEmployeeCVById.List.Empty   «Is there an EmployeeCV with this Id?»
  THEN:
    CALL server EmployeeCV_Create(Source)
    EmployeeCVId = EmployeeCV_Create.EmployeeCVId
    END
  ELSE:
    CALL server EmployeeCV_Update(Source)
    EmployeeCVId = Source.Id
    ↪ continue at «End»
```

### Employees › EmployeeCV_Delete  _(server)_
In: EmployeeCVId: EmployeeCV Identifier · Out: —
```text
QUERY GetEmployeeCVById from EmployeeCV; where EmployeeCV.Id = EmployeeCVId
CALL server DeleteEmployeeCV(EmployeeCVId)
END
```

### Employees › EmployeeCV_New  _(server)_
In: Filename: Text, File: Binary Data, Filetype: Text · Out: Record: EmployeeCV
```text
Record.Filename = Filename; Record.File = File; Record.Filetype = Filetype
END
```

### Employees › EmployeeCV_Update  _(server)_
In: Source: EmployeeCV · Out: —
```text
CALL server GetEmployeeCV(Source.Id)
CALL server UpdateEmployeeCV(Source)
END
```

### Employees › EmployeeLocation_Create  _(server)_
In: Source: EmployeeLocation · Out: EmployeeLocationId: EmployeeLocation Identifier
```text
CALL server CreateEmployeeLocation(Source)
EmployeeLocationId = CreateEmployeeLocation.Id
END
```

### Employees › EmployeeLocation_CreateOrUpdate  _(server)_
In: Source: EmployeeLocation · Out: EmployeeLocationId: EmployeeLocation Identifier
```text
QUERY GetEmployeeLocationById from EmployeeLocation; where EmployeeLocation.EmployeeId = Source.EmployeeId
IF GetEmployeeLocationById.List.Empty   «Is there an EmployeeLocation with this Id?»
  THEN:
    CALL server EmployeeLocation_Create(Source)
    EmployeeLocationId = EmployeeLocation_Create.EmployeeLocationId
    END
  ELSE:
    CALL server EmployeeLocation_Update(Source)
    EmployeeLocationId = Source.EmployeeId
    ↪ continue at «End»
```

### Employees › EmployeeLocation_Delete  _(server)_
In: EmployeeLocationId: EmployeeLocation Identifier · Out: —
```text
QUERY GetEmployeeLocationById from EmployeeLocation; where EmployeeLocation.EmployeeId = EmployeeLocationId
CALL server DeleteEmployeeLocation(EmployeeLocationId)
END
```

### Employees › EmployeeLocation_New  _(server)_
In: EmployeeId: Employee Identifier, Address: Text, Latitude: Text, Longitude: Text, RetrievedDateTime: TimezoneInfo Identifier, TimezoneInfoId: TimezoneInfo Identifier · Out: Record: EmployeeLocation
```text
Record.EmployeeId = EmployeeId; Record.Address = Address; Record.Latitude = Latitude; Record.Longitude = Longitude; Record.TimezoneInfoId = RetrievedDateTime; Record.TimezoneInfoId = TimezoneInfoId
END
```

### Employees › EmployeeLocation_Update  _(server)_
In: Source: EmployeeLocation · Out: —
```text
CALL server GetEmployeeLocation(Source.EmployeeId)
CALL server UpdateEmployeeLocation(Source)
END
```

### Employees › EmployeePicture_Create  _(server)_
In: Source: EmployeePicture · Out: EmployeePictureId: EmployeePicture Identifier
```text
Source.UpdatedOn = CurrDateTime()
CALL server CreateEmployeePicture(Source)
EmployeePictureId = CreateEmployeePicture.Id
CALL server EmployeePictures_MarkUpdate()
END
```

### Employees › EmployeePicture_CreateOrUpdate  _(server)_
In: Source: EmployeePicture · Out: EmployeePictureId: EmployeePicture Identifier
```text
QUERY GetEmployeePictureById from EmployeePicture; where EmployeePicture.EmployeeId = Source.EmployeeId
Source.UpdatedOn = CurrDateTime()
IF GetEmployeePictureById.List.Empty   «Is there an EmployeePicture with this Id?»
  THEN:
    CALL server EmployeePicture_Create(Source)
    EmployeePictureId = EmployeePicture_Create.EmployeePictureId
    CALL server EmployeePictures_MarkUpdate()
    END
  ELSE:
    CALL server EmployeePicture_Update(Source)
    EmployeePictureId = Source.EmployeeId
    ↪ continue at «EmployeePictures_MarkUpdate»
```

### Employees › EmployeePicture_Delete  _(server)_
In: EmployeePictureId: EmployeePicture Identifier · Out: —
```text
CALL server DeleteEmployeePicture(EmployeePictureId)
CALL server EmployeePictures_MarkUpdate()
END
```

### Employees › EmployeePicture_New  _(server)_
In: EmployeeId: EmployeePicture Identifier, Picture: Binary Data, Filename: Text, Filetype: Text · Out: Record: EmployeePicture
```text
Record.EmployeeId = EmployeeId; Record.Picture = Picture; Record.Filename = Filename; Record.Filetype = Filetype
END
```

### Employees › EmployeePicture_Update  _(server)_
In: Source: EmployeePicture · Out: —
```text
Source.UpdatedOn = CurrDateTime()
CALL server UpdateEmployeePicture(Source)
CALL server EmployeePictures_MarkUpdate()
END
```

### Employees › EmployeePictures_GetLastModifiedDateTime  _(server)_
In: — · Out: LastModifiedDateTime: Date Time
```text
LastModifiedDateTime = Site.EmployeePicturesLastModified
END
```

### Employees › EmployeePictures_MarkUpdate  _(server)_
```text
Site.EmployeePicturesLastModified = CurrDateTime()
END
```

### Employees › Employees_GetLastModifiedDateTime  _(server)_
In: — · Out: LastModifiedDateTime: Date Time
```text
LastModifiedDateTime = Site.EmployeesLastModified
END
```

### Employees › Employees_MarkUpdate  _(server)_
```text
Site.EmployeesLastModified = CurrDateTime()
END
```

### Employees › EmployeesAvailable  _(server)_
In: — · Out: IsAvailable: Boolean
```text
IsAvailable = True
END
```

### Employees › EmployeesVersion  _(server)_
In: — · Out: AppVersion: Text
```text
AppVersion = "1.6"
END
```

### Employees › EmployeeThumbnail_Create  _(server)_
In: Source: EmployeeThumbnail · Out: EmployeeThumbnaild: EmployeeThumbnail Identifier
```text
Source.UpdatedOn = CurrDateTime()
CALL server CreateEmployeeThumbnail(Source)
EmployeeThumbnaild = CreateEmployeeThumbnail.Id
CALL server EmployeeThumbnail_MarkUpdate()
END
```

### Employees › EmployeeThumbnail_CreateOrUpdate  _(server)_
In: Source: EmployeeThumbnail · Out: EmployeeThumbnailId: EmployeeThumbnail Identifier
```text
QUERY GetEmployeeThumbnailById from EmployeeThumbnail; where EmployeeThumbnail.EmployeeId = Source.EmployeeId
Source.UpdatedOn = CurrDateTime()
IF GetEmployeeThumbnailById.List.Empty   «Is there an EmployeeThumb with this Id?»
  THEN:
    CALL server EmployeeThumbnail_Create(Source)
    EmployeeThumbnailId = EmployeeThumbnail_Create.EmployeeThumbnaild
    CALL server EmployeeThumbnail_MarkUpdate()
    END
  ELSE:
    CALL server EmployeeThumbnail_Update(Source)
    EmployeeThumbnailId = Source.EmployeeId
    ↪ continue at «EmployeeThumbnail_MarkUpdate»
```

### Employees › EmployeeThumbnail_Delete  _(server)_
In: EmployeeThumbnailId: EmployeeThumbnail Identifier · Out: —
```text
CALL server DeleteEmployeeThumbnail(EmployeeThumbnailId)
CALL server EmployeeThumbnail_MarkUpdate()
END
```

### Employees › EmployeeThumbnail_GetLastModifiedDateTime  _(server)_
In: — · Out: LastModifiedDateTime: Date Time
```text
LastModifiedDateTime = Site.EmployeeThumbnailsLastModified
END
```

### Employees › EmployeeThumbnail_MarkUpdate  _(server)_
```text
Site.EmployeeThumbnailsLastModified = CurrDateTime()
END
```

### Employees › EmployeeThumbnail_New  _(server)_
In: EmployeeId: EmployeeThumbnail Identifier, Thumbnail: Binary Data, Filename: Text, Filetype: Text · Out: Record: EmployeeThumbnail
```text
Record.EmployeeId = EmployeeId; Record.Thumbnail = Thumbnail; Record.Filename = Filename; Record.Filetype = Filetype
END
```

### Employees › EmployeeThumbnail_Update  _(server)_
In: Source: EmployeeThumbnail · Out: —
```text
Source.UpdatedOn = CurrDateTime()
CALL server UpdateEmployeeThumbnail(Source)
CALL server EmployeeThumbnail_MarkUpdate()
END
```

### Employees › EmployeeUser_CreateOrUpdate  _(server)_
In: EmployeeUser: EmployeeUser · Out: EmployeeId: Employee Identifier
```text
QUERY GetUserByEmail from User; where User.Is_Active = True AND User.Email = EmployeeUser.Email; max 1
GetUserByEmail.List.Current.User.Name = EmployeeUser.Name; GetUserByEmail.List.Current.User.Username = EmployeeUser.Username; GetUserByEmail.List.Current.User.Email = EmployeeUser.Email; GetUserByEmail.List.Current.User.Is_Active = True; GetUserByEmail.List.Current.User.MobilePhone = EmployeeUser.TelephoneNumber
IF GetUserByEmail.List.Empty   «New user?»
  THEN:
    GetUserByEmail.List.Current.User.Password = EncryptPassword(GetUserByEmail.List.Current.User.Username,"outsystems"); GetUserByEmail.List.Current.User.Creation_Date = CurrDateTime()
    CALL server CreateOrUpdateUser(GetUserByEmail.List.Current)
    GetUserByEmail.List.Current.User.Id = CreateOrUpdateUser.Id; Employee.UserId = CreateOrUpdateUser.Id
    QUERY GetOrganizationUnitsByName from OrganizationUnit; where OrganizationUnit.Name = EmployeeUser.DepartmentOrUnit; max 1
    Employee.OrganizationUnitId = GetOrganizationUnitsByName.List.Current.OrganizationUnit.Id
    CALL server Employee_CreateOrUpdate(Employee)
    EmployeeId = Employee_CreateOrUpdate.EmployeeId
    END
  ELSE:
    ↪ continue at «CreateOrUpdateUser»
```

### Employees › JobTitle_Create  _(server)_
In: Source: JobTitle · Out: JobTitleId: JobTitle Identifier
```text
Source.UpdatedOn = CurrDateTime()
CALL server CreateJobTitle(Source)
JobTitleId = CreateJobTitle.Id
END
```

### Employees › JobTitle_CreateOrUpdate  _(server)_
In: Source: JobTitle · Out: JobTitleId: JobTitle Identifier
```text
QUERY GetJobTitleById from JobTitle; where JobTitle.Id = Source.Id
Source.UpdatedOn = CurrDateTime()
IF GetJobTitleById.List.Empty   «Is there a JobTitle with this Id?»
  THEN:
    CALL server JobTitle_Create(Source)
    JobTitleId = JobTitle_Create.JobTitleId
    END
  ELSE:
    CALL server JobTitle_Update(Source)
    JobTitleId = Source.Id
    ↪ continue at «End»
```

### Employees › JobTitle_Delete  _(server)_
In: JobTitleId: JobTitle Identifier · Out: —
```text
CALL server DeleteJobTitle(JobTitleId)
END
```

### Employees › JobTitle_New  _(server)_
In: Name: Text · Out: Record: JobTitle
```text
Record.Name = Name
END
```

### Employees › JobTitle_Update  _(server)_
In: Source: JobTitle · Out: —
```text
CALL server GetJobTitle(Source.Id)
Source.UpdatedOn = CurrDateTime()
CALL server UpdateJobTitle(Source)
END
```

### Employees › OfficeLocation_Create  _(server)_
In: Source: OfficeLocation · Out: OfficeLocationd: OfficeLocation Identifier
```text
Source.UpdatedOn = CurrDateTime()
CALL server CreateOfficeLocation(Source)
OfficeLocationd = CreateOfficeLocation.Id
END
```

### Employees › OfficeLocation_CreateOrUpdate  _(server)_
In: Source: OfficeLocation · Out: OfficeLocationId: OfficeLocation Identifier
```text
QUERY GetNewOfficeLocationById from OfficeLocation; where OfficeLocation.Id = Source.Id
Source.UpdatedOn = CurrDateTime()
IF GetNewOfficeLocationById.List.Empty   «Is there an OfficeLocation with this Id?»
  THEN:
    CALL server OfficeLocation_Create(Source)
    OfficeLocationId = OfficeLocation_Create.OfficeLocationd
    END
  ELSE:
    CALL server OfficeLocation_Update(Source)
    OfficeLocationId = Source.Id
    ↪ continue at «End»
```

### Employees › OfficeLocation_Delete  _(server)_
In: OfficeLocationId: OfficeLocation Identifier · Out: —
```text
CALL server DeleteOfficeLocation(OfficeLocationId)
END
```

### Employees › OfficeLocation_New  _(server)_
In: Name: Text, Address: Text, PhoneNumber: Phone Number, CountryStateId: CountryState Identifier, TimezoneInfoId: TimezoneInfo Identifier, LocationId: Location Identifier · Out: Record: OfficeLocation
```text
Record.Name = Name; Record.Address = Address; Record.PhoneNumber = PhoneNumber; Record.CountryStateId = CountryStateId; Record.TimezoneInfoId = TimezoneInfoId; Record.LocationId = LocationId
END
```

### Employees › OfficeLocation_Update  _(server)_
In: Source: OfficeLocation · Out: —
```text
CALL server GetOfficeLocation(Source.Id)
Source.UpdatedOn = CurrDateTime()
CALL server UpdateOfficeLocation(Source)
END
```

### Employees › OrganizationUnit_Create  _(server)_
In: OrganizationUnit: OrganizationUnit Record · Out: OrganizationId: OrganizationUnit Identifier
```text
OrganizationUnit.OrganizationUnit.UpdatedOn = CurrDateTime()
IF OrganizationUnit.OrganizationUnit.ParentOrgUnitId = NullIdentifier()   «No Parent?»
  THEN:
    OrganizationUnit.OrganizationUnit.Level = 1; OrganizationUnit.OrganizationUnit.HierarchicalName = OrganizationUnit.OrganizationUnit.Name
    CALL server CreateOrganizationUnit(OrganizationUnit)
    OrganizationId = CreateOrganizationUnit.Id
    END
  ELSE:
    GetParentOrganizationUnit_ParentOrgUnitId = OrganizationUnit.OrganizationUnit.ParentOrgUnitId
    QUERY GetParentOrganizationUnit from OrganizationUnit; where OrganizationUnit.Id = GetParentOrganizationUnit_ParentOrgUnitId
    OrganizationUnit.OrganizationUnit.Level = GetParentOrganizationUnit.List.Current.OrganizationUnit.Level+1; OrganizationUnit.OrganizationUnit.HierarchicalName = GetParentOrganizationUnit.List.Current.OrganizationUnit.HierarchicalName + Site.OrganizationUnit_HierarchicalNameSepara…
    ↪ continue at «CreateOrganizationUnit»
```

### Employees › OrganizationUnit_CreateOrUpdate  _(server)_
In: OrganizationUnit: OrganizationUnit Record · Out: OrganizationId: OrganizationUnit Identifier
```text
IF OrganizationUnit.OrganizationUnit.Id=NullIdentifier()   «New?»
  THEN:
    CALL server OrganizationUnit_Create(OrganizationUnit)
    OrganizationId = OrganizationUnit_Create.OrganizationId
    END
  ELSE:
    CALL server OrganizationUnit_Update(OrganizationUnit)
    OrganizationId = OrganizationUnit_Update.OrganizationId
    ↪ continue at «End»
```

### Employees › OrganizationUnit_Delete  _(server)_
In: OrganizationUnitId: OrganizationUnit Identifier · Out: —
```text
CALL server GetOrganizationUnit(OrganizationUnitId)
SQL ClearChildReferences (see SQL below)
CALL server DeleteOrganizationUnit(OrganizationUnitId)
END
```
SQL `ClearChildReferences`:
```sql
update {OrganizationUnit} set {OrganizationUnit}.[ParentOrgUnitId]=null 
where {OrganizationUnit}.[ParentOrgUnitId]=@OrganizationUnitId
```

### Employees › OrganizationUnit_New  _(server)_
In: OrganizationTypeId: OrganizationUnitType Identifier, Name: Text, ManagerId: User Identifier, ParentOrgUnitId: OrganizationUnit Identifier · Out: Record: OrganizationUnit Record
```text
Record.OrganizationUnit.Type = OrganizationTypeId; Record.OrganizationUnit.Name = Name; Record.OrganizationUnit.ManagerId = ManagerId; Record.OrganizationUnit.ParentOrgUnitId = ParentOrgUnitId
IF ParentOrgUnitId <> NullIdentifier()   «Has Parent?»
  THEN:
    QUERY GetParentOrganizationUnit from OrganizationUnit; where OrganizationUnit.Id = ParentOrgUnitId
    Record.OrganizationUnit.Level = GetParentOrganizationUnit.List.Current.OrganizationUnit.Level+1; Record.OrganizationUnit.HierarchicalName = GetParentOrganizationUnit.List.Current.OrganizationUnit.HierarchicalName+"\"+Name
    END
  ELSE:
    Record.OrganizationUnit.Level = 1; Record.OrganizationUnit.HierarchicalName = Name
    ↪ continue at «End»
```

### Employees › OrganizationUnit_Update  _(server)_
In: Source: OrganizationUnit Record · Out: OrganizationId: OrganizationUnit Identifier
```text
CALL server GetOrganizationUnit(Source.OrganizationUnit.Id)
Source.OrganizationUnit.UpdatedOn = CurrDateTime()
IF GetPrevious.Record.OrganizationUnit.ParentOrgUnitId <> Source.OrganizationUnit.ParentOrgUnitId   «<> ParentOrgUnit»
  THEN:
    QUERY GetPreviousOrganizationUnitById from OrganizationUnit; where OrganizationUnit.Id = GetPrevious.Record.OrganizationUnit.ParentOrgUnitId
    QUERY GetNewOrganizationUnitById from OrganizationUnit; where OrganizationUnit.Id = Source.OrganizationUnit.ParentOrgUnitId
    ChangeHistory = ChangeHistory + " Parent Organization Unit: '" + GetPreviousOrganizationUnitById.List.Current.OrganizationUnit.Hierarch…
    IF GetPrevious.Record.OrganizationUnit.Name <> Source.OrganizationUnit.Name   «<> Name»
      THEN:
        ChangeHistory = ChangeHistory + " Name: '" + GetPrevious.Record.OrganizationUnit.Name + "' to '" + Source.OrganizationUnit.Name + "'"
        IF GetPrevious.Record.OrganizationUnit.Type <> Source.OrganizationUnit.Type   «<> Type»
          THEN:
            ChangeHistory = ChangeHistory + " Type: '" + GetPrevious.Record.OrganizationUnit.Type + "' to '" + Source.OrganizationUnit.Type + "'"
            IF GetPrevious.Record.OrganizationUnit.ManagerId <> Source.OrganizationUnit.ManagerId   «<> Manager»
              THEN:
                QUERY GetPreviousManagerUser from User; where User.Id = GetPrevious.Record.OrganizationUnit.ManagerId
                QUERY GetNewManagerUser from User; where User.Id = Source.OrganizationUnit.ManagerId
                ChangeHistory = ChangeHistory + " Manager: '" + GetPreviousManagerUser.List.Current.User.Name + "' to '" + GetNewManagerUser.List.Curre…
                IF Source.OrganizationUnit.ParentOrgUnitId <> NullIdentifier()   «No Parent?»
                  THEN:
                    QUERY GetParentOrganizationUnit from OrganizationUnit; where OrganizationUnit.Id = Source.OrganizationUnit.ParentOrgUnitId
                    Source.OrganizationUnit.Level = GetParentOrganizationUnit.List.Current.OrganizationUnit.Level+1; Source.OrganizationUnit.HierarchicalName = GetParentOrganizationUnit.List.Current.OrganizationUnit.HierarchicalName + Site.OrganizationUnit_HierarchicalNameSepara…
                    CALL server UpdateOrganizationUnit(Source)
                    OrganizationId = Source.OrganizationUnit.Id
                    END
                  ELSE:
                    Source.OrganizationUnit.Level = 1; Source.OrganizationUnit.HierarchicalName = Source.OrganizationUnit.Name
                    ↪ continue at «UpdateOrganizationUnit»
              ELSE:
                ↪ continue at «No Parent?»
          ELSE:
            ↪ continue at «<> Manager»
      ELSE:
        ↪ continue at «<> Type»
  ELSE:
    ↪ continue at «<> Name»
```

### Employees › Project_Create  _(server)_
In: Source: Project Record · Out: ProjectId: Project Identifier
```text
CALL server CreateProject(Source)
ProjectId = CreateProject.Id
END
```

### Employees › Project_CreateOrUpdate  _(server)_
In: Source: Project Record · Out: ProjectId: Project Identifier
```text
IF Source.Project.Id = NullIdentifier()
  THEN:
    CALL server Project_Create(Source)
    ProjectId = Project_Create.ProjectId
    END
  ELSE:
    CALL server Project_Update(Source)
    ProjectId = Source.Project.Id
    ↪ continue at «End»
```

### Employees › Project_Delete  _(server)_
In: ProjectId: Project Identifier · Out: —
```text
CALL server GetProject(ProjectId)
CALL server DeleteProject(ProjectId)
END
```

### Employees › Project_New  _(server)_
In: Name: Text, Code: Text, OrganizationUnitId: OrganizationUnit Identifier, StartDate: Date, EndDate: Date, Description: Text, ResponsibleId: Employee Identifier, EstimatedDays: Decimal · Out: Record: Project Record
```text
Record.Project.Name = Name; Record.Project.Code = Code; Record.Project.OrganizationUnitId = OrganizationUnitId; Record.Project.StartDate = StartDate; Record.Project.EndDate = EndDate; Record.Project.Description = Description; Record.Project.ResponsibleId = ResponsibleId; Record.Project.IsClosed = False; Record.Project.IsActive = True; Record.Project.EstimateDays = EstimatedDays
END
```

### Employees › Project_Update  _(server)_
In: Source: Project Record · Out: —
```text
CALL server GetProject(Source.Project.Id)
ChangeHistory = "changed project"
IF GetPrevious.Record.Project.Name <> Source.Project.Name   «<> Name»
  THEN:
    ChangeHistory = ChangeHistory + " Name: '" + GetPrevious.Record.Project.Name + "' to '" + Source.Project.Name + "'"
    IF GetPrevious.Record.Project.Code <> Source.Project.Code   «<> Code»
      THEN:
        ChangeHistory = ChangeHistory + " Code: '" + GetPrevious.Record.Project.Code + "' to '" + Source.Project.Code + "'"
        IF GetPrevious.Record.Project.OrganizationUnitId <> Source.Project.OrganizationUnitId   «<> OrgUnit»
          THEN:
            QUERY GetPreviousOrganizationUnitById from OrganizationUnit; where OrganizationUnit.Id = GetPrevious.Record.Project.OrganizationUnitId
            QUERY GetNewOrganizationUnitById from OrganizationUnit; where OrganizationUnit.Id = Source.Project.OrganizationUnitId
            ChangeHistory = ChangeHistory + " OrganizationUnit: '" + GetPreviousOrganizationUnitById.List.Current.OrganizationUnit.HierarchicalName…
            IF GetPrevious.Record.Project.StartDate <> Source.Project.StartDate   «<> StartDate»
              THEN:
                ChangeHistory = ChangeHistory + " StartDate: '" + GetPrevious.Record.Project.StartDate + "' to '" + Source.Project.StartDate + "'"
                IF GetPrevious.Record.Project.EndDate <> Source.Project.EndDate   «<> EndDate»
                  THEN:
                    ChangeHistory = ChangeHistory + " EndDate: '" + GetPrevious.Record.Project.EndDate + "' to '" + Source.Project.EndDate + "'"
                    IF GetPrevious.Record.Project.Description <> Source.Project.Description   «<> Description»
                      THEN:
                        ChangeHistory = ChangeHistory + " Description: '" + GetPrevious.Record.Project.Description + "' to '" + Source.Project.Description + "'"
                        IF GetPrevious.Record.Project.ResponsibleId <> Source.Project.ResponsibleId   «<> Responsible»
                          THEN:
                            QUERY GetPreviousResponsible from User; where User.Id = GetPrevious.Record.Project.ResponsibleId
                            QUERY GetNewResponsible from User; where User.Id = GetPrevious.Record.Project.ResponsibleId
                            ChangeHistory = ChangeHistory + " Name: '" + GetPreviousResponsible.List.Current.User.Name + "' to '" + GetNewResponsible.List.Current.…
                            IF GetPrevious.Record.Project.EstimateDays <> Source.Project.EstimateDays   «<>Estimated Days»
                              THEN:
                                ChangeHistory = ChangeHistory + " EstimateDays: '" + GetPrevious.Record.Project.EstimateDays + "' to '" + Source.Project.EstimateDays +…
                                IF GetPrevious.Record.Project.IsClosed <> Source.Project.IsClosed   «<>IsClosed»
                                  THEN:
                                    ChangeHistory = ChangeHistory + " IsClosed: '" + GetPrevious.Record.Project.IsClosed + "' to '" + Source.Project.IsClosed + "'"
                                    IF GetPrevious.Record.Project.IsActive <> Source.Project.IsActive   «<>IsActive»
                                      THEN:
                                        ChangeHistory = ChangeHistory + " IsActive: '" + GetPrevious.Record.Project.IsActive + "' to '" + Source.Project.IsActive + "'"
                                        CALL server UpdateProject(Source)
                                        END
                                      ELSE:
                                        ↪ continue at «UpdateProject»
                                  ELSE:
                                    ↪ continue at «<>IsActive»
                              ELSE:
                                ↪ continue at «<>IsClosed»
                          ELSE:
                            ↪ continue at «<>Estimated Days»
                      ELSE:
                        ↪ continue at «<> Responsible»
                  ELSE:
                    ↪ continue at «<> Description»
              ELSE:
                ↪ continue at «<> EndDate»
          ELSE:
            ↪ continue at «<> StartDate»
      ELSE:
        ↪ continue at «<> OrgUnit»
  ELSE:
    ↪ continue at «<> Code»
```

### Employees › UpdateOfficeLocationWithEntities  _(server)_
In: OfficeLocation: OfficeLocation · Out: —
```text
QUERY GetCurrentOfficeLocation from OfficeLocation; where OfficeLocation.Id = OfficeLocation.Id; max 1
IF OfficeLocation.Name <>GetCurrentOfficeLocation.List.Current.OfficeLocation.Name   «Was the name changed?»
  THEN:
    CALL server CreateNewLocation(OfficeLocation.Name)
    OfficeLocation.LocationId = CreateNewLocation.LocationId
    IF GetCurrentOfficeLocation.List.Current.OfficeLocation.Address <> OfficeLocation.Address   «Has the Address changed?»
      THEN:
        CALL server GetCurrentTimezoneInfoForAddress(OfficeLocation.Address, Site.ServerAPIKeyGoogleMaps, GetTimestampFromDateTime(CurrDateTime()))
        OfficeLocation.TimezoneInfoId = GetCurrentTimezoneInfoForAddress.TimezoneInfoId
        IF OfficeLocation.TimezoneInfoId = NullIdentifier()   «No timezone info?»
          THEN:
            CALL server Feedback_Message(EncodeHtml("Could not obtain timezone information for the s…, Entities.MessageType.Error)
            CALL server OfficeLocation_CreateOrUpdate(OfficeLocation)
            END
          ELSE:
            ↪ continue at «CreateOrUpdateOfficeLocation»
      ELSE:
        ↪ continue at «CreateOrUpdateOfficeLocation»
  ELSE:
    ↪ continue at «Has the Address changed?»
```

### Employees › CheckOfficeLocationForValidLocation_SA  _(service)_
In: OfficeLocationId: OfficeLocation Identifier · Out: Result: Boolean
```text
CALL server CheckOfficeLocationForValidLocation(OfficeLocationId)
Result = CheckOfficeLocationForValidLocation.Result
END
```

### Employees › CompanyName_Get_SA  _(service)_
In: — · Out: CompanyName: Text
```text
CALL server CompanyName_Get()
CompanyName = CompanyName_Get.CompanyName
END
```

### Employees › CreateOfficeLocationWithEntities_SA  _(service)_
In: OfficeLocation: OfficeLocation · Out: OfficeLocationId: OfficeLocation Identifier
```text
CALL server CreateOfficeLocationWithEntities(OfficeLocation)
OfficeLocationId = CreateOfficeLocationWithEntities.OfficeLocationId
END
```

### Employees › CreateOrUpdateEmployeeLocationFromAddress_SA  _(service)_
In: Address: Text, EmployeeId: Employee Identifier, APIKey: Text · Out: EmployeeLocationId: EmployeeLocation Identifier
```text
CALL server CreateOrUpdateEmployeeLocationFromAddress(Address, APIKey, EmployeeId)
EmployeeLocationId = CreateOrUpdateEmployeeLocationFromAddress.EmployeeLocationId
END
```

### Employees › Employee_Create_SA  _(service)_
In: Source: Employee Record · Out: EmployeeId: Employee Identifier
```text
CALL server Employee_Create(Source)
EmployeeId = Employee_Create.EmployeeId
END
```

### Employees › Employee_CreateOrUpdate_SA  _(service)_
In: Source: Employee Record · Out: EmployeeId: Employee Identifier
```text
CALL server Employee_CreateOrUpdate(Source)
EmployeeId = Employee_CreateOrUpdate.EmployeeId
END
```

### Employees › Employee_Delete_SA  _(service)_
In: EmployeeId: Employee Identifier · Out: —
```text
QUERY GetEmployee from Employee; where Employee.UserId = EmployeeId; max 1
IF GetEmployee.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server Employee_Delete(EmployeeId)
    END
```

### Employees › Employee_GetByPictureName_SA  _(service)_
In: Filename: Text · Out: Employee: Employee Record
```text
CALL server Employee_GetByPictureName(Filename)
Employee = Employee_GetByPictureName.Employee
END
```

### Employees › Employee_GetPicture_SA  _(service)_
In: EmployeeId: Employee Identifier · Out: FileBinary: Binary Data, Filename: Text
```text
QUERY GetEmployeePictureByEmployeeId from EmployeePicture; where EmployeePicture.EmployeeId = EmployeeId; max 1
FileBinary = GetEmployeePictureByEmployeeId.List.Current.EmployeePicture.Picture; Filename = GetEmployeePictureByEmployeeId.List.Current.EmployeePicture.Filename
END
```

### Employees › Employee_GetResponsible_SA  _(service)_
In: EmployeeId: Employee Identifier · Out: ResponsibleId: Employee Identifier
```text
CALL server Employee_GetResponsible(EmployeeId)
ResponsibleId = Employee_GetResponsible.ResponsibleId
END
```

### Employees › Employee_New_SA  _(service)_
In: UserId: User Identifier, OrganizationUnitId: OrganizationUnit Identifier, ReportsToId: Employee Identifier, Birthday: Date, Gender: Gender Identifier, Address: Text, JobTitleId: JobTitle Identifier, OfficeLocationId: OfficeLocation Identifier, EmployeeCode: Text, Extension: Text, SkypeUsername: Text, IMUsername: Text, LinkedInUrl: Text, EntryDate: Date, CreatedOn: Date, CreatedBy: User Identifier, EndedOn: Date, EndedBy: User Identifier · Out: Record: Employee Record
```text
CALL server Employee_New(Address, Birthday, CreatedBy, CreatedOn, EmployeeCode, EndedBy, EndedOn, EntryDate, Extension, Gender, IMUsername, JobTitleId, LinkedInUrl, OfficeLocationId, OrganizationUnitId, ReportsToId, SkypeUsername, UserId)
Record = Employee_New.Record
END
```

### Employees › Employee_Update_SA  _(service)_
In: Source: Employee Record · Out: —
```text
CALL server Employee_Update(Source)
END
```

### Employees › EmployeeCV_Create_SA  _(service)_
In: Source: EmployeeCV · Out: EmployeeCVId: EmployeeCV Identifier
```text
CALL server EmployeeCV_Create(Source)
EmployeeCVId = EmployeeCV_Create.EmployeeCVId
END
```

### Employees › EmployeeCV_CreateOrUpdate_SA  _(service)_
In: Source: EmployeeCV · Out: EmployeeCVId: EmployeeCV Identifier
```text
CALL server EmployeeCV_CreateOrUpdate(Source)
EmployeeCVId = EmployeeCV_CreateOrUpdate.EmployeeCVId
END
```

### Employees › EmployeeCV_Delete_SA  _(service)_
In: EmployeeCVId: EmployeeCV Identifier · Out: —
```text
QUERY GetEmployeeCVById from EmployeeCV; where EmployeeCV.Id = EmployeeCVId; max 1
IF GetEmployeeCVById.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server EmployeeCV_Delete(EmployeeCVId)
    END
```

### Employees › EmployeeCV_New_SA  _(service)_
In: Filename: Text, File: Binary Data, Filetype: Text · Out: Record: EmployeeCV
```text
CALL server EmployeeCV_New(File, Filename, Filetype)
Record = EmployeeCV_New.Record
END
```

### Employees › EmployeeCV_Update_SA  _(service)_
In: Source: EmployeeCV · Out: —
```text
CALL server EmployeeCV_Update(Source)
END
```

### Employees › EmployeeLocation_Create_SA  _(service)_
In: Source: EmployeeLocation · Out: EmployeeLocationId: EmployeeLocation Identifier
```text
CALL server EmployeeLocation_Create(Source)
EmployeeLocationId = EmployeeLocation_Create.EmployeeLocationId
END
```

### Employees › EmployeeLocation_CreateOrUpdate_SA  _(service)_
In: Source: EmployeeLocation · Out: EmployeeLocationId: EmployeeLocation Identifier
```text
CALL server EmployeeLocation_CreateOrUpdate(Source)
EmployeeLocationId = EmployeeLocation_CreateOrUpdate.EmployeeLocationId
END
```

### Employees › EmployeeLocation_Delete_SA  _(service)_
In: EmployeeLocationId: EmployeeLocation Identifier · Out: —
```text
QUERY GetEmployeeLocationByEmployeeId from EmployeeLocation; where EmployeeLocation.EmployeeId = EmployeeLocationId; max 1
IF GetEmployeeLocationByEmployeeId.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server EmployeeLocation_Delete(EmployeeLocationId)
    END
```

### Employees › EmployeeLocation_New_SA  _(service)_
In: EmployeeId: Employee Identifier, Address: Text, Latitude: Text, Longitude: Text, RetrievedDateTime: TimezoneInfo Identifier, TimezoneInfoId: TimezoneInfo Identifier · Out: Record: EmployeeLocation
```text
CALL server EmployeeLocation_New(Address, EmployeeId, Latitude, Longitude, RetrievedDateTime, TimezoneInfoId)
Record = EmployeeLocation_New.Record
END
```

### Employees › EmployeeLocation_Update_SA  _(service)_
In: Source: EmployeeLocation · Out: —
```text
CALL server EmployeeLocation_Update(Source)
END
```

### Employees › EmployeePicture_Create_SA  _(service)_
In: Source: EmployeePicture · Out: EmployeePictureId: EmployeePicture Identifier
```text
CALL server EmployeePicture_Create(Source)
EmployeePictureId = EmployeePicture_Create.EmployeePictureId
END
```

### Employees › EmployeePicture_CreateOrUpdate_SA  _(service)_
In: Source: EmployeePicture · Out: EmployeePictureId: EmployeePicture Identifier
```text
CALL server EmployeePicture_CreateOrUpdate(Source)
EmployeePictureId = EmployeePicture_CreateOrUpdate.EmployeePictureId
END
```

### Employees › EmployeePicture_Delete_SA  _(service)_
In: EmployeePictureId: EmployeePicture Identifier · Out: —
```text
QUERY GetEmployeePictureByEmployeeId from EmployeePicture; where EmployeePicture.EmployeeId = EmployeePictureId
IF GetEmployeePictureByEmployeeId.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server EmployeePicture_Delete(EmployeePictureId)
    END
```

### Employees › EmployeePicture_New_SA  _(service)_
In: EmployeeId: EmployeePicture Identifier, Picture: Binary Data, Filename: Text, Filetype: Text · Out: Record: EmployeePicture
```text
CALL server EmployeePicture_New(EmployeeId, Filename, Filetype, Picture)
Record = EmployeePicture_New.Record
END
```

### Employees › EmployeePicture_Update_SA  _(service)_
In: Source: EmployeePicture · Out: —
```text
CALL server EmployeePicture_Update(Source)
END
```

### Employees › EmployeePictures_GetLastModifiedDateTime_SA  _(service)_
In: — · Out: LastModifiedDateTime: Date Time
```text
CALL server EmployeePictures_GetLastModifiedDateTime()
LastModifiedDateTime = EmployeePictures_GetLastModifiedDateTime.LastModifiedDateTime
END
```

### Employees › Employees_GetLastModifiedDateTime_SA  _(service)_
In: — · Out: LastModifiedDateTime: Date Time
```text
CALL server Employees_GetLastModifiedDateTime()
LastModifiedDateTime = Employees_GetLastModifiedDateTime.LastModifiedDateTime
END
```

### Employees › EmployeeUser_CreateOrUpdate_SA  _(service)_
In: EmployeeUser: EmployeeUser · Out: EmployeeId: Employee Identifier
```text
CALL server EmployeeUser_CreateOrUpdate(EmployeeUser)
EmployeeId = EmployeeUser_CreateOrUpdate.EmployeeId
END
```

### Employees › JobTitle_Create_SA  _(service)_
In: Source: JobTitle · Out: JobTitleId: JobTitle Identifier
```text
CALL server JobTitle_Create(Source)
JobTitleId = JobTitle_Create.JobTitleId
END
```

### Employees › JobTitle_CreateOrUpdate_SA  _(service)_
In: Source: JobTitle · Out: JobTitleId: JobTitle Identifier
```text
CALL server JobTitle_CreateOrUpdate(Source)
JobTitleId = JobTitle_CreateOrUpdate.JobTitleId
END
```

### Employees › JobTitle_Delete_SA  _(service)_
In: JobTitleId: JobTitle Identifier · Out: —
```text
QUERY GetJobTitleById from JobTitle; where JobTitle.Id = JobTitleId; max 1
IF GetJobTitleById.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server JobTitle_Delete(JobTitleId)
    END
```

### Employees › JobTitle_New_SA  _(service)_
In: Name: Text · Out: Record: JobTitle
```text
CALL server JobTitle_New(Name)
Record.Name = JobTitle_New.Record.Name
END
```

### Employees › JobTitle_Update_SA  _(service)_
In: Source: JobTitle · Out: —
```text
CALL server JobTitle_Update(Source)
END
```

### Employees › OfficeLocation_Create_SA  _(service)_
In: Source: OfficeLocation · Out: OfficeLocationd: OfficeLocation Identifier
```text
CALL server OfficeLocation_Create(Source)
OfficeLocationd = OfficeLocation_Create.OfficeLocationd
END
```

### Employees › OfficeLocation_CreateOrUpdate_SA  _(service)_
In: Source: OfficeLocation · Out: OfficeLocationId: OfficeLocation Identifier
```text
CALL server OfficeLocation_CreateOrUpdate(Source)
OfficeLocationId = OfficeLocation_CreateOrUpdate.OfficeLocationId
END
```

### Employees › OfficeLocation_Delete_SA  _(service)_
In: OfficeLocationId: OfficeLocation Identifier · Out: —
```text
QUERY GetOfficeLocationById from OfficeLocation; where OfficeLocation.Id = OfficeLocationId; max 1
IF GetOfficeLocationById.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server OfficeLocation_Delete(OfficeLocationId)
    END
```

### Employees › OfficeLocation_New_SA  _(service)_
In: Name: Text, Address: Text, PhoneNumber: Phone Number, CountryStateId: CountryState Identifier, TimezoneInfoId: TimezoneInfo Identifier, LocationId: Location Identifier · Out: Record: OfficeLocation
```text
CALL server OfficeLocation_New(Address, CountryStateId, LocationId, Name, PhoneNumber, TimezoneInfoId)
Record = OfficeLocation_New.Record
END
```

### Employees › OfficeLocation_Update_SA  _(service)_
In: Source: OfficeLocation · Out: —
```text
CALL server OfficeLocation_Update(Source)
END
```

### Employees › OrganizationUnit_Create_SA  _(service)_
In: OrganizationUnit: OrganizationUnit Record · Out: OrganizationId: OrganizationUnit Identifier
```text
CALL server OrganizationUnit_Create(OrganizationUnit)
OrganizationId = OrganizationUnit_Create.OrganizationId
END
```

### Employees › OrganizationUnit_CreateOrUpdate_SA  _(service)_
In: OrganizationUnit: OrganizationUnit Record · Out: OrganizationId: OrganizationUnit Identifier
```text
CALL server OrganizationUnit_CreateOrUpdate(OrganizationUnit)
OrganizationId = OrganizationUnit_CreateOrUpdate.OrganizationId
END
```

### Employees › OrganizationUnit_Delete_SA  _(service)_
In: OrganizationUnitId: OrganizationUnit Identifier · Out: —
```text
QUERY GetOrganizationUnitById from OrganizationUnit; where OrganizationUnit.Id = OrganizationUnitId; max 1
IF GetOrganizationUnitById.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server OrganizationUnit_Delete(OrganizationUnitId)
    END
```

### Employees › OrganizationUnit_New_SA  _(service)_
In: OrganizationTypeId: OrganizationUnitType Identifier, Name: Text, ManagerId: User Identifier, ParentOrgUnitId: OrganizationUnit Identifier · Out: Record: OrganizationUnit Record
```text
CALL server OrganizationUnit_New(ManagerId, Name, OrganizationTypeId, ParentOrgUnitId)
Record = OrganizationUnit_New.Record
END
```

### Employees › OrganizationUnit_Update_SA  _(service)_
In: Source: OrganizationUnit Record · Out: OrganizationId: OrganizationUnit Identifier
```text
CALL server OrganizationUnit_Update(Source)
OrganizationId = OrganizationUnit_Update.OrganizationId
END
```

### Employees › Project_Create_SA  _(service)_
In: Source: Project Record · Out: ProjectId: Project Identifier
```text
CALL server Project_Create(Source)
ProjectId = Project_Create.ProjectId
END
```

### Employees › Project_CreateOrUpdate_SA  _(service)_
In: Source: Project Record · Out: ProjectId: Project Identifier
```text
CALL server Project_CreateOrUpdate(Source)
ProjectId = Project_CreateOrUpdate.ProjectId
END
```

### Employees › Project_Delete_SA  _(service)_
In: ProjectId: Project Identifier · Out: —
```text
QUERY GetProjectById from Project; where Project.Id = ProjectId; max 1
IF GetProjectById.List.Empty   «Empty?»
  THEN:
    END
  ELSE:
    CALL server Project_Delete(ProjectId)
    END
```

### Employees › Project_New_SA  _(service)_
In: Name: Text, Code: Text, OrganizationUnitId: OrganizationUnit Identifier, StartDate: Date, EndDate: Date, Description: Text, ResponsibleId: Employee Identifier, EstimatedDays: Decimal · Out: Record: Project Record
```text
CALL server Project_New(Code, Description, EndDate, EstimatedDays, Name, OrganizationUnitId, ResponsibleId, StartDate)
Record = Project_New.Record
END
```

### Employees › Project_Update_SA  _(service)_
In: Source: Project Record · Out: —
```text
CALL server Project_Update(Source)
END
```

### Employees › UpdateOfficeLocationWithEntities_SA  _(service)_
In: OfficeLocation: OfficeLocation · Out: —
```text
CALL server UpdateOfficeLocationWithEntities(OfficeLocation)
END
```

### SalesSampleData › CompanyAccountManager_ImportExcel  _(server)_
```text
EXCEL→LIST GetSampleCompanyAccountManagerFomExcel from Resources.SampleCompanyAccountManager_xlsx.Content 
FOR EACH GetSampleCompanyAccountManagerFomExcel:
  QUERY GetCompanyByName from Company; where Company.CompanyName = GetSampleCompanyAccountManagerFomExcel.Current.Excel_CompanyAccountManager.Co…; max 1
  QUERY GetAccountManager from AccountManager; where AccountManager.CompanyId = GetCompanyByName.List.Current.Company.Id AND AccountManager.UserId = User_GetIdbyEmail(Email:GetSampleCompanyAccountManagerFomExcel.Current.Exce…
  GetAccountManager.List.Current.AccountManager.UserId = User_GetIdbyEmail(Email:GetSampleCompanyAccountManagerFomExcel.Current.Excel_CompanyAccountManager.AccountManager); GetAccountManager.List.Current.AccountManager.CompanyId = GetCompanyByName.List.Current.Company.Id
  CALL server CreateOrUpdateAccountManager(GetAccountManager.List.Current.AccountManager)
  ↪ continue at «ForEach»
END
```

### SalesSampleData › Opportunity_GetIdFromName  _(server)_
In: OpportunityName: Text · Out: OpportunityId: Opportunity Identifier
```text
QUERY GetOpportunityFromName from Opportunity; where Opportunity.Name = OpportunityName
OpportunityId = GetOpportunityFromName.List.Current.Opportunity.Id
END
```

### SalesSampleData › Opportunity_ImportFromExcel  _(server)_
In: Excel: Binary Data · Out: —
```text
CALL server Opportunity_ImportFromExcel_ShiftDate(Excel, False)
END
```

### SalesSampleData › Opportunity_ImportFromExcel_ShiftDate  _(server)_
In: Excel: Binary Data, IsSampleData: Boolean · Out: —
```text
IF IsSampleData
  THEN:
    DiffDate = DiffDays(Site.ZeroDate,CurrDate())
    EXCEL→LIST OppurtinitiesExcel from Excel
    FOR EACH OppurtinitiesExcel:
      OpportunityRecord.Opportunity.CompanyId = Company_GetIdFromName(OppurtinitiesExcel.Current.Excel_Opportunity.CompanyName); OpportunityRecord.Opportunity.Name = OppurtinitiesExcel.Current.Excel_Opportunity.Name; OpportunityRecord.Opportunity.Description = OppurtinitiesExcel.Current.Excel_Opportunity.Description; OpportunityRecord.Opportunity.CloseDate = AddDays(OppurtinitiesExcel.Current.Excel_Opportunity.CloseDate,DiffDate); OpportunityRecord.Opportunity.Amount = OppurtinitiesExcel.Current.Excel_Opportunity.Amount; OpportunityRecord.Opportunity.Stage = OpportunityStage_GetIdFromLabel(OppurtinitiesExcel.Current.Excel_Opportunity.Stage); OpportunityRecord.Opportunity.LastModified = CurrDateTime()
      CALL server Opportunity_Create(OpportunityRecord)
      ↪ continue at «ForEach»
    END
  ELSE:
    ↪ continue at «OppurtinitiesExcel»
```

### SalesSampleData › OpportunityStage_GetIdFromLabel  _(server)_
In: Label: Text · Out: OpportunityStageId: OpportunityStage Identifier
```text
QUERY GetOpportunityStage from OpportunityStage; where OpportunityStage.Label = Label; max 1
OpportunityStageId = GetOpportunityStage.List.Current.OpportunityStage.Id
END
```

### SalesSampleData › SampleData_Bootstrap  _(server)_
```text
IF SampleData_BootstrapDone() = True   «Bootstrap done?»
  THEN:
    END
  ELSE:
    Site.BootstrapFeedback = ""
    CALL server GetRoleForUpdate(Roles.UserManager)
    CALL server CRM_SampleData_Bootstrap()
    CALL server SampleData_Bootstrap_Employee("", "")
    DiffDate = DiffDays(Site.ZeroDate, CurrDate())
    CALL server CompanyAccountManager_ImportExcel()
    CALL server Opportunity_ImportFromExcel_ShiftDate(Resources.SampleOpportunities_xlsx.Content, True)
    CALL server SampleData_BootstrapUsers()
    EXCEL→LIST GetSampleQuarterQuotasFromExcel from Resources.SampleQuarterQuotas_xlsx.Content
    FOR EACH GetSampleQuarterQuotasFromExcel:
      QuarterQuotaRecord.QuarterQuota.UserId = User_GetIdbyEmail(Email:GetSampleQuarterQuotasFromExcel.Current.Excel_QuarterQuota.Username); QuarterQuotaRecord.QuarterQuota.QuarterQuota = GetSampleQuarterQuotasFromExcel.Current.Excel_QuarterQuota.QuarterQuota
      CALL server CreateQuarterQuota(QuarterQuotaRecord)
      ↪ continue at «ForEach»
    EXCEL→LIST GetSampleTasksFromExcel from Resources.SampleTasks_xlsx.Content
    FOR EACH GetSampleTasksFromExcel:
      TaskRecord.Task.Name = GetSampleTasksFromExcel.Current.Excel_Task.Name; TaskRecord.Task.UserId = User_GetIdbyEmail(Email:GetSampleTasksFromExcel.Current.Excel_Task.Email); TaskRecord.Task.DueDate = AddDays(GetSampleTasksFromExcel.Current.Excel_Task.DueDate,DiffDate); TaskRecord.Task.CompanyId = Company_GetIdFromName(GetSampleTasksFromExcel.Current.Excel_Task.CompanyName); TaskRecord.Task.OpportunityId = Opportunity_GetIdFromName(GetSampleTasksFromExcel.Current.Excel_Task.OpportunityName); TaskRecord.Task.ContactId = Contact_GetIdFromName(GetSampleTasksFromExcel.Current.Excel_Task.Contact); TaskRecord.Task.IsCompleted = GetSampleTasksFromExcel.Current.Excel_Task.IsCompleted
      CALL server CreateTask(TaskRecord)
      ↪ continue at «ForEach»
    CALL server EspaceInvalidateCache()
    END
ON EXCEPTION All Exceptions:
  Site.BootstrapFeedback = "There was a problem creating sample data. <br /><br />Please check ServiceCenter error log."
  CALL server EspaceInvalidateCache()
  END
```

### SalesSampleData › SampleData_BootstrapDone  _(server)_
In: — · Out: BootstrapDone: Boolean
```text
QUERY GetOpportunities 
IF not GetOpportunities.List.Empty   «Has Opportunities?»
  THEN:
    END
  ELSE:
    QUERY GetQuarterQuotas 
    IF not GetQuarterQuotas.List.Empty   «Has Quotas?»
      THEN:
        END
      ELSE:
        QUERY GetTasks 
        IF not GetTasks.List.Empty   «Has Tasks?»
          THEN:
            END
          ELSE:
            BootstrapDone = False
            END
```

### SalesSampleData › SampleData_BootstrapUsers  _(server)_
In: — · Out: Success: Boolean
```text
EXCEL→LIST GetSampleUsersFromExcel from Resources.SampleSalesUsers_xlsx.Content 
FOR EACH GetSampleUsersFromExcel:
  QUERY GetUserByEmail from User; where User.Is_Active = True AND User.Email = GetSampleUsersFromExcel.Current.Excel_User.Email; max 1
  IF not GetUserByEmail.List.Empty   «Exists?»
    THEN:
      CALL server User_GrantPermissionByName(GetSampleUsersFromExcel.Current.Excel_User.Permission, GetUserByEmail.List.Current.User.Id)
      ↪ continue at «ForEach»
    ELSE:
      ↪ continue at «ForEach»
END
ON EXCEPTION All Exceptions:
  IF Index(AllExceptions.ExceptionMessage, "Users")>=0   «Licensing?»
    THEN:
      Site.BootstrapFeedback = "Sample data was not loaded. <br /><br /> " + AllExceptions.ExceptionMessage + "<br /><br /> To load the sample data, p…; Success = False
      END
    ELSE:
      Site.BootstrapFeedback = "Sample data was not loaded because there was a problem creating users. <br /><br />Please check Service Center for a d…; Success = False
      ↪ continue at «End»
```

### SalesSampleData › SampleData_Delete  _(server)_
In: DeleteCustomerData: Boolean · Out: —
```text
SQL DeleteTasks (see SQL below)
SQL DeleteQuarterQuotas (see SQL below)
SQL DeleteOppurtunities (see SQL below)
SQL DeleteAccountManagers (see SQL below)
EXCEL→LIST GetSampleUsersFromExcel from Resources.SampleSalesUsers_xlsx.Content 
FOR EACH GetSampleUsersFromExcel:
  QUERY GetUser from User; where User.Is_Active = True AND User.Email = GetSampleUsersFromExcel.Current.Excel_User.Email; max 1
  IF not GetUser.List.Empty   «Exists?»
    THEN:
      CALL server RevokeAccountManagerRole(GetUser.List.Current.User.Id)
      CALL server RevokeSalesManagerRole(GetUser.List.Current.User.Id)
      ↪ continue at «ForEach»
    ELSE:
      ↪ continue at «ForEach»
IF DeleteCustomerData   «Delete Customer Data?»
  THEN:
    CALL server SampleData_DeleteCRMCore()
    IF SampleData_DeleteCRMCore.Success   «Success?»
      THEN:
        CALL server Feedback_Message("All sample data has been deleted.", Entities.MessageType.Success)
        END
      ELSE:
        CALL server Feedback_Message("Customers sample data could not be deleted. It might be in…, Entities.MessageType.Warning)
        ↪ continue at «End»
  ELSE:
    ↪ continue at «Feedback_Message_Success»
```
SQL `DeleteTasks`:
```sql
DELETE FROM {Task}
```
SQL `DeleteQuarterQuotas`:
```sql
DELETE FROM {QuarterQuota}
```
SQL `DeleteOppurtunities`:
```sql
DELETE FROM {Opportunity}
```
SQL `DeleteAccountManagers`:
```sql
DELETE FROM {AccountManager}
```

### SalesSampleData › SampleData_DeleteBootstrap  _(server)_
```text
CALL server SampleData_Delete(False)
CALL server SampleData_Bootstrap()
END
```

### SalesSampleData › SampleData_DeleteCRMCore  _(server)_
In: — · Out: Success: Boolean
```text
CALL server CRM_SampleData_Delete()
END
ON EXCEPTION Database Exception:
  Success = False
  END
```

### SalesSampleData › User_GrantPermissionByName  _(server)_
In: UserId: User Identifier, Permission: Text · Out: —
```text
IF Permission = "AccountManager"   «AccountManager?»
  THEN:
    CALL server GrantAccountManagerRole(UserId)
    END
  ELSE:
    IF Permission = "SalesManager"   «SalesManager?»
      THEN:
        CALL server GrantSalesManagerRole(UserId)
        END
      ELSE:
        END
```

### SalesSampleData › Configuration › DeleteAllData  _(screen)_
```text
IF IsSampleUser
  THEN:
    CALL server Logout()
    CALL server SampleData_Delete(True)
    IF IsSampleUser
      THEN:
        QUERY CheckAdmin from User; where User.Username like "admin" AND User.Password like GenerateSaltedMD5Hash("admin")
        IF not CheckAdmin.List.Empty   «Admin/Admin»
          THEN:
            CALL server User_Login("admin", False, "admin")
            CALL server Feedback_Message("You are now logged in with the admin user.", Entities.MessageType.Info)
            END
          ELSE:
            CALL server Feedback_Message("Login with admin user from User Management to create Sales…, Entities.MessageType.Info)
            GO TO Login
      ELSE:
        END
  ELSE:
    ↪ continue at «SampleData_Delete»
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### SalesSampleData › Configuration › DownloadTemplateCompanies  _(screen)_
```text
DOWNLOAD
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### SalesSampleData › Configuration › DownloadTemplateContacts  _(screen)_
```text
DOWNLOAD
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### SalesSampleData › Configuration › DownloadTemplateOpportunities  _(screen)_
```text
DOWNLOAD
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### SalesSampleData › Configuration › ReloadSampleData  _(screen)_
```text
CALL server SampleData_Bootstrap()
CALL server Feedback_Message("Sample Data Loaded.", Entities.MessageType.Success)
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### SalesSampleData › Configuration › UploadCompanies  _(screen)_
```text
CALL server Company_ImportFromExcel(InputFileCompanies.Content)
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### SalesSampleData › Configuration › UploadContacts  _(screen)_
```text
CALL server Contact_ImportFromExcel(InputFileContacts.Content)
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### SalesSampleData › Configuration › UploadOpportunities  _(screen)_
```text
CALL server Opportunity_ImportFromExcel(InputFileOpportunities.Content)
END
ON EXCEPTION All Exceptions:
  CALL server Feedback_Message(AllExceptions.ExceptionMessage, Entities.MessageType.Error)
  END
```

### SalesSampleData › Configuration › Preparation  _(preparation)_
```text
QUERY GetUser from User; where User.Is_Active = True AND User.Id = GetUserId()
IsSampleUser = Index(GetUser.List.Current.User.Email,"sales.example") <> -1
QUERY GetOpportunities max 1
END
```

### EmployeesSampleData › BPT_Bootstrap  _(server)_
In: SYDId: Text, SYDCallback: Text · Out: —
```text
CALL server SampleData_Bootstrap(SYDCallback, SYDId)
IF SYDId <> ""   «SYDId Exists?»
  THEN:
    CALL server GetRequest_Submit("SYDId=" + SYDId + "&Operation=BOOT&Status=OK&Message=", SYDCallback)
    END
  ELSE:
    ↪ continue at «End»
ON EXCEPTION All Exceptions:
  CALL server AbortTransaction()
  CALL server GetRequest_Submit("SYDId=" + SYDId + "&Operation=BOOT&Status=ERROR&Message=" …, SYDCallback)
  END
```

### EmployeesSampleData › BPT_Delete  _(server)_
In: SYDId: Text, SYDCallback: Text · Out: —
```text
CALL server SampleData_Delete()
IF SYDId <> ""   «SYDId Exists?»
  THEN:
    CALL server GetRequest_Submit("SYDId=" + SYDId + "&Operation=DEL&Status=OK&Message=", SYDCallback)
    END
  ELSE:
    ↪ continue at «End»
ON EXCEPTION All Exceptions:
  CALL server AbortTransaction()
  CALL server GetRequest_Submit("SYDId=" + SYDId + "&Operation=DEL&Status=ERROR&Message=" +…, SYDCallback)
  END
```

### EmployeesSampleData › CheckForData  _(server)_
In: — · Out: HasData: Boolean
```text
QUERY GetEmployees from Employee, Gender, User; max 1
HasData = not GetEmployees.List.Empty
END
```

### EmployeesSampleData › Employee_GetIdFromName  _(server)_
In: Name: Text · Out: EmployeeId: Employee Identifier
```text
QUERY GetEmployee from Employee, User; where Name = User.Name
EmployeeId = GetEmployee.List.Current.User.Id
END
```

### EmployeesSampleData › GetBootstrapFeedback  _(server)_
In: — · Out: BootstrapFeedback: Text
```text
BootstrapFeedback = Site.BootstrapFeedback
END
```

### EmployeesSampleData › GetNextBirthdays  _(server)_
In: NumberOfObjects: Integer · Out: EmployeeList: Employee List
```text
QUERY GetEmployeeByNextBirthdayProximity from Employee, User; where User.Is_Active = True; calc BirthdayProximity = DiffDays(CurrDate(),NextBirthday); calc NextBirthday = NewDate(If(DiffDays(ThisYearsBirthday,CurrDate())<0,Year(CurrDate()),Year(CurrDate())+1),Month(Empl…; calc ThisYearsBirthday = NewDate(Year(CurrDate()),Month(Employee.Birthday),Day(Employee.Birthday)); max NumberOfObjects
CALL server ListAppendAll(EmployeeList, ConvertList(GetEmployeeByNextBirthdayProximity.List, {Value…)
END
```

### EmployeesSampleData › GetSampleData  _(server)_
In: — · Out: SampleData: Binary Data
```text
SampleData = Resources.EmployeeSampleData_xlsx.Content
END
```

### EmployeesSampleData › OrganizationUnit_GetIdFromName  _(server)_
In: Name: Text · Out: OrganizationUnitId: OrganizationUnit Identifier
```text
QUERY GetOrganizationUnitsByName from OrganizationUnit; where OrganizationUnit.Name = Name; max 1
OrganizationUnitId = GetOrganizationUnitsByName.List.Current.OrganizationUnit.Id
END
```

### EmployeesSampleData › SampleData_BoostrapAdjustedDates  _(server)_
```text
CALL server GetNextBirthdays(5)
FOR EACH GetNextBirthdays.EmployeeList:
  GetNextBirthdays.EmployeeList.Current.Birthday = AddDays(NewDate(Year(GetNextBirthdays.EmployeeList.Current.Birthday),Month(CurrDate()),Day(CurrDate())),GetNextBirthday…
  CALL server Employee_Update(GetNextBirthdays.EmployeeList.Current)
  ↪ continue at «ForEach»
QUERY GetEmployeeByNextEntryProximity from Employee, User; where User.Is_Active = True AND DiffDays(NextEntry,CurrDate()) < 0; calc EntryProximity = DiffDays(NextEntry,CurrDate()); calc NextEntry = NewDate(Year(CurrDate()),Month(Employee.EntryDate),Day(Employee.EntryDate)); max 5
FOR EACH GetEmployeeByNextEntryProximity.List:
  GetEmployeeByNextEntryProximity.List.Current.Employee.EntryDate = AddDays(CurrDate(),-GetEmployeeByNextEntryProximity.List.CurrentRowNumber-1)
  CALL server Employee_Update(GetEmployeeByNextEntryProximity.List.Current.Employee)
  ↪ continue at «ForEach»
QUERY GetEmployeeByNextLeaveProximity from Employee, User; where User.Is_Active = True AND Employee.EndedOn <> NullDate(); calc LeaveProximity = DiffDays(NextLeave,CurrDate()); calc NextLeave = NewDate(Year(CurrDate()),Month(Employee.EndedOn),Day(Employee.EndedOn)); max 5
FOR EACH GetEmployeeByNextLeaveProximity.List:
  GetEmployeeByNextLeaveProximity.List.Current.Employee.EndedOn = DateTimeToDate(AddDays(CurrDate(),-GetEmployeeByNextLeaveProximity.List.CurrentRowNumber-1)); GetEmployeeByNextLeaveProximity.List.Current.User.Is_Active = False
  CALL server Employee_Update(GetEmployeeByNextLeaveProximity.List.Current.Employee)
  CALL server UpdateUser(GetEmployeeByNextLeaveProximity.List.Current.User)
  ↪ continue at «ForEach»
CALL server CommitTransaction()
END
```

### EmployeesSampleData › SampleData_BoostrapEmployeeData  _(server)_
In: ExcelToBootstrap: Binary Data · Out: —
```text
CALL server SampleData_BoostrapEmployees(ExcelToBootstrap)
CALL server SampleData_BootstrapEmployeesReportsTo(ExcelToBootstrap)
CALL server SampleData_BootstrapEmployeePictures()
CALL server SampleData_BootstrapExtraEmployeeData(ExcelToBootstrap)
CALL server SampleData_BootstrapEmployeeLocations(ExcelToBootstrap)
CALL server SampleData_BootstrapEmployeePlans(ExcelToBootstrap)
END
```

### EmployeesSampleData › SampleData_BoostrapEmployees  _(server)_
In: Excel: Binary Data · Out: —
```text
QUERY GetEmployees from Employee, User
IF GetEmployees.List.Empty   «Are there Employees already created?»
  THEN:
    EXCEL→LIST GetEmployeesFromExcel from Excel
    FOR EACH GetEmployeesFromExcel:
      CALL server EmployeeUser_CreateOrUpdate(ConvertRecord(GetEmployeesFromExcel.Current.Excel_EmployeeL…)
      ↪ continue at «ForEach»
    END
  ELSE:
    END
```

### EmployeesSampleData › SampleData_BoostrapOrganizationUnits  _(server)_
In: Excel: Binary Data · Out: —
```text
EXCEL→LIST GetOrganizationUnitFromExcel from Excel
FOR EACH GetOrganizationUnitFromExcel:
  QUERY GetOrganizationUnit from OrganizationUnit; where OrganizationUnit.Name = GetOrganizationUnitFromExcel.Current.OrganizationUnit_Excel.Unit
  GetOrganizationUnit.List.Current.OrganizationUnit.Name = GetOrganizationUnitFromExcel.Current.OrganizationUnit_Excel.Unit; GetOrganizationUnit.List.Current.OrganizationUnit.ParentOrgUnitId = OrganizationUnit_GetIdFromName(GetOrganizationUnitFromExcel.Current.OrganizationUnit_Excel.Department); GetOrganizationUnit.List.Current.OrganizationUnit.Type = If(GetOrganizationUnitFromExcel.Current.OrganizationUnit_Excel.Department = "", Entities.OrganizationUnitType.Departmen…; GetOrganizationUnit.List.Current.OrganizationUnit.ManagerId = Employee_GetIdFromName(Name:GetOrganizationUnitFromExcel.Current.OrganizationUnit_Excel.UnitManager)
  CALL server OrganizationUnit_CreateOrUpdate(GetOrganizationUnit.List.Current)
  ↪ continue at «ForEach»
END
```

### EmployeesSampleData › SampleData_BoostrapProjects  _(server)_
In: Excel: Binary Data, IsSampleData: Boolean · Out: —
```text
IF IsSampleData
  THEN:
    DiffDate = DiffDays(Site.ZeroDate,CurrDate())
    EXCEL→LIST GetProjectsFromExcel from Excel
    FOR EACH GetProjectsFromExcel:
      QUERY GetProjectByName from Project; where GetProjectsFromExcel.Current.Projects_Excel.Name = Project.Name; max 1
      GetProjectByName.List.Current.Project.Name = GetProjectsFromExcel.Current.Projects_Excel.Name; GetProjectByName.List.Current.Project.Code = GetProjectsFromExcel.Current.Projects_Excel.Code; GetProjectByName.List.Current.Project.StartDate = If(GetProjectsFromExcel.Current.Projects_Excel.StartDate = NullDate(),NullDate(),AddDays(GetProjectsFromExcel.Current.P…; GetProjectByName.List.Current.Project.EndDate = If(GetProjectsFromExcel.Current.Projects_Excel.EndDate = NullDate(),NullDate(),AddDays(GetProjectsFromExcel.Current.Pro…; GetProjectByName.List.Current.Project.Description = GetProjectsFromExcel.Current.Projects_Excel.Description; GetProjectByName.List.Current.Project.EstimateDays = GetProjectsFromExcel.Current.Projects_Excel.EstimateDays; GetProjectByName.List.Current.Project.OrganizationUnitId = OrganizationUnit_GetIdFromName(GetProjectsFromExcel.Current.Projects_Excel.OrganizationUnit); GetProjectByName.List.Current.Project.ResponsibleId = Employee_GetIdFromName(Name:GetProjectsFromExcel.Current.Projects_Excel.Responsible)
      CALL server Project_CreateOrUpdate(GetProjectByName.List.Current)
      ↪ continue at «ForEach»
    END
  ELSE:
    ↪ continue at «GetProjectsFromExcel»
```

### EmployeesSampleData › SampleData_Bootstrap  _(server)_
In: SYDId: Text, SYDCallback: Text, ExcelToBootstrap: Binary Data, IsSampleData: Boolean · Out: —
```text
IF not Compare(ExcelToBootstrap, NullBinary())   «Was an Excel given?»
  THEN:
    CALL server UpdateBootstrapStatus(10, SYDCallback, SYDId, "Bootstrapping secondary entities...")
    CALL server SampleData_BootstrapSecondaryEntities(ExcelToBootstrap)
    CALL server UpdateBootstrapStatus(30, SYDCallback, SYDId, "Bootstrapping employee data...")
    CALL server SampleData_BoostrapEmployeeData(ExcelToBootstrap)
    CALL server UpdateBootstrapStatus(50, SYDCallback, SYDId, "Bootstrapping organization units...")
    CALL server SampleData_BoostrapOrganizationUnits(ExcelToBootstrap)
    CALL server UpdateBootstrapStatus(70, SYDCallback, SYDId, "Bootstrapping projects...")
    CALL server SampleData_BoostrapProjects(ExcelToBootstrap, IsSampleData)
    CALL server UpdateBootstrapStatus(100, SYDCallback, SYDId, "Bootstrap finished.")
    END
  ELSE:
    ExcelToBootstrap = Resources.EmployeeSampleData_xlsx.Content
    ↪ continue at «UpdateBootstrapStatus»
```

### EmployeesSampleData › SampleData_BootstrapEmployeeLocations  _(server)_
In: ExcelToBootstrap: Binary Data · Out: —
```text
EXCEL→LIST ConvertEmployeeListFromExcel from ExcelToBootstrap
FOR EACH ConvertEmployeeListFromExcel:
  QUERY GetEmployeeByEmail from Employee, Gender, User; where User.Email = ConvertEmployeeListFromExcel.Current.Excel_EmployeeLocation.EmployeeEmail AND Gender.Is_Active = True
  CALL server CreateOrUpdateEmployeeLocationFromAddress(ConvertEmployeeListFromExcel.Current.Excel_EmployeeLocation…, GetEmployeeByEmail.List.Current.Employee.UserId)
  ↪ continue at «ForEach»
END
```

### EmployeesSampleData › SampleData_BootstrapEmployeePictures  _(server)_
```text
CALL server LoadZip(Resources.Photos_zip.Content)
CALL server GetFiles(LoadPhotosZip.ZipHandle)
FOR EACH GetPhotos.FileList:
  CALL server Employee_GetByPictureName(GetPhotos.FileList.Current.File.Name)
  IF GetEmployeeByPictureName.Employee.Employee.UserId = NullIdentifier()   «Empty?»
    THEN:
      ↪ continue at «ForEach»
    ELSE:
      CALL server String_Split(".", GetPhotos.FileList.Current.File.Name)
      EmployeePicture.EmployeeId = GetEmployeeByPictureName.Employee.Employee.UserId; EmployeePicture.Picture = GetPhotos.FileList.Current.File.Content; EmployeePicture.Filename = GetPhotos.FileList.Current.File.Name; EmployeePicture.Filetype = "image/"+String_Split_Extension.List[String_Split_Extension.List.Length-1].Text.Value
      CALL server EmployeePicture_CreateOrUpdate(EmployeePicture)
      CALL server CropPhoto(EmployeePicture.Picture)
      EmployeeThumbnail.EmployeeId = GetEmployeeByPictureName.Employee.Employee.UserId; EmployeeThumbnail.Thumbnail = CropPhoto.Thumbnail; EmployeeThumbnail.Filename = GetPhotos.FileList.Current.File.Name; EmployeeThumbnail.Filetype = "image/"+String_Split_Extension.List[String_Split_Extension.List.Length-1].Text.Value
      CALL server EmployeeThumbnail_CreateOrUpdate(EmployeeThumbnail)
      ↪ continue at «ForEach»
END
```

### EmployeesSampleData › SampleData_BootstrapEmployeePlans  _(server)_
In: ExcelToBootstrap: Binary Data · Out: —
```text
QUERY GetOfficeLocationsOnlyWithLocationsAndPlans from Location, OfficeLocation, Plan; where Location.IsActive = True
IF not GetOfficeLocationsOnlyWithLocationsAndPlans.List.Empty   «Were Plans created for OfficeLocations?»
  THEN:
    END
  ELSE:
    CALL server LoadZip(Resources.OfficeLocationPlanImages_zip.Content)
    CALL server GetFiles(LoadPlanImagesZip.ZipHandle)
    FOR EACH GetPlanImages.FileList:
      CALL server String_Split(".", GetPlanImages.FileList.Current.File.Name)
      CALL server CreateFileEntities(GetPlanImages.FileList.Current.File.Content, GetPlanImages.FileList.Current.File.Name, "image/"+String_Split_ByExtension.List[String_Split_ByExten…)
      ↪ continue at «ForEach»
    EXCEL→LIST SpaceClasses from ExcelToBootstrap
    FOR EACH SpaceClasses:
      CALL server SpaceClassCreateOrUpdate(SpaceClasses.Current.Excel_SpaceClasses.Restricted, SpaceClasses.Current.Excel_SpaceClasses.Selectable, SpaceClasses.Current.Excel_SpaceClasses.SpaceClass)
      ↪ continue at «ForEach»
    CALL server CommitTransaction()
    EXCEL→LIST OfficeLocationPlans from ExcelToBootstrap
    FOR EACH OfficeLocationPlans:
      QUERY GetLocationFromOfficeLocationName from Location, OfficeLocation; where Location.IsActive = True AND OfficeLocation.Name = OfficeLocationPlans.Current.Excel_OfficeLocationPlans.OfficeLocation
      QUERY GetPlanFileByFilename from File2; where File2.Filename = OfficeLocationPlans.Current.Excel_OfficeLocationPlans.PlanImageName; max 1
      CALL server CreatePlanForFile(GetPlanFileByFilename.List.Current.File2.Id, False, GetLocationFromOfficeLocationName.List.Current.Location.Id, OfficeLocationPlans.Current.Excel_OfficeLocationPlans.PlanN…)
      CALL server CreatePlanInfoForPlan(OfficeLocationPlans.Current.Excel_OfficeLocationPlans.HTML, OfficeLocationPlans.Current.Excel_OfficeLocationPlans.JSON, CreatePlanForFile.PlanId)
      CALL server ProcessPlan(CreatePlanForFile.PlanId)
      ↪ continue at «ForEach»
    CALL server CommitTransaction()
    EXCEL→LIST EmployeePlacement from ExcelToBootstrap
    FOR EACH EmployeePlacement:
      QUERY GetUsersOfficeLocation from Employee, OfficeLocation, User; where User.Is_Active = True AND User.Email = EmployeePlacement.Current.Excel_EmployeePlacement.EmployeeEmail
      QUERY GetSpaceForPlanOnUsersOfficeLocation from Location, Plan, Space, SpaceClass; where Location.IsActive = True AND Space.Name = EmployeePlacement.Current.Excel_EmployeePlacement.SpaceName AND SpaceClass.Name = EmployeePlacement.Current.Excel_EmployeePlacement.SpaceClass AND Plan.Name = EmployeePlacement.Current.Excel_EmployeePlacement.PlanName AND Location.Id = GetUsersOfficeLocation.List.Current.OfficeLocation.LocationId
      IF not GetSpaceForPlanOnUsersOfficeLocation.List.Empty   «Is the Space available?»
        THEN:
          CALL server UpdateUserFloorPlanSpace(GetUsersOfficeLocation.List.Current.Employee.UserId, GetUsersOfficeLocation.List.Current.OfficeLocation.Location…, GetSpaceForPlanOnUsersOfficeLocation.List.Current.SpaceClas…, EmployeePlacement.Current.Excel_EmployeePlacement.SpaceName)
          ↪ continue at «ForEach»
        ELSE:
          ↪ continue at «ForEach»
    CALL server CommitTransaction()
    END
```

### EmployeesSampleData › SampleData_BootstrapEmployeesReportsTo  _(server)_
In: Excel: Binary Data · Out: —
```text
EXCEL→LIST GetEmployeesFromExcel from Excel
FOR EACH GetEmployeesFromExcel:
  IF Length(GetEmployeesFromExcel.Current.Excel_EmployeeList.ReportsTo) > 0   «Reports to?»
    THEN:
      QUERY GetReportsTo from Employee, User; where User.Is_Active = True AND User.Name = GetEmployeesFromExcel.Current.Excel_EmployeeList.ReportsTo
      IF GetReportsTo.List.Empty   «Not found?»
        THEN:
          ↪ continue at «ForEach»
        ELSE:
          QUERY GetEmployee from Employee, User; where User.Is_Active = True AND User.Name = GetEmployeesFromExcel.Current.Excel_EmployeeList.Name
          IF GetEmployee.List.Empty   «Employee not found?»
            THEN:
              ↪ continue at «ForEach»
            ELSE:
              GetEmployee.List.Current.Employee.ReportsToId = GetReportsTo.List.Current.Employee.UserId
              CALL server Employee_Update(GetEmployee.List.Current.Employee)
              ↪ continue at «ForEach»
    ELSE:
      ↪ continue at «ForEach»
END
```

### EmployeesSampleData › SampleData_BootstrapExtraEmployeeData  _(server)_
In: ExcelToBootstrap: Binary Data · Out: —
```text
QUERY GetEmployees from Employee, Gender, User; where User.Is_Active = True
IF GetEmployees.List.Empty   «Are there no Employees?»
  THEN:
    END
  ELSE:
    EXCEL→LIST ConvertEmployeeListFromExcel from ExcelToBootstrap
    FOR EACH ConvertEmployeeListFromExcel:
      QUERY GetEmployeeByEmail from Employee, User; where User.Email = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.Email
      IF GetEmployeeByEmail.List.Empty   «Employee not found?»
        THEN:
          CALL server LogMessage("An employee with email '" + ConvertEmployeeListFromExcel.C…, "DIRECTORY_DATA")
          ↪ continue at «ForEach»
        ELSE:
          Employee.UserId = GetEmployeeByEmail.List.Current.Employee.UserId; Employee.OrganizationUnitId = GetEmployeeByEmail.List.Current.Employee.OrganizationUnitId; Employee.ReportsToId = GetEmployeeByEmail.List.Current.Employee.ReportsToId; Employee.Birthday = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.Birthday; Employee.Address = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.Address; Employee.EmployeeCode = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.EmployeeCode; Employee.Extension = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.Extension; Employee.SkypeUsername = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.SkypeUsername; Employee.IMUsername = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.IMUsername; Employee.LinkedInUrl = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.LinkedInUrl; Employee.EntryDate = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.EntryDate; Employee.CreatedOn = CurrDateTime(); Employee.EndedOn = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.EndedOn; Employee.CreatedBy = GetUserId(); Employee.EndedBy = GetUserId()
          CALL server SampleData_UpdateEmployeeDataFromExcel(Employee, ConvertEmployeeListFromExcel.Current.Excel_EmployeeList)
          GetEmployeeByEmail.List.Current.User.Is_Active = True; GetEmployeeByEmail.List.Current.User.Name = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.Name; GetEmployeeByEmail.List.Current.User.MobilePhone = ConvertEmployeeListFromExcel.Current.Excel_EmployeeList.MobilePhone
          CALL server UpdateUser(GetEmployeeByEmail.List.Current.User)
          ↪ continue at «ForEach»
    CALL server SampleData_BoostrapAdjustedDates()
    END
```

### EmployeesSampleData › SampleData_BootstrapJobTitle  _(server)_
```text
QUERY GetJobTitles from JobTitle
IF GetJobTitles.List.Empty
  THEN:
    EXCEL→LIST ExcelJobTitleToRecordList from Resources.EmployeeSampleData_xlsx.Content
    FOR EACH ExcelJobTitleToRecordList:
      CALL server JobTitle_New(ExcelJobTitleToRecordList.Current.JobTitle.Name)
      CALL server JobTitle_Create(JobTitle_New.Record)
      ↪ continue at «ForEach»
    CALL server CommitTransaction()
    END
  ELSE:
    END
```

### EmployeesSampleData › SampleData_BootstrapOfficeLocation  _(server)_
In: ExcelFile: Binary Data · Out: —
```text
QUERY GetOfficeLocations 
IF GetOfficeLocations.List.Empty
  THEN:
    EXCEL→LIST ExcelOfficeLocationToRecordList from ExcelFile
    FOR EACH ExcelOfficeLocationToRecordList:
      QUERY GetCountryStateByCode from CountryState; where CountryState.Name = ExcelOfficeLocationToRecordList.Current.Excel_OfficeLocation.CountryState
      CALL server OfficeLocation_New(ExcelOfficeLocationToRecordList.Current.Excel_OfficeLocatio…, GetCountryStateByCode.List.Current.CountryState.Id, NullIdentifier(), ExcelOfficeLocationToRecordList.Current.Excel_OfficeLocatio…, ExcelOfficeLocationToRecordList.Current.Excel_OfficeLocatio…, NullIdentifier())
      CALL server CreateOfficeLocationWithEntities(OfficeLocation_New.Record)
      ↪ continue at «ForEach»
    CALL server CommitTransaction()
    END
  ELSE:
    END
```

### EmployeesSampleData › SampleData_BootstrapOnPublish  _(server)_
```text
IF Site.DoBootstrapOnPublish
  THEN:
    CALL server SampleData_Bootstrap("", "")
    Site.DoBootstrapOnPublish = False
    END
  ELSE:
    END
```

### EmployeesSampleData › SampleData_BootstrapSecondaryEntities  _(server)_
In: ExcelToBootstrap: Binary Data · Out: —
```text
CALL server SampleData_BootstrapJobTitle()
CALL server SampleData_BootstrapOfficeLocation(ExcelToBootstrap)
CALL server SampleData_BoostrapOrganizationUnits(ExcelToBootstrap)
END
```

### EmployeesSampleData › SampleData_Delete  _(server)_
```text
QUERY GetProjects 
FOR EACH GetProjects.List:
  CALL server Project_Delete(GetProjects.List.Current.Project.Id)
  ↪ continue at «ForEach»
CALL server SampleData_DeleteEmployeePictures()
CALL server SampleData_DeleteAllEmployees()
CALL server SampleData_DeleteOfficeLocations()
CALL server SampleData_DeleteAllJobTitles()
CALL server SampleData_DeleteEmployeesCVLocation()
QUERY GetOrganizationUnits 
FOR EACH GetOrganizationUnits.List:
  CALL server OrganizationUnit_Delete(GetOrganizationUnits.List.Current.OrganizationUnit.Id)
  ↪ continue at «ForEach»
CALL server CommitTransaction()
END
```

### EmployeesSampleData › SampleData_DeleteAllEmployees  _(server)_
```text
QUERY GetEmployees from Employee, User
FOR EACH GetEmployees.List:
  CALL server Employee_Delete(GetEmployees.List.Current.Employee.UserId)
  ↪ continue at «ForEach»
CALL server CommitTransaction()
END
```

### EmployeesSampleData › SampleData_DeleteAllJobTitles  _(server)_
```text
QUERY GetJobTitles from JobTitle
FOR EACH GetJobTitles.List:
  CALL server JobTitle_Delete(GetJobTitles.List.Current.JobTitle.Id)
  ↪ continue at «ForEach»
CALL server CommitTransaction()
END
```

### EmployeesSampleData › SampleData_DeleteEmployeePictures  _(server)_
```text
QUERY GetEmployeePictures from Employee, EmployeePicture, User
FOR EACH GetEmployeePictures.List:
  CALL server EmployeePicture_Delete(GetEmployeePictures.List.Current.EmployeePicture.EmployeeId)
  ↪ continue at «ForEach»
QUERY GetEmployeeThumbnails from Employee, EmployeeThumbnail, User
FOR EACH GetEmployeeThumbnails.List:
  CALL server EmployeeThumbnail_Delete(GetEmployeeThumbnails.List.Current.EmployeeThumbnail.Employ…)
  ↪ continue at «ForEach»
CALL server CommitTransaction()
END
```

### EmployeesSampleData › SampleData_DeleteEmployeesCVLocation  _(server)_
```text
QUERY GetEmployeeCVs from Employee, EmployeeCV, User; where User.Is_Active = True
FOR EACH GetEmployeeCVs.List:
  CALL server EmployeeCV_Delete(GetEmployeeCVs.List.Current.EmployeeCV.Id)
  ↪ continue at «ForEach»
QUERY GetEmployeeLocations from Employee, EmployeeLocation, User; where User.Is_Active = True
FOR EACH GetEmployeeLocations.List:
  CALL server EmployeeLocation_Delete(GetEmployeeLocations.List.Current.EmployeeLocation.Employee…)
  ↪ continue at «ForEach»
CALL server CommitTransaction()
END
```

### EmployeesSampleData › SampleData_DeleteOfficeLocations  _(server)_
```text
QUERY GetOfficeLocations 
FOR EACH GetOfficeLocations.List:
  CALL server OfficeLocation_Delete(GetOfficeLocations.List.Current.OfficeLocation.Id)
  CALL server DeleteLocationAndPlans(GetOfficeLocations.List.Current.OfficeLocation.LocationId)
  ↪ continue at «ForEach»
CALL server CommitTransaction()
END
```

### EmployeesSampleData › SampleData_UpdateEmployeeDataFromExcel  _(server)_
In: ExcelRow: Excel_EmployeeList, EmployeeData: Employee · Out: —
```text
IF ExcelRow.Gender = GetGender(Entities.Gender.Male).Gender.Label   «If Excel row Gender is 'Male'?»
  THEN:
    EmployeeData.Gender = Entities.Gender.Male
    QUERY GetOfficeLocationsByName from OfficeLocation; where OfficeLocation.Name = ExcelRow.OfficeLocation
    IF GetOfficeLocationsByName.List.Empty   «Is there an OfficeLocation with this name?»
      THEN:
        QUERY GetJobTitlesByName from JobTitle; where JobTitle.Name = ExcelRow.JobTitle
        IF GetJobTitlesByName.List.Empty   «Are there no JobTitles with this Name?»
          THEN:
            CALL server JobTitle_New(ExcelRow.JobTitle)
            CALL server JobTitle_Create(JobTitle_New.Record)
            EmployeeData.JobTitleId = JobTitle_Create.JobTitleId
            CALL server Employee_Update(EmployeeData)
            END
          ELSE:
            EmployeeData.JobTitleId = GetJobTitlesByName.List.Current.JobTitle.Id
            ↪ continue at «Employee_Update»
      ELSE:
        EmployeeData.OfficeLocationId = GetOfficeLocationsByName.List.Current.OfficeLocation.Id
        ↪ continue at «GetJobTitlesByName»
  ELSE:
    EmployeeData.Gender = Entities.Gender.Female
    ↪ continue at «GetOfficeLocationsByName»
```

### EmployeesSampleData › UpdateBootstrapStatus  _(server)_
In: SYDId: Text, SYDCallback: Text, StatusPercentage: Integer, UpdateStepDescription: Text · Out: —
```text
IF SYDId <> ""   «SYDId Exists?»
  THEN:
    CALL server GetRequest_Submit("SYDId=" + SYDId + "&Operation=UPD&Status=" + StatusPercent…, SYDCallback)
    END
  ELSE:
    ↪ continue at «End»
ON EXCEPTION All Exceptions:
  END
```

### EmployeesSampleData › User_GetIdbyEmail  _(server)_
In: Email: Email · Out: UserId: User Identifier
```text
IF Length(Email) > 0   «Email present?»
  THEN:
    QUERY GetUserByEmail from User; where User.Email = Email AND User.Is_Active = True; max 1
    UserId = GetUserByEmail.List.Current.User.Id
    END
  ELSE:
    UserId = NullIdentifier()
    END
```

