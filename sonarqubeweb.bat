cd C:\working\ENSEK\ENSEKWeb

dotnet sonarscanner begin /k:"ENSEKWeb" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_18344350e6674140dc15c10afc1a0bf3e85d9723"

dotnet build

dotnet sonarscanner end /d:sonar.token="sqp_18344350e6674140dc15c10afc1a0bf3e85d9723"

cd ..