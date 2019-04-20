﻿using System;
using System.Collections.Generic;
using System.Text;
using Joblify.Core.Data.Context;
using Joblify.Core.Data.Models;
using Joblify.Core.Data.Repository;
using Joblify.Core.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Joblify.Core.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<File> FileRepository{ get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<ExternalProvider> ExternalProviderRepository { get; }

        void Commit();
        void RejectChanges();
        void Dispose();
    }
}
