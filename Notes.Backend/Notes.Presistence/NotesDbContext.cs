﻿using Microsoft.EntityFrameworkCore;
using Notes.Aplication.Interfaces;
using Notes.Domain;
using Notes.Presistence.EntityTypeConfigurations;

namespace Notes.Presistence
{
    public class NotesDbContext : DbContext, INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesDbContext(DbContextOptions<NotesDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
