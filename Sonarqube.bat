dotnet sonarscanner begin /k:"ENSEK" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_bb8136cf52e8e691a1c3840108d42b45f524f93f"
dotnet build .\ENSEK-API\ENSEK-API.csproj
dotnet sonarscanner end /d:sonar.token="sqp_bb8136cf52e8e691a1c3840108d42b45f524f93f"   
REM procep.exe
REM java
