Background:
QQVault is a secure digital banking system built using CSS, Bootstrap, C# and various ASP.NET libraries (i.e Identity.EntityFrameworkCore, AspNetCore.Mvc, AspNetCore.Authentication, AspNetCore.Authorization, EntityFrameworkCore.Sqlite). It manages user accounts, transactions, and role-based access, enabling admins, consultants, and clients to handle banking tasks like transfers, transaction history, and notifications. The system ensures security with login records and file uploads.

🚀 Usage:

1. Register in your desired role
2. Explore your dashboard, view transaction history, account balances etc.
3. Make a transaction of your choice
4. To make a transfer you will need to register 2 separate accounts
5. You will need both account numbers to make a transfer (preferably write them down on a piece of paper for convenience)
6. Respective notifications will be sent to both accounts involved in the transaction (Receiver and Sender).
7. In the case of a withdrawal, a notification will be sent to the accounts involved in the transaction.
8. Input your account number as both the receiver and sender when making a withdrawal.
9. Your feedback would be greatly appreciated. Enjoy!

🛠️ How to Run:
This is an ASP.NET application created using Visual Studio in C#. Ensure to install all the necessary dependencies required to run the application on your device.

1. 🖥️ Development Tools:
Visual Studio (with ASP.NET workload) or Visual Studio Code (with C# extension).

.NET SDK (for ASP.NET Core) or .NET Framework (for ASP.NET MVC 5).

2. Runtime
Install .NET Runtime

3.📚 Dependencies:
Install required NuGet packages (e.g., Microsoft.AspNetCore.Mvc, EntityFramework).

4. Run the Application:

Steps
1. Clone the repository.
2. Open the project in Visual Studio.
3. Restore NuGet packages and build the solution.
4. Run the application using F5 or dotnet run.

Test in a modern browser (e.g., Chrome, Edge).

✨ Features
1. 👤 User Registration & Role-based Accounts
Users can register as one of three roles:
Student (using a 10-digit student number)
Staff (using a 7-digit staff number)
Guest (using a 13-digit South African ID number)
Accounts are automatically created with specific prefixes based on the user role:
Student accounts start with STU
Staff accounts start with UFS
Guest accounts start with GST, all followed by 7 random digits
Each account starts with a simulated balance of R500 for testing purposes.

2. 💼 Account Management
Multiple accounts can be created per user.
Accounts can make transfers to other users or withdraw money. A receipt (with a reference number) is generated for cash withdrawals.

3. 🔔 Transaction Notifications
A notification system is integrated to keep users informed of all their transactions. Notifications can be viewed on a dedicated page.

4. 📊 Personal Dashboard
After successful login, users are redirected to their personal dashboard. The dashboard is the central hub for:
Viewing and managing transactions
Checking transaction history
Viewing account balances
Viewing personal details in the profile page
Financial advisory messages will appear when a user exceeds a certain spending threshold.

5. 👑 Admin Functionality
Admins can access a custom dashboard for managing the entire system.
Admins can:
View all user transactions and transaction details
View detailed information about users
Add, modify, and delete user accounts
Perform actions based on user roles (Student, Staff, Guest)

🤝 Contributing
Contributions are welcome! Please fork the repository and submit a pull request.
