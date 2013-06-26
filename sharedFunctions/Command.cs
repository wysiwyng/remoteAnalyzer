using System;
using System.IO;

namespace sharedFunctions
{
    /// <summary>
    /// a basic command object
    /// </summary>
    public class Command
    {
        private int id;

        /// <summary>
        /// the command's database id
        /// </summary>
        public int ID { get { return id; } }

        private int fromID;

        /// <summary>
        /// the unique sender's id
        /// </summary>
        public int FromID { get { return fromID; } }

        private int forTargetID;

        /// <summary>
        /// the unique receiver id
        /// </summary>
        public int ForTargetID { get { return forTargetID; } }

        private String commandData;

        /// <summary>
        /// the command's actual data
        /// </summary>
        public String CommandData { get { return commandData; } }
        
        private bool read;

        /// <summary>
        /// the command's read status
        /// </summary>
        public bool Read { get { return read; } }

        /// <summary>
        /// creates a new command from its parameters
        /// </summary>
        /// <param name="_forTargetID">the targets id the command belongs to</param>
        /// <param name="_command">the actual command</param>
        public Command(int _forTargetID, String _command)
        {
            forTargetID = _forTargetID;
            commandData = _command;
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
            id = Convert.ToInt32(inputSplit[0]);
            fromID = Convert.ToInt32(inputSplit[1]);
            forTargetID = Convert.ToInt32(inputSplit[2]);
            read = Convert.ToBoolean(inputSplit[3]);
            commandData = inputSplit[4];
        }        

        public override String ToString()
        {
            return "\ncommand id: " + id.ToString() + "\n fromID: " + fromID.ToString() + "\n forTargetID: " + forTargetID.ToString() + "\n " + commandData + "\n read: " + read;
        }

    }
}
