using CommandsService.Models;
using System;
//using PlatformService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : IcommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command, int platformId)
        {
            if ( command == null )
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;
            _context.Commands.Add(command);
            
        }

        public void CreatePlatform(Platform platform)
        {
            if ( platform == null )
            {
                throw new ArgumentNullException(nameof(platform)); 
            }
            else 
            {
                _context.Add(platform);
            }
        }

        public void DeleteCommand(int commandId)
        {
            throw new NotImplementedException( );
        }

        public bool DeletePlatformById(int platformId)
        {
            if ( PlatformExists(platformId) )
            {
                _context.Remove(platformId);
                return true;
            }
            return false;
        }

        public bool ExternalPlatformExists(int ExternalPlatformId)
        {
            return _context.Platforms.Any(p => p.ExternalId == ExternalPlatformId);

        }

        public IEnumerable<Platform> GetAllPlatforms( )
        {
            return _context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault( );

        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name);
        }

        public bool PlatformExists(int platformId)
        {
            return _context.Platforms.Any(c => c.Id == platformId);
        }

        public bool SaveChanges( )
        {
            return _context.SaveChanges( ) >= 0;
        }
    }
}
