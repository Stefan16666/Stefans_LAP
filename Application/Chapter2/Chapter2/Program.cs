using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;


namespace Chapter2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Convertin between different types

            // implicit conversíons
            HttpClient client = new HttpClient();
            object o = client;
            IDisposable d = client;

            // expicit conversions
            double x = 1234.7;
            int a;

            a = (int)x; // a= 1234;

            Object stream = new System.IO.MemoryStream();
            MemoryStream memorystream = (MemoryStream)stream;
        }

        // user defined conversions

        public void OpenConnction(DbConnection connection)
        {
            if (connection is SqlConnection)
            {
                // whatever you want to do
            }
        }

        public void LogStream(Stream stream)
        {
            MemoryStream memoryStream = stream as MemoryStream;

            if (memoryStream != null)
            {
                // do anything
            }
        }

        // exporting some data to excel

        public void DispayInExcel(IEnumerable<dynamic> entities)
        {
            // var excelApp = new Excel.Application(); 
        }

        dynamic obj = new SampleObject();

    }
    public class SampleObject : DynamicObject
    {
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return base.TryGetMember(binder, out result);
        }
    }
    public class Accessibility
    {
        private string _myField;

        public string MyProperty
        {
            get { return _myField; }
            set { _myField = value; }
        }

        //private string[] _otherField;

        //public string[] OtherProperty
        //{
        //    get { return _otherField[0]; }
        //    set { _otherField[0] = value; }
        //}


    }
    public class Base { 
        
        private int _privateField = 42;
        protected int _protectedField = 42;

        private int privatefield;

        public int MyProperty
        {
            get { return privatefield; }
            set { privatefield = value; }
        }

        protected void MyProtectedMehtod() { }
    }

    public class Derived : Base
    {
        
        public void MyDerivedMethod() {
            //_privateField = 42; is not seen, because of private

            _protectedField = 42; //you can acccess, because of it's protected
            
        }
    }


}


