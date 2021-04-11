// 
// === vue.config.js is how you modify the default webpack configuration produced by the vue cli
//


const isProduction = ['production'].includes(process.env.NODE_ENV);
const usingDevServer = process.env.WEBPACK_DEV_SERVER === 'true';

module.exports = {
	//
	// The base URL your application bundle will be deployed at (see https://cli.vuejs.org/config/#publicpath)
	//
	publicPath: "/",

	//
	// The directory where the production build files will be generated (see https://cli.vuejs.org/config/#outputdir)
	//
	outputDir: "../wwwroot/",

	//
	// The directory (relative to outputDir) to nest generated static assets (js, css, img, fonts) under (see https://cli.vuejs.org/config/#assetsdir)
	//
	assetsDir: "app",

	//
	// Optionally add cache busting hash to generated js/css files
	//
	filenameHashing: true,

	//
	// This can get annoying, finds errors as you save
	//
	lintOnSave: false,

	//
	// === Development Server
	//
	devServer: {
		progress: false,
		//,host:'admin.dealervision.local'
	},

	//
	// === Modify Vue/Webpack Plugin Defaults
	//
	chainWebpack: (config) => {
		//
		// When not running webpack's dev server
		//
		if (process.env.WEBPACK_DEV_SERVER !== "true") {
			//
			// Update the HtmlWebpackPlugin to inject _VueScripts.cshtml partial
			// with link  s to js, css, etc instead of index.html.
			//
			config.plugin("html").tap((args) => {
				args[0] = {
					inject: false,
					template: "./src/assets/_VueScripts.template",
					filename: "../views/shared/_VueScripts.cshtml",
					minify: false,
				};
				return args;
			});

			//
			// Disabing 'hot module reload' since the proxy won't inject our razor pages
			// and we can use LiveReload to capture both razor and client side changes
			//
			config.plugins.delete("hmr");
		}
	},

	//
	// === Configure source-maps
	//
	configureWebpack: {
		//
		// Using source-map allows VS Code to correctly debug inside vue files
		//
		devtool: "source-map",

		// Breakpoints in VS and VSCode won’t work since the source maps
		// consider ClientApp the project root, rather than its parent folder
		output: {

        	devtoolModuleFilenameTemplate: (info) => {

                const resourcePath = info.resourcePath.replace(
                    "./src/",
                    "./ClientApp/src/"
                );

				//console.log(`${info.resourcePath} -> ${resourcePath} ? ${info.loaders} `);
                
                return info.loaders
                    ? `webpack:///${resourcePath}?${info.loaders}`
                    : `webpack:///${resourcePath}`;
            },
		},
	},
};