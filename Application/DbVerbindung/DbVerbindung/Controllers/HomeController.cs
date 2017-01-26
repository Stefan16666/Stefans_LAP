using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DbVerbindung.Controllers
{
    public class HomeController : Controller
    {
       

        public ActionResult Index()
        {
            DbTest test = new DbTest();
            IDbConnection connection = test.Database.Connection;
            string path = @"C:\Stefans_LAP\Skripte\testimages\1_hotel_test.jpg";

            bool geklappt = PerisitImage(path, connection);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public static bool PerisitImage(string path, IDbConnection connection)
        {
            

            bool geklappt = false;
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.Connection.Open();
                    Image img = Image.FromFile(path);                 
                    MemoryStream tmpStream = new MemoryStream();
                    img.Save(tmpStream, ImageFormat.Png); // change to other format

                    byte[] imgBytes = tmpStream.ToArray();

                    //tmpStream.Seek(0, SeekOrigin.Begin);
                    //byte[] imgBytes = new byte[8000];
                    //tmpStream.Read(imgBytes, 0, 8000);

                    command.CommandText = "INSERT INTO Bild(bilddaten,raum_id) VALUES (@payload ,@raum_id)";

                    IDataParameter par = command.CreateParameter();
                    par.ParameterName = "payload";
                    par.DbType = DbType.Binary;
                    par.Value = imgBytes;
                    command.Parameters.Add(par);

                    IDataParameter par_raumId = command.CreateParameter();
                    par_raumId.ParameterName = "raum_id";
                    par_raumId.DbType = DbType.Int32;
                    par_raumId.Value = 1;
                    command.Parameters.Add(par_raumId);

                    int anzahl = command.ExecuteNonQuery();

                    if (anzahl>0)
                    {
                        geklappt = true;
                    }
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
               
            }
            return geklappt;
        }
    }
}