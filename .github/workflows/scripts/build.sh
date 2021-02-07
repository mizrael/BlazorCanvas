#!/bin/bash

rm -rf build
mkdir build
cp readme.md build/readme.md

publish(){
    local projectName=$1
    filename=$(basename -- "$projectName")    
    filename="${filename%.*}"
    buildPath="build/$filename-t"    
	dotnet publish --configuration Release $projectName --output $buildPath --no-build --no-restore
    mv $buildPath/wwwroot/ "build/$filename"        
    rm -rf $buildPath

    sed -i -e "s/<base href=\"\/\" \/>/<base href=\"\/BlazorCanvas\/$filename\/\" \/>/g" build/$filename/index.html
}

dotnet restore
dotnet build -c Release --no-restore

projects=($(ls -1 -- **/*.csproj))
for i in "${projects[@]}"; do publish "$i" & done