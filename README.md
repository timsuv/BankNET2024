The project is a banking application that simulates a banking service with user management, account management, and transaction logging. It includes two main roles: User (regular user) and Admin (administrator). Users can have multiple types of accounts, including Account, SavingAccount, and LoanAccount, and they can perform actions such as deposits, withdrawals, transfers, and managing their transactions. Administrators have additional privileges, such as managing user accounts, changing exchange rates, and viewing all user account information.

Admin Role:
The Admin represents an administrator who can manage users and their accounts, as well as change exchange rates. The admin has several key functions, including the ability to change exchange rates and manage users. This includes actions such as deleting users or viewing a list of all users. The admin can also view and modify exchange rate lists and adjust rates using a method called ChangeCurrencyRate(). The GetCurrencyDictionary() method allows the admin to manipulate the exchange rates by returning a list of current rates.

Account:
The Account class represents a standard bank account where money is stored and transactions are made. It can perform basic functions such as deposits, withdrawals, and transfers. The account keeps track of its account number, balance, and currency type. It logs all transactions (such as deposits or withdrawals) using a TransactionLog. Each account can be associated with a user, granting them access to manage and perform operations on the account.

SavingAccount:
The SavingAccount class is a special type of account designed for savings. It inherits from the regular Account class but adds features specific to saving accounts. Typically, these accounts offer higher interest rates or have restrictions on withdrawals. Users can still perform actions like deposits, but withdrawals may be limited. This class also allows the user to manage their savings while retaining the core functionality of an account, such as tracking transactions.

LoanAccount:
The LoanAccount class represents an account used to manage loans taken by the user. It tracks the amount borrowed and the balance that remains to be repaid. The LoanAccount class manages loan payments and provides information on how much the user still owes. It can interact with other accounts to make payments or display the loan's current status. This class is also linked to a specific user, allowing them to manage their loan through their account.

IUser Interface:
The IUser interface defines the common properties and functions that all user types (such as regular users and administrators) must implement. It establishes a set of behaviors for logging in, viewing account information, managing accounts, and handling currency functions. Both the User and Admin classes implement the IUser interface, which ensures they can be treated similarly in terms of user interactions. By using this interface, both user roles can interact with the system in a unified way.

User:
The User class represents a regular user who can have multiple types of accounts, such as regular accounts, savings accounts, and loan accounts. Users can create new accounts, take out loans, manage their accounts, and view their transactions. The User class provides methods for depositing and withdrawing money, transferring money to other users, and viewing account information and transaction history. It also allows users to create new accounts, including saving accounts and loan accounts, and interact with their financial data.

TransactionLog:
The TransactionLog class logs all transactions that occur on an account, such as deposits, withdrawals, or transfers. It records the date, amount, and type of transaction, and keeps a history of these events. The log helps users keep track of their financial activities by providing a list of past transactions. The TransactionLog class is used by all account types (including LoanAccount and SavingAccount) to ensure that every financial action is tracked.

Menu:
The Menu class represents a navigation menu that presents options to the user, such as deposit, withdrawal, or transfer. It displays a list of available options and handles user input, allowing the user to select the desired action. The MenuRun() method is responsible for executing the menu, processing the user's choice, and returning the selected option. This class acts as a central point for user interaction, allowing them to easily navigate through the application's various features.

ManageBank:
The ManageBank class is the central controller of the application. It is responsible for managing user login, interactions with both users and administrators, and account management. The LogIn() method handles user authentication by prompting for a username and password and verifying them against stored credentials. Depending on whether the user is a regular user or an administrator, the system presents the appropriate menu using the UserMenu() or AdminMenu() methods. The ManageBank class also logs users out after three failed login attempts and can exit the program when needed. It is also responsible for managing various types of accounts, including creating new accounts and handling different banking actions like deposits and transfers.

Relationships Between Classes:
The ManageBank class governs the entire application and keeps track of all users as IUser objects. It manages both user and administrator menus and directs interactions accordingly. The User class implements the IUser interface and can have various types of accounts, including Account, SavingAccount, and LoanAccount. Each account type is linked to a specific user, and the accounts (whether regular, saving, or loan) all interact with the TransactionLog to track financial transactions.

The Admin class is a specialized implementation of IUser with additional functionality for managing users and adjusting exchange rates. The Menu class is used by both User and Admin to navigate the application and choose the appropriate actions.

Application Flow:
The ManageBank class is responsible for initializing the application and providing the user with the option to log in. After logging in, depending on the user type (regular user or admin), a corresponding menu is presented through the Menu class. Regular users can create accounts like LoanAccount and SavingAccount, manage their accounts, and perform actions like deposits, withdrawals, and transfers. All transactions, such as withdrawals, deposits, and transfers, are logged by the TransactionLog. Administrators have the ability to manage and remove users, adjust exchange rates, and view all user account details.

Projektet gjort av Ossy, Tim, Joel, Axel, Sami, Joshua
Exemple på användare
AnvändarNamn : User1
lösenord: 1
AnvändarNamn: Admin
Lösenord: 3


First UML draft:
![Sample Image](BankNET2024/BankNet.png)

Updated UML
![BankUmlUpdate](https://github.com/user-attachments/assets/597ebc36-c706-4ced-9110-d4746d53e3ce)
