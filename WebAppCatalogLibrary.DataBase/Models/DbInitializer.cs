using Microsoft.EntityFrameworkCore;

namespace WebAppCatalogLibrary.DataBase.Models;

public static class DbInitializer
{
    public static void Initialize(DbPostgresContext context)
    {
        context.Database.Migrate();
        InitializeAuthors(context);
        InitializeBookSections(context);
    }
    private static void InitializeAuthors(DbPostgresContext context)
    {
        if (context.Authors.Any())
        {
            return;
        }

        var authors = new[] {
            new Author() {
                FirstName = "Дональд",
                SecondName = "Кнут"
            },
            new Author() {
                FirstName = "Эрих",
                SecondName = "Гамма"
            },
            new Author() {
                FirstName = "Ричард",
                SecondName = "Хелм"
            },
            new Author() {
                FirstName = "Ральф",
                SecondName = "Джонсон"
            },
            new Author() {
                FirstName = "Джон",
                SecondName = "Владимир"
            },
            new Author() {
                FirstName = "Брайан",
                SecondName = "Кернинган"
            },
            new Author() {
                FirstName = "Деннис",
                SecondName = "Ритчи"
            },
            new Author() {
                FirstName = "Федор",
                SecondName = "Достоевсикй",
                MiddleName = "Михайлович"
            },
            new Author() {
                FirstName = "Иван",
                SecondName = "Антонович",
                MiddleName = "Ефремов"
            }
        };
        context.Authors.AddRange(authors);
        context.SaveChanges();
    }  
    private static void InitializeBookSections(DbPostgresContext context)
    {
        if (context.BookSections.Any())
        {
            return;
        }

        var bookSections = new[] {
           new BookSection() { Name = "Компьютерная литература" },
           new BookSection() { Name = "Классическая литература" },
           new BookSection() { Name = "Научная фантастика" }
        };
        context.BookSections.AddRange(bookSections);
        context.SaveChanges();
    }
} 