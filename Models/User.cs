using System.Text.Json.Serialization;

namespace ApiInventoryControl.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public IList<Role> Roles { get; set; }
    }
}
