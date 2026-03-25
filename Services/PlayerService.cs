/*
    Date 4 Mar 2026
*/
/*
    SQLServer services, IPlayerInterface -> PlayerService -> PlayerController
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSLA.Data;
using Microsoft.EntityFrameworkCore;

using CSLA.Models;
using CSLA.Models.Player;
using CSLA.DTO.Player;

namespace CSLA.Services.Player
{
    public class PlayerService : IPlayerInterface
    {
        private readonly AppDbContext _context;
        public PlayerService(AppDbContext context)
        {
            _context = context;
        }

        // Player.Player
        public async Task<ResponseModel<List<PlayerResponseDTO>>> GetAllPlayers()
        {
            ResponseModel<List<PlayerResponseDTO>> response = new ResponseModel<List<PlayerResponseDTO>>();
            try
            {
                var playerList = await _context.PlayerPlayer
                .AsNoTracking()
                .Select( player => new PlayerResponseDTO
                {
                    Id = player.Id,
                    Nickname = player.Nickname,
                    CreatedAt = player.CreatedAt
                })
                .ToListAsync();

                if (playerList == null || playerList.Count == 0)
                {
                    response.Message = "Nenhum Player.Player encontrado!";
                    response.Dados = playerList;
                    return response;
                }

                response.Dados = playerList;
                response.Message = "Todos os Player.Player foram coletados!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<PlayerResponseDTO>> GetPlayerById(Guid PlayerId)
        {
            ResponseModel<PlayerResponseDTO> response = new ResponseModel<PlayerResponseDTO>();
            try
            {
               // var Player = await _context.Players.FirstOrDefaultAsync(x => x.Id == PlayerId);
                
                var Player = await _context.PlayerPlayer
                .AsNoTracking()
                .Select( player => new PlayerResponseDTO
                {
                    Id = player.Id,
                    Nickname = player.Nickname,
                    CreatedAt = player.CreatedAt
                })
                .FirstOrDefaultAsync(x => x.Id == PlayerId);

                if ( Player == null )
                {
                    response.Message = "Player.Player não encontrado!";
                    return response;           
                }

                response.Dados = Player;
                response.Message = "Player.Player encontrado!";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<List<PlayerResponseDTO>>> GetPlayersByNickname(string nickname)
        {
            ResponseModel<List<PlayerResponseDTO>> response = new ResponseModel<List<PlayerResponseDTO>>();
            try
            {
                var Players = await _context.PlayerPlayer
                .AsNoTracking()
                .Select( player => new PlayerResponseDTO
                {
                    Id = player.Id,
                    Nickname = player.Nickname,
                    CreatedAt = player.CreatedAt
                })
                .Where(x => x.Nickname == nickname)
                .ToListAsync();

                if ( Players == null || Players.Count == 0 )
                {
                    response.Message = "Player.Player não encontrado!";
                    return response;
                }

                response.Message = Players.Count() + "Player.Player coletado(s)!";
                response.Dados = Players;

                return response;
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<PlayerResponseDTO>> CreatePlayer(PlayerCreateDTO PlayerDTO)
        {
            ResponseModel<PlayerResponseDTO> response = new ResponseModel<PlayerResponseDTO>();
            try
            {
               var Player = new PlayerModel()
               {
                   Nickname = PlayerDTO.Nickname
               };

                _context.PlayerPlayer.Add(Player);
                await _context.SaveChangesAsync();

                var PlayerResponse = new PlayerResponseDTO
                {
                    Id = Player.Id,
                    Nickname = Player.Nickname,
                    CreatedAt = Player.CreatedAt
                };
        
                response.Message = "Player.Player criado com sucesso!";
                response.Dados = PlayerResponse;

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<PlayerResponseDTO>> EditPlayer(PlayerEditDTO PlayerEditDTO)
        {
            // user envia Id, que sera usado para proucurar o player, e Nickname, que sera usado como novo Nickname.

            ResponseModel<PlayerResponseDTO> response = new ResponseModel<PlayerResponseDTO>();
            try
            {
                var Player = await _context.PlayerPlayer
                .FirstOrDefaultAsync( Player => Player.Id == PlayerEditDTO.Id);
                
                if ( Player == null )
                {
                    response.Message = "Player.Player not found!";
                    return response;
                }

                Player.Nickname = PlayerEditDTO.Nickname;
                //_context.Update(player);
                await _context.SaveChangesAsync();

                PlayerResponseDTO PlayerResponse = new PlayerResponseDTO
                {
                    Id = Player.Id,
                    Nickname = Player.Nickname,
                    CreatedAt = Player.CreatedAt,
                };

                response.Dados = PlayerResponse;
                response.Message = "Player edited";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<PlayerResponseDTO>> RemovePlayer(Guid PlayerId)
        {
            ResponseModel<PlayerResponseDTO> response = new ResponseModel<PlayerResponseDTO>();
            try
            {
                var Player = await _context.PlayerPlayer
                .FirstOrDefaultAsync( Player => Player.Id == PlayerId );

                if ( Player == null )
                {
                    response.Message = "Player not found!";
                    return response;
                }

                PlayerResponseDTO PlayerResponse = new PlayerResponseDTO
                {
                    Id = Player.Id,
                    Nickname = Player.Nickname,
                    CreatedAt = Player.CreatedAt,
                };

                _context.Remove(Player);
                await _context.SaveChangesAsync();

                response.Dados = PlayerResponse;
                response.Message = "Player deleted";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        // Player.Profile
        public async Task<ResponseModel<List<ProfileResponseDTO>>> GetAllProfiles()
        {
            ResponseModel<List<ProfileResponseDTO>> response = new ResponseModel<List<ProfileResponseDTO>>();
            try
            {
                var Profiles = await _context.PlayerProfile
                .AsNoTracking()
                .Select( p => new ProfileResponseDTO
                {
                    Id = p.Id,
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Born = p.Born,
                    Status = p.Status,
                    ApproxTotalWinnings = p.ApproxTotalWinnings,
                    YearCareerStart = p.YearCareerStart,
                    YearCareerEnd = p.YearCareerEnd,
                    CreatedAt = p.CreatedAt,
                    PlayerNickname = p.Player!.Nickname
                })
                .ToListAsync();

                if ( Profiles == null || Profiles.Count == 0 )
                {
                    response.Message = "Player.Profile não encontrado(s)";
                    response.Dados = Profiles;
                    return response;
                }

                response.Dados = Profiles;
                response.Message = "Player.Profile foram coletados!";
                
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<ProfileResponseDTO>> GetProfileById(int ProfileId)
        {
            ResponseModel<ProfileResponseDTO> response = new ResponseModel<ProfileResponseDTO>();
            try
            {
                var profile = await _context.PlayerProfile
                .AsNoTracking()
                .Select( p => new ProfileResponseDTO
                {
                    Id = p.Id,
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Born = p.Born,
                    Status = p.Status,
                    ApproxTotalWinnings = p.ApproxTotalWinnings,
                    YearCareerStart = p.YearCareerStart,
                    YearCareerEnd = p.YearCareerEnd,
                    CreatedAt = p.CreatedAt,
                    PlayerNickname = p.Player!.Nickname
                })
                .FirstOrDefaultAsync( profile => profile.Id == ProfileId );      
                
                if ( profile == null )
                {
                    response.Message = "Player.Profile não encontrado!";
                    response.Dados = profile;
                    return response;
                }

                response.Dados = profile;
                response.Message = "Player.Profile coletado!";
                
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<List<ProfileResponseDTO>>> GetProfilesByNames(string FirstName, string LastName)
        {
            ResponseModel<List<ProfileResponseDTO>> response = new ResponseModel<List<ProfileResponseDTO>>();
            try
            {
                var query = _context.PlayerProfile.AsQueryable();

                if (!string.IsNullOrEmpty(FirstName))
                {
                    query = query.Where(x => x.FirstName == FirstName);
                }

                if (!string.IsNullOrEmpty(LastName))
                {
                    query = query.Where(x => x.LastName == LastName);
                }

                var Profiles = await query
                .AsNoTracking()
                .Select( p => new ProfileResponseDTO
                {
                    Id = p.Id,
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Born = p.Born,
                    Status = p.Status,
                    ApproxTotalWinnings = p.ApproxTotalWinnings,
                    YearCareerStart = p.YearCareerStart,
                    YearCareerEnd = p.YearCareerEnd,
                    CreatedAt = p.CreatedAt,
                    PlayerNickname = p.Player!.Nickname
                })
                .ToListAsync();

                if ( Profiles == null )
                {
                    response.Message = "Player.Profile não encontrado!";
                    response.Dados = Profiles;
                    return response;
                }

                response.Dados = Profiles;
                response.Message = "Player.Profile coletado(s)!";
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<ProfileResponseDTO>> CreateProfile(ProfileCreateDTO CreateDTO)
        {
            ResponseModel<ProfileResponseDTO> response = new ResponseModel<ProfileResponseDTO>();
            try
            {
               var Profile = new ProfileModel()
               {
                    PlayerId = CreateDTO.PlayerId,
                    FirstName = CreateDTO.FirstName,
                    LastName = CreateDTO.LastName,
                    Born = CreateDTO.Born,
                    Status = CreateDTO.Status,
                    ApproxTotalWinnings = CreateDTO.ApproxTotalWinnings,
                    YearCareerStart = CreateDTO.YearCareerStart,
                    YearCareerEnd = CreateDTO.YearCareerEnd,
                    CreatedAt = DateTime.UtcNow
               };

                _context.PlayerProfile.Add(Profile);
                await _context.SaveChangesAsync();

                var Player = await _context.PlayerPlayer
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == Profile.PlayerId);

                var ProfileResponse = new ProfileResponseDTO
                {
                    PlayerId = Profile.PlayerId,
                    FirstName = Profile.FirstName,
                    LastName = Profile.LastName,
                    Born = Profile.Born,
                    Status = Profile.Status,
                    ApproxTotalWinnings = Profile.ApproxTotalWinnings,
                    YearCareerStart = Profile.YearCareerStart,
                    YearCareerEnd = Profile.YearCareerEnd,
                    CreatedAt = Profile.CreatedAt,
                    PlayerNickname = Player!.Nickname
                };
        
                response.Message = "Player.Profile criado com sucesso!";
                response.Dados = ProfileResponse;

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<ProfileResponseDTO>> EditProfile(ProfileEditDTO EditDTO)
        {
            ResponseModel<ProfileResponseDTO> response = new ResponseModel<ProfileResponseDTO>();
            try
            {
                var Profile = await _context.PlayerProfile
                .Include(p => p.Player) // para pegar o nickname de player.player
                .Where( p => p.Id == EditDTO.Id )
                .FirstOrDefaultAsync();

                if ( Profile == null )
                {
                    response.Message = "Player.Profile nao encontrado!";
                    return response;
                }

                Profile.FirstName = EditDTO.FirstName;
                Profile.LastName = EditDTO.LastName;
                Profile.Born = EditDTO.Born;
                Profile.Status = EditDTO.Status;
                Profile.ApproxTotalWinnings = EditDTO.ApproxTotalWinnings;
                Profile.YearCareerStart = EditDTO.YearCareerStart;
                Profile.YearCareerEnd = EditDTO.YearCareerEnd;
            
                await _context.SaveChangesAsync();

                /* outra alternativa para pegar o nickname
                var playerNickname = await _context.PlayerPlayer
                .AsNoTracking()
                .Where(x => x.Id == Profile.PlayerId)
                .Select(x => x.Nickname)
                .FirstOrDefaultAsync() ?? string.Empty;
                */

                ProfileResponseDTO ProfileResponse = new ProfileResponseDTO
                {
                    Id = Profile.Id,
                    PlayerId = Profile.PlayerId,
                    FirstName = Profile.FirstName,
                    LastName = Profile.LastName,
                    Born = Profile.Born,
                    Status = Profile.Status,
                    ApproxTotalWinnings = Profile.ApproxTotalWinnings,
                    YearCareerStart = Profile.YearCareerStart,
                    YearCareerEnd = Profile.YearCareerEnd,
                    PlayerNickname = Profile.Player?.Nickname ?? "",
                };
        
                response.Message = "Player.Profile editado!";
                response.Dados = ProfileResponse;

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<ProfileResponseDTO>> RemoveProfile(int ProfileId)
        {
            ResponseModel<ProfileResponseDTO> response = new ResponseModel<ProfileResponseDTO>();
            try
            {
                var Profile = await _context.PlayerProfile            
                .Where( p => p.Id == ProfileId )
                .FirstOrDefaultAsync();

                if ( Profile == null )
                {
                    response.Message = "Player.Profile nao encontrado!";
                    return response;
                }
             
                ProfileResponseDTO ProfileResponse = new ProfileResponseDTO
                {
                    Id = Profile.Id,
                    PlayerId = Profile.PlayerId,
                    FirstName = Profile.FirstName,
                    LastName = Profile.LastName,
                    Born = Profile.Born,
                    Status = Profile.Status,
                    ApproxTotalWinnings = Profile.ApproxTotalWinnings,
                    YearCareerStart = Profile.YearCareerStart,
                    YearCareerEnd = Profile.YearCareerEnd,
                    PlayerNickname = Profile.Player?.Nickname ?? "",
                };

                _context.Remove(Profile);
                await _context.SaveChangesAsync();
        
                response.Message = "Player.Profile deletado!";
                response.Dados = ProfileResponse;

                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }
    
        // Player.NativeName
        public async Task<ResponseModel<List<NativeNameResponseDTO>>> GetAllNativeNames()
        {
            ResponseModel<List<NativeNameResponseDTO>> response = new ResponseModel<List<NativeNameResponseDTO>>();
            try
            {
                var NativeNameList = await _context.PlayerNativeName
                .AsNoTracking()
                .Select( x => new NativeNameResponseDTO
                {
                    PlayerId = x.PlayerId,
                    NativeFirstName = x.NativeFirstName,
                    NativeLastName = x.NativeLastName
                })
                .ToListAsync();

                if ( NativeNameList.Count == 0 || NativeNameList == null )
                {
                    response.Dados = null;
                    response.Message = "Player.NativeName nao encontrado(s)";
                    return response;
                }

                response.Dados = NativeNameList;
                response.Message = "Player.NativeName encontrado(s)";
                return response;

            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<NativeNameResponseDTO>> GetNativeNameByPlayerId(Guid PlayerId)
        {
            ResponseModel<NativeNameResponseDTO> response = new ResponseModel<NativeNameResponseDTO>();
            try
            {
                var NativeName = await _context.PlayerNativeName
                .AsNoTracking()
                .Where( x => x.PlayerId == PlayerId)
                .Select( x => new NativeNameResponseDTO
                {
                    PlayerId = x.PlayerId,
                    NativeFirstName = x.NativeFirstName,
                    NativeLastName = x.NativeLastName
                })
                .FirstOrDefaultAsync();

                if ( NativeName == null )
                {
                    response.Dados = null;
                    response.Message = "Player.NativeName nao encontrado";
                    return response;
                }

                response.Dados = NativeName;
                response.Message = "Player.NativeName encontrado";
                return response;

            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<NativeNameResponseDTO>> CreateNativeName(NativeNameCreateDTO CreateDTO)
        {
            ResponseModel<NativeNameResponseDTO> response = new ResponseModel<NativeNameResponseDTO>();
            try
            {
                var NativeName = new NativeNameModel
                {
                    PlayerId = CreateDTO.PlayerId,
                    NativeFirstName = CreateDTO.NativeFirstName,
                    NativeLastName = CreateDTO.NativeLastName,
                };

                _context.PlayerNativeName.Add(NativeName);
                await _context.SaveChangesAsync();

                var NativeNameResponse = new NativeNameResponseDTO 
                {
                    PlayerId = NativeName.PlayerId,
                    NativeFirstName = NativeName.NativeFirstName,
                    NativeLastName = NativeName.NativeLastName,
                };          

                response.Dados = NativeNameResponse;
                response.Message = "Player.NativeName criado com sucesso!";
                return response;

            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<NativeNameResponseDTO>> EditNativeName(NativeNameEditDTO EditDTO)
        {
            ResponseModel<NativeNameResponseDTO> response = new ResponseModel<NativeNameResponseDTO>();
            try
            {
                
                var NativeName = await _context.PlayerNativeName
                .Where( x => x.PlayerId == EditDTO.PlayerId )
                .FirstOrDefaultAsync();

                if ( NativeName == null )
                {
                    response.Dados = null;
                    response.Message = "Player.NativeName nao encontrado";
                    return response;
                }
                
                NativeName!.NativeFirstName = EditDTO.NativeFirstName;
                NativeName.NativeLastName = EditDTO.NativeLastName;

                await _context.SaveChangesAsync();

                var NativeNameResponse = new NativeNameResponseDTO
                {
                    PlayerId = NativeName.PlayerId,
                    NativeFirstName = NativeName.NativeFirstName,
                    NativeLastName = NativeName.NativeLastName,
                };
      
                response.Dados = NativeNameResponse;
                response.Message = "Player.NativeName editado com sucesso!";
                return response;

            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<NativeNameResponseDTO>> DeleteNativeName(Guid PlayerId)
        {
            ResponseModel<NativeNameResponseDTO> response = new ResponseModel<NativeNameResponseDTO>();
            try
            {
                var NativeName = await _context.PlayerNativeName
                .Where( x => x.PlayerId == PlayerId)
                .FirstOrDefaultAsync();

                var NativeNameResponse = new NativeNameResponseDTO
                {
                    PlayerId = NativeName!.PlayerId,
                    NativeFirstName = NativeName.NativeFirstName,
                    NativeLastName = NativeName.NativeLastName,
                };

                _context.PlayerNativeName.Remove(NativeName);

                await _context.SaveChangesAsync();

                response.Dados = NativeNameResponse;
                response.Message = "Player.NativeName deletado com sucesso";
                return response;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }

        // Player.AlternateID
        public async Task<ResponseModel<List<AlternateIDResponseDTO>>> GetAllAlternateIDs()
        {
            ResponseModel<List<AlternateIDResponseDTO>> response = new ResponseModel<List<AlternateIDResponseDTO>>();
            try
            {
                var AlternateIDs = await _context.PlayerAlternateID
                .AsNoTracking()               
                .Select( alternateId => new AlternateIDResponseDTO
                {
                    Id = alternateId.Id,
                    PlayerId = alternateId.PlayerId,     
                    AlternateID = alternateId.AlternateID,      
                    PlayerNickname = alternateId.Player!.Nickname
                })
                .ToListAsync();
                
                if ( AlternateIDs == null || AlternateIDs.Count == 0 )
                {
                    response.Dados = null;
                    response.Message = "AlternateIDs nao encontrado(s)";
                    return response;
                }

                response.Dados = AlternateIDs;
                response.Message = "AlternateIDs encontrado(s)";
                return response;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<List<AlternateIDResponseDTO>>> GetAlternateIDsByPlayerId(Guid PlayerId)
        {
            ResponseModel<List<AlternateIDResponseDTO>> response = new ResponseModel<List<AlternateIDResponseDTO>>();
            try
            {
                var AlternateIDs = await _context.PlayerAlternateID
                .AsNoTracking()  
                .Where( altId => altId.PlayerId == PlayerId )             
                .Select( alternateId => new AlternateIDResponseDTO
                {
                    Id = alternateId.Id,
                    PlayerId = alternateId.PlayerId,     
                    AlternateID = alternateId.AlternateID,      
                    PlayerNickname = alternateId.Player!.Nickname
                })
                .ToListAsync();
                
                if ( AlternateIDs == null || AlternateIDs.Count == 0 )
                {
                    response.Dados = null;
                    response.Message = "AlternateIDs nao encontrado(s)";
                    return response;
                }

                response.Dados = AlternateIDs;
                response.Message = "AlternateID encontrado com sucesso";
                return response;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<AlternateIDResponseDTO>> CreateAlternateID(AlternateIDCreateDTO CreateDTO)
        {
            ResponseModel<AlternateIDResponseDTO> response = new ResponseModel<AlternateIDResponseDTO>();
            try
            {
                var AlternateID = new AlternateIDModel
                {
                    PlayerId = CreateDTO.PlayerId,
                    AlternateID = CreateDTO.AlternateID,
                };

               _context.PlayerAlternateID.Add(AlternateID);
               await _context.SaveChangesAsync();
 
                var AlternateIDResponse = new AlternateIDResponseDTO
                {
                    Id = AlternateID.Id,
                    PlayerId = AlternateID.PlayerId,
                    AlternateID = AlternateID.AlternateID,
                    PlayerNickname = AlternateID.Player?.Nickname,
                };
                
                response.Dados = AlternateIDResponse;
                response.Message = "AlternateID criado com sucesso";
                return response;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<AlternateIDResponseDTO>> EditAlternateID(AlternateIDEditDTO EditDTO)
        {
            ResponseModel<AlternateIDResponseDTO> response = new ResponseModel<AlternateIDResponseDTO>();
            try
            {
                var AlternateID = await _context.PlayerAlternateID
                .Where( altID => altID.Id == EditDTO.Id )
                .FirstOrDefaultAsync();

                if ( AlternateID == null )
                {
                    response.Status = false;
                    response.Message = "AlternateID não encontrado";
                    return response;
                }

                AlternateID.AlternateID = EditDTO.AlternateID;
                await _context.SaveChangesAsync();
 
                var AlternateIDResponse = new AlternateIDResponseDTO
                {
                    Id = AlternateID!.Id,
                    PlayerId = AlternateID.PlayerId,
                    AlternateID = AlternateID.AlternateID,
                    PlayerNickname = AlternateID.Player?.Nickname,
                };
                
                response.Dados = AlternateIDResponse;
                response.Message = "AlternateID editado com sucesso";
                return response;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<AlternateIDResponseDTO>> RemoveAlternateID(int Id)
        {
            ResponseModel<AlternateIDResponseDTO> response = new ResponseModel<AlternateIDResponseDTO>();
            try
            {
                var AlternateID = await _context.PlayerAlternateID
                .Where( altID => altID.Id == Id )
                .FirstOrDefaultAsync();

                if ( AlternateID == null )
                {
                    response.Status = false;
                    response.Message = "AlternateID não encontrado";
                    return response;
                }

                _context.PlayerAlternateID.Remove(AlternateID);
                await _context.SaveChangesAsync();
 
                var AlternateIDResponse = new AlternateIDResponseDTO
                {
                    Id = AlternateID!.Id,
                    PlayerId = AlternateID.PlayerId,
                    AlternateID = AlternateID.AlternateID,
                    PlayerNickname = AlternateID.Player?.Nickname,
                };
                
                response.Dados = AlternateIDResponse;
                response.Message = "AlternateID removido com sucesso";
                return response;
            }
            catch (System.Exception ex)
            {
                response.Message = ex.InnerException?.Message ?? ex.Message;
                response.Status = false;
                return response;
            }
        }
    }
}