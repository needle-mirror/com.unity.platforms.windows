using Bee.Toolchain.Windows;
using DotsBuildTargets;
using Unity.BuildSystem.NativeProgramSupport;
abstract class DotsWindowsTarget : DotsBuildSystemTarget
{
    protected override ToolChain ToolChain => new WindowsToolchain(WindowsSdk.Locatorx64.UserDefaultOrDummy);
}

class DotsWindowsDotNetTarget : DotsWindowsTarget
{
    protected override string Identifier => "windows-dotnet";

    protected override ScriptingBackend ScriptingBackend => ScriptingBackend.Dotnet;

    // burst is busted and always tries to build 32-bit here
    protected override bool CanUseBurst => true;
}

abstract class DotsWindows32Target : DotsBuildSystemTarget
{
    protected override ToolChain ToolChain => new WindowsToolchain(WindowsSdk.Locatorx86.UserDefaultOrDummy);
}

class DotsWindows32DotNetTarget : DotsWindows32Target
{
    protected override string Identifier => "windows32-dotnet";

    protected override ScriptingBackend ScriptingBackend => ScriptingBackend.Dotnet;
    protected override bool CanUseBurst => false;
}

class DotsWindowsIL2CPPTarget : DotsWindowsTarget
{
    protected override string Identifier => "windows-il2cpp";
    protected override bool CanUseBurst => true;
}
