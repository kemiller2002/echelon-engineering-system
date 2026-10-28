import { mkdirSync, readFileSync, statSync } from "node:fs";
import { test, expect } from "@playwright/test";

const output = "output/pdf";
const documents = [
  { route: "/system/", file: "echelon-system-architecture.pdf", heading: "A system of bounded responsibilities" },
  { route: "/components/", file: "echelon-component-inventory.pdf", heading: "One bounded job per system" },
  { route: "/process/", file: "echelon-engineering-lifecycle.pdf", heading: "From requirement to verified implementation" },
  { route: "/research/", file: "echelon-research-summaries.pdf", heading: "Observed, interpreted, or still hypothesized" },
];

mkdirSync(output, { recursive: true });

test.describe.configure({ mode: "serial" });

for (const document of documents) {
  test(`generates Letter PDF: ${document.file}`, async ({ page }) => {
    await page.goto(document.route);
    await expect(page.getByRole("heading", { level: 1, name: document.heading })).toBeVisible();
    await page.evaluate(() => document.fonts.ready);
    await page.pdf({
      path: `${output}/${document.file}`,
      format: "Letter",
      preferCSSPageSize: true,
      printBackground: false,
      tagged: true,
      outline: true,
    });

    const path = `${output}/${document.file}`;
    expect(statSync(path).size).toBeGreaterThan(10_000);
    expect(readFileSync(path).subarray(0, 5).toString()).toBe("%PDF-");
  });
}

test("all representative print documents use Folio primitives", async ({ page }) => {
  for (const document of documents) {
    await page.goto(document.route);
    await page.emulateMedia({ media: "print" });
    await expect(page.locator("ef-print-document")).toHaveCount(1);
    expect(await page.locator("ef-print-section").count()).toBeGreaterThanOrEqual(2);
    await expect(page.locator("ef-print-header")).toContainText("Echelon / Engineering");
    await expect(page.locator("ef-print-footer")).toContainText("echelonfoundry.com");
  }
});
