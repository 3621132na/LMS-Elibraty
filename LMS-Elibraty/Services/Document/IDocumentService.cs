using LMS_Elibraty.Data;

namespace LMS_Elibraty.Services.Document
{
    public interface IDocumentService
    {
        Task<Documents> Create(Documents document);
        Task<IEnumerable<Documents>> All();
        Task<Documents> Details(int id);
        Task<Documents?> Update(int id, Documents updatedDocument);
        Task<Documents?> UpdateStatus(int id, string newStatus, string approvedBy, string note);
        Task<bool> Delete(int id);
    }
}
