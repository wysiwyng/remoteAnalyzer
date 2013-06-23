using System;
using System.IO;

namespace sharedFunctions
{
    public class Command
    {
        private int ID;
        private int fromID;
        private int forTargetID;
        
        private String command;
        
        private bool read;

        public Command(int _forTargetID, String _command)
        {
            forTargetID = _forTargetID;
            command = _command;
            read = false;
        }

        public Command(String inputString)
        {
            createFromString(inputString);
        }

        private void createFromStream(Stream valueStream)
        {
            createFromString(new StreamReader(valueStream).ReadToEnd());
        }

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
