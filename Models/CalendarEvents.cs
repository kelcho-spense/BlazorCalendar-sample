using System;
using System.Collections.Generic;

namespace BlazorSample.Models
{
    public class CalendarEvent
    {


        public string Subject { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Color { get; private set; }
    }
}