using FastEndpoints;
using System.ComponentModel;

namespace UserManager.Contracts.Requests
{
    public class GetAllUsersRequest
    {
        /// <summary>
        /// Page number. Defaults to 1.
        /// </summary>
        [QueryParam, BindFrom("page")]
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Number of items per page. Defaults to 10.
        /// </summary>
        [QueryParam, BindFrom("perPage")]
        [DefaultValue(10)]
        public int PerPage { get; set; } = 10;
    }
}
