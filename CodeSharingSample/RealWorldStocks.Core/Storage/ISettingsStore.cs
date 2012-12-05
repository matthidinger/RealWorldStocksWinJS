using System.Threading.Tasks;

namespace RealWorldStocks.Core.Storage
{
    public interface ISettingsStore
    {
        Task<string> ReadAsync(string fileName);
        Task SaveAsync(string fileName, string content);
        Task DeleteAsync(string fileName);
    }
}