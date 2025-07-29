/* eslint-env node */
import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';

let httpsConfig = false;

const baseFolder = process.env.APPDATA
    ? `${process.env.APPDATA}/ASP.NET/https`
    : `${process.env.HOME}/.aspnet/https`;

const certificateName = "polokwane_surf.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

// Ensure the cert folder exists
if (!fs.existsSync(baseFolder)) {
    fs.mkdirSync(baseFolder, { recursive: true });
}

// Export cert files if they don't exist
if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    const result = child_process.spawnSync('dotnet', [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ]);
    if (result.status !== 0) {
        throw new Error("Could not create certificate.");
    }
}

httpsConfig = {
    key: fs.readFileSync(keyFilePath),
    cert: fs.readFileSync(certFilePath),
};

export default defineConfig({
    base: '/', 
    plugins: [react()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url)),
        },
    },
    server: {
        https: httpsConfig,
        port: 49381,
    },
    build: {
        outDir: 'dist',
        emptyOutDir: true,
    },
});
