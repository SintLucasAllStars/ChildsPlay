using System;
using System.Text.RegularExpressions;

namespace ProBuilder2.EditorCommon
{
    public enum VersionType
    {
        Final = 3,
        Beta = 2,
        Patch = 1
    }

    [Serializable]
    public struct pb_VersionInfo : IEquatable<pb_VersionInfo>, IComparable<pb_VersionInfo>
    {
        public int major;
        public int minor;
        public int patch;
        public int build;
        public VersionType type;
        public string text;
        public bool valid;

        public override bool Equals(object o)
        {
            return o is pb_VersionInfo && Equals((pb_VersionInfo) o);
        }

        public override int GetHashCode()
        {
            var hash = 13;

            unchecked
            {
                if (valid)
                {
                    hash = hash * 7 + major.GetHashCode();
                    hash = hash * 7 + minor.GetHashCode();
                    hash = hash * 7 + patch.GetHashCode();
                    hash = hash * 7 + build.GetHashCode();
                    hash = hash * 7 + type.GetHashCode();
                }
                else
                {
                    return text.GetHashCode();
                }
            }

            return hash;
        }

        public bool Equals(pb_VersionInfo version)
        {
            if (valid != version.valid)
                return false;

            if (valid)
                return major == version.major &&
                       minor == version.minor &&
                       patch == version.patch &&
                       type == version.type &&
                       build == version.build;

            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(version.text))
                return false;

            return text.Equals(version.text);
        }

        public int CompareTo(pb_VersionInfo version)
        {
            const int GREATER = 1;
            const int LESS = -1;

            if (Equals(version))
                return 0;
            if (major > version.major)
                return GREATER;
            if (major < version.major)
                return LESS;
            if (minor > version.minor)
                return GREATER;
            if (minor < version.minor)
                return LESS;
            if (patch > version.patch)
                return GREATER;
            if (patch < version.patch)
                return LESS;
            if ((int) type > (int) version.type)
                return GREATER;
            if ((int) type < (int) version.type)
                return LESS;
            if (build > version.build)
                return GREATER;
            return LESS;
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}{3}{4}", major, minor, patch, type.ToString().ToLower()[0], build);
        }

        /**
         *	Create a pb_VersionInfo type from a string.
         *	Ex: "2.5.3b1"
         */
        public static pb_VersionInfo FromString(string str)
        {
            var version = new pb_VersionInfo();
            version.text = str;

            try
            {
                var split = Regex.Split(str, @"[\.A-Za-z]");
                var type = Regex.Match(str, @"A-Za-z");
                int.TryParse(split[0], out version.major);
                int.TryParse(split[1], out version.minor);
                int.TryParse(split[2], out version.patch);
                int.TryParse(split[3], out version.build);
                version.type = GetVersionType(type != null && type.Success ? type.Value : "");
                version.valid = true;
            }
            catch
            {
                version.valid = false;
            }

            return version;
        }

        private static VersionType GetVersionType(string type)
        {
            if (type.Equals("b") || type.Equals("B"))
                return VersionType.Beta;
            if (type.Equals("p") || type.Equals("P"))
                return VersionType.Patch;

            return VersionType.Final;
        }
    }
}