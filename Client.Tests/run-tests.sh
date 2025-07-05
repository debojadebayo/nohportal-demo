#!/bin/bash

# Playwright Test Runner Script for NationOH Client.Tests
# This script sets up and runs the Playwright.NET tests with proper configuration

set -e

echo "🎭 NationOH Playwright Test Runner"
echo "=================================="

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Default values
TEST_PATTERN=""
HEADLESS=true
BROWSER="chromium"
WORKERS=1
TIMEOUT=30000
OUTPUT_DIR="TestResults"
TAKE_SCREENSHOTS=true
RECORD_VIDEO=false
GENERATE_TRACE=false

# Function to display help
show_help() {
    echo "Usage: $0 [OPTIONS]"
    echo ""
    echo "Options:"
    echo "  -h, --help                    Show this help message"
    echo "  -p, --pattern PATTERN         Run tests matching pattern (e.g., 'Customer*')"
    echo "  -b, --browser BROWSER         Browser to use (chromium, firefox, webkit)"
    echo "  -w, --workers COUNT           Number of parallel workers (default: 1)"
    echo "  -t, --timeout MS              Test timeout in milliseconds (default: 30000)"
    echo "  -o, --output DIR              Output directory for results (default: TestResults)"
    echo "  --headed                      Run tests in headed mode (show browser)"
    echo "  --no-screenshots             Disable screenshots on failure"
    echo "  --record-video               Record video of test execution"
    echo "  --generate-trace             Generate Playwright traces"
    echo "  --forms-only                 Run only form validation tests"
    echo "  --smoke-only                 Run only smoke tests"
    echo "  --accessibility-only         Run only accessibility tests"
    echo ""
    echo "Examples:"
    echo "  $0                           Run all tests"
    echo "  $0 --headed --browser firefox Run in Firefox with visible browser"
    echo "  $0 --pattern 'Customer*'     Run only customer-related tests"
    echo "  $0 --forms-only              Run only form validation tests"
}

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        -h|--help)
            show_help
            exit 0
            ;;
        -p|--pattern)
            TEST_PATTERN="$2"
            shift 2
            ;;
        -b|--browser)
            BROWSER="$2"
            shift 2
            ;;
        -w|--workers)
            WORKERS="$2"
            shift 2
            ;;
        -t|--timeout)
            TIMEOUT="$2"
            shift 2
            ;;
        -o|--output)
            OUTPUT_DIR="$2"
            shift 2
            ;;
        --headed)
            HEADLESS=false
            shift
            ;;
        --no-screenshots)
            TAKE_SCREENSHOTS=false
            shift
            ;;
        --record-video)
            RECORD_VIDEO=true
            shift
            ;;
        --generate-trace)
            GENERATE_TRACE=true
            shift
            ;;
        --forms-only)
            TEST_PATTERN="*Forms*"
            shift
            ;;
        --smoke-only)
            TEST_PATTERN="*Navigation*"
            shift
            ;;
        --accessibility-only)
            TEST_PATTERN="*Accessibility*"
            shift
            ;;
        *)
            echo -e "${RED}Unknown option: $1${NC}"
            show_help
            exit 1
            ;;
    esac
done

# Check if we're in the right directory
if [ ! -f "Client.Tests.csproj" ]; then
    echo -e "${RED}Error: Client.Tests.csproj not found. Please run this script from the Client.Tests directory.${NC}"
    exit 1
fi

echo -e "${BLUE}Configuration:${NC}"
echo "  Browser: $BROWSER"
echo "  Headless: $HEADLESS"
echo "  Workers: $WORKERS"
echo "  Timeout: ${TIMEOUT}ms"
echo "  Output Directory: $OUTPUT_DIR"
echo "  Test Pattern: ${TEST_PATTERN:-'All tests'}"
echo ""

# Create output directory
mkdir -p "$OUTPUT_DIR"

# Install Playwright browsers if needed
echo -e "${YELLOW}Checking Playwright browsers...${NC}"
if ! dotnet run --project . -- install --help > /dev/null 2>&1; then
    echo -e "${YELLOW}Installing Playwright browsers...${NC}"
    pwsh bin/Debug/net9.0/playwright.ps1 install
fi

# Set environment variables for test configuration
export PLAYWRIGHT_BROWSERS_PATH=~/.cache/ms-playwright
export PLAYWRIGHT_JUNIT_OUTPUT_NAME="$OUTPUT_DIR/test-results.xml"

# Build test arguments
TEST_ARGS=()

if [ ! -z "$TEST_PATTERN" ]; then
    TEST_ARGS+=("--filter" "FullyQualifiedName~$TEST_PATTERN")
fi

TEST_ARGS+=("--logger" "trx;LogFileName=$OUTPUT_DIR/test-results.trx")
TEST_ARGS+=("--logger" "console;verbosity=normal")

if [ "$HEADLESS" = false ]; then
    export HEADED=true
fi

export BROWSER=$BROWSER
export TAKE_SCREENSHOTS=$TAKE_SCREENSHOTS
export RECORD_VIDEO=$RECORD_VIDEO
export GENERATE_TRACE=$GENERATE_TRACE

echo -e "${YELLOW}Building test project...${NC}"
dotnet build

echo -e "${YELLOW}Running tests...${NC}"
echo ""

# Run the tests
if dotnet test "${TEST_ARGS[@]}" --configuration Debug --verbosity normal; then
    echo ""
    echo -e "${GREEN}✅ Tests completed successfully!${NC}"
    
    # Check for test results
    if [ -f "$OUTPUT_DIR/test-results.trx" ]; then
        echo -e "${BLUE}Test results saved to: $OUTPUT_DIR/test-results.trx${NC}"
    fi
    
    # Check for screenshots
    SCREENSHOT_COUNT=$(find "$OUTPUT_DIR" -name "*.png" 2>/dev/null | wc -l)
    if [ $SCREENSHOT_COUNT -gt 0 ]; then
        echo -e "${YELLOW}📸 $SCREENSHOT_COUNT screenshots captured in $OUTPUT_DIR${NC}"
    fi
    
    # Check for videos
    VIDEO_COUNT=$(find "$OUTPUT_DIR" -name "*.webm" 2>/dev/null | wc -l)
    if [ $VIDEO_COUNT -gt 0 ]; then
        echo -e "${YELLOW}🎥 $VIDEO_COUNT videos recorded in $OUTPUT_DIR${NC}"
    fi
    
    # Check for traces
    TRACE_COUNT=$(find "$OUTPUT_DIR" -name "*.zip" 2>/dev/null | wc -l)
    if [ $TRACE_COUNT -gt 0 ]; then
        echo -e "${YELLOW}🔍 $TRACE_COUNT trace files generated in $OUTPUT_DIR${NC}"
        echo -e "${BLUE}View traces with: npx playwright show-trace $OUTPUT_DIR/*.zip${NC}"
    fi
    
    exit 0
else
    echo ""
    echo -e "${RED}❌ Tests failed!${NC}"
    
    # Show failure details
    if [ -f "$OUTPUT_DIR/test-results.trx" ]; then
        echo -e "${RED}Check test results: $OUTPUT_DIR/test-results.trx${NC}"
    fi
    
    # Show screenshots if available
    SCREENSHOT_COUNT=$(find "$OUTPUT_DIR" -name "*.png" 2>/dev/null | wc -l)
    if [ $SCREENSHOT_COUNT -gt 0 ]; then
        echo -e "${YELLOW}📸 $SCREENSHOT_COUNT failure screenshots available in $OUTPUT_DIR${NC}"
    fi
    
    exit 1
fi
