SonarScanner.MSBuild.exe begin /k:"ENSEK" /d:sonar.host.url="http://localhost:9000" /d:sonar.token="sqp_a1094c292f9fa16d41451a2f69d6612a8780e42e"
dotnet build .\ENSEK-API\ENSEK-API.csproj
SonarScanner.MSBuild.exe end /d:sonar.token="sqp_a1094c292f9fa16d41451a2f69d6612a8780e42e"
REM procep.exe
REM java