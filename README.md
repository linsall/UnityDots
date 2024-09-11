# UnityDots
 
I have created a game where there spawns a wave of enemies from the top and they go downwards (towards the player), and the enemy can shoot at them (no collisions sadly)
Outside of what was essentially created from the lectures I have made an enemy authoring and enemy move system script. 
The enemy authoring script simply has an IComponentData struct that is used for the movement speed and a baker that adds it to the enemy. 
The enemy move system script is Burst Compiled for performance improvement and all it does is change the enemy's position downwards.
I also changed the SpawnerAuthoring script by adding a variable to change the enemy lifetime, which I use on the SpawnerSystem to add a new Lifetime to the enemy to detroy the enemies after they've gone out of frame
One final change I did to the Spawner is add a variable for how many enemies to spawn which is used in a for-loop in the system to instantiate them in a row on the x-axis