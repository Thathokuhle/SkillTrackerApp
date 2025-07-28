💡 SkillTrackerApp
SkillTrackerApp is a personal skill and learning goal tracking system built with ASP.NET Core MVC. It empowers users to log the skills they’re learning, set actionable goals, and monitor their development journey—ideal for junior software developers who want to showcase their growth and technical skills.

🚀 Purpose
This project was created to:

Practice real-world ASP.NET Core MVC development.

Implement authentication, authorization, and CRUD operations.

Track personal and professional learning goals.

Demonstrate clean architecture, maintainable code, and project management on GitHub.

🛠️ Tech Stack
ASP.NET Core MVC

Entity Framework Core

SQL Server (LocalDB or full)

Bootstrap (UI styling)

ASP.NET Core Identity (User authentication)

Git & GitHub (Version control & project tracking)

✅ Key Features
🔐 User Registration and Login (via ASP.NET Core Identity)

➕ Add, ✏️ Edit, and ❌ Delete Skills

🎯 Set Learning Goals with target dates

👤 Link skills and goals to authenticated users

🔍 Search and filter skills

📊 Dashboard summary of active goals

📱 Responsive layout with sidebar navigation

📦 Integrated EF Core Migrations and DbContext

📋 Project tasks tracked using GitHub Project Board

📁 Project Structure
graphql
Copy
Edit
SkillTrackerApp/
│
├── Controllers/         # MVC controllers
├── Models/              # Application data models
├── Views/               # Razor views
├── Data/                # EF Core DbContext and migrations
├── wwwroot/             # Static assets (CSS, JS, images)
│
├── appsettings.json     # App configuration
├── Program.cs           # App entry point
└── README.md            # Project documentation
🧪 Getting Started
1. Clone the Repository
bash
Copy
Edit
git clone https://github.com/YOUR_USERNAME/SkillTrackerApp.git
cd SkillTrackerApp
2. Set Up the Database
Update appsettings.json with your local SQL Server connection string.

Run EF Core migrations:

bash
Copy
Edit
dotnet ef database update
3. Run the App
bash
Copy
Edit
dotnet run
Open in browser: https://localhost:5001

📈 Project Management
Project tasks are organized on the GitHub Project Board:

🗂️ TODO – Planned tasks

🔧 IN PROGRESS – Actively being developed

✅ DONE – Completed and tested

🤝 Contributing
Pull requests are welcome! For major changes, please open an issue first to discuss what you’d like to change.

📃 License
This project is licensed under the MIT License.
