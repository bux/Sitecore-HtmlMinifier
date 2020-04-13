# Sitecore-HtmlMinifier
A cacheable Sitecore HTML Minifier pipeline

This plugin will minify HTML markup by removing whitespaces and linebreaks. It will do it for each rendering by injecting itself into the `ExecuteRendering` pipeline thus making the output cacheable.

## Installation
1. Clone repository
2. Adjust Sitecore and Microsoft nuget packages to match the versions you use (will be optimized in the future)
3. Build the project and copy the dll to the `bin\` folder
4. Copy the config files to `App_Config\Include\zzz\` and adjust the values
