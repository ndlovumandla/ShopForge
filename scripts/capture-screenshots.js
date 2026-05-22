const { chromium } = require("playwright");
const fs = require("fs/promises");
const path = require("path");

const adminBaseUrl = process.env.SHOPFORGE_ADMIN_URL || "http://localhost:5089";
const apiBaseUrl = process.env.SHOPFORGE_API_URL || "http://localhost:5002";
const email = process.env.SHOPFORGE_ADMIN_EMAIL || "admin@shopforge.co.za";
const password = process.env.SHOPFORGE_ADMIN_PASSWORD || "Admin@123";
const outputDir = path.resolve(__dirname, "../docs/screenshots");

async function signIn(context) {
  const loginResponse = await context.request.post(`${apiBaseUrl}/api/auth/login`, {
    data: { email, password }
  });

  if (!loginResponse.ok()) {
    throw new Error(`API login failed with ${loginResponse.status()}`);
  }

  const result = await loginResponse.json();
  const auth = result.data;
  const user = auth.user;

  const sessionResponse = await context.request.post(`${adminBaseUrl}/auth/session`, {
    data: {
      token: auth.accessToken,
      refreshToken: auth.refreshToken,
      userId: user.id,
      userName: `${user.firstName} ${user.lastName}`,
      role: user.role
    }
  });

  if (!sessionResponse.ok()) {
    throw new Error(`Admin session creation failed with ${sessionResponse.status()}`);
  }
}

async function capture(page, route, fileName) {
  await page.goto(`${adminBaseUrl}${route}`, { waitUntil: "networkidle" });
  await page.screenshot({
    path: path.join(outputDir, fileName),
    fullPage: true
  });
}

(async () => {
  await fs.mkdir(outputDir, { recursive: true });

  const browser = await chromium.launch({
    channel: "msedge",
    headless: true
  });

  try {
    const context = await browser.newContext({
      viewport: { width: 1440, height: 1000 },
      deviceScaleFactor: 1
    });
    await signIn(context);

    const page = await context.newPage();
    await capture(page, "/dashboard", "admin-dashboard.png");
    await capture(page, "/products", "admin-products.png");
    await capture(page, "/products/new", "admin-product-form.png");

    const mobileContext = await browser.newContext({
      viewport: { width: 390, height: 844 },
      isMobile: true,
      hasTouch: true,
      deviceScaleFactor: 2
    });
    await signIn(mobileContext);
    const mobilePage = await mobileContext.newPage();
    await capture(mobilePage, "/dashboard", "admin-dashboard-mobile.png");
  } finally {
    await browser.close();
  }
})().catch((error) => {
  console.error(error);
  process.exit(1);
});
