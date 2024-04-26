using System;
using System.Transactions;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api.Rifamos.BackEnd.Domain.Persistence.Repositories
{

    public class VentaRepository(RifamosContext context) : RepositoryBase<Ventum>(context), IVentaRepository
    {
    }
}