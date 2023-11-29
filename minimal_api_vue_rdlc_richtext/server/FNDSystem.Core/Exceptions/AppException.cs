namespace FNDSystem.Core.Exceptions;

[Serializable]
public class ControlledException : Exception
{
    public int ErrorCode = 400;
    public string ErrorMessage = string.Empty;


    public ControlledException()
    {
        ErrorMessage = "an error has occurred";
    }

    public ControlledException(string ErrorMessage, int ErrorCode = 400)
        : base(ErrorMessage)
    {
        this.ErrorMessage = ErrorMessage;
        this.ErrorCode = ErrorCode;
    }
}

public class UnauthorizedException : ControlledException
{
    public UnauthorizedException()
    {
        ErrorCode = 401;
        ErrorMessage = "Authorization Required";
    }

    public UnauthorizedException(string ErrorMessage, int ErrorCode = 401)
        : base(ErrorMessage)
    {
        this.ErrorMessage = ErrorMessage;
        this.ErrorCode = ErrorCode;
    }
}