const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');

module.exports = (env, argv) => {

  const miniCssExtractPlugin = new MiniCssExtractPlugin({
    filename: "styles/[name].css",
    chunkFilename: "styles/[name].css"
  });

  const isProductionMode = argv && argv.mode === 'production';
  const BUNDLE_OUTPUT_DIR = isProductionMode ? 'wwwroot/dist' : 'wwwroot/dev';
  console.log(`PROD?: ${isProductionMode}`);
  console.log(`OUTPUT DIR: ${BUNDLE_OUTPUT_DIR}`);

  return {
    mode: isProductionMode ? "production" : "development",
    devtool: "source-map",
    resolve: {
      extensions: ['.js', '.ts', '.tsx']
    },
    entry: Object.assign({
      main: path.join(__dirname, './client/src/index.tsx'),
    }, isProductionMode 
        ? {} 
        : { 'styles-webpack': path.join(__dirname, './client/styles/styles.scss') }),
    output: path.join(__dirname, BUNDLE_OUTPUT_DIR),
    module: {
      rules: [
        {
          test: /\.tsx?$/,
          // include: /client/,
          use: 'ts-loader'
        },
        {
          test: /\.(png|jpg|jpeg|gif|svg)$/,
          use: [{
            loader: 'url-loader',
            options: {
              limit: 8192,
              name: 'images/[name].[ext]'
            }
          }]
        },
        {
          test: /\.scss$/,
          use: [

            {
              loader: isProductionMode 
                ? MiniCssExtractPlugin.loader
                : 'style-loader' 
            },
            {
              loader: "css-loader",
              options: {
                url: true,
                minimize: !isProductionMode
              }
            },
            {
              loader: "sass-loader",
              options: {
                relativeUrls: false
              }
            }
          ]
        },
        {
          test: /\.css$/,
          use: [
            {
              loader: isProductionMode 
                ? MiniCssExtractPlugin.loader
                : 'style-loader' 
            },
            {
              loader: "css-loader",
              options: {
                url: true,
                minimize: isProductionMode
              }
            }
          ]
        },
        {
          test: /\.(eot|svg|ttf|woff|woff2)$/,
          loader: 'file-loader',
          options: {
            name: "[name].[ext]?[hash]",
            outputPath: "fonts/"
          }
        }
      ]
    },
    output: {
      filename: 'js/[name].js',
      chunkFilename: "js/[name].js",
      path: path.join(__dirname, BUNDLE_OUTPUT_DIR),
      publicPath: "/"
    },
    plugins: [
      miniCssExtractPlugin
    ],
    optimization: {
      minimize: isProductionMode,
      noEmitOnErrors: true,
      minimizer: [
        new UglifyJsPlugin({
          cache: true,
          parallel: true,
          sourceMap: !isProductionMode
        }),
        new OptimizeCSSAssetsPlugin({
          assetNameRegExp: /\.css$/g,
          cssProcessor: require('cssnano'),
          cssProcessorOptions: {
            safe: true,
            discardComments: {
              removeAll: true
            }
          },
          canPrint: true
        })
      ]
    },
    stats: {
      modules: false
    }
  }
};
