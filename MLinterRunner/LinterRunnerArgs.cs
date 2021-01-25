using System;
using System.Collections.Generic;
using System.Text;

namespace MLinterRunner
{
    using System.Collections.Generic;
    using PowerArgs;

    [TabCompletion]
    internal class LinterRunnerArgs
    {
        [HelpHook, ArgShortcut("-?"), ArgDescription("Shows this help")]
        public bool Help { get; set; }

        [ArgShortcut("-f"), ArgExistingFile, ArgDescription("Files that should be parsed")]
        public List<string> Files { get; set; }

        [ArgShortcut("-d"), ArgExistingDirectory, ArgDescription("Directories that should be parsed")]
        public List<string> Directories { get; set; }

        [ArgDefaultValue("dist"), ArgShortcut("-t"), ArgDescription("Folder where the generated code will be written to")]
        public string Dist { get; set; }

        [ArgDefaultValue(false), ArgShortcut("--changed"), ArgDescription("Only files that are changed according to git will be checked")]
        public bool ChangedFilesOnly { get; set; }
    }
}
