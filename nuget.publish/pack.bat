msbuild.exe ..\SQLite.sln /property:Configuration=Release /m 
sn -R "..\WindowsRuntime\bin\Release\OutcoldSolutions.SQLite.dll" %OutcoldTools%\OutcoldSolutions.snk
nuget pack "..\WindowsRuntime\WindowsRuntime.csproj" -basepath "..\WindowsRuntime\bin\Release\" -prop Configuration=Release