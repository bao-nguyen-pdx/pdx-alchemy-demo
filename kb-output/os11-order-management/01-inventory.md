# 01 — Inventory

| Element | OrderManagement (UI) | OrderManagement_CS (core) |
|---|---|---|
| Screens | 15 | 0 |
| Blocks (own / referenced) | 17 / 31 | 0 |
| Entities (server / static) | — (references CS) | 11 / 4 |
| Server actions (own) | 11 | 61 |
| Client actions (own) | 6 | 0 |
| Service actions | 0 | 0 |
| Roles | references CS | `OrderManager`, `OrderRepresentative` (+ system `Anonymous`, `Registered`) |
| Timers | 0 | 1 (`Application_Bootstrap`, runs when published) |
| Site properties | 0 | 6 |
| User exceptions | 0 | 1 (`InvalidArgumentsException`) |
| Integrations (REST/SOAP) | none | none |
| Validation messages | 3 warnings, 0 errors | none |

Sources: `getScreenNames`, `getWebBlockNames`, `getActionNames` (client/server/service), `getDataModel`, `getRoleNames`, `getTimers`, `getSettings`, `getUserExceptions`, `getIntegrations`, `getValidationMessages`.

## Screens by UI flow
| Flow | Screens |
|---|---|
| Common | Login, Splash, InvalidPermissions |
| Dashboard | Dashboard, DashboardManager |
| Account | Accounts, AccountShow, AccountEdit |
| Opportunity | Opportunities, OpportunityDetail |
| Order | Orders, OrderDetail, Invoices |
| Product | Products, ProductDetail |

## Roles
| Role | Duties (from the role description) | Source |
|---|---|---|
| `OrderManager` | Monitor orders made by the order entry specialist; approve and validate orders | `OrderManagement_CS › Roles › OrderManager` |
| `OrderRepresentative` | Create and update sales orders and return orders; monitor fulfilment; work directly with customers | `OrderManagement_CS › Roles › OrderRepresentative` |

## Own UI blocks
`Account_Opportunities`, `Account_OrdersClosed`, `Dashboard_GoalProgress`, `Dashboard_OrdersBySalesManager`, `Dashboard_OrdersBySalesRepresentative`, `Dashboard_TopPerformers`, `Dashboard_TopProducts`, `OrderStatus`, `Menu`, `IconMenu`, `UserInfo`, `ApplicationTitle`, `Footer`, `LayoutTop`, `LayoutTopHeader`, `LayoutBlank`, `ResizeChart`. The rest are OutSystems UI and Charts patterns.

## Core actions (OrderManagement_CS), by entity
| Entity | Actions |
|---|---|
| Account | `_Save`, `_Validate`, `_GenerateCode`, `_Bootstrap`, `_Teardown` |
| Order | `_Save`, `_Validate`, `_GenerateCode`, `_Bootstrap`, `_Teardown`, `_Save_BOOTSTRAP` |
| OrderItem | `_Save`, `_Validate`, `_Save_BOOTSTRAP` |
| Product | `_Save`, `_Validate`, `_Edit`, `_GenerateCode`, `_Bootstrap`, `_Teardown` |
| ProductCategory / Feature / Picture | `_Save`, `_Validate`, `_GenerateCode` / `_Delete`, `_SetIsActive` |
| Opportunity, Country, Brand, Model | `_Save`, `_Validate`, `_Bootstrap`, `_Teardown` |
| Dashboard | `Dashboard_GetByCategory`, `Dashboard_GetHistory` |
| Settings | `SiteProperty_GetAll`, `_GetCurrency`, `_GetDashboard` |
| Application | `Application_Bootstrap`, `Application_Teardown`, `User_Bootstrap` |

## UI-side logic (OrderManagement)
Server: `SaveOrderItemandUpdateStock`, `UpdateOrderAmount`, `UpdateProductStock`, `UpdateStockOnOrderDelete`, `Product_GetAmountAndStock`, `GetCeilingandFloor`, `User_GetRoles`, `DoLogin`, `DoLogout`, `Login_SampleUser`, `SampleUsers_Get`.
Client: `InitClientVars`, `SetUserRoles`, `PrettyFormatCurrency`, and client wrappers for the three stock and amount actions.

## Seed data resources (CS)
`Accounts.xlsx`, `Categories.xlsx`, `Countries.xlsx`, `Opportunities.xlsx`, `Orders.xlsx`, `Products.xlsx`, `ProductPictures.zip`. These are loaded by the `*_Bootstrap` actions.

## Site properties (CS)
| Name | Default | Purpose |
|---|---|---|
| Currency | `€` | Currency symbol |
| DecimalSeparator / GroupSeparator | `.` / `,` | Number formatting |
| Dashboard_TargetValue | 10000 | Target shown on dashboard |
| Dashboard_PersonalGoal | 50000 | Personal goal |
| Dashboard_TeamGoalPercent | 54 | Team share of company goal (%) |
