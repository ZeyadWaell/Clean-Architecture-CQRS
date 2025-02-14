using ChatApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.ExternalServices
{
    public class GeminiBotStrategy : IBotStrategy
    {
        public async Task<string> ProcessMessageAsync(string message)
        {
            // Simulate an external API call to Gemini.
            await Task.Delay(100);
            return $"[Gemini] Processed message: {message}";
        }
    }
}
