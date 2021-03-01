using System;
using System.Collections.Generic;
using System.Linq;

namespace S6A0702.VO
{
    public sealed class PagedCollectionVO<TVO, TEntity>
        where TVO : IVO<TEntity>, new()
    {
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalSize { get; }

        public TVO[] Items
        {
            get;
            private set;
        }

        public PagedCollectionVO(IEnumerable<TEntity> entities, int currentPage, int pageSize)
        {
            TotalSize = entities.Count();

            PageSize = pageSize;

            if (currentPage <= 0)
                currentPage = 1;
            else if (currentPage > TotalSize)
                currentPage = TotalSize;
            CurrentPage = currentPage;

            Items = GetPageItens(entities ?? Array.Empty<TEntity>()).ToArray();
        }

        private IEnumerable<TVO> GetPageItens(IEnumerable<TEntity> entities)
        {
            var _index = (CurrentPage - 1) * PageSize;
            foreach (var entity in entities.Skip(_index).Take(PageSize))
                yield return entity.CreateVO<TEntity, TVO>();
        }
    }
}
