using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.Coverlet.CoverletTasks;
using static Nuke.Common.Tools.ReportGenerator.ReportGeneratorTasks;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.ReportGenerator;
using Nuke.Common.Tools.DotCover;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Report);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    private AbsolutePath CoverageDirectory => RootDirectory / "TestCoverage/Results";

    private AbsolutePath CoverageReportDirectory => RootDirectory / "TestCoverage/Report";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    Target Restore => _ => _
        .Executes(() =>
        {
        });

    Target Compile => _ => _
    .DependsOn(Restore)
    .Executes(() =>
    {
        DotNetBuild(s => s
            .SetProjectFile(RootDirectory / "UserManager.sln")
            .SetConfiguration(Configuration)
        );
    });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            CoverageDirectory.CreateOrCleanDirectory();
            var testDll = RootDirectory / "UserManager.Test" / "bin" / Configuration / "UserManager.Test.dll";
            Coverlet(s => s
                .SetTarget("dotnet")
                .SetProcessWorkingDirectory(RootDirectory / "UserManager.Test")
                .SetTargetArgs("test --no-build --no-restore")
                .SetAssembly(testDll)
                .AddExclude("[*]Program") // entry point
                .AddExclude("[*]*Summary") // Endpoints documentation
                //.SetThreshold(80)
                .SetOutput(CoverageDirectory / "cobertura.xml")
                .SetFormat(CoverletOutputFormat.cobertura));
        });

    Target Report => _ => _
        .DependsOn(Test)
        .AssuredAfterFailure()
        .Executes(() =>
        {
            CoverageReportDirectory.CreateOrCleanDirectory();
            ReportGenerator(s => s
                    .SetTargetDirectory(CoverageReportDirectory)
                    .SetFramework("net6.0")
                    .SetReportTypes(new ReportTypes[] { ReportTypes.Html })
                    .SetReports(CoverageDirectory / "cobertura.xml"));
        });
}
