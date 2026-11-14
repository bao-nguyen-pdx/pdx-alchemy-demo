# 05 — Security and integrations

## Access control
| Area | Who | Enforced where | Source |
|---|---|---|---|
| All business screens | OrderManager, OrderRepresentative | Screen roles | `getScreen` (Roles) |
| DashboardManager | OrderManager only | Screen roles | `DashboardManager` |
| Save order | Manager or Representative | **Server** | `CS › Order_Save` |
| Change order status (submit, approve, reject) | **Manager only** | **Server** | `CS › Order_Save` |
| Approve/Reject buttons | Shown to **any** role when the order is Submitted | UI only | `OrderDetail › if_OrderIsSubmitted` (see Q3) |
| Delete order | Manager, Draft only | UI (button visibility) | `OrderDetail › ifWidget3` |
| Client role flags | `Client.IsOrderManager`, `Client.IsOrderEntrySpecialist` set at login from `User_GetRoles` | Client variables, used for **UI visibility only** | `UI › SetUserRoles` |

## Validation warnings from Service Studio
| Type | Where | Message | Source |
|---|---|---|---|
| ⚠ Security | `Common › Login › GetSampleUsers › SampleUsers_Get` | Server action exposed for public access without authentication. Consider removing the Anonymous role. | `getValidationMessages` |
| ⚠ Login security | `Login_SampleUser › Login` | "Unexpected Login": consider using the login actions from the Users app | `getValidationMessages` |
| ⚠ Unused | `Common › Menu › ActiveMenu` | Input parameter never used | `getValidationMessages` |

Note: the **sample-user login** (`Login_SampleUser`) logs a user in by **UserId only, with no password**, for any user in the hard-coded sample list. That's fine for a demo app and must not survive into production. (The sample user accounts are referenced by username in `SampleUsers_Get`; they're omitted here on purpose.)

## Data exposure
`getDataExposure` lists every data source that reaches the browser (aggregates, data actions, variables) per entity and attribute. Raw output: `raw/OrderManagement.getDataExposure.txt` (545k) and `raw/OrderManagement_CS.getDataExposure.txt`. These are the inputs for a "what data leaves the server" review. Not summarised yet.

## Integrations
**None.** `getIntegrations` returns no REST or SOAP consumers or exposures in either module. External dependencies are platform components only: OutSystems UI, OutSystems Charts, Users (login), and Zip/Excel utilities for bootstrap.
