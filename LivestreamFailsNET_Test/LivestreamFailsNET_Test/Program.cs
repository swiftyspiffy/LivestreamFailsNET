using System;

namespace LivestreamFailsNET_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new LivestreamFailsNET.Client();
            
            var failsJustChatting = client.GetFails(timeframe: LivestreamFailsNET.Enums.Timeframe.YEAR, game: "Just Chatting");
            Console.WriteLine($"Game: {failsJustChatting[0].Game}\nId: {failsJustChatting[0].Id}\nNSFW: {failsJustChatting[0].NSFW}\nPoints: {failsJustChatting[0].Points}\nStreamer: {failsJustChatting[0].Streamer}\nThumbnail: {failsJustChatting[0].ThumbnailUrl}\nTitle: {failsJustChatting[0].Title}\nUrl: {failsJustChatting[0].Url}");

            Console.WriteLine("--------------")

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
