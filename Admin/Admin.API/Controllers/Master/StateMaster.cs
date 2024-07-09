using Admin.Application.Interfaces;
using Admin.Core.Entity.Masters;
using Admin.Infastructure.Redis.Base;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Admin.API.Controllers.Master
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StateMaster : ControllerBase
    {
        private readonly IStateRepository _stateRepository;
        private readonly IRedisCache _cacheService;
        public StateMaster(IStateRepository stateRepository, IRedisCache redisCache)
        {
            _stateRepository = stateRepository;
            _cacheService = redisCache;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost("AddState")]
        public async Task<ActionResult<int>> AddState(string statename)
        {
            var retVal = await _stateRepository.AddState(statename);
            if (retVal > 0)
            {
                var data = await _stateRepository.GetState();
                _cacheService.SetData("state", data, DateTime.Now.AddMinutes(15));
            }
            return Ok(retVal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetState")]
        public async Task<ActionResult> GetState()
        {
            var data = _cacheService.GetData<List<State>>("state");
            if (data != null)
            {
                return Ok(data);
            }
            data = await _stateRepository.GetState();
            _cacheService.SetData<List<State>>("state", data, DateTime.Now.AddMinutes(15));
            return Ok(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statename"></param>
        /// <returns></returns>
        [HttpPost("DeleteState")]
        public async Task<ActionResult<int>> DeleteState(string statename)
        {
            var retVal = await _stateRepository.RemoveState(statename);
            _cacheService.RemoveData("state");
            return Ok(retVal);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStateList")]
        public async Task<ActionResult<State>> GetStateList()
        {
            var statelist = await _stateRepository.GetState();
            return Ok(statelist);
        }
    }
}
