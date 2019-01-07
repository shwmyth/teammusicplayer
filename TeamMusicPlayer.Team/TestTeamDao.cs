namespace TeamMusicPlayer.Team
{
    using System.Collections.Generic;

    internal sealed class TestTeamDao : TeamDao
    {
        private readonly IDictionary<int, TeamMember> teamMemberStore = new Dictionary<int, TeamMember>();
        private readonly IDictionary<int, Team> teamStore = new Dictionary<int, Team>();
        private int teamIdCounter = 1;
        private int teamMemberIdCounter = 1;

        private readonly IDictionary<int, List<TeamMember>> teamTeamMemberLinkStore =
            new Dictionary<int, List<TeamMember>>();

        protected internal override Team InsertTeam(string teamName)
        {
            var teamMembers = new List<TeamMember>();
            teamTeamMemberLinkStore.Add(teamIdCounter, teamMembers);
            var team = new Team(teamIdCounter, teamName, teamMembers);
            teamStore.Add(teamIdCounter, team);
            ++teamIdCounter;
            return team;
        }

        protected internal override TeamMember InsertTeamMember(Team team, string firstName, string lastName)
        {
            var newTeamMember = new TeamMember(teamMemberIdCounter, firstName, lastName);
            teamMemberStore.Add(teamMemberIdCounter, newTeamMember);
            ++teamMemberIdCounter;
            List<TeamMember> teamMembers = teamTeamMemberLinkStore[team.Id];
            teamMembers.Add(newTeamMember);
            return newTeamMember;
        }
    }
}