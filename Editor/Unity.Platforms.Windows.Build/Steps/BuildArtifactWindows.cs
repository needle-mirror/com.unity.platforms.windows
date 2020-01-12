using System.IO;
using Unity.Platforms.Build;

namespace Unity.Platforms.Windows.Build
{
    // TODO: Don't make this class public until platform team reviews the fields inside, there is a concern that FileInfo is not a correct field
    //       Due to fact build can produce multiple files instead of one
    sealed class BuildArtifactWindows : IBuildArtifact
    {
        public FileInfo OutputTargetFile;
    }
}
