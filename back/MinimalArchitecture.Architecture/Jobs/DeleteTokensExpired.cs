using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Architecture.Jobs.Common;
using MinimalArchitecture.Common.Extensions;
using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Jobs
{
    [JobConfiguration(cronSchedule: TimePeriods.EveryMinute)]
    public class DeleteTokensExpired : IJob
    {
        private readonly IRepositoryBase<Token> repository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteTokensExpired(ITokenService tokenService, 
                                   IRepositoryBase<Token> repository,
                                   IUnitOfWork unitOfWork)
        {
            TokenService = tokenService;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public ITokenService TokenService { get; }

        public async Task Execute(IJobExecutionContext context)
        {
            var tokens = await repository.GetAsync(w => !w.Active || w.ExpirationTime < DateTime.Now);

            if (tokens.HasElements())
            {
                repository.Delete(tokens.ToList());

                await unitOfWork.Save();
                
            }

            
        }
    }
}
