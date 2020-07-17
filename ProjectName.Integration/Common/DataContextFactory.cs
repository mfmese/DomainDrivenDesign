namespace Integration
{
    public static class DataContextFactory
    {
        public static LogDbIntegration.LogDbContext Create()
        {
            return new LogDbIntegration.LogDbContext("connection string");
        }
    }
}
