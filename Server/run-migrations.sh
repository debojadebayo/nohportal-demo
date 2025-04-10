#!/bin/bash

# Navigate to the Server directory
cd "$(dirname "$0")"

# Loop through each module's Database folder
for module in Modules/*/Infrastructure/Database; do
    if [ -d "$module" ]; then
        # Extract the module name from the path
        module_name=$(basename "$(dirname "$module")")
        
        # Run the migration command
        dotnet ef migrations add FixIds --context "${module_name}DbContext" --output-dir Database --project "$module" --startup-project "$(pwd)/WebApi/WebApi.csproj"
    fi
done