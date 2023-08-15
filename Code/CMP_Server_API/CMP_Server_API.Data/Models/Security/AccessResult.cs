namespace CMP_Server_API.CMP_Server_API.Data.Models.Security
{
    public class AccessResult
    {
        public string token { get; set; }
        public string expiry { get; set; }

        public AccessResult() { }
        public AccessResult(string token, string expiry)
        {
            this.token = token;
            this.expiry = expiry;
        }
    }
}
