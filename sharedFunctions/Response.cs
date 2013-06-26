using System;

namespace sharedFunctions
{
    public class Response
    {
        private int id;

        /// <summary>
        /// the response's database id
        /// </summary>
        public int ID { get { return id; } }

        private int forCommandID;

        /// <summary>
        /// the database id the response belongs to
        /// </summary>
        public int ForCommandID { get { return forCommandID; } }

        private int uid;

        /// <summary>
        /// the sender unique id
        /// </summary>
        public int UID { get { return uid; } }

        private String responseData;

        /// <summary>
        /// the actual response
        /// </summary>
        public String ResponseData { get { return responseData; } }

        private bool read;

        /// <summary>
        /// the response's read status
        /// </summary>
        public bool Read { get { return read; } }

        /// <summary>
        /// constructs a new response from given values
        /// </summary>
        /// <param name="_forCommandID">the database id of the command this response belongs to</param>
        /// <param name="_response">the actual response</param>
        public Response(int _forCommandID, String _response)
        {
            forCommandID = _forCommandID;
            responseData = _response;
            read = false;
        }

        /// <summary>
        /// constructs a response from a string of data received from the server
        /// </summary>
        /// <param name="respString"></param>
        public Response(String respString)
        {
            setValues(respString);
        }

        /// <summary>
        /// sets the internal values according to a string containing them
        /// </summary>
        /// <param name="inputString">the input string containing the response's data</param>
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
