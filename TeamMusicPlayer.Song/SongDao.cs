namespace TeamMusicPlayer.SongLibrary
{
    using System.Collections.Generic;

    internal abstract class SongDao
    {
        protected internal abstract IEnumerable<Song> SelectAllSongs();
        protected internal abstract Song SelectSongByName(string songName);
        protected internal abstract Song InsertSong(string songName, string artistName);
    }
}