using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Entities;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;

namespace Model.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDbConnection _db;
        private readonly IOptions<Setting> _settings;
        public ContactRepository(IOptions<Setting> settings)
        {
            _settings = settings;
        }

        public IEnumerable<Contact> Get(string searchString)
        {
            using (var conn = new SqlConnection(_settings.Value.DefaultConnection))
            {
                conn.Open();
                var lookup = new Dictionary<int, Contact>();

                var result = conn.Query<Contact, ContactTag, Tag, ContactCommunicationDetail, CommunicationType, Contact>(@"
                        SELECT a.*,b.*, c.*, e.*, f.*
                    FROM Contacts a
                    INNER JOIN ContactTags b ON b.ContactId = a.Id     
                    INNER JOIN Tags c ON b.TagId = c.Id
                    INNER JOIN ContactCommunicationDetails e ON e.ContactId = a.Id     
                    INNER JOIN CommunicationTypes f ON e.CommunicationTypeId = f.Id
                    Where ( a.FirstName like  CONCAT('%',@searchString,'%') ) ", (c, p, d, e, f) =>
                {
                    Contact contact;

                    if (!lookup.TryGetValue(c.Id, out contact))
                        lookup.Add(c.Id, contact = c);

                    if (contact.ContactTags == null)
                        contact.ContactTags = new List<ContactTag>();

                    if (contact.ContactCommunicationDetails == null)
                        contact.ContactCommunicationDetails = new List<ContactCommunicationDetail>();

                    p.Tag = d;

                    p.Contact = contact;

                    if (!contact.ContactTags.Any(a => a.Id == p.Id))
                        contact.ContactTags.Add(p);

                    e.CommunicationType = f;

                    e.Contact = contact;

                    if (!contact.ContactCommunicationDetails.Any(a => a.Id == e.Id))
                        contact.ContactCommunicationDetails.Add(e);

                    return contact;
                }, new { searchString = searchString });

                return result.Distinct().ToList();
            }
        }

        public IEnumerable<Tag> GetTags(string searchString)
        {
            using (var conn = new SqlConnection(_settings.Value.DefaultConnection))
            {
                conn.Open();
                var query = @" SELECT a.* FROM Tags a  Where ( a.Description like  CONCAT('%',@searchString,'%') ) ";
                var result = conn.Query<Tag>(query, new { searchString = searchString });
                return result.ToList();
            }
        }

        public bool Any(int id)
        {
            using (var conn = new SqlConnection(_settings.Value.DefaultConnection))
            {
                conn.Open();
                var exists = conn.ExecuteScalar<bool>("select count(1) from Tags where Id=@id", new { id });
                return exists;
            }
        }

        public void Update(Tag tag)
        {
            using (var connection = new SqlConnection(_settings.Value.DefaultConnection))
            {
                connection.Open();
                connection.ExecuteScalar("usp_UpdateTag", new { Id = tag.Id, Description = tag.Description }, commandType: CommandType.StoredProcedure);
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_settings.Value.DefaultConnection))
            {
                connection.Open();
                connection.ExecuteScalar("usp_DeleteTag", new { Id = id }, commandType: CommandType.StoredProcedure);
            }
        }

        public void Add(Tag tag)
        {
            using (var connection = new SqlConnection(_settings.Value.DefaultConnection))
            {
                connection.Open();
                connection.ExecuteScalar("usp_InsertTag", new { Description = tag.Description }, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Contact> GetById(int id)
        {
            using (var conn = new SqlConnection(_settings.Value.DefaultConnection))
            {
                conn.Open();
                var lookup = new Dictionary<int, Contact>();

                var result = conn.Query<Contact, ContactTag, Tag, ContactCommunicationDetail, CommunicationType, Contact>(@"
                        SELECT a.*,b.*, c.*, e.*, f.*
                    FROM Contacts a
                    INNER JOIN ContactTags b ON b.ContactId = a.Id     
                    INNER JOIN Tags c ON b.TagId = c.Id
                    INNER JOIN ContactCommunicationDetails e ON e.ContactId = a.Id     
                    INNER JOIN CommunicationTypes f ON e.CommunicationTypeId = f.Id
                    Where a.id = @id ", (c, p, d, e, f) =>
                {
                    Contact contact;

                    if (!lookup.TryGetValue(c.Id, out contact))
                        lookup.Add(c.Id, contact = c);

                    if (contact.ContactTags == null)
                        contact.ContactTags = new List<ContactTag>();

                    if (contact.ContactCommunicationDetails == null)
                        contact.ContactCommunicationDetails = new List<ContactCommunicationDetail>();

                    p.Tag = d;

                    p.Contact = contact;

                    if (!contact.ContactTags.Any(a => a.Id == p.Id))
                        contact.ContactTags.Add(p);

                    e.CommunicationType = f;

                    e.Contact = contact;

                    if (!contact.ContactCommunicationDetails.Any(a => a.Id == e.Id))
                        contact.ContactCommunicationDetails.Add(e);

                    return contact;
                }, new { id = id });

                return result.Distinct().ToList();
            }
        }

        public IEnumerable<Contact> GetByTagId(int id)
        {
            using (var conn = new SqlConnection(_settings.Value.DefaultConnection))
            {
                conn.Open();
                var lookup = new Dictionary<int, Contact>();

                var result = conn.Query<Contact, ContactTag, Tag, ContactCommunicationDetail, CommunicationType, Contact>(@"
                        SELECT a.*,b.*, c.*, e.*, f.*
                    FROM Contacts a
                    INNER JOIN ContactTags b ON b.ContactId = a.Id     
                    INNER JOIN Tags c ON b.TagId = c.Id
                    INNER JOIN ContactCommunicationDetails e ON e.ContactId = a.Id     
                    INNER JOIN CommunicationTypes f ON e.CommunicationTypeId = f.Id
                    Where c.id = @id ", (c, p, d, e, f) =>
                {
                    Contact contact;

                    if (!lookup.TryGetValue(c.Id, out contact))
                        lookup.Add(c.Id, contact = c);

                    if (contact.ContactTags == null)
                        contact.ContactTags = new List<ContactTag>();

                    if (contact.ContactCommunicationDetails == null)
                        contact.ContactCommunicationDetails = new List<ContactCommunicationDetail>();

                    p.Tag = d;

                    p.Contact = contact;

                    if (!contact.ContactTags.Any(a => a.Id == p.Id))
                        contact.ContactTags.Add(p);

                    e.CommunicationType = f;

                    e.Contact = contact;

                    if (!contact.ContactCommunicationDetails.Any(a => a.Id == e.Id))
                        contact.ContactCommunicationDetails.Add(e);

                    return contact;
                }, new { id = id });

                return result.Distinct().ToList();
            }
        }
    }
}
