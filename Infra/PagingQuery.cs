using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class PagingQuery
    {
        int _pageSize;
        int _page;

        protected PagingQuery()
        {
            _pageSize = 25;
            _page = 1;
        }

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("PageSize must be bigger than 0.");

                if (value > 1000)
                    throw new ArgumentOutOfRangeException("PageSize must be less than 1000.");

                _pageSize = value;
            }
        }

        public int Page
        {
            get { return _page; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Page must be bigger than 0.");

                _page = value;
            }
        }
    }

    public interface IPage<T> : IEnumerable<T>
    {
        Pagination Pagination { get; }
    }

    class Page<T> : IPage<T>
    {
        public Page(Pagination pagination, IEnumerable<T> items)
        {
            Pagination = pagination;
            Items = items;
        }

        public Pagination Pagination { get; }
        IEnumerable<T> Items { get; }
        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public static class Page
    {
        public static IPage<TResult> AsPage<T, TResult>(this IQueryable<T> source, PagingQuery query, Func<T, TResult> selector) =>
            source
                .AsPage(query)
                .Select(selector);

        public static IPage<T> AsPage<T>(this IQueryable<T> source, PagingQuery query) =>
            new Page<T>(
                new Pagination(query, source.Count()),
                source
                     .Skip((query.Page - 1) * query.PageSize)
                     .Take(query.PageSize)
                     .AsEnumerable());

        public static IPage<TResult> Select<T, TResult>(this IPage<T> source, Func<T, TResult> selector) =>
            new Page<TResult>(
                source.Pagination,
                source
                    .AsEnumerable()
                    .Select(selector));
    }


    public class Pagination
    {
        public Pagination(PagingQuery query, int total)
        {
            Page = query.Page;
            PageSize = query.PageSize;
            Total = total;
        }

        public int Page { get; set; }
        public int PageSize { get; private set; }
        public int Total { get; private set; }
        public int Pages
        {
            get { return (int)Math.Ceiling((double)Total / (double)PageSize); }
        }
    }
}
