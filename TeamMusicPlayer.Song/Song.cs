namespace TeamMusicPlayer.SongLibrary
{
    public sealed class Song
    {
        internal Song(int id, string name, string artistName)
        {
            Id = id;
            Name = name;
            ArtistName = artistName;
        }

        public int Id { get; }

        public string Name { get; }

        public string ArtistName { get; }

        public override string ToString() => "'" + Name + "' by " + ArtistName;
    }
}