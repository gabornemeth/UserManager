﻿using FluentValidation;
using MongoDB.Bson;
using UserManager.Models;

namespace UserManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly AbstractValidator<User>? _validator;

        public UserService(IUserRepository repository, AbstractValidator<User>? validator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator;
        }

        public async Task<bool> Create(User user, CancellationToken cancellationToken = default)
        {
            if (user.Id != null)
            {
                var existingUser = await _repository.Get(user.Id.ToString()!, cancellationToken);
                if (existingUser != null)
                {
                    return false;
                }
            }

            if (_validator?.Validate(user).IsValid == false)
            {
                return false;
            }

            await _repository.Create(user);
            return true;
        }

        public async Task<bool> Delete(string id, CancellationToken cancellationToken = default)
        {
            var user = await _repository.Get(id, cancellationToken);
            if (user == null)
            {
                return false;
            }

            return await _repository.Delete(user);
        }

        public Task<IEnumerable<User>> GetAll(CancellationToken cancellation = default)
        {
            return _repository.GetAll(cancellation);
        }

        public Task<User?> Get(string id, CancellationToken cancellationToken = default)
        {
            return _repository.Get(id, cancellationToken);
        }

        public async Task<bool> Update(User user, CancellationToken cancellationToken = default)
        {
            if (user.Id == null) return false;
            var userToUpdate = await _repository.Get(user.Id.ToString()!);
            if (userToUpdate == null)
            {
                return false;
            }

            if (_validator?.Validate(user).IsValid == false)
            {
                return false;
            }

            await _repository.Update(user);
            return true;
        }
    }
}
