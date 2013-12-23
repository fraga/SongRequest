using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongRequest.SongPlayer
{
    public interface ISongPlayerData
    {
        PlatformID Platform { get; }
        string PlayerType { get; }
    }
}
