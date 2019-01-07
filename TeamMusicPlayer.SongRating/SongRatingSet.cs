namespace TeamMusicPlayer.SongRating
{
    using System.Collections.Generic;
    using Team;

    public sealed class SongRatingSet
    {
       private readonly IDictionary<int, SongRating> songRatingsByTeamMemberId =
            new Dictionary<int, SongRating>();

        internal void RateSong(TeamMember teamMember, SongRating songRating)
        {
            songRatingsByTeamMemberId[teamMember.Id] = songRating;
        }

        public bool HasDislikedRatingBy(TeamMember teamMember)
        {
            SongRating songRating = getSongRatingForTeamMember(teamMember);
            return songRating == SongRating.Dislike;
        }

        public bool HasLikedRatingBy(TeamMember teamMember)
        {
            SongRating songRating = getSongRatingForTeamMember(teamMember);
            if (songRating == SongRating.Like)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private SongRating getSongRatingForTeamMember(TeamMember teamMember)
        {
            SongRating songRating;
            if (songRatingsByTeamMemberId.TryGetValue(teamMember.Id, out songRating))
            {
                return songRating;
            }

            return SongRating.NoRating;
        }
    }
}