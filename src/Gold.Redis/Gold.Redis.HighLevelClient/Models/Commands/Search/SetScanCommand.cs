﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gold.Redis.HighLevelClient.Models.Commands.Search
{
    public class SetScanCommand : ScanCommandBase
    {
        public string SetKey { get; set; }
        public override string GetCommandString() => $"SSCAN {SetKey} " + Cursor + "" + GetMatchOption() + "" + GetCountOption();
    }
}