using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using SmallSharpTools.Pipes.Twitter;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            using (IUnityContainer container = new UnityContainer())
            {
                UnityConfigurationSection config
                    = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                if (config != null)
                {
                    config.Containers.Default.Configure(container);
                }

                var tr = container.Resolve<ITwitterRepository>();

                var tweets = tr.FetchByScreenName("smallsharptools", 20);
                foreach (var tweet in tweets)
                {
                    Console.WriteLine(String.Format("Tweet: {0} - {1}", tweet.ScreenName, tweet.TweetText));
                }
            }

        }
    }
}
