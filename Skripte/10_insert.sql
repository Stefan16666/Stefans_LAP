USE reisebuero;
GO

INSERT INTO Rolle(bezeichnung) VALUES('Firmenansprechpartner');
INSERT INTO Rolle(bezeichnung) VALUES('MitarbeiterIVA');
GO


INSERT INTO Firma(bezeichnung, strasse, nummer, plz, ort) VALUES('IBM', 'Obere-Donausstrasse', 1, 1020, 'Wien');
INSERT INTO Firma(bezeichnung, strasse, nummer, plz, ort) VALUES('Donauversicherung', 'Ringstrasse', 1, 1010, 'Wien');
INSERT INTO Firma(bezeichnung, strasse, nummer, plz, ort) VALUES('XYZ-Solution', 'Marc-Aurelstrasse', 50, 1010, 'Wien');
INSERT INTO Firma(bezeichnung, strasse, nummer, plz, ort) VALUES('IBM', 'Ameisgasse', 122, 1140, 'Wien');
GO

INSERT INTO Art(bezeichnung) VALUES('Besprechung');
INSERT INTO Art(bezeichnung) VALUES('Office');
INSERT INTO Art(bezeichnung) VALUES('Seminar');
GO

INSERT INTO Ausstattung(bezeichnung) VALUES('Computer');
INSERT INTO Ausstattung(bezeichnung) VALUES('Buffet');
INSERT INTO Ausstattung(bezeichnung) VALUES('Flipchart');
GO

INSERT INTO Bauwerk(bezeichnung, strasse, nummer, plz, ort)
VALUES('Milleniumtower', 'Handelskai', 120, 1210, 'Wien');
GO

INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis) VALUES ('0125', 'Heller Raum mit Blick auf Donau', 1,1,34,12);
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis) VALUES ('0203', 'Ruhiger Raum mit allen Anschlüssen', 1,1,34,12);
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis) VALUES ('0713', 'Penthhouseraum mit allen Annehmlichkeiten', 1,1,34,25);
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis) VALUES ('0115', 'Heller Raum mit Blick auf Donau', 1,1,34,12);
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis) VALUES ('0233', 'Ruhiger Raum hoch gelegen', 1,1,34,12);
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis) VALUES ('0644', 'Raum mit grossen Tischen', 1,1,34,12);
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis) VALUES ('0715', 'ideale Ausstattung', 1,1,34,12);
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis) VALUES ('0514', 'Raum mit Drucker', 1,1,34,12);
INSERT INTO Raum(bezeichnung,beschreibung, art_id, bauwerk_id, groesse, preis) VALUES ('0515', 'Raum mit Fax', 1,1,34,12);
GO

INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(1,1, 10);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(1,2, 15);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,3, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(1,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(3,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(4,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,2, 8);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,2, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(3,1, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(5,2, 12);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(6,2, 6);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,2, 5);
INSERT INTO Raum_Ausstattung(raum_id, ausstattung_id, Anzahl_Ausstattung) VALUES(2,2, 3);
GO


INSERT INTO Benutzer(bezeichnung, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('muster@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Max', 'Muster', 1, 1);
INSERT INTO Benutzer(benutzername, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('marco@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Marco', 'Wurz',1, 2);
INSERT INTO Benutzer(benutzername, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('claudia@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Claudia', 'Stiegl', 3, 3);
INSERT INTO Benutzer(benutzername, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('daniel@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Daniel', 'Zalli', 2, 4);
INSERT INTO Benutzer(benutzername, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('stefan@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Stefan', 'Groinig', null, 5);
GO



INSERT INTO Bild (bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\1_hotel_test.jpg', Single_Blob) AS import;
GO

INSERT INTO Bild(bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\2_hotel_test.jpg', Single_Blob) AS import;
GO

INSERT INTO Bild (bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\3_hotel_test.jpg', Single_Blob) AS import;
GO

INSERT INTO Bild (bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\4_hotel_test.jpg', Single_Blob) AS import;
GO

INSERT INTO Bild (bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\5_hotel_test.jpg', Single_Blob) AS import;
GO

INSERT INTO Bild (bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\1_reise_test.jpg', Single_Blob) AS import;	
GO

INSERT INTO Bild (bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\2_reise_test.jpg', Single_Blob) AS import;
GO

INSERT INTO Bild (bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\3_reise_test.jpg', Single_Blob) AS import;
GO

INSERT INTO Bild (bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\4_reise_test.jpg', Single_Blob) AS import;
GO

INSERT INTO Bild (bilddaten)
SELECT BulkColumn
FROM Openrowset( 
Bulk 'C:\ITIN20AKT\Datenbank\Skripte\testimages\5_reise_test.jpg', Single_Blob) AS import;
GO

INSERT INTO Reise_Bild (bild_id, reise_id) VALUES(1, 1);
INSERT INTO Reise_Bild (bild_id, reise_id) VALUES(2, 2);
INSERT INTO Reise_Bild (bild_id, reise_id) VALUES(3, 3);
INSERT INTO Reise_Bild (bild_id, reise_id) VALUES(4, 4);
INSERT INTO Reise_Bild (bild_id, reise_id) VALUES(5, 5);
GO

INSERT INTO Unterkunft_Bild (bild_id, unterkunft_id) VALUES(6,1);
INSERT INTO Unterkunft_Bild (bild_id, unterkunft_id) VALUES(7,2);
INSERT INTO Unterkunft_Bild (bild_id, unterkunft_id) VALUES(8,3);
INSERT INTO Unterkunft_Bild (bild_id, unterkunft_id) VALUES(9,4);
INSERT INTO Unterkunft_Bild (bild_id, unterkunft_id) VALUES(10,5);
GO

INSERT INTO Zahlungsart(bezeichnung) VALUES('Visa');
INSERT INTO Zahlungsart(bezeichnung) VALUES('šberweisung');
INSERT INTO Zahlungsart(bezeichnung) VALUES('MasterCard');
GO

INSERT INTO Zahlung(vorname, nachname, nummer, zahlungsart_id) VALUES('Max', 'Bichi','1568744958',1);
INSERT INTO Zahlung(vorname, nachname, nummer, zahlungsart_id) VALUES('Michi', 'Lehnchen','1567774958',2);
INSERT INTO Zahlung(vorname, nachname, nummer, zahlungsart_id) VALUES('Steffi', 'Gindl','177744555445',3);
GO

INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(1);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(1);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(1);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(1);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(1);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(2);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(2);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(2);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(2);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(2);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(3);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(3);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(3);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(3);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(3);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(4);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(4);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(4);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(4);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(4);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(5);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(5);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(5);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(5);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(5);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(6);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(6);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(6);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(6);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(6);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(7);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(7);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(7);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(7);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(7);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(8);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(8);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(8);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(8);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(8);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(9);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(9);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(9);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(9);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(9);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(10);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(10);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(10);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(10);
INSERT INTO Reisedurchfuehrung(reisedatum_id) VALUES(10);
GO

INSERT INTO Buchung(vorname, nachname, geburtsdatum, reisedatum_id,benutzer_id,passnummer) 
VALUES('Hubert', 'Meier', '1960-12-12',1, 1,'AB12345670');
INSERT INTO Buchung(vorname, nachname, geburtsdatum,reisedatum_id,benutzer_id,passnummer)
VALUES('Karl', 'Huber', '1980-10-4',2, 2,'56484651GHD');
INSERT INTO Buchung(vorname, nachname, geburtsdatum,reisedatum_id,benutzer_id,passnummer) 
VALUES('Maria', 'Huber','1950-04-12',3, 3,'KHGKFKU265456');
GO

INSERT INTO Buchung_Zahlung(buchung_id, zahlung_id) VALUES(1,3);
INSERT INTO Buchung_Zahlung(buchung_id, zahlung_id) VALUES(2,2);
INSERT INTO Buchung_Zahlung(buchung_id, zahlung_id) VALUES(3,1);
GO

INSERT INTO Bewertung(wertung, reise_id) VALUES(5,1);
INSERT INTO Bewertung(wertung, reise_id) VALUES(3,2);
INSERT INTO Bewertung(wertung, reise_id) VALUES(4,3);
GO

INSERT INTO BuchungStorniert(buchung_id) VALUES(1);
GO

