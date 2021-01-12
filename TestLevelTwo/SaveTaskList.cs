using System;
using System.Collections.Generic;

namespace TaskList
{
    public struct PlanReminderObj {
        public DateTime _DateTime { get; set; }
        public string _Text { get; set; }
        public void ViewInfo() {
            Console.Write($"Date and time: {_DateTime}  ToDo: {_Text}");
        }
    }
    
    public class SaveTaskList {
        List<PlanReminderObj> _TaskList = new List<PlanReminderObj>();
        TaskListStorageFile _TaskListStorageFile = new TaskListStorageFile();
        public SaveTaskList() {
        }
        public void SetPlanReminder(PlanReminderObj planObj) {
            _TaskList.Add(planObj);
            try {
                _TaskList.Sort(CompareDates);
            }
            catch {
                return;
            }
        }
        public void SetPlanReminderAt(PlanReminderObj planObj, int num) {
            _TaskList[num] = planObj;
            try {
                _TaskList.Sort(CompareDates);
            }
            catch {
                return;
            }
        }
        public PlanReminderObj GetPlanReminder(int num) {
            return _TaskList[num];
        }
        public int GetPlanRemindersNum() {
            return _TaskList.Count;
        }
        public void RemovePlanReminderFromList(int num) {
            _TaskList.RemoveAt(num);
        }
        private static int CompareDates(PlanReminderObj first, PlanReminderObj second) {
            return first._DateTime.CompareTo(second._DateTime);
        }
        private void ParseFileToList() {
            int state = _TaskListStorageFile.OpenFile();
            if (state.Equals(-1)) {
                return; // file empty or not exists
            }
            else {
                string tStr;
                while ((tStr = _TaskListStorageFile.GetTheNextStringFromFile()) != null) {
                    PlanReminderObj planReminderObj = new PlanReminderObj();
                    try {
                        DateTime tDate = DateTime.Parse(tStr);
                        planReminderObj._DateTime = tDate;
                        tStr = _TaskListStorageFile.GetTheNextStringFromFile();
                        planReminderObj._Text = tStr;
                        SetPlanReminder(planReminderObj);
                    }
                    catch {
                        return; // something wrong with data in file
                    }
                }
            }
        }
        public void ReadFile() {
            ParseFileToList();
        }
        public void SaveToDoListToFile() {
            int eventsNum = _TaskList.Count;
            PlanReminderObj planReminderObj;
            for (int i = 0; i < eventsNum; i++){
                planReminderObj = GetPlanReminder(i);
                _TaskListStorageFile.WriteLineToFile(planReminderObj._DateTime.ToString());
                _TaskListStorageFile.WriteLineToFile(planReminderObj._Text);
            }
        }
    }
}
