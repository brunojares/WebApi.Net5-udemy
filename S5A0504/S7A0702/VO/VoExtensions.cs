using System.Collections.Generic;

namespace S6A0702.VO
{
    public static class VoExtensions
    {
        public static TEntity CreateEntity<TEntity>(this IVO<TEntity> vo)
            where TEntity : new()
        {
            var _result = new TEntity();
            vo.CopyTo(ref _result);
            return _result;
        }
        public static TVO CreateVO<TEntity, TVO>(this TEntity entity)
            where TVO : IVO<TEntity>, new()
        {
            var _result = new TVO();
            _result.CopyFrom(entity);
            return _result;
        }

        public static IEnumerable<TEntity> Parse<TEntity>(this IEnumerable<IVO<TEntity>> vos)
            where TEntity : new()
        {
            foreach (var vo in vos)
                yield return vo.CreateEntity();
        }
        public static IEnumerable<TVO> Parse<TEntity, TVO>(this IEnumerable<TEntity> entities)
            where TVO : IVO<TEntity>, new()
        {
            foreach (var entity in entities)
            {
                var _vo = new TVO();
                _vo.CopyFrom(entity);
                yield return _vo;
            }
        }
    }
}
