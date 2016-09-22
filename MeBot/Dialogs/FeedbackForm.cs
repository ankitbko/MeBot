using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeBot.Dialogs
{
    public class FeedbackForm
    {
        [Describe("Enter your name")]
        [Prompt(new string[] { "What is your name?", "What should I call you?" })]
        public string Name { get; set; }

        [Describe("Enter your email or twitter handle")]
        [Prompt("How can Ankit contact you? Enter your email id or twitter handle (@something)")]
        public string Contact { get; set; }

        [Describe("Enter your feedback")]
        [Prompt("What's your feedback?")]
        public string Feedback { get; set; }

        public static IForm<FeedbackForm> BuildForm()
        {
            return new FormBuilder<FeedbackForm>()
                .Field(nameof(Contact), validate: ValidateContactInformation)
                .Field(nameof(Feedback), active: FeedbackEnabled)
                .AddRemainingFields()
                .Build();
        }

        private static bool FeedbackEnabled(FeedbackForm state) => 
            !string.IsNullOrWhiteSpace(state.Contact) && !string.IsNullOrWhiteSpace(state.Name);

        private static Task<ValidateResult> ValidateContactInformation(FeedbackForm state, object response)
        {
            var result = new ValidateResult();
            string contactInfo = string.Empty;
            if(GetTwitterHandle((string)response, out contactInfo) || GetEmailAddress((string)response, out contactInfo))
            {
                result.IsValid = true;
                result.Value = contactInfo;
            }
            else
            {
                result.IsValid = false;
                result.Feedback = "You did not enter valid email address or twitter handle. Make sure twitter handle starts with @.";
            }
            return Task.FromResult(result);
        }

        private static bool GetEmailAddress(string response, out string contactInfo)
        {
            contactInfo = string.Empty;
            var match = Regex.Match(response, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            if (match.Success)
            {
                contactInfo = match.Value;
                return true;
            }
            return false;
        }

        private static bool GetTwitterHandle(string response, out string contactInfo)
        {
            contactInfo = string.Empty;
            if (!response.StartsWith("@"))
                return false;
            contactInfo = response.Remove(0, 1);
            return true;
        }
    }
}
