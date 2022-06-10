using Microsoft.AspNetCore.Http;

namespace AP204_Pronia.Existent
{
    public static class Existent
    {
        public static bool isExtent(this IFormFile formFile ,int mb)
        {
            return formFile.ContentType.Contains("image") && formFile.Length < mb * 1024 * 1024;
        }
    }
}
