cd ../
dotnet publish ./Dummy.Api -c Release -o ./bin/Docker
dotnet publish ./Dummy.Service.Activities -c Release -o ./bin/Docker
dotnet publish ./Dummy.Service.Identity -c Release -o ./bin/Docker