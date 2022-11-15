﻿using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityServer.Client.Dtos;

namespace Volo.Abp.IdentityServer.Client
{
    public interface IClientAppService :
        ICrudAppService<ClientWithDetailsDto, Guid, GetClientListInput, CreateClientDto, UpdateClientDto>
    {
    }
}
