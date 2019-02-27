cd ../
docker build -f ./Dummy.Api/Dockerfile -t dummy.api ./Dummy.Api
docker build -f ./Dummy.Service.Activities/Dockerfile -t dummy.service.activities ./Dummy.Service.Activities
docker build -f ./Dummy.Service.Identity/Dockerfile -t dummy.service.identity ./Dummy.Service.Identity