# 01 — Inventory

Source: `getSerializedObject` (full model) and `getValidationMessages`, per module.

| Element | `CustomersSampleData` | `Sales` | `Customers` | `Employees` | `SalesSampleData` | `EmployeesSampleData` |
|---|---|---|---|---|---|---|
| Screens | 0 | 21 | 0 | 0 | 1 | 0 |
| Blocks | 0 | 11 | 0 | 0 | 0 | 0 |
| UI flows | 0 | 4 | 0 | 0 | 1 | 0 |
| Entities (server / static) | 0 / 0 | 5 / 3 | 5 / 3 | 9 / 2 | 0 / 0 | 0 / 0 |
| Structures | 2 | 3 | 0 | 2 | 5 | 8 |
| Server actions | 10 | 12 | 17 | 61 | 12 | 32 |
| Client actions | 0 | 0 | 0 | 0 | 0 | 0 |
| Service actions | 0 | 0 | 13 | 51 | 0 | 0 |
| Roles | 0 | 2 | 0 | 1 | 0 | 0 |
| Timers | 1 | 0 | 0 | 0 | 3 | 2 |
| Site properties | 2 | 7 | 2 | 8 | 5 | 5 |
| User exceptions | 0 | 1 | 0 | 1 | 1 | 0 |
| Resources | 2 | 1 | 0 | 0 | 5 | 3 |
| Scripts | 0 | 0 | 0 | 0 | 0 | 0 |
| Validation warnings / errors | 2 / 0 | 3 / 0 | 1 / 0 | 4 / 0 | 1 / 0 | 0 / 0 |

## `CustomersSampleData` (Service)
> Customers sample data for the CRM applications. Version 8.0.1.3
**References:** `(System)`, `Customers`, `HTTPRequestHandler`, `Users`

| Server action folder | Actions |
|---|---|
| (no folder) | CompanyType_GetIdFromLabel, Company_GetIdFromName, Company_ImportFromExcel, Contact_GetIdFromName, Contact_ImportFromExcel, DownloadSampleCompanies, DownloadSampleContacts, SampleData_Bootstrap, SampleData_BootstrapDone, SampleData_Delete |

| Timer | Action | Schedule |
|---|---|---|
| SampleData_Bootstrap | SampleData_Bootstrap | WhenPublished |

| Site property | Type | Default |
|---|---|---|
| TenantId | Tenant Identifier |  |
| TenantName | Text |  |

**Resources:** `SampleCompanies.xlsx`, `SampleContacts.xlsx`

## `Sales` (Traditional)
> Help your team keep track of their opportunities and get an accurate sales forecast.
**References:** `(System)`, `Charts`, `Customers`, `Employees`, `HTTPRequestHandler`, `OutSystemsUIWeb`, `PlatformPasswordUtils`, `RichWidgets`, `Users`

| UI flow | Screens | Blocks |
|---|---|---|
| MainFlow | AccountManager_SetupQuotas, Dashboard, Dashboard_SalesManager, Opportunities_ListClosed, Opportunity_Edit, Opportunity_List, Opportunity_Show | EmployeeInfo, NoteTaskHistory, QuarterStatus |
| ContactsAndCompanies | Company_Edit, Company_FullHistory, Company_List, Company_Show, Contact_Edit, Contact_Import, Contact_List, Contact_Show | — |
| Common | InternalError, InvalidPermissions, Login, MyInfo, NoPermission, SearchResults | ApplicationTitle, Footer, Layout, LoginInfo, Menu, MenuIcon, SampleUsersLogin |
| Widgets | — | InfiniteScroll |

**Roles:** `AccountManager`, `SalesManager`

| Server action folder | Actions |
|---|---|
| (no folder) | CheckPermissionsForCompany, CheckSuperUserRole, DashboardByAccountManager, DashboardForecast, DashboardQuarterStatus, FormatAddress, FormatDate, GetCurrentAccountManager, GetQuarterEnd, NoteOrTaskHistory, OpportunityHistoryLog, Opportunity_Create |

