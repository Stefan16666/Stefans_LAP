using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatenManipulation.logic
{
    public class RaumVerwaltung
    {
        public static bool Anlegen(string bezeichnung)
        {
            Debug.WriteLine("RaumVerwaltung - Anlegen");
            bool erfolgreich = false;
            
            if (!string.IsNullOrEmpty(bezeichnung))
            {
                using (var context = new restaurantreservierungEntities())
                {
                    try
                    {
                        Raum neuerRaum = new Raum();
                        neuerRaum.Bezeichnung = bezeichnung;
                        context.AlleRaeume.Add(neuerRaum);
                        int number = context.SaveChanges();
                        if (number>0)
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
    }
}
