using HtmlAgilityPack;
using MeBot.Entities;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeBot.Internal
{
    public class BlogSearch
    {
        const string BLOG_URI = "http://ankitbko.github.io/archive";

        public List<Post> GetPostsWithTag(string tag)
        {
            List<Post> posts = GetAllPosts();
            return posts.FindAll(p => IsMatch(tag, p.Tags));
        }

        public List<Post> GetAllPosts()
        {
            var blogHtml = GetHtmlFromBlog();
            return blogHtml.DocumentNode.SelectSingleNode("//ul").ChildNodes.Where(t => t.Name == "li")     // Select all posts
                .Select((f, n) =>
                    new Post()
                    {
                        Name = f.SelectSingleNode("a").InnerText,                                           // Select post name
                        Tags = f.SelectSingleNode("ul").ChildNodes.Where(t => t.Name == "li")               // Select Tags
                                .Select(t => t.InnerText).ToList(),                                         // Select tag name
                        Url = f.SelectSingleNode("a[@href]").GetAttributeValue("href", string.Empty) //Select URL
                    }).ToList();                                      
                        
        }

        private HtmlDocument GetHtmlFromBlog()
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(BLOG_URI);
        }

        private bool IsMatch(string tag, List<string> tags)
        {
            var terms = tags.SelectMany(t => Language.GenerateTerms(Language.CamelCase(t), 3));
            foreach (var t in terms)
            {
                if (Regex.IsMatch(tag, t))
                    return true;
            }
            return false;
        }
    }
}