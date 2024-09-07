using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace Ecommerce_Wolmart.API.Slug
{
    public class SlugGenerator
    {
        public static string GenerateSlug(string title)
        {
            // Chuyển thành chữ thường
            string slug = title.ToLowerInvariant();

            // Loại bỏ dấu tiếng Việt
            slug = RemoveDiacritics(slug);

            // Thay thế khoảng trắng và các ký tự đặc biệt bằng dấu gạch dưới
            slug = Regex.Replace(slug, @"\s+", "_"); // Thay thế khoảng trắng bằng dấu gạch dưới
            slug = Regex.Replace(slug, @"[^a-z0-9_]", ""); // Loại bỏ các ký tự không hợp lệ

            return slug;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
