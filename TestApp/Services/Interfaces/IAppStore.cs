﻿using System;

namespace TestApp.Services.Interfaces
{
    public interface IAppStore : IDisposable
    {
        void Dispose();
        T Load<T>(string section);
        void Open(string fileName);
        void Save(string section, object value);
    }
}