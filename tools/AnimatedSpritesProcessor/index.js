const args = process.argv.slice(2);
if (!args.length) {
    console.log("please provide a valid path");
    return;
}

const fs = require('fs'),
    path = require('path'),
    sizeOfImage = require('image-size');

const directoryPath = args[0],
    fps = 12, //TODO: get from command line argument
    frameSize = {
        width: 150, //TODO: get from command line argument
        height: 150 //TODO: get from command line argument
    },
    name = 'warrior',  //TODO: get from command line argument
    outputFullPath = path.join(__dirname, name + '.json'), //TODO: get from command line argument
    validExts = ['.png'];

fs.readdir(directoryPath, function (err, files) {    
    if (err) {
        return console.log('Unable to scan directory: ' + err);
    }

    const result = {
        version: 1,
        name: name,
        animations: []
    }
    
    files.forEach( (file) => {    
        const ext = path.extname(file);
        if(validExts.indexOf(ext) < 0){
            console.log(`invalid extension, skipping file: ${file}`);
            return;
        }
            
        const imageFullPath = path.join(directoryPath, file),
            buffer = fs.readFileSync(imageFullPath);
        if(!buffer){
            console.log(`unable to read data from ${imageFullPath}`);
            return;
        }

        const imageMeta = sizeOfImage(imageFullPath);

        result.animations.push({
            name: path.basename(file, ext),
            imageData: buffer.toString('base64'),            
            imageMeta: imageMeta,
            frameSize: frameSize,
            fps: fps,
            loop: true //TODO: make this configurable
        });        
    });
    
    fs.writeFileSync(outputFullPath, JSON.stringify(result));
});