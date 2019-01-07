namespace TeamMusicPlayer.SongRating
{
    using System.Collections.Generic;
    using SongLibrary;
    using Team;

    internal sealed class TestSongRatingDao : SongRatingDao
    {
        private readonly IDictionary<int, SongRatingSet> songRatingStore = new Dictionary<int, SongRatingSet>();

        protected internal override SongRatingSet SelectSongRatingSet(Song song) => getSongRatingSetForSong(song);

        protected internal override void MarkSongAsDisliked(TeamMember dislikingTeamMember, Song dislikedSong)
        {
            SongRatingSet songRatingSet = getSongRatingSetForSong(dislikedSong);
            songRatingSet.RateSong(dislikingTeamMember, SongRating.Dislike);
        }

        protected internal override void MarkSongAsLiked(TeamMember likingTeamMember, Song likedSong)
        {
            SongRatingSet songRatingSet = getSongRatingSetForSong(likedSong);
            songRatingSet.RateSong(likingTeamMember, SongRating.Like);
        }

        private SongRatingSet getSongRatingSetForSong(Song song)
        {
            SongRatingSet songRatingSet;
            if (!songRatingStore.TryGetValue(song.Id, out songRatingSet))
            {
                songRatingSet = new SongRatingSet();
                songRatingStore.Add(song.Id, songRatingSet);
            }

            return songRatingSet;
        }
    }
}