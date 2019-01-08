# RealmeyeSharp
[Realmeye API](https://www.realmeye.com/) that gets information of [realmeye](https://www.realmeye.com/) to mostly string variable in C#

I have made it using web scraping.
# Example
Go to tester and look at the Program.cs for more help.

How to use:

User user = new User()

Realm.GetAllUserInfo(String IGN, User user)

you need to use Summary before you can use anything else.

Realm.GetUserSummary(string IGN, User user)

Realm.GetUserPetStats(User user)

Realm.GetUserDescription(User user)

Realm.GetUserClasses(User user).

and to use the data use
User user = new User();
user.Name and etc

or you can look at my [Test / Example](Tester/Program.cs)

# MyNuget
Install-Package RealmeyeSharp -Version 2.2.0

Or use My NuGet Package manager and search after: RealmeyeSharp

# License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

# Author
My IGN in realm is: [Celestial](https://www.realmeye.com/player/Celestial)

# Helper

Additions made by: 
Github: [Argocyte](https://github.com/Argocyte/)
Realmeye: [Argocyte](https://www.realmeye.com/player/Argocyte)
