using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{
    public class CommitteeEvent : Post
    {
        public int CommitteeId { get; set; }
        public virtual Committee Committee { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// I know this is bad but I'm just trying to 
        /// finish this request.
        /// </summary>
        [NotMapped]
        public DateTime ViewEndDate { get; set; }

        public virtual Location Location { get; set; }

        // Date to compare against DateTime.Today
        public DateTime CompareDate()
        {
            if (EndDate.HasValue)
                return EndDate.Value.Date;
            else 
                return EventDate.Date;
        }

        public string DateString()
        {
            string dateString = EventDate.ToString("MMM d");
            if (EndDate.HasValue)
            {
                if (EventDate.Date < EndDate.Value.Date)
                {
                    if (EventDate.Date.Month == EndDate.Value.Date.Month)
                        dateString += String.Format(" - {0}", EndDate.Value.Day); 
                    else
                        dateString += String.Format(" - {0}", EndDate.Value.ToString("MMM d"));
                }
            }

            return dateString;
        }

        public string TimeString()
        {
            string timeString = EventDate.ToShortTimeString();
            if (EndDate.HasValue)
            {
                if (EventDate.Date == EndDate.Value.Date)
                {
                    timeString += String.Format(" - {0}", EndDate.Value.ToShortTimeString());
                }
            }
            return timeString;
        }

        public bool DateOnly()
        {
            if (EndDate.HasValue)
            {
                if (EventDate.Date < EndDate.Value.Date)
                    return true;
            }

            return false;
        }
    }
}
