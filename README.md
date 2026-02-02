# ⚔️ Scarecrow
![image](https://github.com/GameProgrammingTeam8/Scarecrow/assets/50892930/d8d01a6a-59ed-49c3-a517-6d9ade66b4d1)

<p align="middle">
  <img src="https://img.shields.io/badge/Unity-000000?style=flat&logo=Unity&logoColor=white"/>
  <img src="https://img.shields.io/badge/C%23-6600FF?style=flat&logo=CSharp&logoColor=white"/>
  <img src="https://img.shields.io/badge/Android-3DDC84?style=flat&logo=Android&logoColor=white"/>
</p>

</br>

## Overview

**Scarecrow** is a mobile top-down hack & slash dungeon escape game developed with Unity.

This project focuses on:
- **State-based enemy AI design**
- **Clear separation of combat responsibilities**
- **Refactoring an early-stage project to improve structure and readability**

It is included in my portfolio to demonstrate **system-oriented thinking** and
how gameplay logic can be structured for clarity and extensibility.

</br>

## Tech Stack

- Unity
- C#
- Android

</br>

## Core System

### 1. State-based Enemy AI System
Enemy behaviors are implemented using a **state-based architecture**
to clearly separate responsibilities and simplify future extensions.

**States**
- Chase
- Attack
- Die

**Design Intent**
- Reduce conditional logic inside `Update`
- Make behavior transitions explicit and predictable
- Allow new enemy behaviors to be added without modifying existing states

📂 **Related Code**

Assets/02.Scripts/Unit/Enemy/

</br>

### 2. Combat & Hit Reaction System
Combat logic is designed by separating **hit detection**, **damage processing**,
and **reaction handling**. Damage is applied through a shared IDamageable interface,
while hit feedback and knockback behavior are encapsulated
in a reusable HitReaction component.

**Components**
- Hit Detection
- Damage Processing
- Knockback Reaction

**Design Intent**
- Improve readability and responsibility separation
- Enable future skill and effect expansion without rewriting core combat logic

📂 **Related Code**

Assets/02.Scripts/Unit/

## 🔄 Refactoring Note

This project was originally developed in **2023** with a strong focus on
**gameplay completion**.

Later, a dedicated refactoring branch was created to:
- Improve folder structure
- Clarify naming conventions
- Separate logic by system and state

Gameplay behavior remains unchanged.
The refactoring reflects my **current development standards**.

</br>

## 📼 Gameplay Video
<div align="center">
  
[![Scarecrow-PV](https://i9.ytimg.com/vi_webp/ZiXnrXimhiE/mq3.webp?sqp=CKDVgMwG-oaymwEmCMACELQB8quKqQMa8AEB-AH-CYAC0AWKAgwIABABGD0gTChyMA8=&rs=AOn4CLBsZZKBYMMTSapDAEoY2w0DpqkCJQ)](https://www.youtube.com/watch?v=ZiXnrXimhiE)</br>
Click to watch gameplay

</div>

</br>

## 🎮 Game Description
Scarecrow is a mobile 3D top-view hack & slash dungeon escape game.

Players destroy scarecrow triggers while avoiding enemies,
then escape the dungeon before time runs out.

</br>

## 🎯 Game Objective

- Complete the tutorial
- Enter the dungeon
- Destroy all scarecrow triggers
- Escape before HP or time runs out

</br>

## 🏴 Defeat Conditions

- Player HP reaches 0  
- Time limit expires before escaping

</br>
