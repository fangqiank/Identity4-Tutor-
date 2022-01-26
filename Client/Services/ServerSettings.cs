namespace Client.Services
{
    public class ServerSettings
    {
        public string DiscoveryUrl { get; set; }
        public string  ClientName{ get; set; }
        public string ClientPassword { get; set; }
        public bool UseHttps { get; set; }
    }
}
