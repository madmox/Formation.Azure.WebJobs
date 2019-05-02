using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace Formation.Webjobs.ContinuousHelloWorld
{
    public class AppSettingsSubscriptionNameResolver : INameResolver
    {
        private SubscriptionNamesSettings _settings;

        public AppSettingsSubscriptionNameResolver(IOptions<SubscriptionNamesSettings> options)
        {
            this._settings = options.Value;
        }

        string INameResolver.Resolve(string name)
        {
            if (!this._settings.ContainsKey(name))
            {
                throw new ArgumentException($"Subscription name {name} is not set in the app settings");
            }

            string subscriptionName = this._settings[name];
            if (string.IsNullOrEmpty(subscriptionName))
            {
                throw new ArgumentException($"Subscription name {name} is null or empty");
            }

            return subscriptionName;
        }
    }

    public class SubscriptionNamesSettings : Dictionary<string, string>
    {
    }
}
