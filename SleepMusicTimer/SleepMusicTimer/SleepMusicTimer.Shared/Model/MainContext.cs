using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SleepMusicTimer.Model
{
    public class MainContext
    {
        public ObservableCollection<int> Hours { get; set; }

        public ObservableCollection<int> Minutes { get; set; }

        public MainContext()
        {
            Hours = new ObservableCollection<int>(TimerModel.Hours);
            Minutes = new ObservableCollection<int>(TimerModel.Minutes);
        }

    }
}
