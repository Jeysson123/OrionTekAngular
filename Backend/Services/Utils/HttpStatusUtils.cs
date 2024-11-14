using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utils
{
    public class HttpStatusUtils
    {
        public static readonly string OK = "200";

        public static readonly string UNAUTHORIZED = "401";

        public static readonly string NOT_FOUND = "404";

        public static readonly string NO_CONTENT = "204";

        public static readonly string INTERNAL_ERROR = "500";

        private static HttpStatusDto _httpStatus = new HttpStatusDto();
        public static void UpdateStatus(HttpStatusDto httpStatus)
        {
            _httpStatus = httpStatus;
        }
        public async static Task<HttpStatusDto> GetStatus()
        {
            return await Task.FromResult(_httpStatus);
        }
    }
}
