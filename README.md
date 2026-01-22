<div align="center">

# 🐞 Bug Tracking System
###  System zarządzania zgłoszeniami w ASP.NET Core


</div>

---

## O projekcie
**BugTrackSys** to aplikacja służąca do kompleksowego zarządzania cyklem życia zgłoszeń błędów. Umożliwia użytkownikom zgłaszanie problemów, a administratorom efektywne zarządzanie ich statusem i priorytetem. System wspiera pracę zespołową poprzez system komentarzy oraz powiadomienia w czasie rzeczywistym.

---

## 🛠 Inicjalizacja i Technologia

Projekt został utworzony w środowisku Visual Studio przy użyciu szablonu **ASP.NET Core Web App (MVC)**.

### ⚙️ Konfiguracja Architektury
* **Platforma:** `.NET 10.0`
* **Baza danych:** `Microsoft SQL Server` (SQL Express)
* **Uwierzytelnianie:** `Individual User Accounts` (Identity)
* **Bezpieczeństwo:** Wymuszone `HTTPS`

### 📦 Kluczowe pakiety NuGet
| Pakiet | Zastosowanie |
| :--- | :--- |
| `Microsoft.EntityFrameworkCore.SqlServer` | Komunikacja z bazą danych MS SQL |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | Obsługa tożsamości, ról i użytkowników |
| `Microsoft.AspNetCore.Identity.UI` | Gotowe widoki Razor dla logowania i rejestracji |
| `Microsoft.VisualStudio.Web.CodeGeneration.Design` | Generowanie kodu (Scaffolding) |

### 🔌 Łańcuch połączenia
Konfiguracja bazy danych znajduje się w pliku `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-HEAT3N\\SQLEXPRESS;Database=BDwAI_BugTrackSys;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```
---

## 🚀 Główne funkcjonalności

### 👤 Funkcje użytkownika
-  Rejestracja i logowanie (ASP.NET Identity)
-  Dodawanie nowych zgłoszeń błędów  
  *(przypisanie do projektu i priorytetu)*
-  Podgląd **własnych zgłoszeń**  
  *(filtrowanie po ID zalogowanego użytkownika)*
-  Dodawanie komentarzy do zgłoszeń
-  Powiadomienia o:
	- zmianie statusu zgłoszenia
	- odpowiedzi administratora

---

### 🛡️ Funkcje administratora
-  Pełny dostęp do wszystkich zgłoszeń
-  Zmiana statusów zgłoszeń  
  *(Nowe → W trakcie → Zakończone)*
-  Zarządzanie słownikami:
	- Projekty
	- Statusy
	- Priorytety
-  Powiadomienia o:
	 - nowych zgłoszeniach
	 - nowych komentarzach

---

## 🗄️ Baza danych

### Struktura encji

System opiera się na następujących tabelach:

| Encja | Opis |
| --- | --- |
| **Zgloszenie** | Główna encja systemu (temat, opis, daty, klucze obce) |
| **Projekt** | Słownik projektów  |
| **Status** | Statusy zgłoszeń |
| **Priorytet** | Priorytety zgłoszeń  |
| **Komentarz** | Dyskusja i historia komunikacji |
| **Powiadomienie** | Alerty systemowe dla użytkowników |
| **AspNetUsers / AspNetRoles** | Systemowe tabele ASP.NET Identity |

---

### Inicjalizacja danych

Projekt posiada wbudowany mechanizm **Seed Data** (`DbInicjalizator`), który przy pierwszym uruchomieniu:

1. Tworzy bazę danych (jeśli nie istnieje)
2. Dodaje role: **Admin**, **User**
3. Tworzy konta testowe
4. Uzupełnia słowniki systemowe
5. Dodaje przykładowe zgłoszenia

---

### Użytkownicy testowi

Jeśli baza danych została zainicjalizowana automatycznie, dostępne są następujące konta:

| Rola | Email | Hasło |
| --- | --- | --- |
| **Administrator** | `admin@admin.pl` | `Admin123!` |
| **Użytkownik** | `test@test.pl` | `Test123!` |

---

## 🔌REST API

Aplikacja udostępnia **REST API** dla obsługi zgłoszeń:

-  **Endpoint:** `/api/ZgloszeniaApi`
-  Operacje CRUD dla encji **Zgloszenie**

