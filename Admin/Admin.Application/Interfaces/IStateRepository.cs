using Admin.Core.Entity.Masters;

namespace Admin.Application.Interfaces
{
    public interface IStateRepository
    {
        /// <summary>
        /// For Adding of State Name
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        Task<int> AddState(string stateName);
        /// <summary>
        /// For Removing the State Name
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        Task<int> RemoveState(string stateName);
        /// <summary>
        /// For Getting all the State Name
        /// </summary>
        /// <returns></returns>
        Task<List<State>> GetState();
    }
}
