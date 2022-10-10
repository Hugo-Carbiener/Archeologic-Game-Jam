![22-161_-_declis_web-event_0](https://user-images.githubusercontent.com/41008899/194821347-57bfc0dc-d27f-4d89-8031-96ff50a72d40.png)

<h1 align="center" style="font-weight: bold">Steppe by step</h1>

**Steppe by step** is a video game made during the Etiolles Game Jam that took place in Evry, France from the 8 October to the 10 October 2022. This Game Jam was organised to celebrate the **50th anniversary** of the **[archeological excavation site of Etiolles](https://www.etiolles.fr/ma-ville/historique/site-archeologique-detiolles/)**, Essone. 

During this **scientific Game Jam** no specific theme was provided. Rather, we were given acces to an impressive asset database made by researchers and archeologists and filled with field studies, topographic surveys, 3D models of excavated items from the prehistorical magdalenian period and pictures. The aim was to create a game with as little historical and scientifical discrepencies as possible.

Etiolles is an excavation site dedicated to the **Magdalenian culture** that roamed Europe from 17 000 to 12 000 years ago. As such, this is the period on which we based the many details of our game.

![515_ckeditor_actu_44398_63282ac384d31_1](https://user-images.githubusercontent.com/41008899/194822497-fdc1b12c-0b15-457f-b3d8-809c3e008eac.jpg)

(No mammoths were hurt during the making of this game jam nor during the magdalenian period as they already were extinct)

<h2 align="center" style="font-weight: bold">:video_game: Gameplay :video_game:</h2>

This game aims at recreating the way reindeer hunts would take place. As such, you control a group of hunter-gatherers. Your goal is to find traces of the reindeer herd that roams this steppe, follow and corner it, and finally kill at least one to feed your tribe.

<p align="center">
<img align="center" width="848" alt="Capture d’écran 2022-10-10 094615" src="https://user-images.githubusercontent.com/41008899/194824130-cd79c7fa-6ae1-4084-88d6-ea8aa11ab510.png">
<p/>

As a mean to kill your prey, your tribe can throw a **flurry of spears**. But beware, the further you aim, the larger the area where your spears can land. As such, you must make a compromise between between slowly sneaking up on your prey to get closer and not scare it away too soon. 

On a more technical register, the hunters' movement system has been designed to give the player multiple levels of speed which interact with the reindeers' detection range to allow the player to sneak up on an animal and recreate the feel of the hunt.

Similarly, our adaptative music evolves with the different steps of the hunt (roaming, prey in sight, prey and the run and so on) to make the player feel the tension of the situation as they stand nearby their prey.

<h2 align="center" style="font-weight: bold">:deer: Deer herd :deer:</h2>

<p align="center">
<img width="848" alt="Capture d’écran 2022-10-10 095057" src="https://user-images.githubusercontent.com/41008899/194828151-81457301-b07a-49a2-ae49-9b22ab2412d9.png">
<p/>

Magdalenian used to hunt reindeers and horses, but killing horses seemed somewhat too grim so we went with a model of reindeers.

When idle, the herd follows a predetermined path around the steppe. They can spot the hunters and start to flee in the opposite direction. We wanted to implement a way for the player to lead the herd towards natural deadends. This strategy is called hemming hunt and consists in leading your prey towards deadends as you cannot outspeed them. As such, throughout the map, where there are rock formations or natural peninsulas, we placed spots where the deer herd might trap themselves.

<h2 align="center" style="font-weight: bold">:earth_africa: World design :earth_africa:</h2>

<p align="center">
<img align="center" width="803" alt="Capture d’écran 2022-10-10 093700" src="https://user-images.githubusercontent.com/41008899/194830413-743600a4-1f87-4285-ab5c-4f9c6e710126.png">
<p/>

The world has been carefully hand made using low poly models and our archeologist advice. The wind-swept steppe has been voluntarly left somewhat desolated to mimic the landscapes that covered Europe during this era. In parallel, the trees and animal species we chose to put in our world are thought after what could really be encoutered in this region during this period. Colours are voluntarily chosen to be **desaturated** in order to mimic the harsh vegetal environment.

The terrain has also been made realistically as it is a **3D model of a height map representing the prehistorical landscape of central France**.

We've also placed some fish and berries as a mean for the hunter-gatherers to get some stamina while on the hunt !

<div style="display:block;" align="center">
<img width="45%" alt="Capture d’écran 2022-10-10 095552" src="https://user-images.githubusercontent.com/41008899/194831745-3e390ba9-9849-4ac0-a87e-7d2f010e4723.png">

<img width="45%" alt="Capture d’écran 2022-10-10 095734" src="https://user-images.githubusercontent.com/41008899/194831027-c23e6c19-065d-4095-b3e0-c677570689fc.png">
</div>
