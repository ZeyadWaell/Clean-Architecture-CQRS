using System;
using System.Collections.Generic;
using System.Linq;
using ChatApp.Application.Strategies.Bot;
using ChatApp.Core.Interfaces;

namespace ChatApp.Application.BotStrategies
{
    public class BotStrategyFactory : IBotStrategyFactory
    {
        private readonly IEnumerable<IBotStrategy> _strategies;

        public BotStrategyFactory(IEnumerable<IBotStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IBotStrategy GetStrategy(string botType)
        {
            if (string.IsNullOrWhiteSpace(botType))
                throw new ArgumentException("Bot type must be provided.", nameof(botType));

            botType = botType.ToLowerInvariant();

            IBotStrategy strategy = null;

            if (botType.Contains("gemini"))
            {
                strategy =  _strategies.FirstOrDefault(s => s.GetType().Name.ToLowerInvariant().Contains("gemini"));
            }
            else if (botType.Contains("chatgpt"))
            {
                strategy = _strategies.FirstOrDefault(s => s.GetType().Name.ToLowerInvariant().Contains("chatgpt"));
            }

            if (strategy == null)
                throw new ArgumentException($"No bot strategy found for type: {botType}", nameof(botType));

            return strategy;
        }
    }
}
