# Extinct-Exposure

https://play.unity.com/en/games/7fc0098d-124e-4af4-98f7-ef6b7d31c341/extinct-exposure

 Modern time photographer gets sent back to the Jurassic period! Take pictures of all the dinosaurs to return. Game was created in Unity.

# Game description. 
The player will be a character from a modern time who is working as a photographer. They get sent back in time somehow and end up in the Jurassic period where they find themselves surrounded by dinosaurs. The player will be traveling around the map taking pictures of the many creatures they encounter on the island. Whenever they capture a picture of a creature, they will be able to access the picture and read a description of the creature in the menu. The goal of the game is for the player to capture pictures of all the creatures so that they will be allowed to return back to their time period. The target audience for this game is people who are interested in dinosaurs and want to learn more about them.

## Core Features

### Camera System
- Screenshot capture system with real-time feedback
- Battery management (limited shots)
- Camera flash effect
- Saves photos to local Screenshots directory
- Battery cheat code ('B' key)

### Dinosaur Recognition
- Supports 3 unique dinosaurs:
  - Pachycephalasaurus
  - Stegasaurus_20K
  - PBR_Velociraptor_Blue
- Real-time dinosaur detection
- Success/Failure feedback UI

### Encyclopedia System
- Interactive menu ('E' key)
- Multiple pages with descriptions
- Shows captured dinosaur photos
- Progress tracking
- Left/Right arrow navigation
- Win condition when all dinosaurs photographed

### Game Systems
- First-person controller
- Pause menu system
- Win screen with options:
  - Resume gameplay
  - Return to main menu
- Time scale management
- Cursor lock/unlock system

### UI Features
- Battery level indicator
- Photo feedback messages
- Interactive button highlights
- Menu transitions
- Encyclopedia page system

### Save System
- Automatic screenshot saving
- Timestamp-based filenames
- Directory management
- Photo tracking per dinosaur

## Dinosaur AI System

### State Machine Architecture
- Multiple behavior states:
  - Wander: Default exploration state
  - Chase: Active pursuit of player
  - Idle: Stationary observation
  - State transitions based on player proximity and visibility

### Movement System
- **Wander State**:
  - Random directional movement
  - Configurable wandering speed (0.5f default)
  - Smooth rotation (0.01f rotation speed)
  - Animation state management
  - Boundary awareness and correction

- **Chase State**:
  - Player tracking
  - Increased movement speed
  - Direct pursuit mechanics
  - Line of sight checks

### Boundary System
- Defined play area enforcement
- Center point (248,27,250)
- Automatic return behavior when outside bounds
- Smooth transition back to boundary

### Environmental Awareness
- Collision detection
- Boundary checks
- Player distance calculations
- Line of sight verification

### Animation Integration
- Walking animation states
- Idle animation states
- Smooth transitions between states
- Speed-based animation control

### Player Interaction
- Proximity detection
- Chase initiation distance
- Return to wander when player distant
- Animation state changes based on behavior

