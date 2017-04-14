using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VerschlüsselungDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "123user!123user!";
            string plainText = "heute ist mein letzter tag am bbrz und ich bin froh wenn ich arbeiten gehen kann!";

            byte[] chiffre = Encrypt(plainText, key);

            Console.WriteLine(Decrypt(chiffre, key));
        }

        /// <summary>
        /// Verschlüsselt einen gegebenen Klartext mit einem übergebenen Schlüssel
        /// </summary>
        /// <param name="plainText">der zu verschlüsselnde Klartext</param>
        /// <param name="key">der Schlüssel für die Verschlüsselung</param>
        /// <returns>das verschlüsselte byte[]</returns>
        static byte[] Encrypt(string plainText, string key)
        {
            /// Alle Verschlüsselungsalgorithmen arbeiten mit bytes (NICHT mit Strings!)
            /// wir müssen daher string -> byte umwandeln. Dafür wird ein 
            /// Zeichensatz = Encoding benötigt!
            UTF8Encoding enc = new UTF8Encoding();
            MemoryStream ms = null;

            /// Verwende den Algorithmus symmetrischen AES
            using (SymmetricAlgorithm algo = new AesManaged())
            {
                byte[] arrKey = enc.GetBytes(key);
                /// Verschlüsselungsalgorithmen können mit Hilfe eines 
                /// Initialisierungs-Vektors initialisiert werden (erhöht die Sicherheit)
                byte[] iv = new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

                /// Erzeuge einen "Verschlüssler"
                ICryptoTransform encryptor = algo.CreateEncryptor(arrKey, iv);
                ms = new MemoryStream();

                /// Schreibe auf den CryptoStream (Verschlüssle also!)
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    StreamWriter sw = new StreamWriter(cs);
                    sw.Write(plainText);
                    sw.Close();
                }
            }
            /// gib die verschlüsselten bytes zurück
            return ms?.ToArray();
        }

        /// <summary>
        /// Entschlüsselt ein byte[] mit Hilfe eines übergebenen Schlüssels
        /// </summary>
        /// <param name="chiffre">die zu entschlüsselnden bytes</param>
        /// <param name="key">der Schlüssel zum Entschlüsseln</param>
        /// <returns>der entschlüsselte Text</returns>
        static string Decrypt(byte[] chiffre, string key)
        {
            string plainText = null;
            UTF8Encoding enc = new UTF8Encoding();
            byte[] arrKey = enc.GetBytes(key);
            byte[] iv = new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            using (SymmetricAlgorithm algo = new AesManaged())
            {
                /// Erzeuge einen "Entschlüssler"
                ICryptoTransform decryptor = algo.CreateDecryptor(arrKey, iv);
                MemoryStream ms = new MemoryStream(chiffre);   

                /// Lies über den CryptoStream die verschlüsselten Bytes
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    StreamReader sr = new StreamReader(cs);
                    /// welche damit entschlüsselt werden
                    plainText = sr.ReadToEnd();
                }
            }
            return plainText;
        }
    }
}
