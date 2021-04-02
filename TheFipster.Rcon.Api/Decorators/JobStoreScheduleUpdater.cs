using System;
using System.Collections.Generic;
using TheFipster.Rcon.Api.Abstractions;
using TheFipster.Rcon.Api.Components;
using TheFipster.Rcon.Api.Repository.Models;

namespace TheFipster.Rcon.Api.Decorators
{
    public class JobStoreScheduleUpdater : ICronJobStore
    {
        private readonly ICronJobStore _component;
        private readonly JobGovernor _jobGovernor;

        public JobStoreScheduleUpdater(ICronJobStore component, JobGovernor jobGovernor)
        {
            _component = component;
            _jobGovernor = jobGovernor;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CronJob> Get()
            => _component.Get();


        public CronJob Get(string id)
            => _component.Get(id);

        public string Insert(CronJob entry)
        {
            throw new NotImplementedException();
        }
    }
}
