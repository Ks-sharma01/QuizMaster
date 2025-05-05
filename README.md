🎯 Quiz Application - ASP.NET Core MVC
A full-featured Quiz Application built using ASP.NET Core MVC with Role-Based Authentication and Authorization. The application allows users to take quizzes and view their results on their profile page, while admins can manage content and view top performers on a leaderboard.

🚀 Features
👤 User Role:
Register & Login securely

Attempt multiple quizzes

View score history on profile page

Access only user-allowed pages

🛠️ Admin Role:
View leaderboard (Top 10 users by score)

Manage quizzes (Add/Update/Delete)

Role-protected admin dashboard

Monitor user performances

🛠️ Tech Stack
Frontend: HTML, CSS, JavaScript, JQuery, Bootstrap

Backend: ASP.NET Core MVC (C#)

Authentication: Role-based Authentication

Database: SQL Server/Stored Procedure

ORM: Entity Framework Core

🔐 Roles & Permissions
Role	Access
User	Take quiz, view profile, view score
Admin	View leaderboard, manage users/quizzes, add categories/questions through stored procedure

🧑‍💻 Getting Started
Prerequisites
.NET Core 6 or later

SQL Server

Visual Studio 2022 or VS Code

Installation
Clone the repository:

bash
Copy
Edit
git clone https://github.com/Ks-sharma01/QuizMaster.git
cd QuizMaster
Update the appsettings.json with your SQL Server connection string.

Run migrations and update the database:

bash
Copy
Edit
dotnet ef database update
Run the application:

bash
Copy
Edit
dotnet run
Navigate to https://localhost:5001 in your browser.

User:

Register a new user via the UI

📂 Folder Structure
bash
Copy
Edit
/Controllers
/Views
/Models
/ViewModels
/Data
/Migrations
/wwwroot

📌 Future Enhancements
Quiz timer functionality

Quiz categories and difficulty levels

User analytics dashboard

🤝 Contributing
Contributions are welcome! Feel free to fork the repo and submit a PR.

📄 License
This project is licensed under the MIT License.
