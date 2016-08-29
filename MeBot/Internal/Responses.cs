using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeBot
{
    internal static class Responses
    {
        public const string Features =
            "* Answer questions about the author of this blog: Try 'Who is Ankit'\n\n"
            + "* Search this blog for posts on a particular topic: Try 'What are the artciles on chat bot'\n\n";

        public const string WelcomeMessage =
            "Hi there\n\n"
            + "I am MeBot. Designed to answer questions about this blog.  \n"
            + "Currently I have following features  \n"
            + Features
            + "I will get more intelligent in future. You can type 'Help' to get this information again";

        public const string HelpMessage =
            "I can do following   \n"
            + Features
            + "What would you like me to do?";
    }
}
