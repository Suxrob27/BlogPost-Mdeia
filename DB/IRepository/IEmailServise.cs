using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.IRepository
{
    public interface IEmailServise
    {
        Task Send(string form, string to, string subject, string body);
    }
}
