﻿using MediatR;
using System.ComponentModel;
using System.Threading.Tasks;

namespace IdentityForm.Api.Extensions;

public class MediatorHangfireBridge
{
    private readonly IMediator _mediator;

    public MediatorHangfireBridge(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Send(IRequest command)
    {
        await _mediator.Send(command);
    }

    [DisplayName("{0}")]
    public async Task Send(string jobName, IRequest command)
    {
        await _mediator.Send(command);
    }
}