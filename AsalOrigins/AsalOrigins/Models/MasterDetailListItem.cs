﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AsalOrigins.Models
{
    class MasterDetailListItem
    {
        public string Title { get; set; }
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }
}