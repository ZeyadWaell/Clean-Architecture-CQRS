using ChatApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Application.Strategies.Bot
{
    public interface IBotStrategyFactory
    {
        IBotStrategy GetStrategy(string botType);
    }
}
