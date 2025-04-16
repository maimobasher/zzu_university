using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.data.Model;
using zzu_university.data.Repository.UnitOfWork;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.UserService
{
    public class UserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.User.GetAllAsync();
            return users.Select(u => new UserDto
            {
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address,
                UserType = u.UserType,
                CreatedAt = u.CreatedAt
            });
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.User.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                UserType = user.UserType,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var user = new User
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                Password = userDto.Password, // Ensure password is hashed before saving
                Phone = (string)userDto.Phone,
                Address = (string)userDto.Address,
                UserType = (string)userDto.UserType,
                CreatedAt = userDto.CreatedAt ?? DateTime.Now
            };

            await _unitOfWork.User.AddAsync(user);
            _unitOfWork.Save();
        }

        public async Task UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _unitOfWork.User.GetByIdAsync(id);
            if (user == null) return;

            user.FullName = userDto.FullName;
            user.Email = userDto.Email;
            user.Phone = (string?)userDto.Phone;
            user.Address = (string)userDto.Address;
            user.UserType = (string)userDto.UserType;

            _unitOfWork.User.Update(user);
            _unitOfWork.Save();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.User.GetByIdAsync(id);
            if (user == null) return;

            _unitOfWork.User.Delete(user);
            _unitOfWork.Save();
        }
    }
}

