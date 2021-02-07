namespace BlazorCanvas.Example11.Core.Assets
{
    public class ImageFormatUtils
    {
        public static ImageFormat FromPath(string path)
        {
            if (path.EndsWith(".png", System.StringComparison.OrdinalIgnoreCase)) 
                return ImageFormat.PNG;

            if (path.EndsWith(".jpg", System.StringComparison.OrdinalIgnoreCase) || path.EndsWith(".jpeg", System.StringComparison.OrdinalIgnoreCase)) 
                return ImageFormat.JPG;

            return ImageFormat.Unknown;
        }
    }
}