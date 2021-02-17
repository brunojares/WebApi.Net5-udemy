using Microsoft.AspNetCore.Mvc.ViewFeatures;
using S6A0702.Moldel.Entities;
using S6A0702.Moldels.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private WebApi001Context _webApi001Context;

        public BookRepository(WebApi001Context webApi001Context)
        {
            _webApi001Context = webApi001Context;
        }

        public IEnumerable<Book> GetAll() =>
            _webApi001Context.Books
        ;

        public Book GetById(long id) =>
            _webApi001Context.Books.FirstOrDefault(
                item => item.Id == id
            )
        ;

        public bool Exists(long id) =>
            _webApi001Context.Books.Count(item => item.Id == id) > 0
        ;
        public void Create(ref Book entity)
        {
            _webApi001Context.Books.Add(entity);
            _webApi001Context.SaveChanges();
        }
        public void Update(ref Book entity)
        {
            var _databaseEntity = GetById(entity.Id);
            _webApi001Context.Entry(_databaseEntity).CurrentValues.SetValues(entity);
            _webApi001Context.SaveChanges();
        }

        public void DeleteById(long id)
        {
            var _databaseEntity = GetById(id);
            _webApi001Context.Books.Remove(_databaseEntity);
            _webApi001Context.SaveChanges();
        }
    }
}
