# MLinter
This is a small customizable linter to help find problems in .net files.

run >  ./MLinterRunner.exe -?  

Usage - MLinterRunner -options  

GlobalOption                   Description  
Help (-?)                      Shows this help  
Files (-f)                     Files that should be parsed  
Directories (-d)               Directories that should be parsed  
Dist (-t)                      Folder where the generated code will be  
                               written to [Default='dist'], not used yet  
ChangedFilesOnly (--changed)   Only files that are changed according to git  
                               will be checked [Default='False']  
