using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;

namespace stickyNote
{
    class Program
    {
        static void Main(String[] args)
        {
           ReadCommand();
           Console.ReadLine();
        }

        private static string NoteDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Sticky-sharp\";

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
                default:
                    Menu();
                    Main(null);
                    break;
            }
        }

        private static void NewNote()
        {
            
            Console.WriteLine("Enter your new File name : ");
            string fileName = Console.ReadLine() + ".txt";
            
            Console.WriteLine("Enter Your Note : \n");
            string note = Console.ReadLine(); //your note

            XmlWriterSettings Note = new XmlWriterSettings();
            
            Note.CheckCharacters = false;
            Note.ConformanceLevel = ConformanceLevel.Auto;
            Note.Indent = true;
            
            using (XmlWriter NewNote = XmlWriter.Create(NoteDirectory + fileName, Note))
            {
                NewNote.WriteStartDocument();
                NewNote.WriteStartElement("Note");
                NewNote.WriteElementString("body",note);
                NewNote.WriteEndElement();
                
                NewNote.Flush();
                NewNote.Close();
                
            }
            
        }

        private static void EditNote()
        {
            Console.WriteLine("please Enter Your File name with '.txt' : ");

            string fileName = Console.ReadLine().ToLower(); // get userInput

            if (File.Exists(NoteDirectory + fileName ))
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
            Console.WriteLine("please Enter Your File name with '.txt' : ");

            string fileName = Console.ReadLine().ToLower();

            if (File.Exists(NoteDirectory + fileName))
            {
                XmlDocument Doc = new XmlDocument();
                Doc.Load(NoteDirectory+fileName);
                
                Console.WriteLine(Doc.SelectSingleNode("//body").InnerText);
            }
            else
            {
                Console.WriteLine("File not Found :( ");
            }
        }

        private static void DeleteNote()
        {
            Console.WriteLine("please Enter Your File name with '.txt' : ");
            string fileName = Console.ReadLine();

            if (File.Exists(NoteDirectory + fileName))
            {
                Console.WriteLine(Environment.NewLine + "Are you sure you wish to delete the note? Y/N ");
                string selection = Console.ReadLine().ToLower();

                if (selection == "y")
                {
                    try
                    {
                        File.Delete(NoteDirectory + fileName);
                        Console.WriteLine("File has been deleted \n ");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("File unable to delete ");
                    }
                }
                else if ( selection == "n")
                {
                    Main(null);
                }
                else
                {
                    Console.WriteLine("File does not exist \n");
                }
            }
        }

        private static void Exit()
        {
            Console.WriteLine("Do you want to exit? Y/N ");
            string exit = Console.ReadLine().ToLower();

            if (exit == "y")
            {
                Environment.Exit(0);
            }else if (exit == "n")
            {
                Main(null);
            }
            else
            {
                Exit();
            }
        }

        private static void Menu()
        {
            Console.WriteLine("-------------WELCOME STICKY SHARP-------------");
            Console.WriteLine("");
            Console.WriteLine("----------type 'new' to add new note----------");
            Console.WriteLine("----------type 'edit' to edit note------------");
            Console.WriteLine("----------type 'read' to read note------------");
            Console.WriteLine("----------type 'delete' to delete note--------");
            Console.WriteLine("----------type 'exit' to exit-----------------");
        }
    }
    
}