# Tempo: design

## Overview

Tempo is a chess training site. You play a bot that makes human-like moves at the rating you choose, then get an engine review of the game that explains your mistakes.

## Engines

| Engine | Role |
| --- | --- |
| Maia | Plays as the bot, choosing the move a human of the chosen rating would likely make |
| Stockfish | Analyses games: evaluates each position and finds the best move |

## Stack

| Layer | Technology |
| --- | --- |
| API | C#, ASP.NET Core, REST, using controllers |
| Database | SQL Server, accessed through Entity Framework Core |
| Front end | TypeScript and React |
| Tests | xUnit, with Testcontainers for tests against a real SQL Server |
| CI | GitHub Actions |
| Hosting | Docker and Azure |

## Database schema

### Game

| Column | Type | Notes |
| --- | --- | --- |
| Id | int | Primary key |
| BotElo | int | The rating the bot plays at |
| Status | enum | Ongoing or Complete |
| Outcome | enum, nullable | Draw, ComputerWin or HumanWin; empty while the game is ongoing |
| HumanColor | enum | White or Black |

### Move

| Column | Type | Notes |
| --- | --- | --- |
| Id | int | Primary key |
| GameId | int | Foreign key to Game |
| MoveNumber | int | Order of the move within the game |
| AlgNotation | string | The move played, in algebraic notation |
| EvaluationId | int, nullable | Foreign key to Evaluation; empty until the move is analysed |

### Evaluation

| Column | Type | Notes |
| --- | --- | --- |
| Id | int | Primary key |
| BestMove | string | Stockfish's best move, in algebraic notation |
| Score | int | Stockfish's evaluation |