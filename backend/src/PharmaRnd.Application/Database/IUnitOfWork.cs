using System;
using System.Data;

namespace PharmaRnd.Application.Database;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(
        CancellationToken cancellationToken = default);

    Task SaveChanges(
        CancellationToken cancellationToken = default);
}
