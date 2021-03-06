﻿using Microsoft.DotNet.Cli.Build.Framework;
using Microsoft.DotNet.InternalAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.DotNet.Cli.Build
{
    public class Monikers
    {
        public const string SharedFrameworkName = "Microsoft.NETCore.App";
        public const string CLISdkBrandName = "Microsoft .NET Core 1.0.0 RC2 - SDK Preview 1";
        public const string SharedFxBrandName = "Microsoft .NET Core 1.0.0 RC2 - Runtime";
        public const string SharedHostBrandName = "Microsoft .NET Core 1.0.0 RC2 - Host";

        public static string GetProductMoniker(BuildTargetContext c, string artifactPrefix, string version)
        {
            string rid = RuntimeEnvironment.GetRuntimeIdentifier();

            if (rid == "ubuntu.16.04-x64" || rid == "fedora.23-x64" || rid == "opensuse.13.2-x64")
            {
                return $"{artifactPrefix}-{rid}.{version}";
            }
            else
            {
                string osname = GetOSShortName();
                var arch = CurrentArchitecture.Current.ToString();
                return $"{artifactPrefix}-{osname}-{arch}.{version}";
            }
        }

        public static string GetBadgeMoniker()
        {
            switch (RuntimeEnvironment.GetRuntimeIdentifier())
            {
                case "ubuntu.16.04-x64":
                    return "Ubuntu_16_04_x64";
                case "fedora.23-x64":
                    return "Fedora_23_x64";
                case "opensuse.13.2-x64":
                    return "openSUSE_13_2_x64";
            }

            return $"{CurrentPlatform.Current}_{CurrentArchitecture.Current}";
        }

        public static string GetDebianSharedFrameworkPackageName(string sharedFrameworkNugetVersion)
        {
            return $"dotnet-sharedframework-{SharedFrameworkName}-{sharedFrameworkNugetVersion}".ToLower();
        }

        public static string GetDebianSharedHostPackageName(BuildTargetContext c)
        {
            return $"dotnet-host".ToLower();
        }

        public static string GetOSShortName()
        {
            string osname = "";
            switch (CurrentPlatform.Current)
            {
                case BuildPlatform.Windows:
                    osname = "win";
                    break;
                default:
                    osname = CurrentPlatform.Current.ToString().ToLower();
                    break;
            }

            return osname;
        }
    }
}
