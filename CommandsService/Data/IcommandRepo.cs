using CommandsService.Models;

namespace CommandsService.Data
{
    public interface IcommandRepo
    {
        bool SaveChanges( );
       
        #region platforms
        IEnumerable<Platform> GetAllPlatforms();
        bool DeletePlatformById(int platformId);
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);
        #endregion

        #region Commands
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void DeleteCommand(int commandId);
        void CreateCommand(Command command, int platformId);
        bool ExternalPlatformExists(int ExternalPlatformId);

        #endregion

    }
}
