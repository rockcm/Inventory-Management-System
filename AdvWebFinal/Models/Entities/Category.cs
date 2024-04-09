namespace AdvWebFinal.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<ProductCategory> CategoryProduct { get; set; } = new List<ProductCategory>();

    }
}
