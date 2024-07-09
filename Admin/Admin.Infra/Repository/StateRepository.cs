using Admin.Application.Interfaces;
using Admin.Core.Entity.Masters;
using Admin.Infra.Data.Base;
using Admin.Infra.Data.Factory;
using System.Data;
using Dapper;

namespace Admin.Infra.Repository
{
    public class StateRepository : ConnectionRepositoryBase, IStateRepository
    {
        public StateRepository(IConnectionFactory connectionFactory):base(connectionFactory)
        {

        }
        /// <summary>
        /// For Inserting the data of State
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public async Task<int> AddState(string stateName)
        {
            var p = new DynamicParameters();
            p.Add("@StateName", stateName);
            p.Add("@CreatedBy", 1);
            p.Add("result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await Connection.QueryAsync("USP_Create_State", p, commandType: CommandType.StoredProcedure);
            int result = p.Get<int>("@result");
            return result;
        }       

        public async Task<List<State>> GetState()
        {
            var result = await Connection.QueryAsync<State>("USP_GetState", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<int> RemoveState(string stateId)
        {
            var p = new DynamicParameters();
            p.Add("@stateId", stateId);
            p.Add("result", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await Connection.QueryAsync("USP_DeleteState", p, commandType: CommandType.StoredProcedure);
            int result = p.Get<int>("@result");
            return result;
        }
    }
}
