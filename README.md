<div align="center">

# ?? Bug Tracking System
###  System zarz?dzania zg?oszeniami w ASP.NET Core


</div>

---

## O projekcie
**BugTrackSys** to aplikacja s?u??ca do kompleksowego zarz?dzania cyklem ?ycia zg?osze? b??dów. Umo?liwia u?ytkownikom zg?aszanie problemów, a administratorom efektywne zarz?dzanie ich statusem i priorytetem. System wspiera prac? zespo?ow? poprzez system komentarzy oraz powiadomienia w czasie rzeczywistym.

---

## ?? Inicjalizacja i Technologia

Projekt zosta? utworzony w ?rodowisku Visual Studio przy u?yciu szablonu **ASP.NET Core Web App (MVC)**.

### ?? Konfiguracja Architektury
* **Platforma:** `.NET 10.0`
* **Baza danych:** `Microsoft SQL Server` (SQL Express)
* **Uwierzytelnianie:** `Individual User Accounts` (Identity)
* **Bezpiecze?stwo:** Wymuszone `HTTPS`

### ?? Kluczowe pakiety NuGet
| Pakiet | Zastosowanie |
| :--- | :--- |
| `Microsoft.EntityFrameworkCore.SqlServer` | Komunikacja z baz? danych MS SQL |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | Obs?uga to?samo?ci, ról i u?ytkowników |
| `Microsoft.AspNetCore.Identity.UI` | Gotowe widoki Razor dla logowania i rejestracji |
| `Microsoft.VisualStudio.Web.CodeGeneration.Design` | Generowanie kodu (Scaffolding) |

### ?? ?a?cuch po??czenia
Konfiguracja bazy danych znajduje si? w pliku `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-HEAT3N\\SQLEXPRESS;Database=BDwAI_BugTrackSys;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```
---

## ?? G?ówne funkcjonalno?ci

### ?? Funkcje u?ytkownika
-  Rejestracja i logowanie (ASP.NET Identity)
-  Dodawanie nowych zg?osze? b??dów  
  *(przypisanie do projektu i priorytetu)*
-  Podgl?d **w?asnych zg?osze?**  
  *(filtrowanie po ID zalogowanego u?ytkownika)*
-  Dodawanie komentarzy do zg?osze?
-  Powiadomienia o:
	- zmianie statusu zg?oszenia
	- odpowiedzi administratora

---

### ??? Funkcje administratora
-  Pe?ny dost?p do wszystkich zg?osze?
-  Zmiana statusów zg?osze?  
  *(Nowe ? W trakcie ? Zako?czone)*
-  Zarz?dzanie s?ownikami:
	- Projekty
	- Statusy
	- Priorytety
-  Powiadomienia o:
	 - nowych zg?oszeniach
	 - nowych komentarzach

---

## ??? Baza danych

### Struktura encji

System opiera si? na nast?puj?cych tabelach:

| Encja | Opis |
| --- | --- |
| **Zgloszenie** | G?ówna encja systemu (temat, opis, daty, klucze obce) |
| **Projekt** | S?ownik projektów  |
| **Status** | Statusy zg?osze? |
| **Priorytet** | Priorytety zg?osze?  |
| **Komentarz** | Dyskusja i historia komunikacji |
| **Powiadomienie** | Alerty systemowe dla u?ytkowników |
| **AspNetUsers / AspNetRoles** | Systemowe tabele ASP.NET Identity |

---

### Inicjalizacja danych

Projekt posiada wbudowany mechanizm **Seed Data** (`DbInicjalizator`), który przy pierwszym uruchomieniu:

1. Tworzy baz? danych (je?li nie istnieje)
2. Dodaje role: **Admin**, **User**
3. Tworzy konta testowe
4. Uzupe?nia s?owniki systemowe
5. Dodaje przyk?adowe zg?oszenia

---

### U?ytkownicy testowi

Je?li baza danych zosta?a zainicjalizowana automatycznie, dost?pne s? nast?puj?ce konta:

| Rola | Email | Has?o |
| --- | --- | --- |
| **Administrator** | `admin@admin.pl` | `Admin123!` |
| **Administrator** | `test2@test.pl` | `Test123!` |
| **U?ytkownik** | `test@test.pl` | `Test123!` |

---

## ??REST API

Aplikacja udost?pnia **REST API** dla obs?ugi zg?osze?:

-  **Endpoint:** `/api/ZgloszeniaApi`
-  Operacje CRUD dla encji **Zgloszenie**