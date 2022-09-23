namespace MedGrupo.Domain.ContactAggregate
{
    public interface IContactRepository
    {
        Contact GetContact(int id);
        void CreateContact(Contact contact);
        void DeleteContact(Contact contact);
        void UpdateContact(Contact contact);
        IEnumerable<Contact> GetContacts();
    }
}