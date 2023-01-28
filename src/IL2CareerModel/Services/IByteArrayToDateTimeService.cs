namespace IL2CareerModel.Services;
public interface IByteArrayToDateTimeService
{
    DateTime? GetDateTime(byte[] bytes);
}
