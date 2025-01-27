using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public interface ICommanDataCLient
    {
        Task SendPlatformToCommand(PlatformReadDto platformReadDto);
    }
}
