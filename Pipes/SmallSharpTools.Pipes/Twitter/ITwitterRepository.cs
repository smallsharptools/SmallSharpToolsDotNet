using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmallSharpTools.Pipes.Model;

namespace SmallSharpTools.Pipes.Twitter
{

    public interface ITwitterRepository : IRepository<Tweet>
    {

        TweetCollection FetchByScreenName(String screenName, int max);

        TweetCollection SearchByLocation(String keyword, int max, int kilometers, double latitude, double longitude);

    }

}
