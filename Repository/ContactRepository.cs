using MedGrupo.Domain.ContactAggregate;

namespace Repository.ContactRepository
{
    public class ContactRepository : IContactRepository
    {
        private readonly IDBContext dBContext;
        public ContactRepository(IDBContext context)
        {
            dBContext = context;
        }
        public void CreateContact(Contact contact)
        {
            dBContext.CreateContact(contact);
        }

        public void DeleteContact(Contact contact)
        {
            dBContext.DeleteContact(contact);
        }

        public Contact GetContact(int id)
        {
            return dBContext.GetContact(id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return dBContext.GetContacts();
        }

        public void UpdateContact(Contact contact)
        {
            dBContext.UpdateContact(contact);
        }
    }
}