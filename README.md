# Bowling Score Card

## Overview
This .NET Core projects exemplify a bowling game application, written based on Functional Programming principles.
There are three projects: 
1. Console application (BowlingScorecardApp): an interactive bowling game
2. Bowling game business logic (BowlingScorecard)
3. Unit tests project (BowlingForFunTests)

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
