using System;
using System.Collections.Generic;
using System.Text;

namespace PharmaRnd.Domain.Shared
{
    public interface ISoftDeletable
    {
        void Delete();
        void Restore();
    }
}
