﻿using System.Text;
using Gold.Redis.Common;
using Gold.Redis.LowLevelClient.Interfaces.Parsers;

namespace Gold.Redis.LowLevelClient.Parsers
{
    public class RequestBuilder : IRequestBuilder
    {
        public string Build(string request)
        {
            var separatedCommands = request.Split(' ');
            var builder = new StringBuilder();

            var requestStartString = $"{CommandPrefixes.Array}{separatedCommands.Length}{Constants.CrLf}";
            builder.Append(requestStartString);

            foreach (var commandContent in separatedCommands)
            {
                var command = $"{CommandPrefixes.BulkString}{commandContent.Length}{Constants.CrLf}{commandContent}{Constants.CrLf}";
                builder.Append(command);
            }

            return builder.ToString();
        }
    }
}
