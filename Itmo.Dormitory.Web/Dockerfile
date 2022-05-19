#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Itmo.Dormitory.API/Itmo.Dormitory.API.csproj", "Itmo.Dormitory.API/"]
COPY ["Itmo.Dormitory.DataAccess/Itmo.Dormitory.DataAccess.csproj", "Itmo.Dormitory.DataAccess/"]
COPY ["Itmo.Dormitory.Domain/Itmo.Dormitory.Domain.csproj", "Itmo.Dormitory.Domain/"]
COPY ["Itmo.Dormitory.Core/Itmo.Dormitory.Core.csproj", "Itmo.Dormitory.Core/"]
COPY ["Itmo.Dormitory.Common/Itmo.Dormitory.Common.csproj", "Itmo.Dormitory.Common/"]
COPY ["Itmo.Dormitory.Web/Itmo.Dormitory.Web.csproj", "Itmo.Dormitory.Web/"]
RUN dotnet restore "Itmo.Dormitory.Web/Itmo.Dormitory.Web.csproj"
COPY . .
WORKDIR "Itmo.Dormitory.Web"
RUN dotnet build "Itmo.Dormitory.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Itmo.Dormitory.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Itmo.Dormitory.Web.dll"]