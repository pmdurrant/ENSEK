dotnet sonarscanner begin /k:"ENSEK" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_9ee8ff5b1d4174443ed0ec89a99bddbbd17fa865"
dotnet build .\ENSEK-API\ENSEK-API.csproj
dotnet sonarscanner end /d:sonar.token="sqp_9ee8ff5b1d4174443ed0ec89a99bddbbd17fa865"   
REM procep.exe
REM java