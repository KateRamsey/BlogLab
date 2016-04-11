using System.Collections.Generic;
using BlogLab.Models;
using FizzWare.NBuilder;

namespace BlogLab.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BlogDBContext";
        }

        protected override void Seed(BlogDBContext context)
        {
            if (!context.BlogPosts.Any())
            {
                List<BlogPost> posts = (List<BlogPost>)Builder<BlogPost>.CreateListOfSize(10)
                    .All().With(a => a.Author = Faker.NameFaker.Name())
                    .With(x=>x.CreateDate = Faker.DateTimeFaker.DateTime(DateTime.Now.AddMonths(-4), DateTime.Now))
                    .With(b=>b.Body = Faker.TextFaker.Sentences(30))
                    .With(t=>t.Title = Faker.StringFaker.Alpha(30))
                    .With(p=>p.ImageLink = "http://i.imgur.com/eQjT6JF.jpg")
                    .Random(4).With(p => p.ImageLink = "http://i.imgur.com/0WHwXdl.jpg")
                    .Random(4).With(p => p.ImageLink = "http://i.imgur.com/yecyFDs.jpg")
                    .Random(4).With(p => p.ImageLink = "http://i.imgur.com/iH3eNxZ.jpg")
                    .Build();

                context.BlogPosts.AddRange(posts);
                context.SaveChanges();
            }
        }
    }
}
