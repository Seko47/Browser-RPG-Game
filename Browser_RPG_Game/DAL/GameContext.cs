using Browser_RPG_Game.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Browser_RPG_Game.DAL
{
    public class GameContext : DbContext
    {
        public GameContext() : base("DefaultConnection")
        { }

        public DbSet<Models.Attribute> Attributes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterAttribute> CharacterAttributes { get; set; }
        public DbSet<CharacterEquipment> CharacterEquipment { get; set; }
        public DbSet<CharacterItem> CharacterItems { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<EnemyLocation> EnemyLocations { get; set; }
        public DbSet<EquipmentSlot> EquipmentSlots { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemAttribute> ItemAttributes { get; set; }
        public DbSet<ItemLoot> ItemLoots { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Loot> Loots { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileType> ProfileTypes { get; set; }
        public DbSet<Bulletin> Bulletins { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ItemStore> ItemStores { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<BuildingProperty> BuildingProperties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}