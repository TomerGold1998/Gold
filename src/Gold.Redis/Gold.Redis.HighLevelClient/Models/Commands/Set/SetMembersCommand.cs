﻿namespace Gold.Redis.HighLevelClient.Models.Commands.Set
{
    public class SetMembersCommand : Command
    {
        public string SetKey { get; set; }
        public override string GetCommandString() => $"SMEMBERS {SetKey}";
    }
}