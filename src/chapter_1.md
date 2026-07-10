# Chapter 1 Project Structure




## Files

## FileService.cs
- This is a class responsible for containing Data points






# Methods.cs
- I created Methods cs file to store all the methods that I will use in my project. This file contains various utility functions that help in performing specific tasks, such as file operations, text manipulation, and other helper methods that are used throughout the application.
    - The purpose of this file is to keep the code organized and maintainable by separating the logic into reusable methods. This way, I can easily call these methods from different parts of the application without duplicating code.
- I then Call the methods from the Methods.cs file in the main application code, allowing for a cleaner and more modular design. This approach enhances code readability and makes it easier to manage and update the application as needed.
    - OpenFile_OnClick
    - Save_OnClick
    - Save-As_OnClick
    - Print-OnClick
    - Exit_OnClick
    - Time_Date_Stamp_OnClick
    - Change_Font_OnClick
    - New_OnClick
    - Format_OnClick
    - Settings_OnClick

## Mainwindow.xaml.cs
- There were some functions that I have to code directly in the Mainwindow.xaml.cs file because they are closely tied to the user interface and require direct interaction with the UI elements. These functions handle events triggered by user actions, such as button clicks or menu selections, and need to access specific UI components to perform their tasks effectively.
    - The Textbox_Main-OnKeyDown Method was one method that had to be in the Mainwindow.xaml.
    - This method is for shortcutKeys. If the user pressed F5 There would be a timestamp inside the Textbox
    - If the user pressed F6 There would be a bulletein point inside the Textbox



## LoggingData.cs
 - Serilog is used for logging 
 - Information Loggging is enabled by default
 - Information and Degbug Logging are implemented


## SettingsView.xaml.cs
 - This is a custom view for selecting which type of logging is enabled
 - Users will be able to select information or Debug Log enabling




