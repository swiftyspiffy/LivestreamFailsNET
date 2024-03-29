# LivestreamFailsNET

## Overview
Small .NET library that allows you to query Livestreamfails.net for fails, streamers and games, with the data nicely organized.

### Sample Usage
```
namespace LivestreamFailsNET_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new LivestreamFailsNET.Client();
            
            var failsJustChatting = client.GetFails(timeframe: LivestreamFailsNET.Enums.Timeframe.YEAR, game: "Just Chatting");
            Console.WriteLine($"Game: {failsJustChatting[0].Game}\nId: {failsJustChatting[0].Id}\nNSFW: {failsJustChatting[0].NSFW}\nPoints: {failsJustChatting[0].Points}\nStreamer: {failsJustChatting[0].Streamer}\nThumbnail: {failsJustChatting[0].ThumbnailUrl}\nTitle: {failsJustChatting[0].Title}\nUrl: {failsJustChatting[0].Url}");

            Console.WriteLine("--------------");

            var failsXqcow = client.GetFails(timeframe: LivestreamFailsNET.Enums.Timeframe.YEAR, streamer: "xqcow");
            Console.WriteLine($"Game: {failsXqcow[0].Game}\nId: {failsXqcow[0].Id}\nNSFW: {failsXqcow[0].NSFW}\nPoints: {failsXqcow[0].Points}\nStreamer: {failsXqcow[0].Streamer}\nThumbnail: {failsXqcow[0].ThumbnailUrl}\nTitle: {failsXqcow[0].Title}\nUrl: {failsXqcow[0].Url}");

            Console.WriteLine("--------------");

            var games = client.GetGames();
            Console.WriteLine($"Game: {games[0].Name}\nImage: {games[0].ImageUrl}\nFails: {games[0].Fails}");

            Console.WriteLine("--------------");

            var streamers = client.GetStreamers();
            Console.WriteLine($"Streamer: {streamers[0].Name}\nImage: {streamers[0].ImageUrl}\nFails: {streamers[0].Fails}");

            Console.ReadLine();
        }
    }
}
```

### Sample Output
```
Game: Just Chatting
Id: 48692
NSFW: False
Points: 912
Streamer: Reckful
Thumbnail: https://cdn.livestreamfails.com/thumbnail/5cc9e0fc7ec0b.jpg
Title: reckful on mizkif shist
Url: https://livestreamfails.com/post/48692
--------------
Game:
Id: 48714
NSFW: False
Points: 3287
Streamer: xQcOW
Thumbnail: https://cdn.livestreamfails.com/thumbnail/5cca0d497f3dc.jpg
Title: xQc encounters the most adorable stream sniper
Url: https://livestreamfails.com/post/48714
--------------
Game: IRL
Image: https://static-cdn.jtvnw.net/ttv-boxart/IRL-272x380.jpg
Fails: 9817
--------------
Streamer: xQcOW
Image: https://static-cdn.jtvnw.net/jtv_user_pictures/xqcow-profile_image-9298dca608632101-300x300.jpeg
Fails: 2343
```
### Testing
Included is a .NET Core console application I used to test the library as I developed it.

## Contributors
 * Cole ([@swiftyspiffy](http://twitter.com/swiftyspiffy))
 
## License
MIT License. &copy; 2019 Cole