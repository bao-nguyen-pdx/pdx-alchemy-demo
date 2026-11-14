# Knowledge Base — Order Management (OutSystems 11)

> Machine-extracted with the O11 MCP (`servicestudio`, read-only GA tools) on **2026-11-14** from the modules open in Service Studio.
> Every fact cites its source as `Module › element`. The raw tool output is in [`raw/`](raw/).
> **Nothing was written to the module.** No edit session was started, and no Beta write tools were used.

## The application in one paragraph
Order Management is OutSystems' sample **Reactive Web** app for running the order lifecycle: sales representatives record **accounts** (customer companies), **opportunities** and **orders** made up of **products**. An order moves **Draft → Submitted → Closed** (or **Rejected → back to editable**), stock is reserved as items are added, and a manager approves or rejects submitted orders. Dashboards show sales by category and month against configurable goals.

## Modules
| Module | Type | Role | Source |
|---|---|---|---|
| `OrderManagement` | Reactive | UI: 15 screens, 6 flows, 17 own blocks, UI-side logic | `listApps` |
| `OrderManagement_CS` | Reactive | Core services: 15 entities, CRUD/validation actions, bootstrap data, site properties | `listApps` |

## Contents
| File | What's in it |
|---|---|
| [01-inventory.md](01-inventory.md) | Counts, flows, screens, roles, actions, blocks, resources |
| [02-data-model.md](02-data-model.md) | 15 entities, attributes, relationships, static data |
| [03-screens.md](03-screens.md) | Per-screen purpose, roles, data and actions |
| [04-business-rules.md](04-business-rules.md) | **43 business rules**, each traced to its action |
| [05-security-and-integrations.md](05-security-and-integrations.md) | Roles and permissions, validation warnings, integrations (none) |
| [06-open-questions.md](06-open-questions.md) | **10 findings or questions for an SME**, including real defects |

## Headline findings (useful for the demo)
1. **Three different formulas for the same "order amount"**, and one of them is wrong (`Dashboard_GetByCategory` inflates discounted amounts). This is a source-of-truth reconciliation example. → Q1
2. **Validation messages don't match their checks** in `Account_Validate`: the "Company name must be unique" check actually tests the *Code*. → Q2
3. **UI vs server role mismatch:** the Approve/Reject buttons show for any role, but the server only allows managers to change status. → Q3
4. **The dashboard history is hard-coded to 2020.** → Q4
5. **Every publish wipes and reseeds all data** (timer `Application_Bootstrap` runs Teardown first). This is handy for demo resets, and dangerous in a real environment. → Q6
