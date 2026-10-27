import { defineConfig, devices } from "@playwright/test";

export default defineConfig({
  testDir: "./tests",
  testMatch: "branding.spec.mjs",
  timeout: 45_000,
  reporter: "list",
  workers: 1,
  use: { ...devices["Desktop Chrome"] },
  webServer: [
    {
      command: "python3 -m http.server 4173 -d dist",
      url: "http://127.0.0.1:4173/",
      reuseExistingServer: false,
      timeout: 30_000,
    },
    {
      command: "python3 -m http.server 4175 -d /private/tmp/echelon-foundry-branding/dist",
      url: "http://127.0.0.1:4175/",
      reuseExistingServer: false,
      timeout: 30_000,
    },
  ],
});
