import { mkdirSync } from "node:fs";
import { test, expect } from "@playwright/test";

const output = "evidence/branding/screenshots";
const sites = [
  { id: "corporate", url: "http://127.0.0.1:4175/" },
  { id: "engineering", url: "http://127.0.0.1:4173/" },
];

mkdirSync(output, { recursive: true });

for (const width of [390, 1280]) {
  for (const site of sites) {
    test(`${site.id} reference screenshot at ${width}px`, async ({ page }) => {
      await page.setViewportSize({ width, height: 900 });
      await page.goto(site.url);
      await expect(page.locator("h1")).toBeVisible();
      await page.screenshot({ path: `${output}/${site.id}-${width}.png`, fullPage: false });
    });
  }
}

test("engineering site retains the corporate identity contract", async ({ page }) => {
  await page.goto("http://127.0.0.1:4173/");
  const values = await page.evaluate(() => {
    const root = getComputedStyle(document.documentElement);
    const body = getComputedStyle(document.body);
    const heading = getComputedStyle(document.querySelector("h1"));
    return {
      surface: root.getPropertyValue("--ef-color-surface-primary").trim(),
      heading: root.getPropertyValue("--ef-color-text-heading").trim(),
      accent: root.getPropertyValue("--ef-color-accent-primary").trim(),
      secondaryAccent: root.getPropertyValue("--ef-color-accent-secondary").trim(),
      bodyFamily: body.fontFamily,
      headingFamily: heading.fontFamily,
      headingWeight: heading.fontWeight,
    };
  });

  expect(values).toEqual({
    surface: "#f2efe7",
    heading: "#202421",
    accent: "#905831",
    secondaryAccent: "#47756b",
    bodyFamily: "Manrope, system-ui, sans-serif",
    headingFamily: "Newsreader, Georgia, serif",
    headingWeight: "500",
  });
});
