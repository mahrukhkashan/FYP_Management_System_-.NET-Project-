# **FYP Management System**  
*A .NET-based Final Year Project (FYP) Management System for universities*  

![.NET](https://img.shields.io/badge/.NET-6.0-blue)  
![C#](https://img.shields.io/badge/C%23-10.0-green)  
![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-orange)  

## **📌 Overview**  
This **Final Year Project (FYP) Management System** is a web-based application built with **ASP.NET Core MVC** and **SQL Server**. It streamlines the process of managing student projects, supervisor assignments, evaluations, and progress tracking in universities.  

### **✨ Key Features**  
✅ **User Roles:**  
- **Admin** (Manage users, projects, and system settings)  
- **Supervisor** (Guide students, approve/reject proposals, evaluate progress)  
- **Student** (Submit proposals, upload documents, track progress)  

✅ **Project Management:**  
- Proposal submission & approval  
- Document upload (reports, presentations)  
- Progress tracking & milestone updates  

✅ **Evaluation & Grading:**  
- Supervisor feedback & grading  
- Committee evaluations  
- Final project marking  

✅ **Notifications & Alerts**  
- Email/SMS reminders for deadlines  
- Real-time updates on project status  

---

## **🛠️ Technologies Used**  
- **Backend:** ASP.NET Core MVC, C#  
- **Frontend:** Forms 
- **Database:** MS SQL Server  
- **Authentication:** ASP.NET Core Identity  

---

## **🚀 Getting Started**  

### **Prerequisites**  
- **.NET 6.0 SDK** ([Download](https://dotnet.microsoft.com/download))  
- **Visual Studio 2022** (or VS Code)  
- **SQL Server 2019+** (or Azure SQL Database)  

### **Installation Steps**  
1. **Clone the repository:**  
   ```bash
   git clone https://github.com/mahrukhkashan/FYP_Management_System_-.NET-Project-.git
   cd FYP_Management_System_-.NET-Project-
   ```

2. **Restore dependencies:**  
   ```bash
   dotnet restore
   ```

3. **Set up the database:**  
   - Update the connection string in `appsettings.json`.  
   - Run migrations:  
     ```bash
     dotnet ef database update
     ```

4. **Run the application:**  
   ```bash
   dotnet run
   ```
   - Access the system at `https://localhost:5001`.  

---

## **📂 Project Structure**  
```
FYP_Management_System/
├── Controllers/        # MVC Controllers
├── Models/             # Database & View Models
├── Views/              # Razor Pages
├── Migrations/         # Entity Framework Migrations
├── wwwroot/            # Static files (CSS, JS, Images)
├── appsettings.json    # Configuration file
└── Program.cs          # Startup configuration
```

---

## **🔐 Default Login Credentials**  
| Role       | Email               | Password  |
|------------|---------------------|----------|
| **Admin**  | admin@fyp.com       | Admin@123|
| **Supervisor** | supervisor@fyp.com | Sup@123  |
| **Student** | student@fyp.com    | Stu@123  |


## **🤝 Contributing**  
Contributions are welcome!  
1. Fork the repository  
2. Create a new branch (`git checkout -b feature-branch`)  
3. Commit changes (`git commit -m "Add new feature"`)  
4. Push to the branch (`git push origin feature-branch`)  
5. Open a **Pull Request**  



This **README** provides a clear overview of the project, setup instructions, and usage guidelines. You can enhance it further by adding:  
- **Screenshots** of the system  
- **Deployment guide** (Azure/AWS)  
- **API documentation** (if applicable)  

Let me know if you'd like any modifications! 🚀
