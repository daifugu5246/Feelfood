# 🍴 Feel Food

A social food ordering web application developed as part of the **01076119 Web Application Development** course. Feel Food allowing users to request their friends to buy food from the canteen.

## 📖 About the Project

Feel Food is a unique social dining platform that solves the common campus problem of wanting food from the canteen but being unable to go yourself. The application allows students to send food requests to their friends who are heading to the canteen.

## ✨ Features

- **Announce job**: Announce that users are heading to canteen
- **Friend Requests**: Send food purchase requests to friends
- **Request Status**: Track status food request
- **User Profiles**: Manage personal information and password
- **Responsive Design**: Optimized for all devices

## 🚀 Technologies Used

### Frontend
- **HTML5**: Modern markup structure and semantic elements
- **CSS3**: Advanced styling, animations, and responsive design
- **JavaScript**: Interactive functionality and DOM manipulation
- **jQuery**: Simplified AJAX requests and enhanced user interactions

### Backend
- **ASP.NET Core MVC (.NET 7)**: Server-side framework
- **Entity Framework**: Object Relational mapping (ORM) for convert object to table
- **C#**: Primary programming language for backend logic

### Database
- **Microsoft SQL Server**: Relational database management system

### Development Tools
- **Visual Studio**: Integrated development environment
- **SQL Server Management Studio**: Database administration

## 🛠️ Installation & Setup

### Prerequisites
- Visual Studio 2022 or Visual Studio Code
- .NET 8 SDK
- Microsoft SQL Server
- SQL Server Management Studio (optional)

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/feelfood.git
   cd feelfood
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update database connection string**
   - Open `appsettings.json`
   - Update the connection string to match your SQL Server instance

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   Navigate to `https://localhost:5119`

## 💾 Database Setup

### Connection String Example
```appsettings.json
{
  "ConnectionStrings": {
    "FeelfoodDbContextConnection": "Server=(localdb)\\(mssqllocaldb);Database=master;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

### Key Database Tables
- **Users**: User account information and profiles
- **Job**: Job announced
- **Order**: Order requests to job

## 🎯 Key Learning Outcomes

Through the development of Feel Food, I experienced with:

### Frontend Development
- **HTML5**: Semantic markup, form handling, and accessibility
- **CSS3**: Flexbox, Grid, animations, and responsive design principles
- **JavaScript**: ES6+ features, async/await, and modern programming patterns
- **DOM Manipulation**: Dynamic content updates and event handling
- **Responsive Web Design**: Mobile-first approach and media queries

### Backend Development
- **ASP.NET Core MVC (.NET 7)**: Controllers, Views, Models, and routing
- **Entity Framework Core**: Latest ORM for database operations
- **Authentication & Authorization**: User security implementation

### Additional Skills
- **AJAX with jQuery**: Asynchronous data exchange
- **Database Design**: Relational database structure and optimization
- **Debugging**: Problem-solving and performance optimization
