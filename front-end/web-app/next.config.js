/** @type {import('next').NextConfig} */
const nextConfig = {
    images: {
        domains: ['cdn.pixabay.com']
    },
    experimental:{
        serverActions: true
    }
}

module.exports = nextConfig
