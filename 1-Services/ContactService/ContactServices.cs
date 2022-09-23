using MedGrupo.Domain.ContactAggregate;
using MedGrupo.Domain.ContactAggregate.Exceptions;

namespace MedGrupo.Services.ContactService
{
    public class ContactServices : IContactServices
    {
        private IContactRepository contactRepository;

        public ContactServices(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }
        public Contact CreateContact(Contact contact)
        {
            var validationErrors = GetContactValidationErrors(contact);
            
            if (validationErrors.Count() > 0)
                throw new ValidationException(validationErrors);
            
            contactRepository.CreateContact(contact);

            return contact;
        }

        public void DeleteContact(int id)
        {
            var contact = GetContact(id);
            contactRepository.DeleteContact(contact);
        }

        public Contact GetContact(int id)
        {
            var savedContact = contactRepository.GetContact(id);

            if (savedContact == null)
                throw new ContactException($"Contact '{id}' not found.");
            else if (savedContact.Active == false)
                throw new ContactException($"Contact is inative.");
            
            return savedContact;
        }

        public IEnumerable<Contact> GetContacts()
        {
            return contactRepository.GetContacts();
        }

        public Contact UpdateContact(int id, Contact contactToUpdate)
        {
            var savedcontact = GetContact(id);

            var errors = GetContactValidationErrors(contactToUpdate);
            
            if (errors.Count() > 0)
                throw new ValidationException(errors);

            savedcontact.Update(contactToUpdate);
            
            contactRepository.UpdateContact(savedcontact);
            
            return savedcontact;
        }

        public IEnumerable<ContactException> GetContactValidationErrors(Contact contact)
        {
            var errors = new List<ContactException>();

            if (contact.BirthDate > DateTime.Today)
                errors.Add(new ContactException("The birthday date cannot be greater than today."));
            if (contact.Age < 18)
                errors.Add(new ContactException("The contact cannot be under age."));

            return errors;
        }
    }
}