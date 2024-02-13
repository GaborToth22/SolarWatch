import react from '@vitejs/plugin-react';
import { defineConfig, loadEnv } from 'vite';

export default defineConfig(({ mode }) => {
  // Load env file based on mode in the current working directory.
  // Set the third parameter to '' to load all env regardless of the VITE_ prefix.
  // eslint-disable-next-line no-undef
  const env = loadEnv(mode, process.cwd(), '')  //ez lehet nem kell, de ha amiatt romlik el hogy kitörlöd, kezed letöröm!!!!
  //const backendPort = env.VITE_BACKEND_PORT  env.VITE_BACKEND_DOCKER_PORT;
  
  return {
    plugins: [react()],
    server: {
      watch: {
        usePolling: true,
      },
      host: true, 
      port:5173,
      strictPort: true,
      proxy: {
        '/api': env.BACKEND_URL || `http://localhost:5104`,
    }
  }
  }
})
