# 06 — Open questions and findings (for SME confirmation)

These are the things a converter would carry across silently. In a rebuild, each one needs a **decision**: keep, fix or retire.

| # | Finding | Evidence | Question for the SME | Severity |
|---|---|---|---|---|
| **Q1** | **Three formulas for "order amount":** (a) item amount `P×Q − P×Q×D/100` at save time; (b) order total recalculated in SQL with the **current** product price; (c) the dashboard uses `P × Q × (100 − D/100)`, an operator-precedence bug that inflates amounts by about 100×. | `CS › OrderItem_Save`; `UI › UpdateOrderAmount` (SQL); `CS › Dashboard_GetByCategory` | Which is the source of truth? Should the total use the price **at order time** or the current price? | 🔴 Defect (c); 🟠 ambiguity (a vs b) |
| **Q2** | `Account_Validate` messages don't match their checks: the "Company name must be unique" check tests **Code** uniqueness, and "Code is required" fires when **Contact** is empty. Company-name uniqueness isn't checked at all. | `CS › Account_Validate` | Should company names be unique? Confirm the intended messages. | 🟠 Defect |
| **Q3** | The **Approve/Reject buttons show for any role**, but the server only lets managers change status. A Representative who clicks gets a permission error. The same server rule also blocks a **Representative from submitting** (Draft → Submitted is a status change). | `UI › OrderDetail`; `CS › Order_Save` | Can Representatives submit orders? Confirm at runtime. Should the buttons be hidden for non-managers? | 🟠 Needs runtime check |
| **Q4** | The order-history chart is hard-coded to **1 Jan – 31 Dec 2020**. | `CS › Dashboard_GetHistory` (arguments `NewDate(2020,1,1)`) | Should it be a rolling 12 months? | 🟡 |
| **Q5** | Codes are `ORD`/`A`/`P` + 4 digits, based on the highest code + 1. This breaks after 9,999, and there's no concurrency guard, so two users saving at once can get the same code. | `CS › *_GenerateCode` | Expected volumes? Is uniqueness enforced elsewhere? | 🟡 |
| **Q6** | **Every publish wipes and reseeds all data** (`Application_Bootstrap` timer, `WhenPublished`, Teardown first). | `CS › Timers` | Sample-only behaviour. Confirm it's excluded from any real environment. | 🔴 if real data |
| **Q7** | Sample-user login works with **no password** and is exposed to Anonymous (Service Studio security warning). | `UI › Login_SampleUser`, validation messages | Remove for production? | 🔴 if real users |
| **Q8** | Naming drift: the role is `OrderRepresentative`, the client flag is `IsOrderEntrySpecialist`, and the role description says "Order entry specialist". | `CS › Roles`, `UI › SetUserRoles` | Pick one term for the rebuild. | 🟢 |
| **Q9** | ProductDetail checks Price `> 0` in one place and `>= 0` in another. The server allows 0. | `UI › ProductDetail`, `CS › Product_Validate` | Can products be free? | 🟢 |
| **Q10** | The UI uses `DEPRECATED_WizardV2`/`TabsV2` blocks. There are also typos in user-facing text ("succefully", "leaser or equal"). | `UI › OrderDetail`, `CS › Opportunity_Validate` | Cosmetic. Fix in the rebuild. | 🟢 |

**Not yet covered by this extraction** (next pass): block internals (`Dashboard_*`, `OrderStatus`), the `Dashboard_GetHistory` SQL text, the `getDataExposure` review, and the `Order_Bootstrap` and other `*_Bootstrap` mappings from the Excel resources.
