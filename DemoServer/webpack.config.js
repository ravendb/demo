const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");
const TerserPlugin = require('terser-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = (env, argv) => {

  const miniCssExtractPlugin = new MiniCssExtractPlugin({
    filename: "styles/[name].css",
    chunkFilename: "styles/[name].css"
  });

  const htmlPlugin = new HtmlWebpackPlugin({
    template: path.join(__dirname, './client/src/index.html')
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
    module: {
      rules: [
        {
          test: /bootstrap\.native/,
          use: {
            loader: 'bootstrap.native-loader',
            options: {
              only: ['dropdown']
            }
          }
        },
        {
          test: /\.tsx?$/,
          // include: /client/,
          use: 'ts-loader'
        },
          {
              test: /\.(png|jpg|jpeg|gif|svg)$/,
              type: "asset",
              generator: {
                  filename: 'img/[name].[hash:8][ext][query]',
              }
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
                url: true
              }
            },
            {
              loader: "sass-loader"
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
                url: true
              }
            }
          ]
        },
          {
              test: /\.(eot|ttf|woff|woff2)$/,
              type: "asset",
              generator: {
                  filename: "fonts/[name].[hash:8][ext][query]"
              }
          },
      ]
    },
    output: {
      filename: 'js/[name].js',
      chunkFilename: "js/[name].js",
      path: path.join(__dirname, BUNDLE_OUTPUT_DIR),
      publicPath: "/"
    },
    plugins: [
      miniCssExtractPlugin, 
      htmlPlugin
    ],
    optimization: {
      minimize: isProductionMode,
      emitOnErrors: false,
      minimizer: [
        new TerserPlugin(),
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
    },
    devServer: {
        port: process.env.PORT,
        hot: true,
        compress: true,
        historyApiFallback: true,

        setupMiddlewares: function(middlewares) {
            console.log('Starting the development server... Port = ' + process.env.PORT + '\n');
            
            return middlewares;
        }
    }
  }
};
