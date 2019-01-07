namespace TeamMusicPlayer.SongMix
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using SongLibrary;
    using SongRating;
    using Team;

    public sealed class SongMixEntry
    {
        private readonly SongRatingSet songRatingSet;

        internal SongMixEntry(Song song)
        {
            Song = song;
            songRatingSet = SongRatings.GetSongRatingSet(song);
        }

        public Song Song { get; }

        public int PlayCount { get; private set; }

        public DateTime LastPlayed { get; private set; } = DateTime.MinValue;

        public void Play()
        {
            ++PlayCount;
            LastPlayed = DateTime.Now;
            Thread.Sleep(1); // to ensure LastPlayed timestamps are unique
        }

        public bool IsNotDislikedByAnyListener(IEnumerable<TeamMember> listeners)
        {
            foreach (TeamMember teamMember in listeners)
            {
                if (songRatingSet.HasDislikedRatingBy(teamMember))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetLikesForSong(Team localteam)
        {
            int count = 0;

            foreach (TeamMember teamMember in localteam.Members)
            {
                if (songRatingSet.HasLikedRatingBy(teamMember))
                {
                    count++;
                }
            }


            //foreach (value in localsongRatingSet.songRatingsByTeamMemberId.Values)
            //{
            //    if (entry.Value.ToString() == "Like")
            //    {
            //        count++;
            //    }
            //}

            return count;
        }
    }
}