﻿using System;

namespace ParcelCalculator.API.CastleWindsor
{
    class Release : IDisposable
    {
        private readonly Action release;

        public Release(Action release)
        {
            this.release = release;
        }

        public void Dispose()
        {
            this.release();
        }
    }
}