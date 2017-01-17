USE Innovation4austria;
GO

ALTER TABLE Art
ADD
CONSTRAINT PK_Art
PRIMARY KEY(id);
GO

ALTER TABLE Ausstattung
ADD
CONSTRAINT PK_Ausstattung
PRIMARY KEY(id);
GO

ALTER TABLE Bauwerk
ADD
CONSTRAINT PK_Bauwerk
PRIMARY KEY(id);
GO

ALTER TABLE Benutzer
ADD
CONSTRAINT PK_Benutzer
PRIMARY KEY (id);
GO

ALTER TABLE Buchung
ADD
CONSTRAINT PK_Buchung
PRIMARY KEY (id);
GO

ALTER TABLE Bild
ADD
CONSTRAINT PK_Bild
PRIMARY KEY(id);
GO

ALTER TABLE Buchungsdetails
ADD
CONSTRAINT PK_Buchungsdetails
PRIMARY KEY (id);
GO

ALTER TABLE Buchungsdetails
ADD
CONSTRAINT PK_Buchungsdetails
PRIMARY KEY (id);
GO

ALTER TABLE Raum_Ausstattung
ADD
CONSTRAINT PK_Raum_Ausstattung
PRIMARY KEY (id);
GO

ALTER TABLE Rechnung
ADD
CONSTRAINT PK_Rechnung
PRIMARY KEY (id);
GO

ALTER TABLE Rechnungsdetails
ADD
CONSTRAINT PK_Rechnungsdetails
PRIMARY KEY (id);
GO

ALTER TABLE Rolle
ADD
CONSTRAINT PK_Rolle
PRIMARY KEY (id);
GO
