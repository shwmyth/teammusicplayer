namespace TeamMusicPlayer.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SongLibrary;
    using SongMix;
    using SongRating;
    using Team;

    [TestFixture]
    public sealed class TestTeamSongMix
    {
        [SetUp]
        public void SetUp()
        {
            Songs.BeginTestMode();
            Teams.BeginTestMode();
            SongRatings.BeginTestMode();
        }

        private void populateTestSongs(string[][] songs)
        {
            foreach (string[] song in songs)
            {
                Songs.AddSong(song[0], song[1]);
            }
        }

        private Team populateTestTeam(string teamName, string[][] teamMembers)
        {
            Team newTeam = Teams.CreateTeam(teamName);
            foreach (string[] teamMember in teamMembers)
            {
                Teams.AddTeamMember(newTeam, teamMember[0], teamMember[1]);
            }

            return newTeam;
        }

        private void assertPlayListOrderBySongName(SongMix songMix, IEnumerable<string> songNames)
        {
            foreach (string expectedNextSongName in songNames)
            {
                assertThatNextSongPlayedIsNamed(songMix, expectedNextSongName);
            }
        }

        private void assertThatNextSongPlayedIs(SongMix songMix, Song expectedSong)
        {
            SongMixEntry nextSongMixEntryToPlay = songMix.DetermineNextSongMixEntryToPlay();
            Assert.That(nextSongMixEntryToPlay.Song, Is.EqualTo(expectedSong));
            nextSongMixEntryToPlay.Play();
        }

        private void assertThatNextSongPlayedIsNamed(SongMix songMix, string expectedSongName)
        {
            SongMixEntry nextSongMixEntryToPlay = songMix.DetermineNextSongMixEntryToPlay();
            Assert.That(nextSongMixEntryToPlay.Song.Name, Is.EqualTo(expectedSongName));
            nextSongMixEntryToPlay.Play();
        }

        [Test]
        public void TestThatAddingOnlyOneSongResultsInThatSongPlayingRepeatedly()
        {
            Team team = populateTestTeam("Team1", TestData.FIVE_TEAM_MEMBERS);
            Song song = Songs.AddSong("Sail Away", "David Gray");
            var songMix = new SongMix(team);
            for (var i = 0; i < 10; ++i)
            {
                assertThatNextSongPlayedIs(songMix, song);
            }
        }

        [Test]
        public void TestThatAddingOnlyTwoSongsResultsInTheSongsAlternatingPlay()
        {
            Team team = populateTestTeam("Team1", TestData.FIVE_TEAM_MEMBERS);
            Song song1 = Songs.AddSong("Sail Away", "David Gray");
            Song song2 = Songs.AddSong("Yellow", "Coldplay");
            var songMix = new SongMix(team);
            for (var i = 0; i < 10; ++i)
            {
                assertThatNextSongPlayedIs(songMix, song1);
                assertThatNextSongPlayedIs(songMix, song2);
            }
        }

        [Test]
        public void TestThatWhenASongIsDislikedByATeamMemberItDoesntPlayWhenThatTeamMemberIsListening()
        {
            populateTestSongs(TestData.TEN_SONGS);
            Team team = populateTestTeam("Team1", TestData.FIVE_TEAM_MEMBERS);
            var songMix = new SongMix(team);
            TeamMember dislikingTeamMember = team.GetMemberByName("Bob", "Brickman");
            Song dislikedSong = Songs.GetSongByName("Yellow");

            SongRatings.MarkSongAsDisliked(dislikingTeamMember, dislikedSong);
            IEnumerable<string> defaultPlayOrderBySongName = TestData.TEN_SONGS.Select(s => s[0]);
            assertPlayListOrderBySongName(songMix, defaultPlayOrderBySongName);

            songMix.AddListener(dislikingTeamMember);
            IEnumerable<string> playOrderWithDislikedSongRemoved =
                defaultPlayOrderBySongName.Where(s => !s.Equals(dislikedSong.Name));
            Assert.That(playOrderWithDislikedSongRemoved.Count(), Is.EqualTo(9));
            assertPlayListOrderBySongName(songMix, playOrderWithDislikedSongRemoved);
        }
        [Test]
        public void TestThatMostLikedSongPlaysMost()
        {
            populateTestSongs(TestData.TEN_SONGS);
            Team team = populateTestTeam("Team1", TestData.FIVE_TEAM_MEMBERS);
            var songMix = new SongMix(team);

            //add liked and unliked songs for Bob
            TeamMember ActiveTeamMember = team.GetMemberByName("Bob", "Brickman");
            Song dislikedSong = Songs.GetSongByName("Yellow");
            SongRatings.MarkSongAsDisliked(ActiveTeamMember, dislikedSong);
            Song likedSong = Songs.GetSongByName("Lovefool");
            SongRatings.MarkSongAsLiked(ActiveTeamMember, likedSong);
            likedSong = Songs.GetSongByName("Amber");
            SongRatings.MarkSongAsLiked(ActiveTeamMember, likedSong);

            //add liked songs for Sue
            TeamMember LikingTeamMember = team.GetMemberByName("Sue", "Smith");
            likedSong = Songs.GetSongByName("Amber");
            SongRatings.MarkSongAsLiked(LikingTeamMember, likedSong);
            likedSong = Songs.GetSongByName("Mustang Sally");
            SongRatings.MarkSongAsLiked(LikingTeamMember, likedSong);
            likedSong = Songs.GetSongByName("Waiting In Vain");
            SongRatings.MarkSongAsLiked(LikingTeamMember, likedSong);

            //add liked songs for Matt
            LikingTeamMember = team.GetMemberByName("Matt", "Miller");
            likedSong = Songs.GetSongByName("Amber");
            SongRatings.MarkSongAsLiked(LikingTeamMember, likedSong);
            likedSong = Songs.GetSongByName("Shut Your Eyes");
            SongRatings.MarkSongAsLiked(LikingTeamMember, likedSong);
            likedSong = Songs.GetSongByName("Waiting In Vain");
            SongRatings.MarkSongAsLiked(LikingTeamMember, likedSong);

            IEnumerable<string> defaultPlayOrderBySongName = TestData.TEN_SONGS.Select(s => s[0]);
            assertPlayListOrderBySongName(songMix, defaultPlayOrderBySongName);

            //Notes
            //I would randomize the songs after this 
            songMix.addLikedSongsToMix();

            //check Songmix has original list + liked
            Assert.That(songMix.TotalEntryCount(), Is.EqualTo(18));

            songMix.AddListener(ActiveTeamMember);

            //check Songmix has original list + liked - dislikes
            Assert.That(songMix.TotalEntryMinusDislikesCount(), Is.EqualTo(17));

            //check most liked song appears in mix most often
            Assert.That(songMix.SongInMixCount("Amber"), Is.EqualTo(4));
            Assert.That(songMix.SongMostInMix(), Is.EqualTo("Amber"));
        }
    }
   
}