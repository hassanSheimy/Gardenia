using Gardenia.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardenia.Interfaces
{
    public interface ISmsService
    {
        Task<string> SendSmsAsync(SmsRequest smsRequest);
    }
}
