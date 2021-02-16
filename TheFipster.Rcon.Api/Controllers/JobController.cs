using Microsoft.AspNetCore.Mvc;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Rcon.Api.Abstractions;
using TheFipster.Rcon.Api.Models;
using TheFipster.Rcon.Api.Repository.Models;

namespace TheFipster.Rcon.Api.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly ICronJobStore _cronJobStore;

        public JobController(ICronJobStore cronJobStore)
            => _cronJobStore = cronJobStore;

        [HttpGet]
        public IEnumerable<CronJob> GetAll()
            => _cronJobStore.Get();


        [HttpGet("{id}")]
        public CronJob GetById(string id)
            => _cronJobStore.Get(id);

        [HttpPost]
        public IActionResult PostNewJob([FromBody] NewJobRequest request)
        {
            if (!CronExpression.IsValidExpression(request.CronExpression))
                throw new ArgumentException("Cron Expression is not valid.", nameof(request.CronExpression));

            if (request.Commands.Count == 0 || request.Commands.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentException("Commands are empty or consist only out of whitespace.");

            var job = new CronJob(request.CronExpression, request.Commands);
            var jobId = _cronJobStore.Insert(job);

            var url = Url.Action("GetById", new { id = jobId });
            return Created(url, job);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJob(string id)
        {
            _cronJobStore.Delete(id);
            return NoContent();
        }
    }
}
