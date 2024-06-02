﻿using LMS_Elibraty.Data;
using LMS_Elibraty.DTOs;

namespace LMS_Elibraty.Services
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<string> Login(LoginModel model);
        Task<User> Detail(string id);
        Task<User> Update(string id,UserViewModel user);
        Task<bool> ChangeAvatar(string id, IFormFile avatar);
        Task<bool> Delete(string id);
        Task<IEnumerable<User>> All();
        Task<User> ChangePassword(string id, ChangePasswordModel model);
        Task<bool> ForgotPassword(string email);
    }
}
