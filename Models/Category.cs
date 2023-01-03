using System.Text.Json.Serialization;

namespace ApiInventoryControl.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public IList<Product> Products { get; set; }
    }
}
