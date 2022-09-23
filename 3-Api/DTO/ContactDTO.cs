using MedGrupo.Domain.ContactAggregate;

namespace MedGrupo.Api.DTO
{
    public abstract class ContactDTO {
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public EGender? Gender { get; set; }
        public abstract Contact ToDomain();
    }
    public class CreateContactDTO : ContactDTO
    {
        public override Contact ToDomain() => new Contact{
            Name = this.Name,
            BirthDate = this.BirthDate,
            Gender = this.Gender
        };
    }

    public class UpdateContactDTO : ContactDTO
    {
        public bool? Active { get; set; }
        public override Contact ToDomain() => new Contact{
            Active = this.Active,
            Name = this.Name,
            BirthDate = this.BirthDate,
            Gender = this.Gender
        };
    }
}