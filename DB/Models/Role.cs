namespace DB //QoolloSSO.DataBase.Models
{
    public class Role
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Users { get; set; }
    }
}