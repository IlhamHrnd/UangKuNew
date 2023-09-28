﻿using UangKu.Model.Base;

namespace UangKu.Model.Menu
{
    public class Home : BaseModel
    {
        public Home()
        {
            
        }
        private DateTime datetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 30, 0);
        public DateTime DateTime { get => datetime; set => SetProperty(ref datetime, value); }
        private string title = "Home Page";
        public string Title { get => title; set => SetProperty(ref title, value); }
        private string name = string.Empty;
        public string Name { get => name; set => name = value; }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private string image = string.Empty;
        public string Image { get => image; set => image = value; }
    }
}
