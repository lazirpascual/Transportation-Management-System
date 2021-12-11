# Transportation-Management-System

This application is a working prototype designed for organizations who desire to have their cargo shipped via truck or train.
It is a software system that effectively manages the shipping of goods accross the country, where companies can keep track of the purchasing, scheduling, monitoring and billing of their orders. The results of this project can greatly benefit organizations who ship their goods through models like LTL (Less Than Load) and FTL (Full Truck Load).
 <br />

The project was creating using C# and WPF, supported with a MySQL Database. Agile Methodology and Test-Driven Development were also incorporated throughout the entirety of the project lifecycle.
 <br />

## 🛠 Tech Stack

| <img src="https://i.imgur.com/2Eo8JaI.png" width="40"> | <img src="https://cdn.jsdelivr.net/npm/simple-icons@v4/icons/mysql.svg" width="40"> |  

- **User Interface**: Windows Presentation Foundation (WPF) </br>
- **Languages Used**: C# </br>
- **Database**: MySQL </br>
- **Testing**: .NET Unit Testing Framework </br>
- **Deployment**: Windows Installer </br>

## ⚙️ Features

- Clean user interface that provides easy navigation for users
- User registration/login authenticated through hashing algorithm
- 3 highly functional pages (Admin, Buyer, Planner) designed for the different user roles within the system, including features such as:
  - Adding contracts from the marketplace
  - Filtering orders through their active/completed states
  - Selecting carriers for each order
  - Calculating the estimated trip for each order
  - Generating invoices for completed orders
  - Performing backups for the local TMS database
  - Logging all interactions performed within the system

# Application Preview

## 1. Login Page

<img src="https://i.imgur.com/dLDzK08.png" alt="Login Page" width="450"/>

## 2. Admin Page

![preview](https://i.imgur.com/vL3fEe4.png)

- The admin page represents a role for an individual who has IT experience and is tasked with configuration, maintenance, and troubleshooting of the TMS application. They may access general configuration options for TMS, such as selecting directories for log files, targeting IP address and ports for all DBMS communications. They can access and update TMS Data such as Rate/Fee, Carrier, and Routes, all stored within the database.
The admin also has the ability to initiate a backup to be performed on the local TMS database, where the directory for the backup files is specified.

## 3. Buyer Page

![preview](https://i.imgur.com/ox4QZQz.png)

- The buyer page represents an employee who is tasked with requesting Customer contracts from the Contract Marketplace and generating an initial Order or contract. Their chief output is an Order, which is marked for action by the Planner. After the Planner’s work is completed, the Buyer confirms each completed Order and generates an Invoice to the Customer. Each invoice generated results in a text file generated with the appropriate billing details, which is also stored in the TMS database.

## 4. Planner Page

![preview](https://i.imgur.com/Xz1lLpG.png)

- The Planner employee is responsible for furthering the order by selecting one or more registered Carriers to fulfill the Order, in the form of Trips. Once assigned, the Planner monitors the progression of time in the application, ensuring that when all Trips on an order are completed, the Order will be marked as Completed and sent back to the Buyer for Invoice Generation. Finally, the planner can generate a summary report of all invoice data, with the option to filter the invoices generated for the past 2 weeks.


# Development Process

- **Planning** <br />

  ![preview](https://i.imgur.com/3JBPqYA.png)

  - Before any coding was started, adequate planning was performed to ensure a safe and suitable approach in regards to the development of the project, covering mandatory project management goals, but also some goals related to the design of the actual software solution. As such, the team devoted their time to developing diagrams that would greatly aid with the development of the project. These diagrams include the following: WBS diagram and dictionary, Sequence Diagram for buyer order creation, Use Case Diagram for the planner role, Schema Diagram for the TMS Database, and Class Diagram for the TMS solution.

- **Testing** <br />
  - To ensure that the quality of the project is kept at the highest level, the team decided to adapt Test-Driven Development for the project. The benefits of this system was apparent, and the team decided to implement this system throughout the entire duration of project lifecycle. One of the benefits of TTD was its ability to produce modular code, where the focus is kept to a single feature at a time and further development is paused until the associated unit test is passed. Through multiple iterations, the code quality was enhanced, making it easier to discover bugs and reuse the code. Additionally, the code was a lot easier to maintain and code refactoring was a lot easier to perform. The team noticed that the TDD approach produced a naturally cleaner, more readable, and manageable code.
