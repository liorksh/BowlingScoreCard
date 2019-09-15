# Bowling Score Card

## Overview
This .NET Core projects exemplify a bowling game application, written based on Functional Programming principles.
There are three projects: 
1. Console application (BowlingScorecardApp): an interactive bowling game
2. Bowling game business logic (BowlingScorecard)
3. Unit tests project (BowlingForFunTests)

The business logic was written based on the Functional Programming concepts:
* Pure functions. Each function is stateless.
* Immutable objects and variables: creating new objects in case of change.
* Trying to avoid loops and use recursions.

## Main methods

1.	Creating an empty score card by calling a static method:<br>
``ScoreCard.GenerteEmptyScoreCards()
``

2.	Given a score card, score a frame: the method returns a new score card. The method is part of Game class:<br>
``public static ScoreCard RollNewFrame(ScoreCard scoreCard, int tryNo1, int tryNo2)
``
3.	Determine if a game is complete - if so, provide the final score:<br>
`` public static bool IsEligibleForAnotherTry(ScoreCard scoreCard)
``
<br>Calculating the game's score:<br>
``public static int GetScore(ScoreCard scoreCard)
``
<br>Get the score of a single frame. If the score is not determied yet (spare or strike), the methid returns null:<br>
``public static int? GetFrameScore(ScoreCard scoreCard, int index)
``

## Running the console application

An example for possible output:<br>
![Application output](/Images/GameSapmle1.PNG)
