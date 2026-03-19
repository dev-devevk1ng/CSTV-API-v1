/*
    Date 4 Mar 2026
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CSLA.Models;
using CSLA.Models.Player;
using CSLA.DTO.Player;

namespace CSLA.Services.Player
{
    public interface IPlayerInterface
    {
        // Player.Player
        Task<ResponseModel<List<PlayerResponseDTO>>> GetAllPlayers();
        Task<ResponseModel<PlayerResponseDTO>> GetPlayerById(Guid PlayerId);
        Task<ResponseModel<List<PlayerResponseDTO>>> GetPlayersByNickname(string Nickname);
        Task<ResponseModel<PlayerResponseDTO>> CreatePlayer(PlayerCreateDTO CreateDTO);
        Task<ResponseModel<PlayerResponseDTO>> EditPlayer(PlayerEditDTO EditDTO);
        Task<ResponseModel<PlayerResponseDTO>> RemovePlayer(Guid PlayerId);
        
        // Player.Profile
        Task<ResponseModel<List<ProfileResponseDTO>>> GetAllProfiles();
        Task<ResponseModel<ProfileResponseDTO>> GetProfileById(int ProfileId);
        Task<ResponseModel<List<ProfileResponseDTO>>> GetProfilesByNames(string FirstName, string LastName);
        Task<ResponseModel<ProfileResponseDTO>> CreateProfile(ProfileCreateDTO CreateDTO);
        Task<ResponseModel<ProfileResponseDTO>> EditProfile(ProfileEditDTO EditDTO);
        Task<ResponseModel<ProfileResponseDTO>> RemoveProfile(int ProfileId);
    }
}