﻿using System.Web;
using System.Web.Optimization;

namespace Bicikli_Admin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                        "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/Content/gmaps_picker_helper").Include(
                        "~/Scripts/gmaps_picker_helper.js"));

            bundles.Add(new ScriptBundle("~/Content/gmaps_showmap_helper").Include(
                        "~/Scripts/gmaps_showmap_helper.js"));

            bundles.Add(new ScriptBundle("~/Content/clearbox").Include(
                        "~/Scripts/clearbox.js"));
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/normalize.css",
                        "~/Content/main.css",
                        "~/Content/highlights.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}