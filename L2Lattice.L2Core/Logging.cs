using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace L2Lattice.L2Core
{
    public static class Logging
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();
        public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();
    }
}
