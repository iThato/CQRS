using System;

namespace Dvt.Infrastructure.Interfaces
{
    public interface IClock
    {
        DateTime NowAsUtc { get; }
        DateTime NowAsLocal { get; }
        DateTime NowAsSouthAfrican { get; }
    }
}