| Site property | Type | Default |
|---|---|---|
| CompanyName | Text | "OutSystems" |
| Currency | Text | "$" |
| DecimalSeparator | Text | "." |
| GroupSeparator | Text | "," |
| QuotaChartColor | Text | "94A176" |
| TenantId | Tenant Identifier |  |
| TenantName | Text |  |

**Resources:** `SampleContacts.xlsx`

## `Customers` (Service)
> Master entities for CRM applications (Sales, Campaigns, Cases...). Version 8.0.1.3
**References:** `(System)`

| Server action folder | Actions |
|---|---|
| (no folder) | Company_Create, Company_CreateHistory, Company_CreateOrUpdate, Company_Delete, Company_DeleteAllCompanies, Company_DeleteHistory, Company_New, Company_Update, Contact_Create, Contact_CreateHistory, Contact_CreateOrUpdate, Contact_CreateOrUpdatePicture, Contact_Delete, Contact_DeleteAllContacts, Contact_DeleteHistory, Contact_New, Contact_Update |

| Site property | Type | Default |
|---|---|---|
| TenantId | Tenant Identifier |  |
| TenantName | Text |  |

## `Employees` (Service)
> Master entities for Intranet applications (Directory, Documents, Expenses...)
**References:** `(System)`, `FloorPlan_CS`, `Google_Maps`, `GoogleMapsTimezone`, `Locations`, `RichWidgets`, `Users`

**Roles:** `Employee`

| Server action folder | Actions |
|---|---|
| Api | EmployeesAvailable, EmployeesVersion |
| Company | CompanyName_Get |
| Employee | Employee_Create, Employee_CreateOrUpdate, Employee_Delete, Employee_GetByPictureName, Employee_GetResponsible, Employee_New, Employee_Update, Employees_GetLastModifiedDateTime, Employees_MarkUpdate |
| EmployeeCV | EmployeeCV_Create, EmployeeCV_CreateOrUpdate, EmployeeCV_Delete, EmployeeCV_New, EmployeeCV_Update |
| EmployeeLocation | CreateOrUpdateEmployeeLocationFromAddress, EmployeeLocation_Create, EmployeeLocation_CreateOrUpdate, EmployeeLocation_Delete, EmployeeLocation_New, EmployeeLocation_Update |
| EmployeePicture | EmployeePicture_Create, EmployeePicture_CreateOrUpdate, EmployeePicture_Delete, EmployeePicture_New, EmployeePicture_Update, EmployeePictures_GetLastModifiedDateTime, EmployeePictures_MarkUpdate |
| EmployeeThumbnail | EmployeeThumbnail_Create, EmployeeThumbnail_CreateOrUpdate, EmployeeThumbnail_Delete, EmployeeThumbnail_GetLastModifiedDateTime, EmployeeThumbnail_MarkUpdate, EmployeeThumbnail_New, EmployeeThumbnail_Update |
| EmployeeUser | EmployeeUser_CreateOrUpdate |
| JobTitle | JobTitle_Create, JobTitle_CreateOrUpdate, JobTitle_Delete, JobTitle_New, JobTitle_Update |
| OfficeLocation | CheckOfficeLocationForValidLocation, CreateOfficeLocationWithEntities, OfficeLocation_Create, OfficeLocation_CreateOrUpdate, OfficeLocation_Delete, OfficeLocation_New, OfficeLocation_Update, UpdateOfficeLocationWithEntities |
| Organization | OrganizationUnit_Create, OrganizationUnit_CreateOrUpdate, OrganizationUnit_Delete, OrganizationUnit_New, OrganizationUnit_Update |
| Project | Project_Create, Project_CreateOrUpdate, Project_Delete, Project_New, Project_Update |

| Site property | Type | Default |
|---|---|---|
| CompanyName | Text | "InSolutions" |
| EmployeePicturesLastModified | Date Time |  |
| EmployeesLastModified | Date Time |  |
| EmployeeThumbnailsLastModified | Date Time |  |
| OrganizationUnit_HierarchicalNameSeparator | Text | " - " |
| ServerAPIKeyGoogleMaps | Text |  |
| TenantId | Tenant Identifier |  |
| TenantName | Text |  |

