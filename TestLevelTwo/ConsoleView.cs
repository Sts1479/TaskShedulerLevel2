using System;
namespace TaskList
{
    public class ConsoleView {
        DateTime _StartDateAndTime = DateTime.Now;
        public ConsoleView() { }
        public void StartView() {
            Console.WriteLine("Welcome to the Planner app");
            Console.WriteLine("Today is {0:d} at {0:T}.", _StartDateAndTime);
            Console.WriteLine("Enter 'help' to view accepted commands");
        }
        public void HelpView() {
            Console.WriteLine("There is a simple commands list:");
            Console.WriteLine("'help' - to view this menu");
            Console.WriteLine("'view' - to view all stored events");
            Console.WriteLine("'new' - to add new item to list");
            Console.WriteLine("'edit' - to edit existing element from list");
            Console.WriteLine("'delete' - to remove an item from list");
            Console.WriteLine("'save' - to save all events to local file");
            Console.WriteLine("'exit' - to complete application run");
        }
        public void NewViewDateTime() {
            Console.WriteLine("Please, enter event date and time in format " +
            	"'yyyy-mm-dd hh:mm tt', where 'tt' is AM or PM:");
        }

        public void NewDateTimeError() {
            Console.WriteLine("Sorry, time and date is wrong or incomplete. " +
            	"Please, try any command again.");
        }
        public void NewViewEventText() {
            Console.WriteLine("Please, enter event text:");
        }
        public void NewViewEventComplete() {
            Console.WriteLine("Event was successfully stored.");
        }
        public void EditViewStart() {
            Console.WriteLine("Please, enter event number to edit:");
        }
        public void EditViewComplete() {
            Console.WriteLine("Event was successfully edited.");
        }
        public void DeleteViewStart() {
            Console.WriteLine("Please, enter event number to delete:");
        }
        public void DeleteViewComplete() {
            Console.WriteLine("Event was successfully deleted.");
        }
        public void SaveView() {
            Console.WriteLine("Task list successfully saved.");
        }
        public void TrashView() {
            Console.WriteLine("Sorry, command is wrong or incomplete. " +
            	"Please, try again.");
        }
        public void SendTextToConsole(string str) {
            Console.WriteLine(str);
        }
        public void SendTextToConsoleAtSameLine(string str) {
            Console.Write(str);
        }
        public void PasteSpaceToConsole() {
            Console.Write(" ");
        }
    }
}
