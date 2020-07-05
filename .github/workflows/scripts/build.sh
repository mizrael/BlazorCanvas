projects=($(ls -1 -- **/*.csproj))
for i in "${projects[@]}"
do
    filename=$(basename -- "$i")    
    filename="${filename%.*}"
    buildPath="build/$filename-t"    
	dotnet publish --configuration Release $i --output $buildPath
    mv $buildPath/wwwroot/ "build/$filename"    
    #cp "build/$filename/.nojekyll" "build/.nojekyll"       
    rm -rf $buildPath
done