const { execSync } = require("child_process");
const os = require("os");

// Get all script names passed as arguments (e.g., 'flf', 'dev')
const scriptNames = process.argv.slice(2);

if (scriptNames.length === 0) {
  console.error("\nError: No script name provided.");
  console.error("Usage: node scripts/run-script.js <script1> [script2] ...\n");
  process.exit(1);
}

/**
 * Checks if a command exists on the system's PATH.
 */
function commandExists(cmd) {
  try {
    const checkCmd = os.platform() === "win32" ? "where" : "command -v";
    execSync(`${checkCmd} ${cmd}`, { stdio: "pipe" });
    return true;
  } catch (error) {
    return false;
  }
}

let packageManager;

// Determine which package manager is available.
if (commandExists("pnpm")) {
  packageManager = "pnpm";
} else if (commandExists("yarn")) {
  packageManager = "yarn";
} else if (commandExists("npm")) {
  packageManager = "npm";
} else {
  console.error(
    "\nError: Could not find 'pnpm', 'yarn', or 'npm' in your system's PATH.",
  );
  console.error("Please install a package manager.\n");
  process.exit(1);
}

console.log(`> Using package manager: ${packageManager}`);
console.log(`> Working directory: ${process.cwd()}`);

// Execute in same order as given
for (const scriptName of scriptNames) {
  const finalCommand = `${packageManager} run ${scriptName}`;
  console.log(`\n> Executing command: ${finalCommand}\n`);

  try {
    // Execute the command, showing its output directly in the Zed terminal.
    execSync(finalCommand, { stdio: "inherit" });
  } catch (error) {
    console.error(
      `\nError: Command "${finalCommand}" failed. Halting execution.`,
    );
    process.exit(1); // Exit immediately if any command fails
  }
}

console.log("\n✅ All scripts completed successfully.");
