import { test, expect } from "@playwright/test";
import AxeBuilder from "@axe-core/playwright";

const routes = ["/", "/system/", "/process/", "/agents/", "/components/", "/research/", "/tour/", "/reference/"];
const widths = [320, 390, 768, 851, 1000, 1280];

const horizontalOverflow = page => page.evaluate(() => ({
  document: document.documentElement.scrollWidth - document.documentElement.clientWidth,
  body: document.body.scrollWidth - document.body.clientWidth,
}));

for (const width of widths) {
  test(`all routes reflow without horizontal overflow at ${width}px`, async ({ page }) => {
    await page.setViewportSize({ width, height: 900 });
    for (const route of routes) {
      await page.goto(route);
      await expect(page.locator("h1")).toBeVisible();
      const overflow = await horizontalOverflow(page);
      expect(overflow, `${route} overflow at ${width}px`).toEqual({ document: 0, body: 0 });
    }
  });
}

test("primary content and navigation remain semantic on every route", async ({ page }) => {
  for (const route of routes) {
    await page.goto(route);
    await expect(page.getByRole("banner")).toHaveCount(1);
    await expect(page.getByRole("main")).toHaveCount(1);
    await expect(page.getByRole("contentinfo")).toHaveCount(1);
    await expect(page.getByRole("heading", { level: 1 })).toHaveCount(1);
    await expect(page.locator('.ef-site-nav__link[aria-current="page"]')).toHaveCount(1);
    await expect(page.locator('script[src$="assets/folio/register.js"]')).toHaveCount(1);
  }
});

test("keyboard navigation exposes the skip link and a visible focus indicator", async ({ page }) => {
  await page.goto("/");
  await page.keyboard.press("Tab");
  const skip = page.getByRole("link", { name: "Skip to main content" });
  await expect(skip).toBeFocused();
  await expect(skip).toBeVisible();
  await page.keyboard.press("Enter");
  await expect(page.locator("#main-content")).toBeFocused();

  await page.goto("/");
  await page.keyboard.press("Tab");
  await page.keyboard.press("Tab");
  const focused = page.locator(":focus");
  const style = await focused.evaluate(element => {
    const value = getComputedStyle(element);
    return { outlineStyle: value.outlineStyle, outlineWidth: value.outlineWidth, boxShadow: value.boxShadow };
  });
  expect(style.outlineStyle !== "none" || style.boxShadow !== "none").toBeTruthy();
});

test("reduced motion disables animated transitions", async ({ page }) => {
  await page.emulateMedia({ reducedMotion: "reduce" });
  await page.goto("/");
  const duration = await page.locator(".ef-button").first().evaluate(element => getComputedStyle(element).transitionDuration);
  expect(Number.parseFloat(duration)).toBeLessThanOrEqual(0.00001);
});

test("200 percent text scaling preserves reflow", async ({ page }) => {
  await page.setViewportSize({ width: 1280, height: 900 });
  await page.goto("/components/");
  await page.addStyleTag({ content: "html { font-size: 200% !important; }" });
  const overflow = await horizontalOverflow(page);
  expect(overflow).toEqual({ document: 0, body: 0 });
  await expect(page.getByRole("heading", { level: 1 })).toBeVisible();
});

test("adversarial long content stays inside its containers", async ({ page }) => {
  await page.setViewportSize({ width: 320, height: 900 });
  await page.goto("/components/");
  await page.evaluate(() => {
    const card = document.querySelector(".ef-card");
    if (card) {
      card.insertAdjacentHTML("beforeend", `<p>${"StateDirectedEngineeringVerificationBoundary".repeat(10)}</p><p><a href="#">https://evidence.example/${"unbroken-segment".repeat(20)}</a></p>`);
    }
  });
  const overflow = await horizontalOverflow(page);
  expect(overflow).toEqual({ document: 0, body: 0 });
});

test("content remains usable when Forma fails to load", async ({ page }) => {
  await page.route("**/assets/forma/*.css", route => route.abort("failed"));
  await page.goto("/");
  await expect(page.getByRole("heading", { level: 1 })).toBeVisible();
  await expect(page.getByRole("link", { name: "Start the guided tour" })).toBeVisible();
  await expect(page.getByRole("main")).toContainText("Engineering what makes AI-built software trustworthy");
});

