namespace TeamMusicPlayer.SongLibrary
{
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class TestSongDao : SongDao
    {
        private int songIdCounter = 1;
        private readonly IDictionary<int, Song> songStore = new Dictionary<int, Song>();

        protected internal override IEnumerable<Song> SelectAllSongs()
        {
            return songStore.Select(s => s.Value);
        }

        protected internal override Song SelectSongByName(string songName)
        {
            KeyValuePair<int, Song> matchingEntry =
                songStore.FirstOrDefault(s => s.Value.Name.Equals(songName));
            return matchingEntry.Value;
        }

        protected internal override Song InsertSong(string songName, string artistName)
        {
            var song = new Song(songIdCounter, songName, artistName);
            songStore.Add(songIdCounter, song);
            ++songIdCounter;
            return song;
        }
    }
}