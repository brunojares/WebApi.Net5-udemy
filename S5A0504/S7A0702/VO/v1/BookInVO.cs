using S6A0702.Moldel.Entities;
using System;

namespace S6A0702.VO.v1
{
    public class BookInVO : IVO<Book>
    {
        public string Author { get; set; }
        public DateTime? LaunchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }

        public virtual void CopyFrom(Book entity)
        {
            if (entity != null)
            {
                Author = entity.Author;
                LaunchDate = entity.LaunchDate;
                Price = entity.Price;
                Title = entity.Title;
            }
        }

        public virtual void CopyTo(ref Book entity)
        {
            if (entity != null)
            {
                entity.Author = Author;
                entity.LaunchDate = LaunchDate;
                entity.Price = Price;
                entity.Title = Title;
            }
        }
    }
}
