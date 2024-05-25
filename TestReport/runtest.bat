dotnet test ..\ApiTesting.csproj --filter TestCategory=regression
livingdoc test-assembly ..\bin\Debug\net6.0\ApiTesting.dll -t ..\bin\Debug\net6.0\TestExecution.json --title "API Testing"
pause