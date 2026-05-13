-- =============================================
-- Folklor baza podataka
-- Autor: Pavle Radojicic
-- =============================================

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Folklor')
    DROP DATABASE Folklor;
GO

CREATE DATABASE Folklor;
GO

USE Folklor;
GO

-- =============================================
-- TABELE
-- =============================================

CREATE TABLE korisnik (
    korisnik_id INT IDENTITY(1,1) PRIMARY KEY,
    ime VARCHAR(50) NOT NULL,
    prezime VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    lozinka VARCHAR(255) NOT NULL,
    datum_registracije DATETIME DEFAULT GETDATE()
);

CREATE TABLE ansambl (
    ansambl_id INT IDENTITY(1,1) PRIMARY KEY,
    naziv VARCHAR(100) NOT NULL,
    grad VARCHAR(100),
    godina_osnivanja INT,
    tip VARCHAR(50),
    korisnik_id INT,
    FOREIGN KEY (korisnik_id) 
        REFERENCES korisnik(korisnik_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);

CREATE TABLE igrac (
    igrac_id INT IDENTITY(1,1) PRIMARY KEY,
    ime VARCHAR(50) NOT NULL,
    prezime VARCHAR(50) NOT NULL,
    datum_rodjenja DATE,
    pol CHAR(1) CHECK (pol IN ('M', 'Z')),
    pozicija VARCHAR(50),
    datum_pridruzivanja DATE,
    ansambl_id INT,
    FOREIGN KEY (ansambl_id) 
        REFERENCES ansambl(ansambl_id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);

CREATE TABLE koreografija (
    koreografija_id INT IDENTITY(1,1) PRIMARY KEY,
    naziv VARCHAR(100) NOT NULL,
    trajanje INT,
    stil VARCHAR(50),
    datum_premijere DATE,
    ansambl_id INT,
    FOREIGN KEY (ansambl_id) 
        REFERENCES ansambl(ansambl_id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

-- =============================================
-- PODACI
-- =============================================

INSERT INTO korisnik (ime, prezime, email, lozinka)
VALUES 
    ('Marko', 'Markovic', 'marko@example.com', 'password123'),
    ('Ana', 'Jovanovic', 'ana@example.com', 'password456'),
    ('Ivana', 'Petrovic', 'ivana@example.com', 'password789');

INSERT INTO ansambl (naziv, grad, godina_osnivanja, tip, korisnik_id)
VALUES 
    ('KUD Mladost', 'Novi Sad', 1995, 'Pevacki', 1),
    ('AKUD Ivo Lola Ribar', 'Beograd', 1995, 'Folklorni', 2),
    ('KUD Abrasevic', 'Nis', 2010, 'Moderni', 3);

INSERT INTO igrac (ime, prezime, datum_rodjenja, pol, pozicija, datum_pridruzivanja, ansambl_id)
VALUES 
    ('Nikola', 'Markovic', '1995-08-20', 'M', 'Visoki igrac', '2020-06-01', 1),
    ('Milica', 'Jovanovic', '2000-11-15', 'Z', 'Niski igrac', '2021-03-12', 1),
    ('Aleksandar', 'Petrovic', '1998-01-30', 'M', 'Harmonicas', '2019-05-22', 2),
    ('Jovana', 'Novak', '2002-04-10', 'Z', 'Solista', '2022-01-10', 2),
    ('Marija', 'Stojanovic', '1997-12-05', 'Z', 'Trener', '2015-09-15', 3);

INSERT INTO koreografija (naziv, trajanje, stil, datum_premijere, ansambl_id)
VALUES 
    ('Igre iz Sumadije', 15, 'Pevacki', '2023-06-10', 1),
    ('Vlaske igre', 30, 'Igracki', '2022-11-20', 2),
    ('Vojvodina', 20, 'Moderni', '2023-02-01', 3);
GO

-- =============================================
-- PROCEDURE - KORISNIK
-- =============================================

CREATE PROCEDURE InsertKorisnik
    @ime VARCHAR(50),
    @prezime VARCHAR(50),
    @email VARCHAR(100),
    @lozinka VARCHAR(255)
AS
BEGIN
    BEGIN TRY
        INSERT INTO korisnik (ime, prezime, email, lozinka)
        VALUES (@ime, @prezime, @email, @lozinka);
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

CREATE PROCEDURE UpdateKorisnik
    @korisnik_id INT,
    @ime VARCHAR(50),
    @prezime VARCHAR(50),
    @email VARCHAR(100),
    @lozinka VARCHAR(255)
AS
BEGIN
    BEGIN TRY
        UPDATE korisnik
        SET ime = @ime, prezime = @prezime, email = @email, lozinka = @lozinka
        WHERE korisnik_id = @korisnik_id;
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

CREATE PROCEDURE DeleteKorisnik
    @korisnik_id INT
AS
BEGIN
    BEGIN TRY
        DELETE FROM korisnik
        WHERE korisnik_id = @korisnik_id;
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

-- =============================================
-- PROCEDURE - ANSAMBL
-- =============================================

CREATE PROCEDURE InsertAnsambl
    @naziv VARCHAR(100),
    @grad VARCHAR(100),
    @godina_osnivanja INT,
    @tip VARCHAR(50),
    @korisnik_id INT
AS
BEGIN
    BEGIN TRY
        INSERT INTO ansambl (naziv, grad, godina_osnivanja, tip, korisnik_id)
        VALUES (@naziv, @grad, @godina_osnivanja, @tip, @korisnik_id);
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

CREATE PROCEDURE UpdateAnsambl
    @ansambl_id INT,
    @naziv VARCHAR(100),
    @grad VARCHAR(100),
    @godina_osnivanja INT,
    @tip VARCHAR(50),
    @korisnik_id INT
AS
BEGIN
    BEGIN TRY
        UPDATE ansambl
        SET naziv = @naziv, grad = @grad, godina_osnivanja = @godina_osnivanja, 
            tip = @tip, korisnik_id = @korisnik_id
        WHERE ansambl_id = @ansambl_id;
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

CREATE PROCEDURE DeleteAnsambl
    @ansambl_id INT
AS
BEGIN
    BEGIN TRY
        DELETE FROM ansambl
        WHERE ansambl_id = @ansambl_id;
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

-- =============================================
-- PROCEDURE - IGRAC
-- =============================================

CREATE PROCEDURE InsertIgrac
    @ime VARCHAR(50),
    @prezime VARCHAR(50),
    @datum_rodjenja DATE,
    @pol CHAR(1),
    @pozicija VARCHAR(50),
    @datum_pridruzivanja DATE,
    @ansambl_id INT
AS
BEGIN
    BEGIN TRY
        INSERT INTO igrac (ime, prezime, datum_rodjenja, pol, pozicija, datum_pridruzivanja, ansambl_id)
        VALUES (@ime, @prezime, @datum_rodjenja, @pol, @pozicija, @datum_pridruzivanja, @ansambl_id);
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

CREATE PROCEDURE UpdateIgrac
    @igrac_id INT,
    @ime VARCHAR(50),
    @prezime VARCHAR(50),
    @datum_rodjenja DATE,
    @pol CHAR(1),
    @pozicija VARCHAR(50),
    @datum_pridruzivanja DATE,
    @ansambl_id INT
AS
BEGIN
    BEGIN TRY
        UPDATE igrac
        SET ime = @ime, prezime = @prezime, datum_rodjenja = @datum_rodjenja, pol = @pol, 
            pozicija = @pozicija, datum_pridruzivanja = @datum_pridruzivanja, ansambl_id = @ansambl_id
        WHERE igrac_id = @igrac_id;
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

CREATE PROCEDURE DeleteIgrac
    @igrac_id INT
AS
BEGIN
    BEGIN TRY
        DELETE FROM igrac
        WHERE igrac_id = @igrac_id;
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

-- =============================================
-- PROCEDURE - KOREOGRAFIJA
-- =============================================

CREATE PROCEDURE InsertKoreografija
    @naziv VARCHAR(100),
    @trajanje INT,
    @stil VARCHAR(50),
    @datum_premijere DATE,
    @ansambl_id INT
AS
BEGIN
    BEGIN TRY
        INSERT INTO koreografija (naziv, trajanje, stil, datum_premijere, ansambl_id)
        VALUES (@naziv, @trajanje, @stil, @datum_premijere, @ansambl_id);
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

CREATE PROCEDURE UpdateKoreografija
    @koreografija_id INT,
    @naziv VARCHAR(100),
    @trajanje INT,
    @stil VARCHAR(50),
    @datum_premijere DATE,
    @ansambl_id INT
AS
BEGIN
    BEGIN TRY
        UPDATE koreografija
        SET naziv = @naziv, trajanje = @trajanje, stil = @stil, 
            datum_premijere = @datum_premijere, ansambl_id = @ansambl_id
        WHERE koreografija_id = @koreografija_id;
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO

CREATE PROCEDURE DeleteKoreografija
    @koreografija_id INT
AS
BEGIN
    BEGIN TRY
        DELETE FROM koreografija
        WHERE koreografija_id = @koreografija_id;
    END TRY
    BEGIN CATCH
        RETURN @@ERROR;
    END CATCH
END;
GO
