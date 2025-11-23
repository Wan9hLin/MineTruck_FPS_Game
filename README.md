# Minetruck

## Overview
**Minetruck** is a first-person shooter adventure built in **Unity**, inspired by games like *Deep Rock Galactic*.  
Set on a hostile alien planet where humanity is forced to mine scarce resources, the game blends **FPS combat**, **mineral mining**, **escort mechanics**, and **base defense** into a unified gameplay loop.

This repository contains the full Unity project for technical review, along with a curated **CoreCodeSamples/** folder that highlights the most relevant gameplay systems—including player control, mining/escort logic, vehicle repair, and enemy/boss AI.

---

## Technical Highlights
- **Player Control Framework** – First-person movement, gravity, jumping, ground detection, speed tuning, and integrated footstep audio.  
- **Modular Weapon & Skill System** – Weapon switching, ammo management, grenade skills, airstrikes, and a flexible cooldown architecture.  
- **Mining & Escort Mechanics** – Mining progress logic, resource storage, cart movement along predefined paths, repair mini-games, and escort triggers.   
- **Enemy & Boss AI** – Custom AI for melee/ranged monsters, plus a multi-phase boss with invincibility cycles, minion summoning, and unique attack patterns.  

---

## How to Use

### Requirements
- Unity **2021.3 LTS** or later  
- Windows PC  
- No additional external packages required  

### Setup
1. Clone or download this repository.  
2. Open the project in **Unity Hub**.  
3. Open the main scene located under:
 ```text
   Assets/Scenes/Main Menu.unity
```
4. Press Play in the Unity Editor to start the game.

---

## Core System Architecture
The project includes a complete Unity implementation, while the **CoreCodeSamples/** folder highlights the most essential gameplay systems for technical review.  
Each submodule represents a major component of the game’s FPS–mining–escort–defense framework, reflecting modular and scalable system design.

### CoreCodeSamples/Player/
Handles all player-centered systems, including first-person movement, jumping, gravity, input responsiveness, locomotion feel, and combat-ready control logic.

### CoreCodeSamples/WeaponSkill/
Implements the weapon and skill framework used throughout combat encounters.  
Supports weapon switching, ammo tracking, and player abilities such as grenades, bullet support, and airstrikes.

### CoreCodeSamples/MiningEscort/
Defines the full mining → storage → escort → repair gameplay loop.  
Includes mining progress logic, cart movement along predefined paths, repair minigame mechanics, and vehicle health/damage flow.

### CoreCodeSamples/EnemyBossAI/
Drives enemy behaviors and encounter pacing.  
Covers standard monster AI, boss phase transitions, minion summoning, and dynamic difficulty escalation based on health thresholds.

---

### Links
🌐 **Portfolio Page** – Full project breakdown and gameplay demo video  
*([Click here](https://www.henrywang.online/copy-of-dragonoath))*
