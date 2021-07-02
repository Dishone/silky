﻿using System;
using System.Threading.Tasks;
using RedLockNet;

namespace Silky.Lms.Lock.Provider
{
    public interface ILockerProvider : IDisposable
    {
        Task<IRedLock> CreateLockAsync(string resource, TimeSpan expiry);

        Task<IRedLock> CreateLockAsync(string resource, TimeSpan expiry, TimeSpan wait, TimeSpan retry);

        Task<IRedLock> CreateLockAsync(string resource);
    }
}