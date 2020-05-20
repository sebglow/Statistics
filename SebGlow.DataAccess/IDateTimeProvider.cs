using System;

namespace SebGlow.Service.Provider
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
