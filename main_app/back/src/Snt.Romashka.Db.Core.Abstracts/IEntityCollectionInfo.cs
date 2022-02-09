using System.Collections.Generic;

namespace Snt.Romashka.Db.Core.Abstracts
{
    public interface IEntityCollectionInfo
    {
        int PageCount { get; set; }
        int Count { get; set; }
    }

    public interface IEntityCollectionInfo<TEntity> : IEntityCollectionInfo where TEntity: class
    {
        IEnumerable<TEntity> Entities { get; set; }
    }
}