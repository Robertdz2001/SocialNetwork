﻿using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkBackend.Application.Requests.UserRequests.LoginUser;
using SocialNetworkBackend.Application.Requests.UserRequests.PasswordReset;
using SocialNetworkBackend.Application.Requests.UserRequests.RegisterUser;
using SocialNetworkBackend.Application.Requests.UserRequests.VerifyLoginUser;
using SocialNetworkBackend.Application.Requests.UserRequests.VerifyPasswordReset;
using SocialNetworkBackend.Application.Requests.UserRequests.VerifyRegisterUser;

namespace SocialNetworkBackend.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Checks if email is not taken and sends verification token.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("verify-register")]
    public async Task<IActionResult> VerifyRegisterUser(VerifyRegisterUserRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Creates new user.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
    {
        var validator = new RegisterUserRequestValidator();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ErrorMessage);
        }

        await _mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Checks email and passwords then sends verification code.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginUserRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Checks token during logging in.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("verify-login")]
    public async Task<IActionResult> VerifyLoginUser(VerifyLoginUserRequest request)
    {
        var token = await _mediator.Send(request);
        return Ok(token);
    }

    /// <summary>
    /// Checks token during resetting password.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("verify-password-reset")]
    public async Task<IActionResult> VerifyPasswordReset(VerifyPasswordResetRequest request)
    {
        await _mediator.Send(request);
        return Ok();
    }

    /// <summary>
    /// Resets password.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("password-reset")]
    public async Task<IActionResult> PasswordReset(PasswordResetRequest request)
    {
        var validator = new PasswordResetRequestValidator();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors[0].ErrorMessage);
        }

        await _mediator.Send(request);
        return Ok();
    }
}