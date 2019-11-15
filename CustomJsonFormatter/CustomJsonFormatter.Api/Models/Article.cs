using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomJsonFormatter.Api.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid AuthorId { get; set; }

        public Article()
        {
            Id = Guid.NewGuid();
            AuthorId = Guid.NewGuid();
            var lorem = new Bogus.DataSets.Lorem();
            Title = lorem.Sentence();
            Description = lorem.Paragraph(5);
        }

        public Article(Guid id) : this()
        {
            Id = id;
        }

    }
}
