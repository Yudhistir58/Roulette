﻿using Microsoft.Extensions.DependencyInjection;
using Roulette.Repositories;

namespace Roulette
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBetRepository, BetRepository>();
            return services;
        }
    }
}