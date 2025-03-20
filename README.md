# DT191G Projket ASP.NET
I det här projektet har en ASP.net MVC Core med Entity Framework applikation för företaget LifeLine Academy skapas som är ett (fiktivt) företag inom området livräddande utbildningar och föreläsningar om krisberedskap. 
Applikationen vänder sig både till besökare för att presentera företagets tjänster och för att besökare ska kunna göra en offertförfrågan samt till personalen för att hantera offertförfrågningar, föreläsningar, kategorier och föreläsare. 
De olika delarna har därav anpassas utifrån roll och Indentity används för att hantera användarregistrering, inloggning och behörigheter utifrån roller. 

# Funkitonalitet

## Användarhantering:  
- Registrering och inloggning via **ASP.NET Core Identity**  
- Rollbaserad åtkomstkontroll för **administratörer, föreläsare och användare**  

## Databashantering:  
- **SQLite** används som databas  
- **Entity Framework Core (EF Core)** används för ORM och Code-First-databasstruktur  

## Hantering av föreläsningar:
- Skapa, redigera, ta bort och lista föreläsningar  
- Kategorisering av föreläsningar med filtreringsmöjligheter  

## Offertförfrågningar:  
- Användare kan skicka offertförfrågningar på föreläsningar  
- Administratörer kan hantera och besvara offertförfrågningar  

## Gränssnitt:  
- **Bootstrap** används för en responsiv och användarvänlig design  
- **Razor-syntax** används  

## Säkerhet & Behörigheter:  
- Rollbaserad åtkomstkontroll med `[Authorize]`  
- Skyddade länkar/operationer  

## Teknisk Struktur  

- **Backend:** ASP.NET Core MVC  
- **Frontend:** Razor Views + Bootstrap  
- **Databas:** SQLite + Entity Framework Core  
- **Autentisering:** ASP.NET Core Identity  
- **Versionshantering:** Git och GitHub  

## Skapad av:
Adela Knap adkn2300@student.miun.se
