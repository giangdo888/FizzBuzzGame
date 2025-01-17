import { defineConfig } from 'vite'

export default defineConfig({
    // Default configuration for Vite
    server: {
        // Configure the server settings
        host: '0.0.0.0',
        port: 3000,
        open: false, // Automatically open the browser
    },
    build: {
        // Build options (optional)
        outDir: 'dist', // Output directory
        sourcemap: true, // Enable sourcemaps for easier debugging
    },
    plugins: [
        // You can add plugins here
    ],
})
