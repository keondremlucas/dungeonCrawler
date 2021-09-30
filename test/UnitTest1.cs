using System;
using Xunit;
using lib;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace test
{
    public class Test1
    {


        private DbContextOptions<Database> _options;

        public Test1()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();

            _options = new DbContextOptionsBuilder<Database>()
                .UseSqlite(conn)
                .Options;

        }



        [Fact]
        public void ShouldCreateMonster()
        {
            using var context = new Database(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Add(new Monster() { Name = "Spider",Description = "scarySpider",Type="Aracnid",Level = 20,Health=20,Energy=20,Location="web"});
            context.SaveChanges();

            context.Monsters.Count().Should().Be(1);
        }
        [Fact]
        public void ShouldCreateItem()
        {
            using var context = new Database(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Add(new Item() { Name = "Spider",Description = "scarySpider",Type="Aracnid",Level = 20});
            context.SaveChanges();

            context.Items.Count().Should().Be(1);
        }
        [Fact]
        public void ShouldCreateCharacter()
        {
            using var context = new Database(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Add(new Character() { Name = "Spider",Description = "scarySpider",Type="Aracnid",Level = 20,Health=20,Energy=20,Location="web"});
            context.SaveChanges();

            context.Characters.Count().Should().Be(1);
        }
        [Fact]
        public void ShouldCreateLog()
        {
            using var context = new Database(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Add(new Log() { Description = "scarySpider"});
            context.SaveChanges();

            context.Logs.Count().Should().Be(1);
        }
        [Fact]
        public void ShouldCreateMap()
        {
            using var context = new Database(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Add(new Location() { Name = "Spider",Description = "scarySpider",Type="Aracnid"});
            context.SaveChanges();

            context.Map.Count().Should().Be(1);
        }
        [Fact]
        public void ShouldAddItemToCharacter()
        {
            using var context = new Database(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var Character = (new Character() { Name = "Spider",Description = "scarySpider",Type="Aracnid",Level = 20,Health=20,Energy=20,Location="web"});
            var Item = new Item() { Name = "Spider",Description = "scarySpider",Type="Aracnid",Level = 20};
            Character.Items.Add(Item);
            context.Add(Character);
            context.SaveChanges();
            context.Characters.First().Items.Count().Should().Be(1);
            context.Characters.First().Items.First().Should().Be(context.Items.First());
        }
        [Fact]
        public void ShouldAddItemToMonster()
        {
            using var context = new Database(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var Monster = (new Monster() { Name = "Spider",Description = "scarySpider",Type="Aracnid",Level = 20,Health=20,Energy=20,Location="web"});
            var Item = new Item() { Name = "Spider",Description = "scarySpider",Type="Aracnid",Level = 20};
            Monster.Items.Add(Item);
            context.Add(Monster);
            context.SaveChanges();
            context.Monsters.First().Items.Count().Should().Be(1);
            context.Monsters.First().Items.First().Should().Be(context.Items.First());
        }


    }
}
