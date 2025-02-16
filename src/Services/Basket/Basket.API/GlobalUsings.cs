#region NuGet packages
global using Carter;
global using Marten;
global using Mapster;
global using MediatR;
global using Marten.Schema;
global using FluentValidation;
global using HealthChecks.UI.Client;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;

#endregion


#region BuildingBlocks
global using BuildingBlocks.CQRS;
global using BuildingBlocks.Consts;
global using BuildingBlocks.Behaviors;
global using BuildingBlocks.CustomExceptions;
global using BuildingBlocks.CustomExceptions.Handlers;

#endregion


#region Basket.API
global using Basket.API;
global using Basket.API.Data;
global using Basket.API.Models;
global using Basket.API.Basket.StoreBasket;
global using Basket.API.Basket.StoreBasket.Interfaces;

#endregion

#region BasketDiscount
global using BasketDiscount.Models;

#endregion
