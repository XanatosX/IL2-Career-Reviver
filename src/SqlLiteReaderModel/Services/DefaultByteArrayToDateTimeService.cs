using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CarrerReviverModel.Services;
internal sealed class DefaultByteArrayToDateTimeService : IByteArrayToDateTimeService
{
    public DateTime? GetDateTime(byte[] bytes)
    {
        DateTime? returnTime = null;
        if (bytes.Length == 0)
        {
            return returnTime;
        }
        string translated = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
        try
        {
            returnTime = DateTime.Parse(translated);
        }
        catch (Exception)
        {
            //Parsing gone wrong
        }
        return returnTime;
    }
}
