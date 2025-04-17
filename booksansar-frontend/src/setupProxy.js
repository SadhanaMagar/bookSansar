const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function (app) {
    app.use(
        '/api',
        createProxyMiddleware({
            target: 'https://localhost:7104',
            changeOrigin: true,
            secure: false // Only for development with self-signed certs
        })
    );
};