using todo_db.Models;
using Microsoft.EntityFrameworkCore;

namespace todo_db
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Item { get; set; }
    }
}
