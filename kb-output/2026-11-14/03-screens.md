# 03 — Screens (OrderManagement)

Source: `OrderManagement › getScreen <name>` for all 15 screens. The decoded code is in `raw/screens/*.cs`.
All business screens are restricted to **OrderManager + OrderRepresentative** unless noted.

| Screen | Purpose | Data (aggregates / data actions) | Key actions & server calls |
|---|---|---|---|
| **Login** | Sign in, or pick a sample user | `GetSampleUsers` | `DoLogin`, `LoginWithSampleUser` → `Login_SampleUser`; `InitClientVars` |
| **Splash** | Loading / upgrade progress | — | `Navigate`, `OnComplete` |
| **InvalidPermissions** | Shown when access is denied | — | — |
| **Dashboard** | Sales KPIs for the current user | `GetUser`; `GetDashboardByCategory`, `GetDashboardHistory` | Manager-only sections when `Client.IsOrderManager` |
| **DashboardManager** | Manager dashboard | same as Dashboard | **OrderManager only** |
| **Accounts** | List and filter customer companies | `GetAccounts` (joins Country) | `OnChange_Filters`, `OnSort` |
| **AccountShow** | Read-only account detail | `GetAccountById` | — |
| **AccountEdit** | Create or edit an account | `GetAccount`, `GetCountry` | `OnSave` → `Account_GenerateCode` (new), `Account_Save`; errors shown from the exception message |
| **Opportunities** | Opportunity pipeline list | `GetOpportunities` | filter by stage (default **Prospecting**), sort |
| **OpportunityDetail** | One opportunity and its orders | `GetOpportunity`, `GetOrdersByOpportunityId` | special state when stage = **ClosedWon** |
| **Orders** | Search and filter all orders | `GetOrdersByEmployeeId`, `GetMinMax`; `GetEmployees` | filter by status tab, employee, keyword (account code/company/order code), **amount range slider** (`GetCeilingandFloor`), pagination |
| **OrderDetail** ★ | Create, edit, submit, approve or reject an order and its items | `GetOrder`, `GetOrderItemsByOrderId`, `GetProducts`, `GetAccounts` | `Order_Save`, `Order_SetStatus`, `Order_Delete`, `Product_Save` (item) → `SaveOrderItemandUpdateStock`, `UpdateOrderAmount`, `UpdateStockOnOrderDelete`, `Product_GetAmountAndStock` |
| **Invoices** | Closed orders (invoiced) by employee | `GetOrdersByEmployeeId` (filter **Status = Closed**) | sort, paginate, employee dropdown |
| **Products** | Product catalogue (gallery) | `GetProductsByProductName`, `GetCategories`, `GetProductSorts` | search by name, category filter, sort, "load more" |
| **ProductDetail** | View or edit a product, its features and images | `GetProductInfo`, `GetModelsByBrand` | `Product_OnSave` → `Product_Edit` (product + pictures + features in one call) |

## OrderDetail: the core screen (detail)
- **Editable** only when new, **Draft** or **Rejected**. Source: `OrderDetail › isEditable` condition.
- **Wizard:** Draft → Submitted → Closed, shown with the `DEPRECATED_WizardV2` block.
- **Submit** shows when status is Draft/Rejected **and the order has at least one item**. It sets status Submitted. Source: `OrderDetail › draftStatus`.
- **Approve / Reject** show when status is Submitted, and set Closed or Rejected. Source: `OrderDetail › if_OrderIsSubmitted`, `button4`.
- **Delete order** shows only for **managers** on **Draft** orders. Source: `OrderDetail › ifWidget3`.
- Messages: "The order was successfully saved.", "The order status was changed.", "Order deleted succefully" (sic).

## ProductDetail: UI validations
Price ≥ 0 (and > 0 in one check), Discount 0–100, Stock ≥ 0, Category required, at least one cover image ("Please upload at least one image for the cover of the product"), up to 4 new features and up to 6 new images per edit. Source: `ProductDetail` conditions and messages.
