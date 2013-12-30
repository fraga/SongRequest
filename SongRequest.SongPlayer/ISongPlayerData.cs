using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongRequest.SongPlayer
{
    public interface ISongPlayerData
    {
        string PlayerType { get; }
        string Desc { get; }
    }
}
