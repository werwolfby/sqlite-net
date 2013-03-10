msbuild.exe ..\SQLite.sln /property:Configuration=Release /m 
sn -R "..\WindowsStore\bin\Release\OutcoldSolutions.SQLite.dll" %OutcoldTools%\OutcoldSolutions.snk
sn -R "..\Windows\bin\Release\OutcoldSolutions.SQLite.dll" %OutcoldTools%\OutcoldSolutions.snk
nuget pack "OutcoldSolutions.SQLite.nuspec" -basepath "..\\"