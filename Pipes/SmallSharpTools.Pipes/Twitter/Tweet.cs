using System;
using System.Collections.Generic;
using SmallSharpTools.Pipes.Extensions;

namespace SmallSharpTools.Pipes.Twitter
{

    public class Tweet
    {

        public string ScreenName { get; set; }
        public string ProfileImageUrl { get; set; }
        public long TweetID { get; set; }
        public String TweetText { get; set; }
        public String LocationText { get; set; }
        public DateTime TweetCreated { get; set; }

        public String TweetCreatedDisplay
        {
            get
            {
                if (TweetCreated > DateTime.Now.AddMinutes(-3))
                {
                    return "a moment ago";
                }
                else if (TweetCreated > DateTime.Now.AddHours(-1))
                {
                    var diff = TweetCreated - DateTime.Now;
                    return String.Format("{0} minutes ago", diff.Minutes * -1);
                }
                else if (TweetCreated > DateTime.Now.AddDays(-1))
                {
                    var diff = TweetCreated - DateTime.Now;
                    if (diff.Hours == -1)
                    {
                        return "about an hour";
                    }
                    return String.Format("about {0} hours ago", diff.Hours * -1);
                }

                return TweetCreated.ToLongDateString();
            }
        }

        public bool IsFriendly()
        {
            // the competition
            if (TweetText.Contains("onmilwaukee", StringComparison.OrdinalIgnoreCase) ||
                TweetText.Contains("shepherd express", StringComparison.OrdinalIgnoreCase) ||
                TweetText.Contains("expressmilwaukee.com", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // unwanted language
            if (TweetText.Contains("fuck", StringComparison.OrdinalIgnoreCase) ||
                TweetText.Contains("shit", StringComparison.OrdinalIgnoreCase) ||
                TweetText.Contains("cunt", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (ScreenName.Contains("onmilwaukee", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return TweetID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Tweet;
            return (other != null && other.TweetID == TweetID);
        }

        public override string ToString()
        {
            return TweetText;
        }

    }

    public class TweetCollection : List<Tweet>
    {

        public TweetCollection()
        {
        }

        public TweetCollection(IEnumerable<Tweet> tweets)
        {
            AddRange(tweets);
        }

        public new void AddRange(IEnumerable<Tweet> tweets)
        {
            foreach (var t in tweets)
            {
                if (!Contains(t.TweetID))
                {
                    Add(t);
                }
            }
        }

        public bool Contains(long tweetId)
        {
            return Exists(t => t.TweetID.Equals(tweetId));
        }

    }

}
