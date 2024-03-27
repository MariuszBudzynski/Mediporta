namespace Mediporta.Extensions
{
    public static class Extensions
    {
        public static TMappedObj Mapp<TSource, TMappedObj>(this TSource source, Func<TSource, TMappedObj> mappfunction)
        {
            return mappfunction(source);
        }
    }
}
