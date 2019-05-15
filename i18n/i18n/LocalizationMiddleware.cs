namespace i18n
{
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// The LocalizationMiddleware class.
    /// </summary>
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizationMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Task.</returns>
        public async Task Invoke(HttpContext context)
        {
            var localValue = "en-US";
            if (context.Request.Headers.ContainsKey("locale"))
            {
                localValue = context.Request.Headers["locale"];
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo(localValue);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(localValue);

            await _next.Invoke(context);
        }
    }

    /// <summary>
    /// The LocalizationMiddlewareExtensions class.
    /// </summary>
    public static class LocalizationMiddlewareExtensions
    {
        /// <summary>
        /// Uses the localization middleware.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The ApplicationBuilder.</returns>
        public static IApplicationBuilder UseLocalizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LocalizationMiddleware>();
        }
    }
}