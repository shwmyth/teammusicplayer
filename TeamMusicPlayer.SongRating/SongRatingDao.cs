namespace TeamMusicPlayer.SongRating
{
    using SongLibrary;
    using Team;

    internal abstract class SongRatingDao
    {
        protected internal abstract SongRatingSet SelectSongRatingSet(Song song);
        protected internal abstract void MarkSongAsDisliked(TeamMember dislikingTeamMember, Song dislikedSong);
        protected internal abstract void MarkSongAsLiked(TeamMember likingTeamMember, Song likedSong);
    }
}