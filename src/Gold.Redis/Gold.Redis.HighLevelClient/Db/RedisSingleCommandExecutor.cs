﻿using Gold.Redis.LowLevelClient.Interfaces;
using System;
using System.Threading.Tasks;
using Gold.Redis.Common.Interfaces.Pipelining;
using Gold.Redis.HighLevelClient.Interfaces;
using Gold.Redis.HighLevelClient.Models.Commands;
using Gold.Redis.LowLevelClient.Responses;

namespace Gold.Redis.HighLevelClient.Db
{
    public class RedisSingleCommandExecutor : IRedisCommandExecutor
    {
        private readonly IRedisCommandHandler _redisCommandHandler;

        public RedisSingleCommandExecutor(
            IRedisCommandHandler redisConnection)
        {
            _redisCommandHandler = redisConnection;
        }
        public async Task<T> Execute<T>(Command command) where T : Response
        {
            var commandStr = command.GetCommandString();
            if (string.IsNullOrEmpty(commandStr))
            {
                throw new InvalidOperationException("Can not execute null command");
            }

            return await _redisCommandHandler.ExecuteCommand<T>(commandStr);
        }
    }
}