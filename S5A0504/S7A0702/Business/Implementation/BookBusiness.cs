using S6A0702.Moldel.Entities;
using S6A0702.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S6A0702.Business.Implementation
{
    public class BookBusiness : IBookBusiness
    {
        private IBookRepository _bookRepository;

        public BookBusiness(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAll() =>
            _bookRepository.GetAll()
        ;

        public Book GetById(long id) =>
            _bookRepository.GetById(id)
        ;
        public void Create(ref Book entity) =>
            _bookRepository.Create(ref entity)
        ;
        public void Update(ref Book entity)
        {
            if(!_bookRepository.Exists(entity.Id))
                throw new KeyNotFoundException($"Book {entity.Id} not found");
            _bookRepository.Update(ref entity);
        }
        public void DeleteById(long id)
        {
            if (_bookRepository.Exists(id))
                _bookRepository.DeleteById(id);
        }
    }
}
