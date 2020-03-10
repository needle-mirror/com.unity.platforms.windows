using Bee.Toolchain.Xcode;
using JetBrains.Annotations;
using Unity.BuildSystem.NativeProgramSupport;

[UsedImplicitly]
class CustomizerForTinyWindows: AsmDefCSharpProgramCustomizer
{
    public override string CustomizerFor => "Unity.Platforms.Common";

    public override void CustomizeSelf(AsmDefCSharpProgram program)
    {
        program.NativeProgram.Libraries.Add(c =>
            ((DotsRuntimeNativeProgramConfiguration) c).CSharpConfig.EnableManagedDebugging &&
            ((DotsRuntimeNativeProgramConfiguration) c).CSharpConfig.WaitForManagedDebugger &&
            c.Platform is WindowsPlatform, new SystemLibrary("user32.lib"));
    }
}
