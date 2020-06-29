namespace projectRootNamespace.Api.Infrastructure
{
    public static class ComponentInfo
    {
        public static string Title { get; private set; }
        public static string ServerUrl { get; private set; }
        public static string ServerBaseUrl { get; private set; }

        public static void Initialize(string title, string serverUrl, string serverBaseUrl)
        {
            Title = title;
            ServerUrl = serverUrl;
            ServerBaseUrl = serverBaseUrl;
        }
    }
}
