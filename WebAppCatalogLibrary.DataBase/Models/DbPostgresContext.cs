using System;
using System.Collections.Generic;
using System.Data.Common;

using Microsoft.EntityFrameworkCore;

namespace WebAppCatalogLibrary.DataBase.Models;

public partial class DbPostgresContext : DbContext
{
    public DbPostgresContext(DbContextOptions<DbPostgresContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<AssociationBookAuthor> AssociationBookAuthors { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookSection> BookSections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssociationBookAuthor>(entity =>
        {
            entity.HasKey(e => e.AssociationBookAuthorId).HasName("association_book_author_pkey");

            entity.ToTable("association_book_author");

            entity.Property(e => e.AssociationBookAuthorId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("association_book_author_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(now() AT TIME ZONE 'UTC'::text)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");

            entity.HasOne(d => d.Author).WithMany(p => p.AssociationBookAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("association_book_author_author_id_fkey");

            entity.HasOne(d => d.Book).WithMany(p => p.AssociationBookAuthors)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("association_book_author_book_id_fkey");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("author_pkey");

            entity.ToTable("author");

            entity.Property(e => e.AuthorId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("author_id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(now() AT TIME ZONE 'UTC'::text)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.MiddleName)
                .IsRequired(false)
                .HasColumnType("character varying")
                .HasColumnName("middle_name");
            entity.Property(e => e.SecondName)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("second_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("book_pkey");

            entity.ToTable("book");

            entity.Property(e => e.BookId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("book_id");
            entity.Property(e => e.BookSectionId).HasColumnName("book_section_id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(now() AT TIME ZONE 'UTC'::text)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Location)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("location");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("title");
            entity.Property(e => e.Tom).HasColumnName("tom");

            entity.HasOne(d => d.BookSection).WithMany(p => p.Books)
                .HasForeignKey(d => d.BookSectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("book_book_section_id_fkey");
        });

        modelBuilder.Entity<BookSection>(entity =>
        {
            entity.HasKey(e => e.BookSectionId).HasName("book_section_pkey");

            entity.ToTable("book_section");

            entity.Property(e => e.BookSectionId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("book_section_id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(now() AT TIME ZONE 'UTC'::text)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("name");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
