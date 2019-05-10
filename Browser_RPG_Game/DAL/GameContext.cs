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

        public DbSet<Character> Characters { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemLoot> ItemLoots { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Loot> Loots { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileType> ProfileTypes { get; set; }
        public DbSet<Bulletin> Bulletins { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Character>()
                .HasOptional<Item>(c => c.Helmet)
                .WithMany(i => i.CharactersHelmets)
                .HasForeignKey(c => c.HelmetID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Character>()
                .HasOptional<Item>(c => c.Armor)
                .WithMany(i => i.CharactersArmors)
                .HasForeignKey(c => c.ArmorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Character>()
                .HasOptional<Item>(c => c.Gloves)
                .WithMany(i => i.CharactersGloves)
                .HasForeignKey(c => c.GlovesID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Character>()
                .HasOptional<Item>(c => c.Boots)
                .WithMany(i => i.CharactersBoots)
                .HasForeignKey(c => c.BootsID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Character>()
                .HasOptional<Item>(c => c.Weapon)
                .WithMany(i => i.CharactersWeapons)
                .HasForeignKey(c => c.WeaponID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Character>()
                .HasOptional<Item>(c => c.Shield)
                .WithMany(i => i.CharactersShields)
                .HasForeignKey(c => c.ShieldID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired<Character>(m => m.Sender)
                .WithMany(c => c.SendedMessages)
                .HasForeignKey(m => m.SenderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired<Character>(m => m.Receiver)
                .WithMany(c => c.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverID)
                .WillCascadeOnDelete(false);
        }
    }
}