# ComposedHealthBase Module Generation and Project Workflow

This document outlines the steps to generate a new module, integrate it into the application, build, and run the project.

## Generating a New Module

1.  **Navigate to the Server Directory**:
    Open your terminal and change to the `Server` directory:
    ```sh
    cd Server
    ```

2.  **Make the Script Executable (if needed)**:
    If you haven't done so already, make the `generate-module.sh` script executable:
    ```sh
    chmod +x generate-module.sh
    ```

3.  **Run the Generation Script**:
    Execute the script:
    ```sh
    ./generate-module.sh
    ```

4.  **Enter Module Name**:
    When prompted, type the desired name for your new module (e.g., `Billing`, `Reporting`) and press Enter. The script will then generate the necessary files and directory structure for your module under `Server/Modules/YourModuleName`.

## Integrating the New Module

After the module is generated, you need to integrate it into the main Web API project.

1.  **Navigate to the WebApi Directory**:
    ```sh
    cd WebApi
    ```
    (Assuming you are still in the `Server` directory. If you are in the root, it would be `cd Server/WebApi`)

2.  **Add Project Reference to `WebApi.csproj`**:
    Open the `Server/WebApi/WebApi.csproj` file. Add a `<ProjectReference>` to your newly generated module's `.csproj` file. It should look similar to the existing module references. For a module named `YourModuleName`, you would add a reference to its infrastructure project:
    ```xml
    <!-- filepath: Server/WebApi/WebApi.csproj -->
    <!-- ...existing code... -->
    <ItemGroup>
      <!-- ...existing project references... -->
      <ProjectReference Include="..\Modules\YourModuleName\Infrastructure\Server.Modules.YourModuleName.Infrastructure.csproj" />
    </ItemGroup>
    <!-- ...existing code... -->
    ```
    *Self-correction: The `generate-module.sh` script output indicates that the main module project (e.g., `Server.Modules.YourModuleName.csproj`) should be referenced by the startup project, and the infrastructure project is referenced by the main module project. However, the user's instructions specifically ask to add a reference "in keeping with how they look in there" for `WebApi.csproj`. Looking at the provided `Server/WebApi/Program.cs`, it uses `AuthModule`, `ClinicalModule`, etc., which are defined in their respective `Infrastructure` projects (e.g., `Server.Modules.Auth.Infrastructure.AuthModule`). The `generate-module.sh` script also creates `YourModuleNameModule.cs` inside the `Infrastructure` directory of the new module. Therefore, the reference should indeed be to the Infrastructure project of the new module.*

    *Correction based on `generate-module.sh` output for next steps (step 2): "Register the module in your main application's Program.cs... builder.Services.AddYourModuleNameModule(builder.Configuration);". This implies the `YourModuleNameModule` class, which is generated in the `Infrastructure` project, needs to be accessible. The `generate-module.sh` script also mentions in step 4: "Make sure your main API/Server project (the startup project) references the main module project ($MAIN_PROJECT_NAME)." and "The main module project, in turn, references the infrastructure project." This seems contradictory to the direct usage in `Program.cs`. However, the `Program.cs` directly uses types like `AuthModule` from `Server.Modules.Auth.Infrastructure`. The `generate-module.sh` creates `YourModuleNameModule.cs` in `Server.Modules.YourModuleName.Infrastructure`. So, `WebApi.csproj` should reference the `Infrastructure` project of the new module to access `YourModuleNameModule`.*

    *Final decision: The user's instruction to "add a reference to your new module in keeping with how they look in there" and the existing `Program.cs` structure (using `AuthModule` from `Server.Modules.Auth.Infrastructure`) suggests referencing the new module's `Infrastructure` project in `WebApi.csproj`.*

    For a module named `MyNewModule`, the reference would be:
    ```xml
    <!-- filepath: Server/WebApi/WebApi.csproj -->
    <!-- ...existing code... -->
    <ItemGroup>
      <!-- ...existing project references... -->
      <ProjectReference Include="..\Modules\MyNewModule\Infrastructure\Server.Modules.MyNewModule.Infrastructure.csproj" />
    </ItemGroup>
    <!-- ...existing code... -->
    ```

3.  **Update `Program.cs`**:
    Open the [Server/WebApi/Program.cs](Server/WebApi/Program.cs) file.
    *   Add a `using` statement for your new module's `Infrastructure` namespace. For a module named `MyNewModule`:
        ```csharp
        // filepath: Server/WebApi/Program.cs
        // ...existing code...
        using Server.Modules.Scheduling.Infrastructure;
        using Server.Modules.Auth.Infrastructure;
        using Server.Modules.MyNewModule.Infrastructure; // Add this line

        var builder = WebApplication.CreateBuilder(args);
        // ...existing code...
        ```
    *   Add your new module's type (e.g., `MyNewModuleModule`) to the `moduleTypes` list:
        ```csharp
        // filepath: Server/WebApi/Program.cs
        // ...existing code...
        var moduleTypes = new List<Type>
        {
            typeof(AuthModule),
            typeof(ClinicalModule),
            typeof(CRMModule),
            typeof(SchedulingModule),
            typeof(MyNewModuleModule) // Add this line
        };
        // ...existing code...
        ```

## Building the Project

1.  **Navigate to the Server Directory**:
    If you are not already there, change to the `Server` directory from the project root:
    ```sh
    cd Server
    ```

2.  **Build the Project**:
    Run the .NET build command:
    ```sh
    dotnet build
    ```

## Running the Application

1.  **Navigate to the Project Root Directory**:
    Ensure you are in the root directory of the project (where the `docker-compose.yaml` file is located).
    ```sh
    cd .. 
    ```
    (If you are in `Server/WebApi`, you'd type `cd ../..`. If in `Server`, type `cd ..`)

2.  **Run with Docker Compose**:
    Use Docker Compose to build and run the application:
    ```sh
    docker-compose up --build
    ```

## Troubleshooting: Cleaning Build Files

If you encounter issues, especially after changes or failed builds, you can clean up previous build artifacts.

1.  **Navigate to the Project Root Directory**:
    Ensure you are in the root directory of the project.

2.  **Run the Hard Clean Script**:
    Execute the [hardclean.sh](hardclean.sh) script:
    ```sh
    ./hardclean.sh
    ```
    (You might need to make it executable first: `chmod +x hardclean.sh`)

This script will help remove Docker containers, images, and other build-related files to ensure a fresh start.