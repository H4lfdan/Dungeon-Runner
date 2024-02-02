# Dungeon-Runner
**Overview**

  Dungeon Runner is an FPS build in Unity in which you progress through a dungeon level full of monsters, armed with a crossbow and a rifle. Fight your way to the objective in the deepest part of the dungeon to complete the level!

  I built this project to experiment with first person level design, ammo resource management, enemy group behavior, destructable terrain, and two different mechanics for player weaponry. 
  Enemies will detect the player if they have line of sight and are within detection range. There is a very short grace period of nondetection before they become provoked. Enemies are also capable of alerting nearby allies to the player's presence, so they can and will attack in groups.
  Enemies will also deaggro if out of sight and detection range for a significant amount of time.

  ![Dungeon-Runner1](https://github.com/H4lfdan/Dungeon-Runner/assets/105895180/1d619fd4-7d31-4fc4-b026-de4ad1d39d7e)


**Controls**
  - Movement: WASD for forward, back and side movement, Spacebar to jump, and hold Shift to sprint.
  - Camera Control: Mouse Look
  - Fire Weapon / Reload Crossbow: Left Mouse Button
  - Weapon Zoom: Right Mouse Button
  - Pause / Resume Game: Esacpe

#How to Run the Game

  ![Dungeon-Runner ReadMe Instructions](https://github.com/H4lfdan/Dungeon-Runner/assets/105895180/d107044f-5a29-465d-828a-788054011b41)
  .

  **With Git**
  
  If you have Git installed, click the green "Code" drop down menu, and copy the HTTPS address to your clipboard.
  Open GitBash from the location you want to install the game, and input the command "git clone" and paste the HTTPS address. Once the clone is complete, locate and open the "Dungeon Runner" executable, and you're playing!
  
  ![Dungeon-Runner GitBash](https://github.com/H4lfdan/Dungeon-Runner/assets/105895180/2d8d1ab2-3a78-4eb2-b511-83737ec0d8b1)

  ![image](https://github.com/H4lfdan/Dungeon-Runner/assets/105895180/fe92a7e7-52ad-40d1-8352-2a98088eec40)



  **Without Git**

  If you haven't got Git installed, still click the green "Code" drop down menu, but select "Download Zip" instead. Once downloaded, opening the Dungeon Runner executable will prompt you to extract the necessary files. Extract them all, and the game is ready.

  Enjoy!
  
  .

# Features

**Player**

  The player is equipped with two weapons: a rifle and a crossbow. The rifle functionality was taken from GameDev.tv's *Zombie Runner* project, which raycasts forward from the player camera and instantiates a particle effect and explosive physics force at the point of impact. The rifle does not explicitly reload, but its "Shoot" method is an IEnumerator which limits the rate of fire. 
  
  The crossbow does not raycast, and has an explicit reload function which has to be triggered by player input. If the player clicks to fire, and there is no arrow loaded, the crossbow's "ReloadAfterTime" method will instantiate a new arrow childed to the crossbow at the end of a short reload animation. Only after the animation is complete and the arrow is created will the bow fire, unchilding the arrow, imparting a forward impulse onto it, and playing the shoot animation. The arrow  then self destructs upon coming into contact with an enemy, or after a short timer. This crossbow, as well as the beholder and mimic models, were my introduction to managing object animations.
  
  The weapons both require ammunition to function, which is tracked separately for each weapon, and displayed at the bottom right corner of the screen. The Ammo code lives on the player, where the "Arrows" and "Bullets" ammo slots have to be declared in the editor.

  With more time put into this project, I would like to include simple melee weapon options with limited durability, and even a shield option which could actively block and nullify incoming attacks. I also would like to provide the player with an explosive weapon option which destroys terrain as the Beholder Boss's missile attack does. 

**Enemies**

  The beholders and chest monsters, or "mimics", use Unity's built in NavMeshAgent to move and navigate. Though their functionality is somewhat different, they have the same "EnemyAI" script governing their behavior. The enemies begin the game in a state of non-aggression until provoked by the player. In *Zombie Runner*, an enemy would be provoked only if the player dealt damage to it, or came within its aggro range. I expanded this system by giving the enemy agents not just an aggro range, but also an alert ally range, so that when one enemy becomes provoked, each of its allies within alert range will also be provoked. When provoked, their NavMeshAgent destination is set to the player's position so they approach the player, and when they come within their attack range, their destination is set again to their current position, and their attack animation plays. The enemies' damage functionality is bound to an animation event within their respective attack animations. Oringinally the beholders as well as the mimics were melee attackers, but I decided to have the beholders to fire projectiles instead. 
  
  This posed a challenge because at the time, the enemy attack functionality all existed inside the EnemyAI script. Each enemy type had to have the same AI script in order for the AlertAllies method to function without becoming too cumbersome, and yet they had to have different methods trigger from their respective attack animation events. So I left the code that triggered animation in the EnemyAI script, but pulled the content of the attack into separate scripts. This way when a mimic attacks, it applies damage directly. However when a beholder attacks, it instantiates a moving projectile which deals damage to the player on collision.

  I also wanted to introduce some rudimentary stealth functionality, so I set each enemy to raycast from their position to the player camera. If they ray hits the enemy's target, the enemy has line of sight. This let me prevent enemies from becoming provoked through walls, but it didn't strictly help the player behave more stealthily. I wanted to create a grace period, where the player could come into range and line of sight of an enemy, and escape without being noticed. So I created an aggression process, in which a timer counts up while the player is in line of sight and aggro range. When that timer hits a threshold, the agents is provoked. Then I used the same method to create a deaggro timer, so that when line of sight is broken for long enough, the enemy will lose interest, and its NavMesh destination will be set to its starting location.

  I considered having the beholders only fire if they had line of sight, but I wanted to reward the player for using clever positioning to take cover from ranged attacks. However this made ranged enemies very easy to manipulate so the player could fire on them without risking taking damage. I then created the Beholder Boss, and made it fire a separate projectile. Regular beholder missiles only find the player's location when they are instantiated, and then move in the direction of that location. I set the Boss's projectile to LookAt the target in Update, and I had a seeker missile. Finally, perhaps my favorite feature of this entire project, in order to ensure that the player has to stay on the move when facing a Beholder Boss, I gave most of the terrain the "Destructable" tag, and set the seeker to destroy destructable objects on collision.

  In the future, I intend to have the NavMesh update whenever terrain is destroyed. As it stands, enemies will continue to navigate over destroyed terrain.

**Pickups**

  *Zombie Runner* introduced ammo pickups of different ammo types, but those pickups were only placed statically throughout the world. I wanted to incentivise the player to not be wasteful with their ammo, so I limited the amount of ammo drops throughout the map, and made mimics drop arrow pickups on death. By default, if the player does not miss a shot when killing a mimic with a crossbow, they will net gain one arrow. I then created a pickup with access to the PlayerHealth script, and made a makeshift health drop, to be dropped by beholders on death. Bullet pickups, remain only found in the world, as the rifle is far stronger than the crossbow.

  With more time put into this project, I may put a cap on maximum player health, to prevent astronomical health totals, and also give bullet ammo drops to higher threat enemies.

  ![Dungeon-Runner2](https://github.com/H4lfdan/Dungeon-Runner/assets/105895180/20075b8c-2cf7-4f4e-92f8-b6ca880a30f5)

 


**Credits**

  - The basis of this project is taken from the *Zombie Runner* project from the Complete C# Unity Game Developer 3D course by Ben Tristem, Rick Davidson, the GameDev.tv Team, and Gary Pettie. https://www.udemy.com/course/unitycourse2/
  - Additional arrow functionality research from Nicolai Andersen https://www.youtube.com/watch?v=Fu9X3OowEy0
  - The level is populated with monsters based on the beholders and mimics of Forgotten Realms. Models and animations made by Dungeon Mason: http://alexkim0415.wixsite.com/dungeonmason
  - Classical Crossbow asset by RyuGiKen https://assetstore.unity.com/packages/3d/props/weapons/classical-crossbow-196127
  - Vintage rifle - Western model asset by Dim Lit Studio https://assetstore.unity.com/packages/3d/props/weapons/vintage-rifle-western-model-211535
  - Low Poly Dungeons Lite assets by JustCreate https://assetstore.unity.com/packages/3d/environments/dungeons/low-poly-dungeons-lite-177937
  - Campfires & Torches Models and FX! assets by Piloto Studio https://assetstore.unity.com/packages/3d/environments/campfires-torches-models-and-fx-242552
  - Music: *Instinct*, by Benjamin Tissot https://www.bensound.com/free-music-for-videos
  - Sound effects from freesound.org users andrest2003, Andromadax24, Bertsz, Erdie, Go2SleepD, MadPanCake, Micheal Klier, Michel88, phlair, and Robinhood76. 

