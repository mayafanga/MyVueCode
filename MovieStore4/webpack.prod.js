//定义生产配置
const merge = require('webpack-merge');
const common = require('./webpack.common.js');
const MiniCssExtractPlugin = require('mini-css-extract-plugin'); //压缩 css 文件
module.exports = merge(common, {
    mode: 'production', //压缩代码
    devtool: 'source-map',
    module: { //配置第三方 loader
        rules: [{
                test: require.resolve('jquery'),
                use: [
                    { loader: 'expose-loader', options: 'jQuery' },
                    { loader: 'expose-loader', options: '$' }
                ]
            }, //引入jquery 同时在 index.js中加上 require('jquery.js')
            { test: /\.css$/, use: ['style-loader', 'css-loader'] }, //处理 css 文件的 loader
            { test: /\.(png|jpg|jpeg|svg|gif)$/, use: ['file-loader'] }, //处理 图片的 loader
            { test: /\.(woff|woff2|eot|ttf|otf)$/, use: ['file-loader'] }, //处理 字体的 loader
            { test: /\.scss$/, use: [{ loader: "style-loader" }, { loader: "css-loader" }, { loader: "sass-loader" }] }, //处理 sass 文件的 loader
            { test: /\.less$/, use: ['style-loader', 'css-loader', 'less-loader'] }, //处理 less 文件的 loader
            { test: /\.vue$/, use: ['vue-loader'] }, //处理 .vue 文件的 loader
            {
                test: /\.js$/,
                use: {
                    loader: 'babel-loader'
                },
                exclude: /node_modules/
            }, //处理 ES6 的 loader
            { test: /\.(mpeg|mp4|webm|ogv)$/, use: ['file-loader'] } //处理视频的相关loader
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({ //压缩 css 文件
            filename: 'style.css'
        })
    ]
})