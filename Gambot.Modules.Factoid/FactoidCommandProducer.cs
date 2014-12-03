﻿using System;
using System.Text.RegularExpressions;
using Gambot.Core;
using Gambot.Data;

namespace Gambot.Modules.Factoid
{
    internal class FactoidCommandProducer : IMessageProducer
    {
        private IDataStore dataStore;

        public void Initialize(IDataStoreManager dataStoreManager)
        {
            dataStore = dataStoreManager.Get("Factoids");
        }

        public ProducerResponse Process(IMessage message, bool addressed)
        {
            if (addressed)
            {
                var match = Regex.Match(message.Text, @"(.+) (<[^>]+>) (.+)",
                                        RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    var term = match.Groups[1].Value;
                    var verb = match.Groups[2].Value;
                    var response = match.Groups[3].Value;

                    if (verb == "alias" && term == response)
                    {
                        return
                            new ProducerResponse(
                                String.Format(
                                    "Sorry {0}, but you can't alias {1} to itself.",
                                    message.Who, term), false);
                    }

                    return new ProducerResponse(String.Format(dataStore.Put(term, verb + " " + response) ? "Okay, {0}." : "{0}: I already had it that way!", message.Who), false);
                }
            }

            return null;
        }
    }
}
