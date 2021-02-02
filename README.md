# Space-shooter

Jeu de style "Shoot'em Up" réalisé avec Unity dans le cadre d'un cours.

![alt text](https://github.com/Dakistos/Space-shooter/blob/master/Assets/Sprites/screenshot.png)

## Gameplay

- Joueur :
  - Le joueur ne peut se déplacer que de gauche à droite et ne peut sortir de l'écran.
  - Capacité de tirer des missiles (avec clic gauche)
- Ennemis :
  - Tireurs :
    - Déplacement de haut en bas et ne peut sortir de l'écran
    - Capacité de tirer des missiles
  - Ennemis rapides :
    - Déplacement du bas vers le haut
    - Détruit lorsqu'il sort de l'écran ou rencontre le joueur
    - Pas de capacité spéciale, le joueur doit simplement les éviter
- Capsules bonus (Lachée à la mort du dernier ennemi d'une vague)
  - Vie : Donne +1 vie au joueur
  - Bouclier : Apporte un bouclier au joueur (capacité de 10 vies)
  
## UML

### GameManager
Classe pour gérer les interfaces et autres comportements inhérents au jeu
```
States {wait,play,dead} : enum
score : int
lives : int
scoreTxt : Text
livesTxt : Text
messageTxt : Text
player : GameObject
boom : GameObject
cam : Camera
height : float
waitToStart : GameObject
-----------------
md_LaunchGame() : void
md_LoadLevel() : void
md_InitGame() : void
md_UpdateTexts() : void
md_AddScore() : void
md_KillPlayer() : void
PlayerAgain() : IEnumerator
md_GameOver() : void
```
### md_PauseManager
Classe pour gérer la mise en pause du jeu
```
pauseTxt : Text
-----------------
md_PauseGame() : void
```
### md_EnemySpawner
Classe pour créer les vagues d'ennemis
```
SpawnState {spawn,wait,count} : enum
timer : float
enemyPrefab[] : GameObject
capsule : md_CapsuleBonus
LastEnemyPosition : Vector3
-----------------
md_SpawnEnemies() : IEnumerator
md_LastEnemy() : void
```
### md_Ship
Classe pour gérer le comportement du joueur
```
speed : float
shield : Transform
activateShield : bool
shieldLife : int
projectile : GameObject
projectileSpeed : float
nexFire : float
rb : RigidBody2D
gameManager : GameManager
capsule : md_capsule
-----------------
md_Fire() : void
md_Shoot() : void
md_Move() : void
md_HasShieldTest() : void
OnTriggerEnter2D(Collider2D) : void
```
### md_Enemys
Classe pour gérer le comportement des ennemis
```
maxValue : float
minValue : float
position_Y : float
speed : float
canShoot : bool
fireRate : float
nextFire : float
projectile : GameObject
points : int
gameManager : GameManager
-----------------
md_Move() : void
md_Shoot() : void
OnTriggerEnter2D(Collider2D) : void
```
### md_Bullet
Classe pour gérer le comportement des projectiles
```
cam : Camera
height : float
width : float
speed : float
is_EnemyBullet : float
-----------------
OnTriggerEnter2D(Collider2D) : void
```
### md_CapsuleBonus
Classe pour gérer le comportement des capsules bonus
```
gameManager : GameManager
capsule[] : GameOject
bonusItem : GameObject
playerGO : GameObject
speed : float
cam : Camera
height : float
width : float
-----------------
md_GetBonus(Vector3) : void
md_Heal() : void
md_Shield() : void
md_DestroyCapsule() : void
```

## Assets sprites

https://www.kenney.nl/assets/space-shooter-redux
