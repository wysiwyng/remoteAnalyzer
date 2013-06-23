using System;

namespace sharedFunctions
{
    public class Response
    {
        private int ID;
        private int forCommandID;
        private int UID;
        
        private String response;
        
        private bool read;

        public Response(int _forCommandID, int _UID, String _response)
        {
            UID = _UID;
            forCommandID = _forCommandID;
            response = _response;
            read = false;
        }

        public Response(String respString)
        {
            setValues(respString);
        }

        private void setValues(String inputString)
        {
            String[] inputSplit = inputString.Split(new Char[] { '\f' });
            ID = Convert.ToInt32(inputSplit[0]);
            forCommandID = Convert.ToInt32(inputSplit[1]);
            UID = Convert.ToInt32(inputSplit[2]);
            response = inputSplit[4];
            read = Convert.ToBoolean(inputSplit[3]);
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

        public override String ToString()
        {
            return "\nresponse id: " + ID.ToString() + "\n forCmdID: " + forCommandID.ToString() + "\n TargetID: " + UID.ToString() + "\n " + response + "\n read: " + read;
        }
    }
}
