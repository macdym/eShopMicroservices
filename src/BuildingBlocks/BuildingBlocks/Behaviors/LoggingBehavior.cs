﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "[START] Handle Request={Request} - Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var response = await next();
            stopwatch.Stop();

            var timeTaken = stopwatch.Elapsed;
            
            if(timeTaken.Seconds > 3)
            {
                logger.LogWarning(
                    "[PERFORMANCE] The Request {Request} took {TimeTaken}seconds.",
                    typeof(TRequest).Name, timeTaken.Seconds);
            }

            logger.LogInformation(
                "[END] Handled Request={Request} with Response={Response}",
                typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
