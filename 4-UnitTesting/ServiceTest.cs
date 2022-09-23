using MedGrupo.Domain.ContactAggregate.Exceptions;
using MedGrupo.Services.ContactService;

namespace MedGrupo.UnitTesting;

public class ServiceTest
{
    [Fact]
    public void CreateContact_OK()
    {
        var moqRepo = new Mock<IContactRepository>();
        var contactServices = new ContactServices(moqRepo.Object);

        var ex = Record.Exception(() => contactServices.CreateContact(new Contact{
            Name = "William Diana",
            BirthDate = new DateTime(1999, 1, 22),
            Gender = EGender.MALE
        }));

        Assert.Null(ex);
    }
    [Fact]
    public void CreateContact_FutureBirthDate_Error()
    {
        var moqRepo = new Mock<IContactRepository>();
        var contactServices = new ContactServices(moqRepo.Object);

        var ex = Assert.Throws<ValidationException>(() => contactServices.CreateContact(new Contact{
            Name = "William Diana",
            BirthDate = new DateTime(2999, 1, 22),
            Gender = EGender.MALE
        }));

        Assert.True(ex.InnerExceptions.Any(m => m.Message == "The birthday date cannot be greater than today."));
    }
    [Fact]
    public void CreateContact_UnderAge_Error()
    {
        var moqRepo = new Mock<IContactRepository>();
        var contactServices = new ContactServices(moqRepo.Object);

        var ex = Assert.Throws<ValidationException>(() => contactServices.CreateContact(new Contact{
            Name = "William Diana",
            BirthDate = new DateTime(2010, 1, 22),
            Gender = EGender.MALE
        }));

        Assert.True(ex.InnerExceptions.Any(m => m.Message == "The contact cannot be under age."));
    }
    [Fact]
    public void DeleteContact_OK()
    {
        var id = 1;
        var ct = new Contact{
            Id = 1,
            Active = true,
            Name = "William Diana",
            Gender = EGender.MALE,
            BirthDate = new DateTime(1999, 1, 22)
        };
        
        var moqRepo = new Mock<IContactRepository>();
        moqRepo.Setup(r => r.GetContact(ct.Id)).Returns(ct);

        var contactService = new ContactServices(moqRepo.Object);

        var ex = Record.Exception(() => contactService.DeleteContact(id));
        Assert.Null(ex);
    }
    [Fact]
    public void DeleteContact_ContactNofFound()
    {
        var id = 2;
        var ct = new Contact{
            Id = 1,
            Active = true,
            Name = "William Diana",
            Gender = EGender.MALE,
            BirthDate = new DateTime(1999, 1, 22)
        };
        
        var moqRepo = new Mock<IContactRepository>();
        moqRepo.Setup(r => r.GetContact(ct.Id)).Returns(ct);

        var contactService = new ContactServices(moqRepo.Object);

        var ex = Assert.Throws<ContactException>(() => contactService.DeleteContact(id));
        Assert.True(ex.Message == $"Contact '{id}' not found.");
    }
    [Fact]
    public void UpdateContact_OK()
    {
        var id = 1;
        var saved = new Contact{
            Id = 1,
            Active = true,
            Name = "William Diana",
            Gender = EGender.MALE,
            BirthDate = new DateTime(1999, 1, 22)
        };
        var updated = new Contact{
            // Id = 1,
            Active = false,
            Name = "William Andrade Diana",
            // Gender = EGender.MALE,
            // BirthDate = null
        };

        var moqRepo = new Mock<IContactRepository>();
        moqRepo.Setup(r => r.GetContact(saved.Id)).Returns(saved);

        var contactService = new ContactServices(moqRepo.Object);

        var ex = Record.Exception(() => {
            contactService.UpdateContact(id, updated);
        });

        Assert.Null(ex);
        Assert.True(saved.Active == updated.Active && saved.Name == updated.Name);
    }
}