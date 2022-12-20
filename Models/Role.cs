namespace ApiInventoryControl.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<User> Users { get; set; }
    }
}
