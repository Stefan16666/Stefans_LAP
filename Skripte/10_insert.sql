USE Innovation4austria;
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

INSERT INTO Ausstattung(bezeichnung, bilddaten) VALUES('Computer', 
														(SELECT BulkColumn
														 FROM Openrowset( 
														 Bulk 'C:\Pic\computer-ok-icon.png', Single_Blob) AS import)
														 )
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


INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('muster@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Max', 'Muster', 1, 1);
INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('marco@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Marco', 'Wurz',1, 2);
INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('claudia@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Claudia', 'Stiegl', 1, 3);
INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('daniel@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Daniel', 'Zalli', 2, 4);
INSERT INTO Benutzer(email, passwort, vorname, nachname, rolle_id, firma_id)
VALUES('stefan@gmail.at', HASHBYTES('SHA2_512', '123user!'), 'Stefan', 'Groinig', 1, null);
GO


INSERT INTO Buchung (Raum_id, firma_id) VALUES (4, 1);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (2, 3);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (4, 1);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (6, 2);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (2, 3);
INSERT INTO Buchung (Raum_id, firma_id) VALUES (4, 1);
GO

INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 1, '16-06-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 4, '14-06-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 4, '08-07-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 1, '10-10-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 1, '08-04-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 4, '23-12-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 1, '16-02-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 3, '28-10-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 2, '29-11-2017', 12.0);
INSERT INTO Buchungsdetails (buchung_id, datum, preis) VALUES( 2, '29-11-2017', 12.0);
GO

INSERT INTO Rechnung (datum) VALUES ('2017-02-26')
INSERT INTO Rechnung (datum) VALUES ('2017-01-12')
INSERT INTO Rechnung (datum) VALUES ('2017-02-28')
INSERT INTO Rechnung (datum) VALUES ('2017-01-16')
GO

INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,9);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,1);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,2);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,4);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (2,3);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (2,6);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (4,5);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (1,7);
INSERT INTO Rechnungsdetails (rechnung_id, buchungsdetails_id) VAlUES (4,8);
GO

--------