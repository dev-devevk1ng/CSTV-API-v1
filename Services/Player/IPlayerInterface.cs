/*
    Date 4 Mar 2026
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CSTV_v1.Models;
using CSTV_v1.Models.Player;
using CSTV_v1.DTO.Player;

namespace CSTV_v1.Services.Player
{
    public interface IPlayerInterface
    {
        Task<ResponseModel<List<PlayerModel>>> GetPlayerList();
        Task<ResponseModel<PlayerModel>> GetPlayerById(Guid PlayerId);
        Task<ResponseModel<List<PlayerModel>>> GetPlayerByNickname(string Nickname);
        Task<ResponseModel<PlayerModel>> CreatePlayer(CreatePlayerDTO PlayerDTO);
        Task<ResponseModel<PlayerModel>> EditPlayer(EditPlayerDTO EditPlayerDTO);
        Task<ResponseModel<PlayerModel>> RemovePlayer(Guid PlayerId);
    }
}