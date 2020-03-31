#!/usr/bin/env bash
exec dotnet ef -s ../src/Application.WebApi/Application.WebApi.csproj -p ../src/Application.Infrastructure/Application.Infrastructure.csproj "$@"
