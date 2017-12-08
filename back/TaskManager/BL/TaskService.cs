using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManager.BL.Abstraction;
using TaskManager.Core.Models.DataAccess;
using TaskManager.Core.Models.DTO;
using TaskManager.DataAccess.UnitOfWork.Abstraction;

namespace TaskManager.BL
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly UserManager<UserDbModel> _userManager;

        public TaskService(IUnitOfWork unitOfWork, UserManager<UserDbModel> userManager ) : base(unitOfWork)
        {
            _userManager = userManager;
        }

        public async Task CreateNewTaskAsync(string userId, TaskModelDto model)
        {
            var entity = Mapper.Map<TaskModelDto, TaskDbModel>(model);
            var user = await _userManager.FindByIdAsync(userId);
            entity.User = user;
            UnitOfWork.TaskRepository.Insert(entity);
            await UnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task<TaskModelDto> GetTaskAsync(int taskId)
        {
            return Mapper.Map<TaskDbModel, TaskModelDto>(await UnitOfWork.TaskRepository.GetAsync(taskId).ConfigureAwait(false));
        }

        public async Task<IEnumerable<TaskModelDto>> GetAllTasksByUserId(string userId)
        {
            return Mapper.Map<IEnumerable<TaskDbModel>, IEnumerable<TaskModelDto>>(await UnitOfWork.TaskRepository.FindAsync(model => model.User.Id == userId).ConfigureAwait(false));
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            await UnitOfWork.TaskRepository.DeleteAsync(taskId).ConfigureAwait(false);
            await UnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task UpdateTaskAsync(string userId, TaskModelDto model)
        {
            var entity = Mapper.Map<TaskModelDto, TaskDbModel>(model);
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            entity.User = user;
            UnitOfWork.TaskRepository.Update(entity);
            await UnitOfWork.SaveAsync().ConfigureAwait(false);
        }       
    }
}
