﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PTAData.Entities;

namespace HickoryPTAApp.Models
{
    public class BaseRepository
    {
        protected void UpdateAutoGeneratedFields(object obj, string user, bool newItem)
        {
            var item = obj as IAutoGenerateFields;

            if (item == null)
                return;

            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternZone);

            if (newItem)
                item.CreatedOn = easternTime;

            item.UserModified = user;
            item.LastModified = easternTime;
        }
    }
}