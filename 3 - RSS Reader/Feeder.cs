using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace rss_demo
{
    public class Feeder
    { 
        //public class FeedCollection : ObservableCollection<FeedClass>
        //{ }

        public static ObservableCollection<FeedClass> FeedItems;

        public static async Task GetFeedAsync(string feedUrl)
        {
            ObservableCollection<FeedClass> collection = new ObservableCollection<FeedClass>();
            Windows.Web.Syndication.SyndicationClient client = new SyndicationClient();
            Uri feedUri = new Uri(feedUrl);

            try
            {
                SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri);

                if (feed.Items != null && feed.Items.Count > 0)
                {
                    foreach (SyndicationItem item in feed.Items)
                    {
                        FeedClass feedItem = new FeedClass();

                        if (item.Title != null && item.Title.Text != null)
                        {
                            feedItem.Title = item.Title.Text;
                        }
                        if (item.PublishedDate != null)
                        {
                            feedItem.PubDate = item.PublishedDate.DateTime;
                        }
                        if (item.Authors != null && item.Authors.Count > 0)
                        {
                            feedItem.Author = item.Authors[0].Name.ToString();
                        }

                        // Handle the differences between RSS and Atom feeds.
                        if (feed.SourceFormat == SyndicationFormat.Atom10)
                        {
                            if (item.Content != null && item.Content.Text != null)
                            {
                                feedItem.Content = item.Content.Text;
                            }
                            if (item.Id != null)
                            {
                                feedItem.Link = new Uri(item.Id);
                            }
                        }
                        else if (feed.SourceFormat == SyndicationFormat.Rss20)
                        {
                            if (item.Summary != null && item.Summary.Text != null)
                            {
                                feedItem.Content = item.Summary.Text;
                            }
                            if (item.Links != null && item.Links.Count > 0)
                            {
                                feedItem.Link = item.Links[0].Uri;
                            }
                        }

                        collection.Add(feedItem);
                    }
                    FeedItems = collection;
                }
            }
            catch (Exception)
            {
                //message
            }            
        }
    }

    public class FeedClass
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subtitle { get; set; }
        public DateTime PubDate { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public Uri Link { get; set; }
        public string Image { get; set; }
    }
}
