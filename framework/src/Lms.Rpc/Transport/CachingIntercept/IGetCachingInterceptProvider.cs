﻿namespace Lms.Rpc.Transport.CachingIntercept
{
    public interface IGetCachingInterceptProvider : ICachingInterceptProvider
    {
        string CacheName { get; }
    }
}