namespace TeamMusicPlayer.Team
{
    internal abstract class TeamDao
    {
        protected internal abstract Team InsertTeam(string teamName);
        protected internal abstract TeamMember InsertTeamMember(Team team, string firstName, string lastName);
    }
}