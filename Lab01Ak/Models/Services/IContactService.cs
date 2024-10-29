using Lab01Ak.Models;

namespace LaboratoriumASPNET.Models.Services;

public interface IContactService
{
    void Add(ContactModel contact);
    void Update(ContactModel contact);
    void Delete(int id);
    List<ContactModel> GetAll();
    ContactModel? GetById(int id);
}