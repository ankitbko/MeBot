using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Luis;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Luis.Models;

namespace MeBot.Dialogs
{
    [LuisModel("modelid", "subkey")]
    [Serializable]
    public class MeBotLuisDialog : LuisDialog<object>
    {
        public MeBotLuisDialog(params ILuisService[] services) : base(services)
        {
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry. I didn't understand you.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("AboutMe")]
        public async Task AboutMe(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(@"Ankit is a Software Engineer currently working in Microsoft Center of Excellence team at Mindtree. 
                He started his professional career in 2013 after completing his graduation as Bachelor in Computer Science.");
            await context.PostAsync(@"He is a technology enthusiast and loves to dig in emerging technologies. 
                Most of his working hours are spent on creating architecture, evaluating upcoming products and developing frameworks.");
            context.Wait(MessageReceived);
        }

    }
}