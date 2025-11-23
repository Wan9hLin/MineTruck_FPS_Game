# Enemy & Boss AI System

**Focus:**  
Defines all hostile behaviors that drive combat encounters, from basic monsters to multi-phase boss battles.  
Implements movement, attack logic, damage handling, invincibility windows, and minion-summoning mechanics to create escalating combat difficulty.

**Key Scripts:**  
- **MonsterController.cs** – Controls standard enemy movement, melee/ranged attacks, health management, invincibility frames, and death behavior.  
- **BossController.cs** – Manages boss logic, including phased health-based behavior changes, ability patterns, invincibility states, and minion summoning triggers.  
- **SpellRecall.cs** – Handles boss-initiated minion summoning, including spellcasting effects, cooldown tracking, and positioning spawned enemies relative to the boss.
