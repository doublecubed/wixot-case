# wixot-case

The project is a platformer prototype that demonstrates basic game capability.

The game uses the following "modules" to decouple the gameplay:

* Player Module: Deals with player-related actions like moving and shooting, and activation of abilities. It Uses the IPlayerControls interface
* Weaponry Module: Deals with weapons. Uses the IPlayerWeapon interface for Player module to interact with
* GamePlay Module: Controls game flow and relays inputs to Player Module. Uses IGameFlow interface for interaction with UI
* Input Module: Deals with input. Uses ITapInput and ISwipeInput interfaces, for cases that the two might be needed separately.

Each module is confined to its own namespace. The game also has UI, Utilities and DI namespaces for containing further functionality.
