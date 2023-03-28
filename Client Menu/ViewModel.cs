using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Client_Menu
{
    [AddINotifyPropertyChangedInterface]
    class ViewModel
    {
        private ObservableCollection<AllMessage> processes;
        public ViewModel()
        {
            processes = new ObservableCollection<AllMessage>();
        }
        public IEnumerable<AllMessage> Processes => processes;
        public string address { get; set; }
        public int port { get; set; }
        public void AddProgress(AllMessage info)
        {
            processes.Add(info);
        }
    }

    [AddINotifyPropertyChangedInterface]
    class AllMessage
    {
        public AllMessage() { }
        public AllMessage(string text)
        {
            Message = text;
        }
        public SolidColorBrush color { get; set; }
        public string Message { get; set; }
        public string ReceivedMessage { get; set; }
        public DateTime Time { get; set; }
    }
}
