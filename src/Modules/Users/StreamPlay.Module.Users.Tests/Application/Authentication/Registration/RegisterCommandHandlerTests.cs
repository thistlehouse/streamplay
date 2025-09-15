using System.Net;
using NSubstitute;
using StreamPlay.Common.Domain;
using StreamPlay.Module.Users.Application.Abstractions.Identity;
using StreamPlay.Module.Users.Application.Authentication.Registration;
using StreamPlay.Module.Users.Domain;
using StreamPlay.Module.Users.Infrastructure.Identity;
using StreamPlay.Module.Users.Tests.Abstractions;
using StreamPlay.Module.Users.Tests.TestUtils.Persistence;

namespace StreamPlay.Module.Users.Tests.Application.Authentication.Registration;

public class RegisterCommandHandlerTests : BaseTest
{
    private readonly InMemoryUserRepository _repository;
    private readonly RegisterUserCommandHandler _handler;
    private readonly IIdentityProviderService _identityProviderService;
    private string _username = string.Empty;
    private string _email = string.Empty;
    private string _password = string.Empty;

    public RegisterCommandHandlerTests()
    {
        _repository = new InMemoryUserRepository();
        _identityProviderService = Substitute.For<IIdentityProviderService>();
        _handler = new RegisterUserCommandHandler(_identityProviderService, _repository);
    }

    [Fact]
    public async Task RegisterUserUseCase_Should_Register_NewUser()
    {
        _username = Faker.Name.FirstName();
        _email = Faker.Internet.Email();
        _password = Faker.Lorem.Word();

        _identityProviderService.RegisterUserAsync(
                Arg.Any<UserModel>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Success("12345")));

        var result = await _handler.Handle(new RegisterUserCommand(_username, _email, _password), default);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.False(string.IsNullOrEmpty(result.Value.Token));
        Assert.Equal(result.Value.User.Username, _username);
        Assert.Equal(result.Value.User.Email, _email);
        Assert.Equal(result.Value.User.Password, _password);
    }

    [Fact]
    public async Task RegisterUserUseCase_Should_Return_NullUser_When_EmailAlreadyExists()
    {
        _username = Faker.Name.FirstName();
        _email = Faker.Internet.Email();
        _password = Faker.Lorem.Word();

        var user = User.Create(_username, _email, _password);

        _identityProviderService.RegisterUserAsync(
                Arg.Any<UserModel>(),
                Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(Result.Failure<string>(Error.Conflict(
                "Email.NotUnique",
                "Email is not unique"))));

        var result = await _handler.Handle(new RegisterUserCommand(_username, _email, _password), default);

        Assert.NotNull(result);
        Assert.NotNull(result.Error);
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal((int)HttpStatusCode.Conflict, (int)result.Error.Type);
        Assert.Equal("Email.NotUnique", result.Error.Code);
    }
}