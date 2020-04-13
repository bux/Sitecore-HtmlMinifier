using System;
using System.IO;
using System.Linq;
using System.Text;
using Bx.HtmlMinifier.Interfaces;
using Sitecore.Configuration;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;

namespace Bx.HtmlMinifier.Pipelines.RenderRendering {

    public class ExecuteCustomRenderer : ExecuteRenderer {

        private static string[] _ignoredUrls;

        private readonly IHtmlMinifierService _htmlMinifierService;

        public ExecuteCustomRenderer() {
            _htmlMinifierService = ServiceLocator.ServiceProvider.GetService(typeof(IHtmlMinifierService)) as IHtmlMinifierService;
        }

        public ExecuteCustomRenderer(IRendererErrorStrategy errorStrategy) : base(errorStrategy) {
            _htmlMinifierService = ServiceLocator.ServiceProvider.GetService(typeof(IHtmlMinifierService)) as IHtmlMinifierService;
        }

        private static string[] IgnoredUrls {
            get {
                if (_ignoredUrls == null || _ignoredUrls.Length == 0) {
                    var ignoredUrlsString = Settings.GetSetting(Constants.IgnoredUrls, "/sitecore");
                    _ignoredUrls = ignoredUrlsString.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.ToLowerInvariant()).ToArray();
                }

                return _ignoredUrls;
            }
        }

        protected override bool Render(Renderer renderer, TextWriter writer, RenderRenderingArgs args) {
            if (
                !Settings.GetBoolSetting(Constants.Enabled, false)
                || ShouldUrlBeIgnored(args.PageContext.RequestContext.HttpContext.Request.RawUrl)
            ) {
                return base.Render(renderer, writer, args);
            }

            try {
                var stringBuilder = new StringBuilder();

                using (var stringWriter = new StringWriter(stringBuilder)) {
                    renderer.Render(stringWriter);
                }

                var content = stringBuilder.ToString();

                if (Settings.GetBoolSetting(Constants.RemoveWhitespaces, false)) {
                    content = _htmlMinifierService.RemoveWhitespaces(content);
                }

                if (Settings.GetBoolSetting(Constants.RemoveLineBreaks, false)) {
                    content = _htmlMinifierService.RemoveLineBreaks(content);
                }

                writer.Write(content);
            } catch (Exception ex) {
                Log.Error("Error while rendering", ex, this);

                if (!IsHandledByErrorStrategy(renderer, ex, writer)) {
                    throw;
                }
            }

            return true;
        }

        private bool ShouldUrlBeIgnored(string currentUrl) {
            foreach (var ignoredUrl in IgnoredUrls) {
                if (currentUrl.ToLowerInvariant().Contains(ignoredUrl)) {
                    return true;
                }
            }

            return false;
        }

    }

}
