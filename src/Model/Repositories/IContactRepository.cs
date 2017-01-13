using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> Get(string searchString);

        IEnumerable<Contact> GetById(int id);
        IEnumerable<Contact> GetByTagId(int id);
        IEnumerable<Tag> GetTags(string searchString);
        bool Any(int id);
        void Update(Tag tag);
        void Delete(int id);
        void Add(Tag tag);
    }
}
