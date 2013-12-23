using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using SongRequest.Config;
using System;
using System.Collections.Generic;

namespace SongRequest.SongPlayer
{
    public class SongPlayerFactory
    {
        private static object _lockObject = new object();
        private static SongPlayerFactory _factory;

        private static SongPlayerFactory Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (_factory == null)
                        _factory = new SongPlayerFactory();
                }

                return _factory;
            }
        }

        public static ISongplayer GetSongPlayer()
        {
            return Instance.SongPlayers.Find(t => t.Metadata.Platform == Environment.OSVersion.Platform && t.Metadata.PlayerType == "Vlc").Value;
        }

        [ImportMany]
        List<Lazy<ISongplayer, ISongPlayerData>> SongPlayers;

        private ISongplayer CurrentSongPlayer;
        
        private SongPlayerFactory()
        {
            InitializeAddinCatalog();
        }
        
        public static ConfigFile GetConfigFile()
        {
            if (!System.IO.File.Exists("songrequest.config"))
            {
                ConfigFile configFile = new ConfigFile("songrequest.config");
                configFile.SetValue("server.port", "8765");
                configFile.SetValue("server.clients", "all");
                
                if (Settings.IsRunningOnWindows())
                    configFile.SetValue ("library.path", "c:\\music");
                else
                    configFile.SetValue ("library.path", "~/Music");

                configFile.SetValue("library.minutesbetweenscans", "1");
                configFile.SetValue("library.extensions", "mp3");
                configFile.SetValue("player.minimalsonginqueue", "0");
                configFile.SetValue("player.maximalsonginqueue", "500");
                configFile.SetValue("player.startupvolume", "50");
                configFile.Save();
            }

            return new ConfigFile("songrequest.config");
        }

        public void InitializeAddinCatalog()
        {
            DirectoryCatalog catalog = new DirectoryCatalog("addins");
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}

