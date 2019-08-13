using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LivestreamFailsNET.Helpers
{
    internal static class Parsing
    {
        internal static List<Models.Fail> ParseFails(string html)
        {
            List<Models.Fail> fails = new List<Models.Fail>();
            if (html == null || html.Length < 5)
                return fails;

            var doc = buildDoc(html);
            foreach(var cardDeck in doc.DocumentNode.SelectNodes("//div[@class='card-deck']"))
            {
                foreach(var card in cardDeck.SelectNodes("//div[@class='card col-sm-3 post-card']"))
                {
                    Models.Fail fail = new Models.Fail();

                    fail.Url = card.SelectNodes(".//a").LastOrDefault().Attributes["href"].Value;
                    fail.Id = fail.Url.Split('/')[4];
                    fail.ThumbnailUrl = card.SelectNodes(".//a//img").LastOrDefault().Attributes["src"].Value;
                    fail.Title = card.SelectNodes(".//a//p").LastOrDefault().InnerText.Trim();

                    fail.Points = parsePoints(card.SelectNodes(".//a//small//span[@class='oi oi-arrow-circle-top']").LastOrDefault().ParentNode.InnerText);
                    fail.TimeSinceCreated = parseTime(card.SelectNodes(".//a//small//span[@class='oi oi-clock']").LastOrDefault().ParentNode.InnerText);
                    var nsfwNodes = card.SelectNodes(".//a//small//span[@class='oi oi-warning']");
                    fail.NSFW = nsfwNodes != null && nsfwNodes.Count > 0;

                    var sInfoNodes = card.SelectNodes(".//div[@class='stream-info']");
                    var sInfo = sInfoNodes != null && sInfoNodes.Count > 0 ? sInfoNodes.LastOrDefault() : null;
                    if (sInfo != null)
                    {
                        fail.Streamer = sInfo.SelectNodes(".//small//a")[0].InnerText;
                        fail.Game = sInfo.SelectNodes(".//small//a")[1].InnerText;
                    }

                    fails.Add(fail);
                }
            }

            return fails;
        }

        internal static List<Models.Game> ParseGames(string html)
        {
            List<Models.Game> games = new List<Models.Game>();
            if (html == null || html.Length < 5)
                return games;

            var doc = buildDoc(html);
            foreach (var cardDeck in doc.DocumentNode.SelectNodes("//div[@class='card-deck']"))
            {
                foreach (var card in cardDeck.SelectNodes("//div[@class='card mb-2 post-card']"))
                {
                    Models.Game game = new Models.Game();

                    game.Name = card.SelectNodes(".//div[@class='card-body']//a//p").LastOrDefault().InnerText;
                    game.ImageUrl = card.SelectNodes(".//a//img[@class='card-img-top']").LastOrDefault().Attributes["src"].Value;
                    game.Fails = parseFails(card.SelectNodes(".//div[@class='card-body']//small[@class='text-muted']").LastOrDefault().InnerText);

                    games.Add(game);
                }
            }

            return games;
        }

        internal static List<Models.Streamer> ParseStreamers(string html)
        {
            List<Models.Streamer> streamers = new List<Models.Streamer>();
            if (html == null || html.Length < 5)
                return streamers;

            var doc = buildDoc(html);
            foreach (var cardDeck in doc.DocumentNode.SelectNodes("//div[@class='card-deck']"))
            {
                foreach (var card in cardDeck.SelectNodes("//div[@class='card mb-2 post-card']"))
                {
                    Models.Streamer streamer = new Models.Streamer();

                    streamer.Name = card.SelectNodes(".//div[@class='card-body']//a//p").LastOrDefault().InnerText;
                    streamer.ImageUrl = card.SelectNodes(".//a//img[@class='card-img-top']").LastOrDefault().Attributes["src"].Value;
                    streamer.Fails = parseFails(card.SelectNodes(".//div[@class='card-body']//small[@class='text-muted']").LastOrDefault().InnerText);

                    streamers.Add(streamer);
                }
            }

            return streamers;
        }

        private static HtmlDocument buildDoc(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.OptionFixNestedTags = true;
            doc.OptionCheckSyntax = false;
            doc.LoadHtml(html);
            if (doc.ParseErrors != null && doc.ParseErrors.Count() > 0
                || doc.DocumentNode == null)
                throw new Exception("Failed to parse document!");

            return doc;
        }
        
        private static int parseFails(string failsStr)
        {
            failsStr = failsStr.Trim();
            return int.Parse(failsStr.Split(' ')[0]);
        }

        private static int parsePoints(string pointsStr)
        {
            pointsStr = pointsStr.Trim();
            return int.Parse(pointsStr.Split(' ')[0]);
        }

        private static TimeSpan parseTime(string timeStr)
        {
            var timeMeanings = new List<KeyValuePair<string, TimeSpan>>()
            {
                new KeyValuePair<string, TimeSpan>("s", TimeSpan.FromSeconds(1)),
                new KeyValuePair<string, TimeSpan>("m", TimeSpan.FromMinutes(1)),
                new KeyValuePair<string, TimeSpan>("h", TimeSpan.FromHours(1)),
                new KeyValuePair<string, TimeSpan>("w", TimeSpan.FromDays(7)),
                new KeyValuePair<string, TimeSpan>("mo", TimeSpan.FromDays(30)),
            };

            timeStr = timeStr.Trim();
                var time = timeStr.Split(' ')[0];

                foreach (var timeMeaning in timeMeanings)
                {
                    if (time.EndsWith(timeMeaning.Key))
                    {
                        var timeAmount = int.Parse(time.TrimEnd(timeMeaning.Key.ToCharArray()));
                        return timeAmount * timeMeaning.Value;
                    }
                }

                throw new Exception($"Unable to parse time for: {timeStr}");
            }
    }
}
