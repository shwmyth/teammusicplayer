namespace TeamMusicPlayer.SongMix
{
    using System.Collections.Generic;
    using System.Linq;
    using SongLibrary;
    using Team;

    public sealed class SongMix
    {
        private readonly HashSet<TeamMember> listeners = new HashSet<TeamMember>();
        private readonly List<SongMixEntry> songMixEntries = new List<SongMixEntry>();

        public SongMix(Team team)
        {
            Team = team;
            addAllAvailableSongsToMix();
        }

        public Team Team { get; }

        public SongMixEntry DetermineNextSongMixEntryToPlay()
        {
            if (songMixEntries.Count > 0)
            {

                IOrderedEnumerable<SongMixEntry> songMixEntriesInLastPlayedOrder =
                    songMixEntries.OrderBy(s => s.LastPlayed);

                SongMixEntry firstSongNotDislikedByACurrentListener =
                    songMixEntriesInLastPlayedOrder.FirstOrDefault(s => s.IsNotDislikedByAnyListener(listeners));
                return firstSongNotDislikedByACurrentListener;
            }

            return null;
        }

        public void AddListener(TeamMember teamMember)
        {
            listeners.Add(teamMember);
        }

        private void addAllAvailableSongsToMix()
        {
            IEnumerable<Song> allSongs = Songs.GetAllSongs();
            foreach (Song song in allSongs)
            {
                var songMixEntry = new SongMixEntry(song);
                songMixEntries.Add(songMixEntry);
            }
        }

        public void addLikedSongsToMix()
        {
            IEnumerable<Song> allSongs = Songs.GetAllSongs();
            foreach (Song song in allSongs)
            {
                var songMixEntry = new SongMixEntry(song);
                int count = songMixEntry.GetLikesForSong(Team);

                for (var i = 0; i < count; ++i)
                {
                    songMixEntries.Add(songMixEntry);

                }
            }
        }

        public int TotalEntryCount()
        {
            return songMixEntries.Count;
        }

        public int TotalEntryMinusDislikesCount()
        {
            if (songMixEntries.Count > 0)
            {
                IEnumerable<SongMixEntry> songMixEntriesMinusDislikes =
                    songMixEntries.Where(s => s.IsNotDislikedByAnyListener(listeners));

                return songMixEntriesMinusDislikes.Count();
            }
            else
            {
                return 0;
            }
            
        }

        public int SongInMixCount(string song)
        {
            if (songMixEntries.Count > 0)
            {
                IEnumerable<SongMixEntry> songMixEntriesForSong =
                    songMixEntries.Where(s => s.Song.Name == song);

                return songMixEntriesForSong.Count();
            }
            else
            {
                return 0;
            }

        }

        public string SongMostInMix()
        {
            if (songMixEntries.Count > 0)
            {
                string songMixEntriesForSong =
                    songMixEntries.GroupBy(s => s.Song.Name).OrderByDescending(g => g.Count()).First().Key; 
                return songMixEntriesForSong;
            }
            else
            {
                return "";
            }

        }
    }
}