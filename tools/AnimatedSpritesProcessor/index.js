const commandLineArgs = require('command-line-args'),
    fs = require('fs'),
    path = require('path'),
    sizeOfImage = require('image-size');
const { exit } = require('process');

const mainDefinitions = [
    { name: 'path', defaultOption: true, description: 'path containing the spritesheets to combine' },
    { name: 'fps', alias: 'f', type: Number },
    { name: 'framesCount', alias: 'c', type: Number },
    { name: 'width', alias: 'w', type: Number, description: 'frame width' },
    { name: 'height', alias: 'h', type: Number, description: 'frame height' },
    { name: 'output', alias: 'o', type: String },
],
arguments = commandLineArgs(mainDefinitions, { stopAtFirstUnknown: true });

if(!arguments.path){
    console.error("invalid path");
    exit(-1);
}

const directoryPath = arguments.path,
    fps = arguments.fps || 12,
    framesCount = arguments.framesCount || null,
    frameSize = {
        width: arguments.width || 150,
        height: arguments.height || 150
    },
    name = arguments.output,
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

        const imageMeta = sizeOfImage(imageFullPath),
            cols = imageMeta.width / frameSize.width,
            rows = imageMeta.height / frameSize.height
            computedFramesCount = cols*rows;

        result.animations.push({
            name: path.basename(file, ext),
            imageData: buffer.toString('base64'),            
            imageMeta: imageMeta,
            frameSize: frameSize,
            framesCount: framesCount || computedFramesCount,
            fps: fps
        });        
    });
    
    fs.writeFileSync(outputFullPath, JSON.stringify(result));
});