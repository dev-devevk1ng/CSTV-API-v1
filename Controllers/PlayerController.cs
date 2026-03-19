/*
    Date 3 Mar 2026
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSLA.Data;
using Microsoft.AspNetCore.Mvc;

using CSLA.Models;
using CSLA.Models.Player;
using CSLA.Services.Player;
using CSLA.DTO.Player;

namespace CSLA.Controllers.Player
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerInterface _playerInterface;
        public PlayerController(IPlayerInterface playerInterface)
        {
            _playerInterface = playerInterface;
        }

        // Player.Player
        [Tags("Player")]
        [HttpGet("GetAllPlayers")]
        public async Task<ActionResult<ResponseModel<List<PlayerResponseDTO>>>> GetAllPlayers()
        {
            var players = await _playerInterface.GetAllPlayers();
            return Ok(players);
        }

        [HttpGet("PlayerId")]
        public async Task<ActionResult<ResponseModel<PlayerResponseDTO>>> GetPlayerById(Guid Id)
        {
            var player = await _playerInterface.GetPlayerById(Id);
            return Ok(player);
        }

        [HttpGet("Nickname")]
        public async Task<ActionResult<ResponseModel<List<PlayerResponseDTO>>>> GetPlayersByNickname(string Nickname)
        {
            var Players = await _playerInterface.GetPlayersByNickname(Nickname);
            return Ok(Players);
        }

        [HttpPost("CreatePlayer")]
        public async Task<ActionResult<ResponseModel<PlayerResponseDTO>>> CreatePlayer(PlayerCreateDTO CreateDTO)
        {
            var response = await _playerInterface.CreatePlayer(CreateDTO);
            return Ok(response);
        }

        [HttpPut("EditPlayer")]
        public async Task<ActionResult<ResponseModel<PlayerResponseDTO>>> EditPlayer(PlayerEditDTO EditDTO)
        {
            var response = await _playerInterface.EditPlayer(EditDTO);
            return Ok(response);
        }

        [HttpDelete("RemovePlayer")]
        public async Task<ActionResult<ResponseModel<PlayerResponseDTO>>> RemovePlayer(Guid id)
        {
            var response = await _playerInterface.RemovePlayer(id);
            return Ok(response);
        }

        // Player.Profile
        [Tags("Profile")]
        [HttpGet("GetAllProfiles")] 
        public async Task<ActionResult<ResponseModel<List<ProfileResponseDTO>>>> GetAllProfiles()
        {
            var response = await _playerInterface.GetAllProfiles();
            return Ok(response);
        }

        [Tags("Profile")]
        [HttpGet("ProfileById")]
        public async Task<ActionResult<ResponseModel<ProfileResponseDTO>>> GetProfileById(int ProfileId)
        {
            var Profile = await _playerInterface.GetProfileById(ProfileId);
            return Ok(Profile);
        }

        [Tags("Profile")]
        [HttpGet("GetProfileByName")]
        public async Task<ActionResult<ResponseModel<List<ProfileResponseDTO>>>> GetProfilesByNames(string FirstName=null, string LastName=null)
        {
            var Profiles = await _playerInterface.GetProfilesByNames(FirstName, LastName);
            return Ok(Profiles);
        }

        [Tags("Profile")]
        [HttpPost("CreateProfile")]
        public async Task<ActionResult<ResponseModel<ProfileResponseDTO>>> CreateProfile(ProfileCreateDTO CreateDTO)
        {
            var Profile = await _playerInterface.CreateProfile(CreateDTO);
            return Ok(Profile);
        }

        [Tags("Profile")]
        [HttpPut("EditProfile")]
        public async Task<ActionResult<ResponseModel<ProfileResponseDTO>>> EditProfile(ProfileEditDTO EditDTO)
        {
            var Profile = await _playerInterface.EditProfile(EditDTO);
            return Ok(Profile);
        }

        [Tags("Profile")]
        [HttpDelete("RemoveProfile")]
        public async Task<ActionResult<ResponseModel<ProfileResponseDTO>>> RemoveProfile(int ProfileId)
        {
            var Profile = await _playerInterface.RemoveProfile(ProfileId);
            return Ok(Profile);
        }
        
    }
}