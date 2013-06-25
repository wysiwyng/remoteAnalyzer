using System;

namespace sharedFunctions
{
    public class Response
    {
        private int id;
        public int ID { get { return id; } }

        private int forCommandID;
        public int ForCommandID { get { return forCommandID; } }

        private int uid;
        public int UID { get { return uid; } }

        private String responseData;
        public String ResponseData { get { return responseData; } }

        private bool read;
        public bool Read { get { return read; } }

        public Response(int _forCommandID, int _UID, String _response)
        {
            uid = _UID;
            forCommandID = _forCommandID;
            responseData = _response;
            read = false;
        }

        public Response(String respString)
        {
            setValues(respString);
        }

        private void setValues(String inputString)
        {
            String[] inputSplit = inputString.Split(new Char[] { '\f' });
            id = Convert.ToInt32(inputSplit[0]);
            forCommandID = Convert.ToInt32(inputSplit[1]);
            uid = Convert.ToInt32(inputSplit[2]);
            responseData = inputSplit[4];
            read = Convert.ToBoolean(inputSplit[3]);
        }

        public override String ToString()
        {
            return "\nresponse id: " + id.ToString() + "\n forCmdID: " + forCommandID.ToString() + "\n TargetID: " + uid.ToString() + "\n " + responseData + "\n read: " + read;
        }
    }
}
