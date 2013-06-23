using System;
using System.IO;

namespace sharedFunctions
{
    /// <summary>
    /// a basic command object
    /// </summary>
    public class Command
    {
        private int ID;
        private int fromID;
        private int forTargetID;
        
        private String command;
        
        private bool read;

        /// <summary>
        /// creates a new command from its parameters
        /// </summary>
        /// <param name="_forTargetID">the targets id the command belongs to</param>
        /// <param name="_command">the actual command</param>
        public Command(int _forTargetID, String _command)
        {
            forTargetID = _forTargetID;
            command = _command;
            read = false;
        }

        /// <summary>
        /// creates a command from a string received from the server
        /// </summary>
        /// <param name="inputString">the server response containing the commands parameters</param>
        public Command(String inputString)
        {
            createFromString(inputString);
        }

        /// <summary>
        /// reads the commands values from a string and sets them 
        /// </summary>
        /// <param name="valueString">the string containing the commands data</param>
        private void createFromString(String valueString)
        {
            String[] inputSplit = valueString.Split(new Char[] { '\f' }, StringSplitOptions.RemoveEmptyEntries);
            ID = Convert.ToInt32(inputSplit[0]);
            fromID = Convert.ToInt32(inputSplit[1]);
            forTargetID = Convert.ToInt32(inputSplit[2]);
            read = Convert.ToBoolean(inputSplit[3]);
            command = inputSplit[4];
        }        

        public int getID()
        {
            return ID;
        }

        public int getFromID()
        {
            return fromID;
        }

        public int getForTargetID()
        {
            return forTargetID;
        }

        public String getCommandData()
        {
            return command;
        }

        public bool getReadStatus()
        {
            return read;
        }

        public override String ToString()
        {
            return "\ncommand id: " + ID.ToString() + "\n fromID: " + fromID.ToString() + "\n forTargetID: " + forTargetID.ToString() + "\n " + command + "\n read: " + read;
        }

    }
}
