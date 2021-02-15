using System.Collections.Generic;
using TheFipster.Rcon.Api.Repository.Models;

namespace TheFipster.Rcon.Api.Abstractions
{
    public interface ICronJobStore
    {
        IEnumerable<CronJob> Get();
        CronJob Get(string id);
        string Insert(CronJob entry);
        void Delete(string id);
    }
}