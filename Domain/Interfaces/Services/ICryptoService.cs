using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Models;

namespace Api.Rifamos.BackEnd.Domain.Interfaces.Services
{
    public interface ICryptoService
    {
        public List<string> IEncrypt (string sValorEncriptar);
        public string IDecrypt (List<string> oListToken);

    }
}