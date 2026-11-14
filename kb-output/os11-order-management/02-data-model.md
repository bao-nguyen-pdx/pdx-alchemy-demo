# 02 — Data model (OrderManagement_CS)

Source: `OrderManagement_CS › getDataModel` (93k chars of Model API code, summarised here; per-attribute detail in `raw/_datamodel_summary.txt`).
Conventions used by most entities: `IsActive` (soft delete), audit fields `CreatedBy/On`, `UpdatedBy/On` (→ `User`), unique business `Code`.

## Relationships
```
Country ◄── Account ◄── Opportunity ──► OpportunityType (static)
              ▲             │      └──► OpportunityStage (static)
              │             ▼
              └──────── Order ──► OrderStatus (static)
                          ▲
                      OrderItem ──► Product ──► ProductCategory
                                      │   └──► Model ──► Brand
                                      ├── ProductFeature   (delete cascades)
                                      └── ProductPicture   (delete cascades)
```
Delete rules: `Protect` on all business foreign keys; `Delete` (cascade) for `ProductFeature.ProductId` and `ProductPicture.ProductId`; `Ignore` on audit fields of Brand/Model/Feature/Picture.

## Entities
| Entity | Purpose | Key attributes (* = mandatory) |
|---|---|---|
| **Account** | A customer company | Code* (10), Company* (100), Contact* (100), CountryId → Country, Address, City, PostalCode, Phone, Email |
| **Country** | Country lookup | Code* (50), Name* (100) |
| **Opportunity** | Sales opportunity for an account | Name* (100), AccountId* → Account, OpportunityTypeId*, OpportunityStageId*, Description (1500), Amount, Probability, ExpectedRevenue, CloseOn |
| **Order** | Sales order | Code* (10), AccountId* → Account, OpportunityId → Opportunity, OrderStatusId*, TotalAmount ("sum of order items"), ExpectedClosedOn, RequiredOn, ShippedOn |
| **OrderItem** | A product line on an order | OrderId* → Order, ProductId* → Product, Quantity*, Discount (%), Amount |
| **Product** | Sellable product | Code* (10), Name* (250), CategoryId* → ProductCategory, ModelId → Model, Price*, Discount (%), Stock, Reference, EAN, Weight, Dimensions, Description (1000) |
| **ProductCategory** | Product category | Code (10), Name* (50), Description |
| **ProductFeature** | Bullet features of a product | ProductId* → Product (cascade), Description* (100) |
| **ProductPicture** | Product images | ProductId* → Product (cascade), Filename, Filetype, File (binary), IsCover |
| **Brand** | Product brand | Name* (50) |
| **Model** | Product model | Name* (50), BrandId* → Brand |

## Static entities (fixed reference data)
| Static entity | Records |
|---|---|
| **OrderStatus** | Draft, Submitted, Closed, Rejected (+ icon and colour per status) |
| **OpportunityStage** | Prospecting, Investigation, ProposalMade, Negotiation, ClosedWon, ClosedLost |
| **OpportunityType** | NewBusiness ("New business"), ExistingBusiness ("Existing business") |
| **ProductSort** | Name, CreatedOn, Stock (sort options on the Products screen) |

## Structures (CS)
Excel import shapes (`AccountExcel`, `OrderExcel`, `ProductExcel`, … ×9) for bootstrap, plus `OrderHistory` (month, amount) for the dashboard. Source: `getStructures`.

## Observations
- `Order.TotalAmount` is **stored** (denormalised) and recalculated by two different actions. See 04 › R-ORD-07 and 06 › Q1.
