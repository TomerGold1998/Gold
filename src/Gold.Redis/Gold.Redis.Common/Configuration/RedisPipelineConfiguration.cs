﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gold.Redis.Common.Configuration
{
    public class RedisPipelineConfiguration
    {
        public int MaxItemsPerRequest { get; set; } = 1000;
        public int MaxDegreeOfParallelism { get; set; } = 1;
        public TimeSpan MaxWaitTime { get; set; } = TimeSpan.FromSeconds(5);
    }
}