namespace S6A0702.VO
{
    public interface IVO<TEntity>
    {
        void CopyFrom(TEntity entity);
        void CopyTo(ref TEntity entity);
    }
}
