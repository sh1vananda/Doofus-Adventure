# Doofus Adventure Game

## Overview

This project is a Unity-based game where the player controls a character ("Doofus") navigating across platforms ("Pulpits") that spawn and disappear over time. The game is a simple yet engaging challenge where players need to continuously move Doofus to new platforms to keep the game going, while keeping track of the score as they progress.

## Game Mechanics

- **Pulpit Spawning:** 
  - The game starts with a single pulpit spawned at the origin. 
  - Every few seconds, a new pulpit spawns adjacent to the current one, and the previous pulpit disappears.
  - The old pulpit is destroyed after its time is up, ensuring only two pulpits are present at any given time.

- **Character Movement:**
  - The player controls Doofus using the arrow keys or WASD keys.
  - Doofus can move freely across the pulpits as long as they are active.

- **Score System:**
  - The score increments each time Doofus successfully moves onto a new pulpit.
  - The score is displayed on the screen using TextMeshPro and updates in real-time.

- **Timer:**
  - Each pulpit has a countdown timer displayed on it, showing the remaining time before it disappears.
  - The timer is displayed in seconds and milliseconds, giving players precise feedback.

## Implementation Details

### PulpitManager
- Handles the spawning and destruction of pulpits.
- Manages the timing of pulpit appearances and disappearances to ensure smooth gameplay.
- Spawns new pulpits at random adjacent positions to the current pulpit.
- Integrates the `PulpitTimer` script to manage the pulpit lifetime and destruction.

### DoofusController
- Handles the movement of the player's character.
- Interacts with the `ScoreManager` to update the score when Doofus enters a new pulpit.

### ScoreManager
- Manages and updates the player's score.
- Displays the score on the screen using a TextMeshPro UI element.

### PulpitTimer
- Implements the countdown timer for each pulpit.
- Displays the remaining time on the pulpit until it disappears.

## Dependencies

- **Unity:** The game is built using Unity 2020.3 or later.
- **TextMeshPro:** Used for displaying text on UI elements and pulpits.

## Future Enhancements

- Adding more challenging mechanics, such as moving pulpits or varying pulpit sizes.
- Implementing different levels with increasing difficulty.
- Adding sound effects and background music to enhance the gameplay experience.

