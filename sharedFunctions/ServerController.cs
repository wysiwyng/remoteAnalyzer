﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace sharedFunctions
{
    public class ServerController
    {
        private String id;
        private String type;
        private String uri;

        public ServerController(IOperator op)
        {
            id = Encoder.stringToHex(op.getUID().ToString());
            type = "op";
            uri = loadUri();
        }

        public ServerController(ITarget target)
        {
            id = Encoder.stringToHex(target.getUID().ToString());
            type = "target";
            uri = loadUri();
        }

        public String loadUri()
        {
            return Properties.Settings.Default.serverUri;
        }

        public void saveUri(String uri)
        {
            Properties.Settings.Default.serverUri = uri;
            Properties.Settings.Default.Save();
        }

        public void register()
        {
            serverIO("action=register&type=" + Encoder.stringToHex(type));           
        }

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

        public Operator[] getOperators()
        {
            throw new NotImplementedException();
        }

        public Command saveCommand(Command command)
        {
            String[] inputString = serverIO("action=saveCommand&to=" + Encoder.stringToHex(command.getForTargetID().ToString()) + "&cmd=" + Encoder.stringToHex(command.getCommandData()));
            String[] commandStr = inputString[0].Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new Command(commandStr[0] + "\f" + Encoder.hexToString(commandStr[1]));
        }

        public Response saveResponse(Response response)
        {
            String[] inputString = serverIO("action=saveResponse&cmdId=" + Encoder.stringToHex(response.getForCommandID().ToString()) + "&response=" + Encoder.stringToHex(response.getResponseData()));
            String[] commandStr = inputString[0].Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new Response(commandStr[0] + "\f" + Encoder.hexToString(commandStr[1]));
        }

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

        public Command getCommand(int cmdId)
        {
            String[] inputString = serverIO("action=getCommand&ID=" + cmdId.ToString());
            String[] commandStr = inputString[0].Split(new Char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return new Command(commandStr[0] + "\f" + Encoder.hexToString(commandStr[1]));
        }

        public Response[] listResponses()
        {
            throw new NotImplementedException();
        }

        public Response getResponse(String respId)
        {
            throw new NotImplementedException();
        }

        private String[] serverIO(String payload)
        {
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
                    Console.WriteLine("debug: " + str);
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
