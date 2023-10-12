﻿using FlightPlanner.Core.Models;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlightPlanner
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options) : 
            base(options)
        {
                
        }

        public DbSet<Flights> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
