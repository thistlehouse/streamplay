using System.Net;
using StreamPlay.Module.Users.Application.Authentication.Login;
using StreamPlay.Module.Users.Domain;
using StreamPlay.Module.Users.Tests.Abstractions;
using StreamPlay.Module.Users.Tests.TestUtils.Persistence;

namespace StreamPlay.Module.Users.Tests.Application.Authentication.Login;

public class LoginUserQueryHandlerTests : BaseTest
{
    private readonly LoginUserQueryHandler _handler;
    private readonly InMemoryUserRepository _repository;
    private string _username = string.Empty;
    private string _email = string.Empty;
    private string _password = string.Empty;

    public LoginUserQueryHandlerTests()
    {
        _repository = new InMemoryUserRepository();
        _handler = new LoginUserQueryHandler(_repository);
    }

    [Fact]
    public async Task LoginUserUseCase_Should_LogUserIn()
    {
        _username = Faker.Name.FirstName();
        _email = Faker.Internet.Email();
        _password = Faker.Lorem.Word();

        var user = User.Create(_username, _email, _password);

        await _repository.AddAsync(user);

        var result = await _handler.Handle(new LoginUserQuery(_email, _password), default);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.False(string.IsNullOrEmpty(result.Value.Token));
        Assert.NotNull(result.Value.User);
        Assert.Equal(user, result.Value.User);
    }

    [Fact]
    public async Task LoginUserUseCase_ShouldNot_LogUserIn_When_UserDoesNotExist()
    {
        _username = Faker.Name.FirstName();
        _email = Faker.Internet.Email();

        var result = await _handler.Handle(new LoginUserQuery(_email, _password), default);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal((int)HttpStatusCode.NotFound, (int)result.Error.Type);
        Assert.Equal("User.NotFound", result.Error.Code);
    }

    [Fact]
    public async Task LoginUserUseCase_ShouldNot_LogUserIn_When_InvalidCredentials()
    {
        _username = Faker.Name.FirstName();
        _email = Faker.Internet.Email();
        _password = Faker.Lorem.Word();

        await _repository.AddAsync(User.Create(_username, _email, _password));

        var result = await _handler.Handle(new LoginUserQuery(_email, "password"), default);

        Assert.NotNull(result);
        Assert.False(result!.IsSuccess);
        Assert.Equal((int)HttpStatusCode.BadRequest, (int)result.Error.Type);
        Assert.Equal("Auth.InvalidCredentials", result.Error.Code);
    }
}