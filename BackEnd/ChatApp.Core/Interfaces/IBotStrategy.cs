using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Interfaces
{
    public interface IBotStrategy
    {
        Task<string> ProcessMessageAsync(string message);
    }
}