test("empty and missing evidence states remain understandable", async ({ page }) => {
  await page.goto("/reference/");
  await expect(page.getByText("Known limitations", { exact: true })).toBeVisible();
  await expect(page.getByText("Current limitation", { exact: true })).toBeVisible();
  await page.goto("/components/");
  await expect(page.getByText("Causa is documented without inventing a package", { exact: true })).toBeVisible();
  await expect(page.getByRole("row", { name: /Causa/ })).toContainText("Documented");
});

test("four static diagrams expose equivalent text relationships", async ({ page }) => {
  const expected = [
    ["/system/", "#d01"],
    ["/process/", "#d02"],
    ["/agents/", "#d03"],
    ["/components/", "#d04"],
  ];

  for (const [route, id] of expected) {
    await page.goto(route);
    const diagram = page.locator(id);
    await expect(diagram).toHaveCount(1);
    await expect(diagram.locator("figcaption")).not.toBeEmpty();
    await expect(diagram.locator(".ef-diagram__viewport")).toHaveAttribute("tabindex", "0");
    await expect(diagram.locator("marker")).toHaveAttribute("markerUnits", "userSpaceOnUse");
    await expect(diagram.locator("marker")).toHaveAttribute("markerWidth", "8");
    expect(await diagram.locator(".ef-diagram__relations li").count()).toBeGreaterThanOrEqual(4);
  }
});

test("system architecture uses a non-scrolling mobile relationship layout", async ({ page }) => {
  await page.setViewportSize({ width: 390, height: 844 });
  await page.goto("/system/");

  const diagram = page.locator("#d01");
  await expect(diagram.locator(":scope > .site-diagram-desktop")).toBeHidden();
  await expect(diagram.locator(":scope > .site-diagram-mobile")).toBeVisible();
  await expect(diagram.locator(".site-diagram-mobile__path")).toHaveCount(4);
  await expect(diagram.locator(".site-diagram-mobile__arrow")).toHaveCount(7);

  const mobileSize = await diagram.locator(":scope > .site-diagram-mobile").evaluate(element => ({
    clientWidth: element.clientWidth,
    scrollWidth: element.scrollWidth,
  }));
  expect(mobileSize.scrollWidth).toBeLessThanOrEqual(mobileSize.clientWidth);

  await page.setViewportSize({ width: 1280, height: 900 });
  await expect(diagram.locator(":scope > .site-diagram-desktop")).toBeVisible();
  await expect(diagram.locator(":scope > .site-diagram-mobile")).toBeHidden();
});

test("the removed Indy Init page is absent from routes and navigation", async ({ page }) => {
  const response = await page.goto("/indy-init/");
  expect(response?.status()).toBe(404);
  await page.goto("/");
  await expect(page.getByRole("navigation").getByRole("link", { name: "Indy Init" })).toHaveCount(0);
});

test("axe finds no WCAG 2.2 A or AA violations", async ({ page }) => {
  await page.setViewportSize({ width: 390, height: 844 });
  for (const route of routes) {
    await page.goto(route);
    const result = await new AxeBuilder({ page }).withTags(["wcag2a", "wcag2aa", "wcag21a", "wcag21aa", "wcag22aa"]).analyze();
    expect(result.violations, `${route}: ${result.violations.map(item => `${item.id}: ${item.help}`).join("; ")}`).toEqual([]);
  }
});

test("print preview uses Folio elements and hides screen chrome", async ({ page }) => {
  await page.goto("/process/");
  await page.emulateMedia({ media: "print" });
  await expect(page.locator("ef-print-document")).toHaveCount(1);
  expect(await page.locator("ef-print-section").count()).toBeGreaterThanOrEqual(2);
  const printHeaderDisplay = await page.locator("ef-print-header.site-print-only").evaluate(element => getComputedStyle(element).display);
  expect(printHeaderDisplay).toBe("grid");
  await expect(page.locator("ef-print-header.site-print-only")).toContainText("Echelon / Engineering");
  await expect(page.locator("header.ef-site-header")).toBeHidden();
  const pageSize = await page.evaluate(() => getComputedStyle(document.documentElement).getPropertyValue("--ef-print-page-size").trim());
  expect(pageSize).toBe("Letter");
});
