# Bowling Score Card

## Overview
This .NET Core source code exemplify a bowling game application, written based on Functional Programming principles.
It composes of three C# projects: 
1. Console application (BowlingScorecardApp): an interactive bowling game
2. Bowling game business logic (BowlingScorecard)
3. Unit tests project (BowlingForFunTests)

The business logic was written based on the Functional Programming concepts:
* Pure functions. Each function is stateless.
* Immutable objects and variables: creating new objects in case of change.
* Trying to avoid loops and use recursions.

## Main Methods

1.	Creating an empty scorecard by calling a static method:<br>
``ScoreCard.GenerteEmptyScoreCards()
``

2.	Given a scorecard, score a frame: the method returns a new score card. The method is part of Game class:<br>
``public static ScoreCard RollNewFrame(ScoreCard scoreCard, int tryNo1, int tryNo2)
``
3.	Determine if a game is complete and provide the final score:<br>
`` public static bool IsEligibleForAnotherTry(ScoreCard scoreCard)
``
<br>Calculating the current score of the game (not necessarly at the end of the game):<br>
``public static int GetScore(ScoreCard scoreCard)
``
<br>Get the score of a single frame. If the score is not determied yet (spare or strike), the methid returns null:<br>
``public static int? GetFrameScore(ScoreCard scoreCard, int index)
``

## Running the Console Application

An example for possible output:<br>
![Application output](/Images/GameSapmle1.PNG)

The output when scoring 10 strikes:<br>
![Application output](/Images/FulleSetStrikes.PNG)
