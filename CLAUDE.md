# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a Unity VR darts game project targeting Meta Quest/XR platforms using the Universal Render Pipeline (URP). The project leverages Unity MCP (Model Context Protocol) for AI-assisted development and procedural content generation.

**Product Name:** Darts
**Unity Version:** 2022.3+ (URP 17.2.0)
**Target Platform:** Meta Quest (Android IL2CPP)
**Render Pipeline:** Universal Render Pipeline (URP)

## Key Dependencies

### Critical Packages
- **Unity AI MCP** (`com.unity.ai.mcp`): Local package at `C:/Unity/unity-mcp/Packages/com.unity.ai.mcp` - Enables AI-driven Unity Editor control via MCP protocol
- **Meta XR SDK** (v78.0.0): Full Meta Quest/XR development stack including core, interaction, audio, voice, platform, and simulator
- **Unity Input System** (1.14.2): New input system for VR controllers and hand tracking
- **URP** (17.2.0): Universal Render Pipeline for optimized VR rendering

## MCP Integration

### Unity MCP Tools
This project is configured to work with Unity MCP for AI-assisted development. The MCP server must be running and connected to Unity Editor for Claude Code to interact with the project.

**Connection Check:**
```bash
claude mcp list
```

**Reconnect if needed:**
Use the `/mcp` command in Claude Code to reconnect to Unity MCP.

**Available Unity MCP Operations:**
- `manage_scene` - Get scene hierarchy, load/save scenes
- `manage_gameobject` - Create, modify, delete GameObjects; add/remove components
- `manage_asset` - Import assets, create materials/prefabs, search assets
- `import_external_model` - Import FBX models from external sources (NOT GLB)
- `read_console` - Read Unity Console logs for debugging
- `manage_script` - Create and manage C# scripts

**Important Notes:**
- Unity Editor must be open with this project loaded
- The Unity MCP Bridge must be active (tools will not be available otherwise)
- When importing 3D models, prefer FBX format over GLB - Unity MCP's `import_external_model` only accepts FBX
- GLB files can be placed in Assets but won't auto-instantiate; use FBX for direct scene placement

### Meshy AI Integration
The project has access to Meshy AI MCP for procedural 3D model generation:

**Workflow for generating models:**
1. `create_text_to_3d_task` with prompt and mode ("preview" for quick results)
2. Poll with `retrieve_text_to_3d_task` until status is "SUCCEEDED"
3. Download FBX format (not GLB) for Unity compatibility
4. Use Unity MCP's `import_external_model` to bring into scene

**Model formats available:** GLB, FBX, OBJ, USDZ (always use FBX for Unity)

## Project Structure

### Scene Organization
- **Main Scene:** `Assets/Scenes/SampleScene.unity`
- Scene contains: Main Camera, Directional Light, Global Volume, Floor, Table with children (TableTop, TableLeg1-4)

### Asset Organization
- **ExternalModels/** - Procedurally generated or imported 3D models (auto-created by Unity MCP)
- **Materials/** - Material assets
- **Scenes/** - Unity scene files
- **Settings/** - Project settings and configuration
- **StreamingAssets/** - Runtime-loaded assets
- **Oculus/** - Meta XR platform-specific assets

### Build Configuration
- **Android Settings:**
  - Min SDK: 23
  - Scripting Backend: IL2CPP
  - Target Architectures: ARM64
  - Graphics APIs: OpenGLES3, Vulkan

## Development Workflow

### Working with VR GameObjects
When creating or modifying VR-related GameObjects, remember:
- Table height is at y=0.75 (top surface at y=0.8)
- Use `manage_gameobject` to get component info before modifying positions
- Parent VR objects appropriately to maintain scene hierarchy

### Scene Hierarchy Pattern
The project follows this hierarchy for interactive objects:
```
Scene Root
├── Environment (Floor, Table, etc.)
├── Interactables (physics objects, game pieces)
└── XR Origin (camera/controllers, added by Meta XR)
```

### Creating New Assets
When creating new game assets:
1. Use Unity MCP tools rather than manual file operations
2. Place generated models in `Assets/ExternalModels/{ModelName}/`
3. Prefabs are auto-created by `import_external_model` for reuse
4. Materials should go in `Assets/Materials/`

### Debugging
- Use `read_console` to check Unity Console for errors/warnings
- Check logs with filter options: Error, Warning, Log
- Include stack traces for debugging: set `IncludeStacktrace: true`

## Common Patterns

### Importing and Placing 3D Models
```
1. Generate or download FBX model
2. Call import_external_model with:
   - Name: Asset identifier
   - FbxUrl: Local file path or URL
   - Height: Desired height in Unity units
3. Model is imported, instantiated, and prefab created automatically
4. Use manage_gameobject to position in scene
```

### Getting Scene Context Before Modifications
```
1. Call manage_scene GetHierarchy to see all objects
2. Call manage_gameobject get_components on target object
3. Read transform position/rotation/scale from component data
4. Calculate new positions relative to existing objects
5. Apply modifications with manage_gameobject modify action
```

## Platform-Specific Notes

### Meta Quest/XR
- Project configured for Meta Quest development with full SDK
- XR Simulator (v78.1) installed for testing without headset
- Hand tracking and controller support via Meta XR Interaction SDK
- Spatial audio enabled via Meta XR Audio SDK

### Input System
- Uses new Unity Input System (not legacy)
- Input actions defined in `Assets/InputSystem_Actions.inputactions`
- VR controllers and hand gestures configured via Meta XR SDK

## File Conventions

- **Scripts:** Place custom C# scripts in appropriate subdirectories under Assets/
- **Scenes:** Main gameplay scenes in `Assets/Scenes/`
- **Generated Content:** External models auto-organized in `Assets/ExternalModels/{Name}/`
- **Prefabs:** Created automatically by import workflow; reusable GameObjects

## MCP Best Practices

### When Unity MCP Tools Are Unavailable
If MCP tools aren't showing as available:
1. Verify Unity Editor is open with this project
2. Check MCP connection: `/mcp` command to reconnect
3. Ensure `com.unity.ai.mcp` package is properly installed (check `Packages/manifest.json`)
4. Look for MCP Bridge status in Unity Editor

### Optimal Tool Usage
- Read scene hierarchy before making modifications
- Use get_components to understand object state before changes
- Import FBX (not GLB) for external models
- Check console logs after operations to verify success
- remember always generate textures using Meshy