namespace TeamMusicPlayer.Team
{
    public static class Teams
    {
        private static TeamDao teamDao;

        public static void BeginTestMode()
        {
            teamDao = new TestTeamDao();
        }

        public static Team CreateTeam(string teamName) => teamDao.InsertTeam(teamName);

        public static TeamMember AddTeamMember(Team team, string firstName, string lastName) =>
            teamDao.InsertTeamMember(team, firstName, lastName);
    }
}