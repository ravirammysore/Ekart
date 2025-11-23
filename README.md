```markdown
# EKart – Learning SOLID Principles Step by Step (Student Guide)

Welcome! 👋  
This project is designed to help you **learn the SOLID principles** in a very practical way.

Instead of reading theory alone, you will **see the project evolve step-by-step**, from a messy working version to a clean and well-designed solution.

Every stage of the project is stored as a commit in this repository so you can **check out each stage** and explore the code exactly as it was taught.

---

## 🎯 What You Will Learn

By the end of this exercise, you will understand:

- How a working but poorly designed system looks
- How to refactor code **without changing functionality**
- How to apply:
  - **SRP** – Single Responsibility Principle  
  - **OCP** – Open/Closed Principle  
  - **LSP** - Liskov Substitution Principle
  - **ISP** – Interface Segregation Principle  
  - **DIP** – Dependency Inversion Principle  
- How to separate:
  - Domain models  
  - Business logic  
  - Persistence  
  - Application wiring  
- How good design improves readability and changeability

---

## 📂 Project Layout (Final Stage)

After all refactoring stages, the structure looks like this:

```text
Ekart.sln
│
├── EKartBL           // Business Layer (Class Library)
│   ├── Domain       // Entities like Product, Customer, Order
│   ├── Pricing      // Tax rules, discount rules, order calculator
│   ├── Checkout     // Checkout service, payment, invoice
│   └── Persistence  // Repository and Logger interfaces + implementations
│
└── EkartApp          // Console App (Composition Root)
    └── Program.cs    // Wires everything together and runs the demo

```

This structure makes it easy to see different responsibilities in the software.

----------

## 🔄 How to Explore Each Stage

Every stage is available as a **separate commit** in the repository.

You can view or check out any stage.

### ✔ Option 1 — Easiest (Recommended): Use GitHub Web

Open your browser →  
Go to the repository →  
Click **Commits** →  
Click on any commit →  
**Browse** the code in that stage.

No Git installation needed.

----------

### ✔ Option 2 — Use Git From Command Line (students who know Git)

Make sure Git is installed on your system:  
[https://git-scm.com/downloads](https://git-scm.com/downloads)

Then run:

```bash
git clone https://github.com/ravirammysore/Ekart.git
cd Ekart

```

To view all commits:

```bash
git log --oneline

```

To check out a specific stage such as Stage 3:

```bash
git checkout <commit-hash>

```

Example:

```bash
git checkout 1f3c9ab   // just an example hash

```

Now your folder shows **exactly what the code looked like at Stage 3**.


----------

## 🧪 Stages of the Project (Learning Guide)

Each stage brings a new improvement.

----------

### **Stage 0 — Ugly but Working Code**

You start with:

-   One project
    
-   Everything in one file
    
-   Hard-coded logic
    
-   No design principles applied
    

But it works. This is your baseline.

----------

### **Stage 1 — Add a Business Layer (EKartBL)**

Move classes into a class library.  
Behaviour is still the same.

Students learn:  
✔ Project structuring  
✔ Separation of concerns at project level

----------

### **Stage 2 — SRP: Single Responsibility Principle**

Split the big `CheckoutService` into:

-   `OrderCalculator`
    
-   `InvoicePrinter`
    
-   `PaymentProcessor`
    
-   `CheckoutService` (coordinator)
    

Students learn:  
✔ Why small focused classes are easier to maintain  
✔ How SRP makes testing easier

----------

### **Stage 3 — OCP: Strategy Pattern for Tax & Discount**

Replace hard-coded if/else with:

-   `ITaxCalculator`
    
-   `IDiscountPolicy`
    

Students learn:  
✔ Adding new behaviour without modifying old code  
✔ Strategy pattern in real life

----------

### **Stage 4 — Refactor Structure into Layers**

Re-organize code into folders:

-   Domain
    
-   Pricing
    
-   Checkout
    
-   Persistence
    

Students learn:  
✔ How real projects organise architecture  
✔ Cleaner navigation and maintainability

----------

### **Stage 5 — ISP Violation: Fat Repository Interface**

Introduce a deliberately **bad**, fat interface:

```csharp
IEkartRepository

```

with unrelated methods like logging & exporting mixed together.

Students learn:  
✔ What _not_ to do  
✔ How interfaces can become “junk drawers”

----------

### **Stage 6 — ISP Fix: Separate Repository and Logger**

Clean it up:

-   `IEkartRepository` = pure data access
    
-   `ILogger` = logging
    
-   `ConsoleLogger` = implementation
    
-   `EkartRepository` = no logger responsibilities
    

Students learn:  
✔ Interface Segregation Principle  
✔ Why keeping responsibilities separate matters

----------

### **Stage 7 — DIP: Manual Dependency Injection**

Move **all** `new` statements into `Program.cs`:

-   Tax calculator
    
-   Discount policy
    
-   Order calculator
    
-   Repository
    
-   Logger
    
-   Payment processor
    
-   Invoice printer
    
-   Checkout service
    

Students learn:  
✔ What dependency inversion means  
✔ Why high-level code should use abstractions  
✔ How to inject dependencies manually  
✔ How real applications wire things at one place (composition root)


## 📌 A Simple Note About Liskov Substitution Principle (LSP)

The Liskov Substitution Principle (LSP) says:

> **If a class implements an interface or inherits from another class,  
> it should work perfectly wherever the parent type is expected.**

In very simple words:

> **A child class should not break the system when it is used in place of its parent.**

### ⭐ Try this small experiment (optional)

In this project we have:

```csharp
public interface IDiscountPolicy
{
    decimal CalculateDiscountPercentage(Order order);
}

```

Normally, discount values should be between **0% and 100%**.

#### Step 1: Create a bad policy

```csharp
public class BadDiscountPolicy : IDiscountPolicy
{
    public decimal CalculateDiscountPercentage(Order order)
    {
        return 200; // 200% discount - not valid
    }
}

```

#### Step 2: Use it in Program.cs

```csharp
IDiscountPolicy discountPolicy = new BadDiscountPolicy();

```

#### Step 3: Run the program

You will see strange or ***negative results***.  
This shows that **BadDiscountPolicy** cannot safely replace a valid discount policy →  
**LSP is broken.**

#### Step 4: Fix it

```csharp
public class GoodDiscountPolicy : IDiscountPolicy
{
    public decimal CalculateDiscountPercentage(Order order)
    {
        return 10; // valid discount
    }
}

```

Now the child class follows the rules of the parent type and everything works correctly.

----------

## 🎉 Final Output

Across all stages, the application prints an invoice:

```
========= EKart Invoice =========
Order Id   : 1001
Customer   : Ravi Ram(Premium)
...
Grand Total: XXXX
=================================

```

The **business behaviour never changes**, but the architecture gets better in every stage.

----------

## 👍 Exercise Suggestions for Students

-   Try adding a new discount policy (e.g., “FestivalDiscountPolicy”).
    
-   Try removing invoice printing or replacing it with JSON output.
    
-   Try creating a file based JsonRepository instead of `EkartRepository`.

----------