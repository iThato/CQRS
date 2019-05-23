using System;
using System.Linq;
using System.Threading.Tasks;
using Dvt.Common.Extensions;
using Dvt.Features.Core.Entities;
using Dvt.Infrastructure.Structures;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dvt.Features.Core.Hangfire
{
    public class JobHandler
    {
        // this class deals with Exceptions. that could results from hangfire dashboard.
        private readonly DvtDatabaseContext _dbContext;
        private readonly IMediator _mediator; // used to avoid JobHandler constructor from exploision. Used to send messeages.
        public JobHandler(DvtDatabaseContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public void Queue(HandlerRequestBase message)
        {
            var jobRequest = new JobRequest
            {
                Id = Guid.NewGuid(),
                Body = JsonConvert.SerializeObject(message, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                }),
                Type = "BackgroundJob",
              
            };

            _dbContext.JobRequest.Add(jobRequest);
            _dbContext.SaveChanges();
            BackgroundJob.Enqueue<JobHandler>(m => m.Excute(jobRequest.Id));
        }

        public void Schedule(HandlerRequestBase message,Guid jobId, string cron)
        {
            var job = _dbContext.JobRequest.FirstOrDefault(f => f.Id == jobId);
            if (job.IsNull())
            {
                job = new JobRequest
                {
                    Id = jobId,
                    Body = JsonConvert.SerializeObject(message, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }),
                    Type = "RecurringJob",
                    Cron = cron
                };
                _dbContext.JobRequest.Add(job);
            }
            else
            {
                job.Body = JsonConvert.SerializeObject(message, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });
                job.Cron = cron;
            }
            
            _dbContext.SaveChanges();
            RecurringJob.AddOrUpdate(job.Id.ToString(), () => Excute(job.Id),cron);
        }

        public void RemoveScheduleJob(Guid jobId)
        {
            var job = _dbContext.JobRequest.FirstOrDefault(f => f.Id == jobId);
            if (!job.IsNull())
            {
                job.Deleted = true;
                _dbContext.SaveChanges();
            }
            RecurringJob.RemoveIfExists(jobId.ToString());
        }

        public async Task<OperationResult> Excute(Guid id)
        {
            var job = await _dbContext.JobRequest.SingleAsync(f => f.Id == id);
            var request = JsonConvert.DeserializeObject<dynamic>(job.Body, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return await _mediator.Send(request);
        }
    }
}
