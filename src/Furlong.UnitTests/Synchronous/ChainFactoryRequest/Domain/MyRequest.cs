﻿using System.Collections.Generic;

namespace Furlong.UnitTests.Synchronous.ChainFactoryRequest.Domain
{
    public class MyRequest
    {
        public List<string> Visited { get; } = new List<string>();
    }
}