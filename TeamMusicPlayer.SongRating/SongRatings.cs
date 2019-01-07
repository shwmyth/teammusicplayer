namespace TeamMusicPlayer.SongRating
{
    using SongLibrary;
    using Team;

    public static class SongRatings
    {
        private static SongRatingDao songRatingDao;

        public static void BeginTestMode()
        {
            songRatingDao = new TestSongRatingDao();
        }

        public static SongRatingSet GetSongRatingSet(Song song) => songRatingDao.SelectSongRatingSet(song);

        public static void MarkSongAsDisliked(TeamMember dislikingTeamMember, Song dislikedSong)
        {
            songRatingDao.MarkSongAsDisliked(dislikingTeamMember, dislikedSong);
        }

        public static void MarkSongAsLiked(TeamMember likingTeamMember, Song likedSong)
        {
            songRatingDao.MarkSongAsLiked(likingTeamMember, likedSong);
        }
    }
}