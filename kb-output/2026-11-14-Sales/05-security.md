# 05 — Security & integrations

## Screen access matrix

| Screen | Anonymous | AccountManager | Anonymous | Employee | Registered | SalesManager | UserManager |
|---|---|---|---|---|---|---|---|
| MainFlow › AccountManager_SetupQuotas |  |  |  |  |  | ✓ |  |
| MainFlow › Dashboard |  | ✓ |  |  |  | ✓ |  |
| MainFlow › Dashboard_SalesManager |  |  |  |  |  | ✓ |  |
| MainFlow › Opportunities_ListClosed |  | ✓ |  |  |  | ✓ |  |
| MainFlow › Opportunity_Edit |  | ✓ |  |  |  | ✓ |  |
| MainFlow › Opportunity_List |  | ✓ |  |  |  | ✓ |  |
| MainFlow › Opportunity_Show |  | ✓ |  |  |  | ✓ |  |
| ContactsAndCompanies › Company_Edit |  | ✓ |  |  |  | ✓ |  |
| ContactsAndCompanies › Company_FullHistory |  | ✓ |  |  |  | ✓ |  |
| ContactsAndCompanies › Company_List |  | ✓ |  |  |  | ✓ |  |
| ContactsAndCompanies › Company_Show |  | ✓ |  |  |  | ✓ |  |
| ContactsAndCompanies › Contact_Edit |  | ✓ |  |  |  | ✓ |  |
| ContactsAndCompanies › Contact_Import |  | ✓ | ✓ |  | ✓ | ✓ | ✓ |
| ContactsAndCompanies › Contact_List |  | ✓ |  |  |  | ✓ |  |
| ContactsAndCompanies › Contact_Show |  | ✓ |  |  |  | ✓ |  |
| Common › InternalError |  | ✓ | ✓ |  | ✓ | ✓ | ✓ |
| Common › InvalidPermissions |  | ✓ | ✓ |  | ✓ | ✓ | ✓ |
| Common › Login |  | ✓ | ✓ |  | ✓ | ✓ | ✓ |
| Common › MyInfo |  | ✓ |  |  | ✓ | ✓ | ✓ |
| Common › NoPermission |  | ✓ | ✓ |  | ✓ | ✓ | ✓ |
| Common › SearchResults |  | ✓ |  |  | ✓ | ✓ | ✓ |
| MainFlow › Configuration |  |  |  |  |  |  | ✓ |

## Role checks in logic (enforced in actions)

_None found._

## Role-based UI conditions (visibility only, not enforcement)

- `Sales › AccountManager_SetupQuotas`: `CheckRole(Roles.AccountManager, User.Id)=True`
- `Sales › Opportunity_List`: `CheckRole(Roles.AccountManager, User.Id)=True`
- `Sales › Opportunity_List`: `CheckSalesManagerRole.HasRole`
- `Sales › Company_Edit`: `CheckRole(Roles.AccountManager, User.Id)=True`
- `Sales › Company_List`: `CheckRole(Roles.AccountManager, User.Id) = True`

## Service Studio validation messages

| Module | Type | Where | Message |
|---|---|---|---|
| CustomersSampleData | Warning | /SampleData_Bootstrap/ContactRecord | Unused Element — Local Variable 'ContactRecord' is never used in Server Action 'SampleData_Bootstrap'. Consider deleting it. |
| CustomersSampleData | Warning | /SampleData_Bootstrap/CompanyRecord | Unused Element — Local Variable 'CompanyRecord' is never used in Server Action 'SampleData_Bootstrap'. Consider deleting it. |
| Sales | Warning | /Common/SampleUsersLogin/Login/Login | Unexpected Login — Please consider using login actions from the app 'Users'. |
| Sales | Warning | /CompanyName | Unused Element — Site Property 'CompanyName' is never used in Module 'Sales'. Consider deleting it. |
| Sales | Warning | /QuotaChartColor | Unused Element — Site Property 'QuotaChartColor' is never used in Module 'Sales'. Consider deleting it. |
| Customers | Warning | / | Deprecated jQuery Version — jQuery version '1.4.2 OS' is currently being deprecated. Consider upgrading to the latest version, since new features might not work as expected. |
| Employees | Warning | /CreateOrUpdateEmployeeLocationFromAddress_SA/CreateOrUpdateEmployeeLocationFro… | Unused Argument — Unused argument 'APIKey' to non-existing parameter 'APIKey' in 'CreateOrUpdateEmployeeLocationFromAddress' Run Server Action. Consider deleting it. |
| Employees | Warning | /EmployeeCV_Delete/GetEmployeeCVById | Unused Aggregate — 'GetEmployeeCVById' aggregate is never used in EmployeeCV_Delete. Consider deleting it. |
| Employees | Warning | /Employee_Delete/GetUserById | Unused Aggregate — 'GetUserById' aggregate is never used in Employee_Delete. Consider deleting it. |
| Employees | Warning | /EmployeeLocation_Delete/GetEmployeeLocationById | Unused Aggregate — 'GetEmployeeLocationById' aggregate is never used in EmployeeLocation_Delete. Consider deleting it. |
| SalesSampleData | Warning | /ApplicationName | Unused Element — Site Property 'ApplicationName' is never used in Module 'SalesSampleData'. Consider deleting it. |

## Integrations

- `Sales` › external sites (UI redirects): ExternalURL
- `Employees` › **Integrations**: EmployeesAPI
- `SalesSampleData` › external sites (UI redirects): AskOutSystems, Users
- `EmployeesSampleData` › **Integrations**: EmployeesSampleDataAPI

**Referenced modules (dependencies):** (System), BinaryData, Charts, Customers, CustomersSampleData, Employees, EmployeesSampleData, FloorPlan_CS, GoogleMapsTimezone, Google_Maps, HTTPRequestHandler, Locations, OutSystemsUIWeb, PlatformPasswordUtils, ProfileUtils, RichWidgets, Sales, Text, Users, Zip
