using System;
using System.Collections.Generic;
using SmallSharpTools.Pipes.Model;

namespace SmallSharpTools.Pipes.Twitter
{
    
    public class TwitterRepository : ITwitterRepository
    {
        #region ITwitterRepository Members

        public TweetCollection FetchByScreenName(string screenName, int max)
        {
            Logger.Log("FetchByScreenName: " + screenName, System.Diagnostics.TraceEventType.Information);
            var collection = new TweetCollection();
            collection.Add(new Tweet()
            {
                TweetID = 1,
                LocationText = "Milwaukee, WI",
                ProfileImageUrl = String.Empty,
                ScreenName = screenName,
                TweetText = "First tweet!",
                TweetCreated = DateTime.Now.AddMinutes(-10)
            });
            collection.Add(new Tweet()
            {
                TweetID = 1,
                LocationText = "Milwaukee, WI",
                ProfileImageUrl = String.Empty,
                ScreenName = screenName,
                TweetText = "Second tweet!",
                TweetCreated = DateTime.Now.AddMinutes(-5)
            });
            return collection;
        }

        public TweetCollection SearchByLocation(string keyword, int max, int kilometers, double latitude, double longitude)
        {
            var collection = new TweetCollection();
            return collection;
        }

        #endregion

        #region IRepository<Tweet> Members

        public void Save(Tweet instance)
        {
            throw new NotImplementedException();
        }

        public void SaveAll(IEnumerable<Tweet> collection)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "  Logging  "
        
        public ILogger Logger { get; set; }

        #endregion

    }

}
