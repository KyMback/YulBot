using System;

namespace YulBot.Infrastructure.Common.Utils
{
    public static class EnvironmentUtils
    {
        public static string GetCurrentEnvironment =>
            Environment.GetEnvironmentVariable(SystemConstants.EnvironmentKey);
    }
}