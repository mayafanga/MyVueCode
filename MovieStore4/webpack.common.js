//使用多个配置
//这个文件被其他配置文件依赖
const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin'); //实现 文件的自动打包和引入
const CleanWebpackPlugin = require('clean-webpack-plugin'); //清除缓存中上一次打包的不用的文件
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const webpack = require('webpack');
const OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');
module.exports = {
    entry: {
        app: './src/index.js' //配置入口文件
    },
    output: { //配置出口文件
        filename: '[name].bundle.js', //配置输出文件名字的格式
        path: path.join(__dirname, './dist') //输出的绝对路径
    },
    plugins: [ //配置插件
        new HtmlWebpackPlugin({
            template: './index.html', //指定模板 html 文件
            filename: 'index.html', //输出的 HTML 文件名称
            minify: {
                removeComments: true,
                collapseWhitespace: true,
                removeRedundantAttributes: true,
                useShortDoctype: true,
                removeEmptyAttributes: true,
                removeStyleLinkTypeAttributes: true,
                keepClosingSlash: true,
                minifyJS: true,
                minifyCSS: true,
                minifyURLs: true
            }
        }),
        new CleanWebpackPlugin({ dry: true }), //清除缓存中上一次打包的不用的文件
        new webpack.HotModuleReplacementPlugin(), //热更新所需插件
        new webpack.NamedModulesPlugin(), //热更新所需插件
        new VueLoaderPlugin(), //webpack4配置中启用 vue-loader 必须启用 否则会报错
        new OptimizeCssAssetsPlugin({
            cssProcessor: require('cssnano'),
            cssProcessorPluginOptions: {
                preset: ['default', {
                    discardComments: { removeAll: true }
                }],
            },
            canPrint: true
        })
    ],
    performance: {
        hints: "warning",
        maxAssetSize: 3000000,
        maxEntrypointSize: 500000
    },
    resolve: { //给 vue 配置别名，import 引入 不用写易长串
        alias: {

        }
    }
}