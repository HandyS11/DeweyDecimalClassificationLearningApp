using DeweyDecimalClassification.Business.Models;

namespace DeweyDecimalClassification.Business.Interfaces;

public interface IDeweyService
{
    Task<IEnumerable<SimplifiedDewey>> GetAllAsync();
    Task<IEnumerable<SimplifiedDewey>> GetSomeAsync(int count); 
    Task<Dewey?> GetByIdAsync(float id);
}