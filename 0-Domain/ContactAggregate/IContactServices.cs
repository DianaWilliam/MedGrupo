namespace MedGrupo.Domain.ContactAggregate
{
    public interface IContactServices
    {
        Contact GetContact(int id);
        Contact CreateContact(Contact contact);
        void DeleteContact(int id);
        Contact UpdateContact(int id, Contact contact);
        IEnumerable<Contact> GetContacts();
    }
}