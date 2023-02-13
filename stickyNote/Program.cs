using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;

namespace stickyNote
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadCommand();
            Console.ReadLine();
        }

        private static string NoteDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Notes\";

        private static void ReadCommand()
        {
            Console.Write(Directory.GetDirectories(NoteDirectory));
            string Command = Console.ReadLine();

            switch (Command.ToLower())
            {
                case "new": // new note in menu
                    NewNote();
                    Main(null);
                    break;
                case "edit":
                    EditNote();
                    Main(null);
                    break;
                case "read":
                    ReadNote();
                    Main(null);
                    break;
                case "delete":
                    DeleteNote();
                    Main(null);
                    break;
                case "exit":
                    Exit();
                    Main(null);
                    break;
            }
        }

        private static void NewNote()
        {
            Console.WriteLine("Enter Your Note : \n");
            string note = Console.ReadLine(); //your note

            XmlWriterSettings Note = new XmlWriterSettings();
            
            Note.CheckCharacters = false;
            Note.ConformanceLevel = ConformanceLevel.Auto;
            Note.Indent = true;

            string FileName = DateTime.Now.ToString("dd-MM-yy") + ".xml";

            using (XmlWriter NewNote = XmlWriter.Create(NoteDirectory + FileName, Note))
            {
                NewNote.WriteStartDocument();
                NewNote.WriteStartElement("Note");
                NewNote.WriteElementString("body",input);
                NewNote.WriteEndElement();
                
                NewNote.Flush();
                NewNote.Close();
                
            }
            
        }

        private static void EditNote()
        {
            Console.WriteLine("please Enter Your File name : \n");

            string fileName = Console.ReadLine().ToLower(); // get userInput

            if (File.Exists(NoteDirectory + fileName))
            {
                XmlDocument doc = new XmlDocument();
                
                //load the document

                try
                {
                    doc.Load(NoteDirectory + fileName);
                    Console.Write(doc.SelectSingleNode("//body").InnerText);

                    string ReadInput = Console.ReadLine();

                    if (ReadInput.ToLower() == "cancel")
                    {
                        Main(null);
                    }
                    else
                    {
                        string newText = doc.SelectSingleNode("//body").InnerText = ReadInput;
                        doc.Save(NoteDirectory + fileName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to edit the note following error occurred : " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File not found \n");
            }
        }

        private static void ReadNote()
        {
            Console.WriteLine("Please enter file name : \n");

            string fileName = Console.ReadLine().ToLower();

            if (File.Exists(NoteDirectory + fileName))
            {
                
            }
        }
    }
    
}