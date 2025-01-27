using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();
        Platform GetPlatformById(int id);
        bool DeletePlatformById(int id);
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
    }
}
