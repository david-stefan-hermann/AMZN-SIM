# AMZN-SIM
This is a harmless simulation of the life of a warehouse worker of an imaginary online reseller.

## Workflow
1. Read into recommended file structure for a Unity project.
2. Coming up with an idea for the game's concept. Idea:
   > I want to create a Unity 3D desktop game. IT is name AMZN-SIM, short for ****** simulator. The game level will be two rooms, connected by a door. one is the workplace and the other room is the home. the player is a 3d character that can move, and pick up and place. the workplace consists of a package conveyor belt, which moves a constant flow of packages. the player earns money by picking up damaged packages from the conveyor belt and throwing them into a bin. there are three npcs, two are working at the belt and one is an overseer, who  whips the player if they miss a damaged package. the workplace also has two buttons, one for "start working" and one "stop working". If the player enters the home, the state "stop working" will automatically disabled. during this state, the player will not be hurt by the overseer. In the player home, there is a PC, where the player can access the internet and purchase home decorations from amazon. Once purchased, the pop up in predefined spots in the home. Write the correct controllers and everything else for this project.
3. Using Ai for a Proof of Concept.

After creating the Code Prototypes with ChatGPT, it was clear, that it is not the right AI tool for the job.
Switching to Copilot improved the workflow by a lot. The code is now more structured and the AI is able to understand the task better.

## 3D Models
- The main room was created in blender. It consists of a conveyor belt, a bin, two connected rooms, and a computer.
- The Materials I used are free materials from [poliigon.com](https://poliigon.com).
- For the Package, I used two free materials from [texturecan.com](https://texturecan.com).
- For the desk and the purchasable decorations i used the Furniture_FREE asset pack from the Unity Asset Store.

## Sounds
- Some sounds were modified with Audacity.
- Package Collision Sound: [pixabay.com](https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=65573)
- Package Drop and Pickup Sound: [pixabay.com](https://pixabay.com/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=6317)
  FREE Casual Game SFX Pack by Dustyroom Asset Store
- Free UI Click Sound Pack by SwishSwoosh Asset Store

## FPS Controller
- The FPS Controller is a free asset from Unity from the Unity Asset Store.
- I added a script to the FPS Controller, which allows the player to pick up and place the packages.
