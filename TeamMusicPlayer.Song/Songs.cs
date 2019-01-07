namespace TeamMusicPlayer.SongLibrary
{
    using System.Collections.Generic;

    public static class Songs
    {
        private static SongDao songDao;

        public static void BeginTestMode()
        {
            songDao = new TestSongDao();
        }

        public static IEnumerable<Song> GetAllSongs() => songDao.SelectAllSongs();

        public static Song GetSongByName(string songName) => songDao.SelectSongByName(songName);

        public static Song AddSong(string songName, string artistName) => songDao.InsertSong(songName, artistName);
    }
}