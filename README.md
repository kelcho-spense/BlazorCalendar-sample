# Welcome to MLSA Blazor Outlook Calendar!
It's a Calendar and events application that is synced and integrated with Microsoft Outlook Calendar using Microsoft Graph API.
## Bio
👋 Hey! I'm Kevin Comba Gatimu , [a beta MLSA](https://studentambassadors.microsoft.com/en-US/studentambassadors/profile/b81411c3-fbca-4a78-86c3-e7360f946b7f).

## Objectives
- View all events on your calendar
- Add events to my calendar
- View a summary of events per day

## ScreenShots
- LoginPage

![0](https://user-images.githubusercontent.com/57180726/222939196-9cf02eb5-1c05-406e-b335-eb705db58d28.PNG)

- HomePage

![1](https://user-images.githubusercontent.com/57180726/222939205-d6365220-fbe8-4aa7-9a40-3dc383328bc4.PNG)

![2](https://user-images.githubusercontent.com/57180726/222939220-722b0366-e47e-4c9c-8a2d-d262fc63a7b2.PNG)


## How to make this app works

 - Clone this repository locally to your machine.
 - Make sure you have the latest version of .NET Core 3.1, you can find it [here](https://dotnet.microsoft.com/download) 
 - Sign in to your Microsoft Azure account or create a new free account.
 - Go to your Azure Active Directory then AppRegistrations 
 - Click on create a new registration
 - Call the app what ever you want 
 - Set the account type to Multi-Tenant (Microsoft Personal Accoutns)
 - In the  *Redirect URI*  section: Choose Single Page Application and set the URI to https://locahost:5001/authentication/login-callback
 - Click Register
 - Copy the Application Id that appears in the overview section and paste it within the *AppSettings.json* file in the wwwroot folder inside the BlazorCalendar project.
 - Back to your Azure application go to the *Authentication* page from the left side menu within your app.
 - Go to *Implicit grant* section and check *Access Token and ID Tokens* and save the changes 
 - Go to API Permissions page from the left side menu.
 - Add Permissions then Microsoft Graph and Add *Calendars.ReadWrite* permission.
 - Go back to your project folder and open the cmd then the run the command dotnet run. 
 And ready to go.


