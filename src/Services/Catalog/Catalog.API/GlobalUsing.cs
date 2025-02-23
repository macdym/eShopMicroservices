#region NuGet packages
global using MediatR;
global using Mapster;
global using Carter;
global using Marten;
global using FluentValidation;
global using Marten.Schema;
global using HealthChecks.UI.Client;

#endregion

#region Microsoft
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;

#endregion

#region BuildingBlocks
global using BuildingBlocks.CQRS;
global using BuildingBlocks.Consts;
global using BuildingBlocks.Behaviors;
global using BuildingBlocks.CustomExceptions;
global using BuildingBlocks.CustomExceptions.Handlers;

#endregion


#region Catalog.API
global using Catalog.API;
global using Catalog.API.Data;
global using Catalog.API.Models;

#endregion