# Mining & Escort System

**Focus:**  
Implements the full mining → storage → escort → repair gameplay loop that defines the core progression of Minetruck.  
This system handles mining interactions, cart pathing, repair minigames, resource storage, and vehicle health—ensuring smooth flow from exploration to escort defense.

**Key Scripts:**  
- **CarRepairModule.cs** – Implements the repair minigame using a timing/slider mechanic; successful repairs restore cart health and contribute to escort progression.  
- **CarRepairEnable.cs** – Toggles the repair mode based on player input (e.g., **G** key), controls visibility of repair UI, and resets repair progress after completion.  
- **DetectPlayer.cs** – Detects when the player enters escort zones to start/pause cart movement, update wheel rotation, and trigger related sound effects.  
- **CarHealthController.cs** – Manages cart/vehicle health, including damage intake, UI updates, destruction logic, and temporary invincibility states.  
- **Detecting.cs** – Handles entering/exiting mining zones, activates mining UI, and enables interaction with mining progress systems.  
- **progressBar.cs** – Controls the mining progress slider, filling behavior, and resource acquisition when a mine is completed.  
- **storgeValue.cs** – Tracks stored minerals, updates the UI, and triggers win conditions once the required resource quota is met.  
- **Follower.cs** – Moves the cart or escort vehicle along predefined waypoint paths, with the ability to start/stop movement via the `enabled` flag.