## `SalesSampleData` (Traditional)
> Sample data for the Sales application.
**References:** `(System)`, `Customers`, `CustomersSampleData`, `EmployeesSampleData`, `OutSystemsUIWeb`, `PlatformPasswordUtils`, `RichWidgets`, `Sales`, `Users`

| UI flow | Screens | Blocks |
|---|---|---|
| MainFlow | Configuration | — |

| Server action folder | Actions |
|---|---|
| (no folder) | CompanyAccountManager_ImportExcel, OpportunityStage_GetIdFromLabel, Opportunity_GetIdFromName, Opportunity_ImportFromExcel, Opportunity_ImportFromExcel_ShiftDate, User_GrantPermissionByName |
| SampleData | SampleData_Bootstrap, SampleData_BootstrapDone, SampleData_BootstrapUsers, SampleData_Delete, SampleData_DeleteBootstrap, SampleData_DeleteCRMCore |

| Timer | Action | Schedule |
|---|---|---|
| SampleData_BootstrapOnPublish | SampleData_Bootstrap | WhenPublished |
| SampleData_Delete | SampleData_Delete | None |
| SampleData_DeleteBootstrap | SampleData_DeleteBootstrap | None |

| Site property | Type | Default |
|---|---|---|
| ApplicationName | Text | "Sales Sample Data" |
| BootstrapFeedback | Text |  |
| TenantId | Tenant Identifier |  |
| TenantName | Text |  |
| ZeroDate | Date | #2011-01-01# |

**Resources:** `SampleCompanyAccountManager.xlsx`, `SampleOpportunities.xlsx`, `SampleQuarterQuotas.xlsx`, `SampleSalesUsers.xlsx`, `SampleTasks.xlsx`

## `EmployeesSampleData` (Service)
> Sample data for Intranet applications.
**References:** `(System)`, `BinaryData`, `Employees`, `FloorPlan_CS`, `GoogleMapsTimezone`, `HTTPRequestHandler`, `Locations`, `ProfileUtils`, `Text`, `Users`, `Zip`

| Server action folder | Actions |
|---|---|
| APIutils | BPT_Bootstrap, BPT_Delete, CheckForData |
| Bootstrap | SampleData_BoostrapAdjustedDates, SampleData_BoostrapEmployeeData, SampleData_BoostrapEmployees, SampleData_BoostrapOrganizationUnits, SampleData_BoostrapProjects, SampleData_Bootstrap, SampleData_BootstrapEmployeeLocations, SampleData_BootstrapEmployeePictures, SampleData_BootstrapEmployeePlans, SampleData_BootstrapEmployeesReportsTo, SampleData_BootstrapExtraEmployeeData, SampleData_BootstrapJobTitle, SampleData_BootstrapOfficeLocation, SampleData_BootstrapOnPublish, SampleData_BootstrapSecondaryEntities, SampleData_UpdateEmployeeDataFromExcel, UpdateBootstrapStatus |
| Delete | SampleData_Delete, SampleData_DeleteAllEmployees, SampleData_DeleteAllJobTitles, SampleData_DeleteEmployeePictures, SampleData_DeleteEmployeesCVLocation, SampleData_DeleteOfficeLocations |
| Utils | Employee_GetIdFromName, GetBootstrapFeedback, GetNextBirthdays, GetSampleData, OrganizationUnit_GetIdFromName, User_GetIdbyEmail |

| Timer | Action | Schedule |
|---|---|---|
| SampleData_BootstrapEmployeePictures | SampleData_BootstrapEmployeePictures | None |
| SampleData_BootstrapOnPublish | SampleData_BootstrapOnPublish | WhenPublished |

| Site property | Type | Default |
|---|---|---|
| BootstrapFeedback | Text |  |
| DoBootstrapOnPublish | Boolean | True |
| TenantId | Tenant Identifier |  |
| TenantName | Text |  |
| ZeroDate | Date | #2011-01-01# |

**Resources:** `EmployeeSampleData.xlsx`, `OfficeLocationPlanImages.zip`, `Photos.zip`
