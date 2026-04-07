# Curse of Bloodmore

A 2D action-adventure dungeon crawler built in Unity. Explore dark dungeons, fight undead enemies, complete quests, and uncover the curse that plagues the land of Bloodmore.

## How to Play

### Controls

| Action           | Input      |
| ---------------- | ---------- |
| Move             | WASD       |
| Jump             | Space      |
| Dash             | Shift      |
| Attack           | Left-Click |
| Block            | Q          |
| Ultimate Spell   | ?          |
| Interact         | F          |
| Toggle Inventory | X          |

### Gameplay

- Talk to NPCs to accept quests. Available quests appear in the panel on the left side of the screen.
- Explore each level to find chests containing weapons, potions, keys, gold, and quest items.
- Fight skeleton enemies by timing your attacks and using counter attacks and dash to avoid damage.
- Complete quest objectives and return to the quest giver to collect rewards.

## How to Run

### Playable Build

1. Download the build from the unity editor.
2. Extract the zip file.
3. Run `CurseOfBloodmore.exe`.

### Unity Editor

1. Clone or download this repository.
2. Open the project in **Unity 6**.
3. Open the `Start` scene located in `Assets/Scenes/Start`.
4. Press Play.

## Features

- Side-scrolling 2D combat with attack, counter, dash, and ultimate spell mechanics.
- Quest system with accept/decline, objective tracking, and rewards.
- Multiple levels.
- Inventory and item system.
- Skill system.
- NPC dialogue system.
- Parallax scrolling backgrounds.
- Start screen, win screen, and death screen.

## Scenes

| Scene      | Description                  |
| ---------- | ---------------------------- |
| Start      | Title screen and how to play |
| Level 1    | First dungeon level          |
| Level 2    | Second dungeon level         |
| Level 3    | Third dungeon level          |
| Win        | Victory screen               |
| DeadScreen | Game over screen             |

## Project Structure

```
Assets/
  Scenes/         - All game scenes
  Scripts/
    Player/        - Player movement and combat
    Enemy/         - Enemy AI and behavior
    Entity/        - Shared entity logic
    Data/          - Item and quest data
    Inventory/     - Inventory system
    InventorySystem/ - Item management
    ItemSystem/    - Item definitions
    Skill System/  - Player skills
    StateMachine/  - State machine logic
    InteractiveObjects/ - Chests, doors, etc.
    Interface/     - UI interfaces
    UI/            - UI scripts
    Parallax/      - Background scrolling
  Prefab/          - Reusable prefabs
  Materials/       - Materials and shaders
  Graphics/        - Sprites and animations
  Sounds/          - Audio files
```

## Known Issues

Issue with useability of dialog buttons on some machines.

## Credits

- **Team Members:** Tanner Bell | Shelby Maurice | Girolamo Figliomeni
- **Course:** COMP-4432
- **Engine:** Unity 6
