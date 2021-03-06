﻿using S6A0702.Moldel.Entities;
using System.Collections.Generic;

namespace S6A0702.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person SetEnabled(long id, bool enabled);
        IEnumerable<Person> GetByFilter(string filter);
    }
}
