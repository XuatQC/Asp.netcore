
namespace Core.Exceptions;

[Serializable]
class ControlledException : Exception
{
    public int ErrorCode = 400;
    public string ErrorMessage = string.Empty;


    public ControlledException()
    {
        this.ErrorMessage = "Có lỗi xảy ra";
    }

    public ControlledException(string ErrorMessage, int ErrorCode = 400)
        : base(ErrorMessage)
    {
        this.ErrorMessage = ErrorMessage;
        this.ErrorCode = ErrorCode;
    }
}

class UnauthorizedException : ControlledException
{
    public UnauthorizedException()
    {
        this.ErrorCode = 401;
        this.ErrorMessage = "Truy cập không hợp lệ";
    }

    public UnauthorizedException(string ErrorMessage, int ErrorCode = 401)
        : base(ErrorMessage)
    {
        this.ErrorMessage = ErrorMessage;
        this.ErrorCode = ErrorCode;
    }
}