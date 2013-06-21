using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace sharedFunctions
{
    public class Response
    {
        private int ID;
        private int forCommandID;
        
        private String response;
        
        private bool read;

        public Response(Stream respStream, Char seperator = '\r')
        {
            setValues(respStream, seperator);
        }

        public Response(String respString, Char seperator = '\r')
        {
            setValues(respString, seperator);
        }

        private void setValues(Stream valueStream, Char seperator = '\r')
        {
            setValues(new StreamReader(valueStream).ReadToEnd(), seperator);
        }

        private void setValues(String valueString, Char seperator = '\r')
        {
            String values = Encoder.hexToString(valueString);
            String[] valuesSplit = values.Split(seperator);
            ID = Convert.ToInt32(valuesSplit[0]);
            forCommandID = Convert.ToInt32(valuesSplit[1]);
            response = valuesSplit[2];
            read = Convert.ToBoolean(valuesSplit[3]);
        }        

        public int getID()
        {
            return ID;
        }

        public int getForCommandID()
        {
            return forCommandID;
        }

        public String getResponseData()
        {
            return response;
        }

        public bool getReadStatus()
        {
            return read;
        }

        public String toString(Char seperator = '\r')
        {
            return ID + seperator + forCommandID + seperator + response + seperator + read;
        }
    }
}
