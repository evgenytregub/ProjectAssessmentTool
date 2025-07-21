# ProjectAssessmentTool

**ProjectAssessmentTool** is a WPF desktop application built with .NET and MVVM architecture. It allows you to manage a list of projects, calculate CapEx and OpEx values with overflow protection, and validate user input.

---

## 📌 Key Features

- Automatic calculation of financial metrics:
    - `Revenue`
    - `CapEx`
    - `OpEx` 
    - `Cash Flow`
    - `NPV (Net Present Value)`
    - `IRR (Internal Rate of Return)`
    - `Payback Period`
    - `PI (Profitability Index)`
    - `ROI (Return on Investment)`

---

## 🛠️ Technologies & Structure

- **.NET 9.0** (or later)
- **WPF** (Windows Presentation Foundation)
- **CommunityToolkit.Mvvm** for MVVM support
- Project layout:
  ```
  /Converters
  /Data
  / Enums
  /Model
      /Assessment
      /InitialData
      /Project
      /SystemMessages
  /Resources
  /ViewModel
      MainWindowViewModel.cs
      ProjectViewModel.cs
  /Views
      MainWindow.xaml
      AddProjectWindow.xaml
  ```

---

## 🧩 Usage

### Getting Started

1. Clone the repo:
   ```bash
   git clone https://github.com/evgenytregub/ProjectAssessmentTool.git
   ```
2. Open the `.sln` in Visual Studio 2022+ (or equivalent)
3. Restore NuGet packages (`CommunityToolkit.Mvvm`)
4. Build and run the project

---

### User Interface

- **Main Window:** view your projects, add or delete entries
- Each project shows name, creation date, and allows numeric input for replacements

---

## 🎯 How to Add or Remove Projects

- **Add Project:** Uses a dialog or command bound to entering name and values
- **Delete Project:** 
  - Each item in the `ListBox` has a "Delete" button
  - Button is bound to a `DeleteProjectCommand` in ViewModel

```xml
<Button Content="Delete"
        Command="{Binding DataContext.DeleteProjectCommand, RelativeSource={RelativeSource AncestorType=ListBox}}"
        CommandParameter="{Binding}" />
```

---

## 📝 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.