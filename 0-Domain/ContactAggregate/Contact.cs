using MedGrupo.Domain.ContactAggregate.Exceptions;

namespace MedGrupo.Domain.ContactAggregate
{
    public class Contact {
        public bool? Active { get; set; } = true;
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? BirthDate { get; set; } = null;
        public EGender? Gender { get; set; }
        public int? Age { get => DateTime.Now.Year - BirthDate?.Year; }

        
        public void Update(Contact newValues)
        {
            if (newValues.Active != null)
                this.Active = newValues.Active;
            if (newValues.Name != null)
                this.Name = newValues.Name;
            if (newValues.BirthDate != null)
                this.BirthDate = newValues.BirthDate;
            if (newValues.Gender != null)
                this.Gender = newValues.Gender;
        }
    }

}