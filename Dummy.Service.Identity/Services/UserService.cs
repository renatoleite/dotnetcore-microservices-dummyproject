﻿using Dummy.Common.Exceptions;
using Dummy.Service.Identity.Domain.Models;
using Dummy.Service.Identity.Domain.Repositories;
using Dummy.Service.Identity.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dummy.Service.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEncrypter _encrypter;

        public UserService(
            IUserRepository repository,
            IEncrypter encrypter)
        {
            _repository = repository;
            _encrypter = encrypter;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _repository.GetAsync(email);

            if (user != null)
                throw new CustomException("email_in_use", $"Email: '{email}' is already in use.");

            user = new User(email, name);
            user.SetPassword(password, _encrypter);

            await _repository.AddAsync(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _repository.GetAsync(email);

            if (user == null)
                throw new CustomException("invalid_credentials", $"Invalid credentials.");

            if (!user.ValidatePassword(password, _encrypter))
                throw new CustomException("invalid_credentials", $"Invalid credentials.");
        }
    }
}
