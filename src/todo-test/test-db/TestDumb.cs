using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using todo_db;
using Xunit;

namespace todo_test.test_db
{
    public class TestDumb
    {
        private readonly TodoContext _context;

        private Guid GuidIdTest;

        public TestDumb()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase("EFMemoryToDoTest");

            _context = new TodoContext(dbContextOptionsBuilder.Options);

            GuidIdTest = Guid.NewGuid();
        }

        [Fact]
        public void TestCRUD()
        {
            Assert.False(_context.Item.Where(w => w.Id == GuidIdTest).Any());

            _context.Item.Add(new todo_db.Models.Item
            {
                Id = GuidIdTest,
                Name = "ToDo Test",
                IsComplete = false
            });
            _context.SaveChanges();

            Assert.True(_context.Item.Where(w => w.Id == GuidIdTest).Any());

            var itemAdded = _context.Item.Where(w => w.Id == GuidIdTest).Single();

            itemAdded.IsComplete = true;

            _context.SaveChanges();

            Assert.False(_context.Item.Where(w => w.Id == GuidIdTest && !w.IsComplete).Any());

            var itemToDelete = _context.Item.Where(w => w.Id == GuidIdTest).Single();

            _context.Remove(itemToDelete);

            _context.SaveChanges();

            Assert.False(_context.Item.Where(w => w.Id == GuidIdTest).Any());
        }
    }
}
