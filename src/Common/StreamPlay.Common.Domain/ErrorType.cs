namespace StreamPlay.Common.Domain;

public enum ErrorType
{
    Failure = 500,
    Validation = 400,
    Problem = 2,
    NotFound = 404,
    Conflict = 409
}
