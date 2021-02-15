using System.Collections.Generic;
using TheFipster.Rcon.Api.Repository.Models;

namespace TheFipster.Rcon.Api.Abstractions
{
    public interface IHistoryStore
    {
        void Insert(History entry);

        IEnumerable<History> Get(int n = 1);
    }
}