
Welcome to the Energage Team Music Player codebase!

PROJECT OVERVIEW

We often listen to music in our development team room.  Imagine each of our desks is wired
up to a central music source, and we each control the volume coming out of
our own speakers.  This system has worked very well, but we have yet to find an
ideal system for collaboratively selecting the songs in the play list.  This
codebase represents the beginning of a system that could help with this problem.

The current codebase is intended to be a simple proof-of-concept implementation of
the following requirements:

1)  Provide the ability to define a team with members.
2)  Provide the ability for team members to rate songs in the song library.
3)  Keep track of team members who are currently listening to the mix.
4)  If a team member "dislikes" a song, and if that team member is currently
	listening, then that song will not play in the mix.
5)  The more "likes" a song gets from team members, the more frequently it
	will play in the mix.

THE STATE OF THE CODE

There are five projects in the TeamMusicPlayer solution.  There is no executable
project because to this point development has been driven by unit tests in the
TeamMusicPlayer.Tests project using only test data.

THE MISSION

Currently, the code is a minimal implementation of requirements #1-4 above.

Your mission is to implement requirement #5.  Please consider the codebase to be
very much a work in progress, and feel free to modify it however you see fit to
reach this goal.

If you stop before fully meeting the requirement, please let us know how much time
you spent and what you would do to complete the work.

Good luck and happy coding!

The Energage Development Team

