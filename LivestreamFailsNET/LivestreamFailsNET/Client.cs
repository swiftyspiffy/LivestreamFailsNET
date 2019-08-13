using LivestreamFailsNET.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivestreamFailsNET
{
    public class Client
    {
        private const string LIVESTREAMFAILS_BASE_URL = "https://livestreamfails.com";

        private const string QUERY_MODE = "loadPostMode";
        private const string QUERY_PAGE = "loadPostPage";
        private const string QUERY_TIMEFRAME = "loadPostTimeFrame";
        private const string QUERY_ORDER = "loadPostOrder";
        private const string QUERY_NSFW = "loadPostNSFW";
        private const string QUERY_MODE_STREAMER = "loadPostModeStreamer";
        private const string QUERY_MODE_GAME = "loadPostModeGame";

        private const string QUERY_GAME_ORDER = "loadGameOrder";
        private const string QUERY_GAME_PAGE = "loadGamePage";

        private const string QUERY_STREAMER_ORDER = "loadStreamerOrder";
        private const string QUERY_STREAMER_PAGE = "loadStreamerPage";

        public Client() { }

        public List<Models.Fail> GetFails(int page = 0, Timeframe timeframe = Timeframe.TODAY, Order order = Order.HOT, bool nsfw = true, string game = null, string streamer = null)
        {
            return GetFailsAsync(page, timeframe, order, nsfw, game, streamer).GetAwaiter().GetResult();
        }

        public async Task<List<Models.Fail>> GetFailsAsync(int page = 0, Timeframe timeframe = Timeframe.TODAY, Order order = Order.HOT, bool nsfw = true, string game = null, string streamer = null)
        {
            var endpoint = $"{LIVESTREAMFAILS_BASE_URL}/load/loadPosts.php";

            var mode = getPostMode(game, streamer);
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(QUERY_MODE, mode.ToDescriptionString()),
                new KeyValuePair<string, string>(QUERY_PAGE, page.ToString()),
                new KeyValuePair<string, string>(QUERY_TIMEFRAME, timeframe.ToDescriptionString()),
                new KeyValuePair<string, string>(QUERY_ORDER, order.ToDescriptionString()),
                new KeyValuePair<string, string>(QUERY_NSFW, nsfw ? "1" : "0")
            };
            switch(mode)
            {
                case Mode.STREAMER:
                    parameters.Add(new KeyValuePair<string, string>(QUERY_MODE_STREAMER, streamer));
                    break;
                case Mode.GAME:
                    parameters.Add(new KeyValuePair<string, string>(QUERY_MODE_GAME, game));
                    break;
                case Mode.STANDARD:
                    // do nothing
                    break;
                default:
                    throw new Exception($"Unknown mode: {mode.ToString()} ({mode.ToDescriptionString()})");
            }

            var response = await Helpers.Http.GetRequestAsync(endpoint, parameters);

            return Helpers.Parsing.ParseFails(response);
        }

        public List<Models.Game> GetGames(int page = 0)
        {
            return GetGamesAsync(page).GetAwaiter().GetResult();
        }

        public async Task<List<Models.Game>> GetGamesAsync(int page = 0)
        {
            var endpoint = $"{LIVESTREAMFAILS_BASE_URL}/load/loadGames.php";

            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(QUERY_GAME_ORDER, "amount"),
                new KeyValuePair<string, string>(QUERY_GAME_PAGE, page.ToString())
            };

            var response = await Helpers.Http.GetRequestAsync(endpoint, parameters);

            return Helpers.Parsing.ParseGames(response);
        }

        public List<Models.Streamer> GetStreamers(int page = 0)
        {
            return GetStreamersAsync(page).GetAwaiter().GetResult();
        }

        public async Task<List<Models.Streamer>> GetStreamersAsync(int page = 0)
        {
            var endpoint = $"{LIVESTREAMFAILS_BASE_URL}/load/loadStreamers.php";

            var parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(QUERY_STREAMER_ORDER, "amount"),
                new KeyValuePair<string, string>(QUERY_STREAMER_PAGE, page.ToString())
            };

            var response = await Helpers.Http.GetRequestAsync(endpoint, parameters);

            return Helpers.Parsing.ParseStreamers(response);
        }

        private Mode getPostMode(string game, string streamer)
        {
            if (!string.IsNullOrWhiteSpace(streamer))
                return Mode.STREAMER;
            if (!string.IsNullOrWhiteSpace(game))
                return Mode.GAME;
            return Mode.STANDARD;
        }
    }
}
