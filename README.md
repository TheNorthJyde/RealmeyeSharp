# EyeSharp ![Visitors](https://api.visitorbadge.io/api/visitors?path=https%3A%2F%2Fgithub.com%2FOrbitMPGH%2FEyeSharp&countColor=%23263759)
Makes it easy to gather and interact with data from [Realmeye](https://www.realmeye.com/)

They have no official API, therefore EyeSharp uses WebScraping to get all the information.

Based on [TheNorthJyde's](https://github.com/TheNorthJyde) repository [RealmeyeSharp](https://github.com/TheNorthJyde/RealmeyeSharp)

# Basic usage
All interactions with EyeSharp is done through the RealmEyeClient class.

## Getting a user
First create an instance of RealmEye

``var api = new RealmEyeClient()``

Then you can get a user with their in-game name:

``var user = await api.GetUser(ign);``

# Information available
You can get the following information using just an in-game name:

## User
* Name
* Description
* Amount of Characters
* Amount of Skins
* Fame
* Rank
* Account Fame
* Guild
* Guild Rank
* Creation time
* Characters

## Pet
* Name
* Rarity
* Family
* Ability 1
* Ability 2
* Ability 3
* Max Level

## Guild
* Name
* Member Count
* Characters
* Fame
* Most Active On
* Description
* Members

## Guild Member
* Name
* Guild Rank
* Fame
* Rank
* Characters

# License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

# Credits
* [Orbit](https://github.com/OrbitMPGH) - Current maintainer
* [Argocyte](https://github.com/Argocyte/) - Created old RealmeyeSharp
