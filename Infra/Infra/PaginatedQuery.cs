using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class PaginatedQuery
    {
        int _pageSize;
        int _page;

        protected PaginatedQuery()
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

    public interface IPaginatedCollection<T> : IEnumerable<T>
    {
        Pagination Pagination { get; }
    }

    public class Pagination
    {
        public Pagination(PaginatedQuery query, int total)
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
