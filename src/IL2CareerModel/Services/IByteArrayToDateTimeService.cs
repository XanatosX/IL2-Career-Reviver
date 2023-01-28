using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL2CareerModel.Services;
public interface IByteArrayToDateTimeService
{
    DateTime? GetDateTime(byte[] bytes);
}
