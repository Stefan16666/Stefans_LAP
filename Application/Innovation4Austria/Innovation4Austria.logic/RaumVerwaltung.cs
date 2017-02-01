using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation4Austria.logic
{
    public class RaumVerwaltung
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<Buchung> BookedRooms(int fa_id)
        {
            log.Info("RaumVerwaltung - BookedRooms");
            List<Buchung> bookedRooms = new List<Buchung>();
            if (fa_id != 0)
            {
                try
                {
                    using (var context = new Innovation4AustriaEntities())
                    {

                         bookedRooms = context.AlleBuchungen.Include("Buchungsdetails").Where(x => x.Firma_id == fa_id).ToList();
                        if (bookedRooms==null)
                        {
                            log.Info("RaumVerWaltung - BookedRooms - There are no booked rooms found");
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    log.Error("RaumVerwaltung - BookedRooms - Connection to database failed", ex);
                    if (ex.InnerException != null)
                    {
                        log.Info(ex.InnerException);
                    }
                }
            }
            return bookedRooms;
        }
    }
}
