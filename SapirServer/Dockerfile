FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env
WORKDIR /src
COPY . .
RUN dotnet restore "SapirServer/SapirServer.csproj"
RUN dotnet publish "SapirServer/SapirServer.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:7004
WORKDIR /publish
COPY --from=build-env /publish .
# 🔥 Copy the file/folder into the container
COPY file /publish/file
EXPOSE 7004
ENTRYPOINT ["dotnet", "SapirServer.dll"]