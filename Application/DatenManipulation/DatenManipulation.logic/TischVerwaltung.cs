using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatenManipulation.logic
{
    public class TischVerwaltung
    {
        public static bool Anlegen(string bezeichnung, int personenAnzahl, int raum_id)
        {
            Debug.WriteLine("TischVerwaltung - Anlegen");
            bool erfolgreich = false;

            if (!string.IsNullOrEmpty(bezeichnung))
            {
                using (var context = new restaurantreservierungEntities())
                {
                    try
                    {
                        Tisch neuerTisch = new Tisch();
                        neuerTisch.Bezeichnung = bezeichnung;
                        neuerTisch.Raum_ID = raum_id;
                        neuerTisch.PersonenAnzahl = personenAnzahl;
                        context.AlleTische.Add(neuerTisch);
                        int number = context.SaveChanges();
                        if (number > 0)
                        {
                            erfolgreich = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Anlegen hat nicht geklappt");
                        Debug.WriteLine(ex.Message);

                    }
                }
            }
            return erfolgreich;
        }

        public static List<Raum> alleRaeume()
        {
            Debug.WriteLine("TischVerwaltung - Anlegen");
            List<Raum> liste = new List<Raum>();


            using (var context = new restaurantreservierungEntities())
            {
                try
                {
                    liste = context.AlleRaeume.ToList();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Anlegen hat nicht geklappt");
                    Debug.WriteLine(ex.Message);

                }
            }

            return liste;
        }

    }
}
