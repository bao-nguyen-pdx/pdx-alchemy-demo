# 04 — Business rules

Each rule is traced to the action that enforces it. Error text is quoted exactly. All validation errors raise `InvalidArgumentsException`.
Raw logic: `raw/logic/*.txt`. Prefix `CS` = OrderManagement_CS, `UI` = OrderManagement.

## Orders
| ID | Rule | Source |
|---|---|---|
| R-ORD-01 | Only **OrderManager** or **OrderRepresentative** may save an order. Otherwise: "You don't have permissions to execute this action." | `CS › Order_Save` |
| R-ORD-02 | A **new order** always starts as **Draft** and gets a generated code. | `CS › Order_Save` |
| R-ORD-03 | Order codes are `ORD` + 4 digits (`ORD0001`, `ORD0002`, …): the highest existing code + 1. | `CS › Order_GenerateCode` |
| R-ORD-04 | **Only an OrderManager may change an order's status.** Saving with a different status as a non-manager gives "You don't have permissions…" | `CS › Order_Save` (`OrderStatusId = previous or CheckOrderManagerRole()`) |
| R-ORD-05 | Total amount must be ≥ 0: "Total amount must be greater or equal to zero". | `CS › Order_Validate` |
| R-ORD-06 | Status is required: "Status is required". | `CS › Order_Validate` |
| R-ORD-07 | On save, `TotalAmount` = **sum of active order item amounts**. | `CS › Order_Save` (aggregate `AmountSum`) |
| R-ORD-08 | `UpdateOrderAmount` also recalculates the total, in SQL: `SUM(Price × Quantity × ((100 − Discount)/100))` using the **current product price**. | `UI › UpdateOrderAmount` (SQL `TotalAmount`) |
| R-ORD-09 | Lifecycle: Draft → Submitted (needs ≥ 1 item) → Closed (approve) or Rejected (reject). Rejected orders are editable again. | `UI › OrderDetail` |
| R-ORD-10 | Orders can be edited only when new, Draft or Rejected. | `UI › OrderDetail › isEditable` |
| R-ORD-11 | Only managers can delete an order, and only in Draft. Deleting returns all item quantities to stock. | `UI › OrderDetail`, `UI › UpdateStockOnOrderDelete` |
| R-ORD-12 | Closed orders appear on **Invoices**. | `UI › Invoices › GetOrdersByEmployeeId` filter |

## Order items and stock
| ID | Rule | Source |
|---|---|---|
| R-ITM-01 | Order is required: "Order is required". | `CS › OrderItem_Validate` |
| R-ITM-02 | Product is required: "Product is required". | `CS › OrderItem_Validate` |
| R-ITM-03 | Quantity > 0: "Quantity must be greater than zero". | `CS › OrderItem_Validate` |
| R-ITM-04 | Discount ≥ 0: "Discount must be greater or equal to zero". | `CS › OrderItem_Validate` |
| R-ITM-05 | **Stock check (new item):** product stock must be ≥ quantity, else "Insufficient stock to fulfill your order". | `CS › OrderItem_Validate` (`GetProductById2`) |
| R-ITM-06 | **Stock check (edited item):** (stock + quantity already reserved by this item) must be ≥ the new quantity. | `CS › OrderItem_Validate` (`FinalStock`) |
| R-ITM-07 | Item amount = `Price × Qty − Price × Qty × Discount/100`, using the product's price at save time. | `CS › OrderItem_Save` |
| R-ITM-08 | Saving an item re-saves its parent order, so the total is recalculated (R-ORD-07). | `CS › OrderItem_Save` |
| R-ITM-09 | **Stock is reserved when an item is saved:** `stock += (old qty − new qty)`. | `UI › SaveOrderItemandUpdateStock` → `UpdateProductStock` |
| R-ITM-10 | Only active products can be ordered. | `CS › OrderItem_Save/Validate`, `UI › OrderDetail › GetProducts` |

## Products
| ID | Rule | Source |
|---|---|---|
| R-PRD-01 | Name required; Category required; Price ≥ 0; Stock ≥ 0 (exact messages in source). | `CS › Product_Validate` |
| R-PRD-02 | Product codes: `P` + 4 digits. | `CS › Product_GenerateCode` |
| R-PRD-03 | Product, pictures and features are saved together; deleted pictures and features are removed in the same call. | `CS › Product_Edit` |
| R-PRD-04 | UI requires a cover image; Discount 0–100; max 4 new features and 6 new images per edit. | `UI › ProductDetail` |
| R-PRD-05 | Feature: ProductId and Description required, Description ≤ 100 chars. | `CS › ProductFeature_Validate` |
| R-PRD-06 | Picture: Filename and content required. | `CS › ProductPicture_Validate` |
| R-PRD-07 | Category name required; Brand name required; Model name and brand required. | `CS › ProductCategory/Brand/Model_Validate` |

## Accounts, opportunities, countries
| ID | Rule | Source |
|---|---|---|
| R-ACC-01 | Company name required: "Company name is required". | `CS › Account_Validate` |
| R-ACC-02 | **Account code must be unique.** The message says "Company name must be unique" (see Q2). | `CS › Account_Validate` |
| R-ACC-03 | **Contact is required.** The message says "Code is required" (see Q2). | `CS › Account_Validate` |
| R-ACC-04 | Account codes: `A` + 4 digits. Text fields are trimmed on save. | `CS › Account_GenerateCode`, `Account_Save` |
| R-OPP-01 | Name required and unique; Type and Stage required. | `CS › Opportunity_Validate` |
| R-OPP-02 | Amount ≥ 0; Probability 0–100; Expected revenue ≥ 0. | `CS › Opportunity_Validate` |
| R-CTY-01 | Country code required and unique; name required. | `CS › Country_Validate` |

## Audit and soft delete (all core `_Save` actions)
| ID | Rule | Source |
|---|---|---|
| R-AUD-01 | Create: `CreatedBy = Invoker`, `CreatedOn = now`, `IsActive = True`. Update: `UpdatedBy`, `UpdatedOn`. | `CS › *_Save` |
| R-AUD-02 | Lists only show records with `IsActive = True` (soft delete). | screen aggregates |

## Dashboards and settings
| ID | Rule | Source |
|---|---|---|
| R-DSH-01 | Sales by category = top **5** categories by amount. | `CS › Dashboard_GetByCategory` |
| R-DSH-02 | Order history = **Closed** orders by month, **for 2020 only** (hard-coded, see Q4). | `CS › Dashboard_GetHistory` |
| R-DSH-03 | Goals come from site properties (target 10,000; personal 50,000; team 54 %). | `CS › Settings`, `UI › InitClientVars` |
| R-DSH-04 | Currency is shown compactly: < 1,000 as is, then "k", then "M". | `UI › PrettyFormatCurrency` |

## Data lifecycle
| ID | Rule | Source |
|---|---|---|
| R-SYS-01 | **On every publish**, timer `Application_Bootstrap` runs `Application_Teardown` and then reseeds users, countries, categories, brands, models, products, accounts, opportunities and orders from the Excel resources. | `CS › Timers › Application_Bootstrap` (`WhenPublished`) |
