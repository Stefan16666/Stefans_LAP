CREATE VIEW AlleRechnungenEinerFirma
AS
	SELECT  r.datum AS RechnungsAussstellungsDatum,
			r.id AS Rechnungs_id,
			rd.id AS Rechnungsdetails_id,
			bd.id AS Buchungsdetails_id,
			bd.datum AS BuchungsDetailDatum,
			bd.preis,
			b.id AS Buchungs_id,
			b.aktiv,
			b.raum_id			
	FROM Rechnung AS r
	JOIN Rechnungsdetails AS rd
	ON r.id = rd.rechnung_id
	JOIN Buchungsdetails AS bd
	ON rd.buchungsdetails_id = bd.id
	JOIN Buchung AS b
	ON b.id = bd.buchung_id
GO