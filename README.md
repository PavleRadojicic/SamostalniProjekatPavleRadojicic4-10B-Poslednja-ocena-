# Folklor Windows Forms Aplikacija

## Zahtevi
- Visual Studio 2019/2022
- .NET Framework 4.8
- SQL Server (Express ili Full) sa bazom Folklor

## Pokretanje

### 1. Pokrenite SQL skriptu
Otvorite `Folklor.sql` u SQL Server Management Studio i pokrenite je kako bi kreirali bazu, tabele, podatke i procedure.

### 2. Podesite connection string
Otvorite `DatabaseHelper.cs` i promenite connection string:

```csharp
// Za SQL Server Express (lokalni):
@"Server=.\SQLEXPRESS;Database=Folklor;Trusted_Connection=True;"

// Za SQL Server sa Windows autentifikacijom:
@"Server=localhost;Database=Folklor;Trusted_Connection=True;"

// Za SQL Server sa korisničkim nalogom:
@"Server=localhost;Database=Folklor;User Id=sa;Password=VasaLozinka;"
```

### 3. Otvorite projekat
- Otvorite `FolklorApp.csproj` u Visual Studio
- Build > Build Solution (Ctrl+Shift+B)
- F5 za pokretanje

## Funkcionalnosti

### Login forma
- Prijava sa e-mail adresom i lozinkom
- Dugme za registraciju novog korisnika

### Registracija
- Kreiranje novog korisničkog naloga
- Validacija polja i poklapanja lozinki

### Glavna forma (nakon prijave)
Tri tabela u karticama:

#### Ansambli
- Pregled svih ansambala
- Dodavanje / izmena / brisanje
- Koristi stored procedure: `InsertAnsambl`, `UpdateAnsambl`, `DeleteAnsambl`

#### Igrači
- Pregled svih igrača sa ansamblom
- Dodavanje / izmena / brisanje
- Koristi stored procedure: `InsertIgrac`, `UpdateIgrac`, `DeleteIgrac`

#### Koreografije
- Pregled svih koreografija
- Dodavanje / izmena / brisanje
- Koristi stored procedure: `InsertKoreografija`, `UpdateKoreografija`, `DeleteKoreografija`

## Test podaci za prijavu
- Email: `marko@example.com` | Lozinka: `password123`
- Email: `ana@example.com` | Lozinka: `password456`
- Email: `ivana@example.com` | Lozinka: `password789`

## Napomena
Lozinke su trenutno čuvane kao plain text — za produkcijsku upotrebu 
preporučuje se heširanje (npr. BCrypt).
