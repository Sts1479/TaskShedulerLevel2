using System;
namespace TaskList {
    public class CommandResponse {
        ConsoleView _ConsoleView = new ConsoleView();
        SaveTaskList _SaveTaskList = new SaveTaskList();
        ParseInput _ParseInput = new ParseInput();
        PlanReminderObj _ComRespReminderObj;
        int _EditEventNum = 0;

        public CommandResponse() {}
        public void Response(CommandType cmdType) {
            switch (cmdType) {
                case CommandType.Help:
                    HandleHelp();
                    break;
                case CommandType.View:
                    HandleView();
                    break;
                case CommandType.New:
                    HandleNew();
                    break;
                case CommandType.NewDataDateTime:
                    HandleNewDateTime();
                    break;
                case CommandType.NewDataEventText:
                    HandleNewEventText();
                    break;
                case CommandType.Edit:
                    HandleEdit();
                    break;
                case CommandType.EditArgNum:
                    _EditEventNum = HandleEditTakeNum();
                    break;
                case CommandType.EditArgDateTime:
                    HandleEditDateTime(_EditEventNum);
                    break;
                case CommandType.EditArgEventText:
                    HandleEditEventText(_EditEventNum);
                    break;
                case CommandType.Delete:
                    _ConsoleView.DeleteViewStart();
                    break;
                case CommandType.DeleteArg:
                    HandleDelete();
                    _ConsoleView.DeleteViewComplete();
                    break;
                case CommandType.Save:
                    HandleSave();
                    break;
                case CommandType.Trash:
                    _ConsoleView.TrashView();
                    break;
                default:
                    break;
            }
        }

        private void HandleHelp() {
            _ConsoleView.HelpView();
        }

        private void HandleView() {
            int eventsNum = _SaveTaskList.GetPlanRemindersNum();
            if (eventsNum == 0) {
                _ConsoleView.SendTextToConsole("You have no events to view");
            }
            else {
                _ConsoleView.SendTextToConsole("Existing events found:");
                DateTime tDateTime = DateTime.Now;
                for (int i = 0; i < eventsNum; i++) {
                    PlanReminderObj reminderObj = _SaveTaskList.GetPlanReminder(i);
                    _ConsoleView.SendTextToConsoleAtSameLine((i + 1).ToString());
                    _ConsoleView.PasteSpaceToConsole();
                    reminderObj.ViewInfo();
                    if (reminderObj._DateTime.CompareTo(tDateTime) < 0) {
                        _ConsoleView.SendTextToConsoleAtSameLine(" - outdated\n");
                    }
                    else {
                        _ConsoleView.SendTextToConsoleAtSameLine("\n");
                    }
                }
            }
        }

        private void HandleNew() {
            _ConsoleView.NewViewDateTime();
            _ComRespReminderObj = new PlanReminderObj();
        }

        private void HandleNewDateTime() {
            string tString = _ParseInput.GetInputString();
            try {
                DateTime tDate = DateTime.ParseExact(tString, "yyyy-MM-dd HH:mm tt", null);
                _ComRespReminderObj._DateTime = tDate;
                _ConsoleView.NewViewEventText();
            }
            catch {
                _ParseInput.ResetCmd();
                _ConsoleView.NewDateTimeError();
            }
        }

        private void HandleNewEventText() {
            string teString = _ParseInput.GetInputString();
            _ComRespReminderObj._Text = teString;
            _SaveTaskList.SetPlanReminder(_ComRespReminderObj);
            _ConsoleView.NewViewEventComplete();
        }

        private void HandleEdit(){
            _ConsoleView.EditViewStart();
        }

        private int HandleEditTakeNum() {
            int eventsNum = _SaveTaskList.GetPlanRemindersNum();
            string tString = _ParseInput.GetInputString();
            int fromString = 0;
            try {
                fromString = (int.Parse(tString) - 1);
                if (fromString > eventsNum) {
                    _ConsoleView.SendTextToConsole("Sorry, you have less stored " +
                        "events than you've been entered. Try again.");
                }
                else {
                    _ComRespReminderObj = _SaveTaskList.GetPlanReminder(fromString);
                    _ConsoleView.SendTextToConsole("Enter new date and time of the event:");
                    _ConsoleView.SendTextToConsole(_ComRespReminderObj._DateTime.ToString());
                }
            }
            catch {
                _ParseInput.ResetCmd();
                _ConsoleView.SendTextToConsole("Sorry, can't recognize the number.");
            }
            return fromString;
        }

        private void HandleEditDateTime(int num) {
            string tString = _ParseInput.GetInputString();
            try {
                DateTime tDate = DateTime.ParseExact(tString, "yyyy-MM-dd HH:mm tt", null);
                SetStaticObjDateTime(tDate);
                _ConsoleView.SendTextToConsole("Enter new text of the event:");
                _ConsoleView.SendTextToConsole(_ComRespReminderObj._Text);
            }
            catch {
                _ParseInput.ResetCmd();
                _ConsoleView.NewDateTimeError();
            }
        }

        private void HandleEditEventText(int num) {
            string tString = _ParseInput.GetInputString();
            SetStaticObjText(tString);
            _SaveTaskList.SetPlanReminderAt(_ComRespReminderObj, num);
            _ConsoleView.EditViewComplete();
        }

        private void HandleDelete() {
            int eventsNum = _SaveTaskList.GetPlanRemindersNum();
            string tString = _ParseInput.GetInputString();
            int fromString = 0;
            try {
                fromString = (int.Parse(tString) - 1);
                if (fromString > eventsNum) {
                    _ConsoleView.SendTextToConsole("Sorry, you have less stored " +
                    	"events than you've been entered. Try again.");
                }
                else {
                    _SaveTaskList.RemovePlanReminderFromList(fromString);
                }
            }
            catch {
                _ParseInput.ResetCmd();
                _ConsoleView.SendTextToConsole("Sorry, can't recognize the number.");
            }

        }
        private void HandleSave() {
            _SaveTaskList.SaveToDoListToFile();
            _ConsoleView.SaveView();
        }
        private void SetStaticObjDateTime(DateTime dateTime) {
            _ComRespReminderObj._DateTime = dateTime;
        }
        private void SetStaticObjText(string text) {
            _ComRespReminderObj._Text = text;
        }
        public void TryToGetDataFromFile() {
            _SaveTaskList.ReadFile();
        }
        public void TryToSaveFile() {
            HandleSave();
        }
    }
}
