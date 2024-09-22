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
                FirstName = "�������",
                SecondName = "����"
            },
            new Author() {
                FirstName = "����",
                SecondName = "�����"
            },
            new Author() {
                FirstName = "������",
                SecondName = "����"
            },
            new Author() {
                FirstName = "�����",
                SecondName = "�������"
            },
            new Author() {
                FirstName = "����",
                SecondName = "��������"
            },
            new Author() {
                FirstName = "������",
                SecondName = "���������"
            },
            new Author() {
                FirstName = "������",
                SecondName = "�����"
            },
            new Author() {
                FirstName = "�����",
                SecondName = "�����������",
                MiddleName = "����������"
            },
            new Author() {
                FirstName = "����",
                SecondName = "���������",
                MiddleName = "�������"
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
           new BookSection() { Name = "������������ ����������" },
           new BookSection() { Name = "������������ ����������" },
           new BookSection() { Name = "������� ����������" }
        };
        context.BookSections.AddRange(bookSections);
        context.SaveChanges();
    }
} 