namespace TeamMusicPlayer.Team
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public sealed class Team
    {
        private readonly List<TeamMember> members;

        internal Team(int id, string name, List<TeamMember> members)
        {
            Id = id;
            Name = name;
            this.members = members;
        }

        public int Id { get; }

        public string Name { get; }

        public ReadOnlyCollection<TeamMember> Members => members.AsReadOnly();

        public TeamMember GetMemberByName(string firstName, string lastName)
        {
            return members.FirstOrDefault(
                m => m.FirstName.Equals(firstName) && m.LastName.Equals(lastName));
        }
    }
}