# RealmeyeSharp
[Realmeye API](https://www.realmeye.com/) that gets information on realmeye to string variable in c#

I have made it using web scraping.
# Example
Go to tester and look at the Program.cs for more help.

How to use:

you need to use Summary before you can use anything else.

user = Realm.GetUserSummary(string IGN)

Realm.GetUserPetStats(User user)

Realm.GetUserDescription(User user)

Realm.GetUserClasses(User user).

and to use the data use
User user = new User();
user.Name and etc

# MyNuget
Install-Package RealmeyeSharp -Version 2.0.0

Or use My NuGet Package manager and search after: RealmeyeSharp

# License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

# Author
My IGN in realm is: [Celestial](https://www.realmeye.com/player/Celestial)

# Helper
I have gotten some ideas from [thesuicideheart](https://github.com/thesuicideheart/)
