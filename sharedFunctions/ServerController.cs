using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace sharedFunctions
{
    /// <summary>
    /// a class for creating and receiving server messages
    /// </summary>
    public class ServerController
    {
        private String id;
        private String type;

        private String uri;

        /// <summary>
        /// the server backend uri
        /// </summary>
        public String URI
        {
            get
            {
                return uri;
            }
            set
            {
                uri = value;
            }
        }

        /// <summary>
        /// the constructor for a ServerController object
        /// </summary>
        /// <param name="op">an operator for determining the id</param>
        public ServerController(IOperator op)
        {
            id = Encoder.stringToHex(op.UID.ToString());
            type = "op";
            uri = loadUri();
        }

        /// <summary>
        /// the constructor for a ServerController object
        /// </summary>
        /// <param name="target">a target for determining the id</param>
        public ServerController(ITarget target)
        {
            id = Encoder.stringToHex(target.UID.ToString());
            type = "target";
            uri = loadUri();
        }

        /// <summary>
        /// loads the saved server backend uri from application settings
        /// </summary>
        /// <returns>the saved uri</returns>
        public String loadUri()
        {
            return Properties.Settings.Default.serverUri;
        }

        /// <summary>
        /// sets and saves a given server backend uri to application settings
        /// </summary>
        /// <param name="uri">the uri to save</param>
        public void saveUri(String uri)
        {
            Properties.Settings.Default.serverUri = uri;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// registers the interface the server controller was constructed with
        /// TODO: return the operator/target registered
        /// </summary>
        public void register()
        {
            serverIO("action=register&type=" + Encoder.stringToHex(type));           
        }

        /// <summary>
        /// returns a list of all targets that exist in the servers database
        /// </summary>
        /// <returns>an array of targets</returns>
        public Target[] getTargets()
        {
            List<Target> targets = new List<Target>();
            foreach (String str in serverIO("action=getTargets"))       //iterate over each string the server returns
            {
                String[] targetStr = str.Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);     //splits the string further
                targets.Add(new Target(targetStr[0] + "\f" + Encoder.hexToString(targetStr[1])));               //create a new target from the input string
            }
            return targets.ToArray();
        }

        /// <summary>
        /// returns an array of all operators that exist in the servers database
        /// </summary>
        /// <returns>nothing yet, not implemented at the time</returns>
        public Operator[] getOperators()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// saves a command on the server
        /// </summary>
        /// <param name="command">the command object to save</param>
        /// <returns>the saved command as the server saved it</returns>
        public Command saveCommand(Command command)
        {
            String[] inputString = serverIO("action=saveCommand&to=" + Encoder.stringToHex(command.ForTargetID.ToString()) + "&cmd=" + Encoder.stringToHex(command.CommandData));
            String[] commandStr = inputString[0].Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new Command(commandStr[0] + "\f" + Encoder.hexToString(commandStr[1]));
        }

        /// <summary>
        /// saves a response on the server
        /// </summary>
        /// <param name="response">the response object to save</param>
        /// <returns>the saved response as the server saved it</returns>
        public Response saveResponse(Response response)
        {
            String[] inputString = serverIO("action=saveResponse&cmdId=" + Encoder.stringToHex(response.ForCommandID.ToString()) + "&response=" + Encoder.stringToHex(response.ResponseData));
            String[] commandStr = inputString[0].Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new Response(commandStr[0] + "\f" + Encoder.hexToString(commandStr[1]));
        }

        /// <summary>
        /// lists all commands for the user id the controller registered with
        /// </summary>
        /// <returns>an array of commands</returns>
        public Command[] listCommands()
        {
            List<Command> commands = new List<Command>();
            foreach (String str in serverIO("action=listCommands"))
            {
                String[] commandStr = str.Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
                commands.Add(new Command(commandStr[0] + "\f" + Encoder.hexToString(commandStr[1])));
            }
            return commands.ToArray();
        }

        /// <summary>
        /// gets a specific command by its id
        /// </summary>
        /// <param name="cmdId">the commands id</param>
        /// <returns>the command (if found on the server)</returns>
        public Command getCommand (int cmdId)
		{
			String[] inputString = serverIO ("action=getCommand&ID=" + cmdId.ToString ());
			if (inputString.Length == 0) 
			{
				return null;
			}
            String[] commandStr = inputString[0].Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new Command(commandStr[0] + "\f" + Encoder.hexToString(commandStr[1]));
        }
        
        /// <summary>
        /// gets a specific response by its id
        /// </summary>
        /// <param name="respId">the responses id</param>
        /// <returns>the response (if found on the server)</returns>
        public Response getResponseById(int respId)
        {
			String[] inputString = serverIO ("action=getResponseById&ID=" + respId.ToString());
			if (inputString.Length == 0) 
			{
				return null;
			}
            String[] commandStr = inputString[0].Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new Response(commandStr[0] + "\f" + Encoder.hexToString(commandStr[1]));
        }

        /// <summary>
        /// gets a specific response by its corresponding command
        /// </summary>
        /// <param name="respId">the commands id the response belongs to</param>
        /// <returns>the response (if found on the server)</returns>
        public Response getResponseByCmd(int cmdId)
        {
			String[] inputString = serverIO ("action=getResponseByCmdId&ID=" + cmdId.ToString());
			if (inputString.Length == 0) 
			{
				return null;
			}
            String[] commandStr = inputString[0].Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new Response(commandStr[0] + "\f" + Encoder.hexToString(commandStr[1]));
        }

        /// <summary>
        /// executes an IO operaton with the given server uri
        /// </summary>
        /// <param name="payload">the payload to be sent</param>
        /// <returns>an array of strings containing the servers response</returns>
        private String[] serverIO(String payload)
        {
            Debug.WriteLine("starting server io, payload is: " + payload);
            if (uri == null || uri == "")
            {
                throw new IOException("server backend uri not set!");
            }
            WebRequest webRequest = WebRequest.Create(uri);                     //create webRequest to the given uri
            webRequest.Method = "POST";                                         //set method to post
            webRequest.ContentType = "application/x-www-form-urlencoded";       //and mime type to application

            byte[] byteArray = Encoding.UTF8.GetBytes(payload + "&UID=" + id + "&timestamp=" + Encoder.stringToHex(DateTime.Now.ToString()));   //convert payload to byte array
            webRequest.ContentLength = byteArray.Length;                            //set content length
            webRequest.GetRequestStream().Write(byteArray, 0, byteArray.Length);    //write the byte array to the upload stream (transfer data to server)
            webRequest.GetRequestStream().Close();                                  //close upload stream

            String[] responseSplit = new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd().Split(new Char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<String> responseList = new List<String>();

            foreach (String str in responseSplit)           //filter out debug messages
            {
                if (str.StartsWith("%"))
                {
                    Debug.WriteLine("debug: " + str);
                }
				else if (str.StartsWith("!")) 
				{
                    Debug.WriteLine("error: " + str);
				}
                else
                {
                    responseList.Add(str);
                }
            }

            return responseList.ToArray();
        }
    }
}
