using db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seeder {
    class Program {
        static void Main(string[] args) {
            using(Context db = new Context()) {
                List<User> users = FakeO.Create.New<User>(1000,
                    c => c.Email = FakeO.Internet.Email(),
                    c => c.Name = FakeO.Name.First(),
                    c => c.Surname = FakeO.Name.Last(),
                    c => c.Nickname = FakeO.Internet.UserName(),
                    c => c.Password = FakeO.String.Random(12),
                    c => c.Trusted = FakeO.Number.Next(0, 90) == 1
                ).ToList<User>();

                List<Shelf> shelves = FakeO.Create.New<Shelf>(20,
                    c => c.Name = FakeO.String.Random(10)
                ).ToList();

                List<Book> books = FakeO.Create.New<Book>(2000,
                    c => c.Title = FakeO.Lorem.Sentence(1),
                    c => c.Release = FakeO.Data.Random<DateTime>()
                ).ToList();

                List<Author> authors = FakeO.Create.New<Author>(400,
                    c => c.Name = FakeO.Name.First(),
                    c => c.Surname = FakeO.Name.Last()
                ).ToList();

                foreach(Book book in books) {
                    int k = FakeO.Number.Next(0, 400);
                    int n = FakeO.Number.Next(0, 4);
                    while(k + n > 400) {
                        k -= 1;
                    }
                    for(int i = k; i < k + n; ++i) {
                        Author author = authors[i];
                        author.Books.Add(book);
                        book.Authors.Add(author);
                    }

                    k = FakeO.Number.Next(0, 20);
                    n = FakeO.Number.Next(0, 3);
                    while(k + n > 20) {
                        k -= 1;
                    }
                    for(int i = k; i < k + n; ++i) {
                        Shelf shelf = shelves[i];
                        shelf.Books.Add(book);
                        book.Shelves.Add(shelf);
                    }
                }

                foreach(User user in users) {
                    Book book = books[FakeO.Number.Next(0, books.Count - 1)];
                    List<Comment> comments = FakeO.Create.New<Comment>(0, 30,
                        c => c.User = user,
                        c => c.Content = string.Join(". ", FakeO.Lorem.Sentences(5)),
                        c => c.Book = book
                    ).ToList();
                    foreach(Comment c in comments) {
                        user.Comments.Add(c);
                        book.Comments.Add(c);
                    }

                    Author author = authors[FakeO.Number.Next(0, authors.Count - 1)];
                    comments = FakeO.Create.New<Comment>(0, 10,
                        c => c.User = user,
                        c => c.Content = string.Join(". ", FakeO.Lorem.Sentences(5)),
                        c => c.Author = author
                    ).ToList();
                    foreach(Comment c in comments) {
                        user.Comments.Add(c);
                        author.Comments.Add(c);
                    }

                    int k = FakeO.Number.Next(0, 20);
                    int n = FakeO.Number.Next(0, 7);
                    while(k + n > 20) {
                        k -= 1;
                    }
                    for(int j = k; j < k + n; ++j) {
                        Shelf shelf = shelves[j];
                        shelf.Users.Add(user);
                        user.Shelves.Add(shelf);
                    }

                    db.Users.Add(user);
                }

                List<Admin> admins = FakeO.Create.New<Admin>(50,
                    c => c.Email = FakeO.Internet.Email(),
                    c => c.Name = FakeO.Name.First(),
                    c => c.Surname = FakeO.Name.Last(),
                    c => c.Nickname = FakeO.Name.FullName(),
                    c => c.Password = FakeO.String.Random(12),
                    c => c.Trusted = true
                ).ToList();

                foreach(Admin admin in admins) {
                    db.Admins.Add(admin);
                }

                db.SaveChanges();
            }
        }
    }
}
